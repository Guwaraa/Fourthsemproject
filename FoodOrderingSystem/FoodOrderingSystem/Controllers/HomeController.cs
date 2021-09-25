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
    public class HomeController : Controller
    {
        public string constring = "Data Source=DESKTOP-SNP7JH6\\SQLEXPRESS;Initial Catalog=Online Food Odering System;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        Online_Food_Odering_SystemEntities1 db = new Online_Food_Odering_SystemEntities1();

        public ActionResult Index()
        {
            return View(db.Foodmenus.ToList());
        }
        public ActionResult Login()
        {
            return View();
        }
        string admin = "Admin";
        string user = "User";
        int j = 0;
        int i = 1;
        [HttpPost]
        public ActionResult CheckLogin(string email,string password)
        {
            while(j==0)
            { 
                try
                {
                    Userlogin userdetail = new Userlogin();
                    userdetail = db.Userlogins.Find(i);
                    if (string.Equals(userdetail.Password, password) && string.Equals(userdetail.Email, email))
                    {
                        if (string.Equals(userdetail.Status.ToString(), admin))
                        {
                            Session["username"] = userdetail.Username;
                            Session["id"] = userdetail.Id;
                            return RedirectToAction("Index", "Adminpage");
                        }
                        if (string.Equals(userdetail.Status.ToString(), user))
                        {
                            Session["username"] = userdetail.Username;
                            Session["id"] = userdetail.Id;
                            return RedirectToAction("Index", "Mainpage");

                        }
                    }
                    i++;
                
                }
                catch (Exception)
                {

                }
                finally
                {

                }
            }


            return RedirectToAction("Login", "Home");


        }
        public ActionResult Categories()
        {

            return View(db.Foodmenus.ToList());
        }
        public ActionResult search()
        {
            return View();
        }
        public ActionResult foods()
        {
            return View(db.Foodmenus.ToList());
        }
        public ActionResult createaccount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult registeraccount(string username,string email,string number, string password)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            string query = "Insert into Userlogin(Username,Email,Password,Phoneno,status)Values('" + username + "','" + email + "','" + password + "','" + number + "','" + "User" + "')";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Login", "Home");
        }
        public ActionResult contact()
        {
            return View();
        }
    }
}