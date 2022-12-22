using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class ObraSocialDAL
    {
        DataManager Data = new DataManager();
        public (List<ObraSocialDTO> result, string message) Buscar()
        {
            List<ObraSocialDTO> res = new List<ObraSocialDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new ObraSocial()).Where(x => x != "Cobertura").ToList(); ;
                var sql = Data.SelectExpression("ObraSocial", classKeys);
                var (dtPC, message) = Data.GetList(sql, "ObraSocialDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["ObraSocialId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["ObraSocialId"]);
                    var desc = temp["Nombre"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Nombre"]);
                    var status = temp["Estado"] == DBNull.Value ? false : Convert.ToBoolean(temp["Estado"]);
                    res.Add(new ObraSocialDTO(id, desc, status));
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo ObraSocialDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (ObraSocialDTO result, string message) Buscar(int id)
        {
            var s = new ObraSocialDTO();
            try
            {
                if (id <= 0)
                    return (s, "Error Input Invalido, Metodo ObraSocialDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new ObraSocial()).Where(x => x != "Cobertura").ToList();
                var sql = Data.SelectExpression("ObraSocial", classKeys, WhereExpresion: " WHERE ObraSocialId ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "ObraSocialDAL.BuscarById");
                if (dr is null)
                    return (s, message1);


                s.Id = dr["ObraSocialId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("ObraSocialId")) : 0;
                s.Nombre = dr["Nombre"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Nombre")) : string.Empty;
                s.Estado = dr["Estado"].GetType() != typeof(DBNull) ? dr.GetBoolean(dr.GetOrdinal("Estado")) : false;

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo ObraSocialDAL.BuscarById \n" + ex.Message.ToString());
            }
        }

        public (List<ObraSocialDTO> result, string message) Buscar(string apenom)
        {
            List<ObraSocialDTO> res = new List<ObraSocialDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new ObraSocial()).Where(x => x != "Cobertura").ToList();
                var sql = Data.SelectExpression("ObraSocial", classKeys, WhereExpresion: "Where ObraSocial.Nombre Like '" + apenom + "'");
                var (dtPC, message) = Data.GetList(sql, "ObraSocialDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["ObraSocialId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["ObraSocialId"]);
                    var desc = temp["Nombre"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Nombre"]);
                    var status = temp["Estado"] == DBNull.Value ? false : Convert.ToBoolean(temp["Estado"]);
                    res.Add(new ObraSocialDTO(id, desc, status));
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo ObraSocialDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Guardar(ObraSocialDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo ObraSocialDAL.Guardar");

                var parameters = new List<string> { "'" + input.Nombre + "'", "'" + input.Estado + "'" };
                var classKeys = Data.GetObjectKeys(new ObraSocial()).Where(x => x != "ObraSocialId" && x != "Cobertura").ToList();
                var sql = Data.InsertExpression("ObraSocial", classKeys, parameters);
                var (response, message) = Data.CrudAction(sql, "ObraSocialDAL.Guardar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo ObraSocialDAL.Guardar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Editar(ObraSocialDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo ObraSocialDAL.Editar");

                var parameters = new List<string> { "'" + input.Nombre + "'", "'" + input.Estado + "'" };
                var classKeys = Data.GetObjectKeys(new ObraSocial()).Where(x => x != "ObraSocialId" && x != "Cobertura").ToList();
                var sql = Data.UpdateExpression("ObraSocial", classKeys, parameters, " WHERE ObraSocialId = '" + input.Id + "'");
                var (response, message) = Data.CrudAction(sql, "ObraSocialDAL.Editar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo ObraSocialDAL.Editar \n" + ex.Message.ToString());
            }
        }
    }
}
