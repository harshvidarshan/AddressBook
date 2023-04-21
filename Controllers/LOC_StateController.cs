using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using WebAppTABLE.DAL;
using System.Data.SqlTypes;

using WebAppTABLE.Models;

namespace WebAppTABLE.Controllers
{
    public class LOC_StateController : Controller
    {
        public IConfiguration Configuration;

        public LOC_StateController(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
        }


        #region Search
        public IActionResult Search()
        {
            string str = Configuration.GetConnectionString("mystrings");
            SqlConnection conn = new SqlConnection(str);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_LOC_State_SelectAll";
            cmd.Parameters.AddWithValue("@StateName", Convert.ToString(HttpContext.Request.Form["StateName"]));
            cmd.Parameters.AddWithValue("@StateCode", Convert.ToString(HttpContext.Request.Form["StateCode"]));

            DataTable dt = new DataTable();
            SqlDataReader objSDR = cmd.ExecuteReader();
            dt.Load(objSDR);

            return View("LOC_StateList", dt);
        }
        #endregion

        #region SelectALL
        public IActionResult State()
        {
            string str = this.Configuration.GetConnectionString("mystrings");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_State_SelectAll(str);
            return View("LOC_StateList", dt);
        }
        #endregion

        #region DeleteState
        public IActionResult DeleteState(int StateId)
        {
            string str = this.Configuration.GetConnectionString("mystrings");
            LOC_DAL dalLOC = new LOC_DAL();
            dalLOC.PR_LOC_State_DeleteByPK(str, StateId);
            return RedirectToAction("State");
        }
        #endregion

        #region Add
        public IActionResult Add(int? StateId)
        {
            #region Combobox
            string str = this.Configuration.GetConnectionString("mystrings");
            SqlConnection con1 = new SqlConnection(str);
            DataTable dt1 = new DataTable();
            con1.Open();
            SqlCommand cmd1 = con1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_Country_SelectForDropDown";
            SqlDataReader sdr1 = cmd1.ExecuteReader();
            dt1.Load(sdr1);
            con1.Close();
            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CountryDropDownModel vlst = new LOC_CountryDropDownModel();
                vlst.CountryId = Convert.ToInt32(dr["CountryId"]);
                vlst.CountryName = (dr["CountryName"]).ToString();
                list.Add(vlst);
            }
            ViewBag.CountryList = list;
            #endregion

            #region Record Select by PK
            {
                if (StateId != null)
                {
                    string str1 = this.Configuration.GetConnectionString("mystrings");

                    SqlConnection con = new SqlConnection(str1);
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StateId", StateId);
                    cmd.CommandText = "PR_LOC_State_SelectByPK";
                    DataTable dt = new DataTable();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    if (dt.Rows.Count > 0)
                    {
                        LOC_StateModel modelLOC_State = new LOC_StateModel();
                        foreach (DataRow dr in dt.Rows)
                        {
                            modelLOC_State.StateId = Convert.ToInt32(dr["StateId"]);
                            modelLOC_State.StateName = dr["StateName"].ToString();
                            modelLOC_State.StateCode = dr["StateCode"].ToString();
                            modelLOC_State.PhotoPath = dr["PhotoPath"].ToString();
                            modelLOC_State.CountryId = Convert.ToInt32(dr["CountryId"]);
                            modelLOC_State.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                            modelLOC_State.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                        }
                        return View("LOC_StateAddEdit", modelLOC_State);
                    }
                }
                return View("LOC_StateAddEdit");
            }
            #endregion
        }

        #endregion

        #region Save
        public IActionResult Save(LOC_StateModel modelLOC_State)
        {
            if (modelLOC_State.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string FileNameWithPath = Path.Combine(path, modelLOC_State.File.FileName);
                modelLOC_State.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelLOC_State.File.FileName;

                using (var stream = new FileStream(FileNameWithPath, FileMode.Create))
                {
                    modelLOC_State.File.CopyTo(stream);
                }
            }

            //connecting database
            string str = this.Configuration.GetConnectionString("mystrings");
            LOC_DAL dalLOC = new LOC_DAL();
            if (modelLOC_State.StateId == 0)
            {
                dalLOC.PR_LOC_State_Insert(str, modelLOC_State.CountryId, modelLOC_State.StateName, modelLOC_State.StateCode, modelLOC_State.PhotoPath, modelLOC_State.CreationDate, modelLOC_State.ModificationDate);
            }
            else
            {
                dalLOC.PR_LOC_State_UpdateByPK(str, modelLOC_State.CountryId, modelLOC_State.StateId, modelLOC_State.StateName, modelLOC_State.StateCode, modelLOC_State.PhotoPath, modelLOC_State.ModificationDate);

            }
            return RedirectToAction("State");
        }
        #endregion
    }
}
