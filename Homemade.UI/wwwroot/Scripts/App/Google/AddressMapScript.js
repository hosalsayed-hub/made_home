
let element = document.getElementById("map");
var mainMap, mainmarker;
let defaultLong = 46.723831;
let defaultLat = 24.786505;
var donegetloc = false;
var iswindwo = false;
function GeolocationControl(controlDiv, map) {

    // Set CSS for the control button
    var controlUI = document.createElement('div');
    //controlUI.style.background = 'url(https://cdn.made-home.com/Home/2beded322abadff6c819828876a6f21f.jpg)';
    controlUI.style.background = 'url(https://cdn.klokantech.com/maptilerlayer/v1/geolocation.png)';
    controlUI.style.borderStyle = 'solid';
    controlUI.style.borderWidth = '1px';
    controlUI.style.borderColor = 'rgb(16 16 16 / 15%)';
    controlUI.style.height = '28px';
    controlUI.style.backgroundSize = 'cover';
    controlUI.style.marginTop = '167px';
    controlUI.style.marginLeft = '10px';
    controlUI.style.cursor = 'pointer';
    controlUI.style.textAlign = 'center';
    controlUI.title = 'اضغط هنا للعودة إلي موقعك';
    controlDiv.appendChild(controlUI);

    // Set CSS for the control text
    var controlText = document.createElement('div');
    //controlText.style.fontFamily = 'Arial,sans-serif';
    //controlText.style.fontSize = '10px';
    //controlText.style.fontWeight = 'bolder';
    //controlText.style.color = 'rgb(18 18 18)';
    controlText.style.paddingLeft = '16px';
    controlText.style.paddingRight = '10px';
    //controlText.style.marginTop = '8px';
    //controlText.innerHTML = '';
    controlUI.appendChild(controlText);

    // Setup the click event listeners to geolocate user
    google.maps.event.addDomListener(controlUI, 'click', getLocation);
}

function initMap(xop, lat, lng) {
    debugger;
    //  alert(lat);
    if (lat != undefined) {
        defaultLong = lng;
        defaultLat = lat;
    } else {
        if (!donegetloc) {
            setTimeout(function () {
                getLocation();
                donegetloc = true;
            }, 1000);

        }


        if (!xop) {
            defaultLong = document.getElementById("Lng").value != "" ? parseFloat(document.getElementById("Lng").value) : lng;
            defaultLat = document.getElementById("Lat").value != "" ? parseFloat(document.getElementById("Lat").value) : lat;



        }
        else {
            defaultLong = xop.Lat;
            defaultLat = xop.Long;
        }
    }


    const map = mainMap = new google.maps.Map(element, {
        center: { lat: defaultLat, lng: defaultLong },
        zoom: 17,
        mapTypeControl: true,
        mapTypeControlOptions: {
            style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
            position: google.maps.ControlPosition.TOP_CENTER,
        },
        //zoomControl: true,
        //zoomControlOptions: {
        //    position: google.maps.ControlPosition.LEFT_CENTER,
        //},
        scaleControl: true,
        streetViewControl: false,
        streetViewControlOptions: {
            position: google.maps.ControlPosition.LEFT_TOP,
        },
        fullscreenControl: true,
    });
    //var geoloccontrol = new klokantech.GeolocationControl(map, 17);

    var geolocationDiv = document.createElement('div');
    var geolocationControl = new GeolocationControl(geolocationDiv, map);
    map.controls[google.maps.ControlPosition.LEFT_CENTER].push(geolocationDiv);

    var input = document.getElementById("pac-input");
    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo("bounds", map);

    // Specify just the place data fields that you need.
    autocomplete.setFields(["place_id", "geometry", "name"]);
    // map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    var myLatLng = { lat: defaultLat, lng: defaultLong };

    // Set the position of the marker using the place ID and location.
    mainmarker = new google.maps.Marker({
        position: myLatLng,
        map,
        //icon:"https://mts.googleapis.com/vt/icon/name=icons/spotlight/spotlight-waypoint-a.png&text=H&psize=16&color=ff333333&ax=44&ay=45&scale=1"
        //  title: "Hello World!",
    });
    //My Funstion
    if (iswindwo === true) {
        infowindow.close();
    }
    $.ajax({
        type: "POST",
        url: '/Site/Home/GetLocationData',
        data: {
            'lat': defaultLat,
            'lng': defaultLong
        },
        dataType: "json",
        async: false,
        contentType: "application/x-www-form-urlencoded",
        success: function (result) {
            debugger;
            if (result.status === true) {
                $("#BlockID").val(result.data.blockId);
                $("#CityID").val(result.data.cityId);
                $("#RegionID").val(result.data.regionId);
                $("#Address").val(result.data.address);
                $("#Region_Name").html(result.data.regionName);
                $("#City_Name").html(result.data.cityName);
                $("#Block_Name").html(result.data.blockName);
                $("#Address_Name").html(result.data.address);
            } else {
                $("#BlockID").val(0);
                $("#CityID").val(0);
                $("#RegionID").val(0);
                $("#Address").val("");
                $("#Region_Name").html("");
                $("#City_Name").html("");
                $("#Block_Name").html("");
                $("#Address_Name").html("");
                $("#CustomerLocationID").val(0);
                var Strs = "<strong style='font-size: large;color: red;font-weight: 700;'>" + result.message + "</strong>"
                infowindow = new google.maps.InfoWindow({
                    content: Strs,
                });
                infowindow.open(map, mainmarker);
                iswindwo = true;
                //setTimeout(function () { infowindow.close(); }, 2000);
            }
        }
    });


    autocomplete.addListener("place_changed", () => {
        debugger;

        var place = autocomplete.getPlace();

        if (!place.geometry) {
            return;
        }
        document.getElementById("Lat").value = place.geometry.location.lat();
        document.getElementById("Lng").value = place.geometry.location.lng();

        document.getElementById("LatStr").value = place.geometry.location.lat().toString();
        document.getElementById("LngStr").value = place.geometry.location.lng().toString();


        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);
        }
        var myLatLng = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };

        //My Funstion
        if (iswindwo === true) {
            infowindow.close();
        }
        $.ajax({
            type: "POST",
            url: '/Site/Home/GetLocationData',
            data: {
                'lat': place.geometry.location.lat(),
                'lng': place.geometry.location.lng()
            },
            dataType: "json",
            async: false,
            contentType: "application/x-www-form-urlencoded",
            success: function (result) {
                debugger;
                if (result.status === true) {
                    $("#BlockID").val(result.data.blockId);
                    $("#CityID").val(result.data.cityId);
                    $("#RegionID").val(result.data.regionId);
                    $("#Address").val(result.data.address);
                    $("#Region_Name").html(result.data.regionName);
                    $("#City_Name").html(result.data.cityName);
                    $("#Block_Name").html(result.data.blockName);
                    $("#Address_Name").html(result.data.address);
                } else {
                    $("#BlockID").val(0);
                    $("#CityID").val(0);
                    $("#RegionID").val(0);
                    $("#Address").val("");
                    $("#Region_Name").html("");
                    $("#City_Name").html("");
                    $("#Block_Name").html("");
                    $("#Address_Name").html("");
                    $("#CustomerLocationID").val(0);
                    var Strs = "<strong style='font-size: large;color: red;font-weight: 700;'>" + result.message + "</strong>"
                    infowindow = new google.maps.InfoWindow({
                        content: Strs,
                    });
                    infowindow.open(map, mainmarker);
                    iswindwo = true;
                    //setTimeout(function () { infowindow.close(); }, 2000);
                }
            }
        });
        // Set the position of the marker using the place ID and location.
        mainmarker.setPosition(myLatLng);
        //marker.setPlace({
        //    placeId: place.place_id,
        //    location: place.geometry.location,
        //});
        // 
        //infowindowContent.children.namedItem("place-name").textContent = place.name;
        //infowindowContent.children.namedItem("place-id").textContent =
        //    place.place_id;
        //infowindowContent.children.namedItem("place-address").textContent =
        //    place.formatted_address;
        //infowindow.open(map, marker);
    });
    //map.addListener('click', function (e) {
    //    infowindow.open(map, marker);
    //    // placeMarker(e.latLng, map);
    //});
    map.addListener("click", (mapsMouseEvent) => {
        document.getElementById("Lat").value = mapsMouseEvent.latLng.lat();
        document.getElementById("Lng").value = mapsMouseEvent.latLng.lng();
        document.getElementById("LatStr").value = mapsMouseEvent.latLng.lat().toString();
        document.getElementById("LngStr").value = mapsMouseEvent.latLng.lng().toString();
        var myLatLng = { lat: mapsMouseEvent.latLng.lat(), lng: mapsMouseEvent.latLng.lng() };
        //My Funstion
        if (iswindwo === true) {
            infowindow.close();
        }
        $.ajax({
            type: "POST",
            url: '/Site/Home/GetLocationData',
            data: {
                'lat': mapsMouseEvent.latLng.lat(),
                'lng': mapsMouseEvent.latLng.lng()
            },
            dataType: "json",
            async: false,
            contentType: "application/x-www-form-urlencoded",
            success: function (result) {
                debugger;
                if (result.status === true) {
                    $("#BlockID").val(result.data.blockId);
                    $("#CityID").val(result.data.cityId);
                    $("#RegionID").val(result.data.regionId);
                    $("#Address").val(result.data.address);
                    $("#Region_Name").html(result.data.regionName);
                    $("#City_Name").html(result.data.cityName);
                    $("#Block_Name").html(result.data.blockName);
                    $("#Address_Name").html(result.data.address);
                } else {
                    $("#BlockID").val(0);
                    $("#CityID").val(0);
                    $("#RegionID").val(0);
                    $("#Address").val("");
                    $("#Region_Name").html("");
                    $("#City_Name").html("");
                    $("#Block_Name").html("");
                    $("#Address_Name").html("");
                    $("#CustomerLocationID").val(0);
                    var Strs = "<strong style='font-size: large;color: red;font-weight: 700;'>" + result.message + "</strong>"
                    infowindow = new google.maps.InfoWindow({
                        content: Strs,
                    });
                    infowindow.open(map, mainmarker);
                    iswindwo = true;
                    //setTimeout(function () { infowindow.close(); }, 2000);
                }
            }
        });
        // Set the position of the marker using the place ID and location.
        mainmarker.setPosition(myLatLng);
    });
    // ifThereAnotherMap();
    // Define the LatLng coordinates for the polygon's path.
    //const triangleCoords = [
    //    { lat: 24.458892206697534, lng: 46.667117578475846 },
    //    { lat: 24.703663116013626, lng: 46.991214258163346 },
    //    { lat: 25.017662637383044, lng: 46.749515039413346},
    //    { lat: 24.776003427117153, lng: 46.502322656600846 },
    //    { lat: 24.458892206697534, lng: 46.667117578475846 },


    //];
    //// Construct the polygon.
    //const bermudaTriangle = new google.maps.Polygon({
    //    paths: triangleCoords,
    //    strokeColor: "#FF0000",
    //    strokeOpacity: 0.8,
    //    strokeWeight: 2,
    //    fillColor: "#FF0000",
    //    fillOpacity: 0.35,
    //});

    //bermudaTriangle.setMap(map);
}

function getLocation() {
    if (navigator.geolocation) {

        navigator.geolocation.getCurrentPosition(showPosition);
    } else {
        element.innerHTML = "Geolocation is not supported by this browser.";
    }
}

function showPosition(position) {
    debugger;

    //document.getElementById("Lng").value = position.coords.longitude;
    //document.getElementById("Lat").value = position.coords.latitude; 
    $("Lng").val(position.coords.longitude);
    $("Lat").val(position.coords.latitude);
    $("LatStr").val(position.coords.latitude.toString());
    $("LngStr").val(position.coords.longitude.toString());
    //document.getElementById("LatStr").value = position.coords.latitude.toString();
    //document.getElementById("LngStr").value = position.coords.longitude.toString();
    initMap("", position.coords.latitude, position.coords.longitude);
}

let __RequestVerificationToken = $("input[name=__RequestVerificationToken]");
let link = '';
if (element.hasAttribute('data-link')) {
    link = element.getAttribute('data-link');
} else {
    link = '/Setting/City/';
}

$.post(link + "GetGoogleMapScript", { '__RequestVerificationToken': __RequestVerificationToken.val() }, function (res) {
    debugger;
    var script = document.createElement("script");
    script.append(res);
    document.body.append(script);
});

let appstyle = document.createElement('style');

appstyle.append(`<style>
    #map {
        height: 400px;
        width: 100%
    }

    /* Optional: Makes the sample page fill the window. */

    .controls {
        background-color: #fff;
        border-radius: 2px;
        border: 1px solid transparent;
        box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
        box-sizing: border-box;
        font-family: Roboto;
        font-size: 15px;
        font-weight: 300;
        height: 29px;
        margin-left: 17px;
        margin-top: 10px;
        outline: none;
        padding: 0 11px 0 13px;
        text-overflow: ellipsis;
        width: 400px;
    }

        .controls:focus {
            border-color: #4d90fe;
        }

    .title {
        font-weight: bold;
    }

    #infowindow-content {
        display: none;
    }

    #map #infowindow-content {
        display: inline;
    }

    .pac-container {
        z-index: 99999;
    }
</style>`);

document.body.append(appstyle);



