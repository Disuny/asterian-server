using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace asterian_server
{
    public class WebsocketServer
    {
        private readonly int port;

        private static readonly ConcurrentDictionary<string, Socket> sockets = new();

        public WebsocketServer(int _port = 2070)
        {
            port = _port;
        }

        public async Task StartAsync()
        {
            HttpListener listener = new HttpListener();

            if(listener.IsListening) {
                throw new InvalidOperationException("Server is currently running");
            }

            listener.Prefixes.Clear();
            listener.Prefixes.Add($"http://localhost:{port}/");

            try
            {
                listener.Start();
                Logger.Log(Logger.Type.Normal, $"Server is running on ws://localhost:{port}");

                while (true) {
                    try {
                        var context = await listener.GetContextAsync();
                        if (context.Request.IsWebSocketRequest) {
                            _ = HandleWebSocketAsync(context);
                        }
                        else {
                            context.Response.StatusCode = 400;
                            context.Response.Close();
                        }
                    }
                    catch (Exception ex) {
                        Logger.Log(Logger.Type.Error, $"Listener error: {ex.Message}");
                    }
                }
            }
            catch (HttpListenerException ex) {
                if (ex.Message.Contains("Access is denied")) {
                    return;
                }
                throw;
            }
            
        }

        private async Task HandleWebSocketAsync(HttpListenerContext context)
        {
            WebSocket webSocket;

            try {
                var webSocketContext = await context.AcceptWebSocketAsync(null);
                webSocket = webSocketContext.WebSocket;
            }
            catch (Exception ex){
                Logger.Log(Logger.Type.Error, $"Failed to accept WebSocket: {ex.Message}");
                return;
            }

            string UUID = GUID.Generate();
            Socket socket = new Socket(UUID, webSocket);
            sockets.TryAdd(UUID, socket);

            Logger.Log(Logger.Type.Debug, $"Client connect: {socket.Id.Substring(0, 6)}");

            // Isolar o loop do ReceiveAsync thread separada
            Task.Run(async () => await ReceiveMessages(socket, webSocket));
        }

        private async Task HandleDisconnect(Socket socket)
        {
            sockets.TryRemove(socket.Id, out _);

            Logger.Log(Logger.Type.Debug, $"Client disconnected: {socket.Id.Substring(0, 6)}");
        }

        private async Task ReceiveMessages(Socket socket, WebSocket webSocket)
        {
            var buffer = new ArraySegment<byte>(new byte[1024]);

            try
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    WebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, CancellationToken.None).ConfigureAwait(false);

                    if (result.MessageType == WebSocketMessageType.Close)
                    {
                        break;
                    }

                    if (result.Count > 0)
                    {
                        string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
                        Console.WriteLine(message);
                    }
                }
            }
            catch (WebSocketException ex)
            {
                Logger.Log(Logger.Type.Error, $"WebSocket error: {ex.Message}");
            }
            finally
            {
                await HandleDisconnect(socket);

                if (webSocket.State == WebSocketState.Open || webSocket.State == WebSocketState.CloseReceived)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by server", CancellationToken.None).ConfigureAwait(false);
                }
                webSocket.Dispose();
            }
        }
    }
}
