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
        private ServiceProvider serviceProvider;


        public Bootstrap()
        {
            this.notifyIcon = new NotifyIcon(this.container)
            {
                Visible = true,
            };

            this.ConfigureDependancies();

            this.clipboardMonitor.ClipboardChanged += this.ClipboardMonitor_ClipboardChanged;
        }

        public void Dispose()
        {
            (this.serviceProvider as IDisposable)?.Dispose();
            this.notifyIcon?.Dispose();
            this.container?.Dispose();
            this.clipboardMonitor?.Dispose();
            this.key?.Dispose();
        }

        private void ConfigureDependancies()
        {
            // Add configure services
            var serviceDescriptors = new ServiceCollection();

            serviceDescriptors.AddSingleton<IAction, ConvertActions>();
            serviceDescriptors.AddSingleton<IAction, CountdownActions>();
            serviceDescriptors.AddSingleton<IAction, DeviceActions>();
            serviceDescriptors.AddSingleton<IAction, RandomActions>();
            serviceDescriptors.AddSingleton<IAction, StopwatchActions>();
            serviceDescriptors.AddSingleton<IAction, TimespanActions>();
            serviceDescriptors.AddSingleton<IAction, TimezoneActions>();
            serviceDescriptors.AddSingleton<IAction, TryCalcBmi>();
            serviceDescriptors.AddSingleton<IAction, TryRedirect>();
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
                if (string.IsNullOrWhiteSpace(clipboardText))
                {
                    return;
                }

                // if we get to here, we have text
                this.ProcessClipboardText(clipboardText);
            }
        }

        private void ProcessClipboardText(string clipboardText)
        {
            try
            {

                var service = serviceProvider.GetServices<IAction>().FirstOrDefault(s => s.Matches(clipboardText));

                var service = this.serviceProvider.GetServices<IAction>().FirstOrDefault(s => s.Matches(clipboardText));
                this.clipboardMonitor.ClipboardChanged -= this.ClipboardMonitor_ClipboardChanged;

                ActionResult actionResult = null;

                // run the action
                if (service is object)
                {
                    actionResult = service.TryExecute(clipboardText);
                }

                // re attach the event
                if (actionResult is object && actionResult.IsProcessed)
                {
                    if (!string.IsNullOrWhiteSpace(actionResult.Title) ||
                        !string.IsNullOrWhiteSpace(actionResult.Description))
                    {

                        clipboardMonitor.ClipboardChanged -= ClipboardMonitor_ClipboardChanged;
                        ProcessResult(actionResult, clipboardText);
                        clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
                    }
                    return;
                }


                if (clipboardText.Length > 2)
                    foreach (var question in questionList)

                        this.ProcessResult(actionResult, clipboardText);
                    }

                    this.clipboardMonitor.ClipboardChanged += this.ClipboardMonitor_ClipboardChanged;
                    return;
                }

                if (clipboardText.Length > 2)
                {
                    foreach (var question in this.questionList)
                    {

                        if (question.Text.Contains(clipboardText))
                        {
                            this.ProcessResult(new ActionResult(question.Text, question.Answer), clipboardText);
                            return;
                        }
                    }
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
            if (string.Equals(Clipboard.GetText(), clipboardText, StringComparison.Ordinal))
            {
                Clipboard.Clear();
            }

            this.notifyIcon.Icon = SystemIcons.Exclamation;
            this.notifyIcon.BalloonTipTitle = actionResult.Title;
            this.notifyIcon.BalloonTipText = actionResult.Description;
            this.notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
            this.notifyIcon.ShowBalloonTip(1000);
        }

        internal void EnsureWindowStartup(bool isStartingWithWindows)
        {
            const string keyName = "Clipboard Assistant";
            var keyValue = Assembly.GetExecutingAssembly().Location;
            this.key?.Dispose();
            this.key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", writable: true);
            if (this.key is null)
            {
                return;
            }

            var value = this.key.GetValue(keyName, null) as string;

            if (isStartingWithWindows)
            {
                // key doesn't exist, add it
                if (string.IsNullOrWhiteSpace(value) && string.Equals(value, keyValue, StringComparison.Ordinal))
                {
                    this.key.SetValue(keyName, keyValue);
                }
            }
            else
                // if key exist, remove it
                if (!string.IsNullOrWhiteSpace(value))
                {
                    this.key.DeleteValue(keyName);
                }

            key.Close();
        }

        private RegistryKey key;
    }
}