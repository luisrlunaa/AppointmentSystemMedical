using AppointmentSystemMedical.CapaDatos;
using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaLogica
{
    public class TipoUsuario
    {
        TipoUsuarioDAL tipoUsuarioDAL = new TipoUsuarioDAL();
        public void CargarComboBox(ComboBox cb)
        {
            var (result, message) = tipoUsuarioDAL.Buscar();
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            int ancho = 0;
            int maximo = 0;
            foreach (TipoUsuarioDTO temp in result)
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
    }
}
