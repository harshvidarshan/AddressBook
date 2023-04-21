using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebAppTABLE.Models;
using WebAppTABLE.DAL;

namespace WebAppTABLE.Controllers
{
    public class LOC_CityController : Controller
    {
        public IConfiguration Configuration;

        public LOC_CityController(IConfiguration _Configuration)
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
            cmd.CommandText = "PR_LOC_City_SelectAll";
            cmd.Parameters.AddWithValue("@CityName", Convert.ToString(HttpContext.Request.Form["CityName"]));
            cmd.Parameters.AddWithValue("@CityCode", Convert.ToString(HttpContext.Request.Form["CityCode"]));

            DataTable dt = new DataTable();
            SqlDataReader objSDR = cmd.ExecuteReader();
            dt.Load(objSDR);

            return View("LOC_CityList", dt);
        }
        #endregion
        #region Add
        public IActionResult Add(int? CityId)
        {
            #region Dropdown Country
            string str1 = this.Configuration.GetConnectionString("mystrings");
            SqlConnection conn1 = new SqlConnection(str1);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_Country_SelectForDropDown";

            DataTable dt1 = new DataTable();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            dt1.Load(dr1);
            // conn1.Close();

            List<LOC_CountryDropDownModel> list = new List<LOC_CountryDropDownModel>();
            foreach (DataRow dm in dt1.Rows)
            {
                LOC_CountryDropDownModel dlist = new LOC_CountryDropDownModel();
                dlist.CountryId = Convert.ToInt32(dm["CountryId"]);
                dlist.CountryName = dm["CountryName"].ToString();
                list.Add(dlist);
            }
            ViewBag.CountryList = list;
            #endregion

            #region Dropdown State

            List<LOC_StateDropDownModel> list2 = new List<LOC_StateDropDownModel>();
            ViewBag.StateList = list2;
            #endregion

            #region Record Select by PK
            {
                if (CityId != null)
                {
                    string str = this.Configuration.GetConnectionString("mystrings");

                    SqlConnection con = new SqlConnection(str);
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PR_LOC_City_SelectByPK";
                    cmd.Parameters.AddWithValue("@CityId", CityId);
                    
                    DataTable dt = new DataTable();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt.Load(sdr);
                    if (dt.Rows.Count > 0)
                    {
                        LOC_CityModel modelLOC_City = new LOC_CityModel();
                        foreach (DataRow dr in dt.Rows)
                        {

                            modelLOC_City.CityId = Convert.ToInt32(dr["CityId"]);
                            modelLOC_City.CityName = dr["CityName"].ToString();
                            modelLOC_City.CityCode = dr["CityCode"].ToString();
                            modelLOC_City.PhotoPath = dr["PhotoPath"].ToString();
                            modelLOC_City.CountryId = Convert.ToInt32(dr["CountryId"]);
                            modelLOC_City.StateId = Convert.ToInt32(dr["StateId"]);
                            modelLOC_City.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                            modelLOC_City.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                        }
                        string str8 = this.Configuration.GetConnectionString("mystrings");

                        SqlConnection conn8 = new SqlConnection(str8);
                        conn8.Open();
                        SqlCommand cmd8 = conn8.CreateCommand();
                        cmd8.CommandType = CommandType.StoredProcedure;
                        cmd8.CommandText = "PR_LOC_State_SelectComboBoxbyCountryId";
                        cmd8.Parameters.AddWithValue("@CountryId", modelLOC_City.CountryId);
                        DataTable dt2 = new DataTable();
                        SqlDataReader dr2 = cmd8.ExecuteReader();
                        dt2.Load(dr2);
                        //cmd2.Parameters.AddWithValue("@CountryId", CountryId);
                        conn8.Close();

                        List<LOC_StateDropDownModel> list8 = new List<LOC_StateDropDownModel>();
                        foreach (DataRow dm in dt2.Rows)
                        {
                            LOC_StateDropDownModel dlist = new LOC_StateDropDownModel();
                            dlist.StateId = Convert.ToInt32(dm["StateId"]);
                            dlist.StateName = dm["StateName"].ToString();
                            list8.Add(dlist);
                        }
                        ViewBag.StateList = list8;
                        return View("LOC_CityAddEdit", modelLOC_City);
                    }
                }
                return View("LOC_CityAddEdit");
            }
            #endregion
        }
        #endregion

        #region Select ALL
        public IActionResult City()
        {
            string str = this.Configuration.GetConnectionString("mystrings");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_City_SelectAll(str);
            return View("LOC_CityList", dt);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult DeleteCity(int CityId)
        {
            string str = this.Configuration.GetConnectionString("mystrings");
            LOC_DAL dalLOC = new LOC_DAL();
            dalLOC.PR_LOC_City_DeleteByPK(str, CityId);
            return RedirectToAction("City");
        }
        #endregion

        [HttpPost]
        #region Save
        public IActionResult Save(LOC_CityModel modelLOC_City)
        {

            if (modelLOC_City != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string FileNameWithPath = Path.Combine(path, modelLOC_City.File.FileName);
                modelLOC_City.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelLOC_City.File.FileName;

                using (var stream = new FileStream(FileNameWithPath, FileMode.Create))
                {
                    modelLOC_City.File.CopyTo(stream);
                }
            }
            //connecting database city add error
            string str = this.Configuration.GetConnectionString("mystrings");
            LOC_DAL dalLOC = new LOC_DAL();

            if (modelLOC_City.CityId == 0)
            {
                dalLOC.PR_LOC_City_Insert(str, modelLOC_City.CountryId, modelLOC_City.StateId, modelLOC_City.CityName, modelLOC_City.CityCode, modelLOC_City.PhotoPath, modelLOC_City.CreationDate, modelLOC_City.ModificationDate);
            }
            else
            {
                dalLOC.PR_LOC_City_UpdateByPK(str, modelLOC_City.CountryId, modelLOC_City.StateId, modelLOC_City.CityId, modelLOC_City.CityName, modelLOC_City.CityCode, modelLOC_City.PhotoPath, modelLOC_City.ModificationDate);

             }
          
            /*if(Convert.ToBoolean(cmd.ExecuteNonQuery()))
            {
                if(modelLOC_Country.CountryId == null)
                {
                    TempData["CountryInsertMsg"] = "Data Inserted successfully";
                }
                else
                {
                    TempData["CountryUpdateMsg"] = "Data Updated Successfully";
                }
            } */
            
            return RedirectToAction("City");
        }
        #endregion

        #region Dropdownfill
        [HttpPost]
        public IActionResult DropdownByCountry(int CountryId)
        {
            string connectionstr = this.Configuration.GetConnectionString("mystrings");
            //PrePare a connection
            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection(connectionstr);
            conn.Open();
            SqlCommand objCmd = conn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_LOC_State_SelectComboBoxbyCountryId";
            objCmd.Parameters.AddWithValue("@CountryId", CountryId);
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            conn.Close();

            List<LOC_StateDropDownModel> list = new List<LOC_StateDropDownModel>();

            foreach (DataRow dr in dt.Rows)
            {
                LOC_StateDropDownModel vlst = new LOC_StateDropDownModel();
                vlst.StateId = Convert.ToInt32(dr["StateId"]);
                vlst.StateName = dr["StateName"].ToString();
                list.Add(vlst);
            }

            var vModel = list;
            return Json(vModel);
        }
        #endregion

    }
}
