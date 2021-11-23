using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista18735
{
    public partial class mantenimientoProducto : Form
    {
        public mantenimientoProducto(string usuario)
        {
            InitializeComponent();
            this.dgvProducto.ReadOnly = true;
            this.dgvProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //Parametrización navegador
            TextBox[] alias = navegador1.funAsignandoTexts(this);
            //Textboxs, tabla y BD
            navegador1.funAsignarAliasVista(alias, "producto735", "hotelSanCarlos");
            navegador1.funAsignarSalidadVista(this);
            //Campo Estado
            navegador1.campoEstado = "estado";

            //el id de la aplicación en seguridad
            navegador1.idAplicacion = "07352";
            //Usuario
            navegador1.usuario = usuario;

            //Ejecución de la ayuda
            navegador1.tablaAyuda = "Aplicacion";
            navegador1.campoAyuda = "pkId";

            // Ejecución Reporte
            navegador1.funReportesVista("ruta", "idAplicacion", "Reporte");

            //Data Grid View
            navegador1.pideGrid(this.dgvProducto);
            navegador1.llenaTabla();
            //Referencia para cerrar
            navegador1.pedirRef(this);

            //Bitacora y permisos
            navegador1.aplicacion = "Mantenimiento Productos";//Nombre en seguridad
            navegador1.funActualizarPermisos();
            navegador1.idmodulo = "735";//# del modulo en seguridad
            navegador1.funReportesVista("ruta", "idAplicacion", "Reporte");
        }

        private void dgvProducto_SelectionChanged(object sender, EventArgs e)
        {
            navegador1.funSeleccionarDTVista(dgvProducto);
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            navegador1.funCambioEstatusRBVista(txtEstado, radioButton1, "A");
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            navegador1.funCambioEstatusRBVista(txtEstado, radioButton2, "I");
        }

        private void txtEstado_TextChanged(object sender, EventArgs e)
        {
            navegador1.funSetearRBVista(radioButton1, radioButton2, txtEstado);
        }
    }
}
