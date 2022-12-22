using AppointmentSystemMedical.CapaDatos;
using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaLogica
{
    public class Paciente
    {
        PacienteDAL pacienteDAL = new PacienteDAL();
        public PacienteDTO Buscar(int id)
        {
            var (result, message) = pacienteDAL.Buscar(id);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            return result;
        }

        public void CargarDataGridBusqueda(DataGridView grd, int dni)
        {
            grd.Rows.Clear();
            var (result, message) = pacienteDAL.BuscarDni(dni.ToString());
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var pacientes = result;
            foreach (PacienteDTO pac in pacientes)
            {
                grd.Rows.Add(
                    pac.Id,
                    pac.Persona.Dni,
                    pac.Persona.Apellidos,
                    pac.Persona.Nombres);
            }
        }

        public void CargarDataGridBusqueda(DataGridView grd, string apenom)
        {
            grd.Rows.Clear();
            var (result, message) = pacienteDAL.BuscarApeNom(apenom);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var pacientes = result;
            foreach (PacienteDTO pac in pacientes)
            {
                grd.Rows.Add(
                    pac.Id,
                    pac.Persona.Dni,
                    pac.Persona.Apellidos,
                    pac.Persona.Nombres);
            }
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp)
        {
            grd.Rows.Clear();
            List<PacienteDTO> pacientes = new List<PacienteDTO>();
            if (emp.TipoUsuario.Descripcion == "Medico")
            {
                var (result, message) = pacienteDAL.Buscar(emp);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                pacientes = result;
            }
            else
            {
                var (result, message) = pacienteDAL.Buscar();
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                pacientes = result;
            }

            foreach (PacienteDTO paciente in pacientes)
            {
                grd.Rows.Add(
                    null,
                    null,
                    paciente.Id,
                    paciente.Persona.Apellidos,
                    paciente.Persona.Nombres,
                    paciente.Persona.Dni,
                    CalcularAños(paciente.Persona.FechaNacimiento),
                    paciente.Persona.CorreoElectronico,
                    paciente.Persona.Telefono,
                    (paciente.Persona.Sexo == "M") ? "Masculino" : "Femenino");
            }
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp, string apenom)
        {
            grd.Rows.Clear();
            List<PacienteDTO> pacientes = new List<PacienteDTO>();
            if (emp.TipoUsuario.Descripcion == "Medico")
            {
                var (result, message) = pacienteDAL.BuscarApeNom(emp, apenom);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                pacientes = result;
            }
            else
            {
                var (result, message) = pacienteDAL.BuscarApeNom(apenom);
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                pacientes = result;
            }

            foreach (PacienteDTO paciente in pacientes)
            {
                grd.Rows.Add(
                    null,
                    null,
                    paciente.Id,
                    paciente.Persona.Apellidos,
                    paciente.Persona.Nombres,
                    paciente.Persona.Dni,
                    CalcularAños(paciente.Persona.FechaNacimiento),
                    paciente.Persona.CorreoElectronico,
                    paciente.Persona.Telefono,
                    (paciente.Persona.Sexo == "M") ? "Masculino" : "Femenino");
            }
        }

        public void CargarDataGrid(DataGridView grd, EmpleadoDTO emp, int dni)
        {
            grd.Rows.Clear();
            List<PacienteDTO> pacientes = new List<PacienteDTO>();
            if (emp.TipoUsuario.Descripcion == "Medico")
            {
                var (result, message) = pacienteDAL.BuscarDni(emp, dni.ToString());
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                pacientes = result;
            }
            else
            {
                var (result, message) = pacienteDAL.BuscarDni(dni.ToString());
                if (message.Contains("Error"))
                    MessageBox.Show(message,
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                pacientes = result;
            }

            foreach (PacienteDTO paciente in pacientes)
            {
                grd.Rows.Add(
                    null,
                    null,
                    paciente.Id,
                    paciente.Persona.Apellidos,
                    paciente.Persona.Nombres,
                    paciente.Persona.Dni,
                    CalcularAños(paciente.Persona.FechaNacimiento),
                    paciente.Persona.CorreoElectronico,
                    paciente.Persona.Telefono,
                    (paciente.Persona.Sexo == "M") ? "Masculino" : "Femenino");
            }
        }

        public void Guardar(string dni, string ape, string nom, DateTime fn,
            string sexo, string correo, string tel)
        {
            PacienteDTO nuevo = new PacienteDTO(new PersonaDTO(dni, ape, nom, fn, sexo, correo, tel));

            var (save, message) = pacienteDAL.Guardar(nuevo);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "El Paciente fue guardado correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al guardar el Paciente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void Editar(int id, string dni, string ape, string nom,
            DateTime fn, string sexo, string correo, string tel)
        {
            var (result, message1) = pacienteDAL.BuscarDni(dni);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var pac = result.FirstOrDefault();
            pac.Persona.Apellidos = ape;
            pac.Persona.Nombres = nom;
            pac.Persona.Sexo = sexo;
            pac.Persona.CorreoElectronico = correo;
            pac.Persona.Telefono = tel;

            var (save, message) = pacienteDAL.Editar(pac);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "El Paciente fue guardado correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al guardar el Paciente.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private int CalcularAños(DateTime fecha)
        {
            int aux = DateTime.Now.Year - fecha.Year;
            if (DateTime.Now.Month < fecha.Month || (DateTime.Now.Month == fecha.Month && DateTime.Now.Day < fecha.Day))
            {
                aux -= 1;
            }
            return aux;
        }
    }
}
