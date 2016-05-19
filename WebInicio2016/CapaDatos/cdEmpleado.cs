using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebInicio2016.EntidadesNegocio;

namespace WebInicio2016.CapaDatos
{
    public class cdEmpleado
    {
        #region Operaciones de Filtrado  *****
        public enEmpleado ConsEmpleadoPorID(SqlConnection con, int? ID)
        {
            using (con)
            {
                enEmpleado oenEmpleado = null;
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SELECT * FROM Employees Where EmployeeID = " + ID;
                comando.Connection = con;
                SqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    oenEmpleado = new enEmpleado();
                    reader.Read();
                    oenEmpleado = ObtenerEmpleado(reader);
                }
                return oenEmpleado;
            }
        }  

        public List<enEmpleado> ListarFiltro(SqlConnection con,int NumRegistro, int TamPagina, String Apellido, String Nombre)
        {
            List<enEmpleado> lenEmpleado = null;
            //     SqlCommand cmd = new SqlCommand("uspEmployeesListar", con);
            //     cmd.CommandType = CommandType.StoredProcedure;
        // bien    String sql = "SELECT * FROM Employees WHERE LastName like '%" + Apellido + "%' ORDER BY EmployeeID OFFSET 2 ROWS FETCH NEXT 3 ROWS ONLY";
            String sql = "SELECT * FROM Employees WHERE LastName like '%" + Apellido + "%'";
            sql += "ORDER BY EmployeeID OFFSET " + NumRegistro + " ROWS FETCH NEXT " + TamPagina +" ROWS ONLY";
 
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

            if (drd != null)
            {
                lenEmpleado = new List<enEmpleado>();
                enEmpleado oenEmpleado = new enEmpleado();
                int posIdEmpleado = drd.GetOrdinal("EmployeeID");
                int posApellido = drd.GetOrdinal("LastName");
                int posNombre = drd.GetOrdinal("FirstName");
                int posFechaNacimiento = drd.GetOrdinal("BirthDate");
                while (drd.Read())
                {
                    oenEmpleado = new enEmpleado();
                    oenEmpleado.IdEmpleado = drd.GetInt32(posIdEmpleado);
                    oenEmpleado.Apellido = drd.GetString(posApellido);
                    oenEmpleado.Nombre = drd.GetString(posNombre);
                    oenEmpleado.FechaNacimiento = drd.GetDateTime(posFechaNacimiento);
                    lenEmpleado.Add(oenEmpleado);
                }
                drd.Close();
            }
            return (lenEmpleado);
        }

        public int NumRegistrosFiltro(SqlConnection con, String Apellido, String Nombre)
        {
            int NumRegistros = 0;
            String sql = "SELECT count(*) FROM Employees WHERE LastName like '%" + Apellido + "%'";
            SqlCommand cmd = new SqlCommand(sql, con);
            NumRegistros = Convert.ToInt32(cmd.ExecuteScalar());
            return (NumRegistros);
        }

        public List<enEmpleado> Listar(SqlConnection con)
        {
            List<enEmpleado> lenEmpleado = null;
       //     SqlCommand cmd = new SqlCommand("uspEmployeesListar", con);
       //     cmd.CommandType = CommandType.StoredProcedure;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Employees",con);
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

            if (drd != null)
            {
                lenEmpleado = new List<enEmpleado>();
                enEmpleado oenEmpleado = new enEmpleado();
                int posIdEmpleado = drd.GetOrdinal("EmployeeID");
                int posApellido = drd.GetOrdinal("LastName");
                int posNombre = drd.GetOrdinal("FirstName");
                int posFechaNacimiento = drd.GetOrdinal("BirthDate");
                while (drd.Read())
                {
                    oenEmpleado = new enEmpleado();
                    oenEmpleado.IdEmpleado = drd.GetInt32(posIdEmpleado);
                    oenEmpleado.Apellido = drd.GetString(posApellido);
                    oenEmpleado.Nombre = drd.GetString(posNombre);
                    oenEmpleado.FechaNacimiento = drd.GetDateTime(posFechaNacimiento);
                    lenEmpleado.Add(oenEmpleado);
                }
                drd.Close();
            }
            return (lenEmpleado);
        }
        #endregion
        #region Operaciones de Mantenimiento  *****
        /// Metodo para insertar un registro en la tabla Alumno
        /// Me devuelve un int (-1=fallo y 1=Bien).
        /// Todos los métodos pueden generar una excepcion.
        /// En el objeto oAlumno que recibe, IdAlumno=0 porque no se usa.
        public int AltaEmpleado(SqlConnection con, enEmpleado oenEmpleado)
        {
            using (con)
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "INSERT INTO Employees (LastName, FirstName, Title, TitleOfCourtesy, BirthDate, HireDate, Address, City, Region, PostalCode, Country, HomePhone, Extension, Notes, ReportsTo, PhotoPath)";
                comando.CommandText += "VALUES(@LastName, @FirstName, @Title, @TitleOfCourtesy, @BirthDate, @HireDate, @Address, @City, @Region, @PostalCode, @Country, @HomePhone, @Extension, @Notes, @ReportsTo, @PhotoPath)";
                comando.Parameters.AddWithValue("@LastName", oenEmpleado.Apellido);
                comando.Parameters.AddWithValue("@FirstName", oenEmpleado.Nombre);
                comando.Parameters.AddWithValue("@Title", "");
                comando.Parameters.AddWithValue("@TitleOfCourtesy", "");
                comando.Parameters.AddWithValue("@BirthDate", Convert.ToDateTime("27/01/1966 0:00:00"));
                comando.Parameters.AddWithValue("@HireDate", Convert.ToDateTime("27/01/1966 0:00:00"));
                comando.Parameters.AddWithValue("@Address", "");
                comando.Parameters.AddWithValue("@City", "");
                comando.Parameters.AddWithValue("@Region", "");
                comando.Parameters.AddWithValue("@PostalCode", "");
                comando.Parameters.AddWithValue("@Country", "");
                comando.Parameters.AddWithValue("@HomePhone", "");
                comando.Parameters.AddWithValue("@Extension", "");
        //        comando.Parameters.AddWithValue("@Photo", Convert.ToByte("",0));
                comando.Parameters.AddWithValue("@Notes", "");
                comando.Parameters.AddWithValue("@ReportsTo", 2);
                comando.Parameters.AddWithValue("@PhotoPath", ""); 
                comando.Connection = con;
                int retorno = comando.ExecuteNonQuery();
                return retorno;
            }
        }

        public int ModiEmpleado(SqlConnection con, enEmpleado oenEmpleado)
        {  /// En oenEmpleado me llega el Id del Empleado a modificar y los nuevos datos.
            using (con)
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "UPDATE Employees SET LastName=@LastName, FirstName=@FirstName WHERE EmployeeID =" + oenEmpleado.IdEmpleado;
                comando.Parameters.AddWithValue("@LastName", oenEmpleado.Apellido);
                comando.Parameters.AddWithValue("@FirstName", oenEmpleado.Nombre);
      //          comando.Parameters.AddWithValue("@BirthDate", oenEmpleado.FechaNacimiento);
                comando.Connection = con;
                int retorno = comando.ExecuteNonQuery();
                return retorno;
            }
        }

        public int BajaEmpleado(SqlConnection con, int? IdEmpleado)
        {     /// En el parámetro me llega el Id del registro a dar de baja
            using (con)
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "DELETE FROM Employees WHERE EmployeeID = " + IdEmpleado;
                comando.Connection = con;
                int retorno = comando.ExecuteNonQuery();
                return retorno;
            }
        }
        #endregion


        #region Metodos privados de esta clase  *****
        private enEmpleado ObtenerEmpleado(IDataReader read)
        {
            enEmpleado oenEmpleado = new enEmpleado();
            oenEmpleado.IdEmpleado = (int)read["EmployeeID"];
            oenEmpleado.Apellido = (String)read["LastName"];
            oenEmpleado.Nombre = (String)read["FirstName"];
            oenEmpleado.FechaNacimiento = (DateTime)read["BirthDate"];
            return oenEmpleado;
        }
        #endregion
    }
}