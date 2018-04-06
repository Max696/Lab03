using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Models
{
    public class PartidoNo: IComparable<PartidoNo>
    {
        public int noPartido { get; set; }
        public DateTime fechaPartido { get; set; }
        public string grupo { get; set; }
        public string pais1 { get; set; }
        public string pais2 { get; set; }
        public string estadio { get; set; }

        public int CompareTo(PartidoNo obj)
        {
            if (obj == null)
                return 1;

            PartidoNo otro = obj as PartidoNo;
            if (otro != null)
                return this.noPartido.CompareTo(otro.noPartido);
            else
                return 0; 
        }
    }
}