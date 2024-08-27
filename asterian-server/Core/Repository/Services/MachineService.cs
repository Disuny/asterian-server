using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asterian_server
{
    public class MachineService
    {
        private readonly AppDBContext _context;

        public MachineService()
        {
            _context = Repository.Instance;
        }

        public async Task<List<Machine>> GetAllAsync()
        {
            return await _context.Machine.AsNoTracking().ToListAsync();
        }

        public async Task<Machine> GetByIdAsync(string id)
        {
            return await _context.Machine.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Machine machine)
        {
            _context.Machine.Add(machine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Machine machine)
        {
            _context.Machine.Update(machine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var machine = await _context.Machine.FindAsync(id);
            if (machine != null)
            {
                _context.Machine.Remove(machine);
                await _context.SaveChangesAsync();
            }
        }
    }
}
