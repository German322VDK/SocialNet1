function togglePassword() {
  var input = document.getElementById('password');
  var icon = document.getElementById('icon');

  if (input.type === "password") {
    input.type = "text";
    icon.classList.add('selected');
  } else {
    input.type = "password";
    icon.classList.remove('selected');
  }
}

async function GetHash(act1, act2, act3, emailAdd, url) {

    document.getElementById(act1).style = "display:none";
    document.getElementById(act2).style = "display:none";
    document.getElementById(act3).style = "display:none";

    var email = document.getElementById(emailAdd).value;

    var fullurl = url + "?email=" + email;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    if (body) {
        document.getElementById(act2).style = "display:block";
        document.getElementById(act1).style = "display:none";
        document.getElementById('but1').style = "display:none";
        document.getElementById('but2').style = "display:block";
    }
    else {
        document.getElementById(act1).style = "display:block";
        document.getElementById(act3).style = "display:block";
    }

}

async function Confirm(em, hsh, url) {

    var email = document.getElementById(em).value;
    var hash = document.getElementById(hsh).value;

    var fullurl = url + "?email=" + email + "&hash=" + hash;

    var promise = await fetch(fullurl);

    var body = await promise.json();

    if (body) {

        document.getElementById('nextdoor').click();
    }
    else {
        document.getElementById('finaler').style = "display:block";
    }

    
}

function RegInput() {

    var email = document.getElementById('RegEmail');

    var email1 = document.getElementById('Email');

    var hash = document.getElementById('Hash');

    var emailText = email.value;

    //document.getElementById('Email1').value = document.getElementById('RegEmail').value;

    //$('#Email').value = $('#RegEmail').value;

    $('#Email').val(emailText);


    document.getElementById('regIn').click();
    
}

function CheckInputChange(checkbox) {
    if (checkbox.checked == true)
    {
        document.getElementById("RegButtn").removeAttribute("disabled");
    }
    else {
        document.getElementById("RegButtn").setAttribute("disabled", "disabled");
    }
}