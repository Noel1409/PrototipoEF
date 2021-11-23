using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace CapaModelo735
{
    //Esta Clase es usada para el proceso transaccional
    class conexion
    {
        //Jaime Noel López Daniel 0901-18-735
        public OdbcConnection abrirConexion()
        {//Método para aperturara la conexión, esta es por medio de ODBC

            string DSN = "conexionHSC";
            //Se guarda el nombre del ODBC en un string para poder cambiarlo aquí y que en el error se muestre por si se necesita depurar
            OdbcConnection conexion = new OdbcConnection("Dsn=" + DSN);
            //Se tiene el nombre del DSN, el cual tiene el usuario y la contraseña de acceso
            try
            {
                conexion.Open();
                //se intenta abrir la conexión
            }
            catch (OdbcException)
            {
                Console.WriteLine("¡No se ha podido aperturar la conexión!\n" +
                    "Revisar el nombre del ODBC: " + DSN);
            }
            return conexion;
        }
        //Jaime Noel López Daniel 0901-18-735
        public void cerrarConexion(OdbcConnection conexion)
        {//Método para cerrar una conexión abierta, este recibe un objeto de conexión ODBC e intenta cerrarlo
            try
            {
                conexion.Close();
            }
            catch (OdbcException)
            {
                Console.WriteLine("Error cerrando la conexión");//Este error no debería de suceder en teoría pero
                //se tiene el mensaje para depuración en cualquier caso
            }
        }
    }
}
