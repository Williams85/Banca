using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
namespace SGCFVIEF.Repositorio
{
    public class Conector
    {
        private static SqlConnection instancia = null;
        private static readonly object padlock = new object();
        private Conector() { }

        public static SqlConnection Conexion
        {
            get
            {
                lock (padlock)
                {
                    if (instancia == null)
                    {
                        instancia = new SqlConnection();
                        instancia.ConnectionString = ConfigurationManager.ConnectionStrings["CnBanca"].ConnectionString;
                    }
                    return instancia;
                }
            }
        }
        public static void tranRollBack(DbTransaction tran)
        {
            if (tran != null && tran.Connection != null)
                tran.Rollback();
        }
        public static void tranCommit(DbTransaction tran)
        {
            if (tran != null && tran.Connection != null)
                tran.Commit();
        }
        public static void abrirConexion()
        {
            if (instancia != null)
                instancia.Open();
        }
        public static SqlTransaction initialTransaction()
        {
            SqlTransaction trans = null;
            if (instancia != null)
                trans = instancia.BeginTransaction();
            return trans;
        }

        public static void cerrarConexion()
        {
            if (instancia != null)
                instancia.Close();
        }
    }
}
