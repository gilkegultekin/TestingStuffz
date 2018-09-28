using Microsoft.AspNetCore.Http;
using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketTestServer.Middleware
{
    public class CustomWebsocketMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomWebsocketMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == "/websockettest")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await Echo(webSocket);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("You must make a websocket request to this endpoint!");
                }
            }
            else
            {
                await _next(context);
            }
        }

        private async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var wsResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!wsResult.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, wsResult.Count), wsResult.MessageType,
                        wsResult.EndOfMessage, CancellationToken.None);
                wsResult = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(wsResult.CloseStatus.Value, wsResult.CloseStatusDescription, CancellationToken.None);
        }
    }
}
