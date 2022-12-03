using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class EmpleadoDAL
    {
        DataManager Data = new DataManager();
        PersonaDAL personaDAL = new PersonaDAL();
        TipoUsuarioDAL tipoUsuarioDAL = new TipoUsuarioDAL();

        public (List<EmpleadoDTO> result, string message) Buscar()
        {
            List<EmpleadoDTO> res = new List<EmpleadoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Empleado());
                var sql = Data.SelectExpression("Empleado", classKeys);
                var (dtPC, message) = Data.GetList(sql, "EmpleadoDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new EmpleadoDTO();
                    var idPersonal = temp["PersonaId"] != DBNull.Value ? Convert.ToInt32(temp["PersonaId"]) : 0;
                    if (idPersonal > 0)
                    {
                        var personal = personaDAL.Buscar(idPersonal);
                        if (personal.result != null)
                            s.Persona = personal.result;
                    }

                    var idTipoUsuario = temp["TipoUsuarioId"] != DBNull.Value ? Convert.ToInt32(temp["TipoUsuarioId"]) : 0;
                    if (idTipoUsuario > 0)
                    {
                        var tipoUsuario = tipoUsuarioDAL.Buscar(idTipoUsuario);
                        if (tipoUsuario.result != null)
                            s.TipoUsuario = tipoUsuario.result;
                    }

                    s.Id = temp["EmpleadoId"] != DBNull.Value ? Convert.ToInt32(temp["EmpleadoId"]) : 0;
                    s.Cuil = temp["Cuil"] != DBNull.Value ? Convert.ToString(temp["Cuil"]) : string.Empty;
                    s.Usuario = temp["Usuario"] != DBNull.Value ? Convert.ToString(temp["Usuario"]) : string.Empty;
                    s.Contraseña = temp["Contraseña"] != DBNull.Value ? Convert.ToString(temp["Contraseña"]) : string.Empty;
                    s.FechaIngreso = temp["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(temp["FechaIngreso"]) : DateTime.MinValue;
                    s.Activo = temp["Activo"] != DBNull.Value ? Convert.ToBoolean(temp["Activo"]) : false;

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo EmpleadoDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (EmpleadoDTO result, string message) Buscar(int id)
        {
            var s = new EmpleadoDTO();
            try
            {
                if (id == 0)
                    return (s, "Error Input Invalido, Metodo EmpleadoDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new Empleado());
                var sql = Data.SelectExpression("Empleado", classKeys, WhereExpresion: " WHERE EmpleadoId ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "EmpleadoDAL.BuscarById");
                if (dr is null)
                    return (s, message1);

                var idPersonal = dr["PersonaId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("PersonaId")) : 0;
                if (idPersonal > 0)
                {
                    var personal = personaDAL.Buscar(idPersonal);
                    if (personal.result != null)
                        s.Persona = personal.result;
                }

                var idTipoUsuario = dr["TipoUsuarioId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("TipoUsuarioId")) : 0;
                if (idTipoUsuario > 0)
                {
                    var tipoUsuario = tipoUsuarioDAL.Buscar(idTipoUsuario);
                    if (tipoUsuario.result != null)
                        s.TipoUsuario = tipoUsuario.result;
                }

                s.Id = dr["EmpleadoId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("EmpleadoId")) : 0;
                s.Cuil = dr["Cuil"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Cuil")) : string.Empty;
                s.Usuario = dr["Usuario"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Usuario")) : string.Empty;
                s.Contraseña = dr["Contraseña"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Contraseña")) : string.Empty;
                s.FechaIngreso = dr["FechaIngreso"].GetType() != typeof(DBNull) ? dr.GetDateTime(dr.GetOrdinal("FechaIngreso")) : DateTime.MinValue;
                s.Activo = dr["Activo"].GetType() != typeof(DBNull) ? dr.GetBoolean(dr.GetOrdinal("Activo")) : false;

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo EmpleadoDAL.BuscarById \n" + ex.Message.ToString());
            }
        }

        public (List<EmpleadoDTO> result, string message) BuscarDni(string dni)
        {
            List<EmpleadoDTO> res = new List<EmpleadoDTO>();
            try
            {
                var join = Data.JoinExpression("INNER", new List<string>() { "Empleado" }, new List<string>() { "Persona" }, new List<string>() { "PersonaId" });
                var classKeys = Data.GetObjectKeys(new Empleado());
                var sql = Data.SelectExpression("Empleado", classKeys, JoinExp: join, WhereExpresion: "Where Persona.Dni = '" + dni + "'");
                var (dtPC, message) = Data.GetList(sql, "EmpleadoDAL.BuscarDni");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new EmpleadoDTO();
                    var idPersonal = temp["PersonaId"] != DBNull.Value ? Convert.ToInt32(temp["PersonaId"]) : 0;
                    if (idPersonal > 0)
                    {
                        var personal = personaDAL.Buscar(idPersonal);
                        if (personal.result != null)
                            s.Persona = personal.result;
                    }

                    var idTipoUsuario = temp["TipoUsuarioId"] != DBNull.Value ? Convert.ToInt32(temp["TipoUsuarioId"]) : 0;
                    if (idTipoUsuario > 0)
                    {
                        var tipoUsuario = tipoUsuarioDAL.Buscar(idTipoUsuario);
                        if (tipoUsuario.result != null)
                            s.TipoUsuario = tipoUsuario.result;
                    }

                    s.Id = temp["EmpleadoId"] != DBNull.Value ? Convert.ToInt32(temp["EmpleadoId"]) : 0;
                    s.Cuil = temp["Cuil"] != DBNull.Value ? Convert.ToString(temp["Cuil"]) : string.Empty;
                    s.Usuario = temp["Usuario"] != DBNull.Value ? Convert.ToString(temp["Usuario"]) : string.Empty;
                    s.Contraseña = temp["Contraseña"] != DBNull.Value ? Convert.ToString(temp["Contraseña"]) : string.Empty;
                    s.FechaIngreso = temp["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(temp["FechaIngreso"]) : DateTime.MinValue;
                    s.Activo = temp["Activo"] != DBNull.Value ? Convert.ToBoolean(temp["Activo"]) : false;

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo EmpleadoDAL.BuscarDni \n" + ex.Message.ToString());
            }
        }

        public (List<EmpleadoDTO> result, string message) BuscarApeNom(string apenom)
        {
            List<EmpleadoDTO> res = new List<EmpleadoDTO>();
            try
            {
                var join = Data.JoinExpression("INNER", new List<string>() { "Empleado" }, new List<string>() { "Persona" }, new List<string>() { "PersonaId" });
                var classKeys = Data.GetObjectKeys(new Empleado());
                var sql = Data.SelectExpression("Empleado", classKeys, JoinExp: join, WhereExpresion: "Where Persona.Apellidos LIKE '%" + apenom + "%' OR Persona.Nombres LIKE '%" + apenom + "%'");
                var (dtPC, message) = Data.GetList(sql, "EmpleadoDAL.BuscarApeNom");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new EmpleadoDTO();
                    var idPersonal = temp["PersonaId"] != DBNull.Value ? Convert.ToInt32(temp["PersonaId"]) : 0;
                    if (idPersonal > 0)
                    {
                        var personal = personaDAL.Buscar(idPersonal);
                        if (personal.result != null)
                            s.Persona = personal.result;
                    }

                    var idTipoUsuario = temp["TipoUsuarioId"] != DBNull.Value ? Convert.ToInt32(temp["TipoUsuarioId"]) : 0;
                    if (idTipoUsuario > 0)
                    {
                        var tipoUsuario = tipoUsuarioDAL.Buscar(idTipoUsuario);
                        if (tipoUsuario.result != null)
                            s.TipoUsuario = tipoUsuario.result;
                    }

                    s.Id = temp["EmpleadoId"] != DBNull.Value ? Convert.ToInt32(temp["EmpleadoId"]) : 0;
                    s.Cuil = temp["Cuil"] != DBNull.Value ? Convert.ToString(temp["Cuil"]) : string.Empty;
                    s.Usuario = temp["Usuario"] != DBNull.Value ? Convert.ToString(temp["Usuario"]) : string.Empty;
                    s.Contraseña = temp["Contraseña"] != DBNull.Value ? Convert.ToString(temp["Contraseña"]) : string.Empty;
                    s.FechaIngreso = temp["FechaIngreso"] != DBNull.Value ? Convert.ToDateTime(temp["FechaIngreso"]) : DateTime.MinValue;
                    s.Activo = temp["Activo"] != DBNull.Value ? Convert.ToBoolean(temp["Activo"]) : false;

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo EmpleadoDAL.BuscarApeNom \n" + ex.Message.ToString());
            }
        }

        public (EmpleadoDTO result, string message) BuscarUsuario(string usuario)
        {
            var s = new EmpleadoDTO();
            try
            {
                if (string.IsNullOrWhiteSpace(usuario))
                    return (s, "Error Input Invalido, Metodo EmpleadoDAL.BuscarByUsuario");

                var classKeys = Data.GetObjectKeys(new Empleado());
                var sql = Data.SelectExpression("Empleado", classKeys, WhereExpresion: " WHERE Usuario ='" + usuario + "'");
                var (dr, message1) = Data.GetOne(sql, "EmpleadoDAL.BuscarByUsuario");
                if (dr is null)
                    return (s, message1);

                var idPersonal = dr["PersonaId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("PersonaId")) : 0;
                if (idPersonal > 0)
                {
                    var personal = personaDAL.Buscar(idPersonal);
                    if (personal.result != null)
                        s.Persona = personal.result;
                }

                var idTipoUsuario = dr["TipoUsuarioId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("TipoUsuarioId")) : 0;
                if (idTipoUsuario > 0)
                {
                    var tipoUsuario = tipoUsuarioDAL.Buscar(idTipoUsuario);
                    if (tipoUsuario.result != null)
                        s.TipoUsuario = tipoUsuario.result;
                }

                s.Id = dr["EmpleadoId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("EmpleadoId")) : 0;
                s.Cuil = dr["Cuil"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Cuil")) : string.Empty;
                s.Usuario = dr["Usuario"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Usuario")) : string.Empty;
                s.Contraseña = dr["Contraseña"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Contraseña")) : string.Empty;
                s.FechaIngreso = dr["FechaIngreso"].GetType() != typeof(DBNull) ? dr.GetDateTime(dr.GetOrdinal("FechaIngreso")) : DateTime.MinValue;
                s.Activo = dr["Activo"].GetType() != typeof(DBNull) ? dr.GetBoolean(dr.GetOrdinal("Activo")) : false;

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo EmpleadoDAL.BuscarByUsuario \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Guardar(EmpleadoDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo EmpleadoDAL.Guardar");

                var parameters = new List<string> { "'" + input.Cuil + "'", "'" + input.Usuario + "'", "'" + input.Contraseña + "'",
                    "'" + input.FechaIngreso + "'", "'" + input.Activo + "'", "'" + input.TipoUsuario.Id + "'" };
                var classKeys = Data.GetObjectKeys(new Empleado()).Where(x => x != "EmpleadoId").ToList();
                var sql = Data.InsertExpression("Empleado", classKeys, parameters);
                var (response, message) = Data.CrudAction(sql, "EmpleadoDAL.Guardar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo EmpleadoDAL.Guardar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Editar(EmpleadoDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo EmpleadoDAL.Editar");

                var parameters = new List<string> { "'" + input.Cuil + "'", "'" + input.Usuario + "'", "'" + input.Contraseña + "'",
                    "'" + input.FechaIngreso + "'", "'" + input.Activo + "'", "'" + input.TipoUsuario.Id + "'" };
                var classKeys = Data.GetObjectKeys(new Empleado()).Where(x => x != "EmpleadoId").ToList();
                var sql = Data.UpdateExpression("Empleado", classKeys, parameters, " WHERE EmpleadoId = '" + input.Id + "'");
                var (response, message) = Data.CrudAction(sql, "EmpleadoDAL.Editar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo EmpleadoDAL.Editar \n" + ex.Message.ToString());
            }
        }
    }
}
