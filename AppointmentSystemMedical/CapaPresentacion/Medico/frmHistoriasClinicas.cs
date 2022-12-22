﻿using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaPresentacion.Medico
{
    public partial class frmHistoriasClinicas : Form
    {
        private frmPrincipal Padre;
        private PacienteDTO Pac;
        private TurnoDTO Tur;
        private int rowTur;
        CapaLogica.HistoriaClinica historiaClinica = new CapaLogica.HistoriaClinica();
        CapaLogica.Paciente paciente = new CapaLogica.Paciente();
        CapaLogica.Turno turno = new CapaLogica.Turno();
        public frmHistoriasClinicas(frmPrincipal padre, int idpaciente)
        {
            InitializeComponent();
            Padre = padre;
            Pac = paciente.Buscar(idpaciente);
            Tur = null;
            rowTur = -1;
        }

        private void frmHistoriasClinicas_Load(object sender, EventArgs e)
        {
            historiaClinica.CargarDataGrid(grdTurnos, Padre.Sesion, Pac);
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
            if (Tur != null)
            {
                if (grdTurnos.Rows[rowTur].Cells["ExisteHC"].Value.ToString() == "0")
                {
                    Padre.AbrirFormPanel(new Medico.frmNuevoHistoriaClinica(Padre, Tur));
                }
                else
                {
                    MessageBox.Show(
                        "Este turno ya posee una Historia Clinica",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show(
                    "Debe seleccionar un turno.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void grdTurnos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                if (e.ColumnIndex == 0 && grdTurnos.Rows[e.RowIndex].Cells["ExisteHC"].Value.ToString() == "1")
                {
                    Padre.AbrirFormPanel(new Medico.frmNuevoHistoriaClinica(Padre, turno.Buscar((int)grdTurnos.Rows[e.RowIndex].Cells["Id"].Value), 0));
                }
                else
                {
                    Tur = turno.Buscar((int)grdTurnos.Rows[e.RowIndex].Cells["Id"].Value);
                    rowTur = e.RowIndex;
                }
            }
        }

        private void picFiltrar_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(dtpDesde.Value, dtpHasta.Value) < 0)
            {
                historiaClinica.CargarDataGrid(grdTurnos, Padre.Sesion, Pac, dtpDesde.Value, dtpHasta.Value);
            }
            else
            {
                MessageBox.Show(
                    "El intervalo elegido es invalido.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void picImprimir_Click(object sender, EventArgs e)
        {
            Padre.AbrirFormPanel(new Medico.frmHCReporte(Padre, Pac.Id));
        }
    }
}
