using AppointmentSystemMedical.CapaDatos;
using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaLogica
{
    public class Turno
    {
        TurnoDAL turnoDAL = new TurnoDAL();
        CoberturaDAL coberturaDAL = new CoberturaDAL();
        TurnoEstadoDAL turnoEstadoDAL = new TurnoEstadoDAL();
        public TurnoDTO Buscar(int id)
        {
            var (result, message) = turnoDAL.Buscar(id);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            return result;
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp)
        {
            grd.Rows.Clear();
            List<TurnoDTO> turnos = new List<TurnoDTO>();
            if (emp.TipoUsuario.Id == 3)
            {
                var (result, message) = turnoDAL.Buscar(emp);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                turnos = result;
            }
            else
            {
                var (result, message) = turnoDAL.Buscar();
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }
            foreach (TurnoDTO turno in turnos)
            {
                if (emp.TipoUsuario.Id <= 2 || DateTime.Compare(turno.FechaHora, DateTime.Now) > 0)
                {
                    grd.Rows.Add(
                        null,
                        turno.Id,
                        turno.Medico.Empleado.Persona.Apellidos + " " + turno.Medico.Empleado.Persona.Nombres,
                        turno.Paciente.Persona.Apellidos + " " + turno.Paciente.Persona.Nombres,
                        turno.FechaHora.ToShortDateString(),
                        turno.FechaHora.ToShortTimeString(),
                        turno.Estado.Descripcion,
                        (turno.Cobertura.Id == 10) ? "Ninguna" : turno.Cobertura.ObraSocial.Nombre + " - " + turno.Cobertura.Descripcion);
                }

            }
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp, string apenom)
        {
            grd.Rows.Clear();
            List<TurnoDTO> turnos = new List<TurnoDTO>();
            if (emp.TipoUsuario.Id == 3)
            {
                var (result, message) = turnoDAL.BuscarApeNom(emp, apenom);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }
            else
            {
                var (result, message) = turnoDAL.BuscarApeNom(apenom);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }

            foreach (TurnoDTO turno in turnos)
            {
                if (emp.TipoUsuario.Id <= 2 || DateTime.Compare(turno.FechaHora, DateTime.Now) > 0)
                {
                    grd.Rows.Add(
                        null,
                        turno.Id,
                        turno.Medico.Empleado.Persona.Apellidos + " " + turno.Medico.Empleado.Persona.Nombres,
                        turno.Paciente.Persona.Apellidos + " " + turno.Paciente.Persona.Nombres,
                        turno.FechaHora.ToShortDateString(),
                        turno.FechaHora.ToShortTimeString(),
                        turno.Estado.Descripcion,
                        (turno.Cobertura.Id == 10) ? "Ninguna" : turno.Cobertura.ObraSocial.Nombre + " - " + turno.Cobertura.Descripcion);
                }
            }
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp, int dni)
        {
            grd.Rows.Clear();
            List<TurnoDTO> turnos = new List<TurnoDTO>();
            if (emp.TipoUsuario.Id == 3)
            {
                var (result, message) = turnoDAL.BuscarDni(emp, dni.ToString());
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }
            else
            {
                var (result, message) = turnoDAL.BuscarDni(dni.ToString());
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }

            foreach (TurnoDTO turno in turnos)
            {
                if (emp.TipoUsuario.Id <= 2 || DateTime.Compare(turno.FechaHora, DateTime.Now) > 0)
                {
                    grd.Rows.Add(
                        null,
                        turno.Id,
                        turno.Medico.Empleado.Persona.Apellidos + " " + turno.Medico.Empleado.Persona.Nombres,
                        turno.Paciente.Persona.Apellidos + " " + turno.Paciente.Persona.Nombres,
                        turno.FechaHora.ToShortDateString(),
                        turno.FechaHora.ToShortTimeString(),
                        turno.Estado.Descripcion,
                        (turno.Cobertura.Id == 10) ? "Ninguna" : turno.Cobertura.ObraSocial.Nombre + " - " + turno.Cobertura.Descripcion);
                }
            }
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp, DateTime desde, DateTime hasta)
        {
            grd.Rows.Clear();
            List<TurnoDTO> turnos = new List<TurnoDTO>();
            if (emp.TipoUsuario.Id == 3)
            {
                var (result, message) = turnoDAL.Buscar(emp, desde, hasta);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }
            else
            {
                var (result, message) = turnoDAL.Buscar(desde, hasta);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }
            foreach (TurnoDTO turno in turnos)
            {
                grd.Rows.Add(
                    null,
                    turno.Id,
                    turno.Medico.Empleado.Persona.Apellidos + " " + turno.Medico.Empleado.Persona.Nombres,
                    turno.Paciente.Persona.Apellidos + " " + turno.Paciente.Persona.Nombres,
                    turno.FechaHora.ToShortDateString(),
                    turno.FechaHora.ToShortTimeString(),
                    turno.Estado.Descripcion,
                    (turno.Cobertura.Id == 10) ? "Ninguna" : turno.Cobertura.ObraSocial.Nombre + " - " + turno.Cobertura.Descripcion);

            }
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp, string apenom, DateTime desde, DateTime hasta)
        {
            grd.Rows.Clear();
            List<TurnoDTO> turnos = new List<TurnoDTO>();
            if (emp.TipoUsuario.Id == 3)
            {
                var (result, message) = turnoDAL.BuscarApeNom(emp, apenom, desde, hasta);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }
            else
            {
                var (result, message) = turnoDAL.BuscarApeNom(apenom, desde, hasta);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }

            foreach (TurnoDTO turno in turnos)
            {
                grd.Rows.Add(
                    null,
                    turno.Id,
                    turno.Medico.Empleado.Persona.Apellidos + " " + turno.Medico.Empleado.Persona.Nombres,
                    turno.Paciente.Persona.Apellidos + " " + turno.Paciente.Persona.Nombres,
                    turno.FechaHora.ToShortDateString(),
                    turno.FechaHora.ToShortTimeString(),
                    turno.Estado.Descripcion,
                    (turno.Cobertura.Id == 10) ? "Ninguna" : turno.Cobertura.ObraSocial.Nombre + " - " + turno.Cobertura.Descripcion);
            }
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp, int dni, DateTime desde, DateTime hasta)
        {
            grd.Rows.Clear();
            List<TurnoDTO> turnos = new List<TurnoDTO>();
            if (emp.TipoUsuario.Id == 3)
            {
                var (result, message) = turnoDAL.BuscarDni(emp, dni.ToString(), desde, hasta);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }
            else
            {
                var (result, message) = turnoDAL.BuscarDni(dni.ToString(), desde, hasta);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                turnos = result;
            }

            foreach (TurnoDTO turno in turnos)
            {
                grd.Rows.Add(
                    null,
                    turno.Id,
                    turno.Medico.Empleado.Persona.Apellidos + " " + turno.Medico.Empleado.Persona.Nombres,
                    turno.Paciente.Persona.Apellidos + " " + turno.Paciente.Persona.Nombres,
                    turno.FechaHora.ToShortDateString(),
                    turno.FechaHora.ToShortTimeString(),
                    turno.Estado.Descripcion,
                    (turno.Cobertura.Id == 10) ? "Ninguna" : turno.Cobertura.ObraSocial.Nombre + " - " + turno.Cobertura.Descripcion);
            }
        }

        public void CargarAgenda(DataGridView grd, MedicoDTO med, DateTime dia)
        {
            grd.Rows.Clear();
            // agrego los horarios
            for (int x = 0; x < 48; x++)
            {
                int hh = (x % 2 == 0) ? x / 2 : (x - 1) / 2;
                int mm = (x % 2 == 0) ? 0 : 30;
                grd.Rows.Add(PonerDosNumeros(hh) + " : " + PonerDosNumeros(mm));
            }
            // empiezo 2 dias antes
            dia = dia.AddDays(-2);
            for (int y = 1; y < 6; y++)
            {
                grd.Columns[y].HeaderText = DiaDeLaSemana(dia) + " " + PonerDosNumeros(dia.Day);
                var (result, message) = turnoDAL.Buscar(med, dia);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                var turnos = result;
                for (int x = 0; x < 48; x++)
                {
                    int hh = (x % 2 == 0) ? x / 2 : (x - 1) / 2;
                    int mm = (x % 2 == 0) ? 0 : 30;
                    string marca = "Libre";
                    DateTime aux = new DateTime(dia.Year, dia.Month, dia.Day, hh, mm, 0);
                    if (DateTime.Compare(aux, DateTime.Now) <= 0)
                    {
                        marca = "Pasado";
                    }
                    else if (turnos.Exists(el => el.FechaHora.Hour == hh && el.FechaHora.Minute == mm && (el.Estado.Descripcion == "Reservado" || el.Estado.Descripcion == "En Espera")))
                    {
                        marca = "Ocupado";
                    }
                    grd.Rows[x].Cells[y].Value = marca;
                }
                dia = dia.AddDays(1);
            }
            ColorearAgenda(grd);
        }

        public int HoraAPeriodo(DateTime t)
        {
            return t.Hour * 2 + ((t.Minute == 30) ? 1 : 0);
        }

        public DateTime PerdiodoAHora(int p)
        {
            return new DateTime(1995, 01, 07, (p % 2 == 0) ? p / 2 : (p - 1) / 2, (p % 2 == 0) ? 0 : 30, 0);
        }

        private string DiaDeLaSemana(DateTime d)
        {
            switch (d.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "Domingo";
                case DayOfWeek.Monday:
                    return "Lunes";
                case DayOfWeek.Tuesday:
                    return "Martes";
                case DayOfWeek.Wednesday:
                    return "Miercoles";
                case DayOfWeek.Thursday:
                    return "Jueves";
                case DayOfWeek.Friday:
                    return "Viernes";
                case DayOfWeek.Saturday:
                    return "Sabado";
                default:
                    return "";
            }
        }

        private void ColorearAgenda(DataGridView grd)
        {
            for (int c = 1; c < grd.Columns.Count; c++)
            {
                for (int r = 0; r < grd.Rows.Count; r++)
                {
                    if (grd.Rows[r].Cells[c].Value.ToString() == "Libre")
                    {
                        // asi se camufla la letra
                        grd.Rows[r].Cells[c].Style.BackColor = Color.FromArgb(0, 200, 83);
                        grd.Rows[r].Cells[c].Style.ForeColor = Color.FromArgb(0, 200, 83);
                    }
                    else if (grd.Rows[r].Cells[c].Value.ToString() == "Ocupado")
                    {
                        grd.Rows[r].Cells[c].Style.BackColor = Color.FromArgb(213, 0, 0);
                        grd.Rows[r].Cells[c].Style.ForeColor = Color.FromArgb(213, 0, 0);
                    }
                    else
                    {
                        grd.Rows[r].Cells[c].Style.BackColor = Color.FromArgb(30, 30, 30);
                        grd.Rows[r].Cells[c].Style.ForeColor = Color.FromArgb(30, 30, 30);
                    }
                }
            }
        }

        private string PonerDosNumeros(int n)
        {
            if (n < 10)
            {
                return "0" + n.ToString();
            }

            return n.ToString();
        }

        public void Guardar(MedicoDTO med, PacienteDTO pac, DateTime fh, int cob)
        {
            var (result1, message1) = turnoEstadoDAL.Buscar(1);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var (result2, message2) = coberturaDAL.Buscar(cob);
            if (message2.Contains("Error"))
                MessageBox.Show(message2,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            TurnoDTO nuevo = new TurnoDTO(med, pac, fh, result2, result1);
            var (save, message) = turnoDAL.Guardar(nuevo);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "El turno fue guardado correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al guardar el turno.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void Editar(int id, MedicoDTO med, PacienteDTO pac, DateTime fh, int cob, int est)
        {
            var (result1, message1) = turnoEstadoDAL.Buscar(est);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var (result2, message2) = coberturaDAL.Buscar(cob);
            if (message2.Contains("Error"))
                MessageBox.Show(message2,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            TurnoDTO nuevo = new TurnoDTO(id, med, pac, fh, result2, result1);
            var (save, message) = turnoDAL.Editar(nuevo);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "El turno fue modificado correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al modificar el turno.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
