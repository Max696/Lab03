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
            private int contadorE = 0;
        public int Eliminar(T value)
            {
                DeleteWithNodea(value, _raiz);
                contadorE++;
                if (!checkIfBalance(_raiz))
                {
                    balance(_raiz);
                    contadorE++;
                }
                return contadorE;
            }
            public int Insertar(Nodo<T> _nuevo)
            {
                if (_raiz == null)
                {
                    _raiz = _nuevo;
                    _raiz.parent = null;
                    contador++;
                    return contador;
                }
                else
                {
                    InsercionInterna(_raiz, _nuevo);
                    if (!checkIfBalance(_raiz))
                    {
                        balance(_raiz);
                        contador++;
                    }
                    return contador;
                }
            }
            private void InsercionInterna(Nodo<T> _actual, Nodo<T> _nuevo)
            {
                if (_actual.CompareTo(_nuevo.valor) < 0)
                {
                    if (_actual.derecho == null)
                    {
                        _actual.derecho = _nuevo;
                        _nuevo.parent = _actual;

                    }
                    else
                    {
                        InsercionInterna(_actual.derecho, _nuevo);
                    }
                }
                else if (_actual.CompareTo(_nuevo.valor) > 0)
                {
                    if (_actual.izquierdo == null)
                    {
                        _actual.izquierdo = _nuevo;
                        _nuevo.parent = _actual;
                    }
                    else
                    {
                        InsercionInterna(_actual.izquierdo, _nuevo);
                    }
                }
            }
      
        

            public ArbolAVL() 
            {
                _raiz = null;
            }
            public int maxComp(int a, int b)
            {
                return (a >= b) ? a : b;
            }
            public int heigh(Nodo<T> _root)
            {
                if (_root == null)
                {
                    return 0;
                }
                return 1 + maxComp(heigh(_root.izquierdo), heigh(_root.derecho));
            }
            public bool checkIfBalance(Nodo<T> _root)
            {
                int leftheigh;
                int rightheigh;
                
                if (_root == null)
                {
                    return true;

                }
                leftheigh = heigh(_root.izquierdo);
                rightheigh = heigh(_root.derecho);
                if (Math.Abs(rightheigh - leftheigh) <= 1 && checkIfBalance(_root.izquierdo) && checkIfBalance(_root.derecho))
                {
                    return true;
                }
                return false;
            }
            
            public int BF( Nodo<T> pivot)
            {
                int leftheigh;
                int rightheigh;
               
                leftheigh = heigh(pivot.izquierdo);
                rightheigh = heigh(pivot.derecho);
                return (rightheigh - leftheigh);

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
            private int   contador   = 0;
            private void balanceV2(Nodo<T> pivot)
            {
                int FB = BF(pivot);
                 if ( Math.Abs(FB)>1)
                 {
                     if(FB==2)
                     {

                     } 
                     else if ( FB == -2)
                     {

                     }

                 }
            }
            private void rightRotation (Nodo<T> pivot)

            {
                Nodo<T> aux = pivot.izquierdo;
                pivot.izquierdo = aux.derecho;
                pivot.izquierdo.parent = pivot;
                aux.derecho = pivot;
                aux.parent = pivot.parent;
                pivot.parent = aux;
                if (pivot.Equals(_raiz))
                {
                    _raiz = pivot;
                    pivot = aux;
                }


            }
            private void leftRotation(Nodo<T> pivot)
           {
               Nodo<T> aux = pivot.derecho;
               pivot.derecho = aux.izquierdo;
               pivot.derecho.parent = pivot;
               aux.izquierdo = pivot;
               aux.parent = pivot.parent;
               pivot.parent = aux;
               if (pivot.Equals(_raiz))
               {
                   _raiz = pivot;
                   pivot = aux;
               }
           }
            public void balance (Nodo<T> aux)
            {
                int fbParent = BF(aux.parent);
                int fbSon = BF(aux);
                if ( Math.Sign(fbParent)== Math.Sign(fbSon))
                {
                    // rotacion simple

                    
                }
                else
                {
                     //rotacion doble 
                }

            }
            public void balance2(Nodo<T> aux)
            {
                int balance = BF(aux);
                if ( Math.Abs(balance)>1)
                {
                    if ( balance ==2)
                    {
                        if(BF(aux.izquierdo)==-1)
                        {
                            leftRotation(aux.izquierdo);
                        }
                        else
                        {
                            rightRotation(aux);
                        }
                        
                    }
                    else if ( balance==-2)
                    {
                        if ( BF(aux.derecho) ==1)
                        {
                            rightRotation(aux.derecho);
                        }
                        else
                        {
                            leftRotation(aux); 
                        }
                    }
                }

            }
            public void busqueda(Nodo< T>  pivot)
            {
                int s = BF(pivot);
                if ( Math.Abs(s) != 2 )
                {
                    balance(pivot);
                }
                else
                {
                    if (s == -1)
                        busqueda(pivot.izquierdo);
                    else
                        busqueda(pivot.derecho);
                }

              }
            public Nodo<T> ObtenerRaiz()
            {
                return _raiz;
            }
          
    }
}   
            