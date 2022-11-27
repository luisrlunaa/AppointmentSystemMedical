using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;

namespace AppointmentSystemMedical.CapaDatos
{
    public class PacienteDAL
    {
        DataManager Data = new DataManager();
        PersonaDAL persona = new PersonaDAL();
        public (List<PacienteDTO> result, string message) Buscar()
        {
            List<PacienteDTO> res = new List<PacienteDTO>();
            try
            {
                var join = Data.JoinExpression("INNER", new List<string>() { "Paciente" }, new List<string>() { "Persona" }, new List<string>() { "PersonaId" });
                var classKeys = Data.GetObjectKeys(new Paciente());
                var sql = Data.SelectExpression("Paciente", classKeys);
                var (dtPC, message) = Data.GetList(sql, "PacienteDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["Paciente.Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Paciente.Id"]);
                    var Id = temp["Persona.PersonaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Persona.PersonaId"]);
                    var Dni = temp["Persona.Dni"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Dni"]);
                    var Apellidos = temp["Persona.Apellidos"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Apellidos"]);
                    var Nombres = temp["Persona.Nombres"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Nombres"]);
                    var FechaNacimiento = temp["Persona.FechaNacimiento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["Persona.FechaNacimiento"]);
                    var Sexo = temp["Persona.Sexo"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Sexo"]);
                    var CorreoElectronico = temp["Persona.CorreoElectronico"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.CorreoElectronico"]);
                    var Telefono = temp["Persona.Telefono"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Telefono"]);

                    res.Add(new PacienteDTO(id, new PersonaDTO(
                        Id,
                        Dni,
                        Apellidos,
                        Nombres,
                        FechaNacimiento,
                        Sexo,
                        CorreoElectronico,
                        Telefono)));
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo PacienteDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (PacienteDTO result, string message) Buscar(int id)
        {
            var s = new PacienteDTO(0, new PersonaDTO(0, string.Empty, string.Empty, string.Empty, DateTime.MinValue, string.Empty, string.Empty, string.Empty));
            try
            {
                if (id <= 0)
                    return (s, "Error Input Invalido, Metodo PacienteDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new Paciente());
                var sql = Data.SelectExpression("Paciente", classKeys, WhereExpresion: " WHERE Persona.Id ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "PacienteDAL.BuscarById");
                if (dr is null)
                    return (s, message1);


                s.Id = dr["Id"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("Id")) : 0;
                s.Persona.Id = dr["Persona.PersonaId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("Persona.PersonaId")) : 0;
                s.Persona.Dni = dr["Persona.Dni"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Persona.Dni")) : string.Empty;
                s.Persona.Apellidos = dr["Persona.Apellidos"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Persona.Apellidos")) : string.Empty;
                s.Persona.Nombres = dr["Persona.Nombres"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Persona.Nombres")) : string.Empty;
                s.Persona.FechaNacimiento = dr["Persona.FechaNacimiento"].GetType() != typeof(DBNull) ? dr.GetDateTime(dr.GetOrdinal("Persona.FechaNacimiento")) : DateTime.MinValue;
                s.Persona.Sexo = dr["Persona.Sexo"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Persona.Sexo")) : string.Empty;
                s.Persona.CorreoElectronico = dr["Persona.CorreoElectronico"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Persona.CorreoElectronico")) : string.Empty;
                s.Persona.Telefono = dr["Persona.Telefono"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Persona.Telefono")) : string.Empty;

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo PacienteDAL.BuscarById \n" + ex.Message.ToString());
            }
        }

        public (List<PacienteDTO> result, string message) Buscar(EmpleadoDTO med)
        {
            List<PacienteDTO> res = new List<PacienteDTO>();
            try
            {
                // Busco los pacientes de ese medico
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno" }, new List<string>() { "Medico" }, new List<string>() { "MedicoId" });
                var classKeys = new List<string>() { "PacienteId" };
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: "WHERE Medico.EmpleadoId ='" + med.Id + "'");
                var (dtPC, message) = Data.GetList(sql, "PacienteDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    var (patient, message1) = Buscar(id);
                    if(!message1.Contains("Error"))
                        res.Add(patient);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo PacienteDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (List<PacienteDTO> result, string message) BuscarDni(string dni)
        {
            List<PacienteDTO> res = new List<PacienteDTO>();
            try
            {
                var join = Data.JoinExpression("INNER", new List<string>() { "Paciente" }, new List<string>() { "Persona" }, new List<string>() { "PersonaId" });
                var classKeys = Data.GetObjectKeys(new Paciente());
                var sql = Data.SelectExpression("Paciente", classKeys, WhereExpresion: "WHERE Persona.Dni Like '%"+ dni +"%'");
                var (dtPC, message) = Data.GetList(sql, "PacienteDAL.BuscarDni");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["Paciente.Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Paciente.Id"]);
                    var Id = temp["Persona.PersonaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Persona.PersonaId"]);
                    var Dni = temp["Persona.Dni"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Dni"]);
                    var Apellidos = temp["Persona.Apellidos"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Apellidos"]);
                    var Nombres = temp["Persona.Nombres"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Nombres"]);
                    var FechaNacimiento = temp["Persona.FechaNacimiento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["Persona.FechaNacimiento"]);
                    var Sexo = temp["Persona.Sexo"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Sexo"]);
                    var CorreoElectronico = temp["Persona.CorreoElectronico"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.CorreoElectronico"]);
                    var Telefono = temp["Persona.Telefono"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Telefono"]);

                    res.Add(new PacienteDTO(id, new PersonaDTO(
                        Id,
                        Dni,
                        Apellidos,
                        Nombres,
                        FechaNacimiento,
                        Sexo,
                        CorreoElectronico,
                        Telefono)));
                }
                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo PacienteDAL.BuscarDni \n" + ex.Message.ToString());
            }
        }

        public (List<PacienteDTO> result, string message) BuscarDni(EmpleadoDTO med, string dni)
        {
            List<PacienteDTO> res = new List<PacienteDTO>();
            try
            {
                // Busco los pacientes de ese medico
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno" }, new List<string>() { "Medico" }, new List<string>() { "MedicoId" });
                var classKeys = new List<string>() { "PacienteId" };
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: "WHERE Medico.EmpleadoId ='"+ med.Id + "'");
                var (dtPC, message) = Data.GetList(sql, "PacienteDAL.BuscarDni");

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    var (patient, message1) = Buscar(id);
                    if (!message1.Contains("Error") && patient.Persona.Dni == dni)
                        res.Add(patient);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo PacienteDAL.BuscarDni \n" + ex.Message.ToString());
            }
        }

        public (List<PacienteDTO> result, string message) BuscarApeNom(string apenom)
        {
            List<PacienteDTO> res = new List<PacienteDTO>();
            try
            {
                var join = Data.JoinExpression("INNER", new List<string>() { "Paciente" }, new List<string>() { "Persona" }, new List<string>() { "PersonaId" });
                var classKeys = Data.GetObjectKeys(new Paciente());
                var sql = Data.SelectExpression("Paciente", classKeys, WhereExpresion: "WHERE Persona.Nombres Like '%" + apenom + "%' OR Persona.Apellidos Like '%" + apenom + "%'");
                var (dtPC, message) = Data.GetList(sql, "PacienteDAL.BuscarApeNom");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["Paciente.Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Paciente.Id"]);
                    var Id = temp["Persona.PersonaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Persona.PersonaId"]);
                    var Dni = temp["Persona.Dni"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Dni"]);
                    var Apellidos = temp["Persona.Apellidos"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Apellidos"]);
                    var Nombres = temp["Persona.Nombres"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Nombres"]);
                    var FechaNacimiento = temp["Persona.FechaNacimiento"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["Persona.FechaNacimiento"]);
                    var Sexo = temp["Persona.Sexo"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Sexo"]);
                    var CorreoElectronico = temp["Persona.CorreoElectronico"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.CorreoElectronico"]);
                    var Telefono = temp["Persona.Telefono"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Persona.Telefono"]);

                    res.Add(new PacienteDTO(id, new PersonaDTO(
                        Id,
                        Dni,
                        Apellidos,
                        Nombres,
                        FechaNacimiento,
                        Sexo,
                        CorreoElectronico,
                        Telefono)));
                }
                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo PacienteDAL.BuscarApeNom \n" + ex.Message.ToString());
            }
        }

        public (List<PacienteDTO> result, string message) BuscarApeNom(EmpleadoDTO med, string apenom)
        {
            List<PacienteDTO> res = new List<PacienteDTO>();
            try
            {
                // Busco los pacientes de ese medico
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno" }, new List<string>() { "Medico" }, new List<string>() { "MedicoId" });
                var classKeys = new List<string>() { "PacienteId" };
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: "WHERE Medico.EmpleadoId ='" + med.Id + "'");
                var (dtPC, message) = Data.GetList(sql, "PacienteDAL.BuscarDni");

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    var (patient, message1) = Buscar(id);
                    if (!message1.Contains("Error") && (patient.Persona.Nombres.Contains(apenom) || patient.Persona.Apellidos.Contains(apenom)))
                        res.Add(patient);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo PacienteDAL.BuscarDni \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Guardar(PacienteDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo PacienteDAL.Guardar");

                var (add, message1) = persona.Guardar(new PersonaDTO(input.Persona.Id, input.Persona.Dni, input.Persona.Apellidos, input.Persona.Nombres,
                    input.Persona.FechaNacimiento, input.Persona.Sexo, input.Persona.CorreoElectronico, input.Persona.Telefono));
                
                if(!add)
                    return (add, message1);

                var sql = Data.SelectExpression("Persona", new List<string>() { "PersonaId" },OrderBy: "PersonaId DESC");
                var (dr, message2) = Data.GetOne(sql, "PersonaDAL.BuscarById");
                if (dr is null)
                    return (false, message2);

                var Id = dr["PersonaId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("PersonaId")) : 0;
                var parameters = new List<string> {  "'" + input.Id + "'", "'" + Id + "'"};
                var classKeys = Data.GetObjectKeys(new Paciente()).Where(x => x != "PacienteId").ToList();
                var sql1 = Data.InsertExpression("Paciente", classKeys, parameters);
                var (response, message) = Data.CrudAction(sql1, "PacienteDAL.Guardar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo PacienteDAL.Guardar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Editar(PacienteDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo PacienteDAL.Editar");

                var (edit, message1) = persona.Editar(new PersonaDTO(input.Persona.Dni, input.Persona.Apellidos, input.Persona.Nombres,
                   input.Persona.FechaNacimiento, input.Persona.Sexo, input.Persona.CorreoElectronico, input.Persona.Telefono));

                if (!edit)
                    return (edit, message1);

                return (edit, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo PacienteDAL.Editar \n" + ex.Message.ToString());
            }
        }
    }
}
