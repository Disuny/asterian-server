using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asterian_server
{
    [Table("maquina")]
    public class Machine
    {
        [Column("id")]
        public string Id { get; set; }

        [Column("id_tenant")]
        public string IdTenant { get; set; }

        [Column("id_grupoevento")]
        public string? IdGrupoEvento { get; set; }

        [Column("referencia")]
        public string Referencia { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("ativo")]
        public byte Ativo { get; set; }

        [Column("chaveMonitoramento")]
        public string ChaveMonitoramento { get; set; }
    }
}
