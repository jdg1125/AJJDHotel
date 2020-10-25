var toggleRole = document.getElementById("toggleRole");
var toggleLogin = document.getElementById("toggleLogin");
var adminLink = document.getElementsByClassName("adminView");
var linksToChg = document.getElementsByClassName("chgLinkTxt");
var isAdmin = false;
var isLoggedIn = false;

function toggleAdminView() {

    isAdmin = !isAdmin;

    for (var i = 0; i < adminLink.length; i++)
        if (!isAdmin)
            adminLink[i].classList.add("hideLink");
        else
            adminLink[i].classList.remove("hideLink");

    if (isAdmin)
        isLoggedIn = true;

    toggleLoginText();
}

function toggleLoginView() {
    isLoggedIn = !isLoggedIn;

    if (isAdmin)
        toggleAdminView();
    else
        toggleLoginText();
}

function toggleLoginText() {
    linksToChg[0].innerHTML = isLoggedIn ? "Manage Account" : "Register";
    linksToChg[1].innerHTML = isLoggedIn ? "Logout" : "Login";
    toggleRole.innerHTML = isAdmin ? "Click for customer view" : "Click for admin view";
}


toggleRole.addEventListener("click", toggleAdminView);
toggleLogin.addEventListener("click", toggleLoginView);
