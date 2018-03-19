using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    public class Nodo<T> : IComparable<T>
    {
        public T valor { get; set; }

        public Nodo<T> izquierdo { get; set; }

        public Nodo<T> derecho { get; set; }
        public Nodo<T> parent { get; set; }

        private ComparadorNodosDelegate<T> comparador;

        public Nodo(T _value, ComparadorNodosDelegate<T> _comparador)
        {
            this.valor = _value;
            this.izquierdo = null;
            this.derecho = null;
            comparador = _comparador;
        }

        public int CompareTo(T _other)
        {
            return comparador(this.valor, _other);
        }
    }
}

public delegate int ComparadorNodosDelegate<T>(T _actual, T _nuevo);
