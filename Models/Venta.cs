using System;
using System.Collections.Generic;
using System.Text;

namespace Tiendita.Models
{
    class Venta
    {
        public uint Id { get; set; }
        public decimal Total { get; set; }
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; }
        public virtual ICollection<Detalle> Detalles { get; set; }
    }
}
