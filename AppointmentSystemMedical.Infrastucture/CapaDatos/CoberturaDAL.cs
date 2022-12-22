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
        ObraSocialDAL obraSocial = new ObraSocialDAL();
        public (List<CoberturaDTO> result, string message) Buscar()
        {
            List<CoberturaDTO> res = new List<CoberturaDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new Cobertura()).Where(x=>x != "ObraSocial" && x!= "Turno").ToList();
                var sql = Data.SelectExpression("Cobertura", classKeys);
                var (dtPC, message) = Data.GetList(sql, "CoberturaDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new CoberturaDTO();
                    s.Id = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    var idObraSocial = temp["ObraSocialId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["ObraSocialId"]);
                    s.Descripcion = temp["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Descripcion"]);
                    s.Estado = temp["Estado"] == DBNull.Value ? false : Convert.ToBoolean(temp["Estado"]);

                    if(idObraSocial > 0)
                    {
                       var (obra, message1) = obraSocial.Buscar(idObraSocial);
                        s.ObraSocial = obra;
                    }

                    res.Add(s);
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

                var classKeys = Data.GetObjectKeys(new Cobertura()).Where(x => x != "ObraSocial" && x != "Turno").ToList();
                var sql = Data.SelectExpression("Cobertura", classKeys, WhereExpresion: " WHERE Id ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "CoberturaDAL.BuscarById");
                if (dr is null)
                    return (s, message1);

                s.Id = dr["CoberturaId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("CoberturaId")) : 0;
                s.Descripcion = dr["Descripcion"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;
                s.Estado = dr["Estado"].GetType() != typeof(DBNull) ? dr.GetBoolean(dr.GetOrdinal("Estado")) : false;
                
                var idObraSocial = dr["ObraSocialId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("ObraSocialId")) : 0;
                if (idObraSocial > 0)
                {
                    var (obra, message) = obraSocial.Buscar(idObraSocial);
                    s.ObraSocial = obra;
                }

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
                var classKeys = Data.GetObjectKeys(new Cobertura()).Where(x => x != "ObraSocial" && x != "Turno").ToList();
                var sql = Data.SelectExpression("Cobertura", classKeys, WhereExpresion: "Where ObraSocial.Nombre Like '" + apenom + "' AND Descripcion Like '" + apenom + "'");
                var (dtPC, message) = Data.GetList(sql, "CoberturaDAL.BuscarByDescripcion");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var s = new CoberturaDTO();
                    s.Id = temp["CoberturaId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["CoberturaId"]);
                    var idObraSocial = temp["ObraSocialId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["ObraSocialId"]);
                    s.Descripcion = temp["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Descripcion"]);
                    s.Estado = temp["Estado"] == DBNull.Value ? false : Convert.ToBoolean(temp["Estado"]);

                    if (idObraSocial > 0)
                    {
                        var (obra, message1) = obraSocial.Buscar(idObraSocial);
                        s.ObraSocial = obra;
                    }

                    res.Add(s);
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
                var classKeys = Data.GetObjectKeys(new Cobertura()).Where(x => x != "Id" && x != "ObraSocial" && x != "Turno").ToList();
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
                var classKeys = Data.GetObjectKeys(new Cobertura()).Where(x => x != "Id" && x != "ObraSocialId" && x != "ObraSocial" && x != "Turno").ToList();
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
