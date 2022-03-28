using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Collections;
using System.Data;

namespace OnlineCakeShop.DataAccess
{
    public class CRUDOperation
    {
        /*Other Classes Start*/

        SqlConnection connect = new SqlConnection("data source=LENOVO\\SQLEXPRESS; database=Authentication; MultipleActiveResultSets=True ; integrated security = SSPI");
        SqlCommand command = new SqlCommand();

        /*Other Classes End*/

        public Boolean Gmail(string Email,string Phonenumber)
        {
            if (Emailcheck(Email) == true && Phonenumbercheck(Phonenumber) == true)
            {
                MailMessage mm = new MailMessage("vasanthr352001@gmail.com", Email);
                mm.Subject = "Yummy Cakes";
                mm.Body = "https://localhost:44399/Home/ConfirmationMail";
                mm.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;

                NetworkCredential nc = new NetworkCredential("vasanthr352001@gmail.com", "Vasanthr@3");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = nc;
                smtp.Send(mm);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string Register(string Name, string Email, string Phonenumber, DateTime DOB, string Password)
        {
            if (Emailcheck(Email) == true && Phonenumbercheck(Phonenumber) == true)
            {
                try
                {
                    connect.Open();
                    command.Connection = connect;
                    command.CommandText = "insert into Register values('" + IdCreate(Name, Phonenumber) + "','" + Name + "','" + Email + "','" + Phonenumber + "','" + DOB.ToString("yyyy-MM-dd") + "','" + Password + "')";
                    command.ExecuteNonQuery();
                    connect.Close();
                    MailMessage mm = new MailMessage("vasanthr352001@gmail.com", Email);
                    mm.Subject = "Yummy Cakes";
                    mm.Body = "Welome to Yummy Cakes "+Name+"\r\n\r\n"+"Your Login Id is "+IdCreate(Name, Phonenumber);
                    mm.IsBodyHtml = false;

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    NetworkCredential nc = new NetworkCredential("vasanthr352001@gmail.com", "Vasanthr@3");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = nc;
                    smtp.Send(mm);
                    return "Registered";
                }
                catch (SqlException MysqlCount)
                {
                    connect.Close();
                    return MysqlCount.ToString();
                }
            }
            else
            {
                return "Exist";
            }
        }

        public string Login(string Id, string Password)
        {
            try
            {
                connect.Open();
                string Query = "SELECT Id,Name FROM Register WHERE Id='" + Id + "' and Password='" + Password + "'";
                SqlCommand command = new SqlCommand(Query, connect);
                SqlDataReader Det = command.ExecuteReader();
                int CountOfDuplicate = 0;
                string Name = "";
                while (Det.Read())
                {
                    Name = Det["Name"].ToString();
                    CountOfDuplicate++;
                }
                if (CountOfDuplicate == 1)
                {
                    connect.Close();
                    return "Success";
                }
                else
                {
                    connect.Close();
                    return "Error";
                }
            }
            catch (SqlException MysqlCount)
            {
                connect.Close();
                return "ServerError";
            }
        }

        public string ProductId(string Category)
        {
            int count = 0;
            try
            {
                connect.Open();
                string Query = "SELECT * FROM Products WHERE Category='"+Category+"'";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Det = ExeQuery.ExecuteReader();
                while (Det.Read())
                {
                    count++;
                }
                connect.Close();
                if(Category.Equals("Chocolate_Cake"))
                {
                    return ((1110 + count) + 1).ToString();
                }
                else if(Category.Equals("BlackForest_Cake"))
                {
                    return ((2210 + count) + 1).ToString();
                }
                else if (Category.Equals("Icecream_Cake"))
                {
                    return ((3310 + count) + 1).ToString();
                }
                else if (Category.Equals("Butterscotch_Cake"))
                {
                    return ((4410 + count) + 1).ToString();
                }
                else if (Category.Equals("Cup_Cake"))
                {
                    return ((5510 + count) + 1).ToString();
                }
                else
                {
                    return ((6610 + count) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                return "Count" + ex.ToString();
            }
            finally
            {
                connect.Close();
            }
        }

        public string AddProduct(string Category, string Name, string Price, string Offer, string Stock, string URL)
        {
            if (ItemDuplicateCheck(URL)==true)
            {
                string Add_Date = DateTime.Now.ToString("yyyy-MM-dd");
                try
                {
                    string Query = "insert into Products(URL,Name,Category,Price,Offer,Stock,Added_Date,Id) values('" + URL + "','" + Name + "','"+ Category +"','" + Price + "','" + Offer + "','" + Stock + "','" + Add_Date + "','"+ProductId(Category)+"')";
                    connect.Open();
                    command.Connection = connect;
                    SqlCommand ExeQuery = new SqlCommand(Query, connect);
                    SqlDataReader Det = ExeQuery.ExecuteReader();
                    connect.Close();
                    return "Inserted";
                }
                catch (SqlException MysqlCount)
                {
                    return "ServerError";
                }
            }
            else
            {
                return "Exist";
            }
        }

        public SqlDataReader AdminViewProduct()
        {
            connect.Open();
            string Query = "SELECT * FROM Products WHERE Category='Chocolate_Cake' ORDER BY Added_Date DESC";
            SqlCommand command = new SqlCommand(Query, connect);
            SqlDataReader Det = command.ExecuteReader();
            return Det;
        }

        public string UpdateProduct(string CURURL,string Category, string Name, string Price, string Offer, string Stock)
        {
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "UPDATE Products SET Name='" + Name + "',Category='" + Category + "',Price='" + Price + "',Offer='" + Offer + "',Stock='" + Stock + "' WHERE URL='" + CURURL + "'";
                command.ExecuteNonQuery();
                connect.Close();
                return "Updated";
            }
            catch (SqlException MysqlCount)
            {
                connect.Close();
                return "ServerError";
            }
        }

        public Boolean UpdateSingleValue(string Url,string Value,string Field)
        {
            if(Field.Equals("Category"))
            {
                connect.Open();
                string Query = "UPDATE [Products] SET Category ='" + Value + "' WHERE URL='" + Url + "'";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Vieworder = ExeQuery.ExecuteReader();
                connect.Close();
                return true;
            }
            else if (Field.Equals("Name"))
            {
                connect.Open();
                string Query = "UPDATE [Products] SET Name ='" + Value + "' WHERE URL='" + Url + "'";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Vieworder = ExeQuery.ExecuteReader();
                connect.Close();
                return true;
            }
            else if (Field.Equals("Price"))
            {
                connect.Open();
                string Query = "UPDATE [Products] SET Price ='" + Value + "' WHERE URL='" + Url + "'";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Vieworder = ExeQuery.ExecuteReader();
                connect.Close();
                return true;
            }
            else if (Field.Equals("Stock"))
            {
                connect.Open();
                string Query = "UPDATE [Products] SET Stock ='" + Value + "' WHERE URL='" + Url + "'";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Vieworder = ExeQuery.ExecuteReader();
                connect.Close();
                return true;
            }
            else if (Field.Equals("Offer"))
            {
                connect.Open();
                string Query = "UPDATE [Products] SET Offer ='" + Value + "' WHERE URL='" + Url + "'";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Vieworder = ExeQuery.ExecuteReader();
                connect.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean DeleteProduct(string URL)
        {
            connect.Open();
            string Query = "DELETE FROM Products WHERE URL='" + URL + "'";
            SqlCommand command = new SqlCommand(Query, connect);
            command.ExecuteNonQuery();
            connect.Close();
            return true;
        }

        public SqlDataReader Products()
        {
            connect.Open();
            string Query = "SELECT TOP(28) [URL],[Name],[Price],[Offer],[Stock] FROM Products WHERE NOT(Stock<=0) ORDER BY Added_Date DESC";
            SqlCommand DataQuery = new SqlCommand(Query, connect);
            SqlDataReader Det = DataQuery.ExecuteReader();
            return Det;
        }

        public SqlDataReader SortByCategory(string Category)
        {
            connect.Open();
            string Query = "SELECT TOP(20) [URL],[Name],[Price],[Offer],[Stock] FROM Products WHERE NOT(Stock<=0) AND Category='"+Category+"' ORDER BY Added_Date DESC";
            SqlCommand DataQuery = new SqlCommand(Query, connect);
            SqlDataReader Det = DataQuery.ExecuteReader();
            return Det;
        }

        public SqlDataReader SortByPrice(string AboveYear,string BelowYear)
        {
            connect.Open();
            string Query = "SELECT TOP(20) [URL],[Name],[Price],[Offer],[Stock] FROM Products WHERE NOT(Stock<=0) AND (Price>='"+AboveYear+"' AND Price<='"+BelowYear+"') ORDER BY Added_Date DESC";
            SqlCommand DataQuery = new SqlCommand(Query, connect);
            SqlDataReader Det = DataQuery.ExecuteReader();
            return Det;
        }

        public SqlDataReader AdminSortByCategory(string Category)
        {
            connect.Open();
            string Query = "SELECT * FROM Products WHERE NOT(Stock<=0) AND Category='" + Category + "' ORDER BY Added_Date DESC";
            SqlCommand DataQuery = new SqlCommand(Query, connect);
            SqlDataReader Det = DataQuery.ExecuteReader();
            return Det;
        }

        string Name = "";
        string Price = "";
        string Offer = "";
        public string CartGetDetails(string UserId, string URL)
        {
            if (CartDuplicateCheck(UserId, URL) == true)
            {
                try
                {
                    connect.Open();
                    string Query = "SELECT * FROM Products WHERE URL='" + URL + "'";
                    SqlCommand command = new SqlCommand(Query, connect);
                    SqlDataReader Det = command.ExecuteReader();
                    while (Det.Read())
                    {
                        Name = Det["Name"].ToString();
                        Price = Det["Price"].ToString();
                        Offer = Det["Offer"].ToString();
                    }
                    connect.Close();
                    return "Details";
                }
                catch (SqlException MysqlCount)
                {
                    connect.Close();
                    return "ServerError";
                }
            }
            else
            {
                return "Exist";
            }
        }

        public SqlDataReader Cart(string UserId)
        {
            connect.Open();
            string Query = "SELECT * FROM Cart WHERE UserId='" + UserId + "' ORDER BY AddedDate DESC";
            SqlCommand command = new SqlCommand(Query, connect);
            SqlDataReader Det = command.ExecuteReader();
            return Det;
        }

        public string AddCart(string UserId, string URL, int Quantity)
        {
            string Add_Date = DateTime.Now.ToString("yyyy-MM-dd");
            if (CartGetDetails(UserId, URL).Equals("Details"))
            {
                try
                {
                    connect.Open();
                    command.Connection = connect;
                    command.CommandText = "insert into Cart values('" + UserId + "','" + URL + "','" + Name + "','" + Price + "','" + Offer + "','" + Quantity + "','" + Add_Date + "')";
                    command.ExecuteNonQuery();
                    connect.Close();
                    return "CartAdded";
                }
                catch (SqlException MysqlCount)
                {
                    connect.Close();
                    return "ServerError";
                }
            }
            else if (CartGetDetails(UserId, URL).Equals("Exist"))
            {
                return "Exist";
            }
            else
            {
                return "ServerError";
            }
        }

        public string RemoveCartProduct(string UserId, string Url)
        {
            try
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = "DELETE FROM Cart WHERE UserId='" + UserId + "' AND Url='" + Url + "'";
                command.ExecuteNonQuery();
                connect.Close();
                return "Removed";
            }
            catch (SqlException MysqlCount)
            {
                connect.Close();
                return "ServerError";
            }
        }

        public Boolean Emailcheck(string Email)
        {
            try
            {
                connect.Open();
                string Query = "SELECT Email FROM Register WHERE Email='" + Email + "'";
                SqlCommand command = new SqlCommand(Query, connect);
                SqlDataReader Det = command.ExecuteReader();
                int CountOfDuplicate = 0;
                while (Det.Read())
                {
                    CountOfDuplicate++;
                }
                if (CountOfDuplicate == 0)
                {
                    connect.Close();
                    return true;
                }
                else
                {
                    connect.Close();
                    return false;
                }
            }
            catch (SqlException MysqlCount)
            {
                connect.Close();
                return false;
            }
        }

        public Boolean Phonenumbercheck(string Phonenumber)
        {
            try
            {
                connect.Open();
                string Query = "SELECT Phonenumber FROM Register WHERE Phonenumber='" + Phonenumber + "'";
                SqlCommand command = new SqlCommand(Query, connect);
                SqlDataReader Det = command.ExecuteReader();
                int CountOfDuplicate = 0;
                while (Det.Read())
                {
                    CountOfDuplicate++;
                }
                if (CountOfDuplicate == 0)
                {
                    connect.Close();
                    return true;
                }
                else
                {
                    connect.Close();
                    return false;
                }
            }
            catch (SqlException MysqlCount)
            {
                connect.Close();
                return false;
            }
        }

        public string IdCreate(string Name, string Phonenumber)
        {
            string idname = "";
            for (int i = 0; i < Name.Length; i++)
            {
                if (Name[i] != ' ')
                {
                    idname += Name[i];
                }
            }
            string phone = "";
            for (int i = 5; i < Phonenumber.Length; i++)
            {
                phone += Phonenumber[i];
            }
            string Id = idname + phone;
            return Id;
        }

        public string Username(string Id)
        {
            string Name = "";
            for (int i = 0; i < Id.Length - 5; i++)
            {
                Name += Id[i];
            }
            return Name;
        }

        public Boolean ItemDuplicateCheck(string URL)
        {
            int CountOfDuplicate = 0;
            try
            {
                string Query = "SELECT URL FROM Products WHERE URL='" + URL + "'";
                connect.Close();
                connect.Open();
                SqlCommand command = new SqlCommand(Query, connect);
                SqlDataReader Det = command.ExecuteReader();
                while (Det.Read())
                {
                    CountOfDuplicate++;
                }
                connect.Close();
                if(CountOfDuplicate==0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException MysqlCount)
            {
                return false;
            }
            finally
            {
                connect.Close();
            }
        }

        public Boolean CartDuplicateCheck(string UserId, string URL)
        {
            try
            {
                connect.Open();
                string Query = "SELECT URL FROM Cart WHERE URL='" + URL + "' AND UserId='" + UserId + "'";
                SqlCommand command = new SqlCommand(Query, connect);
                SqlDataReader Det = command.ExecuteReader();
                int CountOfDuplicate = 0;
                while (Det.Read())
                {
                    CountOfDuplicate++;
                }
                if (CountOfDuplicate == 0)
                {
                    connect.Close();
                    return true;
                }
                else
                {
                    connect.Close();
                    return false;
                }
            }
            catch (SqlException MysqlCount)
            {
                connect.Close();
                return false;
            }
        }

        public List<string> SingleProduct(string URL)
        {
            List<string> single = new List<string>();
            try
            {
                connect.Open();
                string Query = "SELECT * FROM Products WHERE URL='" + URL + "'";
                SqlCommand command = new SqlCommand(Query, connect);
                SqlDataReader Det = command.ExecuteReader();
                while (Det.Read())
                {
                    single.Add(Det["Url"].ToString()); //0
                    single.Add(Det["Name"].ToString());//1
                    single.Add(Det["Price"].ToString());//2
                    single.Add(Det["Stock"].ToString());//3
                    single.Add(Det["Offer"].ToString());//4
                    single.Add(Det["Category"].ToString());//5
                }
                connect.Close();
                return single;
            }
            catch(Exception ex)
            {
                single.Add(ex.ToString());
                return single;
            }
            finally
            {
                connect.Close();
            }
        }

        public SqlDataReader RelatedProduct(string URL,string Category)
        {
            connect.Open();
            string Query = "SELECT TOP(10) * FROM Products WHERE NOT(URL='" + URL + "') AND Category='" + Category + "'";
            SqlCommand command = new SqlCommand(Query, connect);
            SqlDataReader Det = command.ExecuteReader();
            return Det;
        }

        public List<string> BuyCakeDetails(string URL)
        {
            List<string> buycakedet = new List<string>();
            try
            {
                connect.Open();
                string Query = "SELECT * FROM Products WHERE URL='" + URL + "'";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Det = ExeQuery.ExecuteReader();
                while (Det.Read())
                {
                    buycakedet.Add(Det["Url"].ToString());//0
                    buycakedet.Add(Det["Name"].ToString());//1
                    buycakedet.Add(Det["Price"].ToString());//2
                    buycakedet.Add(Det["Offer"].ToString());//3
                    buycakedet.Add(Det["Id"].ToString());//4
                }
                connect.Close();
                return buycakedet;
            }
            catch(Exception ex)
            {
                buycakedet.Add("Details");
                return buycakedet;
            }
            finally
            {
                connect.Close();
            }
        }

        public Boolean UpdateStock(string ProductId,int Quantity)
        {
            connect.Open();
            command.Connection = connect;
            command.CommandText = "UPDATE [Products] SET Stock=Stock-'"+Quantity+"' WHERE Id='" + ProductId + "'";
            command.ExecuteNonQuery();
            connect.Close();
            return true;
        }

        public string OrderId()
        {
            int count = 0;
            try
            {
                connect.Open();
                string Query = "SELECT * FROM [Order]";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Det = ExeQuery.ExecuteReader();
                while (Det.Read())
                {
                    count++;
                }
                connect.Close();
                return ((7140+count)+1).ToString();
            }
            catch (Exception ex)
            {
                return "Count"+ex.ToString();
            }
            finally
            {
                connect.Close();
            }
        }

        public string Order(string URL,string OrderId,string P_Name,int P_Price,int Quantity,string P_Id,string C_Id,string C_Phonenumber,string C_Name,string Doorno,string Streat,string Area,string District,string Pincode,string State,string P_Method)
        {
            var today = DateTime.Today;
            var tommorrow = today.AddDays(1);
            string Order_Date = DateTime.Now.ToString("yyyy-MM-dd");
            string Delivery_Date = tommorrow.ToString("yyyy-MM-dd");
            if (P_Method.Equals("Cash"))
            {
                try
                {
                    connect.Open();
                    command.Connection = connect;
                    command.CommandText = "insert into [Order]" +
                        "(Product_URL,Order_Id,Product_Id,Product_Name,Product_Price,Product_Quantity,Customer_Id,Customer_Phonenumber,Customer_Name,DoorNo,Streat,Area,District,Pincode,State,Payment_Status,Order_Status,Order_Date,Delivery_Date)" +
                        " values('"+URL+"','"+OrderId+"','"+P_Id+"','"+P_Name+ "','"+P_Price+ "','"+Quantity+"','"+C_Id+ "','"+C_Phonenumber+ "','"+C_Name+ "','"+Doorno+ "','"+Streat+ "','"+Area+ "','"+District+ "','"+Pincode+ "','"+State+ "','NotPaid','WIP','"+Order_Date+ "','"+Delivery_Date+"')";
                    command.ExecuteNonQuery();
                    connect.Close();
                    return "true";
                }
                catch (Exception ex)
                {
                    return "insert"+ex.ToString();
                }
                finally
                {
                    connect.Close();
                }
            }
            else
            {
                try
                {
                    connect.Open();
                    command.Connection = connect;
                    command.CommandText = "insert into [Order]" +
                        "(Product_URL,Order_Id,Product_Id,Product_Name,Product_Price,Product_Quantity,Customer_Id,Customer_Phonenumber,Customer_Name,DoorNo,Streat,Area,District,Pincode,State,Payment_Status,Order_Status,Order_Date,Delivery_Date)" +
                        " values('"+URL+"','"+OrderId+"','"+P_Id+"','" + P_Name + "','" + P_Price + "','" + Quantity + "','" + C_Id + "','" + C_Phonenumber + "','" + C_Name + "','" + Doorno + "','" + Streat + "','" + Area + "','" + District + "','" + Pincode + "','" + State + "','Paid','WIP','" + Order_Date + "','" + Delivery_Date + "')";
                    command.ExecuteNonQuery();
                    connect.Close();
                    return "true";
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public SqlDataReader ViewOrders()
        {
            connect.Open();
            string Query = "SELECT * FROM [Order] WHERE Order_Status='WIP'";
            SqlCommand ExeQuery = new SqlCommand(Query, connect);
            SqlDataReader Vieworder = ExeQuery.ExecuteReader();
            return Vieworder;
        }

        public SqlDataReader ViewPastOrders()
        {
            connect.Open();
            string Query = "SELECT * FROM [Order] WHERE Order_Status='Delivered' OR Order_Status='Returned'";
            SqlCommand ExeQuery = new SqlCommand(Query, connect);
            SqlDataReader Vieworder = ExeQuery.ExecuteReader();
            return Vieworder;
        }

        public SqlDataReader ViewCustomers()
        {
            connect.Open();
            string Query = "SELECT * FROM [Register]";
            SqlCommand ExeQuery = new SqlCommand(Query, connect);
            SqlDataReader ViewCustomer = ExeQuery.ExecuteReader();
            return ViewCustomer;
        }

        public SqlDataReader SpecificCustomerOrders(string CustomerId)
        {
            connect.Open();
            string Query = "SELECT * FROM [Order] WHERE Customer_Id='"+CustomerId+"'";
            SqlCommand ExeQuery = new SqlCommand(Query, connect);
            SqlDataReader Orders = ExeQuery.ExecuteReader();
            return Orders;
        }

        public SqlDataReader SpecificProductOrders(string ProductId)
        {
            connect.Open();
            string Query = "SELECT * FROM [Order] WHERE Product_Id='" + ProductId + "'";
            SqlCommand ExeQuery = new SqlCommand(Query, connect);
            SqlDataReader Orders = ExeQuery.ExecuteReader();
            return Orders;
        }

        public Boolean Delivered(string OrderId)
        {
            try
            {
                connect.Open();
                string Query = "UPDATE [Order] SET Order_Status='Delivered' WHERE Order_Id='" + OrderId + "'";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Vieworder = ExeQuery.ExecuteReader();
                connect.Close();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public SqlDataReader FeaturedProducts()
        {
            SqlDataReader Orders;
            connect.Open();
            string Query = "SELECT TOP(3) [URL],[Name],[Price],[Offer] FROM Cart ORDER BY AddedDate DESC";
            SqlCommand ExeQuery = new SqlCommand(Query, connect);
            Orders = ExeQuery.ExecuteReader();
            return Orders;
        }

        public SqlDataReader LatestProducts()
        {
            try
            {
                connect.Open();
                string Query = "SELECT TOP(4) [URL],[Name],[Price],[Offer],[Stock] FROM Products WHERE NOT(Stock<=0) ORDER BY Added_Date DESC";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Orders = ExeQuery.ExecuteReader();
                return Orders;
            }
            catch(InvalidOperationException ex)
            {
                string Query = "SELECT TOP(4) [URL],[Name],[Price],[Offer],[Stock] FROM Products WHERE NOT(Stock<=0) ORDER BY Added_Date DESC";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Orders = ExeQuery.ExecuteReader();
                return Orders;
            }
        }

        public List<string> OfferProducts()
        {
            List<string> Offer = new List<string>();
            try
            {
                connect.Open();
                string Query = "SELECT TOP(4) [URL],[Name],[Price],[Offer],[Stock] FROM Products WHERE NOT(Stock<=0) ORDER BY Offer DESC";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Orders = ExeQuery.ExecuteReader();
                while (Orders.Read())
                {
                    Offer.Add(Orders["Url"].ToString());//0
                    Offer.Add(Orders["Name"].ToString());//1
                    Offer.Add(Orders["Price"].ToString());//2
                    Offer.Add(Orders["Offer"].ToString());//3
                    Offer.Add(Orders["Stock"].ToString());//4
                }
                return Offer;
            }
            catch (InvalidOperationException ex)
            {
                string Query = "SELECT TOP(4) [URL],[Name],[Price],[Offer],[Stock] FROM Products WHERE NOT(Stock<=0) ORDER BY Offer DESC";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Orders = ExeQuery.ExecuteReader();
                while (Orders.Read())
                {
                    Offer.Add(Orders["Url"].ToString());//0
                    Offer.Add(Orders["Name"].ToString());//1
                    Offer.Add(Orders["Price"].ToString());//2
                    Offer.Add(Orders["Offer"].ToString());//3
                    Offer.Add(Orders["Stock"].ToString());//4
                }
                return Offer;
            }
        }

        public List<string> Account(string Id)
        {
            List<string> CustomerDetails = new List<string>();
            try
            {
                connect.Open();
                string Query = "SELECT * FROM Register WHERE Id='" + Id + "'";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Det = ExeQuery.ExecuteReader();
                while (Det.Read())
                {
                    CustomerDetails.Add(Det["Id"].ToString());//0
                    CustomerDetails.Add(Det["Name"].ToString());//1
                    CustomerDetails.Add(Det["Email"].ToString());//2
                    CustomerDetails.Add(Det["Phonenumber"].ToString());//3
                    CustomerDetails.Add(Det["DOB"].ToString());//4
                }
                connect.Close();
                return CustomerDetails;
            }
            catch (Exception ex)
            {
                CustomerDetails.Add("Details");
                return CustomerDetails;
            }
            finally
            {
                connect.Close();
            }
        }

        public int CartCount(string UserId)
        {
            int Count = 0;
            SqlDataReader CartData = Cart(UserId);
            while(CartData.Read())
            {
                Count++;
            }
            return Count;
        }

        public int OrderCount(string UserId,string Status)
        {
            connect.Close();
            connect.Open();
            string Query = "SELECT * FROM [Order] WHERE Customer_Id='" + UserId + "' AND Order_Status='"+Status+"'";
            SqlCommand ExeQuery = new SqlCommand(Query, connect);
            SqlDataReader Orders = ExeQuery.ExecuteReader();
            int count = 0;
            while(Orders.Read())
            {
                count++;
            }
            return count;
        }

        public SqlDataReader YourOrder(string Id)
        {
            connect.Open();
            string Query = "SELECT * FROM [Order] WHERE Customer_Id='" + Id + "' AND Order_Status='WIP'";
            SqlCommand ExeQuery = new SqlCommand(Query, connect);
            SqlDataReader Orders = ExeQuery.ExecuteReader();
            return Orders;
        }

        public SqlDataReader PastOrder(string Id)
        {
            connect.Open();
            string Query = "SELECT * FROM [Order] WHERE Customer_Id='" + Id + "' AND Order_Status='Delivered'";
            SqlCommand ExeQuery = new SqlCommand(Query, connect);
            SqlDataReader Orders = ExeQuery.ExecuteReader();
            return Orders;
        }

        public string CustomerCount()
        {
            int count = 0;
            try
            {
                connect.Open();
                string Query = "SELECT * FROM Register";
                SqlCommand command = new SqlCommand(Query, connect);
                SqlDataReader Det = command.ExecuteReader();
                while (Det.Read())
                {
                    count++;
                }
                return count.ToString();
            }
            catch(Exception exe)
            {
                return exe.ToString();
            }
            finally
            {
                connect.Close();
            }
        }

        public string ProductCount()
        {
            int count = 0;
            try
            {
                connect.Open();
                string Query = "SELECT * FROM Products";
                SqlCommand command = new SqlCommand(Query, connect);
                SqlDataReader Det = command.ExecuteReader();
                while (Det.Read())
                {
                    count++;
                }
                return count.ToString();
            }
            catch (Exception exe)
            {
                return exe.ToString();
            }
            finally
            {
                connect.Close();
            }
        }

        public string OrderCount()
        {
            int count = 0;
            try
            {
                connect.Open();
                string Query = "SELECT Order_Id FROM [Order] WHERE Order_Status='WIP'";
                SqlCommand command = new SqlCommand(Query, connect);
                SqlDataReader Det = command.ExecuteReader();
                while (Det.Read())
                {
                    count++;
                }
                return count.ToString();
            }
            catch (Exception exe)
            {
                return exe.ToString();
            }
            finally
            {
                connect.Close();
            }
        }

        public string RetriveCount()
        {
            var today = DateTime.Today;
            string count = "";
            try
            {
                connect.Open();
                string Query = "SELECT * FROM [Viewers] WHERE Date='" + today.ToString("yyyy-MM-dd") + "'";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Det = ExeQuery.ExecuteReader();
                while (Det.Read())
                {
                    count = Det["Count"].ToString();
                }
                connect.Close();
                return count;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
            finally
            {
                connect.Close();
            }
        }

        public void AddCount()
        {
            var today = DateTime.Today;
            int count = Int32.Parse(RetriveCount());
            count = count + 1;
            connect.Open();
            command.Connection = connect;
            command.CommandText = "Update Viewers Set Count='" + count + "' Where Date='" + today.ToString("yyyy-MM-dd") + "'";
            command.ExecuteNonQuery();
            connect.Close();
        }

        public Boolean TruncateViewers()
        {
            connect.Open();
            command.Connection = connect;
            command.CommandText = "delete from Viewers where not(month(Date)=12 and (DAY(Date)>=25 and Day(Date)<=31))";
            command.ExecuteNonQuery();
            connect.Close();
            return true;
        }

        public Boolean LeapYearCheck(int Year)
        {
            if(((Year%4==0) && (Year%100!=0))||(Year%400==0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean OneYearDate()
        {
            if(TruncateViewers()==true)
            {
                var today = DateTime.Today;
                string query = "insert into Viewers values";
                if (LeapYearCheck(today.Year) == true)
                {
                    for (int i = 0; i < 366; i++)
                    {
                        if (i < 365)
                        {
                            query += "(0,'" + today.ToString("yyyy-MM-dd") + "')," + "\r\n\r\n";
                            var tom = today.AddDays(1);
                            today = tom;
                        }
                        else
                        {
                            query += "(0,'" + today.ToString("yyyy-MM-dd") + "')" + "\r\n\r\n";
                            var tom = today.AddDays(1);
                            today = tom;
                        }
                    }
                    query += ";";
                }
                else
                {
                    for (int i = 0; i < 365; i++)
                    {
                        if (i < 364)
                        {
                            query += "(0,'" + today.ToString("yyyy-MM-dd") + "')," + "\r\n\r\n";
                            var tom = today.AddDays(1);
                            today = tom;
                        }
                        else
                        {
                            query += "(0,'" + today.ToString("yyyy-MM-dd") + "')" + "\r\n\r\n";
                            var tom = today.AddDays(1);
                            today = tom;
                        }
                    }
                    query += ";";
                }
                try
                {
                    connect.Open();
                    command.Connection = connect;
                    SqlCommand ExeQuery = new SqlCommand(query, connect);
                    SqlDataReader Det = ExeQuery.ExecuteReader();
                    connect.Close();
                    return true;
                }
                catch (SqlException MysqlCount)
                {
                    return false;
                }
                finally
                {
                    connect.Close();
                }
            }
            else
            {
                return false;
            }
        }

        public Boolean AddCountData()
        {
            var today = DateTime.Today;
            if (today.Day == 01 && today.Month == 01)
            {
                OneYearDate();
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<string> GraphData()
        {
            AddCountData();
            List<string> DataCount = new List<string>();
            var today = DateTime.Today;
            try
            {
                connect.Open();
                string Query = "SELECT Top 5 Count,Date FROM [Viewers] where Date<='"+today.ToString("yyyy-MM-dd")+"' Order by Date Desc;";
                SqlCommand ExeQuery = new SqlCommand(Query, connect);
                SqlDataReader Det = ExeQuery.ExecuteReader();
                while (Det.Read())
                {
                    DataCount.Add(Det["Count"].ToString()+" "+ Det["Date"].ToString());
                }
                connect.Close();
                return DataCount;
            }
            catch (Exception ex)
            {
                DataCount.Add("Error");
                DataCount.Add("Error");
                return DataCount;
            }
            finally
            {
                connect.Close();
            }
        }

        public int CalculatePercentage(string count)
        {
            int per = Int32.Parse(count) / 2;
            if (per >= 90)
            {
                per = 90;
            }
            return per;
        }

        public string DayFind(string Date)
        {
            DateTime Datecon = Convert.ToDateTime(Date);
            string Day = Datecon.DayOfWeek.ToString();
            return Day;
        }

        public SqlDataReader StockCheck()
        {
            connect.Open();
            string Query = "SELECT * FROM [Products] WHERE Stock=0 Order by Added_Date Desc";
            SqlCommand command = new SqlCommand(Query, connect);
            SqlDataReader Det = command.ExecuteReader();
            return Det;
        }

    }
}