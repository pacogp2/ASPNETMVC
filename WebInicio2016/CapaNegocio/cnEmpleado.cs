using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebInicio2016.EntidadesNegocio;
using WebInicio2016.CapaDatos;

namespace WebInicio2016.CapaNegocio
{
    public class cnEmpleado
    {
        string CadenaConexion = ConfigurationManager.ConnectionStrings["conNW"].ConnectionString;
        //      string CadenaConexion = "Data Source=(LocalDB)\\v11.0;AttachDbFilename=|DataDirectory|\\northwind.mdf;Integrated Security=True";   
        String ValidacionEmpleado;
        #region Operaciones de Filtrado  *****
        
        public List<enEmpleado> Listar()
        {
            List<enEmpleado> lenEmpleado = null;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                try
                {
                    con.Open();    
                    cdEmpleado ocdEmpleado = new cdEmpleado();
                    lenEmpleado = ocdEmpleado.Listar(con);
                    con.Close();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error en SQLException (cnEmpleado - Listar()) =" + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en cnEmpleado.Listar() -> cdEmpleado.Listar(con) ) =" + ex.Message);
                }
            } //con.close(); con.Dispose();
            return (lenEmpleado);
        }

        public List<enEmpleado> ListarFiltro(int NumRegistro, int TamPagina, String Apellido, String Nombre)
        {
            List<enEmpleado> lenEmpleado = null;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                try
                {
                    con.Open();
                    cdEmpleado ocdEmpleado = new cdEmpleado();
                    lenEmpleado = ocdEmpleado.ListarFiltro(con, NumRegistro, TamPagina, Apellido, Nombre);
                    con.Close();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error en SQLException (cnEmpleado - Listar()) =" + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en cnEmpleado.Listar() -> cdEmpleado.Listar(con) ) =" + ex.Message);
                }
            } //con.close(); con.Dispose();
            return (lenEmpleado);
        }

        public int NumRegistrosFiltro(String Apellido, String Nombre)
        {
            int NumRegistros = 0;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                try
                {
                    con.Open();
                    cdEmpleado ocdEmpleado = new cdEmpleado();
                    NumRegistros = ocdEmpleado.NumRegistrosFiltro(con, Apellido, Nombre);
                    con.Close();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error en SQLException (cnEmpleado - NumRegistrosFiltro()) =" + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en cnEmpleado.Listar() -> cdEmpleado.NumRegistrosFiltro(con) ) =" + ex.Message);
                }
            } //con.close(); con.Dispose();
            return (NumRegistros);
        }
        public enEmpleado ConsEmpleadoPorID(int? ID)
        {
            enEmpleado oenEmpleado = null;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                try
                {
                    con.Open();
                    cdEmpleado ocdEmpleado = new cdEmpleado();
                    oenEmpleado = ocdEmpleado.ConsEmpleadoPorID(con, ID);
                    con.Close();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error en SQLException (cnEmpleado - Listar()) =" + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en cnEmpleado.Listar() -> cdEmpleado.Listar(con) ) =" + ex.Message);
                }
            } //con.close(); con.Dispose();
            return (oenEmpleado);
        }
        #endregion

        #region Operaciones de Mantenimiento  *****
        /// Metodo para insertar un registro en la tabla Empleado
        /// Me devuelve un int (-1=fallo y 1=Bien).
        /// Todos los métodos pueden generar una excepcion.
        /// En el objeto oenEmpleado que recibe, IdEmpleado=0 porque no se usa.
        public int AltaEmpleado(enEmpleado oenEmpleado)
        {
            //  Validamos los datos para dar alta del empleado
            int resultado = -1;
            if (ValidarEmpleado(oenEmpleado))
            {
                // Modificamos los datos del empleado que me llega como parámetro.
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                try
                {
                    con.Open();
                    cdEmpleado ocdEmpleado = new cdEmpleado();
                    resultado = ocdEmpleado.AltaEmpleado(con, oenEmpleado);
                    con.Close();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error en SQLException (cnEmpleado - AltaEmpleado()) =" + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en cnEmpleado.Listar() -> cdEmpleado.AltaEmpleado(con) ) =" + ex.Message);
                }
            } //con.close(); con.Dispose();
            }
            else
            {
                throw new Exception("Error en cnEmpleado.AltaEmplado() -> validación datos:" + ValidacionEmpleado);
            }
            return (resultado);
        }
        /// Metodo para modificar un registro en la tabla Empleado
        /// Me devuelve un int (-1=fallo y 1=Bien).
        /// Todos los métodos pueden generar una excepcion.
        /// En el objeto oenEmpleado que recibe, IdEmpleado=id a modificar.
        public int ModiEmpleado(enEmpleado oenEmpleado)
        {
            //  Validamos los datos para modificar empleado
            int resultado = -1;
            if (ValidarEmpleado(oenEmpleado))
            {
                // Modificamos los datos del empleado que me llega como parámetro.
                using (SqlConnection con = new SqlConnection(CadenaConexion))
                {
                    try
                    {
                        con.Open();
                        cdEmpleado ocdEmpleado = new cdEmpleado();
                        resultado = ocdEmpleado.ModiEmpleado(con, oenEmpleado);
                        con.Close();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception("Error en SQLException (cnEmpleado - AltaEmpleado()) =" + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error en cnEmpleado.Listar() -> cdEmpleado.AltaEmpleado(con) ) =" + ex.Message);
                    }
                } //con.close(); con.Dispose();
            }
            else
            {
                throw new Exception("Error en cnEmpleado.ModiEmplado() -> validación datos:" + ValidacionEmpleado);
            }
            return (resultado);
        }

        public int BajaEmpleado(int? ID)
        {
            int resultado = -1;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                try
                {
                    con.Open();
                    cdEmpleado ocdEmpleado = new cdEmpleado();
                    resultado = ocdEmpleado.BajaEmpleado(con, ID);
                    con.Close();
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error en SQLException (cnEmpleado - AltaEmpleado()) =" + ex.Message);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error en cnEmpleado.Listar() -> cdEmpleado.AltaEmpleado(con) ) =" + ex.Message);
                }
            } //con.close(); con.Dispose();
            return (resultado);
        }
        #endregion
        #region Operaciones de validación  *****
        private bool ValidarEmpleado(enEmpleado oenEmpleado)
        {
            if (string.IsNullOrEmpty(oenEmpleado.Nombre)) ValidacionEmpleado += "El campo Nombre es obligatorio";
            if (string.IsNullOrEmpty(oenEmpleado.Apellido)) ValidacionEmpleado += "El campo Apellido es obligatorio";
            if (ValidacionEmpleado == null)
                return true;
            else
                return false;
        }
        #endregion
    }
}