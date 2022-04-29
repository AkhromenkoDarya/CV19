namespace CV19.Services.Interfaces
{
    internal interface IWebServerService
    {
        public bool Enabled { get; set; }

        public void Start();

        public void Stop();
    }
}
