using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AppointmentSystemMedical.CapaDatos
{
    public class TipoUsuarioDAL
    {
        DataManager Data = new DataManager();
        public (List<TipoUsuarioDTO> result, string message) Buscar()
        {
            List<TipoUsuarioDTO> res = new List<TipoUsuarioDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new TipoUsuario());
                var sql = Data.SelectExpression("TipoUsuario", classKeys);
                var (dtPC, message) = Data.GetList(sql, "TipoUsuarioDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Id"]);
                    var desc = temp["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Descripcion"]);
                    res.Add(new TipoUsuarioDTO(id, desc));
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TipoUsuarioDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (TipoUsuarioDTO result, string message) Buscar(int id)
        {
            var s = new TipoUsuarioDTO(0, string.Empty);
            try
            {
                if (id <= 0)
                    return (s, "Error Input Invalido, Metodo TipoUsuarioDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new TipoUsuario());
                var sql = Data.SelectExpression("TipoUsuario", classKeys, WhereExpresion: " WHERE Id ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "TipoUsuarioDAL.BuscarById");
                if (dr is null)
                    return (s, message1);


                s.Id = dr["Id"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("Id")) : 0;
                s.Descripcion = dr["Descripcion"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo TipoUsuarioDAL.BuscarById \n" + ex.Message.ToString());
            }
        }
    }
}
