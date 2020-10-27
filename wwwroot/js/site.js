var toggleRole = document.getElementById("toggleRole");
var toggleLogin = document.getElementById("toggleLogin");
var changeResView = document.getElementById("changeResView");
var rowToHide = document.getElementById("rowToHide");
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


var toggleResView = (function () {  //this is a closure
    var isHidden = true;  //this only happens the first time it's called

    return function () {
        isHidden = !isHidden;
        if (!isHidden) {
            rowToHide.classList.remove("hideRes");
            this.innerHTML = "Click to see results of searching by confirmation number";
        }
        else {
            rowToHide.classList.add("hideRes");
            this.innerHTML = "Click to see results of searching by name";
        }
    }
})();

toggleRole.addEventListener("click", toggleAdminView);
toggleLogin.addEventListener("click", toggleLoginView);
if (changeResView != null)
    changeResView.addEventListener("click", toggleResView);

var slideIndex = 0;
var slides = document.getElementsByClassName("slideImg");

function showSlides() {

    for (var i = 0; i < slides.length; i++) {
        slides[i].classList.add("hideLink");
        slides[i].classList.remove("slideImgShow");
    }
    
    if (++slideIndex > slides.length)
        slideIndex = 1;

    slides[slideIndex - 1].classList.remove("hideLink");
    slides[slideIndex - 1].classList.add("slideImgShow");
    setTimeout(showSlides, 3000); 
}

showSlides();
