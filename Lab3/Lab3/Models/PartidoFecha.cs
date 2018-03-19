using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class PartidoFecha : IComparable<PartidoFecha>
    {
        public int noPartido { get; set; }

        public DateTime FechaPartido { get; set; }

        public string Grupo { get; set; }

        public string Pais1 { get; set; }

        public string Pais2 { get; set; }

        public string Estadio { get; set; }

        public int CompareTo(PartidoFecha obj)
        {
            if (obj == null)
                return 1;

            PartidoFecha otro = obj as PartidoFecha;
            if (otro != null)
                return this.FechaPartido.CompareTo(otro.FechaPartido);
            else
                throw new ArgumentException("Object is not a País");
        }
    }
}