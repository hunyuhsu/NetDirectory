using System;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tool.Log.Enum;
using Tool.NetDirectory.Enum;

namespace Tool.NetDirectory
{
    public delegate void EventHandler(ConnectArgs args);

    public partial class NetDirectory
    {
        [DllImport("wininet")] public static extern bool InternetGetConnectedState(ref uint lpdwFlags, uint dwReserved);

        public event EventHandler MsgEvent;
        public event EventHandler ConnectChanged;

        public ConnStatus ConnStatus = ConnStatus.DisConnect;

        public NetDirectory(string destDirPath)
        {
            #region Check Conn Data
            if (string.IsNullOrEmpty(destDirPath))
            {
                throw new Exception("路徑不得為空值!");
            }

            Match match = CheckIPRule(destDirPath);
            #endregion

            NetMode = NetMode.PathOnly;
            DestDirPath = destDirPath;
            IP = match.Value;
            DeviceName = "";
            Account = "";
            Password = "";

        }

        public NetDirectory(string destDirPath, string account, string password)
           : this("", destDirPath, account, password) { }

        public NetDirectory(string deviceName, string destDirPath, string account, string password)
        {
            #region Check Conn Data
            if (string.IsNullOrEmpty(destDirPath))
            {
                throw new Exception("路徑不得為空值!");
            }

            Match match = CheckIPRule(destDirPath);

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                throw new Exception("帳號或密碼不可為空");
            }
            #endregion

            NetMode = string.IsNullOrEmpty(deviceName)
                            ? NetMode.PathWithCredentials : NetMode.DriveLetterWithPathAndCredentials;
            DestDirPath = destDirPath;
            DeviceName = deviceName.Trim();
            IP = match.Value;
            Account = account;
            Password = password;
        }

        public bool Connect()
        {
            bool result = false;

            try
            {
                if (!CheckNetWork())
                {
                    return result;
                }

                if (!CreateNetUse())
                {
                    return result;
                }

                if (!IsExistsDir(DestDirPath))
                {
                    return result;
                }

                result = true;
            }
            catch (Exception ex)
            {
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Error, $"ErrorMsg: {ex.Message}");
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Debug, $"Trace: {ex.StackTrace}");
                result = false;
            }

            return result;
        }

        public bool DisConnect()
        {
            bool result = false;

            try
            {

                result = DeleteNetUse();
            }
            catch (Exception ex)
            {
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Error, $"ErrorMsg: {ex.Message}");
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Debug, $"Trace: {ex.StackTrace}");

                result = false;
            }

            return result;
        }

        // net use X: "\\server\share"  /user:username "password"
        // net use "\\server\share"  /user:username "password"
        private bool CreateNetUse()
        {
            string createNetUseCmd = string.Empty;
            try
            {
                switch (NetMode)
                {
                    default:
                    case NetMode.PathOnly:
                        {
                            createNetUseCmd = $" net use \"{DestDirPath}\" ";
                            break;
                        }
                    case NetMode.PathWithCredentials:
                        {
                            createNetUseCmd = $" net use \"{DestDirPath}\" /user:{Account} \"{Password}\" ";

                            break;
                        }
                    case NetMode.DriveLetterWithPathAndCredentials:
                        {
                            createNetUseCmd = $" net use {DeviceName}: \"{DestDirPath}\" /user:{Account} \"{Password}\" ";
                            break;
                        }
                }

                string processErrorMsg = Process(createNetUseCmd);

                if (string.IsNullOrEmpty(processErrorMsg))
                {
                    ConnStatus_MsgProcess(ConnStatus.Connect, MsgType.Info, $"網路磁碟機建立成功。 cmd: {createNetUseCmd}");
                }
                else
                {
                    ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Error, $"網路磁碟機建立失敗。 cmd: {createNetUseCmd}" + $"ErrorMsg: {processErrorMsg}");
                }

                return true;
            }
            catch (Exception ex)
            {
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Error, "該電腦無法正常使用 net use，請至 cmd.exe 確認\r\n" + $"ErrorMsg: {ex.Message}");
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Debug, $"Trace: {ex.StackTrace} ");
                return false;
            }
        }

        private bool DeleteNetUse()
        {
            string createNetUseCmd = string.Empty;
            try
            {
                switch (NetMode)
                {
                    default:
                    case NetMode.PathOnly:
                    case NetMode.PathWithCredentials:
                        {
                            createNetUseCmd = $" net use \"{DestDirPath}\" /delete ";
                            break;
                        }
                    case NetMode.DriveLetterWithPathAndCredentials:
                        {
                            createNetUseCmd = $" net use {DeviceName}: /delete";
                            break;
                        }
                }

                string processErrorMsg = Process(createNetUseCmd);

                if (string.IsNullOrEmpty(processErrorMsg))
                {
                    ConnStatus_MsgProcess(ConnStatus.Connect, MsgType.Info, $"網路磁碟機刪除成功。 cmd: {createNetUseCmd}");
                }
                else
                {
                    ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Error, $"網路磁碟機刪除失敗。 cmd: {createNetUseCmd}\r\n" + $"ErrorMsg: {processErrorMsg}");
                }

                return true;
            }
            catch (Exception ex)
            {
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Error, "該電腦無法正常使用 net use，請至 cmd.exe 確認。\r\n" + $"ErrorMsg: {ex.Message}");
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Debug, $"Trace: {ex.StackTrace} ");

                return false;
            }
        }

        private string Process(string cmd)
        {
            try
            {
                using (Process proc = new Process())
                {
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.WorkingDirectory = @"C:\"; // 避免unc問題
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardInput = true;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.Start();

                    proc.StandardInput.WriteLine(cmd);
                    proc.StandardInput.WriteLine("exit");

                    while (!proc.HasExited)
                    {
                        proc.WaitForExit(1000);
                    }

                    // string msg = proc.StandardOutput.ReadToEnd().Trim();
                    string msg = proc.StandardError.ReadToEnd().Trim();

                    return msg;
                }
            }
            catch (Exception ex)
            {
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Error, "該電腦無法正常使用Process \r\n" + $"ErrorMsg: {ex.Message}");
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Debug, $"Trace: {ex.StackTrace} ");
                throw ex;
            }
        }

        // To Do 訊息處理
        private void ConnStatus_MsgProcess(ConnStatus connStatus, MsgType msgType, string msg)
        {
            MsgEvent?.Invoke(new ConnectArgs(connStatus, msgType, msg));

            if (ConnStatus != connStatus)
            {
                ConnStatus = connStatus;
                ConnectChanged?.Invoke(new ConnectArgs(connStatus, msgType, msg));
            }
        }

        private bool CheckNetWork()
        {
            if (CheckLocalNetWork() == false)
            {
                ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Error, "本機連線異常，請設備檢查電腦網路連線能力");
                return false;
            }

            using (Ping ping = new Ping())
            {
                try
                {
                    PingReply reply = ping.Send(IP, 500);
                    bool result = reply.Status == IPStatus.Success;

                    if (result == false)
                    {
                        ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Error, $"Ping IP ({IP}) 發生錯誤。Msg: 連線失敗，請確認該IP裝置是否還存活。");
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Error, $"Ping IP ({IP}) 發生錯誤。Msg: {ex.Message}");
                    ConnStatus_MsgProcess(ConnStatus.DisConnect, MsgType.Debug, $"Trace: {ex.StackTrace}");
                    return false;
                }
            }
        }

        private Match CheckIPRule(string destDirPath)
        {
            string pattern = @"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
            Match match = Regex.Match(destDirPath, pattern);
            if (match.Success)
            {
                Console.WriteLine($"找到的 IP 地址是: {match.Value}");
            }
            else
            {
                throw new Exception("未找到有效的 IP 地址!");
            }

            return match;
        }

        public bool CheckLocalNetWork()  // To Do 確認 本機連線能力
        {
            uint flags = 0x0;
            return InternetGetConnectedState(ref flags, 0);
        }

        public bool IsExistsDir(string Path)   // To Do 確認能不能正常連到遠端的資料夾
        {
            // 若路徑 \\*.*.*.*\  判斷為true
            string path = Path.Replace($@"\\{IP}", "").Replace(@"\", "");
            if (string.IsNullOrEmpty(path))
            {
                return true;
            }

            var task = new Task<bool>(() =>
            {
                var monitorDir = new DirectoryInfo(Path);
                try
                {
                    if (!monitorDir.Exists) monitorDir.Create();
                    return monitorDir.Exists;
                }
                catch { return false; }
            });
            task.Start();

            return task.Wait(1000) && task.Result;
        }
    }


    public class ConnectArgs : EventArgs
    {
        public ConnStatus ConnStatus { get; set; }
        public MsgType MsgType { get; set; }
        public string Msg { get; set; }

        public ConnectArgs(ConnStatus connStatus, MsgType msgType, string msg)
        {
            ConnStatus = connStatus;
            MsgType = msgType;
            Msg = msg;
        }
    }

}
