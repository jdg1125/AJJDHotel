//vars involved with toggling login state view
var toggleRole = document.getElementById("toggleRole");
var toggleLogin = document.getElementById("toggleLogin");
var adminLink = document.getElementsByClassName("adminView");
var linksToChg = document.getElementsByClassName("chgLinkTxt");
var isAdmin = false;
var isLoggedIn = false;

//vars to change viewing manage reservation by conf num or name
var changeResView = document.getElementById("changeResView");
var rowToHide = document.getElementById("rowToHide");

//vars for navbar 
var navIcon = document.getElementById("navIcon");
var navLinks = document.getElementById("navLinks");

//vars for index page banner
var slides = document.getElementsByClassName("slideImg");
var slideIndex = 0;

//vars for manage room types
var roomTypeSelect = document.getElementById("roomTypeSelect");
var results = [document.getElementById("result_0"), document.getElementById("result_1"), document.getElementById("result_2")];

//define functions

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

var showNav = (function () {
    var isShown = false;

    return function () {
        isShown = !isShown;

        if (isShown) {
            navLinks.classList.remove("hideLink");
            navLinks.classList.add("showNav");
        } else {
            navLinks.classList.add("hideLink");
            navLinks.classList.remove("showNav");
        }
    }
})();

var showCurrentDesc = (function () {
    var oneToShow;

    return function () {

        if (oneToShow != null) {
            oneToShow.classList.add("hideRes");
            oneToShow.classList.remove("resultItem");
        }

        switch (this.value) {
            case "option_0":
                oneToShow = results[0];
                break;
            case "option_1":
                oneToShow = results[1];
                break;
            case "option_2":
                oneToShow = results[2];
                break;
        }

        oneToShow.classList.remove("hideRes");
        oneToShow.classList.add("resultItem");
    }
})();

    //procedure - add event listeners and call relevant one-time functions
    toggleRole.addEventListener("click", toggleAdminView);
    toggleLogin.addEventListener("click", toggleLoginView);
    navIcon.addEventListener("click", showNav);

    if (slides != null && slides.length != 0)
        showSlides();

    if (changeResView != null)
        changeResView.addEventListener("click", toggleResView);

    if (roomTypeSelect != null) 
        roomTypeSelect.addEventListener("click", showCurrentDesc);
    



