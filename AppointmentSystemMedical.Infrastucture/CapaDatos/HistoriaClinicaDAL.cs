using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class HistoriaClinicaDAL
    {
        DataManager Data = new DataManager();
        TurnoDAL turnoDAL = new TurnoDAL();

        public (HistoriaClinicaDTO result, string message) Buscar(int id)
        {
            var s = new HistoriaClinicaDTO();
            try
            {
                if (id <= 0)
                    return (s, "Error Input Invalido, Metodo EspecialidadDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new HistoriaClinica());
                var sql = Data.SelectExpression("HistoriaClinica", classKeys, WhereExpresion: " WHERE HistoriaClinicaId ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "HistoriaClinicaDAL.BuscarById");
                if (dr is null)
                    return (s, message1);

                s.Id = dr["HistoriaClinicaId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("HistoriaClinicaId")) : 0;
                s.Descripcion = dr["Descripcion"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                s.Archivo = dr["Archivo"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Archivo")) : string.Empty;

                var idt = dr["TurnoId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("TurnoId")) : 0;
                if (idt > 0)
                {
                    var medico = turnoDAL.Buscar(idt);
                    if (medico.result != null)
                        s.Turno = medico.result;
                }

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo HistoriaClinicaDAL.BuscarById \n" + ex.Message.ToString());
            }
        }

        public (HistoriaClinicaDTO result, string message) Buscar(TurnoDTO tur)
        {
            var s = new HistoriaClinicaDTO();
            try
            {
                if (tur.Id <= 0)
                    return (s, "Error Input Invalido, Metodo EspecialidadDAL.BuscarByTurnoId");

                var join = Data.JoinExpression("INNER", new List<string>() { "HistoriaClinica" }, new List<string>() { "Turno" }, new List<string>() { "TurnoId" });
                var classKeys = Data.GetObjectKeys(new HistoriaClinica());
                var sql = Data.SelectExpression("HistoriaClinica", classKeys, WhereExpresion: " WHERE Turno.TurnoId ='" + tur.Id + "'");
                var (dr, message1) = Data.GetOne(sql, "HistoriaClinicaDAL.BuscarByTurnoId");
                if (dr is null)
                    return (s, message1);

                s.Id = dr["HistoriaClinicaId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("HistoriaClinicaId")) : 0;
                s.Descripcion = dr["Descripcion"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                s.Archivo = dr["Archivo"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Archivo")) : string.Empty;

                var idt = dr["TurnoId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("TurnoId")) : 0;
                if (idt > 0)
                {
                    var medico = turnoDAL.Buscar(idt);
                    if (medico.result != null)
                        s.Turno = medico.result;
                }

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo HistoriaClinicaDAL.BuscarByTurnoId \n" + ex.Message.ToString());
            }
        }

        public (List<HistoriaClinicaDTO> result, string message) Buscar(PacienteDTO pac)
        {
            List<HistoriaClinicaDTO> res = new List<HistoriaClinicaDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new HistoriaClinica());
                var join = Data.JoinExpression("INNER", new List<string>() { "HistoriaClinica" }, new List<string>() { "Turno" }, new List<string>() { "TurnoId" });
                var sql = Data.SelectExpression("HistoriaClinica", classKeys, WhereExpresion: " WHERE Turno.PacienteId = '" + pac.Id + "'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "HistoriaClinicaDAL.BuscarByEmpleadoId");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new HistoriaClinicaDTO();
                    s.Id = temp["HistoriaClinicaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["HistoriaClinicaId"]);
                    s.Descripcion = temp["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Descripcion"]);
                    s.Archivo = temp["Archivo"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Archivo"]);

                    var idt = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    if (idt > 0)
                    {
                        var medico = turnoDAL.Buscar(idt);
                        if (medico.result != null)
                            s.Turno = medico.result;
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

        public (List<HistoriaClinicaDTO> result, string message) Buscar(EmpleadoDTO emp, PacienteDTO pac)
        {
            List<HistoriaClinicaDTO> res = new List<HistoriaClinicaDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new HistoriaClinica());
                var join = Data.JoinExpression("INNER", new List<string>() { "HistoriaClinica", "Turno" }, new List<string>() { "Turno", "Medico" }, new List<string>() { "TurnoId" });
                var sql = Data.SelectExpression("HistoriaClinica", classKeys, WhereExpresion: " WHERE Turno.PacienteId = '" + pac.Id + "' AND Medico.EmpleadoId= '" + emp.Id + "'", OrderBy: "FechaHora");
                var (dtPC, message) = Data.GetList(sql, "HistoriaClinicaDAL.BuscarByEmpleadoId");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new HistoriaClinicaDTO();
                    s.Id = temp["HistoriaClinicaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["HistoriaClinicaId"]);
                    s.Descripcion = temp["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Descripcion"]);
                    s.Archivo = temp["Archivo"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Archivo"]);

                    var idt = temp["TurnoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoId"]);
                    if (idt > 0)
                    {
                        var medico = turnoDAL.Buscar(idt);
                        if (medico.result != null)
                            s.Turno = medico.result;
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

        public (bool result, string message) Guardar(HistoriaClinicaDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo HistoriaClinicaDAL.Guardar");

                var parameters = new List<string> { "'" + input.Id + "'", "'" + input.Descripcion + "'", "'" + input.Archivo + "'" };
                var classKeys = Data.GetObjectKeys(new HistoriaClinica()).Where(x => x != "HistoriaClinicaId").ToList();
                var sql = Data.InsertExpression("HistoriaClinica", classKeys, parameters);
                var (response, message) = Data.CrudAction(sql, "HistoriaClinicaDAL.Guardar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo HistoriaClinicaDAL.Guardar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Editar(HistoriaClinicaDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo HistoriaClinicaDAL.Editar");

                var parameters = new List<string> { "'" + input.Id + "'", "'" + input.Descripcion + "'", "'" + input.Archivo + "'" };
                var classKeys = Data.GetObjectKeys(new HistoriaClinica()).Where(x => x != "HistoriaClinicaId").ToList();
                var sql = Data.UpdateExpression("HistoriaClinica", classKeys, parameters, " WHERE HistoriaClinicaId = '" + input.Id + "'");
                var (response, message) = Data.CrudAction(sql, "HistoriaClinicaDAL.Editar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo HistoriaClinicaDAL.Editar \n" + ex.Message.ToString());
            }
        }
    }
}
