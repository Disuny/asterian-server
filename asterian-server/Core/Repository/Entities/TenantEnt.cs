using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asterian_server
{
    public class Tenant
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? ImageUrl { get; set; }
        public string InvateCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedUserId { get; set; }

        //public User CreatedUser { get; set; }
        //public ICollection<User> Users { get; set; }
        //public ICollection<Sensor> Sensors { get; set; }
        //public ICollection<Maquina> Maquinas { get; set; }
        //public ICollection<Formula> Formulas { get; set; }
        //public ICollection<Desktop> Desktops { get; set; }
        //public ICollection<Evento> Eventos { get; set; }
        //public ICollection<GrupoEvento> GrupoEventos { get; set; }
    }
}
