using System;
using System.Timers;

namespace C64.FrontEnd.Helpers
{
    public class NotifierService : IDisposable
    {
        private Timer timer;
        private Random random = new Random();

        public event EventHandler<string> NewMessage;

        public event EventHandler NewDownload;

        public NotifierService()
        {
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            timer.Interval = random.Next(500, 2000);
            var message = $"{DateTime.Now} new message blabla";

            if (NewMessage != null)
            {
                var subscribers = NewMessage.GetInvocationList().Length;
                NewMessage?.Invoke(this, message + "(Suscr: " + subscribers + ")");
            }
        }

        public void AddMessage(string message)
        {
            NewMessage?.Invoke(this, message);
        }

        public void OnDownload()
        {
            NewDownload?.Invoke(this, null);
        }

        public void Dispose()
        {
            timer.Dispose();
        }
    }
}