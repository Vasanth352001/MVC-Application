function AddressValidate()
{
    if (namecheck() == true && phonenumbercheck() == true && DoorNumbercheck() == true &&
        Streatcheck() == true && Areacheck() == true && Districtcheck() == true &&
        pincodecheck()==true && statecheck() == true)
    {
        document.getElementById("personalbtn").disabled = false;
    }
    else
    {
        document.getElementById("personalbtn").disabled = true;
    }
}

function namecheck()
{
    var name = document.getElementById("name").value;
    if(name=="")
    {
        document.getElementById("nameerror").innerHTML = "Name is Required";
        document.getElementById("name").style.border = "2px solid red";
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
                document.getElementById("nameerror").innerHTML = "Not contains numbers ";
                document.getElementById("name").style.border = "2px solid red";
                return false;
            }
            else if(name.charAt(i)>='!' && name.charAt(i)<='@')
            {
                document.getElementById("nameerror").innerHTML = "Not contains special characters ";
                document.getElementById("name").style.border = "2px solid red";
                return false;
            }
        }
        if(count==name.length)
        {
            document.getElementById("nameerror").innerHTML = "";
            document.getElementById("name").style.border = "2px solid #33ff33";
            return true;
        }
        else
        {
            document.getElementById("name").style.border = "2px solid red";
            return false;
        }
    }
}

function phonenumbercheck()
{
    var phone = document.getElementById("phone").value;
    if(phone=="")
    {
        document.getElementById("errorphonenumber").innerHTML = "Phone Number is Required";
        document.getElementById("phone").style.border = "2px solid red";
        return false;
    }
    else
    {
        let count=0;
        for(let i=0;i<phone.length;i++)
        {
            if(!(phone.charAt(i)>='0' && phone.charAt(i)<='9'))
            {
                count++;
            }
        }
        if(phone.length==10)
        {
            if(count>0)
            {
                document.getElementById("errorphonenumber").innerHTML = "Phone Number contains only numbers";
                document.getElementById("phone").style.border = "2px solid red";
            }
            else
            {
                var phonepattern = /^([6-9]+){1}([0-9]+){9}$/;
                if(phonepattern.test(phone))
                {
                    document.getElementById("errorphonenumber").innerHTML = "";
                    document.getElementById("phone").style.border = "2px solid #33ff33";
                    return true;
                }
                else
                {
                    document.getElementById("errorphonenumber").innerHTML = "Phone Number starts with 9 or 8 or 7 or 6";
                    document.getElementById("phone").style.border = "2px solid red";
                    return false;
                }
            }
        }
        else
        {
            document.getElementById("errorphonenumber").innerHTML = "You have entered only " + phone.length + " digits";
            document.getElementById("phone").style.border = "2px solid red";
            return false;
        }
    }
}

function DoorNumbercheck()
{
    var doorno = document.getElementById("doorno").value;
    if (doorno == "")
    {
        document.getElementById("errordoorno").innerHTML = "Phone Number is Required";
        document.getElementById("doorno").style.border = "2px solid red";
        return false;
    }
    else
    {
        document.getElementById("errordoorno").innerHTML = "";
        document.getElementById("doorno").style.border = "2px solid #33ff33";
        return true;
    }
}

function Streatcheck()
{
    var streat = document.getElementById("streat").value;
    if (streat == "")
    {
        document.getElementById("errorstreat").innerHTML = "Phone Number is Required";
        document.getElementById("streat").style.border = "2px solid red";
        return false;
    }
    else
    {
        document.getElementById("errorstreat").innerHTML = "";
        document.getElementById("streat").style.border = "2px solid #33ff33";
        return true;
    }
}

function Areacheck()
{
    var area = document.getElementById("area").value;
    if (area == "")
    {
        document.getElementById("errorarea").innerHTML = "Phone Number is Required";
        document.getElementById("area").style.border = "2px solid red";
        return false;
    }
    else
    {
        document.getElementById("errorarea").innerHTML = "";
        document.getElementById("area").style.border = "2px solid #33ff33";
        return true;
    }
}

function Districtcheck()
{
    var district = document.getElementById("district").value;
    if (district == "District")
    {
        document.getElementById("errordistrict").innerHTML = "Phone Number is Required";
        document.getElementById("district").style.border = "2px solid red";
        return false;
    }
    else
    {
        document.getElementById("errordistrict").innerHTML = "";
        document.getElementById("district").style.border = "2px solid #33ff33";
        return true;
    }
}

function pincodecheck()
{
    var pincode = document.getElementById("pincode").value;
    if(pincode=="")
    {
        document.getElementById("errorpincode").innerHTML = "Phone Number is Required";
        document.getElementById("pincode").style.border = "2px solid red";
        return false;
    }
    else
    {
        if (pincode.length == 6)
        {
            if (numcheck(pincode) == true)
            {
                document.getElementById("errorpincode").innerHTML = "";
                document.getElementById("pincode").style.border = "2px solid #33ff33";
                return true;
            }
            else
            {
                document.getElementById("errorpincode").innerHTML = "Only number allowed";
                document.getElementById("pincode").style.border = "2px solid red";
                return false;
            }
        }
        else
        {
            document.getElementById("errorpincode").innerHTML = "You have entered only " + pincode.length + "numbers";
            document.getElementById("pincode").style.border = "2px solid red";
            return false;
        }
    }
}

function statecheck()
{
    var state = document.getElementById("state").value;
    if (state != "Tamilnadu")
    {
        document.getElementById("errorstate").innerHTML = "State is Required";
        document.getElementById("state").style.border = "2px solid red";
        return false;
    }
    else
    {
        document.getElementById("errorstate").innerHTML = "";
        document.getElementById("state").style.border = "2px solid #33ff33";
        return true;
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


