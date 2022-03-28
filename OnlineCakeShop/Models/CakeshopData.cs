using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineCakeShop.Models
{
    public class CakeshopData 
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public DateTime DOB { get; set; }
        public string Password { get; set; }
    }

    public class Login
    {
        public string Id { get; set; }
        public string Password { get; set; }
    }

    public class AddProduct
    {
        public string Category { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Stock { get; set; }
        public string Offer { get; set; }
        public string URL { get; set; }

    }

    public class EditProduct
    {
        public string URL { get; set; }
    }

    public class DeleteProduct
    {
        public string URL { get; set; }
    }

    public class UpdateProduct
    {
        public string CURURL { get; set; }
        public string UpdateCategory { get; set; }
        public string UpdateName { get; set; }
        public string UpdatePrice { get; set; }
        public string UpdateStock { get; set; }
        public string UpdateOffer { get; set; }
    }

    public class UpdateSingleValue
    {
        public string CURURL { get; set; }
        public string WhichValue { get; set; }
        public string UpdateValue { get; set; }
    }

    public class SingleProductDetails
    {
        public string URL { get; set; }
    }

    public class AddCart
    {
        public string URL { get; set; }
        public int Quantity { get; set; }
    }

    public class BuyProduct
    {
        public string URL { get; set; }
        public int Quantity { get; set; }
    }

    public class Address
    {
        public string Customername { get; set; }
        public string Phonenumber { get; set; }
        public string Doorno { get; set; }
        public string Streat { get; set; }
        public string Area { get; set; }
        public string District { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
    }

    public class SpecificCustomerOrder
    {
        public string CustomerId { get; set; } 
    }

    public class SpecificProductOrder
    {
        public string ProductId { get; set; }
    }

    public class Delivered
    {
        public string OrderId { get; set; }
    }

}