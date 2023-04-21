using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using WebAppTABLE.DAL;
using WebAppTABLE.Models;

namespace WebAppTABLE.Controllers
{
    public class LOC_CountryController : Controller
    {
        public IConfiguration Configuration;

        public LOC_CountryController(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
        }

        #region Search
        public IActionResult Search()
        {
            string str = this.Configuration.GetConnectionString("mystrings");
           
             SqlConnection conn = new SqlConnection(str);
             conn.Open();
             SqlCommand cmd = conn.CreateCommand();
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "PR_LOC_Country_SelectAll";
             cmd.Parameters.AddWithValue("@CountryName", Convert.ToString(HttpContext.Request.Form["CountryName"]));
             cmd.Parameters.AddWithValue("@CountryCode", Convert.ToString(HttpContext.Request.Form["CountryCode"]));
             DataTable dt = new DataTable();
             SqlDataReader objSDR = cmd.ExecuteReader();
             dt.Load(objSDR);

            return View("LOC_CountryList", dt);
        }
        #endregion
        public IActionResult Add(int? CountryId)
        {

            if (CountryId != null)
            {
                string str = this.Configuration.GetConnectionString("mystrings");
                SqlConnection con = new SqlConnection(str);
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CountryId", SqlDbType.Int).Value = CountryId;
                cmd.CommandText = "PR_LOC_Country_SelectByPK";
                DataTable dt = new DataTable();
                SqlDataReader objsdr = cmd.ExecuteReader();
                dt.Load(objsdr);


                LOC_CountryModel modelLOC_Country = new LOC_CountryModel();
                foreach (DataRow dr in dt.Rows)
                {

                    modelLOC_Country.CountryName = dr["CountryName"].ToString();
                    modelLOC_Country.CountryCode = dr["CountryCode"].ToString();
                    modelLOC_Country.PhotoPath = dr["PhotoPath"].ToString();
                    modelLOC_Country.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                    modelLOC_Country.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                }
                return View("LOC_CountryAddEdit", modelLOC_Country);
            }
            return View("LOC_CountryAddEdit");
        }
        #region SelectAll
        public IActionResult Index()
        {
            string str = this.Configuration.GetConnectionString("mystrings");
            LOC_DAL dalLOC = new LOC_DAL();
            DataTable dt = dalLOC.PR_LOC_Country_SelectAll(str);
            return View("LOC_CountryList", dt);
        }
        #endregion

        #region DeleteByPK
        public IActionResult Delete(int CountryId)
        {
            string str = this.Configuration.GetConnectionString("mystrings");
            LOC_DAL dalLOC = new LOC_DAL();
            dalLOC.PR_LOC_Country_DeleteByPK(str, CountryId);
            return RedirectToAction("Index");
        }
        #endregion

        #region Save,PhotoPath

        [HttpPost]
        public IActionResult Save(LOC_CountryModel modelLOC_Country)
        {
            if (modelLOC_Country.File != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string FileNameWithPath = Path.Combine(path, modelLOC_Country.File.FileName);
                modelLOC_Country.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelLOC_Country.File.FileName;

                using (var stream = new FileStream(FileNameWithPath, FileMode.Create))
                {
                    modelLOC_Country.File.CopyTo(stream);
                }
            }

            //connecting database
            string str = this.Configuration.GetConnectionString("mystrings");
         
            LOC_DAL dalLOC = new LOC_DAL();
             if (modelLOC_Country.CountryId == null)
             {
                dalLOC.PR_LOC_Country_Insert(str, modelLOC_Country.CountryName, modelLOC_Country.CountryCode, modelLOC_Country.PhotoPath, modelLOC_Country.CreationDate, modelLOC_Country.ModificationDate);
            }
            else
             {
                dalLOC.PR_LOC_Country_UpdateByPK(str, modelLOC_Country.CountryId, modelLOC_Country.CountryName, modelLOC_Country.CountryCode, modelLOC_Country.PhotoPath, modelLOC_Country.ModificationDate);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
