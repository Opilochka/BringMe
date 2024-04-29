function regCheckForm(el) {
    var name = el.name.value;
    var email = el.email.value;
    var pass = el.pass.value;
    var repass = el.repass.value;
    var radioBuyer = el.buyer;
    var radioCompany = el.company;

    var fail = "";
    if (name == "" || email == "" || pass == "" || repass == "") {
        fail = "Fill in all the fields";
    }
    else if (!radioBuyer.checked && !radioCompany.checked) {
        fail = "Fill in all the fields";
    }
    else if (name.length <= 2 || name.length >= 25) {
        fail = "Enter the correct name";
    }
    else if (
        name.split(",").length > 1 ||
        name.split(".").length > 1 ||
        name.split(" ").length > 1 ||
        name.split("/").length > 1 ||
        name.split("\\").length > 1 ||
        name.split("|").length > 1 ||
        name.split("+").length > 1 ||
        name.split("-").length > 1 ||
        name.split("(").length > 1 ||
        name.split(")").length > 1 ||
        name.split("&").length > 1 ||
        name.split("?").length > 1 ||
        name.split("!").length > 1 ||
        name.split("`").length > 1
    ) {
        fail = "The name contains invalid characters";
    }
    else if (email.split("@").length == 1 || email.split("@").length > 2) {
        fail = "The email was entered incorrectly";
    }
    else if (pass != repass) {
        fail = "The password must match";
    }
    else if (pass.split("&").length > 1 || pass.split(" ").length > 1) {
        fail = "Invalid character in the password";
    }

    if (fail != "") {
        document.getElementById('error').innerHTML = fail;
        return false;
    } else {
        alert("the data is entered correctly");
        return true;
    }
}


function loginCheckForm(el) {
    var name = el.name.value;
    var pass = el.pass.value;

    var fail = ""
    if (name == "" || pass == "") {
        fail = "Fill in all the fields";
    }
    else if (
        name.split(",").length > 1 ||
        name.split(".").length > 1 ||
        name.split(" ").length > 1 ||
        name.split("/").length > 1 ||
        name.split("\\").length > 1 ||
        name.split("|").length > 1 ||
        name.split("+").length > 1 ||
        name.split("-").length > 1 ||
        name.split("(").length > 1 ||
        name.split(")").length > 1 ||
        name.split("&").length > 1 ||
        name.split("?").length > 1 ||
        name.split("!").length > 1 ||
        name.split("`").length > 1
    ) {
        fail = "The name contains invalid characters";
    }

    else if (pass.split("&").length > 1 || pass.split(" ").length > 1) {
        fail = "Invalid character in the password";
    }

    if (fail != "") {
        document.getElementById('error').innerHTML = fail;
        return false;
    } else {
        alert("the data is entered correctly");
        return true;
    }
}