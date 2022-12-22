using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class MedicoDAL
    {
        DataManager Data = new DataManager();
        EmpleadoDAL empleadoDAL = new EmpleadoDAL();
        EspecialidadDAL especialidadDAL = new EspecialidadDAL();

        public (List<MedicoDTO> result, string message) Buscar()
        {
            List<MedicoDTO> res = new List<MedicoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Medico()).Where(x => x != "Empleado" && x != "Especialidad" && x != "Turno").ToList();
                var sql = Data.SelectExpression("Medico", classKeys);
                var (dtPC, message) = Data.GetList(sql, "MedicoDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new MedicoDTO();
                    var idEmpleado = temp["EmpleadoId"] != DBNull.Value ? Convert.ToInt32(temp["EmpleadoId"]) : 0;
                    var idEspecialidad = temp["EspecialidadId"] != DBNull.Value ? Convert.ToInt32(temp["EspecialidadId"]) : 0;

                    s.Id = temp["MedicoId"] != DBNull.Value ? Convert.ToInt32(temp["MedicoId"]) : 0;
                    s.Matricula = temp["Matricula"] != DBNull.Value ? Convert.ToString(temp["Matricula"]) : string.Empty; 
                    
                    if (idEmpleado > 0)
                    {
                        var empleado = empleadoDAL.Buscar(idEmpleado);
                        if (empleado.result != null)
                            s.Empleado = empleado.result;
                    }

                    if (idEspecialidad > 0)
                    {
                        var especialidad = especialidadDAL.Buscar(idEspecialidad);
                        if (especialidad.result != null)
                            s.Especialidad = especialidad.result;
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

        public (MedicoDTO result, string message) Buscar(int id)
        {
            var s = new MedicoDTO();
            try
            {
                if (id == 0)
                    return (s, "Error Input Invalido, Metodo MedicoDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new Medico()).Where(x => x != "Empleado" && x != "Especialidad" && x != "Turno").ToList();
                var sql = Data.SelectExpression("Medico", classKeys, WhereExpresion: " WHERE MedicoId ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "MedicoDAL.BuscarById");
                if (dr is null)
                    return (s, message1);

                var idEmpleado = dr["EmpleadoId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("EmpleadoId")) : 0;
                var idEspecialidad = dr["EspecialidadId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("EspecialidadId")) : 0;

                s.Id = dr["MedicoId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("MedicoId")) : 0;
                s.Matricula = dr["Matricula"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Matricula")) : string.Empty;
                if (idEmpleado > 0)
                {
                    var empleado = empleadoDAL.Buscar(idEmpleado);
                    if (empleado.result != null)
                        s.Empleado = empleado.result;
                }

                if (idEspecialidad > 0)
                {
                    var especialidad = especialidadDAL.Buscar(idEspecialidad);
                    if (especialidad.result != null)
                        s.Especialidad = especialidad.result;
                }

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo MedicoDAL.BuscarById \n" + ex.Message.ToString());
            }
        }

        public (List<MedicoDTO> result, string message) BuscarDni(string dni)
        {
            List<MedicoDTO> res = new List<MedicoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Medico()).Where(x => x != "Empleado" && x != "Especialidad" && x != "Turno").ToList();
                var join = Data.JoinExpression("INNER", new List<string>() { "Medico", "Empleado" }, new List<string>() { "Empleado", "Persona" }, new List<string>() { "MedicoId", "PersonaId" });
                var sql = Data.SelectExpression("Medico", classKeys, JoinExp: join, WhereExpresion: "Where Persona.Dni LIKE '%" + dni + "%'");
                var (dtPC, message) = Data.GetList(sql, "MedicoDAL.BuscarApeNom");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new MedicoDTO();
                    var idEmpleado = temp["EmpleadoId"] != DBNull.Value ? Convert.ToInt32(temp["EmpleadoId"]) : 0;
                    var idEspecialidad = temp["EspecialidadId"] != DBNull.Value ? Convert.ToInt32(temp["EspecialidadId"]) : 0;

                    s.Id = temp["MedicoId"] != DBNull.Value ? Convert.ToInt32(temp["MedicoId"]) : 0;
                    s.Matricula = temp["Matricula"] != DBNull.Value ? Convert.ToString(temp["Matricula"]) : string.Empty;
                    if (idEmpleado > 0)
                    {
                        var empleado = empleadoDAL.Buscar(idEmpleado);
                        if (empleado.result != null)
                            s.Empleado = empleado.result;
                    }

                    if (idEspecialidad > 0)
                    {
                        var especialidad = especialidadDAL.Buscar(idEspecialidad);
                        if (especialidad.result != null)
                            s.Especialidad = especialidad.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo MedicoDAL.BuscarApeNom \n" + ex.Message.ToString());
            }
        }

        public (List<MedicoDTO> result, string message) BuscarApeNom(string apenom)
        {
            List<MedicoDTO> res = new List<MedicoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Medico()).Where(x => x != "Empleado" && x != "Especialidad" && x != "Turno").ToList();
                var join = Data.JoinExpression("INNER", new List<string>() { "Medico", "Empleado" }, new List<string>() { "Empleado", "Persona" }, new List<string>() { "MedicoId", "PersonaId" });
                var sql = Data.SelectExpression("Medico", classKeys, JoinExp: join, WhereExpresion: "Where Persona.Apellidos LIKE '%" + apenom + "%' OR Persona.Nombres LIKE '%" + apenom + "%'");
                var (dtPC, message) = Data.GetList(sql, "MedicoDAL.BuscarApeNom");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new MedicoDTO();
                    var idEmpleado = temp["EmpleadoId"] != DBNull.Value ? Convert.ToInt32(temp["EmpleadoId"]) : 0;
                    var idEspecialidad = temp["EspecialidadId"] != DBNull.Value ? Convert.ToInt32(temp["EspecialidadId"]) : 0;

                    s.Id = temp["MedicoId"] != DBNull.Value ? Convert.ToInt32(temp["MedicoId"]) : 0;
                    s.Matricula = temp["Matricula"] != DBNull.Value ? Convert.ToString(temp["Matricula"]) : string.Empty;
                    if (idEmpleado > 0)
                    {
                        var empleado = empleadoDAL.Buscar(idEmpleado);
                        if (empleado.result != null)
                            s.Empleado = empleado.result;
                    }

                    if (idEspecialidad > 0)
                    {
                        var especialidad = especialidadDAL.Buscar(idEspecialidad);
                        if (especialidad.result != null)
                            s.Especialidad = especialidad.result;
                    }

                    res.Add(s);
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo MedicoDAL.BuscarApeNom \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Guardar(MedicoDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo MedicoDAL.Guardar");

                var parameters = new List<string> { "'" + input.Matricula + "'",
                    "'" + input.Especialidad.Id + "'", "'" + input.Empleado.Id + "'" };
                var classKeys = Data.GetObjectKeys(new Medico()).Where(x => x != "MedicoId" && x != "Empleado" && x != "Especialidad" && x != "Turno").ToList();
                var sql = Data.InsertExpression("Medico", classKeys, parameters);
                var (response, message) = Data.CrudAction(sql, "MedicoDAL.Guardar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo MedicoDAL.Guardar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Editar(MedicoDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo MedicoDAL.Editar");

                var parameters = new List<string> { "'" + input.Matricula + "'",
                    "'" + input.Especialidad.Id + "'", "'" + input.Empleado.Id + "'" };
                var classKeys = Data.GetObjectKeys(new Medico()).Where(x => x != "MedicoId" && x != "Empleado" && x != "Especialidad" && x != "Turno").ToList();
                var sql = Data.UpdateExpression("Medico", classKeys, parameters, " WHERE MedicoId = '" + input.Id + "'");
                var (response, message) = Data.CrudAction(sql, "MedicoDAL.Editar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo MedicoDAL.Editar \n" + ex.Message.ToString());
            }
        }
    }
}
