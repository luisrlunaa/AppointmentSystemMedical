using AppointmentSystemMedical.CapaDatos;
using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaLogica
{
    public class Especialidad
    {
        EspecialidadDAL especialidadDAL = new EspecialidadDAL();
        public EspecialidadDTO Buscar(int id)
        {
            var (result, message) = especialidadDAL.Buscar(id);
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
            var (result, message) = especialidadDAL.Buscar();
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            foreach (EspecialidadDTO temp in result)
            {
                // calculo en ancho mas largo de texto
                ancho = TextRenderer.MeasureText(temp.Descripcion, cb.Font).Width;
                if (maximo < ancho)
                {
                    maximo = ancho;
                }
                cb.Items.Add(new KeyValuePair<int, String>(temp.Id, temp.Descripcion));
            }
            cb.ValueMember = "Key";
            cb.DisplayMember = "Value";
            cb.SelectedIndex = -1;
            cb.DropDownWidth = maximo;
        }

        public void CargarDataGrid(DataGridView grd)
        {
            grd.Rows.Clear();
            var (result, message) = especialidadDAL.Buscar();
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var especialidades = result;
            foreach (EspecialidadDTO temp in especialidades)
            {
                grd.Rows.Add(
                    null,
                    temp.Id,
                    temp.Descripcion);
            }
        }

        public void CargarDataGrid(DataGridView grd, string apenom)
        {
            grd.Rows.Clear();
            var (result, message) = especialidadDAL.Buscar(apenom);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var especialidades = result;
            foreach (EspecialidadDTO temp in especialidades)
            {
                grd.Rows.Add(
                    null,
                    temp.Id,
                    temp.Descripcion);
            }
        }

        public void Guardar(string desc)
        {
            EspecialidadDTO nuevo = new EspecialidadDTO(desc);
            var (save, message) = especialidadDAL.Guardar(nuevo);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "La Especialidad fue guardada correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al guardar la Especialidad.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void Editar(int id, string desc)
        {
            EspecialidadDTO nuevo = new EspecialidadDTO(id, desc);
            var (save, message) = especialidadDAL.Editar(nuevo);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "La Especialidad fue modificada correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al modificar la Especialidad.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
