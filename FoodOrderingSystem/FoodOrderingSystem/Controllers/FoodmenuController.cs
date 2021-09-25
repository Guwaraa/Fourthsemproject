using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodOrderingSystem.Models;
using System.Data.SqlClient;
using FoodOrderingSystem.Controllers;
using System.Data;
namespace FoodOrderingSystem.Controllers

{
    public class FoodmenuController : Controller
    {
        Online_Food_Odering_SystemEntities1 db = new Online_Food_Odering_SystemEntities1();
        public string constring= "Data Source=DESKTOP-SNP7JH6\\SQLEXPRESS;Initial Catalog=Online Food Odering System;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
        string namefood;
        int nameprice;
        string namecategory;
        string picturefood;
        // GET: Foodmenu
        [HttpPost]
        public ActionResult addtocart(string foodsn)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            Foodmenu menu = new Foodmenu();
            int sn = Convert.ToInt32(foodsn);
            menu = db.Foodmenus.Find(sn);
            namefood = menu.Foodname;
            nameprice = Convert.ToInt32(menu.FoodPrice);
            namecategory = menu.FoodCategory;
            picturefood=menu.Foodpicture;
            string query = "Insert into Addtocart(SN,Foodname,FoodPrice,FoodCategory,Foodpicture)Values('"+sn+"','" + namefood + "','" + nameprice + "','" + namecategory + "','" + picturefood + "')";
            SqlDataAdapter sda = new SqlDataAdapter(query,con);
            sda.SelectCommand.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Foods", "Mainpage");
        }
        public ActionResult oder()
        {
            return View(db.Addtocarts.ToList());
        }
        [HttpPost]
        public ActionResult orderconfirm(string foodsn, string quantity)
        {
            int total = 0;
            Addtocart cart = new Addtocart();
            int sn = Convert.ToInt32(foodsn);
            cart = db.Addtocarts.Find(sn);
            int conquantity = Convert.ToInt32(quantity);
            if (conquantity != 1)
            {
                total = Convert.ToInt32(cart.FoodPrice) * (conquantity - 1);
            }
            string totalprice = Session["foodprice"].ToString();
            Session["foodprice"] = Convert.ToInt32(totalprice) + total;
            return RedirectToAction("oder", "Foodmenu");
        }
        public ActionResult verificationpage()
        {
            return View();
        }
        int j = 0;
        int i = 0;
        string joinfood;
        public ActionResult bookingdone(string fullname, string contact, string email, string address)
        {
            SqlConnection con1 = new SqlConnection(constring);
            con1.Open();
            string query = "Select *from Addtocart";
            SqlDataAdapter sda = new SqlDataAdapter(query, con1);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            try
            {
                while (j == 0)
                {
                    string food = dt.Rows[i]["Foodname"].ToString();
                    joinfood = food + " " + joinfood;
                    i++;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                
                con1.Close();
            }



            SqlConnection con = new SqlConnection(constring);

            con.Open();
            string username = Session["username"].ToString();
            int foodprice = Convert.ToInt32(Session["foodprice"]);
            string query2 = "Insert into BookedFood(Username,Fullname,Contact,Email,Address,Foodlist,Foodprice)Values('" + username + "','" + fullname + "','" + contact + "','" + email + "','" + address + "','" + joinfood + "','" + foodprice + "')";
            SqlDataAdapter sda2 = new SqlDataAdapter(query2, con);
            sda2.SelectCommand.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("oderdisplay","Foodmenu");
        }
        public ActionResult oderdisplay()
        {
            return View(db.BookedFoods.ToList());
        }
       

    }
}