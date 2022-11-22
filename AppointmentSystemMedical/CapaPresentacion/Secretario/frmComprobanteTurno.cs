using Microsoft.Reporting.WinForms;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaPresentacion.Secretario
{
    public partial class frmComprobanteTurno : Form
    {
        private frmPrincipal Padre;

        public frmComprobanteTurno(frmPrincipal padre)
        {
            InitializeComponent();
            Padre = padre;
        }

        private void frmEstadisticas_Load(object sender, System.EventArgs e)
        {
            CargarReporte();
        }

        private void CargarReporte()
        {
            using (AppointmentSystemMedicalEntities db = new AppointmentSystemMedicalEntities())
            {
                ObtenerFechaActual_ResultBindingSource.DataSource = db.ObtenerFechaActual();
                ObtenerUltimoTurno_ResultBindingSource.DataSource = db.ObtenerUltimoTurno();
                this.rptViewer.RefreshReport();
            }
            rptViewer.SetDisplayMode(DisplayMode.PrintLayout);
            rptViewer.ZoomMode = ZoomMode.PageWidth;
        }
    }
}
