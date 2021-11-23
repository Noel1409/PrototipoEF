using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;
using System.Data;
using CapaModelo735;
namespace CapaModelo735
{
    public class sentenciasSQL
    {
        //esta es una clase publica tipo objeto común, se usará para guardar los métodos que usan código SQL
        //debido al aislamiento necesario del MVC
        //Jaime Noel López Daniel 0901-18-735

        //--------------------globales-------------------
        //global para manejar la conexion, se usará esta variable para aperturar y cerrar conexiones
        private conexion Conexion = new conexion();

        //-- Métodos genéricos- CRUD no transaccional --\\
        //Jaime Noel López Daniel 0901-18-735
        public DataTable llenarDGV(string tabla)
        {//método para llenar un data grid view, este regresa un objeto dataTable para crear un mejor aislamiento y no 
         //instanciar objetos de ODBCDataAdapter en el controlador

            string select = "SELECT * FROM " + tabla + ";";//select genérico, útil para cualquier tabla

            OdbcConnection conn = Conexion.abrirConexion();//usamos la variable global como puente para abrir y obtener la conexion
            OdbcDataAdapter dataTable = new OdbcDataAdapter(select, conn);
            Conexion.cerrarConexion(conn);//cerramos la conexion
            //procesamiento de la data
            DataTable table = new DataTable();
            dataTable.Fill(table);
            return table;
            //transformarmos el ODBCDataAdapter a un objeto data table
        }

        //Jaime Noel López Daniel 0901-18-735
        public bool insertar(string[] campos, string[] datos, string tabla)
        {//inserta los datos enviados en la BD, y como recibimos una lista con los campos, podemos insertar solo en algunos

            int resultado = 0;
            //usado para almacenar el resultado de la operación
            OdbcConnection conn = Conexion.abrirConexion();
            string sentencia = "INSERT INTO " + tabla + "(";//inicio genérico de la sentencia
            //En esta parte listamos todos los campos en los que ingresaremos datos
            for (int i = 0; i < campos.Length; i++)
            {
                sentencia += campos[i];
                if (i < campos.Length - 1)
                {
                    sentencia += " , ";
                }
                else
                {
                    sentencia += " ) VALUES ( ";//fin del listado de campos, ahora debemos listar los valores a insertar
                }
            }

            for (int i = 0; i < datos.Length; i++)
            {
                sentencia += " '" + datos[i] + "' ";
                if (i < datos.Length - 1)
                {
                    sentencia += " , ";
                }
                else
                {
                    sentencia += " );";//aca termina la sentencia del insert, ahora solo falta ejecutarla
                }
            }

            try
            {
                OdbcCommand ingreso = new OdbcCommand(sentencia, conn);
                //usamos un odbc command para ejecutar el insert, y si se hace bien, colocamos resultado en 1
                ingreso.ExecuteNonQuery();
                resultado = 1;
            }
            catch (OdbcException Error)
            {
                Console.WriteLine("Se produjo un error al ingresar\n" + Error);//si no se logro insetar, 
                //el resultado queda en 0 y lo validaremos para mostrar un error

            }
            Conexion.cerrarConexion(conn);
            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
            //estas variables son usadas en la capa vista para mostrar éxito o fracaso en la operación
        }

        //Jaime Noel López Daniel 0901-18-735
        public bool modificar(string[] campos, string[] datos, string tabla)
        {//Método para realizar actualizaciones de campos, recibe los campos a actualizar, los valores a colocarles, y la tabla a usar
         //Se asume que el primer campo en el arreglo es el campo ID que servirá para el where
            int resultado = 0;
            OdbcConnection conn = Conexion.abrirConexion();
            string sentencia = "UPDATE " + tabla + " SET ";//empezamos a crear la sentencia
            //Formamos los sets y sus valores
            for (int i = 1; i < campos.Length; i++)
            {
                sentencia += campos[i] + " = '" + datos[i] + "'";
                if (i < campos.Length - 1)
                {
                    sentencia += " , ";
                    //si es menor a la longitud menos 1 (hasta el penultimo dato), ingresará la coma
                    //y en el ultimo, se la saltará para que luego pongamos el where
                }
            }
            //asumimos que el primer dato enviado al arreglo es el de campo que servirá de ID
            sentencia += " WHERE " + campos[0] + " = '" + datos[0] + "';";
            //Con la sentencia formada, ejecutamos con odbc command igual que en insertar
            try
            {
                OdbcCommand modificar = new OdbcCommand(sentencia, conn);
                modificar.ExecuteNonQuery();
                resultado = 1;
            }
            catch (OdbcException Error)
            {
                Console.WriteLine("Se produjo un error al modificar\n" + Error);

            }
            //Cerramos la conexion y regresamos resultados para que la capa vista los muestre
            Conexion.cerrarConexion(conn);
            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Jaime Noel López Daniel 0901-18-735
        public bool eliminar(string campo, string dato, string tabla, string campoID, string ID)
        {//Método para dar de baja registros, tomamos en cuenta que siempre tendrán un campo de estado
         //A diferencia de antes, acá uso 5 parametros, que se podrían reducir en un arreglo pero para no confundir los datos
         //mejor lo maneje así. Campo y dato son el campo y el dato que se va a actualizar, ej: estado y 0 para ponerlo inactivo
         //el campo ID y ID son el campo que se usará como índice para ubicar el registro, y el ID el valor que tendrá ese campo
            int resultado = 0;
            OdbcConnection conn = Conexion.abrirConexion();
            string sentencia = "UPDATE " + tabla + " SET ";
            //Empezamos la sentencia de forma similar al actualizar
            //Pero como ahora especificamos el campo de estado, lo agregamos directamente sin usar for's
            sentencia += campo + " = '" + dato + "' ";
            //Por ultimo agregamos la condicional Where
            sentencia += " WHERE " + campoID + " = '" + ID + "';";
            //Y se realiza la operacion
            try
            {
                OdbcCommand eliminar = new OdbcCommand(sentencia, conn);
                eliminar.ExecuteNonQuery();
                resultado = 1;
            }
            catch (OdbcException Error)
            {
                Console.WriteLine("Se produjo un error al dar de baja\n" + Error);

            }
            //Cerramos la conexion y devolvemos el resultado
            Conexion.cerrarConexion(conn);
            if (resultado == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //-----Jaime Noel------ Métodos otros usados para los mantenimientos ------López Daniel----\\
        //Acá coloco todos los otros métodos creados para manejas aspectos más específicos, como es la generación de ID
        //automática, consultas/selects específicos, etc
        public int conteo(string tabla)
        {//Método para contar los registro en una tabla y luego usar ese número para generar el ID siguiente
         //Es util para tablas que no tienen autoincrement o cuya llave es un varchar y no un int
            string consulta = "SELECT COUNT(*) FROM " + tabla + " ;";//sentencia a ejecutar
            int conteo = 0;
            OdbcConnection conn = Conexion.abrirConexion();
            //Ahora intentamos obtener la cuenta de los registros en la tabla
            try
            {
                OdbcCommand contar = new OdbcCommand(consulta, conn);
                OdbcDataReader lector = contar.ExecuteReader();
                while (lector.Read())
                {
                    conteo = Int32.Parse(lector[0].ToString());
                }
            }
            catch (OdbcException)
            {
                Console.WriteLine("Eror al obtener el conteo");
            }
            //siempre se usa try catch por si sucediera algún error en el proceso
            Conexion.cerrarConexion(conn);
            //regresamos el número a la capa controlador para que ella se encargue de generar el siguiente ID
            return conteo;
        }
    }
}
