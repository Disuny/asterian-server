using asterian_server;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = ServerSettings.Load("appsettings.json");
        var port = configuration["Settings:Port"];

        Repository.Initialize(configuration);
        using var dbContext = Repository.Instance;

        if (await dbContext.Database.CanConnectAsync())
        {
            var dataCache = new DataCache();
            Logger.Log(Logger.Type.Normal, $"Database connected successfully.");
            await dataCache.UpdateCacheAsync();

            WebsocketServer server = new WebsocketServer(int.Parse(port));
            await server.StartAsync();
        }
        else
        {
            Logger.Log(Logger.Type.Error, $"Failed to connect to the database.");
        }
    }
}