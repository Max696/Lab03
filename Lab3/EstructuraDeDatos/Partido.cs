using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDeDatos
{
    public class Partido : IComparable<Partido>
    {
        public int noDePartido { get; set; }
        public DateTime fechaPartido { get; set; }
        public string grupo { get; set; }
        public string pais1 { get; set; }
        public string pais2 { get; set; }
        public string estadio { get; set; }

        public Partido izquierdo { get; set; }
        public Partido derecho { get; set; }
        private ComparadorNodosDelegate comparador;

        public int CompareTo(Partido _other)
        {
            return comparador(this, _other);
        }
    }
}
