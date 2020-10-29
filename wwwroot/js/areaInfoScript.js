var weatherDiv = document.getElementById("weatherResult");
var restaurantDiv = document.getElementById("restaurantResults")
const zomatoKey = "8a9d1c7ee58e3e39c979e7fa178b271c";

function getWeather() {
    fetch("https://api.weatherapi.com/v1/forecast.json?key=e9e1451df465427a9fc30246202010&q=30909&days=7")
        .then(response => response.json())
        .then(data => renderWeatherData(data));
}

function renderWeatherData(arg) {
    var curr = arg["current"];

    var icon = document.createElement("img");
    icon.setAttribute("src", curr["condition"]["icon"]);
    icon.setAttribute("alt", "weatherIcon");

    weatherDiv.appendChild(icon);

    var textDiv = document.createElement("div");
    textDiv.className = "textContainer";

    var header = document.createElement("h5");
    header.innerHTML = curr["temp_f"].toString().concat(" deg F - ").concat(curr["condition"]["text"]);
    textDiv.appendChild(header);

    header = document.createElement("p");
    header.innerHTML = "Wind speed: ".concat(curr["wind_mph"].toString()).concat(" mph. Precipitation: ").concat(curr["precip_in"].toString()).concat(" inches.");
    textDiv.appendChild(header);

    weatherDiv.appendChild(textDiv);
}

function getRestaurants() {
    var head = new Headers();
    head.append("user-key", zomatoKey);

    var req = new Request("https://developers.zomato.com/api/v2.1/search?entity_id=628&entity_type=city&sort=rating", {
        headers: head
    });
    fetch(req)
        .then(response => response.json())
        .then(data => renderRestaurantData(data));
}

function renderRestaurantData(arg) {
    console.log(arg);
    for (var i = 0; i < 5; i++)
        createResultNode(arg["restaurants"], i);
}

function createResultNode(arg, index) {
    var node = document.createElement("div");
    node.className = "resultItem";

    var title, cuisine, menuUrl, contact, timings;
    node.appendChild((title=document.createElement("h3")));
    title.innerHTML = arg[index]["restaurant"].name;
    title.className = "result-pad";

    node.appendChild((cuisine = document.createElement("h5")));
    cuisine.innerHTML = arg[index]["restaurant"].cuisines;
    cuisine.className = "result-pad";

    node.appendChild((contact = document.createElement("h6")));
    contact.innerHTML = arg[index]["restaurant"].phone_numbers + "<br><br>" + arg[index]["restaurant"]["location"].address;
    contact.className = "result-pad";

    node.appendChild((timings = document.createElement("p")));
    timings.innerHTML = arg[index]["restaurant"].timings;
    timings.className = "result-add-space";

    node.appendChild((menuUrl = document.createElement("a")));
    menuUrl.setAttribute("href", arg[index]["restaurant"].menu_url);
    menuUrl.setAttribute("target", "_blank");
    menuUrl.innerHTML = "View Menu";
    menuUrl.className = "result-pad";

    restaurantDiv.appendChild(node);
}

getWeather();

getRestaurants();
