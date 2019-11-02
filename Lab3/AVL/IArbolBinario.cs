using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    interface IArbolBinario<T>
    {
        int Insertar(Nodo<T> _nuevo);
        

        int Eliminar(T _key);

        Nodo<T> ObtenerRaiz();
    }
}
