function validate()
{
    if (categorycheck() == true && namecheck() == true && pricecheck() == true && stockcheck() == true && offercheck() == true && urlcheck() == true)
    {
        document.getElementById("addbtn").disabled = false;
    }
    else
    {
        document.getElementById("addbtn").disabled = true;
    }
}

function namecheck()
{
    var name = document.getElementById("name").value;
    if(name=="")
    {
        document.getElementById("nameerror").innerHTML = "Cakename is Required";
        document.getElementById("name").style.border = "2px solid red";
        document.getElementById("nameval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
        return false;
    }
    else
    {
        let count=0;
        for(let i=0;i<name.length;i++)
        {
            if(name.charAt(i)>='a' && name.charAt(i)<='z')
            {
                count++;
            }
            else if (name.charAt(i)==' ')
            {
                count++;
            }
            else if(name.charAt(i)>='A' && name.charAt(i)<='Z')
            {
                count++;
            }
            else if(name.charAt(i)>='0' && name.charAt(i)<='9')
            {
                document.getElementById("nameerror").innerHTML = "Cakename not contains numbers ";
                document.getElementById("name").style.border = "2px solid red";
                document.getElementById("nameval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
                return false;
            }
            else if(name.charAt(i)>='!' && name.charAt(i)<='@')
            {
                document.getElementById("nameerror").innerHTML = "Cakename not contains special characters ";
                document.getElementById("name").style.border = "2px solid red";
                document.getElementById("nameval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
                return false;
            }
        }
        if(count==name.length)
        {
            document.getElementById("nameerror").innerHTML = "";
            document.getElementById("name").style.border = "2px solid #33ff33";
            document.getElementById("nameval").style.boxShadow = "0 0 5pt 0.5pt #33ff33";
            return true;
        }
        else
        {
            document.getElementById("name").style.border = "2px solid red";
            document.getElementById("nameval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
            return false;
        }
    }
}

function updatevalidate()
{
    if (categorycheck() == true && namecheck() == true && pricecheck() == true && stockcheck() == true && offercheck() == true)
    {
        document.getElementById("addbtn").disabled = false;
    }
    else
    {
        document.getElementById("addbtn").disabled = true;
    }
}

function categorycheck()
{
    var category = document.getElementById("category").value;
    if (category === "Category")
    {
        document.getElementById("category").style.border = "2px solid red";
        document.getElementById("category").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
        document.getElementById("categoryerror").innerHTML = "Category is required";
        return false;
    }
    else
    {
        document.getElementById("category").style.border = "2px solid #33ff33";
        document.getElementById("category").style.boxShadow = "0 0 5pt 0.5pt #33ff33";
        document.getElementById("categoryerror").innerHTML = "";
        return true;
    }
}

function pricecheck()
{
    var price = document.getElementById("price").value;
    if(price == "")
    {
        document.getElementById("priceerror").innerHTML = "Price is required";
        document.getElementById("price").style.border = "2px solid red";
        document.getElementById("priceval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
        return false;
    }
    else
    {
        if(numcheck(price)==true)
        {
            document.getElementById("priceerror").innerHTML = "";
            document.getElementById("price").style.border = "2px solid #33ff33";
            document.getElementById("priceval").style.boxShadow = "0 0 5pt 0.5pt #33ff33";
            return true; 
        }
        else
        {
            document.getElementById("priceerror").innerHTML = "Price only accept numbers";
            document.getElementById("price").style.border = "2px solid red";
            document.getElementById("priceval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
            return false;
        }
    }
}

function stockcheck()
{
    var stock = document.getElementById("stock").value;
    if(stock == "")
    {
        document.getElementById("stock").style.border = "2px solid red";
        document.getElementById("stockval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
        document.getElementById("stockerror").innerHTML="Stock is required";
        return false;
    }
    else
    {
        if(numcheck(stock)==true)
        {
            document.getElementById("stockerror").innerHTML = "";
            document.getElementById("stock").style.border = "2px solid #33ff33";
            document.getElementById("stockval").style.boxShadow = "0 0 5pt 0.5pt #33ff33";
            return true; 
        }
        else
        {
            document.getElementById("stock").style.border = "2px solid red";
            document.getElementById("stockval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
            document.getElementById("stockerror").innerHTML="Stock only accept numbers";
            return false;
        }
    }
}

function offercheck()
{
    var offer = document.getElementById("offer").value;
    if(offer == "")
    {
        document.getElementById("offer").style.border = "2px solid red";
        document.getElementById("offerval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
        document.getElementById("offererror").innerHTML="Offer is required";
        return false;
    }
    else
    {
        if(numcheck(offer)==true)
        {
            if (offer >= 0 && offer <= 100)
            {
                document.getElementById("offererror").innerHTML = "";
                document.getElementById("offer").style.border = "2px solid #33ff33";
                document.getElementById("offerval").style.boxShadow = "0 0 5pt 0.5pt #33ff33";
                return true;
            }
            else
            {
                document.getElementById("offer").style.border = "2px solid red";
                document.getElementById("offerval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
                document.getElementById("offererror").innerHTML = "Offer only accept between 0 to 100";
                return false;
            }
        }
        else
        {
            document.getElementById("offer").style.border = "2px solid red";
            document.getElementById("offerval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
            document.getElementById("offererror").innerHTML="Offer only accept numbers";
            return false;
        }
    }
}

function urlcheck()
{
    var url = document.getElementById("url").value;
    if(url == "")
    {
        document.getElementById("url").style.border = "2px solid red";
        document.getElementById("urlval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
        document.getElementById("urlerror").innerHTML="URL is required";
        return false;
    }
    else
    {
        if(url.length<=500)
        {
            document.getElementById("urlerror").innerHTML="Your URL length is "+url.length;
            document.getElementById("urlerror").style.color = "green";
            document.getElementById("url").style.border = "2px solid #33ff33";
            document.getElementById("urlval").style.boxShadow = "0 0 5pt 0.5pt #33ff33";
            return true;
        }
        else
        {
            document.getElementById("url").style.border = "2px solid red";
            document.getElementById("urlval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
            document.getElementById("urlerror").innerHTML="URL character is high.Only 500 characters allowed";
            return false;
        }
    }
}

function numcheck(input)
{
    let c=0;
    for(let i=0;i<input.length;i++)
    {
        if(input.charAt(i)>='0' && input.charAt(i)<='9')
        {
            c++;
        }
    }

    if(c==input.length)
    {
        return true;
    }
    else
    {
        return false;
    }
}

function updatevisible()
{
    if (document.getElementById("update").style.display == "block")
    {
        document.getElementById("update").style.display = "none";
    }
    else
    {
        document.getElementById("update").style.display = "block";
    }
}

function UpdateAll()
{
    if (document.getElementById("all").style.display == "block")
    {
        document.getElementById("all").style.display = "none";
    }
    else
    {
        document.getElementById("all").style.display = "block";
        document.getElementById("updatecategory").style.display = "none";
        document.getElementById("updatename").style.display = "none";
        document.getElementById("updateprice").style.display = "none";
        document.getElementById("updatestock").style.display = "none";
        document.getElementById("updateoffer").style.display = "none";
    }
}

function UpdateCategory()
{
    if (document.getElementById("updatecategory").style.display == "block")
    {
        document.getElementById("updatecategory").style.display = "none";
    }
    else
    {
        document.getElementById("all").style.display = "none";
        document.getElementById("updatecategory").style.display = "block";
        document.getElementById("updatename").style.display = "none";
        document.getElementById("updateprice").style.display = "none";
        document.getElementById("updatestock").style.display = "none";
        document.getElementById("updateoffer").style.display = "none";
    }
}

function UpdateName()
{
    if (document.getElementById("updatename").style.display == "block")
    {
        document.getElementById("updatename").style.display = "none";
    }
    else
    {
        document.getElementById("all").style.display = "none";
        document.getElementById("updatecategory").style.display = "none";
        document.getElementById("updatename").style.display = "block";
        document.getElementById("updateprice").style.display = "none";
        document.getElementById("updatestock").style.display = "none";
        document.getElementById("updateoffer").style.display = "none";
    }
}

function UpdatePrice()
{
    if (document.getElementById("updateprice").style.display == "block")
    {
        document.getElementById("updateprice").style.display = "none";
    }
    else
    {
        document.getElementById("all").style.display = "none";
        document.getElementById("updatecategory").style.display = "none";
        document.getElementById("updatename").style.display = "none";
        document.getElementById("updateprice").style.display = "block";
        document.getElementById("updatestock").style.display = "none";
        document.getElementById("updateoffer").style.display = "none";
    }
}

function UpdateStock()
{
    if (document.getElementById("updatestock").style.display == "block")
    {
        document.getElementById("updatestock").style.display = "none";
    }
    else
    {
        document.getElementById("all").style.display = "none";
        document.getElementById("updatecategory").style.display = "none";
        document.getElementById("updatename").style.display = "none";
        document.getElementById("updateprice").style.display = "none";
        document.getElementById("updatestock").style.display = "block";
        document.getElementById("updateoffer").style.display = "none";
    }
}

function UpdateOffer()
{
    if (document.getElementById("updateoffer").style.display == "block")
    {
        document.getElementById("updateoffer").style.display = "none";
    }
    else
    {
        document.getElementById("all").style.display = "none";
        document.getElementById("updatecategory").style.display = "none";
        document.getElementById("updatename").style.display = "none";
        document.getElementById("updateprice").style.display = "none";
        document.getElementById("updatestock").style.display = "none";
        document.getElementById("updateoffer").style.display = "block";
    }
}

