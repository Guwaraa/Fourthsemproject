using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodOrderingSystem.Models;
using FoodOrderingSystem.Controllers;
using System.Data.SqlClient;
using System.Data;

namespace FoodOrderingSystem.Controllers
{
    public class MainpageController : Controller
    {
        Online_Food_Odering_SystemEntities1 db = new Online_Food_Odering_SystemEntities1();
        public string constring = "Data Source=DESKTOP-SNP7JH6\\SQLEXPRESS;Initial Catalog=Online Food Odering System;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        // GET: Mainpage
        public ActionResult Index()
        {
            return View(db.Foodmenus.ToList());
        }
        public ActionResult Categories()
        {
            return View(db.Foodmenus.ToList());
        }
        public ActionResult Foods()
        {
            return View(db.Foodmenus.ToList());
        }
        public ActionResult Odernow()
        {
            return View();
        }
        public ActionResult search()
        {
            return View();
        }
        int j=0;
        int i = 0;
        int calculate;
        public ActionResult addtocart()
        {   
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string query = "Select *from Addtocart";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                while (j==0)
                {
                    string price = dt.Rows[i]["FoodPrice"].ToString();
                    int priceconvert = Convert.ToInt32(price);
                    calculate = calculate + priceconvert;
                    i++;
                }
            }
            catch(Exception)
            {

            }
            finally
            {
                Session["foodprice"] = calculate;
            }




            return View(db.Addtocarts.ToList());
        }
        public ActionResult removecart(string foodsn)
        {
            int sn = Convert.ToInt32(foodsn);
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string query = "DELETE FROM Addtocart Where SN='" + sn + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("addtocart", "Mainpage");
        }
        public ActionResult contact()
        {
            return View();
        }
        public ActionResult UserProfile()
        {
            Userlogin studenttb = new Userlogin();
            string userid = Session["id"].ToString();
            int id = Convert.ToInt32(userid);
            studenttb = db.Userlogins.Find(id);
            return View(studenttb);
        }
        public ActionResult maincontact()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserProfile([Bind(Include = "Username,Email,Phoneno")] Userlogin studentdata)
        {
            try
            {

            
            if (ModelState.IsValid)
            {
                db.Entry(studentdata).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserProfile");
            }
            }
            catch(Exception)
            {

            }
            return View(studentdata);
        }
    }
}