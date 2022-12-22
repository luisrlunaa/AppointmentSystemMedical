using AppointmentSystemMedical.CapaDatos;
using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaLogica
{
    public class Medico
    {
        MedicoDAL medicoDAL = new MedicoDAL();
        TipoUsuarioDAL tipoUsuarioDAL = new TipoUsuarioDAL();
        EspecialidadDAL especialidadDAL = new EspecialidadDAL();

        public MedicoDTO Buscar(int id)
        {
            var (result, message) = medicoDAL.Buscar(id);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            return result;
        }

        public MedicoDTO BuscarDni(string dni)
        {
            var (result, message) = medicoDAL.BuscarDni(dni);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            return result.FirstOrDefault();
        }

        public void CargarDataGridBusqueda(DataGridView grd, int dni)
        {
            grd.Rows.Clear();
            var (result, message) = medicoDAL.BuscarDni(dni.ToString());
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var medicos = result;
            foreach (MedicoDTO med in medicos)
            {
                if (med.Empleado.Activo)
                {
                    grd.Rows.Add(
                        med.Id,
                        med.Empleado.Persona.Dni,
                        med.Empleado.Persona.Apellidos,
                        med.Empleado.Persona.Nombres,
                        med.Especialidad.Descripcion,
                        med.Matricula);
                }
            }
        }

        public void CargarDataGridBusqueda(DataGridView grd, string apenom)
        {
            grd.Rows.Clear();
            var (result, message) = medicoDAL.BuscarApeNom(apenom);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var medicos = result;
            foreach (MedicoDTO med in medicos)
            {
                if (med.Empleado.Activo)
                {
                    grd.Rows.Add(
                        med.Id,
                        med.Empleado.Persona.Dni,
                        med.Empleado.Persona.Apellidos,
                        med.Empleado.Persona.Nombres,
                        med.Especialidad.Descripcion,
                        med.Matricula);
                }
            }
        }

        public void CargarDataGrid(DataGridView grd)
        {
            grd.Rows.Clear();
            var (result, message) = medicoDAL.Buscar();
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var medicos = result;
            foreach (MedicoDTO med in medicos)
            {
                grd.Rows.Add(
                    null,
                    med.Id,
                    med.Empleado.Id,
                    med.Empleado.Persona.Id,
                    med.Empleado.Persona.Apellidos,
                    med.Empleado.Persona.Nombres,
                    med.Empleado.Persona.Dni,
                    med.Empleado.Cuil,
                    med.Matricula,
                    med.Especialidad.Descripcion,
                    CalcularAños(med.Empleado.Persona.FechaNacimiento),
                    med.Empleado.Persona.CorreoElectronico,
                    med.Empleado.Persona.Telefono,
                    (med.Empleado.Persona.Sexo == "M") ? "Masculino" : "Femenino",
                    med.Empleado.Activo);
            }
        }

        public void CargarDataGrid(DataGridView grd, int dni)
        {
            grd.Rows.Clear();
            var (result, message) = medicoDAL.BuscarDni(dni.ToString());
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var medicos = result;
            foreach (MedicoDTO med in medicos)
            {
                grd.Rows.Add(
                    null,
                    med.Id,
                    med.Empleado.Id,
                    med.Empleado.Persona.Id,
                    med.Empleado.Persona.Apellidos,
                    med.Empleado.Persona.Nombres,
                    med.Empleado.Persona.Dni,
                    med.Empleado.Cuil,
                    med.Matricula,
                    med.Especialidad.Descripcion,
                    CalcularAños(med.Empleado.Persona.FechaNacimiento),
                    med.Empleado.Persona.CorreoElectronico,
                    med.Empleado.Persona.Telefono,
                    (med.Empleado.Persona.Sexo == "M") ? "Masculino" : "Femenino",
                    med.Empleado.Activo);
            }
        }

        public void CargarDataGrid(DataGridView grd, string apenom)
        {
            grd.Rows.Clear();
            var (result, message) = medicoDAL.BuscarApeNom(apenom);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var medicos = result;
            foreach (MedicoDTO med in medicos)
            {
                grd.Rows.Add(
                    null,
                    med.Id,
                    med.Empleado.Id,
                    med.Empleado.Persona.Id,
                    med.Empleado.Persona.Apellidos,
                    med.Empleado.Persona.Nombres,
                    med.Empleado.Persona.Dni,
                    med.Empleado.Cuil,
                    med.Matricula,
                    med.Especialidad.Descripcion,
                    CalcularAños(med.Empleado.Persona.FechaNacimiento),
                    med.Empleado.Persona.CorreoElectronico,
                    med.Empleado.Persona.Telefono,
                    (med.Empleado.Persona.Sexo == "M") ? "Masculino" : "Femenino",
                    med.Empleado.Activo);
            }
        }

        public void Guardar(string dni, string ape, string nom, DateTime fn, string sexo,
            string correo, string tel, string cuil, string usuario, string contra, DateTime fi,
            bool activo, string matricula, int especialidad)
        {

            var (result, message1) = tipoUsuarioDAL.Buscar(3);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var (result1, message2) = especialidadDAL.Buscar(especialidad);
            if (message2.Contains("Error"))
                MessageBox.Show(message2,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            PersonaDTO per = new PersonaDTO(dni, ape, nom, fn, sexo, correo, tel);
            EmpleadoDTO emp = new EmpleadoDTO(per, cuil, usuario, contra, fi, result, activo);
            MedicoDTO med = new MedicoDTO(emp, matricula, result1);

            var (save, message) = medicoDAL.Guardar(med);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "El Medico fue guardado correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al guardar el Medico.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void Editar(int id, string dni, string ape, string nom, DateTime fn, string sexo,
            string correo, string tel, string cuil, string usuario, string contra, DateTime fi,
            bool activo, string matricula, int especialidad)
        {
            var (result, message1) = medicoDAL.BuscarDni(dni);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var (result1, message2) = especialidadDAL.Buscar(especialidad);
            if (message2.Contains("Error"))
                MessageBox.Show(message2,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var med = result.FirstOrDefault();
            med.Empleado.Persona.Apellidos = ape;
            med.Empleado.Persona.Nombres = nom;
            med.Empleado.Persona.Sexo = sexo;
            med.Empleado.Persona.CorreoElectronico = correo;
            med.Empleado.Persona.Telefono = tel;
            med.Empleado.Usuario = usuario;
            med.Empleado.Contraseña = contra;
            med.Empleado.FechaIngreso = fi;
            med.Empleado.Activo = activo;
            med.Matricula = matricula;
            med.Especialidad = result1;

            var (save, message) = medicoDAL.Editar(med);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "El Medico fue modificado correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al modificar el Medico.",
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
