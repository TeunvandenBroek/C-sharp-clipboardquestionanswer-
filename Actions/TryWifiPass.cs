using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace it.Actions
{
    //TO DO
    public class TryWifiPass : IAction
    {
        private string wifilist()
        {
            // netsh wlan show profile
            Process processWifi = new Process();
            processWifi.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processWifi.StartInfo.FileName = "netsh";
            processWifi.StartInfo.Arguments = "wlan show profile";
            processWifi.StartInfo.UseShellExecute = false;
            processWifi.StartInfo.RedirectStandardError = true;
            processWifi.StartInfo.RedirectStandardInput = true;
            processWifi.StartInfo.RedirectStandardOutput = true;
            processWifi.StartInfo.CreateNoWindow = true;
            processWifi.Start();
            string output = processWifi.StandardOutput.ReadToEnd();
            string err = processWifi.StandardError.ReadToEnd();
            processWifi.WaitForExit();
            return output;
        }

        private string wifipassword(string wifiname)
        {
            string argument = "wlan show profile name=\"" + wifiname + "\" key=clear";
            Process processWifi = new Process();
            processWifi.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processWifi.StartInfo.FileName = "netsh";
            processWifi.StartInfo.Arguments = argument;
            processWifi.StartInfo.UseShellExecute = false;
            processWifi.StartInfo.RedirectStandardError = true;
            processWifi.StartInfo.RedirectStandardInput = true;
            processWifi.StartInfo.RedirectStandardOutput = true;
            processWifi.StartInfo.CreateNoWindow = true;
            processWifi.Start();
            string output = processWifi.StandardOutput.ReadToEnd();
            string err = processWifi.StandardError.ReadToEnd();
            processWifi.WaitForExit();
            return output;
        }
        public string wifipassword_single(string wifiname)
        {
            string get_password = wifipassword(wifiname);
            using (StringReader reader = new StringReader(get_password.ToString()))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Regex regex2 = new Regex(@"Key Content * : (?<after>.*)");
                    Match match2 = regex2.Match(line);

                    if (match2.Success)
                    {
                        return match2.Groups["after"].Value;
                    }
                }
            }
            return "Open Network";
        }

        private bool get_passwords() // Main Operation occurs here in this function
        {
            string wifidata = wifilist();
            return true;
        }

        public bool Matches(string clipboardText)
        {
            return clipboardText.ToLower().StartsWith("wifi");
        }

        public ActionResult TryExecute(string clipboardText)
        {
            ActionResult actionResult = new ActionResult();

            switch (clipboardText)
            {
                case "wifi password":
                    {
                        actionResult.Title = "Your wifi password is";
                        actionResult.Description = get_passwords().ToString();
                        break;
                    }
                default:
                    actionResult.IsProcessed = false;
                    break;
            }

            return actionResult;
        }
    }
}
