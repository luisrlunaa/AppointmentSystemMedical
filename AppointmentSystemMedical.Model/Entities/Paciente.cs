namespace AppointmentSystemMedical.Model.Entities
{
    using System.Collections.Generic;

    public partial class Paciente
    {
        public Paciente()
        {
            this.Turno = new HashSet<Turno>();
        }

        public int PacienteId { get; set; }
        public int PersonaId { get; set; }

        public virtual Persona Persona { get; set; }
        public virtual ICollection<Turno> Turno { get; set; }
    }
}
