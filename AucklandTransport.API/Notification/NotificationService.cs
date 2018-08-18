using Microsoft.Extensions.Hosting;
using Notification.Model;
using Notification.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Notification
{
    public class NotificiationService : IHostedService, IDisposable
    {
        private Timer _timer;

        public NotificiationService()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Timed Background Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(600)); // every 10 minutes

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Console.WriteLine("Timed Background Service is working.");
            MakeRequest().GetAwaiter();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Timed Background Service is stopping.2");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async Task MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "161cc7478ff940be910a73459bc6e703");

            var uri = "/notifications/?" + queryString;

            var items = await WebApiService.Instance.GetAsync<RequestResponse>(uri);

            // Console.Write(await response.Content.ReadAsStringAsync());
        }
    }
}
