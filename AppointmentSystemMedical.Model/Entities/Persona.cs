namespace AppointmentSystemMedical.Model.Entities
{
    using System.Collections.Generic;

    public partial class Persona
    {
        public Persona()
        {
            this.Empleado = new HashSet<Empleado>();
            this.Paciente = new HashSet<Paciente>();
        }

        public int Id { get; set; }
        public string Dni { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
        public virtual ICollection<Paciente> Paciente { get; set; }
    }
}
