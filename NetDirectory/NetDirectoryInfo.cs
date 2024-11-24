using Tool.NetDirectory.Enum;

namespace Tool.NetDirectory
{
    public partial class NetDirectory
    {
        public NetMode NetMode { get; private set; }
        public string DestDirPath { get; private set; }
        public string DeviceName { get; private set; }
        public string Account { get; private set; }
        public string Password { get; private set; }
        public string IP { get; private set; }
    }
}