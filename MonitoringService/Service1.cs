using System;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Text.Json;
using System.Threading;
using Nest;

namespace MonitoringService
{
    public partial class Service1 : ServiceBase
    {
        private bool m_stopSignal = false;
        Thread m_search = null;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            m_stopSignal = false;
            m_search = new Thread(new ThreadStart(Monitor));
            m_search.Start();
        }

        protected override void OnStop()
        {
            m_stopSignal = true;
            m_search.Join();
            m_search = null;
        }

        public void Monitor()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
                .DefaultIndex("test.monitor");

            var client = new ElasticClient(settings);

            while (!m_stopSignal)
            {
                try
                {
                    var searchResponse = client.Search<Item>(s => s
                        .From(0)
                        .Size(1)
                        .Query(q => q
                             .Match(m => m
                                .Field(f => f.message)
                                .Query("error")
                             )
                        )
                    );

                    var items = searchResponse.Documents;

                    string res = "";

                    if (items.Count > 0)
                    {
                        var deleteId = "";
                        foreach(var hit in searchResponse.Hits)
                        {
                            deleteId = hit.Id.ToString();

                            // res = JsonSerializer.Serialize(hit.Source);

                        }
                        
                        foreach (var item in items)
                        {
                            res = JsonSerializer.Serialize(item);
                            
                            SendMessage(res);
                            
                            client.Delete<Item>(deleteId);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }

                Thread.Sleep(5000);
            }
        }

        public void SendMessage(string body)
        {
            string serverKey = "your firebase server key";
            var notificationInputDto = new
            {
                to = "target device token",
                notification = new
                {
                    body = body,
                    title = "Error notification"
                },
                data = new
                {
                    body = body,
                    title = "Error notification"
                }
            };
            try
            {
                var result = "";
                var webAddr = "https://fcm.googleapis.com/fcm/send";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + serverKey);
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    streamWriter.Write(JsonSerializer.Serialize(notificationInputDto));
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
