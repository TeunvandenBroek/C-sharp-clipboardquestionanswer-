using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace it.Actions
{
    //TO DO
    public class TryWifiPass : IAction
    {
        public ActionResult TryExecute(string clipboardText)
        {
            var actionResult = new ActionResult();

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

        private string wifilist()
        {
            // netsh wlan show profile
            var processWifi = new Process();
            processWifi.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processWifi.StartInfo.FileName = "netsh";
            processWifi.StartInfo.Arguments = "wlan show profile";
            processWifi.StartInfo.UseShellExecute = false;
            processWifi.StartInfo.RedirectStandardError = true;
            processWifi.StartInfo.RedirectStandardInput = true;
            processWifi.StartInfo.RedirectStandardOutput = true;
            processWifi.StartInfo.CreateNoWindow = true;
            processWifi.Start();
            var output = processWifi.StandardOutput.ReadToEnd();
            var err = processWifi.StandardError.ReadToEnd();
            processWifi.WaitForExit();
            return output;
        }

        private string wifipassword(string wifiname)
        {
            var argument = "wlan show profile name=\"" + wifiname + "\" key=clear";
            var processWifi = new Process();
            processWifi.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processWifi.StartInfo.FileName = "netsh";
            processWifi.StartInfo.Arguments = argument;
            processWifi.StartInfo.UseShellExecute = false;
            processWifi.StartInfo.RedirectStandardError = true;
            processWifi.StartInfo.RedirectStandardInput = true;
            processWifi.StartInfo.RedirectStandardOutput = true;
            processWifi.StartInfo.CreateNoWindow = true;
            processWifi.Start();
            var output = processWifi.StandardOutput.ReadToEnd();
            var err = processWifi.StandardError.ReadToEnd();
            processWifi.WaitForExit();
            return output;
        }

        public string wifipassword_single(string wifiname)
        {
            var get_password = wifipassword(wifiname);
            using (var reader = new StringReader(get_password))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var regex2 = new Regex(@"Key Content * : (?<after>.*)");
                    var match2 = regex2.Match(line);

                    if (match2.Success) return match2.Groups["after"].Value;
                }
            }

            return "Open Network";
        }

        private bool get_passwords() // Main Operation occurs here in this function
        {
            var wifidata = wifilist();
            return true;
        }
    }
}