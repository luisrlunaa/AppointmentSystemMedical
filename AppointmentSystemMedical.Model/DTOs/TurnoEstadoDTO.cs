namespace AppointmentSystemMedical.Model.DTOs
{
    public class TurnoEstadoDTO
    {
        public int Id;
        public string Descripcion;

        public TurnoEstadoDTO() { }
        public TurnoEstadoDTO(int _id, string _descripcion)
        {
            Id = _id;
            Descripcion = _descripcion;
        }

        public TurnoEstadoDTO(string _descripcion)
        {
            Descripcion = _descripcion;
        }
    }
}
