using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebAppTABLE.Models;
using WebAppTABLE.DAL;

namespace WebAppTABLE.Controllers
{
    public class MST_ContactCategoryController : Controller
    {
        public IConfiguration Configuration;
        public MST_ContactCategoryController(IConfiguration _Configuration)
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
            cmd.CommandText = "PR_MST_ContactCategory_SelectAll";
            cmd.Parameters.AddWithValue("@ContactCategoryName", Convert.ToString(HttpContext.Request.Form["ContactCategoryName"]));
            cmd.Parameters.AddWithValue("@ContactCategoryCode", Convert.ToString(HttpContext.Request.Form["ContactCategoryCode"]));
            DataTable dt = new DataTable();
            SqlDataReader objSDR = cmd.ExecuteReader();
            dt.Load(objSDR);

            return View("MST_ContactCategoryList", dt);
        }
        #endregion
        #region Select ALL
        public IActionResult ContactCategory()
        {
            string str = this.Configuration.GetConnectionString("mystrings");
            CON_DAL dalLOC = new CON_DAL();
            DataTable dt = dalLOC.PR_MST_ContactCategory_SelectAll(str);
            return View("MST_ContactCategoryList", dt);
        }
        #endregion

        #region Delete
        [HttpPost]
        public IActionResult DeleteContactCategory(int ContactCategoryId)
        {
            string str = this.Configuration.GetConnectionString("mystrings");
            CON_DAL dalCON = new CON_DAL();
            dalCON.PR_MST_ContactCategory_DeleteByPK(str, ContactCategoryId);
            return RedirectToAction("ContactCategory");
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(MST_ContactCategoryModel model_ContactCategory)
        {
            if (model_ContactCategory != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string FileNameWithPath = Path.Combine(path, model_ContactCategory.File.FileName);
                model_ContactCategory.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + model_ContactCategory.File.FileName;

                using (var stream = new FileStream(FileNameWithPath, FileMode.Create))
                {
                    model_ContactCategory.File.CopyTo(stream);
                }
            }

            //connecting database
            string str = this.Configuration.GetConnectionString("mystrings");
            CON_DAL dalCON = new CON_DAL();

            if (model_ContactCategory.ContactCategoryId == 0)
            {
                dalCON.PR_MST_ContactCategory_Insert(str, model_ContactCategory.ContactCategoryName, model_ContactCategory.ContactCategoryCode, model_ContactCategory.PhotoPath, model_ContactCategory.CreationDate, model_ContactCategory.ModificationDate);
            }
            else
            {
                dalCON.PR_MST_ContactCategory_UpdateByPK(str, model_ContactCategory.ContactCategoryId, model_ContactCategory.ContactCategoryName, model_ContactCategory.ContactCategoryCode,model_ContactCategory.PhotoPath, model_ContactCategory.ModificationDate);
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
            return RedirectToAction("ContactCategory");
        }
        #endregion

        #region Add
        public IActionResult Add(int? ContactCategoryId)
        {
            if (ContactCategoryId != null)
            {
                string str = this.Configuration.GetConnectionString("mystrings");

                SqlConnection con = new SqlConnection(str);
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContactCategoryId", ContactCategoryId);
                cmd.CommandText = "PR_MST_ContactCategory_SelectByPK";
                DataTable dt = new DataTable();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                if (dt.Rows.Count > 0)
                {
                    MST_ContactCategoryModel modelMST_ContactCategory = new MST_ContactCategoryModel();
                    foreach (DataRow dr in dt.Rows)
                    {

                        modelMST_ContactCategory.ContactCategoryId = Convert.ToInt32(dr["ContactCategoryId"]);
                        modelMST_ContactCategory.ContactCategoryName = dr["ContactCategoryName"].ToString();
                        modelMST_ContactCategory.ContactCategoryCode = dr["ContactCategoryCode"].ToString();
                        modelMST_ContactCategory.PhotoPath = dr["PhotoPath"].ToString();
                        modelMST_ContactCategory.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                        modelMST_ContactCategory.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                    }
                    return View("MST_ContactCategoryAddEdit", modelMST_ContactCategory);
                }
            }
            return View("MST_ContactCategoryAddEdit");
        }
        #endregion
    }
}
