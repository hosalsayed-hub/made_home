$(document).ready(function () {
    setTimeout(function () {
        LoadDrobDown()
    }, 2000);

    $(document).on('keyup', '#confirmPassword', function () {
        if ($("#Txt_password").val() !== $(this).val()) {
            $("#Txt_confirmPassword-error").html("Password Not Match");
            $("#Txt_confirmPassword-error").show();
        }
    });

    $(function () {
        try {
            initMap();
        } catch (e) {

        }
    });

});
function LoadDrobDown() {
    LoadCities();
    LoadActivities();
    LoadBanks();
    LoadWizzardNationality();
}


function LoadCities() {

    $($("#cities")).prop("disabled", false);
    $.ajax({
        type: "GET",
        url: '/Site/Home/LoadCities',
        async: true,
        dataType: "json",
        contentType: "application/x-www-form-urlencoded",
        success: function (result) {
            $("#cities").html('');
            $("#cities").append('<option></option>');
            for (var i = 0; i < result.length; i++) {
                var item = result[i];

                $("#cities").append('<option value="' + item.cityID + '">' + item.cityName + '</option>');

            }
            //   $($("#cities")).select2();
        },
        error: function () {

        }
    });
}


function LoadBlocks(e) {
    var cityID = $(e).val();
    $($("#blocks")).prop("disabled", false);
    if (cityID != '' || cityID != undefined) {
        $.ajax({
            type: "GET",
            url: '/Site/Home/LoadBlocks',
            async: true,
            data: { 'cityID': cityID },
            dataType: "json",
            contentType: "application/x-www-form-urlencoded",
            success: function (result) {
                $("#blocks").html('');
                $("#blocks").append('<option></option>');
                for (var i = 0; i < result.length; i++) {
                    var item = result[i];
                    $("#blocks").append('<option value="' + item.blockID + '">' + item.blockName + '</option>');
                }
                // $($("#blocks")).select2();
            },
            error: function () {

            }
        });
    }
}


function LoadActivities() {
    $($("#Activities")).prop("disabled", false);
    $.ajax({
        type: "GET",
        url: '/Site/Home/LoadActivities',
        async: true,
        dataType: "json",
        contentType: "application/x-www-form-urlencoded",
        success: function (result) {
            $("#Activities").html('');
            $("#Activities").append('<option></option>');
            for (var i = 0; i < result.length; i++) {
                var item = result[i];

                $("#Activities").append('<option value="' + item.activityID + '">' + item.activityName + '</option>');

            }
            // $($("#Activities")).select2();
        },
        error: function () {

        }
    });

}


function LoadBanks() {
    $($("#Banks")).prop("disabled", false);
    $.ajax({
        type: "GET",
        url: '/Site/Home/LoadBanks',
        async: true,
        dataType: "json",
        contentType: "application/x-www-form-urlencoded",
        success: function (result) {
            $("#Banks").html('');
            $("#Banks").append('<option></option>');
            for (var i = 0; i < result.length; i++) {
                var item = result[i];

                $("#Banks").append('<option value="' + item.banksID + '">' + item.banksName + '</option>');

            }
            // $($("#Banks")).select2();
        },
        error: function () {

        }
    });

}


function LoadPackages() {
    $($("#Packages")).prop("disabled", false);
    $.ajax({
        type: "GET",
        url: '/Site/Home/LoadPackages',
        async: true,
        dataType: "json",
        contentType: "application/x-www-form-urlencoded",
        success: function (result) {
            $("#Packages").html('');
            $("#Packages").append('<option></option>');
            for (var i = 0; i < result.length; i++) {
                var item = result[i];

                $("#Packages").append('<option value="' + item.packageID + '">' + item.packageName + '</option>');

            }
            //  $($("#Packages")).select2();
        },
        error: function () {

        }
    });

}

function LoadWizzardNationality() {

    $($("#WizzardNationalityID")).prop("disabled", false);
    $.ajax({
        type: "GET",
        url: '/Site/Home/LoadNationality',
        async: true,
        dataType: "json",
        contentType: "application/x-www-form-urlencoded",
        success: function (result) {
            $("#WizzardNationalityID").html('');
            $("#WizzardNationalityID").append('<option></option>');
            for (var i = 0; i < result.length; i++) {
                var item = result[i];

                $("#WizzardNationalityID").append('<option value="' + item.nationalityID + '">' + item.nationalityName + '</option>');

            }
            //   $($("#cities")).select2();
        },
        error: function () {

        }
    });
}






function CheckSendData() {
    // ownerlogo


    if (checkValidAllForm()) {
        debugger;
        var formData = new FormData();

        var fupownerlogo = document.getElementById("fupownerlogo").files;
        var fupstorelogo = document.getElementById("fupstorelogo").files;
        var fupcommercialcumber = document.getElementById("fupcommercialcumber").files;

        formData.append("fileowner", fupownerlogo[0]);
        formData.append("filestore", fupstorelogo[0]);
        formData.append("filecommerc", fupcommercialcumber[0]);

        var formDataspersonal = JSON.parse(JSON.stringify(jQuery('#formpersonal_data').serializeArray())) // store json object
        var formDatasstore = JSON.parse(JSON.stringify(jQuery('#formstore_data').serializeArray())) // store json object
        var formDataspayment = JSON.parse(JSON.stringify(jQuery('#formpayment_data').serializeArray())) // store json object

        for (var i = 0; i < formDataspersonal.length; i++) {
            formData.append(formDataspersonal[i].name, formDataspersonal[i].value)
        }

        for (var i = 0; i < formDatasstore.length; i++) {
            formData.append(formDatasstore[i].name, formDatasstore[i].value)
        }

        for (var i = 0; i < formDataspayment.length; i++) {
            formData.append(formDataspayment[i].name, formDataspayment[i].value)
        }
        // formData.append('__RequestVerificationToken', $('#__RequestVerificationToken').val());

        var radioValue = $("input[name='Gender']:checked").val();

        formData.append("gender", radioValue);
        formData.append("blocks", $("#blocks").val());
        formData.append("lat", $("#txtLatHidden").val());
        formData.append("lng", $("#txtLngHidden").val());



        //debugger;
        debugger;
        var object = {};
        formData.forEach((value, key) => object[key] = value);
        var json = JSON.stringify(object);


        // if (ValidImage()) {
        $.ajax({
            url: "/Site/Home/CheckVendorRegisterAndSendCode",
            method: "post",
            data: formData,
            contentType: false,
            enctype: "multipart/form-data",
            processData: false,
        }).done(function (data) {
            CheckformSuccess(data);
        }).fail(function (data) {
            console.log(data);
        })
        $("#loading-area").show();
        //} else {
        //    alert("يجب ارفاق جميع الصور والملفات")
        //}
    }
}
function SendData() {
    // ownerlogo


    if (checkValidAllForm()) {
        debugger;
        var formData = new FormData();

        var fupownerlogo = document.getElementById("fupownerlogo").files;
        var fupstorelogo = document.getElementById("fupstorelogo").files;
        var fupcommercialcumber = document.getElementById("fupcommercialcumber").files;

        formData.append("fileowner", fupownerlogo[0]);
        formData.append("filestore", fupstorelogo[0]);
        formData.append("filecommerc", fupcommercialcumber[0]);

        var formDataspersonal = JSON.parse(JSON.stringify(jQuery('#formpersonal_data').serializeArray())) // store json object
        var formDatasstore = JSON.parse(JSON.stringify(jQuery('#formstore_data').serializeArray())) // store json object
        var formDataspayment = JSON.parse(JSON.stringify(jQuery('#formpayment_data').serializeArray())) // store json object

        for (var i = 0; i < formDataspersonal.length; i++) {
            formData.append(formDataspersonal[i].name, formDataspersonal[i].value)
        }

        for (var i = 0; i < formDatasstore.length; i++) {
            formData.append(formDatasstore[i].name, formDatasstore[i].value)
        }

        for (var i = 0; i < formDataspayment.length; i++) {
            formData.append(formDataspayment[i].name, formDataspayment[i].value)
        }
        // formData.append('__RequestVerificationToken', $('#__RequestVerificationToken').val());

        var radioValue = $("input[name='Gender']:checked").val();

        formData.append("gender", radioValue);
        formData.append("blocks", $("#blocks").val());
        formData.append("lat", $("#txtLatHidden").val());
        formData.append("lng", $("#txtLngHidden").val());



        //debugger;
        debugger;
        var object = {};
        formData.forEach((value, key) => object[key] = value);
        var json = JSON.stringify(object);


        // if (ValidImage()) {
        $.ajax({
            url: "/Site/Home/VendorRegister",
            method: "post",
            data: formData,
            contentType: false,
            enctype: "multipart/form-data",
            processData: false,
        }).done(function (data) {
            debugger;
            formSuccess(data);
        }).fail(function (data) {
            debugger;
            console.log(data);
        })
        $("#loading-area").show();
        //} else {
        //    alert("يجب ارفاق جميع الصور والملفات")
        //}
    }
}
function CheckformSuccess(data) {
    if (data.status) {
        $("#VerviedCodeModal").modal('show');
        setTimeout(function () {
            $("#Div_Loading").hide();
            $("#Div_sendcode").show();
        }, 2000);
    }
    else {
        $("#messageBlock").attr("class", "alert alert-danger");
        $("#messageBlock").html(data.message);
    }
}
function SubmitVerfiedVendorRegisterCode() {
    $.ajax({
        type: "POST",
        url: "/site/Home/CheckOtpCode",
        data: {
            code: $("#VendorRegisterCode").val(),
        }
        , success: function (result) {
            $("#divMsgParentRegisterVendorCode").hide();
            if (result.status) {
                $("#divMsgParentRegisterVendorCode").slideUp();
                $("#divMsgParentRegisterVendorCode").show();
                $("#divMsgClassRegisterVendorCode").attr("class", "alert alert-success");
                $("#lblMsgRegisterVendorCode").html(result.message);
                window.setTimeout(() => {
                    $("#VerviedCodeModal").modal('hide');
                    SendData();
                }, 1000);
            }
            else {
                $("#divMsgParentRegisterVendorCode").slideUp();
                $("#divMsgParentRegisterVendorCode").show();
                $("#divMsgClassRegisterVendorCode").attr("class", "alert alert-site");
                $("#lblMsgRegisterVendorCode").html(result.message);
            }
        }
    });

}
function formSuccess(data) {
    debugger;
    if (data.status) {

        location.href = "/site/home/SendDone";
    }
    else {
        $("#messageBlock").attr("class", "alert alert-site");
        $("#messageBlock").html(data.message);
    }
}
