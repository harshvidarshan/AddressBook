using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Data;

namespace WebAppTABLE.DAL
{
    public abstract class CON_DALBase
    {
        #region dbo.PR_MST_ContactCategory_SelectAll
        public DataTable PR_MST_ContactCategory_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(conn);
                DbCommand dbcmd = sqldb.GetStoredProcCommand("PR_MST_ContactCategory_SelectAll");
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
        #region  PR_MST_ContactCategory_Insert
        public decimal? PR_MST_ContactCategory_Insert(string con, string ContactCategoryName, string ContactCategoryCode, string PhotoPath, DateTime? CreationDate, DateTime? ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(con);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_Insert");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.VarChar, ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryCode", SqlDbType.VarChar, ContactCategoryCode);
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

        #region  PR_MST_ContactCategory_UpdateByPK
        public bool? PR_MST_ContactCategory_UpdateByPK(string con, int? ContactCategoryId, string ContactCategoryName, string ContactCategoryCode, string PhotoPath, DateTime? ModificationDate)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(con);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_UpdateByPK");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryId", SqlDbType.Int, ContactCategoryId);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryName", SqlDbType.VarChar, ContactCategoryName);
                sqlDB.AddInParameter(dbCMD, "ContactCategoryCode", SqlDbType.VarChar, ContactCategoryCode);
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

        #region Method: PR_MST_ContactCategory_DeleteByPK
        public bool? PR_MST_ContactCategory_DeleteByPK(string conn, int? ContactCategoryId)
        {
            try
            {
                SqlDatabase sqlDB = new SqlDatabase(conn);
                DbCommand dbCMD = sqlDB.GetStoredProcCommand("PR_MST_ContactCategory_DeleteByPK");
                sqlDB.AddInParameter(dbCMD, "ContactCategoryId", SqlDbType.Int, ContactCategoryId);

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

        #region dbo.PR_CON_Contact_SelectAll
        public DataTable PR_CON_Contact_SelectAll(string conn)
        {
            try
            {
                SqlDatabase sqldb = new SqlDatabase(conn);
                DbCommand dbcmd = sqldb.GetStoredProcCommand("PR_CON_Contact_SelectAll");
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
    }
}
