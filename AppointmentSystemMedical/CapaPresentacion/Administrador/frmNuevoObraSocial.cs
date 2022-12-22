using AppointmentSystemMedical.CapaLogica;
using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaPresentacion.Administrador
{
    public partial class frmNuevoObraSocial : Form
    {
        private frmPrincipal Padre;
        private int Id;
        ObraSocial obraSocial = new ObraSocial();
        public frmNuevoObraSocial(frmPrincipal padre)
        {
            InitializeComponent();
            Padre = padre;
            Id = -1;
        }

        public frmNuevoObraSocial(frmPrincipal padre, int pId)
        {
            InitializeComponent();
            Padre = padre;
            Id = pId;
        }

        private void frmNuevoObraSocial_Load(object sender, EventArgs e)
        {
            if (Id != -1)
            {
                lblTitulo.Text = "Editar Obra Social";
                ObraSocialDTO os = obraSocial.Buscar(Id);
                txtNombre.Text = os.Nombre;
                chkActivo.Checked = os.Estado;
                chkActivo.Visible = true;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (ValidarCampos())
            {
                DialogResult res = MessageBox.Show(
                   (Id == -1) ? "¿Desea registrar una Obra Social?" : "¿Desea guardar los cambios?",
                   "Confirmación",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question,
                   MessageBoxDefaultButton.Button2);
                if (res == DialogResult.Yes)
                {
                    if (Id == -1)
                    {
                        obraSocial.Guardar(txtNombre.Text);
                    }
                    else
                    {
                        obraSocial.Editar(Id, txtNombre.Text, chkActivo.Checked);
                    }
                    Padre.AbrirFormPanel(new frmObraSociales(Padre));
                }
            }
        }

        private void txtString_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !(char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void txtNombre_Leave(object sender, EventArgs e)
        {
            txtNombre.Text = PonerMayuculas(txtNombre.Text);
        }

        private string PonerMayuculas(string str)
        {
            string resultado = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (i == 0 || str[i - 1] == ' ')
                {
                    resultado += str[i].ToString().ToUpper();
                }
                else
                {
                    resultado += str[i];
                }
            }
            return resultado;
        }

        private bool ValidarCampos()
        {
            bool camposValidos = true;
            errNuevoObraSocial.Clear();

            if (String.IsNullOrWhiteSpace(txtNombre.Text))
            {
                errNuevoObraSocial.SetError(txtNombre, "Debe ingresar una Obra Social");
                camposValidos = false;
            }

            return camposValidos;
        }
    }
}