namespace it
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using it.Actions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Win32;

    /// <summary>
    ///     The bootstrap class is provided to allow the application to run with out a form.
    ///     We can use a form however in the future by adding it to here.
    /// </summary>
    internal sealed class Bootstrap : IDisposable
    {
        private readonly ClipboardMonitor clipboardMonitor = new ClipboardMonitor();
        private readonly ControlContainer container = new ControlContainer();
        private readonly NotifyIcon notifyIcon;
        private readonly List<Question> questionList = Questions.LoadQuestions();

        // Container to hold the actions
        private IServiceProvider serviceProvider;


        public Bootstrap()
        {
            notifyIcon = new NotifyIcon(container)
            {
                Visible = true,
            };

            ConfigureDependancies();

            clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
        }


        public void Dispose()
        {
            (serviceProvider as IDisposable)?.Dispose();
            notifyIcon?.Dispose();
            container?.Dispose();
            clipboardMonitor?.Dispose();
        }

        private void ConfigureDependancies()
        {
            // Add configure services
            ServiceCollection serviceDescriptors = new ServiceCollection();

            serviceDescriptors.AddSingleton<IAction, ConvertActions>();
            serviceDescriptors.AddSingleton<IAction, TryRomanActions>();
            serviceDescriptors.AddSingleton<IAction, CountdownActions>();
            serviceDescriptors.AddSingleton<IAction, DeviceActions>();
            serviceDescriptors.AddSingleton<IAction, RandomActions>();
            serviceDescriptors.AddSingleton<IAction, StopwatchActions>();
            serviceDescriptors.AddSingleton<IAction, TimespanActions>();
            serviceDescriptors.AddSingleton<IAction, TimezoneActions>();
            serviceDescriptors.AddSingleton<IAction, MathActions>();
            (this.serviceProvider as IDisposable)?.Dispose();
            this.serviceProvider = serviceDescriptors.BuildServiceProvider();
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
                if (string.IsNullOrWhiteSpace(clipboardText)) return;

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
                var service = serviceProvider.GetServices<IAction>().FirstOrDefault(s => s.Matches(clipboardText));
                ActionResult actionResult = null;
                // run the action
                if (service is object) actionResult = service.TryExecute(clipboardText);
                // re attach the event
                if (actionResult is object && actionResult.IsProcessed)
                {
                    if (!String.IsNullOrWhiteSpace(actionResult.Title) || !String.IsNullOrWhiteSpace(actionResult.Description))
                    {
                        clipboardMonitor.ClipboardChanged -= ClipboardMonitor_ClipboardChanged;
                        ProcessResult(actionResult, clipboardText);
                        clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
                    }
                    return;
                }


                if (clipboardText.Length > 2)
                    foreach (var question in questionList)
                        if (question.Text.Contains(clipboardText))
                        {
                            ProcessResult(new ActionResult(question.Text, question.Answer), clipboardText);
                            return;
                        }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private static void ClearClipboard(string clipboardText)
        {
        }


        private void ProcessResult(ActionResult actionResult, string clipboardText)
        {
            if (clipboardText is null)
            {
                throw new ArgumentNullException(nameof(clipboardText));
            }

            if (string.Equals(Clipboard.GetText(), clipboardText, StringComparison.Ordinal)) Clipboard.Clear();

            notifyIcon.Icon = SystemIcons.Exclamation;
            notifyIcon.BalloonTipTitle = actionResult.Title;
            notifyIcon.BalloonTipText = actionResult.Description;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
            notifyIcon.ShowBalloonTip(1000);

        }

        internal static void EnsureWindowStartup(bool isStartingWithWindows)
        {
            string keyName = "Clipboard Assistant";
            string keyValue = Assembly.GetExecutingAssembly().Location;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key == null) return;

            string value = key.GetValue(keyName, null) as string;

            if (isStartingWithWindows)
            {
                // key doesn't exist, add it
                if (String.IsNullOrWhiteSpace(value) && string.Equals(value, keyValue, StringComparison.Ordinal))
                {
                    key.SetValue(keyName, keyValue);
                }
            }
            else
            {
                // if key exist, remove it
                if (!String.IsNullOrWhiteSpace(value))
                {
                    key.DeleteValue(keyName);
                }
            }

            key.Close();
        }
    }
}