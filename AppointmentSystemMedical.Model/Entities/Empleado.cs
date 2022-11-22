namespace AppointmentSystemMedical.Model.Entities
{
    using System.Collections.Generic;

    public partial class Empleado
    {
        public Empleado()
        {
            this.Medico = new HashSet<Medico>();
        }

        public int Id { get; set; }
        public int PersonaId { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public string Usuario { get; set; }
        public string Contraseña { get; set; }
        public int TipoUsuarioId { get; set; }
        public bool Activo { get; set; }
        public string Cuil { get; set; }

        public virtual Persona Persona { get; set; }
        public virtual TipoUsuario TipoUsuario { get; set; }
        public virtual ICollection<Medico> Medico { get; set; }
    }
}
