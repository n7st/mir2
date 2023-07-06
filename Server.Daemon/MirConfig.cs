namespace Server.Daemon
{
    public class MirConfig
    {
        public bool AllowStartGame { get; set; }

        public bool CheckVersion { get; set; } = true;

        public bool Multithreaded { get; set; } = true;

        public ushort Port { get; set; } = 7000;
    }
}
