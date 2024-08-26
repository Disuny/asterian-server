using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asterian_server
{
    public class DataCache
    {
        private static IList<Machine> _Machines;

        public static IList<Machine> Machines
        {
            get
            {
                return _Machines;
            }
        }

        public async Task UpdateCacheAsync()
        {
            try
            {
                int tmp = 0;
                tmp += await UpdateMachinesAsync();
                //tmp += await UpdateGroupEventAsync();
                //tmp += await UpdateEventAsync();
                Logger.Log(Logger.Type.Normal, $"Cache loaded in {tmp}ms");
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Type.Error, $"Error updating cache: {ex.Message}");
            }
        }

        private async Task<int> UpdateMachinesAsync()
        {
            try
            {
                DateTime now = DateTime.Now;
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
