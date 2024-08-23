using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace asterian_server
{
    public class Socket
    {
        public string Id;
        public WebSocket Conn;
        public bool IsConnected = false;

        public Socket(string uuid, WebSocket socket) {
            Id = uuid;
            Conn = socket;
            IsConnected = true;
        }

        public async Task Disconnect()
        {
            if (Conn != null && Conn.State == WebSocketState.Open)
            {
                await Conn.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnected by server", CancellationToken.None);
                IsConnected = false;
                Conn.Dispose();
            }
        }

        public void Close()
        {
            if (Conn != null)
            {
                if (Conn.State == WebSocketState.Open)
                {
                    Conn.Abort();
                }

                Conn.Dispose();
                IsConnected = false;
            }
        }
    }
}
