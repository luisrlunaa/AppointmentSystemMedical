namespace AppointmentSystemMedical.Model.DTOs
{
    public class PacienteDTO
    {
        public int Id;
        public PersonaDTO Persona;

        public PacienteDTO() { }
        public PacienteDTO(int _id, PersonaDTO _persona)
        {
            Id = _id;
            Persona = _persona;
        }

        public PacienteDTO(PersonaDTO _persona)
        {
            Persona = _persona;
        }
    }
}
