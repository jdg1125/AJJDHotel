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
var results = []
for (let i = 0; i < 24; i++) {
    results.push(document.getElementById(`result_${i}`));
}

//results = [document.getElementById("result_0"), document.getElementById("result_1"), document.getElementById("result_2")];

//define functions


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
            oneToShow.classList.remove("textbox");
        }


        oneToShow = results[(this.value).split('_')[1]];
        console.log(oneToShow)


        oneToShow.classList.remove("hideRes");
        oneToShow.classList.add("textbox");

    }
})();

//procedure - add event listeners and call relevant one-time functions

navIcon.addEventListener("click", showNav);

if (slides != null && slides.length != 0)
    showSlides();

if (roomTypeSelect != null)
    roomTypeSelect.addEventListener("click", showCurrentDesc);




