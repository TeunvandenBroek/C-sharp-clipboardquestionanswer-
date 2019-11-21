using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using it.Actions;
using Microsoft.Extensions.DependencyInjection;

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
            notifyIcon = new NotifyIcon(container);
            notifyIcon.Visible = true;

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


        internal void Startup()
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
                GetAnswer(clipboardText);
            }
        }

        private void GetAnswer(string clipboardText)
        {
            try
            {

                //foreach (IAction action in serviceProvider.GetServices<IAction>())
                //{
                //    // disconnect events from the clipboard.
                //    clipboardMonitor.ClipboardChanged -= ClipboardMonitor_ClipboardChanged;
                //    // run the action
                //    ActionResult actionResult = action.TryExecute(clipboardText);
                //    // re attach the event
                //    clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;

                //    // if this action processed the command, exit the loop.
                //    if (actionResult.IsProcessed)
                //    {
                //        if (!String.IsNullOrWhiteSpace(actionResult.Title) || !String.IsNullOrWhiteSpace(actionResult.Description))
                //        {
                //            ShowNotification(actionResult);
                //            Clipboard.Clear();
                //        }
                //        break;
                //    }

                //}

                var service = serviceProvider.GetServices<IAction>().FirstOrDefault(s => s.Matches(clipboardText));
                clipboardMonitor.ClipboardChanged -= ClipboardMonitor_ClipboardChanged;
                // run the action
                ActionResult actionResult = service.TryExecute(clipboardText);
                // re attach the event
                clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
                if (!string.IsNullOrWhiteSpace(actionResult.Title) || !string.IsNullOrWhiteSpace(actionResult.Description))
                {
                    ShowNotification(actionResult);
                    Clipboard.Clear();
                }



                var actions = new List<IAction>(serviceProvider.GetServices<IAction>());
                foreach (var action in actions)
                {
                    // disconnect events from the clipboard.
                    clipboardMonitor.ClipboardChanged -= ClipboardMonitor_ClipboardChanged;
                    // run the action
                    var actionResult = action.TryExecute(clipboardText);
                    // re attach the event
                    clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;

                    // if this action processed the command, exit the loop.
                    if (actionResult.IsProcessed)
                    {
                        if (!string.IsNullOrWhiteSpace(actionResult.Title) ||
                            !string.IsNullOrWhiteSpace(actionResult.Description))
                        {
                            ShowNotification(actionResult);
                            Clipboard.Clear();
                        }

                        break;
                    }
                }

                if (clipboardText.Length > 2)
                    foreach (var question in questionList)
                        if (question.Text.Contains(clipboardText))
                        {
                            ShowNotification(new ActionResult(question.Text, question.Answer));
                            Clipboard.Clear();
                            return;
                        }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ShowNotification(ActionResult actionResult)
        {
            notifyIcon.Icon = SystemIcons.Exclamation;
            notifyIcon.BalloonTipTitle = actionResult.Title;
            notifyIcon.BalloonTipText = actionResult.Description;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
            notifyIcon.ShowBalloonTip(1000);
        }
    }
}