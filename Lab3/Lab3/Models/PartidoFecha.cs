using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class PartidoFecha : IComparable<PartidoFecha>
    {
        public int noPartido { get; set; }

        public DateTime fechaPartido { get; set; }

        public string grupo { get; set; }

        public string pais1 { get; set; }

        public string pais2 { get; set; }

        public string estadio { get; set; }

        public int CompareTo(PartidoFecha obj)
        {
            if (obj == null)
                return 1;

            PartidoFecha otro = obj as PartidoFecha;
            if (otro != null)
                return this.fechaPartido.CompareTo(otro.fechaPartido);
            else
                throw new ArgumentException("Object is not a País");
        }
    }
}