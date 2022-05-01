namespace CV19.Services.Interfaces
{
    internal interface IWebServerService
    {
        public bool IsEnabled { get; set; }

        public void Start();

        public void Stop();
    }
}
