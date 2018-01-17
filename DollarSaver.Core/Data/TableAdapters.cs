using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace DollarSaver.Core.Data.DollarSaverDBTableAdapters {

    partial class CertificateTableAdapter {

        public ArrayList GetDealDates(int stationId) {

            ArrayList dates = new ArrayList();

            SqlCommand cmd = this.Connection.CreateCommand();

            if (cmd.Connection.State != ConnectionState.Open) {
                cmd.Connection.Open();
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ds_user.sp_Certificate_load_deal_dates";

            cmd.Parameters.Add("@StationId", SqlDbType.Int).Value = stationId;


            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter();

            try {
                da.SelectCommand = cmd;
                da.Fill(dt);
            } catch (Exception ex) {

                throw new Exception("Run query error: " + cmd.CommandText + " [" + ex.Message + "]");
            } finally {
                if (cmd.Connection != null) {
                    if (cmd.Connection.State == ConnectionState.Open) {
                        cmd.Connection.Close();
                    }
                    cmd.Connection.Dispose();
                }
            }

            foreach (DataRow row in dt.Rows) {
                dates.Add(Convert.ToDateTime(row[0]));
            }

            return dates;
        }
    }


    partial class CertificateNumberTableAdapter {

        public ArrayList GetDates(int certificateId) {

            ArrayList dates = new ArrayList();

            SqlCommand cmd = this.Connection.CreateCommand();

            if (cmd.Connection.State != ConnectionState.Open) {
                cmd.Connection.Open();
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "ds_user.sp_CertificateNumber_load_dates";

            cmd.Parameters.Add("@CertificateId", SqlDbType.Int).Value = certificateId;


            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter();

            try {
                da.SelectCommand = cmd;
                da.Fill(dt);
            } catch (Exception ex) {

                throw new Exception("Run query error: " + cmd.CommandText + " [" + ex.Message + "]");
            } finally {
                if (cmd.Connection != null) {
                    if (cmd.Connection.State == ConnectionState.Open) {
                        cmd.Connection.Close();
                    }
                    cmd.Connection.Dispose();
                }
            }

            foreach (DataRow row in dt.Rows) {
                dates.Add(Convert.ToDateTime(row[0]));
            }

            return dates;
        }
    }

}

