using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaPresentacion.Medico
{
    public partial class frmNuevoHistoriaClinica : Form
    {
        private frmPrincipal Padre;
        private TurnoDTO Tur;
        private HistoriaClinicaDTO HC;
        CapaLogica.HistoriaClinica historiaClinica = new CapaLogica.HistoriaClinica();
        public frmNuevoHistoriaClinica(frmPrincipal padre, TurnoDTO tur)
        {
            InitializeComponent();
            Padre = padre;
            Tur = tur;
            HC = null;
        }

        public frmNuevoHistoriaClinica(frmPrincipal padre, TurnoDTO tur, int id)
        {
            InitializeComponent();
            Padre = padre;
            Tur = tur;
            HC = historiaClinica.BuscarTurno(tur);
        }

        private void frmNuevoHistoriaClinica_Load(object sender, EventArgs e)
        {
            txtTurno.Text = Tur.Paciente.Persona.Apellidos + " (" + Tur.FechaHora.Day + "/" + Tur.FechaHora.Month + ")";
            if (HC != null)
            {
                lblTitulo.Text = "Editar Historia Clinica";
                txtDescripcion.Text = HC.Descripcion;
                txtArchivo.Text = HC.Archivo;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                DialogResult res = MessageBox.Show(
                   (HC == null) ? "¿Desea registrar una Historia Clinica?" : "¿Desea guardar los cambios?",
                   "Confirmación",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question,
                   MessageBoxDefaultButton.Button2);
                if (res == DialogResult.Yes)
                {
                    if (HC == null)
                    {
                        historiaClinica.Guardar(Tur, txtDescripcion.Text, txtArchivo.Text);
                    }
                    else
                    {
                        historiaClinica.Editar(HC.Id, Tur, txtDescripcion.Text, txtArchivo.Text);
                    }
                    Padre.AbrirFormPanel(new Medico.frmHistoriasClinicas(Padre, Tur.Paciente.Id));
                }
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            Padre.AbrirFormPanel(new Medico.frmHistoriasClinicas(Padre, Tur.Paciente.Id));
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dlgArchivo = new OpenFileDialog();
            if (dlgArchivo.ShowDialog() == DialogResult.OK)
            {
                txtArchivo.Text = dlgArchivo.FileName;
                // fix save file
            }
        }

        private void picButton_MouseEnter(object sender, EventArgs e)
        {
            PictureBox picSender = sender as PictureBox;
            picSender.BackColor = Color.FromArgb(60, 60, 60);
        }

        private void picButton_MouseLeave(object sender, EventArgs e)
        {
            PictureBox picSender = sender as PictureBox;
            picSender.BackColor = Color.FromArgb(45, 45, 45);
        }

        private bool ValidarCampos()
        {
            bool camposValidos = true;
            errNuevoMedico.Clear();

            if (String.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                errNuevoMedico.SetError(txtDescripcion, "Debe ingresar un Apellido");
                camposValidos = false;
            }

            return camposValidos;
        }
    }
}