using it.Actions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace it
{
    /// <summary>
    /// The bootstrap class is provided to allow the application to run with out a form.
    /// We can use a form however in the future by adding it to here.
    /// </summary>
    internal sealed class Bootstrap
    {
        // notify icon
        ControlContainer container = new ControlContainer();
        private NotifyIcon notifyIcon = null;
        private readonly List<Question> questionList = Questions.LoadQuestions();

        // Container to hold the actions
        private IServiceProvider serviceProvider;

        public Bootstrap()
        {
            notifyIcon = new NotifyIcon(container);
            notifyIcon.Visible = true;

            ConfigureDependancies();
        }

        private void ConfigureDependancies()
        {
            // Add configure services
            IServiceCollection serviceDescriptors = new ServiceCollection();

            serviceDescriptors.AddSingleton<IAction, ConvertActions>();
            serviceDescriptors.AddSingleton<IAction, CountdownActions>();
            serviceDescriptors.AddSingleton<IAction, DeviceActions>();
            serviceDescriptors.AddSingleton<IAction, RandomActions>();
            serviceDescriptors.AddSingleton<IAction, StopwatchActions>();
            serviceDescriptors.AddSingleton<IAction, TimespanActions>();
            serviceDescriptors.AddSingleton<IAction, TimezoneActions>();
            serviceDescriptors.AddSingleton<IAction, TryCalcBmi>();
            serviceDescriptors.AddSingleton<IAction, TryRedirect>();
            serviceDescriptors.AddSingleton<IAction, TryWifiPass>();
            serviceDescriptors.AddSingleton<IAction, MathActions>();

            serviceProvider = serviceDescriptors.BuildServiceProvider();
        }


        internal void Startup()
        {
            // monitor the clipboard
            ClipboardMonitor clipboardMonitor = new ClipboardMonitor();
            clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
        }

        private void ClipboardMonitor_ClipboardChanged(object sender, ClipboardChangedEventArgs e)
        {
            // retrieve the text from the clipboard
            string clipboardText = e.DataObject.GetData(DataFormats.Text) as string;
            // the data is not a string. bail.
            if (string.IsNullOrWhiteSpace(clipboardText)) return;

            // if we get to here, we have text
            GetAnswer(clipboardText);
        }

        private void GetAnswer(string clipboardText)
        {
            try
            {
                foreach (IAction action in serviceProvider.GetServices<IAction>())
                {
                    QuestionAnswer questionAnswer = action.TryExecute(clipboardText);
                    // if this action processed the command, exit the loop.
                    if (questionAnswer.IsProcessed)
                    {
                        if (!String.IsNullOrWhiteSpace(questionAnswer.Question) || !String.IsNullOrWhiteSpace(questionAnswer.Answer))
                        {
                            ShowNotification(questionAnswer);
                            Clipboard.Clear();
                        }
                        break;
                    }
                }


                //if (clipboardText.Length > 2)
                //{
                //    foreach (Question question in questionList)
                //    {
                //        if (question.Text.Contains(clipboardText))
                //        {
                //            ShowNotification(new QuestionAnswer(question.Text, question.Answer));
                //            Clipboard.Clear();
                //            return;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void ShowNotification(Actions.QuestionAnswer questionAnswer)
        {
            notifyIcon.Icon = SystemIcons.Exclamation;
            notifyIcon.BalloonTipTitle = questionAnswer.Question;
            notifyIcon.BalloonTipText = questionAnswer.Answer;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
            notifyIcon.ShowBalloonTip(1000);
        }


    }
}
