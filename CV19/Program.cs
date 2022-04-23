using System;

namespace CV19
{
    public static class Program
    {
        [STAThreadAttribute()]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
