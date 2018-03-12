using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVL
{
    public class ArbolAVL<T> : IArbolBinario<T> where T: IComparable<T>
        {
            public Nodo<T> _raiz { get; set; }
            int right = 0;
            int left = 0;

            public ArbolAVL() 
            {
                _raiz = null;
            }
        
            public Nodo<T> Min( Nodo<T> actual)
            {
                if (actual==null)
                {
                    return null;

                }
                if (actual.izquierdo == null)
                {
                    return actual;
                }
                return Min(actual.izquierdo);
            }

            public  Nodo<T> RoveToFindMin()
            {
                return Min(_raiz);
            }

            public void Eliminar(T _key)
            {
                DeleteWithNodea(_key, _raiz);
            }

            public Nodo<T> Max(Nodo<T> n)
            {
                if (n == null)
                {
                    return null;
                }
                else
                {
                    while (n.derecho != null)
                    {
                        n = n.derecho;
                    }
                    return n;
                }
            }
            public Nodo<T> DeleteWithNodea(T _key, Nodo<T> pivot) //Eliminar
            {
                if (pivot==null)
                {
                    return null;
                }
                if (_key.CompareTo(pivot.valor)<0)
                {
                   pivot.izquierdo= DeleteWithNodea(_key, pivot.izquierdo);
                }
                else if (_key.CompareTo(pivot.valor)>0)
                {
                    pivot.derecho=DeleteWithNodea(_key, pivot.derecho);
                }
                else if (pivot == _raiz)
                {
                    rootDelete(_key, pivot);
                }

                else
                {
                    if( pivot.derecho==null && pivot.izquierdo==null)
                    {
                        pivot = null;
                        return pivot;
                    }
                    else if (pivot.derecho == null)
                    {
                        Nodo<T> aux = pivot;
                        pivot = pivot.izquierdo;
                        aux = null;
                    }
                    else if ( pivot.derecho==null)
                    {
                        Nodo<T> aux = pivot;
                        pivot = pivot.derecho;
                        aux = null;
                    }
                    else
                    {
                        Nodo<T> aux = Max(pivot.izquierdo);
                        pivot.valor = aux.valor;
                        pivot.derecho = DeleteWithNodea(_key, pivot.izquierdo);
                    }
                }
                return pivot;
            }

            private void rootDelete(T _key, Nodo<T> root)
            {
                if (root != null)
                {
                    if (root.CompareTo(_key) < 0)
                    {
                        rootDelete(_key, root.izquierdo);
                    }
                    else
                    {
                        if (root.CompareTo(_key) > 0)
                        {
                            rootDelete(_key, root.derecho);
                        }
                        else
                        {
                            Nodo<T> toDeleteNode = root;
                            if (toDeleteNode.derecho == null)
                            {
                                root = toDeleteNode.izquierdo;
                            }
                            else
                            {
                                if (toDeleteNode.izquierdo == null)
                                {
                                    root = toDeleteNode.derecho;
                                }
                                else
                                {
                                    Nodo<T> pivot = null;
                                    Nodo<T> aux = root.izquierdo;
                                    bool mark = false;
                                    while (aux.derecho != null)
                                    {
                                        pivot = aux;
                                        aux = aux.derecho;
                                        mark = true;
                                    }
                                    root.valor = aux.valor;
                                    toDeleteNode = aux;
                                    if (mark == true)
                                    {
                                        pivot.derecho = aux.izquierdo;
                                    }
                                    else
                                    {
                                        root.izquierdo = aux.izquierdo;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public void Insertar(Nodo<T> _nuevo)
            {
                if (_raiz == null)
                {
                    _raiz = _nuevo;
                }
                else
                {
                    InsercionInterna(_raiz, _nuevo);
                }
            }

            public Nodo<T> ObtenerRaiz()
            {
                return _raiz;
            }

            private void InsercionInterna(Nodo<T> _actual, Nodo<T> _nuevo) {
                if (_actual.CompareTo(_nuevo.valor) < 0)
                {
                    if (_actual.derecho == null)
                    {
                        _actual.derecho = _nuevo;
                        right++;
                    }
                    else
                    {
                        InsercionInterna(_actual.derecho, _nuevo);
                    }
                }
                else if (_actual.CompareTo(_nuevo.valor) > 0) {
                    if (_actual.izquierdo == null)
                    {
                        _actual.izquierdo = _nuevo;
                        left++;
                    }
                    else 
                    {
                        InsercionInterna(_actual.izquierdo, _nuevo);
                    }
                }
            }
    }
}
