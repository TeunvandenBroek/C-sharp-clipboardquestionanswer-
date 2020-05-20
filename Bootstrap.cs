namespace it
{
    using it.Actions;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Win32;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using System.Windows.Forms;



    /// <summary>
    ///     The bootstrap class is provided to allow the application to run with out a form.
    ///     We can use a form however in the future by adding it to here.
    /// </summary> 
    ///
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
            notifyIcon = new NotifyIcon(container)
            {
                Visible = true,
            };

            ConfigureDependancies();

            clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
        }

        private IntPtr handle;
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                (serviceProvider as IDisposable)?.Dispose();
                notifyIcon?.Dispose();
                notifyIcon.Icon?.Dispose();
                container?.Dispose();
                clipboardMonitor?.Dispose();
            }
            CloseHandle(handle);
            handle = IntPtr.Zero;
            disposed = true;
        }
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);
        ~Bootstrap()
        {
            Dispose(false);
        }

        private void ConfigureDependancies()
        {
            // Add configure services
            ServiceCollection serviceDescriptors = new ServiceCollection();
            _ = serviceDescriptors.AddSingleton<IAction, CurrencyConversion>();
            _ = serviceDescriptors.AddSingleton<IAction, ConvertActions>();
            _ = serviceDescriptors.AddSingleton<IAction, TryRomanActions>();
            _ = serviceDescriptors.AddSingleton<IAction, CountdownActions>();
            _ = serviceDescriptors.AddSingleton<IAction, DeviceActions>();
            _ = serviceDescriptors.AddSingleton<IAction, RandomActions>();
            _ = serviceDescriptors.AddSingleton<IAction, StopwatchActions>();
            _ = serviceDescriptors.AddSingleton<IAction, TimespanActions>();
            _ = serviceDescriptors.AddSingleton<IAction, numberToHex>();
            _ = serviceDescriptors.AddSingleton<IAction, desktopCleaner>();
            _ = serviceDescriptors.AddSingleton<IAction, TimezoneActions>();
            _ = serviceDescriptors.AddSingleton<IAction, BmiActions>();
            _ = serviceDescriptors.AddSingleton<IAction, tryBinary>();
            _ = serviceDescriptors.AddSingleton<IAction, Currency>();
            _ = serviceDescriptors.AddSingleton<IAction, Wallpaper>();
            _ = serviceDescriptors.AddSingleton<IAction, autoClicker>();
            _ = serviceDescriptors.AddSingleton<IAction, timeCalculations>();
            //_ = serviceDescriptors.AddSingleton<IAction, Weatherforecast>();
            _ = serviceDescriptors.AddSingleton<IAction, MathActions>();
            (serviceProvider as IDisposable)?.Dispose();
            serviceProvider = serviceDescriptors.BuildServiceProvider();
        }
        internal void Startup(string clipboardText)
        {

        }
        private bool clipboardPaused = false;
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
                if (clipboardPaused)
                {
                    if (clipboardText.Equals("hervat") || clipboardText.Equals("resume"))
                    {
                        clipboardPaused = false;
                    }
                    return;
                }
                if (clipboardText.Equals("pauze") || clipboardText.Equals("pause"))
                {
                    clipboardPaused = true;
                    return;
                }
                // if we get to here, we have text
                ProcessClipboardText(clipboardText);
            }
        }

        private bool notifyPaused = false;
        private void ProcessClipboardText(string clipboardText)
        {
            if (clipboardText is null)
            {
                throw new ArgumentNullException(nameof(clipboardText));
            }

            if (notifyPaused)
            {
                if (clipboardText.Equals("show notifications") || clipboardText.Equals("toon notificaties") || clipboardText.Equals("toon") || clipboardText.Equals("show"))
                {
                    notifyPaused = false;
                }
                return;
            }
            if (clipboardText.Equals("hide notifications") || clipboardText.Equals("verberg notificaties") || clipboardText.Equals("verberg") || clipboardText.Equals("hide"))
            {
                notifyPaused = true;
            }
            try
            {
                IAction service = GetService(clipboardText);
                if (service is object)
                {
                    clipboardMonitor.ClipboardChanged -= ClipboardMonitor_ClipboardChanged;
                    ActionResult actionResult = service.TryExecute(clipboardText);
                    clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;
                    // re attach the event
                    if (!string.IsNullOrWhiteSpace(actionResult.Title) || !string.IsNullOrWhiteSpace(actionResult.Description))
                    {
                        ProcessResult(actionResult, clipboardText);
                    }
                    return;

                }
                if (clipboardText.Length > 2)
                {
                    {
                        for (int i = 0; i < questionList.Count; i++)
                        {
                            Question question = questionList[i];
                            if (question.Text.Contains(clipboardText))
                            {
                                ProcessResult(new ActionResult(question.Text, question.Answer), clipboardText);
                                return;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.ToString());
            }
        }

        private IAction GetService(string clipboardText)
        {
            return serviceProvider.GetServices<IAction>().FirstOrDefault(s => s.Matches(GetClipboardText(clipboardText)));
        }

        private static string GetClipboardText(string clipboardText)
        {
            return clipboardText;
        }

        private void ProcessResult(ActionResult actionResult, string clipboardText)
        {
            notifyIcon.Icon = SystemIcons.Exclamation;
            notifyIcon.BalloonTipTitle = actionResult.Title;
            notifyIcon.BalloonTipText = actionResult.Description;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
            if (!notifyPaused)
            {
                notifyIcon.ShowBalloonTip(1000);
            }
        }


        internal static void EnsureWindowStartup(bool isStartingWithWindows)
        {
            const string keyName = "Clipboard Assistant";
            using (
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
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
            }
        }
    }
}
