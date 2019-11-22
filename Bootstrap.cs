using it.Actions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;


namespace it
{
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
                Visible = true
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
            IServiceCollection serviceDescriptors = new ServiceCollection();

            serviceDescriptors.AddSingleton<IAction, ConvertActions>();
            serviceDescriptors.AddSingleton<IAction, CountdownActions>();
            serviceDescriptors.AddSingleton<IAction, DeviceActions>();
            serviceDescriptors.AddSingleton<IAction, MathActions>();
            serviceDescriptors.AddSingleton<IAction, RandomActions>();
            serviceDescriptors.AddSingleton<IAction, StopwatchActions>();
            serviceDescriptors.AddSingleton<IAction, TimespanActions>();
            serviceDescriptors.AddSingleton<IAction, TimezoneActions>();
            serviceDescriptors.AddSingleton<IAction, TryCalcBmi>();
            serviceDescriptors.AddSingleton<IAction, TryRedirect>();
            serviceDescriptors.AddSingleton<IAction, TryWifiPass>();

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
                if (string.IsNullOrWhiteSpace(clipboardText)) return;

                // if we get to here, we have text
                ProcessClipboardText(clipboardText);
            }
        }

        private void ProcessClipboardText(string clipboardText)
        {
            try
            {
                var service = serviceProvider.GetServices<IAction>().FirstOrDefault(s => s.Matches(clipboardText));
                clipboardMonitor.ClipboardChanged -= ClipboardMonitor_ClipboardChanged;
                ActionResult actionResult = null;
                // run the action
                if (service is object) actionResult = service.TryExecute(clipboardText);
                // re attach the event
                if (actionResult is object &&
                    (!string.IsNullOrWhiteSpace(actionResult.Title) || !string.IsNullOrWhiteSpace(actionResult.Description)))
                {
                    ProcessResult(actionResult, clipboardText);
                    clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
                    return;
                }



                foreach (var action in new List<IAction>(serviceProvider.GetServices<IAction>()))
                {
                    // disconnect events from the clipboard.
                    clipboardMonitor.ClipboardChanged -= ClipboardMonitor_ClipboardChanged;
                    // run the action
                    actionResult = action.TryExecute(clipboardText);
                    // re attach the event
                    clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;

                    // if this action processed the command, exit the loop.
                    if (actionResult.IsProcessed)
                    {
                        // if the result is not null, and has a title and description
                        if (!string.IsNullOrWhiteSpace(actionResult.Title) ||
                            !string.IsNullOrWhiteSpace(actionResult.Description))
                        {
                            ProcessResult(actionResult, clipboardText);
                        }

                        break;
                    }
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
            if (string.Equals(Clipboard.GetText(), clipboardText, StringComparison.Ordinal)) Clipboard.Clear();

            notifyIcon.Icon = SystemIcons.Exclamation;
            notifyIcon.BalloonTipTitle = actionResult.Title;
            notifyIcon.BalloonTipText = actionResult.Description;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
            notifyIcon.ShowBalloonTip(1000);

        }

        internal void EnsureWindowStartup(bool isStartingWithWindows)
        {
            string keyName = "Clipboard Assistant";
            string keyValue = Assembly.GetExecutingAssembly().Location;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (key == null) return;

            string value = key.GetValue(keyName, null) as string;

            if (isStartingWithWindows)
            {
                // key doesn't exist, add it
                if (String.IsNullOrWhiteSpace(value) && value == keyValue)
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