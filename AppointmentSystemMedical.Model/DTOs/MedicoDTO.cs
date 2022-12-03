namespace AppointmentSystemMedical.Model.DTOs
{
    public class MedicoDTO
    {
        public int Id;
        public EmpleadoDTO Empleado;
        public string Matricula;
        public EspecialidadDTO Especialidad;

        public MedicoDTO() { }
        public MedicoDTO(int _id, EmpleadoDTO _empleado, string _matricula,
            EspecialidadDTO _especialidad)
        {
            Id = _id;
            Empleado = _empleado;
            Matricula = _matricula;
            Especialidad = _especialidad;
        }

        public MedicoDTO(EmpleadoDTO _empleado, string _matricula, EspecialidadDTO _especialidad)
        {
            Empleado = _empleado;
            Matricula = _matricula;
            Especialidad = _especialidad;
        }
    }
}
