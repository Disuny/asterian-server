using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asterian_server
{
    public class Repository
    {
        private static AppDBContext? _instance;
        private static IConfigurationRoot? _configuration;
        private static readonly object _lock = new object();

        private Repository() { }

        public static void Initialize(IConfigurationRoot configuration)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _configuration = configuration;
                        _instance = new AppDBContext(_configuration);
                    }
                }
            }
        }

        public static AppDBContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException("AppDBContext has not been initialized. Call Initialize() first.");
                }
                return _instance;
            }
        }
    }
}
