var i;
var tabcontent = document.getElementsByClassName('tabcontent')
var tablinks = document.getElementsByClassName('tablinks')
function openReportTab() {
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = 'none';
    }

    if (this == revenuebtn)
        tabcontent[0].style.display = "block";
    else if (this == occupancybtn)
        tabcontent[1].style.display = "block";
    else if (this == stuffbtn)
        tabcontent[2].style.display = "block";
}

var revenuebtn = document.getElementById('revenue-btn')
var occupancybtn = document.getElementById('occupancy-btn')
var stuffbtn = document.getElementById('stuff-btn')

revenuebtn.addEventListener("click", openReportTab)
occupancybtn.addEventListener("click", openReportTab)
stuffbtn.addEventListener("click", openReportTab)