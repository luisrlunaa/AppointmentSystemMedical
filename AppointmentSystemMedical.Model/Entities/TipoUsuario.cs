namespace AppointmentSystemMedical.Model.Entities
{
    using System.Collections.Generic;

    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            this.Empleado = new HashSet<Empleado>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Empleado> Empleado { get; set; }
    }
}
