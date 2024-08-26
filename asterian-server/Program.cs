using asterian_server;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = ServerSettings.Load("appsettings.json");

        using var dbContext = new AppDBContext(configuration);

        if (await dbContext.Database.CanConnectAsync())
        {
            var dataCache = new DataCache();
            Logger.Log(Logger.Type.Normal, $"Database connected successfully.");
            await dataCache.UpdateCacheAsync();

        }
        else
        {
            Logger.Log(Logger.Type.Error, $"Failed to connect to the database.");
        }

        WebsocketServer server = new WebsocketServer();
        await server.StartAsync();
    }
}