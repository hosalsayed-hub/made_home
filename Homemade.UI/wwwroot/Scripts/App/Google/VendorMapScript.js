﻿
let element = document.getElementById("map");
var mainMap, mainmarker;
function initMap(xop) {
    debugger;
    let defaultLong,
        defaultLat;


    if (!xop) {
        defaultLong = element.hasAttribute("data-long") ? parseFloat(element.getAttribute("data-long")) : parseFloat(46.723831);
        defaultLat = element.hasAttribute("data-lat") ? parseFloat(element.getAttribute("data-lat")) : parseFloat(24.786505);
    }
    else {
        defaultLong = xop.Lat;
        defaultLat = xop.Long;
    }
    const map = mainMap = new google.maps.Map(element, {
        center: { lat: defaultLat, lng: defaultLong },
        zoom: 17,
    });
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
        //  title: "Hello World!",
    });

    autocomplete.addListener("place_changed", () => {
        var place = autocomplete.getPlace();

        if (!place.geometry) {
            return;
        }
        document.getElementById("txtLatHidden").value = place.geometry.location.lat();
        document.getElementById("txtLngHidden").value = place.geometry.location.lng();


        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);
        }
        var myLatLng = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };

        // Set the position of the marker using the place ID and location.
        mainmarker.setPosition(myLatLng);
    });
    map.addListener("click", (mapsMouseEvent) => {

        document.getElementById("txtLatHidden").value = mapsMouseEvent.latLng.lat();
        document.getElementById("txtLngHidden").value = mapsMouseEvent.latLng.lng();

        var myLatLng = { lat: mapsMouseEvent.latLng.lat(), lng: mapsMouseEvent.latLng.lng() };

        // Set the position of the marker using the place ID and location.
        mainmarker.setPosition(myLatLng);
    });

    ifThereAnotherMap();

}

function ifThereAnotherMap() {

    let gMap = $(".gMap");

    if (gMap.length > 0) {
        gMap.each(function (i, v) {
            let element = $(this);

            let defaultLong = element.attr("data-long") != undefined ? parseFloat(element.attr("data-long")) : parseFloat(39.19250479999999),
                defaultLat = element.attr("data-lat") != undefined ? parseFloat(element.attr("data-lat")) : parseFloat(21.485811);

            const map = new google.maps.Map(document.getElementsByClassName('gMap')[i], {
                center: { lat: defaultLat, lng: defaultLong },
                zoom: 17,
            });
            const input = document.getElementsByClassName('gMap-input')[i];
            const autocomplete = new google.maps.places.Autocomplete(input);
            autocomplete.bindTo("bounds", map);
            // Specify just the place data fields that you need.
            autocomplete.setFields(["place_id", "geometry", "name"]);
            map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
            const infowindow = new google.maps.InfoWindow();
            const infowindowContent = document.getElementsByClassName('gMap-content')[i];
            infowindow.setContent(infowindowContent);
            const marker = new google.maps.Marker({ map: map });
            marker.addListener("click", () => {
                infowindow.open(map, marker);
            });
            if (element.attr('data-show-marker') == true) {
                //var NewLatLng = new google.maps.LatLng(defaultLat, defaultLong);
            }
            autocomplete.addListener("place_changed", () => {
                infowindow.close();
                const place = autocomplete.getPlace();

                if (!place.geometry) {
                    return;
                }

                try {
                    $(element.find(".gmap-txtLatHidden")[0]).val(place.geometry.location.lat());
                    $(element.find(".gmap-txtLngHidden")[0]).val(place.geometry.location.lng());
                } catch (e) {

                }


                if (place.geometry.viewport) {
                    map.fitBounds(place.geometry.viewport);
                } else {
                    map.setCenter(place.geometry.location);
                    map.setZoom(17);
                }
                // Set the position of the marker using the place ID and location.

                marker.setPlace({
                    placeId: place.place_id,
                    location: place.geometry.location,
                });
                marker.setVisible(true);

                $(infowindowContent).find(".gMap-place-name").text(place.name);
                $(infowindowContent).find(".gMap-place-id").text(place.place_id);
                $(infowindowContent).children.namedItem(".gMap-place-address").text(place.formatted_address);
                infowindow.open(map, marker);

            });
        });
    }
}

debugger;

let __RequestVerificationToken = $("input[name=__RequestVerificationToken]");
let link = '';
if (element.hasAttribute('data-link')) {
    link = element.getAttribute('data-link');
} else {
    link = '/Site/Home/';
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


function GetCityInMap(e) {
    debugger;
    var _lat = parseFloat($("#hid_lat_" + e[0].value + "").val());
    var _lng = parseFloat($("#hid_lng_" + e[0].value + "").val());

    var myLatLng = { lat: _lat, lng: _lng };

    // Set the position of the marker using the place ID and location.
    mainmarker.setPosition(myLatLng);
    mainMap.setCenter(myLatLng);
    mainMap.setZoom(15);
}
