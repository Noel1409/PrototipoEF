using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaModelo735;
using System.Data;
namespace CapaControlador735
{
    public class controlador
    {
        //Jaime Noel López Daniel 0901-18-735
        //Instancia de la clase sentencias, para utilizar sus métodos en esta capa
        sentenciasSQL sentencias = new sentenciasSQL();
        //-------Genéricos CRUD no transaccional----\\
        //Consulta a una tabla para llenar el data grid view
        public DataTable llenarDGV(string tabla)
        {
            return sentencias.llenarDGV(tabla);
            //debido a que el procesamiento se hace en la capa modelo, ya no necesitamos procesar la data en esta capa
        }

        //Traslado de información para la inserción
        public bool insertar(string[] campos, string[] datos, string tabla)
        {
            return sentencias.insertar(campos, datos, tabla);
        }

        //Jaime Noel López Daniel 0901-18-735
        //Traslado de info para la actualización
        public bool modificar(string[] campos, string[] datos, string tabla)
        {
            return sentencias.modificar(campos, datos, tabla);
        }

        //Traslado de info para dar de baja
        public bool eliminar(string campo, string dato, string tabla, string campoID, string ID)
        {
            return sentencias.eliminar(campo, dato, tabla, campoID, ID);
        }

        //Para la generación genérica de siguientes ID's:
        public string generarIdSiguiente(string tabla)
        {//Método para regresar el ID para un nuevo registro, recibe como parámetro la tabla en la que contar para saber
         //que id será el siguiente
            int cuenta = sentencias.conteo(tabla);//obtiene la cantidad de registros de la tabla
            return (cuenta + 1).ToString();//le suma 1 a la cantidad de registros, y lo regresa en string para que se coloque en las textBox
        }


    }
}
