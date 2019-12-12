namespace it
{
    using it.Actions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    ///     The bootstrap class is provided to allow the application to run with out a form.
    ///     We can use a form however in the future by adding it to here.
    /// </summary>
    internal class Bootstrap : IDisposable
    {
        private readonly ClipboardMonitor clipboardMonitor = new ClipboardMonitor();
        private readonly ControlContainer container = new ControlContainer();
        private readonly NotifyIcon notifyIcon;
        private readonly IReadOnlyList<Question> questionList = Questions.LoadQuestions();

        // Container to hold the actions
        private ServiceProvider serviceProvider;


        public Bootstrap()
        {
            notifyIcon = new NotifyIcon(container)
            {
                Visible = true,
            };


            ConfigureDependancies();

            clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
        }

        private bool disposed;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            disposed = true;

            if (disposing)
            {
                
            }
            if (serviceProvider != null)
            {
                (serviceProvider as IDisposable)?.Dispose();
                serviceProvider = null;
            }
            if (notifyIcon != null)
            {
                notifyIcon?.Dispose();
            }
            if (container != null)
            {
                container?.Dispose();
            }
            if (clipboardMonitor != null)
            {
                clipboardMonitor?.Dispose();
            }
        }


        private void ConfigureDependancies()
        {
            // Add configure services
            ServiceCollection serviceDescriptors = new ServiceCollection();

            _ = serviceDescriptors.AddSingleton<IAction, ConvertActions>();
            _ = serviceDescriptors.AddSingleton<IAction, TryRomanActions>();
            _ = serviceDescriptors.AddSingleton<IAction, CountdownActions>();
            _ = serviceDescriptors.AddSingleton<IAction, DeviceActions>();
            _ = serviceDescriptors.AddSingleton<IAction, RandomActions>();
            _ = serviceDescriptors.AddSingleton<IAction, StopwatchActions>();
            _ = serviceDescriptors.AddSingleton<IAction, TimespanActions>();
            _ = serviceDescriptors.AddSingleton<IAction, TimezoneActions>();
            _ = serviceDescriptors.AddSingleton<IAction, BmiActions>();
            _ = serviceDescriptors.AddSingleton<IAction, MathActions>();
            (serviceProvider as IDisposable)?.Dispose();
            serviceProvider = serviceDescriptors.BuildServiceProvider();
        }


        internal static void Startup()
        {
            // monitor the clipboard
        }

        private void ClipboardMonitor_ClipboardChanged(object sender, ClipboardChangedEventArgs e)
        {
            // retrieve the text from the clipboard
            if (e.DataObject.GetData(DataFormats.Text) is string clipboardText)
            {
                // the data is not a string. bail.
                if (string.IsNullOrWhiteSpace(clipboardText))
                {
                    return;
                }

                // if we get to here, we have text
                ProcessClipboardText(clipboardText);
            }
        }

        private void ProcessClipboardText(string clipboardText)
        {
            if (clipboardText is null)
            {
                throw new ArgumentNullException(nameof(clipboardText));
            }

            try
            {
                IAction service = serviceProvider.GetServices<IAction>().FirstOrDefault(s => s.Matches(clipboardText));
                if (service is object)
                {
                    ActionResult actionResult = service.TryExecute(clipboardText);
                    // re attach the event
                    if (!string.IsNullOrWhiteSpace(actionResult.Title) || !string.IsNullOrWhiteSpace(actionResult.Description))
                    {
                        clipboardMonitor.ClipboardChanged -= ClipboardMonitor_ClipboardChanged;
                        ProcessResult(actionResult, clipboardText);
                        clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
                    }
                    return;
                }

                if (clipboardText.Length > 2)
                {
                    foreach (Question question in questionList)
                    {
                        if (question.Text.Contains(clipboardText))
                        {
                            ProcessResult(new ActionResult(question.Text, question.Answer), clipboardText);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.ToString());
            }
        }

        private void ProcessResult(ActionResult actionResult, string clipboardText)
        {
            if (clipboardText is null)
            {
                throw new ArgumentNullException(nameof(clipboardText));
            }

            if (string.Equals(Clipboard.GetText(), clipboardText, StringComparison.Ordinal))
            {
                Clipboard.Clear();
            }

            notifyIcon.Icon = SystemIcons.Exclamation;
            notifyIcon.BalloonTipTitle = actionResult.Title;
            notifyIcon.BalloonTipText = actionResult.Description;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
            notifyIcon.ShowBalloonTip(1000);

        }

        internal static void EnsureWindowStartup(bool isStartingWithWindows)
        {
            const string keyName = "Clipboard Assistant";

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key is null)
            {
                return;
            }

            string value = key.GetValue(keyName, null) as string;

            if (isStartingWithWindows)
            {
                // key doesn't exist, add it
                if (string.IsNullOrWhiteSpace(value) && string.Equals(value, Assembly.GetExecutingAssembly().Location, StringComparison.Ordinal))
                {
                    key.SetValue(keyName, Assembly.GetExecutingAssembly().Location);
                }
            }
            else if (!string.IsNullOrWhiteSpace(value))
            {
                key.DeleteValue(keyName);
            }

            key.Close();
            key.Dispose();
        }
    }
}