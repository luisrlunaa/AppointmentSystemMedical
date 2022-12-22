using AppointmentSystemMedical.Model.DTOs;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaPresentacion.Medico
{
    public partial class frmTurnosDelDia : Form
    {
        private frmPrincipal Padre;
        CapaLogica.Medico medico = new CapaLogica.Medico();
        public frmTurnosDelDia(frmPrincipal padre)
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
        //        MedicoDTO med = medico.BuscarDni(Padre.Sesion.Persona.Dni);
        //        ObtenerTurnosDelDia_ResultBindingSource.DataSource = db.ObtenerTurnosDelDia(med.Id, DateTime.Now);
        //        ObtenerFechaActual_ResultBindingSource.DataSource = db.ObtenerFechaActual();
        //        ObtenerNombreCompleto_ResultBindingSource.DataSource = db.ObtenerNombreCompleto(Padre.Sesion.Persona.Id);
        //        this.rptViewer.RefreshReport();
        //    }
        //    rptViewer.SetDisplayMode(DisplayMode.PrintLayout);
        //    rptViewer.ZoomMode = ZoomMode.PageWidth;
        //}
    }
}
