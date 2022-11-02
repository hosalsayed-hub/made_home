$(function () {
});
$(document).ready(function () {
    $('#formpersonal_data').validate({ ignore: [] });
    $('#formstore_data').validate({ ignore: [] });
    $('#formpayment_data').validate({ ignore: [] });


});
document.addEventListener("DOMContentLoaded", function () {
    var elements = document.getElementsByTagName("INPUT");
    for (var i = 0; i < elements.length; i++) {
        elements[i].oninvalid = function (e) {
            e.target.setCustomValidity("");
            if (!e.target.validity.valid) {
                e.target.setCustomValidity("This field cannot be left blank");
            }
        };
        elements[i].oninput = function (e) {
            e.target.setCustomValidity("");
        };
    }
})
var existmail = false;
var existmobile = false;
function nextTab(currentTab, nextTab) {
    debugger;
    if (checkStepValid(currentTab)) {
        existmail = false;
        existmobile = false;
        if (currentTab == "#formpersonal_data") {

            vendorExistmail();
            if (!existmail && !existmobile) {
                $(".step-btn").removeClass("active");
                $("#" + nextTab + "").addClass("active");
                $(".tab-content").hide();
                $('.' + nextTab.replace('form', '')).fadeIn();
                $(currentTab.replace('form', '')).addClass('step-done')
                $(currentTab.replace('form', '')).addClass('done');
            }

        } else {
            if (currentTab == "#formterms_data") {
                $("#messageBlock").attr("class", "");
                $("#messageBlock").html("");
                if ($("input[type='checkbox'][name='IsAgreeTerms']:checked").val() != 1) {
                    $("#messageBlock").attr("class", "alert alert-site");
                    $("#messageBlock").html("برجاء الموافقة على الشروط والاحكام");
                }
                else {
                    $(".step-btn").removeClass("active");
                    $("#" + nextTab + "").addClass("active");
                    $(".tab-content").hide();
                    $('.' + nextTab.replace('form', '')).fadeIn();
                    $(currentTab.replace('form', '')).addClass('step-done')
                    $(currentTab.replace('form', '')).addClass('done');
                }
            }
            else {

                $(".step-btn").removeClass("active");
                $("#" + nextTab + "").addClass("active");
                $(".tab-content").hide();
                $('.' + nextTab.replace('form', '')).fadeIn();
                $(currentTab.replace('form', '')).addClass('step-done')
                $(currentTab.replace('form', '')).addClass('done');
            }
        }




        //setTimeout(function () {
        //    try {
        //        initMap();
        //    } catch (e) {

        //    }
        //}, 3000);
    }

}
function prevTab(currentTab, nextTab) {
    debugger;
    //  if (checkStepValid(currentTab)) {

    $(".step-btn").removeClass("active");
    $("#" + nextTab + "").addClass("active");


    $(".tab-content").hide();
    $('.' + nextTab.replace('form', '')).fadeIn();
    $('.' + currentTab.replace('form', '')).addClass('step-done')




    //setTimeout(function () {
    //    try {
    //        initMap();
    //    } catch (e) {

    //    }
    //}, 3000);
    // }

}

$(".sendnow").click(function () {
    console.log("sendnow : ");


});

function vendorExistmail() {
    var _Txt_Email = $("#Txt_Email").val();
    var _Txt_Mobile = $("#Txt_Mobile").val();

    $.ajax({
        type: "GET",
        url: "/site/home/checkExistVendorData",
        async: false,
        data: {
            txt_Email: _Txt_Email,
            txt_Mobile: _Txt_Mobile,
        }
    }).done(function (data) {
        $("#messageBlock").attr("class", "");
        $("#messageBlock").html("");

        if (data.existEmail) {
            $("#messageBlock").attr("class", "alert alert-site");
            $("#messageBlock").html("البريد الالكترونى مسجل من قبل");
            existmail = true;
        }

        if (data.existMobile) {
            $("#messageBlock").attr("class", "alert alert-site");
            if (data.existEmail) {
                $("#messageBlock").html($("#messageBlock").html() + "<br />" + "رقم الجوال مسجل من قبل");
            }
            else {
                $("#messageBlock").html($("#messageBlock").html() + "رقم الجوال مسجل من قبل");
            }
            existmobile = true;

        }
    });
}


// validation
function checkValidAllForm() {
    debugger;
    console.log("checkValidAllForm ");

    var $eleid_ = $("#formpersonal_data");
    $eleid_.submit();

    var $eleid = $("#formstore_data");
    $eleid.submit();

    var $eleid__ = $("#formpayment_data");
    $eleid__.submit();


    return [$eleid.valid(), $eleid_.valid(), $eleid__.valid()].filter(Boolean).length === 3;
}
function checkStepValid(id) {
    console.log("checkStepValid : " + id);
    debugger;
    switch (id.toString()) {

        case "#formterms_data":

            return true;
            break;
        case "#formstore_data":
            var $eleid_ = $("#formpersonal_data");
            $eleid_.submit();

            var $eleid = $("#formstore_data");
            var valid = $eleid.valid() == $eleid_.valid();

            $eleid.submit();

            return [$eleid.valid(), $eleid_.valid()/*, $eleid__.valid()*/].filter(Boolean).length === 2;
            break;

        case "#formpayment_data":
            var $eleid_ = $("#formpersonal_data");
            $eleid_.submit();

            var $eleid = $("#formstore_data");
            $eleid.submit();

            //var $eleid__ = $("#formpayment_data");
            //$eleid__.submit();




            return [$eleid.valid(), $eleid_.valid()/*, $eleid__.valid()*/].filter(Boolean).length === 2;
            break;
        default:
            var $eleid_ = $("#formpersonal_data");
            $eleid_.submit();
            return $eleid_.valid();
            break;
    }
}

