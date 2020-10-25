var weatherDiv = document.getElementById("weatherResult");

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

getWeather();