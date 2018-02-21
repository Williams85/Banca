using SGCFVIEF.Entidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGCFVIEF.Repositorio
{
    public class UsuarioRepositorio
    {
        #region "Metodos No Transaccionales"

        public UsuarioEntidad ValidarUsuario(UsuarioEntidad entidad)
        {

            try
            {
                Conector.abrirConexion();
                SqlCommand cmd = new SqlCommand("usp_Usuario_ValidarUsuario", Conector.Conexion);
                cmd.Parameters.Add(new SqlParameter("@Nom_Usuario", SqlDbType.VarChar, 150)).Value = entidad.Nom_Usuario;
                cmd.Parameters.Add(new SqlParameter("@Pass_Usuario", SqlDbType.VarChar, 100)).Value = entidad.Pass_Usuario;
                cmd.CommandType = CommandType.StoredProcedure;
                UsuarioEntidad oUsuarioEntidad = new UsuarioEntidad();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        oUsuarioEntidad.Cod_Usuario = Reader.GetTinyIntValue(reader, "Cod_Usuario");
                        oUsuarioEntidad.Nom_Usuario = Reader.GetStringValue(reader, "Nom_Usuario");
                        oUsuarioEntidad.Pass_Usuario = Reader.GetStringValue(reader, "Pass_Usuario");
                        oUsuarioEntidad.Empleado = new EmpleadoEntidad
                        {
                            Cod_Empleado = Reader.GetStringValue(reader, "Cod_Empleado"),
                            Nombre = Reader.GetStringValue(reader, "Nombre"),
                            Apellido = Reader.GetStringValue(reader, "Apellido"),
                            Apellido2 = Reader.GetStringValue(reader, "Apellido2"),

                        };
                        oUsuarioEntidad.Perfil = new PerfilEntidad
                        {
                            Cod_Perfil = Reader.GetTinyIntValue(reader, "Cod_Perfil"),
                        };
                    }
                }
                return oUsuarioEntidad;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                Conector.cerrarConexion();
            }
        }

        #endregion
    }
}
