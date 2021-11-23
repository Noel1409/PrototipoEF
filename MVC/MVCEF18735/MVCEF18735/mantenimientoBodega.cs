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
    public partial class mantenimientoBodega : Form
    {/*Jaime Noel López Daniel 0901-18-735*/
        
        public mantenimientoBodega(string usuario)
        {
            InitializeComponent();
            this.dgvBodega.ReadOnly = true;
            this.dgvBodega.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //Parametrización navegador
            TextBox[] alias = navegador1.funAsignandoTexts(this);
            //Textboxs, tabla y BD
            navegador1.funAsignarAliasVista(alias, "bodega735", "hotelSanCarlos");
            navegador1.funAsignarSalidadVista(this);
            //Campo Estado
            navegador1.campoEstado = "estado";

            //el id de la aplicación en seguridad
            navegador1.idAplicacion = "07351";
            //Usuario
            navegador1.usuario = usuario;
            
            //Ejecución de la ayuda
            navegador1.tablaAyuda = "Aplicacion";
            navegador1.campoAyuda = "pkId";
            
            // Ejecución Reporte
            navegador1.funReportesVista("ruta", "idAplicacion", "Reporte");

            //Data Grid View
            navegador1.pideGrid(this.dgvBodega);
            navegador1.llenaTabla();
            //Referencia para cerrar
            navegador1.pedirRef(this);

            //Bitacora y permisos
            navegador1.aplicacion = "Mantenimiento Bodegas";//Nombre en seguridad
            navegador1.funActualizarPermisos();
            navegador1.idmodulo = "735";//# del modulo en seguridad
            navegador1.funReportesVista("ruta", "idAplicacion", "Reporte");
        }

        private void dgvBodega_SelectionChanged(object sender, EventArgs e)
        {
            navegador1.funSeleccionarDTVista(dgvBodega);
        }

        private void rdbActivo_CheckedChanged(object sender, EventArgs e)
        {
            navegador1.funCambioEstatusRBVista(txtEstado, rdbActivo, "A");
        }

        private void rdbInactivo_CheckedChanged(object sender, EventArgs e)
        {
            navegador1.funCambioEstatusRBVista(txtEstado, rdbInactivo, "I");
        }

        private void txtEstado_TextChanged(object sender, EventArgs e)
        {
            navegador1.funSetearRBVista(rdbActivo, rdbInactivo, txtEstado);
        }

        private void rdbActivo_MouseClick(object sender, MouseEventArgs e)
        {
            navegador1.funCambioEstatusRBVista(txtEstado, rdbActivo, "A");
        }

        private void rdbInactivo_MouseClick(object sender, MouseEventArgs e)
        {
            navegador1.funCambioEstatusRBVista(txtEstado, rdbInactivo, "I");
        }
    }
}
