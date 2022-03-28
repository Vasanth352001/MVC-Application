function checkval()
{
    var password = document.getElementById("pword");
    var confirmpassword = document.getElementById("cpword");

    if(password.type === "password" && confirmpassword.type === "password")
    {
        password.type="text";
        confirmpassword.type="text";
    }
    else
    {
        password.type="password";
        confirmpassword.type="password";
    }
}

function validate()
{
    if (namecheck() == true && emailcheck() == true && phonenumbercheck() == true && dobcheck() == true && passwordcheck() == true && confirmpasswordcheck() == true)
    {
        document.getElementById("regbtn").disabled = false;
    }
}

function namecheck()
{
    var name = document.getElementById("name").value;
    if(name=="")
    {
        document.getElementById("errorname").innerHTML = "Username is Required";
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
            else if(name.charAt(i)>='A' && name.charAt(i)<='Z')
            {
                count++;
            }
            else if(name.charAt(i)>='0' && name.charAt(i)<='9')
            {
                document.getElementById("errorname").innerHTML = "Username not contains numbers ";
                document.getElementById("name").style.border = "2px solid red";
                return false;
            }
            else if(name.charAt(i)>='!' && name.charAt(i)<='@')
            {
                document.getElementById("errorname").innerHTML = "Username not contains special characters ";
                document.getElementById("name").style.border = "2px solid red";
                return false;
            }
        }
        if(count==name.length)
        {
            document.getElementById("errorname").innerHTML = "";
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

function emailcheck()
{
    var email = document.getElementById("email").value;
    if(email=="")
    {
        document.getElementById("erroremail").innerHTML = "Email is Required";
        document.getElementById("email").style.border = "2px solid red";
        return false;
    }
    else
    {
        var emailpattern = /^([a-zA-Z0-9]+){3,20}@([a-zA-Z]+){3,15}\.([a-zA-Z]){2,4}$/;
        var collagepattern = /^([a-zA-Z0-9]+){3,20}@([a-zA-Z]+){3,15}\.([a-zA-Z]){2,4}\.([a-zA-Z]){2,4}$/;
        if(emailpattern.test(email) || collagepattern.test(email))
        {
            document.getElementById("erroremail").innerHTML = "";
            document.getElementById("email").style.border = "2px solid #33ff33";
            return true;
        }
        else
        {
            document.getElementById("erroremail").innerHTML = "Email Id is not formate";
            document.getElementById("email").style.border = "2px solid red";
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

function dobcheck()
{
    var dob = document.getElementById("dob").value;
    if(dob=="")
    {
        document.getElementById("errordob").innerHTML = "Date Of Birth is Required";
        document.getElementById("dob").style.border = "2px solid red";
        return false;
    }
    else
    {
        var dateofbirth = new Date(dob);
        var month_diff = Date.now()-dateofbirth.getTime();
        var age_dt = new Date(month_diff);
        var year = age_dt.getUTCFullYear();
        var age = Math.abs(year - 1970);
        if(age>=18)
        {
            document.getElementById("errordob").innerHTML = "";
            document.getElementById("dob").style.border = "2px solid #33ff33";
            return true;
        }
        else
        {
            document.getElementById("errordob").innerHTML = "Your age is below 18";
            document.getElementById("dob").style.border = "2px solid red";
            return false;
        }
    }
}

function passwordcheck()
{
    var password = document.getElementById("pword").value;
    if(password=="")
    {
        document.getElementById("errorpassword").innerHTML = "Password is Required";
        document.getElementById("pword").style.border = "2px solid red";
        return false;
    }
    else
    {
        let capitalletter=0;
        let specialcharacter=0;
        let number=0;
        for(let i=0;i<password.length;i++)
        {
            if(password.charAt(i)>='A' && password.charAt(i)<='Z')
            {
                capitalletter++;
            }
            else if(password.charAt(i)>='0' && password.charAt(i)<='9')
            {
                number++;
            }
            else if((password.charAt(i)>='!' && password.charAt(i)<='/') || (password.charAt(i)>=':' && password.charAt(i)<='@'))
            {
                specialcharacter++;
            }
        }
        if(password.length<6)
        {
            document.getElementById("errorpassword").innerHTML = "Password must be atleast 6 letters";
            document.getElementById("pword").style.border = "2px solid red";
            return false;
        }
        else if(capitalletter<=0)
        {
            document.getElementById("errorpassword").innerHTML = "Password must have atleast one capital letter";
            document.getElementById("pword").style.border = "2px solid red";
            return false;
        }
        else if(number<=0)
        {
            document.getElementById("errorpassword").innerHTML = "Password must have atleast one number";
            document.getElementById("pword").style.border = "2px solid red";
            return false;
        }
        else if(specialcharacter<=0)
        {
            document.getElementById("errorpassword").innerHTML = "Password must have atleast one special character";
            document.getElementById("pword").style.border = "2px solid red";
            return false;
        }
        else if(password.length>=6 && capitalletter>=1 && number>=1 && specialcharacter>=1)
        {
            document.getElementById("errorpassword").innerHTML = "";
            document.getElementById("pword").style.border = "2px solid #33ff33";
            return true;
        }
    }
}

function confirmpasswordcheck()
{
    var password = document.getElementById("pword").value;
    var confirmpassword = document.getElementById("cpword").value;
    if(confirmpassword=="")
    {
        document.getElementById("errorconfirmpassword").innerHTML = "Confirm Password is Required";
        document.getElementById("cpword").style.border = "2px solid red";
        return false;
    }
    else
    {
        if(password===confirmpassword)
        {
            document.getElementById("errorconfirmpassword").innerHTML = "";
            document.getElementById("cpword").style.border = "2px solid #33ff33";
            return true;
        }
        else
        {
            document.getElementById("errorconfirmpassword").innerHTML = "Password and Confirm Password is mismatched";
            document.getElementById("cpword").style.border = "2px solid red";
            return false;
        }
    }
}

function Loginvalidate()
{
    if (LoginIdcheck() == true && LoginPasswordcheck() == true)
    {
        document.getElementById("logbtn").disabled = false;
    }
    else
    {
        document.getElementById("logbtn").disabled = true;
    }
}

function LoginIdcheck()
{
    var id = document.getElementById("Id").value;
    if (id == "")
    {
        document.getElementById("Id").style.border = "2px solid red";
        document.getElementById("Idval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
        return false;
    } 
    else
    {
        document.getElementById("Id").style.border = "2px solid #33ff33";
        document.getElementById("Idval").style.boxShadow = "0 0 5pt 0.5pt #33ff33";
        return true;
    }
}

function LoginPasswordcheck()
{
    var password = document.getElementById("Password").value;
    if (password == "")
    {
        document.getElementById("Password").style.border = "2px solid red";
        document.getElementById("Passwordval").style.boxShadow = "0 0 5pt 0.5pt #ff4d4d";
        return false;
    }
    else
    {
        document.getElementById("Password").style.border = "2px solid #33ff33";
        document.getElementById("Passwordval").style.boxShadow = "0 0 5pt 0.5pt #33ff33";
        return true;
    }
}