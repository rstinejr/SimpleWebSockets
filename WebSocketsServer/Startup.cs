using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System;
using System.Threading;

namespace waltonstine.websockets {

    public class Startup {

        private ILogger log;

        public void ConfigureServices(IServiceCollection services) {
            services
                .AddLogging()
                .AddRouting();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory) {

            loggerFactory.AddConsole();

            log = loggerFactory.CreateLogger("UploadService");

            app
                .UseDeveloperExceptionPage()
                .UseRouter(buildRoutes(app))
                .UseWebSockets();

            app.Use(async (context, next) => {
                if (context.WebSockets.IsWebSocketRequest) {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await Upload(context, webSocket, loggerFactory.CreateLogger("Upload"));
                } else {
                    await next();
                }
            });

        }

        private async Task Upload(HttpContext httpCtx, WebSocket sock, ILogger logger) {

            logger.LogInformation($"Upload method called: WebSocket request, request path is {httpCtx.Request.Path}");

            int totalBytes = 0;
            for (;;) {
                byte[] buffer = new byte[10240];
                WebSocketReceiveResult result = await sock.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                logger.LogInformation($"Received {result.Count} bytes from client.");
                totalBytes += result.Count;
                if (result.EndOfMessage || result.CloseStatus != null) {
                    break;
                }
            }

            Console.WriteLine($"Total bytes uploaded: {totalBytes}");

            await sock.CloseAsync(WebSocketCloseStatus.NormalClosure, "Done reading", CancellationToken.None);

            return;
        }

        private IRouter buildRoutes(IApplicationBuilder app) {

            RouteBuilder routeBuilder = new RouteBuilder(app, new RouteHandler(null));

            return routeBuilder.Build();
        }
    }
}
