using System;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace waltonstine.websockets.demo
{
    public class SendFile
    {
        private async static Task<HttpResponseMessage> UploadFile(string fname) {

            HttpResponseMessage response = null;

            ClientWebSocket sock = new ClientWebSocket();
            CancellationToken tok = new CancellationToken();
            Uri serverUri = new Uri("ws://localhost:5000/upload");
            Task sockTask = sock.ConnectAsync(serverUri, tok);
            if ( ! sockTask.Wait(5000) ) {
                Console.WriteLine("Timeout attempting WebSocket connection");
                return response;
            }

            StringContent dummy = new StringContent("wop bop a loo la wop bam boom");
            using (HttpClient client = new HttpClient()) {
                response = await client.PostAsync("http://localhost:5000/upload", dummy);
            }

            return response;
        } 

        public static void Main(string[] args)
        {
            if (args.Length != 1) {
                Console.WriteLine("Usage: SendFile <file>");
                return;
            }

            Task<HttpResponseMessage> uploadTask = UploadFile(args[0]);
            if (uploadTask.Wait(10000)) {
                HttpResponseMessage response = uploadTask.Result;
                Console.WriteLine($"HTTP status code: {response.StatusCode}");
            }

            Console.WriteLine("Done!");
        }
    }
}
