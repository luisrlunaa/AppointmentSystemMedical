using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class TurnoDAL
    {
        DataManager Data = new DataManager();
        MedicoDAL medicoDAL = new MedicoDAL();
        PacienteDAL pacienteDAL = new PacienteDAL();
        CoberturaDAL coberturaDAL = new CoberturaDAL();
        TurnoEstadoDAL turnoEstadoDAL = new TurnoEstadoDAL();

        public (List<TurnoDTO> result, string message) Buscar()
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var sql = Data.SelectExpression("Turno", classKeys, OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo MedicoDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (TurnoDTO result, string message) Buscar(int id)
        {
            var s = new TurnoDTO();
            try
            {
                if (id <= 0)
                    return (s, "Error Input Invalido, Metodo EspecialidadDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new Turno());
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE TurnoId ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "TurnoDAL.BuscarById");
                if (dr is null)
                    return (s, message1);

                s.Id = dr["TurnoId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("TurnoId")) : 0;
                s.FechaHora = dr["FechaHora"].GetType() != typeof(DBNull) ? dr.GetDateTime(dr.GetOrdinal("FechaHora")) : DateTime.MinValue;

                var idm = dr["MedicoId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("MedicoId")) : 0;
                if (idm > 0)
                {
                    var medico = medicoDAL.Buscar(idm);
                    if (medico.result != null)
                        s.Medico = medico.result;
                }

                var idc = dr["CoberturaId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("CoberturaId")) : 0;
                if (idc > 0)
                {
                    var cobertura = coberturaDAL.Buscar(idc);
                    if (cobertura.result != null)
                        s.Cobertura = cobertura.result;
                }

                var idp = dr["CoberturaId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("CoberturaId")) : 0;
                if (idp > 0)
                {
                    var paciente = pacienteDAL.Buscar(idp);
                    if (paciente.result != null)
                        s.Paciente = paciente.result;
                }

                var ide = dr["EstadoId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("EstadoId")) : 0;
                if (ide > 0)
                {
                    var estado = turnoEstadoDAL.Buscar(ide);
                    if (estado.result != null)
                        s.Estado = estado.result;
                }

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo TurnoDAL.BuscarById \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) Buscar(DateTime desde, DateTime hasta)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno" }, new List<string>() { "Medico" }, new List<string>() { "MedicoId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE FechaHora BETWEEN '" + desde + "' AND '" + hasta + "'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarByEmpleadoId");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarByEmpleadoId \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) Buscar(EmpleadoDTO med)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno" }, new List<string>() { "Medico" }, new List<string>() { "MedicoId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE Medico.EmpleadoId ='" + med.Id + "'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarByEmpleadoId");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarByEmpleadoId \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) Buscar(EmpleadoDTO med, DateTime desde, DateTime hasta)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno" }, new List<string>() { "Medico" }, new List<string>() { "MedicoId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE Medico.EmpleadoId ='" + med.Id + "' AND FechaHora BETWEEN '" + desde + "' AND '" + hasta + "'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarByEmpleadoId");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarByEmpleadoId \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) Buscar(MedicoDTO med, DateTime dia)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno", "Empleado", "Persona" }, new List<string>() { "Medico", "Persona", "Paciente" }, new List<string>() { "MedicoId", "PersonaId", "PersonaId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE MedicoId = '" + med.Id + "' AND FechaHora = '" + dia.ToShortDateString() + "'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarByEmpleadoId");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarByEmpleadoId \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) BuscarApeNom(string apenom)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno", "Empleado", "Persona" }, new List<string>() { "Medico", "Persona", "Paciente" }, new List<string>() { "MedicoId", "PersonaId", "PersonaId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE Medico.Empleado.Persona.Apellidos LIKE '%" + apenom + "%'"
                    + "OR Medico.Empleado.Persona.Nombres LIKE '%" + apenom + "%' OR Paciente.Persona.Apellidos LIKE '%" + apenom
                    + "%' OR Paciente.Persona.Nombres LIKE '%" + apenom + "%'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarByEmpleadoId");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarByEmpleadoId \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) BuscarApeNom(string apenom, DateTime desde, DateTime hasta)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno", "Empleado", "Persona" }, new List<string>() { "Medico", "Persona", "Paciente" }, new List<string>() { "MedicoId", "PersonaId", "PersonaId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE Medico.Empleado.Persona.Apellidos LIKE '%" + apenom + "%'"
                    + "OR Medico.Empleado.Persona.Nombres LIKE '%" + apenom + "%' OR Paciente.Persona.Apellidos LIKE '%" + apenom
                    + "%' OR Paciente.Persona.Nombres LIKE '%" + apenom + "%' AND FechaHora BETWEEN '" + desde + "' AND '" + hasta + "'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarByEmpleadoId");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarByEmpleadoId \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) BuscarApeNom(EmpleadoDTO med, string apenom)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno", "Empleado", "Persona" }, new List<string>() { "Medico", "Persona", "Paciente" }, new List<string>() { "MedicoId", "PersonaId", "PersonaId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE Medico.EmpleadoId = " + med.Id + "OR Paciente.Persona.Apellidos LIKE '%" + apenom + "%' OR Paciente.Persona.Nombres LIKE '%" + apenom + "%'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarByEmpleadoId");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarByEmpleadoId \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) BuscarApeNom(EmpleadoDTO med, string apenom, DateTime desde, DateTime hasta)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno", "Empleado", "Persona" }, new List<string>() { "Medico", "Persona", "Paciente" }, new List<string>() { "MedicoId", "PersonaId", "PersonaId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE Medico.EmpleadoId = " + med.Id + " AND Medico.Empleado.Persona.Apellidos LIKE '%" + apenom + "%'"
                    + "OR Medico.Empleado.Persona.Nombres LIKE '%" + apenom + "%' OR Paciente.Persona.Apellidos LIKE '%" + apenom
                    + "%' OR Paciente.Persona.Nombres LIKE '%" + apenom + "%' AND FechaHora BETWEEN '" + desde + "' AND '" + hasta + "'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarByEmpleadoId");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarByEmpleadoId \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) BuscarDni(string dni)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno", "Empleado", "Persona" }, new List<string>() { "Medico", "Persona", "Paciente" }, new List<string>() { "MedicoId", "PersonaId", "PersonaId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE Medico.Empleado.Persona.Dni LIKE '%" + dni + "%'"
                    + "OR Paciente.Persona.Dni LIKE '%" + dni + "%'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarDni");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarDni \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) BuscarDni(string dni, DateTime desde, DateTime hasta)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno", "Empleado", "Persona" }, new List<string>() { "Medico", "Persona", "Paciente" }, new List<string>() { "MedicoId", "PersonaId", "PersonaId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE Medico.Empleado.Persona.Dni LIKE '%" + dni + "%'"
                    + "OR Paciente.Persona.Dni LIKE '%" + dni + "%' AND FechaHora BETWEEN '" + desde + "' AND '" + hasta + "'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarDni");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarDni \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) BuscarDni(EmpleadoDTO med, string dni)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno", "Empleado", "Persona" }, new List<string>() { "Medico", "Persona", "Paciente" }, new List<string>() { "MedicoId", "PersonaId", "PersonaId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE Medico.EmpleadoId = '" + med.Id + "' AND Medico.Empleado.Persona.Dni LIKE '%" + dni + "%'"
                    + "OR Paciente.Persona.Dni LIKE '%" + dni + "%'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarDni");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarDni \n" + ex.Message.ToString());
            }
        }

        public (List<TurnoDTO> result, string message) BuscarDni(EmpleadoDTO med, string dni, DateTime desde, DateTime hasta)
        {
            List<TurnoDTO> res = new List<TurnoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Turno());
                var join = Data.JoinExpression("INNER", new List<string>() { "Turno", "Empleado", "Persona" }, new List<string>() { "Medico", "Persona", "Paciente" }, new List<string>() { "MedicoId", "PersonaId", "PersonaId" });
                var sql = Data.SelectExpression("Turno", classKeys, WhereExpresion: " WHERE Medico.EmpleadoId = '" + med.Id + "' AND Medico.Empleado.Persona.Dni LIKE '%" + dni + "%'"
                    + "OR Paciente.Persona.Dni LIKE '%" + dni + "%' AND FechaHora BETWEEN '" + desde + "' AND '" + hasta + "'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "TurnoDAL.BuscarDni");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new TurnoDTO();
                    s.Id = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    s.FechaHora = temp["FechaHora"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(temp["FechaHora"]);

                    var idm = temp["MedicoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["MedicoId"]);
                    if (idm > 0)
                    {
                        var medico = medicoDAL.Buscar(idm);
                        if (medico.result != null)
                            s.Medico = medico.result;
                    }

                    var idc = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    if (idc > 0)
                    {
                        var cobertura = coberturaDAL.Buscar(idc);
                        if (cobertura.result != null)
                            s.Cobertura = cobertura.result;
                    }

                    var idp = temp["PacienteId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["PacienteId"]);
                    if (idp > 0)
                    {
                        var paciente = pacienteDAL.Buscar(idp);
                        if (paciente.result != null)
                            s.Paciente = paciente.result;
                    }

                    var ide = temp["EstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["EstadoId"]);
                    if (ide > 0)
                    {
                        var estado = turnoEstadoDAL.Buscar(ide);
                        if (estado.result != null)
                            s.Estado = estado.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoDAL.BuscarDni \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Guardar(TurnoDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo TurnoDAL.Guardar");

                var parameters = new List<string> { "'" + input.Paciente.Id + "'", "'" + input.Cobertura.Id + "'", "'" + input.Estado.Id + "'", "'" + input.FechaHora.ToShortDateString() + "'" };
                var classKeys = Data.GetObjectKeys(new Turno()).Where(x => x != "TurnoId").ToList();
                var sql = Data.InsertExpression("Turno", classKeys, parameters);
                var (response, message) = Data.CrudAction(sql, "TurnoDAL.Guardar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo TurnoDAL.Guardar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Editar(TurnoDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo TurnoDAL.Editar");

                var parameters = new List<string> { "'" + input.Paciente.Id + "'", "'" + input.Cobertura.Id + "'", "'" + input.Estado.Id + "'", "'" + input.FechaHora.ToShortDateString() + "'" };
                var classKeys = Data.GetObjectKeys(new Turno()).Where(x => x != "TurnoId").ToList();
                var sql = Data.UpdateExpression("Turno", classKeys, parameters, " WHERE TurnoId = '" + input.Id + "'");
                var (response, message) = Data.CrudAction(sql, "TurnoDAL.Editar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo TurnoDAL.Editar \n" + ex.Message.ToString());
            }
        }
    }
}
