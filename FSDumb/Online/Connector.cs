using nanoFramework.Networking;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading;
using Vroumed.FSDumb.Dependencies;
using Vroumed.FSDumb.Hardware.Representations;
using Vroumed.FSDumb.Managers;
using Vroumed.FSDumb.Online.HTTP;

namespace Vroumed.FSDumb.Online
{
    public class Connector : IDependencyCandidate
    {
        public bool BackendOnline { get; set; } = false;
        public IPEndPoint BackendEndPoint { get; set; }

        [Resolved]
        private IHardwareAccessor HardwareAccessor { get; set; }

        [Resolved]
        private Context Context { get; set; }
        

        public bool IsOnline { get; private set; } = false;

        public string BaseAddress { get; private set; } = "http://";

        public void Run()
        {
            if (!InitializeInternet())
                return;

            HttpServer server = new HttpServer();
            server.ThreadedStart();
        }


        public bool InitializeInternet()
        {
            CancellationTokenSource cs = new(10000);
            IsOnline = WifiNetworkHelper.ScanAndConnectDhcp(InternetConfiguration.WifiSSID, InternetConfiguration.Password, token: cs.Token);
            if (!IsOnline)
            {
                Debug.WriteLine($"Can't connect to the network, error: {WifiNetworkHelper.Status}");
                if (WifiNetworkHelper.HelperException != null)
                {
                    Debug.WriteLine($"ex: {WifiNetworkHelper.HelperException}");
                }
                Context.StartSequence(ExecuteType.Threaded)
                .Schedule(() =>
                {
                    HardwareAccessor.Buzzer.Frequency = 200;
                    HardwareAccessor.Buzzer.Start();
                    HardwareAccessor.Lighting.TurnOff();
                }).Delay(100)
                .Schedule(HardwareAccessor.Buzzer.Stop)
                .Delay(100)
                .Schedule(HardwareAccessor.Buzzer.Start)
                .Delay(100)
                .Schedule(HardwareAccessor.Buzzer.Stop)
                .Execute();
            }
            else
            {
                HardwareAccessor.Buzzer.Ping();
                HardwareAccessor.Lighting.StandardLights();
            }

            return IsOnline;
        }

        public void StartBackendCommunication()
        {
            HttpClient client = new();
            client.BaseAddress = new(BaseAddress + BackendEndPoint.Address + "/status/fsdumb");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "Vroumed.FSDumb");
            client.DefaultRequestHeaders.Add("Authorization", InternetConfiguration.RoverKey);
        }
    }
}
