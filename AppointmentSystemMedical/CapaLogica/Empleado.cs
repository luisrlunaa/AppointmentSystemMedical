using AppointmentSystemMedical.CapaDatos;
using AppointmentSystemMedical.Model.DTOs;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AppointmentSystemMedical.CapaLogica
{
    public class Empleado
    {
        EmpleadoDAL empleadoDAL = new EmpleadoDAL();
        TipoUsuarioDAL tipoUsuarioDAL = new TipoUsuarioDAL();
        PersonaDAL personaDAL = new PersonaDAL();
        public EmpleadoDTO BuscarDni(string dni)
        {
            var (result, message) = empleadoDAL.BuscarDni(dni);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            return result.FirstOrDefault();
        }

        public EmpleadoDTO Ingresar(string usuario, string contraseña)
        {
            var (result, message) = empleadoDAL.BuscarUsuario(usuario);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            // fix cifrar
            return (result != null && result.Activo && result.Contraseña.Equals(contraseña)) ? result : null;
        }

        public void CargarDataGrid(DataGridView grd)
        {
            grd.Rows.Clear();
            var (result, message) = empleadoDAL.Buscar();
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var empleados = result;
            foreach (EmpleadoDTO emp in empleados)
            {
                grd.Rows.Add(
                    (emp.TipoUsuario.Id == 0) ? null : AppointmentSystemMedical.Properties.Resources.feather_edit,
                    emp.Id,
                    emp.Persona.Id,
                    emp.Persona.Apellidos,
                    emp.Persona.Nombres,
                    emp.Persona.Dni,
                    emp.Cuil,
                    CalcularAños(emp.Persona.FechaNacimiento),
                    CalcularAños(emp.FechaIngreso),
                    emp.TipoUsuario.Descripcion,
                    emp.Usuario,
                    emp.Persona.CorreoElectronico,
                    emp.Persona.Telefono,
                    (emp.Persona.Sexo == "M") ? "Masculino" : "Femenino",
                    emp.Activo);
            }
        }

        public void CargarDataGrid(DataGridView grd, int dni)
        {
            grd.Rows.Clear();
            var (result, message) = empleadoDAL.BuscarDni(dni.ToString());
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var empleados = result;
            foreach (EmpleadoDTO emp in empleados)
            {
                grd.Rows.Add(
                    (emp.TipoUsuario.Id == 0) ? null : AppointmentSystemMedical.Properties.Resources.feather_edit,
                    emp.Id,
                    emp.Persona.Id,
                    emp.Persona.Apellidos,
                    emp.Persona.Nombres,
                    emp.Persona.Dni,
                    emp.Cuil,
                    CalcularAños(emp.Persona.FechaNacimiento),
                    CalcularAños(emp.FechaIngreso),
                    emp.TipoUsuario.Descripcion,
                    emp.Usuario,
                    emp.Persona.CorreoElectronico,
                    emp.Persona.Telefono,
                    (emp.Persona.Sexo == "M") ? "Masculino" : "Femenino",
                    emp.Activo);
            }
        }

        public void CargarDataGrid(DataGridView grd, string apenom)
        {
            grd.Rows.Clear();
            var (result, message) = empleadoDAL.BuscarApeNom(apenom);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var empleados = result;
            foreach (EmpleadoDTO emp in empleados)
            {
                grd.Rows.Add(
                    (emp.TipoUsuario.Id == 0) ? null : AppointmentSystemMedical.Properties.Resources.feather_edit,
                    emp.Id,
                    emp.Persona.Id,
                    emp.Persona.Apellidos,
                    emp.Persona.Nombres,
                    emp.Persona.Dni,
                    emp.Cuil,
                    CalcularAños(emp.Persona.FechaNacimiento),
                    CalcularAños(emp.FechaIngreso),
                    emp.TipoUsuario.Descripcion,
                    emp.Usuario,
                    emp.Persona.CorreoElectronico,
                    emp.Persona.Telefono,
                    (emp.Persona.Sexo == "M") ? "Masculino" : "Femenino",
                    emp.Activo);
            }
        }

        public void Guardar(string dni, string ape, string nom, DateTime fn, string sexo,
            string correo, string tel, string cuil, string usuario, string contra, DateTime fi,
            int tipo, bool activo)
        {
            var (tipoUser, message) = tipoUsuarioDAL.Buscar(tipo);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            PersonaDTO per = new PersonaDTO(dni, ape, nom, fn, sexo, correo, tel);
            EmpleadoDTO emp = new EmpleadoDTO(per, cuil, usuario, contra, fi, tipoUser, activo);

            var (save, message1) = empleadoDAL.Guardar(emp);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            if (save)
            {
                MessageBox.Show(
                    "El Empleado fue guardado correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al guardar el Empleado.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        public void Editar(int id, string dni, string ape, string nom, DateTime fn,
            string sexo, string correo, string tel, string cuil, string usuario, string contra,
            DateTime fi, int tipo, bool activo)
        {
            var (tipoUser, message) = tipoUsuarioDAL.Buscar(tipo);
            if (message.Contains("Error"))
                MessageBox.Show(message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var (result, message1) = personaDAL.BuscarDni(dni);
            if (message1.Contains("Error"))
                MessageBox.Show(message1,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

            var per = result.FirstOrDefault();

            per.Apellidos = ape;
            per.Nombres = nom;
            per.Sexo = sexo;
            per.CorreoElectronico = correo;
            per.Telefono = tel;
            EmpleadoDTO modificado = new EmpleadoDTO(id, per, cuil, usuario, contra, fi, tipoUser, activo);

            var (save, message2) = empleadoDAL.Guardar(modificado);
            if (message2.Contains("Error"))
                MessageBox.Show(message2);

            if (save)
            {
                MessageBox.Show(
                    "El Empleado fue modificado correctamente.",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Se produjo un error al modificar el Empleado.",
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
