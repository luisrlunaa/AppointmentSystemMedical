using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class EspecialidadDAL
    {
        DataManager Data = new DataManager();
        public (List<EspecialidadDTO> result, string message) Buscar()
        {
            List<EspecialidadDTO> res = new List<EspecialidadDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Especialidad());
                var sql = Data.SelectExpression("Especialidad", classKeys);
                var (dtPC, message) = Data.GetList(sql, "EspecialidadDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Id"]);
                    var desc = temp["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Descripcion"]);
                    res.Add(new EspecialidadDTO(id, desc));
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo EspecialidadDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (EspecialidadDTO result, string message) Buscar(int id)
        {
            var s = new EspecialidadDTO();
            try
            {
                if (id <= 0)
                    return (s, "Error Input Invalido, Metodo EspecialidadDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new TurnoEstado());
                var sql = Data.SelectExpression("Especialidad", classKeys, WhereExpresion: " WHERE Id ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "EspecialidadDAL.BuscarById");
                if (dr is null)
                    return (s, message1);


                s.Id = dr["Id"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("Id")) : 0;
                s.Descripcion = dr["Descripcion"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo EspecialidadDAL.BuscarById \n" + ex.Message.ToString());
            }
        }

        public (List<EspecialidadDTO> result, string message) Buscar(string apenom)
        {
            List<EspecialidadDTO> res = new List<EspecialidadDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Especialidad());
                var sql = Data.SelectExpression("Especialidad", classKeys, WhereExpresion: "WHERE Especialidad.Descripcion Like '" + apenom + "'");
                var (dtPC, message) = Data.GetList(sql, "EspecialidadDAL.BuscarByDescription");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Id"]);
                    var desc = temp["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Descripcion"]);
                    res.Add(new EspecialidadDTO(id, desc));
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo EspecialidadDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Guardar(EspecialidadDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo EspecialidadDAL.Guardar");

                var parameters = new List<string> { "'" + input.Descripcion + "'" };
                var classKeys = Data.GetObjectKeys(new Especialidad()).Where(x => x != "Id").ToList();
                var sql = Data.InsertExpression("Especialidad", classKeys, parameters);
                var (response, message) = Data.CrudAction(sql, "EspecialidadDAL.Guardar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo EspecialidadDAL.Guardar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Editar(EspecialidadDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo EspecialidadDAL.Editar");

                var parameters = new List<string> { "'" + input.Descripcion + "'" };
                var classKeys = Data.GetObjectKeys(new Especialidad()).Where(x => x != "Id").ToList();
                var sql = Data.UpdateExpression("Especialidad", classKeys, parameters, " WHERE Id = '" + input.Id + "'");
                var (response, message) = Data.CrudAction(sql, "EspecialidadDAL.Editar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo EspecialidadDAL.Editar \n" + ex.Message.ToString());
            }
        }
    }
}
