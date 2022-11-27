namespace AppointmentSystemMedical.Model.Entities
{
    using System.Collections.Generic;

    public partial class Medico
    {
        public Medico()
        {
            this.Turno = new HashSet<Turno>();
        }

        public int MedicoId { get; set; }
        public int EmpleadoId { get; set; }
        public string Matricula { get; set; }
        public int EspecialidadId { get; set; }

        public virtual Empleado Empleado { get; set; }
        public virtual Especialidad Especialidad { get; set; }
        public virtual ICollection<Turno> Turno { get; set; }
    }
}
