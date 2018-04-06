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
                    balance2(_raiz);
                    contadorE++;
                }
                return contadorE;
            }
            public int Insertar(Nodo<T> _nuevo)
            {
                 int   contador   = 0;
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
                    contador++;
                    if (!checkIfBalance(_raiz))
                    {
                       
                        balance2(_nuevo);
                        
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
            public int heigh(Nodo<T> pivot)
            {
                if (pivot == null)
                {
                    return 0;
                }
                return 1 + maxComp(heigh(pivot.izquierdo), heigh(pivot.derecho));
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
               if (pivot == null)
               {
                   return 0;
               }
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
            private Nodo<T> balanceV2(Nodo<T> pivot)
            {
                int FB = BF(pivot);
                 if ( Math.Abs(FB)==2)
                 {
                     if(FB==-2)
                     {
                         Nodo<T> auxLeft = pivot.izquierdo;
                         int s =BF(auxLeft);
                         if(s==-1)
                         {
                             return llRotation(pivot);
                         }
                         else if ( s == 1)
                         {
                             return lrRotation(pivot);
                         }
                     } 
                     else if ( FB == 2)
                     {
                         Nodo<T> auxRigth = pivot.derecho;
                         int s = BF(auxRigth);
                         if (s==-1)
                         {
                             return rlRotation(pivot);
                         }
                         else if ( s == 1)
                         {
                             return rrRotation(pivot);
                         }
                     }

                 }
                 _raiz = pivot;
                 return pivot;
             }
            private Nodo<T>rrRotation(Nodo<T>pivot)
            {
                Nodo<T> aux1 = pivot.derecho;
                pivot.derecho = aux1.izquierdo;
                aux1.izquierdo = pivot;
                return aux1;

            }
            private Nodo<T> llRotation(Nodo<T> pivot)
            {
                Nodo<T> aux = pivot.izquierdo;
                pivot.izquierdo = aux.derecho;
                aux.derecho = pivot;
                return pivot;
            }
            private Nodo<T> lrRotation(Nodo<T> pivot)
           {
               Nodo<T> aux1 = pivot.izquierdo;
               Nodo<T> aux2 = pivot.derecho;
               pivot.izquierdo = aux2.derecho;
               aux1.derecho = aux2.izquierdo;
               aux2.izquierdo = aux1;
               aux2.derecho = pivot;

               return aux2;
           }
            private Nodo<T> rlRotation(Nodo<T> pivot)
        {
            Nodo<T> aux1 = pivot.derecho;
            Nodo<T> aux2 = pivot.izquierdo;
            pivot.derecho = aux2.izquierdo;
            aux1.izquierdo = aux2.derecho;
            aux2.derecho = aux1;
            aux2.izquierdo = pivot;
            return aux2;
        }
            private void rightRotation (Nodo<T> pivot)

            {
                Nodo<T> aux = pivot.parent.parent;
                if (aux.derecho == null)
                {
                    Nodo<T> aux2 = pivot.parent;
                    pivot.parent.parent = aux2;
                    aux.izquierdo = null;
                    pivot.parent.parent.derecho = aux;
                    if (aux.CompareTo(_raiz.valor) == 0)
                    {
                        _raiz = pivot.parent;
                    }
                    
                }
                
            


            }
            private void leftRotation(Nodo<T> pivot)
           {
               Nodo<T> aux = pivot.parent.parent;
                if ( aux.izquierdo== null)
                {
                    Nodo<T> aux2 = pivot.parent;
                    pivot.parent.parent = aux2;
                    aux.derecho = null;
                    pivot.parent.parent.izquierdo = aux;
                    if (aux.CompareTo(_raiz.valor) == 0)
                    {
                        _raiz = pivot.parent;
                    }
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
                int balance = BF(_raiz);
                if ( Math.Abs(balance)>1)
                {
                    if ( balance ==2)
                    {
                        if (BF(aux) == -1)
                        {
                            rightRotation(aux);
                        }   
                        else
                        {
                            leftRotation(aux);  
                        }
                    }
                    else if ( balance==-2)
                    {
                        
                            if (BF(aux) == 1)
                            {
                                leftRotation(aux);
                            }
                            else
                            {
                               rightRotation(aux);
                               
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
            public List<T> PreList = new List<T>();
            public void Pre(Nodo<T> pivot)
            {
                if (pivot != null)
                {
                    PreList.Add(pivot.valor);
                    Pre(pivot.izquierdo);                  
                    Pre(pivot.derecho);                    

                }
            }
          
    }
}   
            