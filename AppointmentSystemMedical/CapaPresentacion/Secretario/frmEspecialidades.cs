﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaPresentacion.Secretario
{
    public partial class frmEspecialidades : Form
    {
        private frmPrincipal Padre;

        public frmEspecialidades(frmPrincipal padre)
        {
            InitializeComponent();
            Padre = padre;
        }

        private void frmEspecialidades_Load(object sender, EventArgs e)
        {
            AdministrarPermisos();
            CapaLogica.Especialidad.CargarDataGrid(grdEspecialidades);
        }

        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "Buscar...")
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (txtBuscar.Text == "")
            {
                txtBuscar.Text = "Buscar...";
                txtBuscar.ForeColor = Color.FromArgb(62, 62, 64);
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Buscar();
            }
        }

        private void picBuscarIcono_Click(object sender, EventArgs e)
        {
            Buscar();
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

        private void picNuevoIcono_Click(object sender, EventArgs e)
        {
            Padre.AbrirFormPanel(new Administrador.frmNuevoEspecialidad(Padre));
        }

        private void grdEspecialidades_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                Padre.AbrirFormPanel(new Administrador.frmNuevoEspecialidad(Padre, (int)grdEspecialidades.Rows[e.RowIndex].Cells["Id"].Value));
            }
        }

        private void AdministrarPermisos()
        {
            switch (Padre.Sesion.TipoUsuario.Id)
            {
                case 1:
                    // Administrador
                    picNuevo.Visible = true;
                    grdEspecialidades.Columns["Editar"].Visible = true;
                    break;
            }
        }

        private void Buscar()
        {
            if (txtBuscar.Text == "Buscar...") return;

            if (txtBuscar.Text == "")
            {
                CapaLogica.Especialidad.CargarDataGrid(grdEspecialidades);
            }
            else
            {
                CapaLogica.Especialidad.CargarDataGrid(grdEspecialidades, txtBuscar.Text);
            }
        }
    }
}
