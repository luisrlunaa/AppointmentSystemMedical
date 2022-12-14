using AppointmentSystemMedical.CapaDatos;
using AppointmentSystemMedical.Model.DTOs;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaLogica
{
    public class TurnoEstado
    {
        TurnoEstadoDAL turnoEstadoDAL = new TurnoEstadoDAL();
        public void CargarComboBox(ComboBox cb)
        {
            int ancho = 0;
            int maximo = 0;
            var (list, message) = turnoEstadoDAL.Buscar();
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            foreach (TurnoEstadoDTO temp in list)
            {
                // calculo en ancho mas largo de texto
                ancho = TextRenderer.MeasureText(temp.Descripcion, cb.Font).Width;
                if (maximo < ancho)
                {
                    maximo = ancho;
                }
                cb.Items.Add(new KeyValuePair<int, string>(temp.Id, temp.Descripcion));
                cb.ValueMember = "Key";
                cb.DisplayMember = "Value";
            }
            cb.DropDownWidth = maximo;
        }
    }
}
