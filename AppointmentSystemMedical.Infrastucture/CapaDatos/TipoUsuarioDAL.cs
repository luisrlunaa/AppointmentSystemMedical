using AppointmentSystemMedical.Model.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class TipoUsuarioDAL
    {
        public static List<TipoUsuarioDTO> Buscar()
        {
            using (AppointmentSystemMedicalEntities db = new AppointmentSystemMedicalEntities())
            {
                List<TipoUsuarioDTO> res = new List<TipoUsuarioDTO>();
                var query = db.TipoUsuario
                    .OrderBy(el => el.Descripcion);
                foreach (TipoUsuario temp in query)
                {
                    res.Add(new TipoUsuarioDTO(
                        temp.Id,
                        temp.Descripcion));
                }
                return res;
            }
        }

        public static TipoUsuarioDTO Buscar(int id)
        {
            using (AppointmentSystemMedicalEntities db = new AppointmentSystemMedicalEntities())
            {
                var query = db.TipoUsuario
                    .Where(el => el.Id == id)
                    .First();
                return new TipoUsuarioDTO(
                    query.Id,
                    query.Descripcion);
            }
        }
    }
}
