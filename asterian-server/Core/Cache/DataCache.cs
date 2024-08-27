using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using asterian_server.Core.Entities;

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
                IList<Machine> machines = await new MachineService().GetAllAsync();
                _Machines = machines;
                Logger.Log(Logger.Type.Normal, $"{machines.Count} Machine(s) loaded.");
                return (int)DateTime.Now.Subtract(now).TotalMilliseconds;
            }
            catch (Exception ex)
            {
                Logger.Log(Logger.Type.Error, $"Error updating machines: {ex.Message}");
                return 0;
            }
        }
    }
}
