var map, mainmarker, service;
var markers = [];
function initMap() {

    var _lat = parseFloat(document.getElementById("Br_Lat").value);
    var _lng = parseFloat(document.getElementById("Br_Lng").value);
    $("#LatSelected").val(_lat);
    $("#LngSelected").val(_lng);

    map = new google.maps.Map(document.getElementById("map"), {
        center: { lat: _lat, lng: _lng },
        zoom: 13,
        mapTypeId: "roadmap",
    });
    // Create the search box and link it to the UI element.
    const input = document.getElementById("txt_Addrerss");
    const searchBox = new google.maps.places.SearchBox(input);
    //map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    // Bias the SearchBox results towards current map's viewport.
    map.addListener("bounds_changed", () => {
        searchBox.setBounds(map.getBounds());
    });
    let markersd = [];
    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener("places_changed", () => {

        const places = searchBox.getPlaces();

        if (places.length == 0) {
            return;
        }
        // Clear out the old markers.
        markersd.forEach((marker) => {
            marker.setMap(null);
        });
        markersd = [];
        // For each place, get the icon, name and location.
        const bounds = new google.maps.LatLngBounds();
        places.forEach((place) => {

            if (!place.geometry || !place.geometry.location) {
                console.log("Returned place contains no geometry");
                return;
            }
            deleteMarkers();
            const icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25),
            };
            // Create a marker for each place.
            var marker = new google.maps.Marker({
                map,
                icon,
                title: place.name,
                position: place.geometry.location,
            })
            markersd.push(
                marker
            );
            markers.push(marker);
            const infowindow = new google.maps.InfoWindow({
                content: "" + place.name + "",
            });
            marker.addListener("click", () => {
                infowindow.open(map, marker);
            });
            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        map.fitBounds(bounds);

        Getneighborhood(places[0].geometry.location.lat(), places[0].geometry.location.lng());
    });



    map.addListener("click", (mapsMouseEvent) => {

        $("#LatSelected").val(mapsMouseEvent.latLng.lat());
        $("#LngSelected").val(mapsMouseEvent.latLng.lng());
        createMarker(mapsMouseEvent.latLng.lat(), mapsMouseEvent.latLng.lng())
        //// Close the current InfoWindow.
        //infoWindow.close();
        //// Create a new InfoWindow.
        //infoWindow = new google.maps.InfoWindow({
        //    position: mapsMouseEvent.latLng,
        //});
        //infoWindow.setContent(
        //    JSON.stringify(mapsMouseEvent.latLng.toJSON(), null, 2)
        //);
        //infoWindow.open(map);


    });


    geocodeLatLng(_lat, _lng);
    Getneighborhood(_lat, _lng);
}

function geocodeLatLng(lat, lng) {
    const geocoder = new google.maps.Geocoder();

    const latlng = {
        lat: lat,
        lng: lng,
    };
    geocoder.geocode({ location: latlng }, (results, status) => {
        if (status === "OK") {
            if (results[0]) {
                map.setZoom(11);
                const marker = new google.maps.Marker({
                    position: latlng,
                    map: map,
                    //icon:"https://maps.gstatic.com/mapfiles/place_api/icons/v1/png_71/geocode-71.png"
                });
                markers.push(marker);
                const infowindow = new google.maps.InfoWindow({
                    content: "" + results[0].formatted_address + "",
                });
                marker.addListener("click", () => {
                    infowindow.open(map, marker);
                });
                $("#txt_Addrerss").val(results[0].formatted_address);

                const bounds = new google.maps.LatLngBounds();
                bounds.extend(latlng);
                map.fitBounds(bounds);
                map.setZoom(15);
            } else {
                //window.alert("No results found");
                if (getCookie("Language") == "ar") {
                    $("#Div_alertMessage").html("لا توجد بيانات متاحه");

                } else {
                    $("#Div_alertMessage").html("No results found");

                }
                $("#btn_modal_alert").click();
            }
        } else {
            $("#Div_alertMessage").html("Geocoder failed due to: " + status);
            $("#btn_modal_alert").click();
        }
    });
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function Getneighborhood(Lat, Lng) {

    var _location = new google.maps.LatLng(Lat, Lng);
    var _radius = $("#txt_radius").val() * 1000;
    var request = {
        location: _location,
        radius: `` + _radius + ``,
        type: ['neighborhood'],

    };

    service = new google.maps.places.PlacesService(map);
    service.textSearch(request, callback);


}

function callback(results, status) {

    $("#Div_neighborhood").html("");
    if (status == google.maps.places.PlacesServiceStatus.OK) {
        $("#NeighborhoodCount").html("( " + results.length + " )" + " حى");
        for (var i = 0; i < results.length; i++) {


            var place = results[i];
            $("#Div_neighborhood").append(`<li class='lineigh' id="neighborhood_` + i + `" ><a href="javascript:void(0)" onclick="createMarker(` + place.geometry.location.lat() + `,` + place.geometry.location.lng() + `,` + `'neighborhood_` + i + `'` + `)">` + place.name + `</a></li>`);

        }
    }
}

function createMarker(lat, lng, placeid) {
    //if (!place.geometry || !place.geometry.location) return;
    deleteMarkers();
    if (placeid != undefined) {
        $('.lineigh').removeClass('li-active');
        $("#" + placeid + "").addClass('li-active');
    }


    geocodeLatLng(lat, lng);
    //var _location = new google.maps.LatLng(lat, lng);

    //const marker = new google.maps.Marker({
    //    map,
    //    position: _location,
    //});
    //markers.push(marker);

    //google.maps.event.addListener(marker, "click", () => {
    //    

    //    const infowindow = new google.maps.InfoWindow({
    //        content: "" + place.name + "",
    //    });
    //    marker.addListener("click", () => {
    //        infowindow.open(map, marker);
    //    });
    //});
    ////map.setCenter(_location);
    //const bounds = new google.maps.LatLngBounds();
    //bounds.extend(_location);
    //map.fitBounds(bounds);
    ////map.panTo(_location);
    //map.setZoom(15);

    // $("#Div_neighborhood").append(`<a href="">` + place.name+`</a>`);
}

function myNeighborhoodFilter() {
    var input, filter, ul, li, a, i, txtValue;
    input = document.getElementById("txt_Neighborhood");
    filter = input.value.toUpperCase();
    ul = document.getElementById("Div_neighborhood");
    li = ul.getElementsByTagName("li");
    for (i = 0; i < li.length; i++) {
        a = li[i].getElementsByTagName("a")[0];
        txtValue = a.textContent || a.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            li[i].style.display = "";
        } else {
            li[i].style.display = "none";
        }
    }
}

// Sets the map on all markers in the array.
function setMapOnAll(map) {
    for (let i = 0; i < markers.length; i++) {
        markers[i].setMap(map);
    }
}

// Removes the markers from the map, but keeps them in the array.
function clearMarkers() {
    setMapOnAll(null);
}

// Shows any markers currently in the array.
function showMarkers() {
    setMapOnAll(map);
}

// Deletes all markers in the array by removing references to them.
function deleteMarkers() {
    clearMarkers();
    markers = [];
}


//user is "finished typing," do something
function doneTypingradius() {
    //do something
    clearTimeout(radiusTimer);
    var _lat = parseFloat(document.getElementById("Br_Lat").value);
    var _lng = parseFloat(document.getElementById("Br_Lng").value);
    Getneighborhood(_lat, _lng);
}

function ExtractUrl() {
    //var url = "https://www.google.com/maps/place/Arctic+Pixel+Digital+Solutions/@63.6741553,-164.9587713,4z/data=!3m1!4b1!4m5!3m4!1s0x5133b2ed09c706b9:0x66deacb5f48c5d57!8m2!3d64.751111!4d-147.3494442";
    //var regex = new RegExp('@(.*),(.*),');
    //var lon_lat_match = url.match(regex);
    //var lon = lon_lat_match[1];
    //var lat = lon_lat_match[2];


    var settings = {
        "url": "/ClientUser/Request/GetPoint",
        "data": {
            _url: $("#maptext").val()
        },
        "method": "GET",
        "timeout": 0,
    };

    $.ajax(settings).done(function (response) {


        $("#LatSelected").val(response.lat);
        $("#LngSelected").val(response.lng);
        createMarker(response.lat, response.lng)
    });


}
function GetLocation() {
    $("#spnMapurlSummary").html("");
    $("#iconlocationcheck").hide();

    var url = $("#txt_LinkLocatiom").val();
    if (url.length > 0) {

        $("#iconlocationloading").show();
        $("#txt_LinkLocatiom").attr("disabled", true);

        var settings = {
            "url": "https://dev.made-home.com/check",
            "method": "POST",
            "timeout": 0,
            "headers": {
                "Content-Type": "application/json"
            },
            "data": JSON.stringify({
                "url": url
            }),
        };

        $.ajax(settings).done(function (response) {

            if (response.status) {
                // 
                // 
                $("#iconlocationloading").hide();
                $("#txt_LinkLocatiom").attr("disabled", false);
                $("#iconlocationcheck").show();

                $("#LatSelected").val(parseFloat(response.data.lat));
                $("#LngSelected").val(parseFloat(response.data.lng));

                createMarker(parseFloat(response.data.lat), parseFloat(response.data.lng))

            } else {
                ReGetLocation(response.data.url);
            }
            console.log(response);
        });
    } else {
        $("#spnMapurlSummary").html("يجب وضع رابط صحيح");
    }

}
function ReGetLocation(url) {


    var settings = {
        "url": "https://dev.made-home.com/check",
        "method": "POST",
        "timeout": 0,
        "headers": {
            "Content-Type": "application/json"
        },
        "data": JSON.stringify({
            "url": url
        }),
    };

    $.ajax(settings).done(function (response) {

        $("#iconlocationloading").hide();
        $("#txt_LinkLocatiom").attr("disabled", false);

        if (response.status) {
            $("#iconlocationcheck").show();
            $("#LatSelected").val(parseFloat(response.data.lat));
            $("#LngSelected").val(parseFloat(response.data.lng));
            createMarker(parseFloat(response.data.lat), parseFloat(response.data.lng))

        } else {
            $("#spnMapurlSummary").html(response.message);
        }
        console.log(response);
    });

}
