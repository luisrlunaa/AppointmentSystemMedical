﻿namespace AppointmentSystemMedical.Model.DTOs
{
    public class EspecialidadDTO
    {
        public int Id;
        public string Descripcion;

        public EspecialidadDTO() { }
        public EspecialidadDTO(int _id, string _descripcion)
        {
            Id = _id;
            Descripcion = _descripcion;
        }

        public EspecialidadDTO(string _descripcion)
        {
            Descripcion = _descripcion;
        }
    }
}
