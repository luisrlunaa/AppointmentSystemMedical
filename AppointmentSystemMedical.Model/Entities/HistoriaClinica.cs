namespace AppointmentSystemMedical.Model.Entities
{

    public partial class HistoriaClinica
    {
        public int HistoriaClinicaId { get; set; }
        public int TurnoId { get; set; }
        public string Archivo { get; set; }
        public string Descripcion { get; set; }

        public virtual Turno Turno { get; set; }
    }
}
