using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using WebAppTABLE.DAL;

namespace WebAppTABLE.Models
{
    public class CON_ContactController : Controller
    {
        public IConfiguration Configuration;

        public CON_ContactController(IConfiguration _Configuration)
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
            cmd.CommandText = "PR_CON_Contact_SelectForPage";
            cmd.Parameters.AddWithValue("@PersonName", Convert.ToString(HttpContext.Request.Form["PersonName"]));
            cmd.Parameters.AddWithValue("@MobileNumber", Convert.ToString(HttpContext.Request.Form["MobileNumber"]));
            /*cmd.Parameters.Add("@CountryName", SqlDbType.VarChar).Value = CountryName;
            cmd.Parameters.Add("@CountryCode", SqlDbType.VarChar).Value = CountryCode;*/
            DataTable dt = new DataTable();
            SqlDataReader objSDR = cmd.ExecuteReader();
            dt.Load(objSDR);

            return View("CON_ContactList", dt);
        }
        #endregion


        #region SelectALL
        public IActionResult Contact()
        {
            string str = Configuration.GetConnectionString("mystrings");
            CON_DAL dalLOC = new CON_DAL();
            DataTable dt = dalLOC.PR_CON_Contact_SelectAll(str);
            /*SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CON_Contact_SelectAll";
            DataTable dt = new DataTable();
            SqlDataReader objsdr = cmd.ExecuteReader();
            dt.Load(objsdr);
            cmd.ExecuteReader();*/
            return View("CON_ContactList", dt);
        }
        #endregion

        #region DeleteByPK
        [HttpPost]
        public IActionResult DeleteContact(int ContactId)
        {
            string str = Configuration.GetConnectionString("mystrings");
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CON_Contact_DeleteByPK";
            cmd.Parameters.AddWithValue("@ContactId", ContactId);
            cmd.ExecuteNonQuery();
            return RedirectToAction("Contact");
        }
        #endregion

        #region Add
        public IActionResult Add(int? ContactId)
        {
            #region Dropdown Country
            string str1 = Configuration.GetConnectionString("mystrings");
            SqlConnection conn1 = new SqlConnection(str1);
            conn1.Open();
            SqlCommand cmd1 = conn1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_LOC_Country_SelectForDropDown";

            DataTable dt1 = new DataTable();
            SqlDataReader dr1 = cmd1.ExecuteReader();
            dt1.Load(dr1);
            conn1.Close();

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
            string str2 = Configuration.GetConnectionString("mystrings");
            SqlConnection conn2 = new SqlConnection(str2);
            conn2.Open();
            SqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_LOC_State_SelectForDropDown";
            DataTable dt2 = new DataTable();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            dt2.Load(dr2);
            conn2.Close();

            List<LOC_StateDropDownModel> list2 = new List<LOC_StateDropDownModel>();
            foreach (DataRow dm in dt2.Rows)
            {
                LOC_StateDropDownModel dlist = new LOC_StateDropDownModel();
                dlist.StateId = Convert.ToInt32(dm["StateId"]);
                dlist.StateName = dm["StateName"].ToString();
                list2.Add(dlist);
            }
            ViewBag.StateList = list2;
            #endregion

            #region CityDropDown


            List<LOC_CityDropDownModel> list3 = new List<LOC_CityDropDownModel>();
            ViewBag.CityList = list3;
            #endregion

            #region ContactCategoryDropDown

            string str4 = Configuration.GetConnectionString("mystrings");
            SqlConnection conn4 = new SqlConnection(str4);
            conn2.Open();
            SqlCommand cmd4 = conn4.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_MST_ContactCategory_SelectForDropDown";
            DataTable dt4 = new DataTable();
            SqlDataReader dr4 = cmd2.ExecuteReader();
            dt4.Load(dr4);
            conn2.Close();
            List<MST_ContactCategoryDropDownModel> list4 = new List<MST_ContactCategoryDropDownModel>();
            foreach (DataRow dm in dt4.Rows)
            {
                MST_ContactCategoryDropDownModel cclist = new MST_ContactCategoryDropDownModel();
                cclist.ContactCategoryId = Convert.ToInt32(dm["ContactCategoryId"]);
                cclist.ContactCategoryName = dm["ContactCategoryName"].ToString();
                list4.Add(cclist);
            }
            ViewBag.ContactCategoryList = list4;
            #endregion

            #region Record Select by PK
            {
                if (ContactId != null)
                {
                    string str = Configuration.GetConnectionString("mystrings");

                    SqlConnection con = new SqlConnection(str);
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PR_CON_Contact_SelectByPK";
                    cmd.Parameters.AddWithValue("@ContactId", ContactId);

                    DataTable dt5 = new DataTable();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dt5.Load(sdr);
                    if (dt5.Rows.Count > 0)
                    {
                        CON_ContactModel modelLOC_Contact = new CON_ContactModel();
                        foreach (DataRow dr in dt5.Rows)
                        {
                            modelLOC_Contact.CountryId = Convert.ToInt32(dr["CountryId"]);
                            modelLOC_Contact.StateId = Convert.ToInt32(dr["StateId"]);
                            modelLOC_Contact.CityId = Convert.ToInt32(dr["CityId"]);
                            modelLOC_Contact.ContactCategoryId = Convert.ToInt32(dr["ContactCategoryId"]);
                            modelLOC_Contact.ContactId = Convert.ToInt32(dr["ContactId"]);
                            modelLOC_Contact.PersonName = dr["PersonName"].ToString();
                            modelLOC_Contact.MobileNumber = dr["MobileNumber"].ToString();
                            modelLOC_Contact.PhotoPath = dr["PhotoPath"].ToString();
                            modelLOC_Contact.Email = dr["Email"].ToString();
                            modelLOC_Contact.Address = dr["Address"].ToString();
                            modelLOC_Contact.Instagram = dr["Instagram"].ToString();
                            modelLOC_Contact.LinkedIn = dr["LinkedIn"].ToString();
                            modelLOC_Contact.Twitter = dr["Twitter"].ToString();
                            modelLOC_Contact.CreationDate = Convert.ToDateTime(dr["CreationDate"]);
                            modelLOC_Contact.ModificationDate = Convert.ToDateTime(dr["ModificationDate"]);
                        }
                        string str9 = Configuration.GetConnectionString("mystrings");

                        SqlConnection conn9 = new SqlConnection(str9);
                        conn9.Open();
                        SqlCommand cmd9 = conn9.CreateCommand();
                        cmd9.CommandType = CommandType.StoredProcedure;
                        cmd9.CommandText = "PR_LOC_State_SelectComboBoxbyCountryId";
                        cmd9.Parameters.AddWithValue("@CountryId", modelLOC_Contact.CountryId);
                        DataTable dt9 = new DataTable();
                        SqlDataReader dr9 = cmd9.ExecuteReader();
                        dt2.Load(dr9);
                        //cmd2.Parameters.AddWithValue("@CountryId", CountryId);
                        conn9.Close();

                        List<LOC_StateDropDownModel> list9 = new List<LOC_StateDropDownModel>();
                        foreach (DataRow dm in dt2.Rows)
                        {
                            LOC_StateDropDownModel dlist = new LOC_StateDropDownModel();
                            dlist.StateId = Convert.ToInt32(dm["StateId"]);
                            dlist.StateName = dm["StateName"].ToString();
                            list9.Add(dlist);
                        }
                        ViewBag.StateList = list9;

                        string str3 = Configuration.GetConnectionString("mystrings");
                        SqlConnection conn3 = new SqlConnection(str3);
                        conn3.Open();
                        SqlCommand cmd3 = conn3.CreateCommand();
                        cmd3.CommandType = CommandType.StoredProcedure;
                        cmd3.CommandText = "PR_LOC_City_SelectComboBoxbyStateId";
                        cmd3.Parameters.AddWithValue("@StateId", modelLOC_Contact.StateId);
                        DataTable dt3 = new DataTable();
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                      
                        dt3.Load(dr3);
                        conn3.Close();
                        List<LOC_CityDropDownModel> list1 = new List<LOC_CityDropDownModel>();
                        foreach (DataRow dm in dt3.Rows)
                        {
                            LOC_CityDropDownModel clist = new LOC_CityDropDownModel();
                            clist.CityId = Convert.ToInt32(dm["CityId"]);
                            clist.CityName = dm["CityName"].ToString();
                            list1.Add(clist);
                        }
                        ViewBag.CityList = list1;
                        return View("CON_ContactAddEdit", modelLOC_Contact);
                    }
                }
                return View("CON_ContactAddEdit");
            }
            #endregion
        }
        #endregion

        #region Save
        [HttpPost]
        public IActionResult Save(CON_ContactModel modelCON_Contact)
        {
            if (modelCON_Contact != null)
            {
                string FilePath = "wwwroot\\Upload";
                string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string FileNameWithPath = Path.Combine(path, modelCON_Contact.File.FileName);
                modelCON_Contact.PhotoPath = "~" + FilePath.Replace("wwwroot\\", "/") + "/" + modelCON_Contact.File.FileName;

                using (var stream = new FileStream(FileNameWithPath, FileMode.Create))
                {
                    modelCON_Contact.File.CopyTo(stream);
                }
            }
            //connecting database
            string str = this.Configuration.GetConnectionString("mystrings");
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            //to prepare command
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CountryId", SqlDbType.Int).Value = modelCON_Contact.CountryId;
            cmd.Parameters.Add("@StateId", SqlDbType.Int).Value = modelCON_Contact.StateId;
            cmd.Parameters.Add("@CityId", SqlDbType.Int).Value = modelCON_Contact.CityId;
            cmd.Parameters.Add("@ContactCategoryId", SqlDbType.Int).Value = modelCON_Contact.ContactCategoryId;
            cmd.Parameters.Add("@PersonName", SqlDbType.NVarChar).Value = modelCON_Contact.PersonName;
            cmd.Parameters.Add("@PhotoPath", SqlDbType.NVarChar).Value = modelCON_Contact.PhotoPath;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = modelCON_Contact.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = modelCON_Contact.Address;
            cmd.Parameters.Add("@Gender", SqlDbType.NChar).Value = modelCON_Contact.Gender;
            cmd.Parameters.Add("@MobileNumber", SqlDbType.NVarChar).Value = modelCON_Contact.MobileNumber;
            cmd.Parameters.Add("@AlternateMobileNumber", SqlDbType.NVarChar).Value = modelCON_Contact.AlternateMobileNumber;
            cmd.Parameters.Add("@Instagram", SqlDbType.NVarChar).Value = modelCON_Contact.Instagram;
            cmd.Parameters.Add("@LinkedIn", SqlDbType.NVarChar).Value = modelCON_Contact.LinkedIn;
            cmd.Parameters.Add("@Twitter", SqlDbType.NVarChar).Value = modelCON_Contact.Twitter;
            cmd.Parameters.Add("@ModificationDate", SqlDbType.DateTime).Value = DBNull.Value;

            if (modelCON_Contact.ContactId == 0)
            {
                cmd.CommandText = "PR_CON_Contact_Insert";
                cmd.Parameters.Add("@CreationDate", SqlDbType.DateTime).Value = DBNull.Value;
               
            }
            else
            {
                cmd.CommandText = "PR_CON_Contact_UpdateByPK";
                cmd.Parameters.Add("@ContactId", SqlDbType.Int).Value = modelCON_Contact.ContactId;
            }
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Contact");
        }
        #endregion

        #region ComboboxStatebyCountryId
        [HttpPost]
        public IActionResult DropDownByCountry(int CountryId)
        {
            string str2 = Configuration.GetConnectionString("mystrings");

            SqlConnection conn2 = new SqlConnection(str2);
            conn2.Open();
            SqlCommand cmd2 = conn2.CreateCommand();
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.CommandText = "PR_LOC_State_SelectComboBoxbyCountryId";
            cmd2.Parameters.AddWithValue("@CountryId", CountryId);
            DataTable dt2 = new DataTable();
            SqlDataReader dr2 = cmd2.ExecuteReader();
            dt2.Load(dr2);
            //cmd2.Parameters.AddWithValue("@CountryId", CountryId);
            conn2.Close();

            List<LOC_StateDropDownModel> list2 = new List<LOC_StateDropDownModel>();
            foreach (DataRow dm in dt2.Rows)
            {
                LOC_StateDropDownModel dlist = new LOC_StateDropDownModel();
                dlist.StateId = Convert.ToInt32(dm["StateId"]);
                dlist.StateName = dm["StateName"].ToString();
                list2.Add(dlist);
            }
            var vmodel = list2;
            return Json(vmodel);
        }
        #endregion

        #region ComboboxCitybyStateId
        [HttpPost]
        public IActionResult DropDownByState(int StateId)
        {
            string str3 = Configuration.GetConnectionString("mystrings");
            SqlConnection conn3 = new SqlConnection(str3);
            conn3.Open();
            SqlCommand cmd3 = conn3.CreateCommand();
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.CommandText = "PR_LOC_City_SelectComboBoxbyStateId";
            cmd3.Parameters.AddWithValue("@StateId", StateId);
            DataTable dt3 = new DataTable();
            SqlDataReader dr3 = cmd3.ExecuteReader();
         
            dt3.Load(dr3);
            conn3.Close();
            List<LOC_CityDropDownModel> list3 = new List<LOC_CityDropDownModel>();
            foreach (DataRow dm in dt3.Rows)
            {
                LOC_CityDropDownModel clist = new LOC_CityDropDownModel();
                clist.CityId = Convert.ToInt32(dm["CityId"]);
                clist.CityName = dm["CityName"].ToString();
                list3.Add(clist);
            }
            var vmodel = list3;
            return Json(vmodel);
        }
        #endregion

    }
}
