using Demo.Component;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Tool.Log;
using Tool.Log.Enum;
using Tool.NetDirectory;

namespace Demo
{
    public partial class Form1 : Form
    {
        private Log Log = new Log();
        private NetDirectory net;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Initial_Click(object sender, EventArgs e)
        {
            string dirPath = txb_DestDirPath.Text;
            string access = txb_Access.Text;
            string password = txb_Password.Text;

            try
            {
                if (string.IsNullOrEmpty(access) && string.IsNullOrEmpty(password))
                {
                    net = new NetDirectory(dirPath);
                }
                else
                {
                    net = new NetDirectory(dirPath, access, password);
                }

                net.MsgEvent += Net_MsgEvent;
                net.ConnectChanged += Net_ConnectChanged;
            }
            catch (Exception ex)
            {
                WriteLog(new MsgInfo(MsgType.Error, $"ErrorMsg:{ex.Message}"), true);
                WriteLog(new MsgInfo(MsgType.Debug, $"StackTrace:{ex.StackTrace}"), false);
            }
        }

        private void btn_ClearMsg_Click(object sender, EventArgs e)
        {
            rtb_MsgInfo.Clear();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            net.Connect();
        }

        private void btn_DisConnect_Click(object sender, EventArgs e)
        {
            net.DisConnect();
        }

        private void Net_ConnectChanged(ConnectArgs args)
        {
            WriteLog(new MsgInfo(args.MsgType, args.Msg), true);
        }

        private void Net_MsgEvent(ConnectArgs args)
        {
            WriteLog(new MsgInfo(args.MsgType, args.Msg), true);
        }

        private void WriteLog(string msg, bool display)
        {
            Log.Write(msg);

            if (display)
            {
                this.Invoke(new Action(() =>
                {
                    rtb_MsgInfo.SelectionStart = rtb_MsgInfo.TextLength;
                    msg = Regex.Replace(msg, @"^[0-9]+\/[0-9]+\/+[0-9]+\s", ""); // 移除 年月日 顯示
                    rtb_MsgInfo.SelectionColor = ConsoleColor.DarkGray.GetColor();
                    rtb_MsgInfo.AppendText(msg + Environment.NewLine);
                    rtb_MsgInfo.ScrollToCaret();
                }));
            }
        }

        public void WriteLog(MsgInfo msgInfo, bool display)
        {
            Log.Write(msgInfo);

            if (display)
            {
                this.Invoke(new Action(() =>
                {
                    rtb_MsgInfo.SelectionStart = rtb_MsgInfo.TextLength;
                    msgInfo.Message = Regex.Replace(msgInfo.Message, @"^[0-9]+\/[0-9]+\/+[0-9]+\s", ""); // 移除 年月日 顯示                    
                    rtb_MsgInfo.SelectionColor = msgInfo.ConsoleColor.GetColor();
                    rtb_MsgInfo.AppendText(msgInfo.Message + Environment.NewLine);
                    rtb_MsgInfo.ScrollToCaret();

                }));
            }
        }

    }
}
