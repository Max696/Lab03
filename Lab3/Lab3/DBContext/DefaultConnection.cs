using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AVL;
using Lab3.Models;

namespace Lab3.DBContext
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();

        
        public List<string> bitacora = new List<string>();
        public List<PartidoFecha> fecha = new List<PartidoFecha>();
        public List<PartidoNo> no = new List<PartidoNo>();

        public ArbolAVL<PartidoFecha> arbolFecha = new ArbolAVL<PartidoFecha>();
        public ArbolAVL<PartidoNo> arbolNo = new ArbolAVL<PartidoNo>();

        public int IDActual { get; set; }

        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }
    }
}