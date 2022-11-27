using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;

namespace AppointmentSystemMedical.CapaDatos
{
    public class TurnoEstadoDAL
    {
        DataManager Data = new DataManager();
        public (List<TurnoEstadoDTO> result, string message) Buscar()
        {
            List<TurnoEstadoDTO> res = new List<TurnoEstadoDTO>();
            try
            {
                var classKeys = Data.GetObjectKeys(new TurnoEstado());
                var sql = Data.SelectExpression("TurnoEstado", classKeys);
                var (dtPC, message) = Data.GetList(sql, "TurnoEstadoDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["Id"] == DBNull.Value ? 0 : Convert.ToInt32(temp["Id"]);
                    var desc = temp["Descripcion"] == DBNull.Value ? string.Empty : Convert.ToString(temp["Descripcion"]);
                    res.Add(new TurnoEstadoDTO(id, desc));
                }

                return (res, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (res, "Error al Cargar Data, Metodo TurnoEstadoDAL.Buscar \n" + ex.Message.ToString());
            }
        }

        public (TurnoEstadoDTO result, string message) Buscar(int id)
        {
            var s = new TurnoEstadoDTO(0, string.Empty);
            try
            {
                if (id <= 0)
                    return (s, "Error Input Invalido, Metodo TurnoEstadoDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new TurnoEstado());
                var sql = Data.SelectExpression("TurnoEstado", classKeys, WhereExpresion: " WHERE Id ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "TurnoEstadoDAL.BuscarById");
                if (dr is null)
                    return (s, message1);


                s.Id = dr["Id"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("Id")) : 0;
                s.Descripcion = dr["Descripcion"].GetType() != typeof(DBNull) ? dr.GetString(dr.GetOrdinal("Descripcion")) : string.Empty;

                return (s, "Proceso Completado");
            }
            catch (Exception ex)
            {
                return (s, "Error al Cargar Data, Metodo TurnoEstadoDAL.BuscarById \n" + ex.Message.ToString());
            }
        }
    }
}
