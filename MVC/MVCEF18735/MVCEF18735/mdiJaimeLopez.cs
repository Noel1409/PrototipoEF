using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaVistaSeguridadHSC;
namespace CapaVista18735
{
    public partial class mdiJaimeLopez : Form
    {
        public mdiJaimeLopez()
        {
            InitializeComponent();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void movimientoDeProductoEntreBódegasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Help.ShowHelp(parent, rutaAyudaCHM, rutaAyudaHTML);
            Help.ShowHelp(this.Parent, "Ayuda/Ayuda1.chm", "Ayuda/AyudaPrincipal.html");
        }

        private void reporteDeMovimientoDeProductoEntreBódegasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ruta = "Reporte/Reporte1.rpt";
            formReporte frm = new formReporte();
            frm.ruta = ruta;
            frm.Show();
        }

        private void mdiJaimeLopez_Load(object sender, EventArgs e)
        {
            frmLoginHSC form = new frmLoginHSC();
            if (form.ShowDialog() == DialogResult.OK)
            {
                txtUsuario.Text = form.usuario();
            }
            else
            {
                this.Close();
            }
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLoginHSC form = new frmLoginHSC();
            if (form.ShowDialog() == DialogResult.OK)
            {
                txtUsuario.Text = form.usuario();
                this.Show();
            }
            else
            { 
                this.Close(); 
            }
        }
    }
}
