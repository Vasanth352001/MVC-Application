using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCakeShop.Models;
using OnlineCakeShop.DataAccess;
using System.Data.SqlClient;
using System.Collections;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace OnlineCakeShop.Controllers
{
    public class HomeController : Controller
    {
        CRUDOperation data = new CRUDOperation();
        SqlConnection connect = new SqlConnection("data source=LENOVO\\SQLEXPRESS; database=Authentication; integrated security = SSPI");
        /*-------------------------------------------------------------------------------------------------*/

        [HttpGet]
        public ActionResult Index()
        {
            data.AddCount();
            Session["Page"] = "Index";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Featured = data.FeaturedProducts();
                ViewBag.Latest = data.LatestProducts();
                List<string> Offer = data.OfferProducts();
                ViewBag.OfferUrl = Offer[0];
                ViewBag.OfferName = Offer[1];
                ViewBag.OfferPrice = Offer[2];
                ViewBag.OfferOffer = Offer[3];
                return View("Index");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Featured = data.FeaturedProducts();
                ViewBag.Latest = data.LatestProducts();
                List<string> Offer = data.OfferProducts();
                ViewBag.OfferUrl = Offer[0];
                ViewBag.OfferName = Offer[1];
                ViewBag.OfferPrice = Offer[2];
                ViewBag.OfferOffer = Offer[3];
                return View("Index");
            }
        }

        public ActionResult AdminHome()
        {
            Session["Page"] = "AdminHome";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    List<string> Data = data.GraphData();
                    List<int> CountPercentage = new List<int>();
                    List<string> Day = new List<string>();
                    string[] DataArray = new string[2];
                    for (int i = 0; i < Data.Count; i++)
                    {
                        string DataIndex = Data[i];
                        DataArray = DataIndex.Split(' ');
                        CountPercentage.Add(data.CalculatePercentage(DataArray[0]));
                        Day.Add(data.DayFind(DataArray[1]));
                    }
                    ViewBag.CustomerCount = data.CustomerCount();
                    ViewBag.ProductCount = data.ProductCount();
                    ViewBag.OrderCount = data.OrderCount();
                    ViewBag.StockCheck = data.StockCheck();
                    ViewBag.Day1 = "Today Viewers";
                    ViewBag.Count1 = CountPercentage[0];
                    ViewBag.Day2 = Day[1];
                    ViewBag.Count2 = CountPercentage[1];
                    ViewBag.Day3 = Day[2];
                    ViewBag.Count3 = CountPercentage[2];
                    ViewBag.Day4 = Day[3];
                    ViewBag.Count4 = CountPercentage[3];
                    ViewBag.Day5 = Day[4];
                    ViewBag.Count5 = CountPercentage[4];
                    return View("AdminHome");
                    /*return Content(CountPercentage[0] + " " + Day[0] + " " + CountPercentage[1] + " " + Day[1] + " " + 
                     * CountPercentage[2] + " " + Day[2] + " " + CountPercentage[3] + " " + Day[3] + " " + 
                     * CountPercentage[4] + " " + Day[4]+" Fifth");*/
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult About()
        {
            Session["Page"] = "About";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Message = "Your application description page.";
                return View();
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Message = "Your application description page.";
                return View();
            }
        }

        public ActionResult Contact()
        {
            Session["Page"] = "Contact";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                return View("Contact");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                return View("Contact");
            }
        }

        public ActionResult Account()
        {
            Session["Page"] = "Account";
            if (Session["userid"] != null)
            {
                //return Content(Customer[0]+" "+ Customer[1] + " " + Customer[2] + " " + Customer[3] + " " + Customer[4]);
                List<string> Customer = data.Account(Session["userid"].ToString());
                int CartCount = data.CartCount(Session["userid"].ToString());
                int OrderWIPCount = data.OrderCount(Session["userid"].ToString(), "WIP");
                int OrderSuccessCount = data.OrderCount(Session["userid"].ToString(), "Delivered");
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.UserId = Customer[0];
                ViewBag.UserName = Customer[1];
                ViewBag.UserEmail = Customer[2];
                ViewBag.UserPhonenumber = Customer[3];
                ViewBag.UserDOB = Customer[4];
                ViewBag.UserCart = CartCount;
                ViewBag.UserWIPOrder = OrderWIPCount;
                ViewBag.UserSuccessOrder = OrderSuccessCount;
                return View("Account");
                //return Content(""+Customer[0]+" "+ Customer[1] + " " + Customer[2] + " " + Customer[3] + " " + Customer[4]+" "+CartCount+" "+OrderWIPCount+" "+OrderSuccessCount);
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult YourOrder()
        {
            Session["Page"] = "YourOrder";
            if (Session["userid"] != null)
            {
                ViewBag.Orders = data.YourOrder(Session["userid"].ToString());
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                return View("YourOrder");
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult PastOrder()
        {
            Session["Page"] = "PastOrder";
            if (Session["userid"] != null)
            {
                ViewBag.Orders = data.PastOrder(Session["userid"].ToString());
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                return View("PastOrder");
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ConfirmationMail()
        {
            Session["Page"] = "Index";
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult AddProduct()
        {
            Session["Page"] = "AddProduct";
            if (Session["AdminId"]==null)
            {
                return View("PageNotFound") ;
            }
            else if(Session["AdminId"]!=null)
            {
                if(Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    return View("AddProduct");
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult EditProduct()
        {
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    return View("EditProduct");
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult Products()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.Products();
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.Products();
                return View("Products");
            }
        }

        public ActionResult Above0()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.SortByPrice("0","100");
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.SortByPrice("0","100");
                return View("Products");
            }
        }

        public ActionResult Above100()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.SortByPrice("100", "400");
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.SortByPrice("100", "400");
                return View("Products");
            }
        }

        public ActionResult Above400()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.SortByPrice("400", "600");
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.SortByPrice("400", "600");
                return View("Products");
            }
        }

        public ActionResult Above600()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.SortByPrice("600", "900");
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.SortByPrice("600", "900");
                return View("Products");
            }
        }

        public ActionResult Above900()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.SortByPrice("900", "10000");
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.SortByPrice("900", "10000");
                return View("Products");
            }
        }

        public ActionResult Chocolate()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.SortByCategory("Chocolate_Cake");
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.SortByCategory("Chocolate_Cake");
                return View("Products");
            }
        }

        public ActionResult BlackForest()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.SortByCategory("BlackForest_Cake");
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.SortByCategory("BlackForest_Cake");
                return View("Products");
            }
        }

        public ActionResult Icecream()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.SortByCategory("Icecream_Cake");
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.SortByCategory("Icecream_Cake");
                return View("Products");
            }
        }

        public ActionResult Butterscotch()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.SortByCategory("Butterscotch_Cake");
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.SortByCategory("Butterscotch_Cake");
                return View("Products");
            }
        }

        public ActionResult Cup()
        {
            Session["Page"] = "Products";
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                ViewBag.Products = data.SortByCategory("Cup_Cake");
                return View("Products");
            }
            else
            {
                ViewBag.UserName = "Account";
                ViewBag.Signbtn = "Login";
                ViewBag.Products = data.SortByCategory("Cup_Cake");
                return View("Products");
            }
        }

        public ActionResult Cart()
        {
            Session["Page"] = "Cart";
            if (Session["userid"] != null)
            {
                try
                {
                    ViewBag.UserName = data.Username(Session["userid"].ToString());
                    ViewBag.Signbtn = "Logout";
                    ViewBag.CartProducts = data.Cart(Session["userid"].ToString());
                    return View("Cart");
                }
                catch (SqlException MysqlCount)
                {
                    connect.Close();
                    return View("ServerError");
                }
            }
            else
            {
                Session["Page"] = "Cart";
                return View("PageNotFound");
            }
        }

        public ActionResult ViewProducts()
        {
            Session["Page"] = "ViewProducts";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        ViewBag.Products = data.AdminViewProduct();
                        return View("ViewProducts");
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult AdminViewChocolate()
        {
            Session["Page"] = "ViewProducts";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        ViewBag.Products = data.AdminSortByCategory("Chocolate_Cake");
                        return View("ViewProducts");
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult AdminViewBlackForest()
        {
            Session["Page"] = "ViewProducts";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        ViewBag.Products = data.AdminSortByCategory("BlackForest_Cake");
                        return View("ViewProducts");
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult AdminViewIcecream()
        {
            Session["Page"] = "ViewProducts";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        ViewBag.Products = data.AdminSortByCategory("Icecream_Cake");
                        return View("ViewProducts");
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult AdminViewButterscotch()
        {
            Session["Page"] = "ViewProducts";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        ViewBag.Products = data.AdminSortByCategory("Butterscotch_Cake");
                        return View("ViewProducts");
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult AdminViewCup()
        {
            Session["Page"] = "ViewProducts";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        ViewBag.Products = data.AdminSortByCategory("Cup_Cake");
                        return View("ViewProducts");
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult AdminViewOrders()
        {
            Session["Page"] = "AdminViewOrders";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        ViewBag.Orders = data.ViewOrders();
                        return View();
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult AdminViewPastOrders()
        {
            Session["Page"] = "AdminViewPastOrders";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        ViewBag.Past = data.ViewPastOrders();
                        return View();
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult AdminViewCustomers()
        {
            Session["Page"] = "AdminViewCustomers";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        ViewBag.Customers = data.ViewCustomers();
                        return View();
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult SpecificCustomerOrders()
        {
            Session["Page"] = "SpecificCustomerOrders";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        return View();
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult SpecificProductOrders()
        {
            Session["Page"] = "SpecificProductOrders";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    try
                    {
                        return View();
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult BuyProduct()
        {
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                return View("CheckCakeDetails");
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult CheckCakeDetails()
        {
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                return View("CheckCakeDetails");
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult CheckPersonalDetails()
        {
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                return View("CheckPersonalDetails");
            }
            else
            {
                return View("PageNotFound");
            }
        }

        public ActionResult Success()
        {
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                return View();
            }
            else
            {
                return View("PageNotFound");
            }
        }


        /*------------------------------------------------------------------------------------------------*/

        [HttpPost]
        public ActionResult Gmail(CakeshopData det)
        {
            if(data.Gmail(det.Email,det.Phonenumber)==true)
            {
                ViewBag.Registererror = "Check Your Mail Message.Please Confirm your Mail Id";
                TempData["RegisterDetails"] = det;
                return View("Register");
            }
            else
            {
                ViewBag.Registererror = "This Account is Already Exist";
                return View("Register");
            }
        }

        [HttpPost]
        public ActionResult Register(CakeshopData det)
        {
            var Register = TempData["RegisterDetails"] as CakeshopData;
            //return Content(Register.Name+" "+Register.Email);
            if(Register == null)
            {
                return View("ServerError");
            }
            else if (data.Register(Register.Name, Register.Email, Register.Phonenumber, Register.DOB, Register.Password).Equals("Registered"))
            {
                ViewBag.Loginerror = "Check Your Mail Message.Your Login Id is there";
                return View("Login");
            }
            else if (data.Register(Register.Name, Register.Email, Register.Phonenumber, Register.DOB, Register.Password).Equals("Exist"))
            {
                ViewBag.Registererror = "This Account is already Exist!!!";
                return View("Register");
            }
            else
            {
                return Content(data.Register(Register.Name, Register.Email, Register.Phonenumber, Register.DOB, Register.Password));
            }
        }

        [HttpPost]
        public ActionResult Login(Login det)
        {
            if (det.Id.Equals("Admin12345") && det.Password.Equals("Admin"))
            {
                Session["AdminId"] = det.Id;
                List<string> AdminPages = new List<string>();
                AdminPages.Add("AddProduct");
                AdminPages.Add("ViewProducts");
                AdminPages.Add("AdminHome");
                AdminPages.Add("AdminViewCustomers");
                AdminPages.Add("AdminViewOrders");
                AdminPages.Add("AdminViewPastOrders");
                AdminPages.Add("SpecificCustomerOrders");
                AdminPages.Add("SpecificProductOrders");

                if (AdminPages.Contains(Session["Page"].ToString()))
                {
                    return RedirectToAction(Session["Page"].ToString());
                }
                else
                {
                    return RedirectToAction("AdminHome");
                }
            }
            else
            {
                if (data.Login(det.Id, det.Password).Equals("Success"))
                {
                    Session["userid"] = det.Id;
                    List<string> CustomerPages = new List<string>();
                    CustomerPages.Add("About");
                    CustomerPages.Add("Account");
                    CustomerPages.Add("Cart");
                    CustomerPages.Add("Contact");
                    CustomerPages.Add("Index");
                    CustomerPages.Add("PastOrder");
                    CustomerPages.Add("Products");
                    CustomerPages.Add("YourOrder");

                    if (CustomerPages.Contains(Session["Page"].ToString()))
                    {
                        return RedirectToAction(Session["Page"].ToString());
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.Loginerror = "Login Id and Password is  wrong";
                    return View("Login");
                }
            }
        }

        [HttpPost]
        public ActionResult AddProduct(AddProduct det)
        {
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    //return Content(det.Name+" "+det.Offer+" "+det.Price+" "+det.Category+" "+det.URL+" "+det.Stock);
                    if (data.AddProduct(det.Category, det.Name, det.Price, det.Offer, det.Stock, det.URL).Equals("Exist"))
                    {
                        ViewBag.AddError = "This Product is Already there";
                        return View("AddProduct");
                    }
                    else if (data.AddProduct(det.Category, det.Name, det.Price, det.Offer, det.Stock, det.URL).Equals("ServerError"))
                    {
                        ViewBag.AddError = "Server Error";
                        return View("AddProduct");
                    }
                    else 
                    {
                        ViewBag.AddError = "Successfully Added!!!";
                        return View("AddProduct");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult EditProduct(EditProduct det)
        {
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    List<string> Datared = data.SingleProduct(det.URL);
                    ViewBag.URL = Datared[0].ToString();
                    ViewBag.Category = Datared[5].ToString();
                    ViewBag.Name = Datared[1].ToString();
                    ViewBag.Price = Datared[2].ToString();
                    ViewBag.Stock = Datared[3].ToString();
                    ViewBag.Offer = Datared[4].ToString();

                    return View("EditProduct");
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult DeleteProduct(DeleteProduct det)
        {
            if (data.DeleteProduct(det.URL) == true)
            {
                if (Session["AdminId"] == null)
                {
                    return View("PageNotFound");
                }
                else if (Session["AdminId"] != null)
                {
                    if (Session["AdminId"].ToString().Equals("Admin12345"))
                    {
                        try
                        {
                            ViewBag.Products = data.AdminViewProduct();
                            return View("ViewProducts");
                        }
                        catch (SqlException MysqlCount)
                        {
                            connect.Close();
                            return View("ServerError");
                        }
                    }
                    else
                    {
                        return View("PageNotFound");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("ServerError");
            }
        }

        [HttpPost]
        public ActionResult UpdateProduct(UpdateProduct det)
        {
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    if (data.UpdateProduct(det.CURURL, det.UpdateCategory, det.UpdateName, det.UpdatePrice, det.UpdateOffer, det.UpdateStock).Equals("ServerError"))
                    {
                        ViewBag.UpdateSuccess = "Server Error";
                        return View("EditProduct");
                    }
                    else if (data.UpdateProduct(det.CURURL, det.UpdateCategory, det.UpdateName, det.UpdatePrice, det.UpdateOffer, det.UpdateStock).Equals("Updated"))
                    {
                        ViewBag.UpdateSuccess = "Updated Successfully!!!";
                        ViewBag.URL = det.CURURL;
                        ViewBag.Category = det.UpdateCategory;
                        ViewBag.Name = det.UpdateName;
                        ViewBag.Price = det.UpdatePrice;
                        ViewBag.Offer = det.UpdateOffer;
                        ViewBag.Stock = det.UpdateStock;
                        return View("EditProduct");
                    }
                    else
                    {
                        ViewBag.UpdateSuccess = "Server Error";
                        return View("EditProduct");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult UpdateSingleValue(UpdateSingleValue Details)
        {
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    //return Content(Details.CURURL+" "+Details.UpdateValue+" "+Details.WhichValue);
                    if (data.UpdateSingleValue(Details.CURURL, Details.UpdateValue, Details.WhichValue) == true)
                    {
                        List<string> UpdatedDetails = data.SingleProduct(Details.CURURL);
                        ViewBag.URL = UpdatedDetails[0];
                        ViewBag.Name = UpdatedDetails[1];
                        ViewBag.Price = UpdatedDetails[2];
                        ViewBag.Stock = UpdatedDetails[3];
                        ViewBag.Offer = UpdatedDetails[4];
                        ViewBag.Category = UpdatedDetails[5];
                        return View("EditProduct");
                    }
                    else
                    {
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult SingleProduct(SingleProductDetails det)
        {
            if(Session["userid"]!= null)
            {
                List<string> single = data.SingleProduct(det.URL);
                try
                {
                    ViewBag.Url = single[0];
                    ViewBag.Name = single[1];
                    ViewBag.Price = single[2];
                    ViewBag.Stock = single[3];
                    ViewBag.Offer = single[4];
                    ViewBag.RelatedProduct = data.RelatedProduct(det.URL, single[5]);
                    ViewBag.UserName = data.Username(Session["userid"].ToString());
                    ViewBag.Signbtn = "Logout";
                    ViewBag.BtnValue = "Add to Cart";
                    return View("SingleProduct");
                }
                catch (Exception ex)
                {
                    return View("ServerError");
                }
            }
            else
            {
                List<string> single = data.SingleProduct(det.URL);
                try
                {
                    ViewBag.Url = single[0];
                    ViewBag.Name = single[1];
                    ViewBag.Price = single[2];
                    ViewBag.Stock = single[3];
                    ViewBag.Offer = single[4];
                    ViewBag.RelatedProduct = data.RelatedProduct(det.URL, single[5]);
                    ViewBag.UserName = "Account";
                    ViewBag.Signbtn = "Login";
                    ViewBag.BtnValue = "Add to Cart";
                    return View("SingleProduct");
                }
                catch (Exception ex)
                {
                    return View("ServerError");
                }
            }
        }

        [HttpPost]
        public ActionResult AddCart(AddCart det)
        {
            if (Session["userid"] != null)
            {
                string UserId = Session["userid"].ToString();
                if (data.AddCart(UserId, det.URL, det.Quantity).Equals("CartAdded"))
                {
                    List<string> single = data.SingleProduct(det.URL);
                    //return Content(single[0]+" "+ single[1] + " " + single[2] + " " + single[3] + " " + single[4] + " " + single[5]);
                    try
                    {
                        ViewBag.Url = single[0];
                        ViewBag.Name = single[1];
                        ViewBag.Price = single[2];
                        ViewBag.Stock = single[3];
                        ViewBag.Offer = single[4];
                        ViewBag.RelatedProduct = data.RelatedProduct(det.URL, single[5]);
                        ViewBag.UserName = data.Username(Session["userid"].ToString());
                        ViewBag.Signbtn = "Logout";
                        ViewBag.BtnValue = "Added!!!";
                        return View("SingleProduct");
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else if (data.AddCart(UserId, det.URL, det.Quantity).Equals("Exist"))
                {
                    List<string> single = data.SingleProduct(det.URL);
                    try
                    {
                        ViewBag.Url = single[0];
                        ViewBag.Name = single[1];
                        ViewBag.Price = single[2];
                        ViewBag.Stock = single[3];
                        ViewBag.Offer = single[4];
                        ViewBag.RelatedProduct = data.RelatedProduct(det.URL, single[5]);
                        ViewBag.UserName = data.Username(Session["userid"].ToString());
                        ViewBag.Signbtn = "Logout";
                        ViewBag.BtnValue = "Already Added!!";
                        return View("SingleProduct");
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("ServerError");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult RemoveCartProduct(SingleProductDetails det)
        {
            if (Session["userid"] != null)
            {
                if (data.RemoveCartProduct(Session["userid"].ToString(), det.URL).Equals("Removed"))
                {
                    try
                    {
                        ViewBag.UserName = data.Username(Session["userid"].ToString());
                        ViewBag.Signbtn = "Logout";
                        ViewBag.CartProducts = data.Cart(Session["userid"].ToString());
                        return View("Cart");
                    }
                    catch (SqlException MysqlCount)
                    {
                        connect.Close();
                        return View("ServerError");
                    }
                }
                else if (data.RemoveCartProduct(det.URL, Session["userid"].ToString()).Equals("ServerError"))
                {
                    return View("ServerError");
                }
                else
                {
                    return View("ServerError");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult CheckCakeDetails(BuyProduct cakedet)
        {
            TempData["BuyCakeDetails"] = cakedet;
            List<string> buycakedet = data.BuyCakeDetails(cakedet.URL);
            int originalprice = Int16.Parse(buycakedet[2]);
            int offer = Int16.Parse(buycakedet[3]);
            int price = ((offer * originalprice)/100)*cakedet.Quantity;
            ViewBag.Url = buycakedet[0];
            ViewBag.Name = buycakedet[1];
            ViewBag.Price = price;
            ViewBag.Quantity = cakedet.Quantity;
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                return View("CheckCakeDetails");
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult CheckPersonalDetails(Address address)
        {
            TempData["PersonalDetails"] = address;
            if (Session["userid"] != null)
            {
                ViewBag.UserName = data.Username(Session["userid"].ToString());
                ViewBag.Signbtn = "Logout";
                return View("CheckPaymentDetails");
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult CashOnDelivery()
        {
            var Cakedetails = TempData["BuyCakeDetails"] as BuyProduct;
            var Address = TempData["PersonalDetails"] as Address;
            List<string> CakeDet = data.BuyCakeDetails(Cakedetails.URL);
            int originalprice = Int32.Parse(CakeDet[2]);
            int offer = Int32.Parse(CakeDet[3]);
            int price = ((offer * originalprice) / 100) * Cakedetails.Quantity;
            string OId = data.OrderId();
            //return Content(CakeDet[1]+" "+price + " " + Session["userid"].ToString()+" "+ Address.Phonenumber+" "+ Address.Customername+" "+ Address.Doorno+" "+ Address.Streat+" "+ Address.Area+" "+ Address.District+" "+ Address.Pincode+" "+ Address.State);
            if (Session["userid"] != null)
            {
                if (data.Order(Cakedetails.URL, OId,CakeDet[1], price, Cakedetails.Quantity, CakeDet[4], Session["userid"].ToString(), Address.Phonenumber, Address.Customername, Address.Doorno, Address.Streat, Address.Area, Address.District, Address.Pincode, Address.State, "Cash").Equals("true"))
                {
                    data.UpdateStock(CakeDet[4], Cakedetails.Quantity);
                    ViewBag.UserName = data.Username(Session["userid"].ToString());
                    ViewBag.Signbtn = "Logout";
                    return View("Success");
                }
                else
                {
                    return View("ServerError");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult CheckPaymentDetails()
        {
            var Cakedetails = TempData["BuyCakeDetails"] as BuyProduct;
            var Address = TempData["PersonalDetails"] as Address;
            List<string> CakeDet = data.BuyCakeDetails(Cakedetails.URL);
            int originalprice = Int32.Parse(CakeDet[2]);
            int offer = Int32.Parse(CakeDet[3]);
            int price = ((offer * originalprice) / 100) * Cakedetails.Quantity;
            //return Content(CakeDet[1]+" "+price + " " + Session["userid"].ToString()+" "+ Address.Phonenumber+" "+ Address.Customername+" "+ Address.Doorno+" "+ Address.Streat+" "+ Address.Area+" "+ Address.District+" "+ Address.Pincode+" "+ Address.State);
            string OId = data.OrderId();
            if (Session["userid"] != null)
            {
                if (data.Order(Cakedetails.URL, OId,CakeDet[1], price, Cakedetails.Quantity, CakeDet[4], Session["userid"].ToString(), Address.Phonenumber, Address.Customername, Address.Doorno, Address.Streat, Address.Area, Address.District, Address.Pincode, Address.State, "Card").Equals("true"))
                {
                    data.UpdateStock(CakeDet[4], Cakedetails.Quantity);
                    ViewBag.UserName = data.Username(Session["userid"].ToString());
                    ViewBag.Signbtn = "Logout";
                    return View("Success");
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult SpecificCustomerOrders(SpecificCustomerOrder Id)
        {
            Session["Page"] = "SpecificCustomerOrders";
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    ViewBag.Name = data.Username(Id.CustomerId);
                    ViewBag.Orders = data.SpecificCustomerOrders(Id.CustomerId);
                    return View("SpecificCustomerOrders");
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult SpecificProductOrders(SpecificProductOrder Id)
        {
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    ViewBag.Orders = data.SpecificProductOrders(Id.ProductId);
                    return View("SpecificProductOrders");
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }

        [HttpPost]
        public ActionResult Delivered(Delivered Id)
        {
            if (Session["AdminId"] == null)
            {
                return View("PageNotFound");
            }
            else if (Session["AdminId"] != null)
            {
                if (Session["AdminId"].ToString().Equals("Admin12345"))
                {
                    if (data.Delivered(Id.OrderId) == true)
                    {
                        ViewBag.Orders = data.ViewOrders();
                        return View("AdminViewOrders");
                    }
                    else
                    {
                        return View("ServerError");
                    }
                }
                else
                {
                    return View("PageNotFound");
                }
            }
            else
            {
                return View("PageNotFound");
            }
        }
    }
}