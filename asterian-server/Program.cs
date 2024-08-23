using asterian_server;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebsocketServer server = new WebsocketServer();
        await server.StartAsync();
    }
}