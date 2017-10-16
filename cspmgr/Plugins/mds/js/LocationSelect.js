var hasMarker;
//var geocoder;
var placesapi;
var map;
var marker;
//地圖定位功能
function getMapLocation() {
    $('#ModalLocationSelect').modal();
}
function initialize() {
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(25.047923, 121.51708000000008);
    var mapOptions = {
        zoom: 16,
        center: latlng,
        panControl: false,
        zoomControl: true,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.SMALL,
            position: google.maps.ControlPosition.RIGHT_BOTTOM
        }
    };
    map = new google.maps.Map(document.getElementById('map-canvas'), mapOptions);
    myListener = google.maps.event.addListener(map, 'click', function (event) {
        placeMarker(event.latLng, false);
    });
    placesapi = new google.maps.places.PlacesService(map);
}
function codeAddress(address) {
    //改用places api增加地標解析精準度
    placesapi.textSearch({ query: address, bounds: map.getBounds() }, function (results, status) {
        if (status == google.maps.places.PlacesServiceStatus.OK) {
            map.setCenter(results[0].geometry.location);
            map.setZoom(16);
            placeMarker(results[0].geometry.location, true);
        }
    });
    //        geocoder.geocode({ 'address': address }, function (results, status) {
    //            if (status == google.maps.GeocoderStatus.OK) {
    //                map.setCenter(results[0].geometry.location);
    //                map.setZoom(16);
    //                placeMarker(results[0].geometry.location, true);
    //            } else {
    //                if (status != google.maps.GeocoderStatus.ZERO_RESULTS) {
    //                    alert('Geocode was not successful for the following reason: ' + status);
    //                }
    //            }
    //        });
}
function placeMarker(location, hasAddress) {
    if (hasMarker) {
        marker.setPosition(location);
    } else {
        hasMarker = true;
        marker = new google.maps.Marker({
            position: location,
            map: map,
            animation: google.maps.Animation.DROP,
            draggable: true
        });
        google.maps.event.addListener(marker, "dragend", function (mEvent) {
            updateLocation(mEvent.latLng);
            updateAddress(mEvent.latLng);
        });
    }
    updateLocation(location);
    if (!hasAddress) {
        updateAddress(location);
    }
}
function updateLocation(pos) {
    $('#maplocation').html("<strong>定位座標:</strong>" + pos.lat().toFixed(6) + "," + pos.lng().toFixed(6));
}
function updateAddress(pos) {
    var latlng = new google.maps.LatLng(pos.lat(), pos.lng());
    geocoder.geocode({ 'latLng': latlng }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            if (results[0]) {
                $('#mapaddress').val(results[0].formatted_address);
            } else {
                alert('No results found');
            }
        } else {
            alert('Geocoder failed due to: ' + status);
        }
    });
}
function getLocationFromMap() {
    $('#form-field-job-location').val($('#mapaddress').val());
    if (hasMarker) {
        // $('#location-info-label').text("經度:" + marker.position.lat() + ",緯度:" + marker.position.lng());
        $("#hidden_location_lat").val(marker.position.lat().toFixed(6));
        $("#hidden_location_lng").val(marker.position.lng().toFixed(6));
        updateLocationLabel();
    }
    else {
        clearLocation();
    }
}
function updateLocationLabel() {
    if ($("#hidden_location_lat").val().length > 0 &&
        $("#hidden_location_lng").val().length > 0) {
        $('#remove_location-btn').show();
        $('#location-info-label').text("(經度:" + $("#hidden_location_lat").val() + ",緯度:" + $("#hidden_location_lng").val() + ")");
    }
    else {
        $('#remove_location-btn').hide();
        $('#location-info-label').text("(尚未定位)");
    }
}
function clearLocation() {
    $("#hidden_location_lat").val("");
    $("#hidden_location_lng").val("");
    updateLocationLabel();
}

$("#ModalLocationSelect").on("shown.bs.modal", function () {
    //$("#ModalLocationSelect").on("shown", function () {
    hasMarker = false;
    initialize();
    var address = $('#form-field-job-location').val();
    $('#mapaddress').val(address);
    if (address.length > 0) {
        codeAddress(address);
    }
});