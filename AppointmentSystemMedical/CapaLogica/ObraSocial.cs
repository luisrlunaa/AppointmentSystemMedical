using AppointmentSystemMedical.CapaDatos;
using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaLogica
{
    public class ObraSocial
    {
        ObraSocialDAL obraSocialDAL = new ObraSocialDAL();
        public ObraSocialDTO Buscar(int id)
        {
            var (result, message) = obraSocialDAL.Buscar(id);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            return result;
        }

        public void CargarComboBox(ComboBox cb)
        {
            int ancho = 0;
            int maximo = 0;
            var (result, message) = obraSocialDAL.Buscar();
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            foreach (ObraSocialDTO temp in result)
            {
                // calculo en ancho mas largo de texto
                ancho = TextRenderer.MeasureText(temp.Nombre, cb.Font).Width;
                if (maximo < ancho)
                {
                    maximo = ancho;
                }
                if (temp.Id != 7)
                {
                    cb.Items.Add(new KeyValuePair<int, String>(temp.Id, temp.Nombre));
                }
            }
            cb.ValueMember = "Key";
            cb.DisplayMember = "Value";
            cb.SelectedIndex = -1;
            cb.DropDownWidth = maximo;
        }

        public void CargarDataGrid(DataGridView grd)
        {
            grd.Rows.Clear();
            var (result, message) = obraSocialDAL.Buscar();
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var os = result;
            foreach (ObraSocialDTO temp in os)
            {
                if (temp.Id != 7)
                {
                    grd.Rows.Add(
                        (temp.Id == 1) ? null : AppointmentSystemMedical.Properties.Resources.feather_edit,
                        temp.Id,
                        temp.Nombre);
                }
            }
        }

        public void CargarDataGrid(DataGridView grd, string apenom)
        {
            grd.Rows.Clear();
            var (result, message) = obraSocialDAL.Buscar(apenom);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var os = result;
            foreach (ObraSocialDTO temp in os)
            {
                if (temp.Id != 7)
                {
                    grd.Rows.Add(
                        (temp.Id == 1) ? null : AppointmentSystemMedical.Properties.Resources.feather_edit,
                        temp.Id,
                        temp.Nombre);
                }
            }
        }

        public void Guardar(string nom)
        {
            ObraSocialDTO nos = new ObraSocialDTO(nom, true);
            var (save, message) = obraSocialDAL.Guardar(nos);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "La Obra Social fue guardada correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al guardar la Obra Social.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void Editar(int id, string nom, bool estado)
        {
            ObraSocialDTO nos = new ObraSocialDTO(id, nom, estado);
            var (save, message) = obraSocialDAL.Editar(nos);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "La Obra Social fue modificada correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al modificar la Obra Social.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
