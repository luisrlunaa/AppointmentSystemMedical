using AppointmentSystemMedical.CapaDatos;
using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaLogica
{
    public class HistoriaClinica
    {
        HistoriaClinicaDAL historiaClinicaDAL = new HistoriaClinicaDAL();
        TurnoDAL turnoDAL = new TurnoDAL();
        public HistoriaClinicaDTO BuscarTurno(TurnoDTO tur)
        {
            var (result, message) = historiaClinicaDAL.Buscar(tur);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            return result;
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp, PacienteDTO pac)
        {
            grd.Rows.Clear();
            var (result1, message1) = turnoDAL.BuscarDni(emp, pac.Persona.Dni);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            var turnos = result1;

            var (result, message) = historiaClinicaDAL.Buscar(emp, pac);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            var hcs = result;

            foreach (TurnoDTO t in turnos)
            {
                grd.Rows.Add(
                    (hcs.Exists(el => el.Turno.Id == t.Id)) ? AppointmentSystemMedical.Properties.Resources.feather_eye : null,
                    t.Id,
                    t.Medico.Empleado.Persona.Apellidos + " " + t.Medico.Empleado.Persona.Nombres,
                    t.Paciente.Persona.Apellidos + " " + t.Paciente.Persona.Nombres,
                    t.FechaHora.ToShortDateString(),
                    t.FechaHora.ToShortTimeString(),
                    (t.Cobertura.Id == 10) ? "Ninguna" : t.Cobertura.ObraSocial.Nombre + " - " + t.Cobertura.Descripcion,
                    (hcs.Exists(el => el.Turno.Id == t.Id)) ? 1 : 0);
            }
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp, PacienteDTO pac, DateTime desde, DateTime hasta)
        {
            grd.Rows.Clear();
            var (result, message) = turnoDAL.BuscarDni(emp, pac.Persona.Dni, desde, hasta);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            var turnos = result;

            var (result1, message1) = historiaClinicaDAL.Buscar(emp, pac);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            var hcs = result1;

            foreach (TurnoDTO t in turnos)
            {
                grd.Rows.Add(
                    (hcs.Exists(el => el.Turno.Id == t.Id)) ? AppointmentSystemMedical.Properties.Resources.feather_eye : null,
                    t.Id,
                    t.Medico.Empleado.Persona.Apellidos + " " + t.Medico.Empleado.Persona.Nombres,
                    t.Paciente.Persona.Apellidos + " " + t.Paciente.Persona.Nombres,
                    t.FechaHora.ToShortDateString(),
                    t.FechaHora.ToShortTimeString(),
                    (t.Cobertura.Id == 10) ? "Ninguna" : t.Cobertura.ObraSocial.Nombre + " - " + t.Cobertura.Descripcion,
                    (hcs.Exists(el => el.Turno.Id == t.Id)) ? 1 : 0);
            }
        }

        public void Guardar(TurnoDTO tur, string desc, string arc)
        {
            HistoriaClinicaDTO hc = new HistoriaClinicaDTO(tur, desc, arc);
            var (save, message) = historiaClinicaDAL.Guardar(hc);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "La Historia Clinica fue guardada correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al guardar La Historia Clinica.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void Editar(int id, TurnoDTO tur, string desc, string arc)
        {
            HistoriaClinicaDTO hc = new HistoriaClinicaDTO(id, tur, desc, arc);
            var (save, message) = historiaClinicaDAL.Editar(hc);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "La Historia Clinica fue modificada correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al modificar la Historia Clinica.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
