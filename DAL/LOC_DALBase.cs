using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.Common;

namespace WebAppTABLE.DAL
{
    public abstract class LOC_DALBase
    {
        #region dbo.PR_LOC_State_SelectAll
        public DataTable PR_LOC_State_SelectAll(string conn )
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(conn);
                DbCommand dbcmd = sqldb.GetStoredProcCommand("PR_LOC_State_SelectAll");
                DataTable dt = new DataTable();
                using (IDataReader dr = sqldb.ExecuteReader(dbcmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  PR_LOC_State_Insert
        public decimal? PR_LOC_State_Insert(string con, int? CountryId, string StateName, string StateCode, string PhotoPath, DateTime? CreationDate, DateTime? ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(con);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryId", SqlDbType.Int, CountryId);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.VarChar, StateCode);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, PhotoPath);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.DateTime, CreationDate);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, ModificationDate);
                var vResult = sqlDB.ExecuteScalar(dbCMD);
                if (vResult == null)
                    return null;

                return (decimal)Convert.ChangeType(vResult, vResult.GetType());

            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
        #endregion

        #region Method: PR_LOC_State_DeleteByPK
        public bool? PR_LOC_State_DeleteByPK(string conn, int? StateId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "StateId", SqlDbType.Int, StateId);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
        #endregion

        #region  PR_LOC_State_UpdateByPK
        public bool? PR_LOC_State_UpdateByPK(string con,int?CountryId, int? StateId, string StateName, string StateCode, string PhotoPath, DateTime? ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(con);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "CountryId", SqlDbType.Int, CountryId);
                sqlDB.AddInParameter(dbCMD, "StateId", SqlDbType.Int, StateId);
                sqlDB.AddInParameter(dbCMD, "StateName", SqlDbType.VarChar, StateName);
                sqlDB.AddInParameter(dbCMD, "StateCode", SqlDbType.VarChar, StateCode);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, PhotoPath);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, ModificationDate);


                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
        #endregion

        #region Method: dbo_PR_LOC_State_SelectbyDropdown
        public DataTable dbo_PR_LOC_State_SelectByDropdownList(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_State_SelectForDropDown");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region dbo.PR_LOC_Country_SelectAll
        public DataTable PR_LOC_Country_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(conn);
                DbCommand dbcmd = sqldb.GetStoredProcCommand("PR_LOC_Country_SelectAll");
                DataTable dt = new DataTable();
                using (IDataReader dr = sqldb.ExecuteReader(dbcmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  PR_LOC_Country_Insert
        public decimal? PR_LOC_Country_Insert(string con, string CountryName, string CountryCode, string PhotoPath, DateTime? CreationDate, DateTime? ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(con);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.VarChar, CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.VarChar, CountryCode);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, PhotoPath);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.DateTime, CreationDate);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, ModificationDate);
                var vResult = sqlDB.ExecuteScalar(dbCMD);
                if (vResult == null)
                    return null;

                return (decimal)Convert.ChangeType(vResult, vResult.GetType());

            }
            catch (Exception ex)
            {
                
                return null;
            }
        }
        #endregion

        #region  PR_LOC_Country_UpdateByPK
        public bool? PR_LOC_Country_UpdateByPK(string con, int? CountryId, string CountryName, string CountryCode, string PhotoPath, DateTime? ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(con);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "CountryId", SqlDbType.Int, CountryId);
                sqlDB.AddInParameter(dbCMD, "CountryName", SqlDbType.VarChar, CountryName);
                sqlDB.AddInParameter(dbCMD, "CountryCode", SqlDbType.VarChar, CountryCode);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, PhotoPath);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, ModificationDate);
           

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
        #endregion

        #region Method:PR_LOC_Country_SelectByPK

        #endregion

        #region Method: PR_LOC_Country_DeleteByPK
        public bool? PR_LOC_Country_DeleteByPK(string conn, int? CountryId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_Country_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CountryId", SqlDbType.Int, CountryId);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }
        #endregion


        #region Method: dbo_PR_LOC_Country_SelectbyDropdown
        public DataTable dbo_PR_LOC_Country_SelectByDropdownList(string conn)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("dbo.PR_LOC_Country_SelectComboBox");

                DataTable dt = new DataTable();
                using (IDataReader dr = sqlDB.ExecuteReader(dbCMD))
                {
                    dt.Load(dr);
                }

                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region dbo.PR_LOC_City_SelectAll
        public DataTable PR_LOC_City_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(conn);
                DbCommand dbcmd = sqldb.GetStoredProcCommand("PR_LOC_City_SelectAll");
                DataTable dt = new DataTable();
                using (IDataReader dr = sqldb.ExecuteReader(dbcmd))
                {
                    dt.Load(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region  PR_LOC_City_Insert
        public decimal? PR_LOC_City_Insert(string con, int?CountryId, int?StateId, string CityName, string CityCode, string PhotoPath, DateTime? CreationDate, DateTime? ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(con);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_Insert");
                sqlDB.AddInParameter(dbCMD, "CountryId", SqlDbType.Int, CountryId);
                sqlDB.AddInParameter(dbCMD, "StateId", SqlDbType.Int, StateId);
           
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.VarChar, CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.VarChar, CityCode);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, PhotoPath);
                sqlDB.AddInParameter(dbCMD, "CreationDate", SqlDbType.DateTime, CreationDate);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, ModificationDate);
                var vResult = sqlDB.ExecuteScalar(dbCMD);
                if (vResult == null)
                    return null;

                return (decimal)Convert.ChangeType(vResult, vResult.GetType());

            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
        #endregion

        #region Method: PR_LOC_City_DeleteByPK
        public bool? PR_LOC_City_DeleteByPK(string conn, int? CityId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "CityId", SqlDbType.Int, CityId);

                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);
            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
        #endregion

        #region  PR_LOC_City_UpdateByPK
        public bool? PR_LOC_City_UpdateByPK(string con, int? CountryId, int? StateId, int? CityId, string CityName, string CityCode, string PhotoPath, DateTime? ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(con);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_LOC_City_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "CountryId", SqlDbType.Int, CountryId);
                sqlDB.AddInParameter(dbCMD, "StateId", SqlDbType.Int, StateId);
                sqlDB.AddInParameter(dbCMD, "CityId", SqlDbType.Int, CityId);
                sqlDB.AddInParameter(dbCMD, "CityName", SqlDbType.VarChar, CityName);
                sqlDB.AddInParameter(dbCMD, "CityCode", SqlDbType.VarChar, CityCode);
                sqlDB.AddInParameter(dbCMD, "PhotoPath", SqlDbType.VarChar, PhotoPath);
                sqlDB.AddInParameter(dbCMD, "ModificationDate", SqlDbType.DateTime, ModificationDate);
                int vReturnValue = sqlDB.ExecuteNonQuery(dbCMD);
                return (vReturnValue == -1 ? false : true);

            }
            catch (Exception ex)
            {
                throw ex;
                return null;
            }
        }
        #endregion
    }
}
