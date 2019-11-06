namespace it
{
    internal class AccessPoint
    {
        public object SSID { get; set; }
        public string BSSID { get; set; }
        public byte Signal { get; set; }
        public object Name { get; internal set; }
    }
}