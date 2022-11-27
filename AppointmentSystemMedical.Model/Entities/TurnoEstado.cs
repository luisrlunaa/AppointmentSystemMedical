namespace AppointmentSystemMedical.Model.Entities
{
    using System.Collections.Generic;

    public partial class TurnoEstado
    {
        public TurnoEstado()
        {
            this.Turno = new HashSet<Turno>();
        }

        public int TurnoEstadoId { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Turno> Turno { get; set; }
    }
}
