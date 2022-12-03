using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class CoberturaDAL
    {
        DataManager Data = new DataManager();
        public (List<CoberturaDTO> result, string message) Buscar()
        {
            List<CoberturaDTO> res = new List<CoberturaDTO>();
            try
            {
                var join = Data.JoinExpression("INNER", new List<string>() { "Cobertura" }, new List<string>() { "ObraSocial" }, new List<string>() { "ObraSocialId" });
                var classKeys = Data.GetObjectKeys(new Cobertura());
                var sql = Data.SelectExpression("Cobertura", classKeys, JoinExp: join);
                var (dtPC, message) = Data.GetList(sql, "CoberturaDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    res.Add(new CoberturaDTO(
                                temp["Cobertura.Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Cobertura.Id"]),
                                new ObraSocialDTO(
                                    temp["ObraSocial.Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["ObraSocial.Id"]),
                                    temp["ObraSocial.Nombre"] == DBNull.Value ? string.Empty : Convert.ToString(temp["ObraSocial.Nombre"]),
                                    temp["ObraSocial.Estado"] == DBNull.Value ? false : Convert.ToBoolean(temp["ObraSocial.Estado"])),
                                temp["Cobertura.Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Cobertura.Descripcion"]),
                                temp["Cobertura.Estado"] == DBNull.Value ? false : Convert.ToBoolean(temp["Cobertura.Estado"])));
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo CoberturaDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (CoberturaDTO result, string message) Buscar(int id)
        {
            var s = new CoberturaDTO();
            try
            {
                if (id <= 0)
                    return (s, "Error Input Invalido, Metodo CoberturaDAL.BuscarById");

                var join = Data.JoinExpression("INNER", new List<string>() { "Cobertura" }, new List<string>() { "ObraSocial" }, new List<string>() { "ObraSocialId" });
                var classKeys = Data.GetObjectKeys(new Cobertura());
                var sql = Data.SelectExpression("Cobertura", classKeys, WhereExpresion: " WHERE Id ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "CoberturaDAL.BuscarById");
                if (dr is null)
                    return (s, message1);

                s.Id = dr["Cobertura.Id"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("Cobertura.Id")) : 0;
                s.ObraSocial = new ObraSocialDTO(
                       dr.GetInt32(dr.GetOrdinal("ObraSocial.Id")) == 0 ? 0 : dr.GetInt32(dr.GetOrdinal("ObraSocial.Id")),
                       dr.GetString(dr.GetOrdinal("ObraSocial.Nombre")),
                       dr.GetBoolean(dr.GetOrdinal("ObraSocial.Estado")));
                s.Descripcion = dr["Cobertura.Descripcion"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Cobertura.Descripcion")) : string.Empty;
                s.Estado = dr["Cobertura.Estado"].GetType() != typeof(DBNull) ? dr.GetBoolean(dr.GetOrdinal("Cobertura.Estado")) : false;

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo CoberturaDAL.BuscarById \n" + ex.Message.ToString());
            }
        }

        public (List<CoberturaDTO> result, string message) Buscar(string apenom)
        {
            List<CoberturaDTO> res = new List<CoberturaDTO>();
            try
            {
                var join = Data.JoinExpression("INNER", new List<string>() { "Cobertura" }, new List<string>() { "ObraSocial" }, new List<string>() { "ObraSocialId" });
                var classKeys = Data.GetObjectKeys(new Cobertura());
                var sql = Data.SelectExpression("Cobertura", classKeys, JoinExp: join, WhereExpresion: "Where ObraSocial.Nombre Like '" + apenom + "' AND Cobertura.Descripcion Like '" + apenom + "'");
                var (dtPC, message) = Data.GetList(sql, "CoberturaDAL.BuscarByDescripcion");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    res.Add(new CoberturaDTO(
                                temp["Cobertura.Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Cobertura.Id"]),
                                new ObraSocialDTO(
                                    temp["ObraSocial.Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["ObraSocial.Id"]),
                                    temp["ObraSocial.Nombre"] == DBNull.Value ? string.Empty : Convert.ToString(temp["ObraSocial.Nombre"]),
                                    temp["ObraSocial.Estado"] == DBNull.Value ? false : Convert.ToBoolean(temp["ObraSocial.Estado"])),
                                temp["Cobertura.Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Cobertura.Descripcion"]),
                                temp["Cobertura.Estado"] == DBNull.Value ? false : Convert.ToBoolean(temp["Cobertura.Estado"])));
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo CoberturaDAL.BuscarByDescripcion \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Guardar(CoberturaDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo CoberturaDAL.Guardar");

                var parameters = new List<string> { "'" + input.ObraSocial.Id + "'", "'" + input.Descripcion + "'", "'" + input.Estado + "'" };
                var classKeys = Data.GetObjectKeys(new Cobertura()).Where(x => x != "Id").ToList();
                var sql = Data.InsertExpression("Cobertura", classKeys, parameters);
                var (response, message) = Data.CrudAction(sql, "CoberturaDAL.Guardar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo CoberturaDAL.Guardar \n" + ex.Message.ToString());
            }
        }

        public (bool result, string message) Editar(CoberturaDTO input)
        {
            try
            {
                if (input == null || input.Id == 0)
                    return (false, "Error Input Invalido, Metodo CoberturaDAL.Editar");

                var parameters = new List<string> { "'" + input.Descripcion + "'", "'" + input.Estado + "'" };
                var classKeys = Data.GetObjectKeys(new Cobertura()).Where(x => x != "Id" && x != "ObraSocialId").ToList();
                var sql = Data.UpdateExpression("Cobertura", classKeys, parameters, " WHERE Id = '" + input.ObraSocial.Id + "'");
                var (response, message) = Data.CrudAction(sql, "CoberturaDAL.Editar");
                if (!response)
                    return (response, message);

                return (response, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (false, "Error al Cargar Data, Metodo CoberturaDAL.Editar \n" + ex.Message.ToString());
            }
        }
    }
}
