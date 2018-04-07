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
                    balance2(_raiz, 0, 0);
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
                    if (!checkIfBalance(_raiz) || !checkIfBalance(_raiz.izquierdo) || !checkIfBalance(_raiz.derecho))
                    {
                        //Crear metodo para encontrar valor diferente
                        int p = BF(_raiz);
                        int k = 0;
                        if (p >= 1)
                        {
                            k = valorDiferenteDer(p);
                        }
                        else
                        {
                            k = valorDiferenteIzq(p);
                        }
                        balance2(_nuevo, k, p);                    
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
       
            private void rightRotation (Nodo<T> pivot)
            {
                //OJO
                Nodo<T> aux = pivot.parent;
                if (aux.derecho == null)
                {
                    if (aux.CompareTo(_raiz.valor) == 0)
                    {
                        pivot.derecho = aux;
                        aux.izquierdo = null;
                        aux.parent = pivot;
                        _raiz = pivot;
                        _raiz.parent = null;
                    }
                    else
                    {
                        //COMPARACION SI ES HIJO DERECHO O IZQUIERDO
                        if (pivot.parent.parent.CompareTo(pivot.parent.valor) == -1)
                        {
                            pivot.parent.parent.derecho = pivot;
                        }
                        else
                        {
                            pivot.parent.parent.izquierdo = pivot;
                        }
                        pivot.derecho = aux;
                        pivot.parent = pivot.derecho.parent;
                        pivot.derecho.parent = pivot;
                        aux.izquierdo = null;
                        aux.parent = pivot;
                    }
                }
                else
                {
                    pivot.izquierdo.parent = pivot.parent;
                    pivot.parent.derecho = pivot.izquierdo;
                    pivot.izquierdo.derecho = pivot;
                    pivot.parent = pivot.izquierdo;
                    pivot.izquierdo = null;
                }
            }

           private void leftRotation(Nodo<T> pivot)
           {
                //Ready?
                Nodo<T> aux = pivot.parent;
                if (aux.izquierdo == null)
                {
                    //Rotacion más sencilla 
                    if (aux.CompareTo(_raiz.valor) == 0)
                    {
                        pivot.izquierdo = aux;
                        aux.derecho = null;
                        aux.parent = pivot;
                        _raiz = pivot;
                        _raiz.parent = null;
                    }
                    else
                    {
                        if (pivot.parent.parent.CompareTo(pivot.parent.valor) == -1)
                        {
                            //Se trata del hijo derecho
                            pivot.parent.parent.derecho = pivot;
                        }
                        else
                        {
                            pivot.parent.parent.izquierdo = pivot;
                        }
                        pivot.izquierdo = aux;
                        pivot.parent = pivot.izquierdo.parent;
                        pivot.izquierdo.parent = pivot;
                        aux.derecho = null;
                        aux.parent = pivot;       
                    }
                }
                else
                {
                    //Inserte codigo aquí jaja   
                    if (pivot.izquierdo != null)
                    {
                        aux = pivot.izquierdo;
                    }
                        
                    if (pivot.parent.parent.CompareTo(pivot.parent.valor) == -1)
                    {
                        //Se trata del hijo derecho del abuelo
                        //Inserte codigo aquí
                        pivot.parent.parent.derecho = pivot;
                    }
                    else
                    {
                        pivot.parent.parent.izquierdo = pivot;
                        //Inserte codigo aquí
                        pivot.izquierdo = pivot.parent;
                        pivot.parent = pivot.izquierdo.parent;
                        pivot.izquierdo.parent = pivot;
                        pivot.izquierdo.derecho = aux;
                        aux.parent = pivot.izquierdo;
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
            //ROTACIONES
            //Yeah
            public void balance2(Nodo<T> aux, int numeroDiferente, int raiz)
            {   
                if (raiz == 2 || raiz == 1)
                {
                    if (raiz == 2)
                    {
                        //Si pertenece al lado derecho de la raiz
                        if (numeroDiferente == -2) //ver cuaderno para apuntes
                        {
                            int i = BF(aux.parent);
                            if (i == -1)
                            {
                                rightRotation(aux.parent);
                            }
                            else if (i == 1)
                            {
                                leftRotation(aux.parent);
                            }
                            else if(i == 10) //valor provicional
                            {
                                //DOBLE ROTACION
                                rightRotation(aux);
                            }
                        }
                        else
                        {
                            leftRotation(aux.parent);
                        }
                    }
                    else
                    {
                        //Raiz FB = 1
                        if (numeroDiferente == -2)
                        {
                            rightRotation(aux.parent);
                        }
                        else if (numeroDiferente == 2)
                        {
                            leftRotation(aux.parent);
                        }
                    }
                }//Arbol tirado al lado izquierdo
                else if (raiz == -2 || raiz == -1)
                {
                    //Arbol tirado al lado derecho
                    if (numeroDiferente == 2)
                    {
                        if (BF(aux.parent) == 1)
                        {
                            if (aux.parent.parent.izquierdo == null)
                                leftRotation(aux.parent);
                            else
                            {
                                //Rotacion a la izquierda
                                leftRotation(aux.parent.parent);
                            }
                        }
                        else
                        {
                            //Doble rotacion a la izquierda
                            //Rotamos der aux y luego izq
                            rightRotation(aux.parent);
                            leftRotation(aux);
                        }
                    }
                    else
                    {
                        //Desequilibrio del lado izquierdo
                        int i = BF(aux.parent.parent);
                        if (i == 1)
                        {
                            //DobleRotacion
                        }
                        else if (i == -2)
                        {
                            if (aux.parent.parent.derecho == null)
                                rightRotation(aux.parent);
                            else
                            {
                                //Doble Rotacion a Der
                            }
                        }
                    }
                }
            }
            public void busqueda(Nodo<T> pivot)
            {

                int s = BF(pivot);
                if (Math.Abs(s) != 2)
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
            
            //Need coment
            //Jjejeje
            public int valorDiferenteDer(int n)
            {
                Nodo<T> aux = _raiz;
                while (BF(aux) == n)
                {
                    aux = aux.derecho;
                }
                return BF(aux);
            }

            //Hola
            //NEED COMENT
            public int valorDiferenteIzq(int n)
            {
                Nodo<T> aux = _raiz;
                while (BF(aux) == n)
                {
                    aux = aux.izquierdo;
                }
                return BF(aux);
            }
    }
}   
            
