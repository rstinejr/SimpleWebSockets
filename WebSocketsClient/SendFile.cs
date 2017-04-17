using System;
using System.IO;
using System.Net.Http;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace waltonstine.websockets.demo
{
    public class SendFile
    {
        private async static Task UploadFile(Stream strm) {

            byte[]            bytes = new byte[1048576];
            BinaryReader      br   = new BinaryReader(strm);

            int cnt = br.Read(bytes, 0, bytes.Length);

            ArraySegment<byte> buff = new ArraySegment<byte>(bytes);

            ClientWebSocket   sock = new ClientWebSocket();
            CancellationToken tok  = new CancellationToken();
            Uri serverUri          = new Uri("ws://localhost:5000/upload");

            Task sockTask          = sock.ConnectAsync(serverUri, tok);
            if ( ! sockTask.Wait(5000) ) {
                Console.WriteLine("Timeout attempting WebSocket connection");
                return;
            }
            Console.WriteLine("CONNECTED!!! <<<<<<<");

            await sock.SendAsync(buff, WebSocketMessageType.Binary, true, tok);
            Console.WriteLine("Bytes sent! <<<<<<<");

            Thread.Sleep(2000);

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

            FileStream fs = new FileStream(args[0], FileMode.Open, FileAccess.Read);

            Task uploadTask = UploadFile(fs);

            uploadTask.Wait();

            Console.WriteLine("Done!");
        }
    }
}
