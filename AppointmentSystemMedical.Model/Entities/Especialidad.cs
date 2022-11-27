namespace AppointmentSystemMedical.Model.Entities
{
    using System.Collections.Generic;

    public partial class Especialidad
    {
        public Especialidad()
        {
            this.Medico = new HashSet<Medico>();
        }

        public int EspecialidadId { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Medico> Medico { get; set; }
    }
}
