using AppointmentSystemMedical.Infrastucture.Contexts;
using AppointmentSystemMedical.Model.DTOs;
using AppointmentSystemMedical.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
                var classKeys = Data.GetObjectKeys(new TurnoEstado()).Where(x => x != "Turno").ToList();
                var sql = Data.SelectExpression("TurnoEstado", classKeys);
                var (dtPC, message) = Data.GetList(sql, "TurnoEstadoDAL.Buscar");
                if (dtPC is null || dtPC.Rows is null || dtPC.Rows.Count == 0)
                    return (res, message);

                foreach (DataRow temp in dtPC.Rows)
                {
                    var id = temp["TurnoEstadoId"] == DBNull.Value ? 0 : Convert.ToInt32(temp["TurnoEstadoId"]);
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
            var s = new TurnoEstadoDTO();
            try
            {
                if (id <= 0)
                    return (s, "Error Input Invalido, Metodo TurnoEstadoDAL.BuscarById");

                var classKeys = Data.GetObjectKeys(new TurnoEstado()).Where(x => x != "Turno").ToList();
                var sql = Data.SelectExpression("TurnoEstado", classKeys, WhereExpresion: " WHERE TurnoEstadoId ='" + id + "'");
                var (dr, message1) = Data.GetOne(sql, "TurnoEstadoDAL.BuscarById");
                if (dr is null)
                    return (s, message1);

                s.Id = dr["TurnoEstadoId"].GetType() != typeof(DBNull) ? dr.GetInt32(dr.GetOrdinal("TurnoEstadoId")) : 0;
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
