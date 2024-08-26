using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asterian_server
{
    public class DatabaseService
    {
        private readonly AppDBContext _context;

        public DatabaseService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Machine>> LoadAllMachinesAsync()
        {
            var machinesData = await _context.Machine.ToListAsync();
            Logger.Log(Logger.Type.Normal, $"{machinesData.Count} Machine(s) loaded.");
            return machinesData;
        }
    }
}
