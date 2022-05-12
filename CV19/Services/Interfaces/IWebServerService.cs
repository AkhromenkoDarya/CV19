namespace CV19.Services.Interfaces
{
    internal interface IWebServerService
    {
        bool IsEnabled { get; set; }

        void Start();

        void Stop();
    }
}
