namespace AppointmentSystemMedical.Model.Entities
{
    using System.Collections.Generic;

    public partial class Turno
    {
        public Turno()
        {
            this.HistoriaClinica = new HashSet<HistoriaClinica>();
        }

        public int Id { get; set; }
        public int MedicoId { get; set; }
        public int PacienteId { get; set; }
        public System.DateTime FechaHora { get; set; }
        public int CoberturaId { get; set; }
        public int EstadoId { get; set; }

        public virtual Cobertura Cobertura { get; set; }
        public virtual ICollection<HistoriaClinica> HistoriaClinica { get; set; }
        public virtual Medico Medico { get; set; }
        public virtual Paciente Paciente { get; set; }
        public virtual TurnoEstado TurnoEstado { get; set; }
    }
}
