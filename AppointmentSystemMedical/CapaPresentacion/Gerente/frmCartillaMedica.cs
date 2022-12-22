using Microsoft.Reporting.WinForms;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaPresentacion.Gerente
{
    public partial class frmCartillaMedica : Form
    {
        private frmPrincipal Padre;

        public frmCartillaMedica(frmPrincipal padre)
        {
            InitializeComponent();
            Padre = padre;
        }

        private void frmEstadisticas_Load(object sender, System.EventArgs e)
        {
            //CargarReporte();
        }

        //private void CargarReporte()
        //{
        //    using (AppointmentSystemMedicalEntities db = new AppointmentSystemMedicalEntities())
        //    {
        //        ObtenerCartillaMedica_ResultBindingSource.DataSource = db.ObtenerCartillaMedica();
        //        ObtenerFechaActual_ResultBindingSource.DataSource = db.ObtenerFechaActual();
        //        this.rptViewer.RefreshReport();
        //    }
        //    rptViewer.SetDisplayMode(DisplayMode.PrintLayout);
        //    rptViewer.ZoomMode = ZoomMode.PageWidth;
        //}
    }
}
