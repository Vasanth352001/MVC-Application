function Paymentvalidate()
{
    if (namecheck() == true && cardnocheck() == true && cardtypecheck() == true && cvvcheck() == true && expdatecheck() == true)
    {
        document.getElementById("paymentbtn").disabled = false;
    }
    else
    {
        document.getElementById("paymentbtn").disabled = true;
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

function cardnocheck()
{
    var cardno = document.getElementById("card_number").value;
    if (cardno == "")
    {
        document.getElementById("errorcardnumber").innerHTML = "Name is Required";
        document.getElementById("card_number").style.border = "2px solid red";
        return false;
    }
    else
    {
        if (numcheck(cardno) == true)
        {
            if (cardno.length == 16)
            {
                document.getElementById("errorcardnumber").innerHTML = "";
                document.getElementById("card_number").style.border = "2px solid #33ff33";
                return true;
            }
            else
            {
                document.getElementById("errorcardnumber").innerHTML = "You have only entered " + cardno.length + " numbers";
                document.getElementById("card_number").style.border = "2px solid red";
                return false;
            }
        }
        else
        {
            document.getElementById("errorcardnumber").innerHTML = "Only numbers are allowed";
            document.getElementById("card_number").style.border = "2px solid red";
            return false;
        }
    }
}

function cardtypecheck()
{
    var cardtype = document.getElementById("card_type").value;
    if (cardtype == "")
    {
        document.getElementById("errortype").innerHTML = "Name is Required";
        document.getElementById("card_type").style.border = "2px solid red";
        return false;
    }
    else
    {
        document.getElementById("errortype").innerHTML = "";
        document.getElementById("card_type").style.border = "2px solid #33ff33";
        return true;
    }
}

function expdatecheck()
{
    var expdate = document.getElementById("expdate").value;
    if (expdate == "")
    {
        document.getElementById("errordate").innerHTML = "Expiry Date is Required";
        document.getElementById("expdate").style.border = "2px solid red";
        return false;
    }
    else
    {
        document.getElementById("errordate").innerHTML = "";
        document.getElementById("expdate").style.border = "2px solid #33ff33";
        return true;
    }
}

function cvvcheck()
{
    var cvv = document.getElementById("cvvno").value;
    if (cvv == "")
    {
        document.getElementById("errorcvv").innerHTML = "CVV is Required";
        document.getElementById("cvvno").style.border = "2px solid red";
        return false;
    }
    else
    {
        if (cvv.length == 3)
        {
            if (numcheck(cvv) == true)
            {
                document.getElementById("errorcvv").innerHTML = "";
                document.getElementById("cvvno").style.border = "2px solid #33ff33";
                return true;
            }
            else
            {
                document.getElementById("errorcvv").innerHTML = "Only allowed numbers";
                document.getElementById("cvvno").style.border = "2px solid red";
                return false;
            }
        }
        else
        {
            document.getElementById("errorcvv").innerHTML = "You have entered " + cvv.length + " numbers";
            document.getElementById("cvvno").style.border = "2px solid red";
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

function success()
{
    let popup = document.querySelector('.popup');
    let close = document.querySelector('.close');
    let conf = document.querySelector('#my-canvas');

    popup.classList.add('active');
    conf.classList.add('active');

    close.onclick = function () {
        popup.classList.remove('active');
        conf.classList.remove('active');
    }
    var confettiSettings = { target: 'my-canvas' };
    var confetti = new ConfettiGenerator(confettiSettings);
    confetti.render();
}