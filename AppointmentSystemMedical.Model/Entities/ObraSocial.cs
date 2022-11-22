namespace AppointmentSystemMedical.Model.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class ObraSocial
    {
        public ObraSocial()
        {
            this.Cobertura = new HashSet<Cobertura>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public Nullable<bool> Estado { get; set; }

        public virtual ICollection<Cobertura> Cobertura { get; set; }
    }
}
