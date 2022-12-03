using AppointmentSystemMedical.CapaDatos;
using AppointmentSystemMedical.Model.DTOs;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaLogica
{
    public class Cobertura
    {
        CoberturaDAL coberturaDAL = new CoberturaDAL();
        ObraSocialDAL obraSocialDAL = new ObraSocialDAL();
        public CoberturaDTO Buscar(int id)
        {
            var (c, message) = coberturaDAL.Buscar(id);
            if (message.Contains("Error"))
                return new CoberturaDTO();

            return c;
        }

        public void CargarDataGrid(DataGridView grd)
        {
            grd.Rows.Clear();
            var (coberturas, message) = coberturaDAL.Buscar();
            foreach (CoberturaDTO temp in coberturas)
            {
                if (temp.Id != 10)
                {
                    grd.Rows.Add(
                        (temp.Id == 1) ? null : AppointmentSystemMedical.Properties.Resources.feather_edit,
                        temp.Id,
                        temp.ObraSocial.Nombre,
                        temp.Descripcion);
                }
            }
        }

        public void CargarDataGrid(DataGridView grd, string apenom)
        {
            grd.Rows.Clear();
            var (coberturas, message) = coberturaDAL.Buscar(apenom);
            foreach (CoberturaDTO temp in coberturas)
            {
                if (temp.Id != 10)
                {
                    grd.Rows.Add(
                        (temp.Id == 1) ? null : AppointmentSystemMedical.Properties.Resources.feather_edit,
                        temp.Id,
                        temp.ObraSocial.Nombre,
                        temp.Descripcion);
                }
            }
        }

        public void CargarDataGridBusqueda(DataGridView grd, string apenom)
        {
            grd.Rows.Clear();
            var (coberturas, message) = coberturaDAL.Buscar(apenom);
            foreach (CoberturaDTO temp in coberturas)
            {
                if (temp.Id != 10 && temp.Estado)
                {
                    grd.Rows.Add(
                        temp.Id,
                        temp.Descripcion,
                        temp.ObraSocial.Nombre);
                }
            }
        }

        public void Guardar(int os, string nom, bool estado)
        {
            var (ob, message) = obraSocialDAL.Buscar(os);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var nuevo = new CoberturaDTO(ob, nom, estado);
            var (save, message1) = coberturaDAL.Guardar(nuevo);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            if (save)
            {
                MessageBox.Show(
                    "La Cobertura fue guardada correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al guardar la Cobertura.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void Editar(int id, int os, string nom, bool estado)
        {
            var (ob, message) = obraSocialDAL.Buscar(os);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            CoberturaDTO modificado = new CoberturaDTO(id, ob, nom, estado);

            var (save, message1) = coberturaDAL.Editar(modificado);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            if (save)
            {
                MessageBox.Show(
                    "La Cobertura fue modificada correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al modificar la Cobertura.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
