using AppointmentSystemMedical.Model.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class ObraSocialDAL
    {
        public static List<ObraSocialDTO> Buscar()
        {
            using (AppointmentSystemMedicalEntities db = new AppointmentSystemMedicalEntities())
            {
                List<ObraSocialDTO> res = new List<ObraSocialDTO>();
                var query = db.ObraSocial
                    .OrderBy(el => el.Nombre);
                foreach (ObraSocial temp in query)
                {
                    res.Add(new ObraSocialDTO(
                        temp.Id,
                        temp.Nombre,
                        temp.Estado));
                }
                return res;
            }
        }

        public static ObraSocialDTO Buscar(int id)
        {
            using (AppointmentSystemMedicalEntities db = new AppointmentSystemMedicalEntities())
            {
                var query = db.ObraSocial
                    .Where(el => el.Id == id)
                    .First();
                return new ObraSocialDTO(
                    query.Id,
                    query.Nombre,
                    query.Estado);
            }
        }

        public static List<ObraSocialDTO> Buscar(string apenom)
        {
            using (AppointmentSystemMedicalEntities db = new AppointmentSystemMedicalEntities())
            {
                List<ObraSocialDTO> res = new List<ObraSocialDTO>();
                var query = db.ObraSocial
                    .Where(el => el.Nombre.Contains(apenom))
                    .OrderBy(el => el.Nombre);
                foreach (ObraSocial temp in query)
                {
                    res.Add(new ObraSocialDTO(
                        temp.Id,
                        temp.Nombre,
                        temp.Estado));
                }
                return res;
            }
        }

        public static bool Guardar(ObraSocialDTO os)
        {
            using (AppointmentSystemMedicalEntities db = new AppointmentSystemMedicalEntities())
            {
                ObraSocial nuevo = new ObraSocial();
                nuevo.Nombre = os.Nombre;
                nuevo.Estado = os.Estado;
                try
                {
                    db.ObraSocial.Add(nuevo);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool Editar(ObraSocialDTO os)
        {
            using (AppointmentSystemMedicalEntities db = new AppointmentSystemMedicalEntities())
            {
                ObraSocial modificado = db.ObraSocial
                    .Where(el => el.Id == os.Id)
                    .First();
                modificado.Nombre = os.Nombre;
                modificado.Estado = os.Estado;
                try
                {
                    db.Entry(modificado).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
