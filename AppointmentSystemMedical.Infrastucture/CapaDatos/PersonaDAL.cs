using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class PersonaDAL
    {
        DataManager Data = new DataManager();
        public (PersonaDTO result, string message) Buscar(int id)
        {
            var s = new PersonaDTO();
            try
            {
                if (id <= 0)
                    return (s, "Error Input Invalido, Metodo PersonaDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new Persona()).Where(x => x != "Empleado" && x != "Paciente").ToList();
                var sql = Data.SelectExpression("Persona", classKeys, WhereExpresion: " WHERE Persona.PersonaId ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "PersonaDAL.BuscarById");
                if (dr is null)
                    return (s, message1);

                s.Id = dr["PersonaId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("PersonaId")) : 0;
                s.Dni = dr["Dni"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Dni")) : string.Empty;
                s.Apellidos = dr["Apellidos"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Apellidos")) : string.Empty;
                s.Nombres = dr["Nombres"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Nombres")) : string.Empty;
                s.FechaNacimiento = dr["FechaNacimiento"].GetType() != typeof(DBNull) ? dr.GetDateTime(dr.GetOrdinal("FechaNacimiento")) : DateTime.MinValue;
                s.Sexo = dr["Sexo"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Sexo")) : string.Empty;
                s.CorreoElectronico = dr["CorreoElectronico"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("CorreoElectronico")) : string.Empty;
                s.Telefono = dr["Telefono"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Telefono")) : string.Empty;

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo PersonaDAL.BuscarById \n" + ex.Message.ToString());
            }
        }

        public (List<PersonaDTO> result, string message) BuscarDni(string dni)
        {
            List<PersonaDTO> res = new List<PersonaDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Persona()).Where(x => x != "Empleado" && x != "Paciente").ToList();
                var sql = Data.SelectExpression("Persona", classKeys, WhereExpresion: "Where Persona.Dni = '" + dni + "'");
                var (dtPC, message) = Data.GetList(sql, "PersonaDAL.BuscarDni");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var Id = temp["PersonaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PersonaId"]);
                    var Dni = temp["Dni"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Dni"]);
                    var Apellidos = temp["Apellidos"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Apellidos"]);
                    var Nombres = temp["Nombres"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Nombres"]);
                    var FechaNacimiento = temp["FechaNacimiento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaNacimiento"]);
                    var Sexo = temp["Sexo"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Sexo"]);
                    var CorreoElectronico = temp["CorreoElectronico"] == DBNull.Value ? string.Empty : Convert.ToString(temp["CorreoElectronico"]);
                    var Telefono = temp["Telefono"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Telefono"]);

                    res.Add(new PersonaDTO(
                        Id,
                        Dni,
                        Apellidos,
                        Nombres,
                        FechaNacimiento,
                        Sexo,
                        CorreoElectronico,
                        Telefono));
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo PersonaDAL.BuscarDni \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Guardar(PersonaDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo PersonaDAL.Guardar");

                var parameters = new List<string> {  "'" + input.Dni + "'", "'" + input.Apellidos + "'", "'" + input.Nombres + "'",
                    "'" + input.FechaNacimiento.ToShortDateString() + "'", "'" + input.Sexo + "'", "'" + input.CorreoElectronico + "'",
                    "'" + input.Telefono + "'" };
                var classKeys = Data.GetObjectKeys(new Persona()).Where(x => x != "PersonaId" && x != "Empleado" && x != "Paciente").ToList();
                var sql = Data.InsertExpression("Persona", classKeys, parameters);
                var (response, message) = Data.CrudAction(sql, "PersonaDAL.Guardar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo PersonaDAL.Guardar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Editar(PersonaDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo ObraSocialDAL.Editar");

                var parameters = new List<string> { "'" + input.Apellidos + "'", "'" + input.Nombres + "'", "'" + input.FechaNacimiento.ToShortDateString() + "'",
                    "'" + input.Sexo + "'", "'" + input.CorreoElectronico + "'", "'" + input.Telefono + "'" };
                var classKeys = Data.GetObjectKeys(new Persona()).Where(x => x != "PersonaId" && x != "Dni" && x != "Empleado" && x != "Paciente").ToList();
                var sql = Data.UpdateExpression("ObraSocial", classKeys, parameters, " WHERE Dni = '" + input.Dni + "'");
                var (response, message) = Data.CrudAction(sql, "PersonaDAL.Editar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo PersonaDAL.Editar \n" + ex.Message.ToString());
            }
        }
    }
}
