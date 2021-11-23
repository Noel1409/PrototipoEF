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
    public partial class formReporte : Form
    {//Jaime Noel López Daniel 0901-18-735
     //Form para poder mostrar los reportes que son de procesos ajenos al navegador

        public string ruta = "";//ruta del reportes, se modificará desde otro form para mostrar el reporte asociado
        public formReporte()
        {
            InitializeComponent();
        }
        private void reporte_Load(object sender, EventArgs e)
        {

            CrystalDecisions.CrystalReports.Engine.ReportDocument reporte = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

            reporte.Load(@"" + ruta);
            crystalReportViewer1.ReportSource = reporte;

        }

        private void reporte_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;

            CrystalDecisions.CrystalReports.Engine.ReportDocument reporte = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
            reporte.Load(@"" + ruta);
            crystalReportViewer1.ReportSource = reporte;

        }
    }
}
