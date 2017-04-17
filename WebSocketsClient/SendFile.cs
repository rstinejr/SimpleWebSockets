using System;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace waltonstine.websockets.demo
{
    public class SendFile
    {
        private async static Task UploadFile(string fname) {

            HttpResponseMessage response = null;

            ClientWebSocket   sock = new ClientWebSocket();
            CancellationToken tok  = new CancellationToken();
            Uri serverUri          = new Uri("ws://localhost:5000/upload");
            Task sockTask          = sock.ConnectAsync(serverUri, tok);
            if ( ! sockTask.Wait(5000) ) {
                Console.WriteLine("Timeout attempting WebSocket connection");
                return;
            }
            Console.WriteLine("CONNECTED!!! <<<<<<<");

            await sock.CloseAsync(WebSocketCloseStatus.NormalClosure, "all she wrote", tok);

            Console.WriteLine("CLOSED!!! <<<<<<<");

            return;
        } 

        public static void Main(string[] args)
        {
            if (args.Length != 1) {
                Console.Error.WriteLine("Usage: SendFile <file>");
                Environment.Exit(-1);
                return;
            }

            Task uploadTask = UploadFile(args[0]);

            uploadTask.Wait();

            Console.WriteLine("Done!");
        }
    }
}
