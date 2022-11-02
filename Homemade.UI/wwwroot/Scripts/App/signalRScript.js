var connection = new signalR.HubConnectionBuilder().withUrl("/pushHub").build();
async function start() {
    try {
        await connection.start();
        console.log('wave hub started');
    } catch (err) {
        console.log('error ==> ' + err);
        // setTimeout(start, 5000);
    }
};


// Start the connection.
start();

//connection.onclose(start);

connection.on("ReceiveMessage", function (value, type) {
    debugger;
    switch (type) {
        case 9: // Trip
        case 1: // Created
            UpdateTripChartData();
            $("#tripVal").html(parseInt($("#tripVal").html()) + parseInt(value));
            $("#createdVal").html(parseInt($("#createdVal").html()) + parseInt(value));
            break;


        case 3: // Accepted
            UpdateTripChartData();
            $("#acceptedVal").html(parseInt($("#acceptedVal").html()) + parseInt(value));
            break;

        case 2: // Not_Found
            UpdateTripChartData();
            $("#notfoundVal").html(parseInt($("#notfoundVal").html()) + parseInt(value));
            break;


        case 5: // Arrived
            UpdateTripChartData();
            $("#arrivedVal").html(parseInt($("#arrivedVal").html()) + parseInt(value));
            break;


        case 8: // Canceled
            UpdateTripChartData();
            $("#canceledVal").html(parseInt($("#canceledVal").html()) + parseInt(value));
            break;


        case 6: // Start
            UpdateTripChartData();
            $("#startVal").html(parseInt($("#startVal").html()) + parseInt(value));
            break;


        case 7: // End
            UpdateTripChartData();
            $("#endVal").html(parseInt($("#endVal").html()) + parseInt(value));
            break;


        case 4: // On The Way
            UpdateTripChartData();
            $("#onTheWayVal").html(parseInt($("#onTheWayVal").html()) + parseInt(value));
            break;
        default:
    }

});

connection.on("ReceiveFinances", function (BaseCost, Collected, CapProfit, CompProfit, DebtComp, type) {
    debugger;
    switch (type) {
        case 0: // all
            UpdateFinancesChartData();
            $("#baseCostVal").html(parseInt($("#baseCostVal").html()) + parseInt(BaseCost));
            $("#collectedVal").html(parseInt($("#collectedVal").html()) + parseInt(Collected));
            $("#capProfitVal").html(parseInt($("#capProfitVal").html()) + parseInt(CapProfit));
            $("#compProfitVal").html(parseInt($("#compProfitVal").html()) + parseInt(CompProfit));
            $("#debtCompVal").html(parseInt($("#debtCompVal").html()) + parseInt(DebtComp));
            break;

        case 1: // BaseCost
            UpdateFinancesChartData();
            $("#baseCostVal").html(parseInt($("#baseCostVal").html()) + parseInt(BaseCost));
            break;

        case 2: // Collected
            UpdateFinancesChartData();
            $("#collectedVal").html(parseInt($("#collectedVal").html()) + parseInt(Collected));
            break;

        case 3: // CapProfit
            UpdateFinancesChartData();
            $("#capProfitVal").html(parseInt($("#capProfitVal").html()) + parseInt(CapProfit));
            break;

        case 4: // CompProfit
            UpdateFinancesChartData();
            $("#compProfitVal").html(parseInt($("#compProfitVal").html()) + parseInt(CompProfit));
            break;


        case 5: // DebtComp
            UpdateFinancesChartData();
            $("#debtCompVal").html(parseInt($("#debtCompVal").html()) + parseInt(DebtComp));
            break;

        default:
    }

});


connection.on("TripAlert", function (message, type) {
    debugger;
    updateIconLocation();
    switch (type) {
        case 2: // Driver = 2
            $('.box-noti').append("<li><a tabindex='-1' href=''><h6>Cancel By Driver</h6><p class='remove-margin'>" + message + "</p></a></li>");
            $('.box-alert').append("<a href='#'><i class='ion-chevron-right push-10-r'></i>" + message + "</a>");
            break;

        case 3: // Client = 3,
            $('.box-noti').append("<li><a tabindex='-1' href=''><h6>Cancel By Client</h6><p class='remove-margin'>" + message + "</p></a></li>");
            $('.box-alert').append("<a href='#'><i class='ion-chevron-right push-10-r'></i>" + message + "</a>");

            break;

        default:
    }

});



connection.on("updateNotFoundStatus",
    function (tripMasterID, status) {
        debugger;
        updateIconLocation();

        var ss = location.href.split("/");

        if (ss[5] == "TripNotFound" || ss[5] == "NotFoundRequests") {
            switch (status) {
                case "N": // Driver = 2
                    readNotFoundTable();

                    break;

                case "R": // Client = 3,

                    $("#tr_" + tripMasterID + "").remove();
                    break;

                default:
            }

        }

    });


connection.on("pushnotificationntatus",
    function (orderID, orderGUID, type, statusID, message, title, notificationID) {
        debugger;
       // Trip = 1,
       /// Driver_Balance = 2,
       /// Client_Balance = 3,
       /// PromoCode = 4,
       // Voucher = 5,
       // Register = 6,
       /// Receive_Orders = 7,
       /// Reward = 8,
       // Trip_Schedule = 9,
       // Trip_Cancel = 10,
       // Trip_NotFound = 11,
    updateIconLocation();

        debugger;
        switch (parseInt(type.toString())) {
            case 1:  
            case 5:  
            case 6:  
            case 2:  
                var message = `
                <li class="isseenoperation">
                    <a tabindex="-1" href="#" onclick="OpenNotificationandSeen('`+ notificationID+`', '`+ orderGUID + `', '` + type + `', '` + statusID+`')">
                        <h6> `+ title+`</h6>
                        <p class="remove-margin">
                            `+ message+`
                                                </p>
                    </a>
                </li>`;

                $('.box-noti-general').append(message);
                 
                if (parseInt($('.count-general').html()) <= 0) {
                    $('.noti-nodata-general').remove()
                }
                $('.count-general').html(parseInt($('.count-general').html()) + 1);
                break;

            case 9:  
                var message = `
                <li class="isseenoperation">
                    <a tabindex="-1" href="#" onclick="OpenScheduleNotificationandSeen(`+ notificationID + `, '` + orderGUID + `')">
                        <h6> `+ title + `</h6>
                        <p class="remove-margin">
                            `+ message + `
                                                </p>
                    </a>
                </li>`;
                $('.box-noti-schedule').append(message);
                if (parseInt($('.count-schedule').html()) <= 0) {
                    $('.noti-nodata-schedule').remove()
                }
                $('.count-schedule').html(parseInt($('.count-schedule').html()) + 1)

                break;

            case 10:
                var message = `
                <li class="isseenoperation">
                    <a tabindex="-1" href="#" onclick="OpenNotificationandSeen('`+ notificationID + `', '` + orderGUID + `', '` + type + `', '` + statusID + `')">
                        <h6> `+ title + `</h6>
                        <p class="remove-margin">
                            `+ message + `
                                                </p>
                    </a>
                </li>`;
                $('.box-noti-cancel').append(message);
                if (parseInt($('.count-cancel').html()) <= 0) {
                    $('.noti-nodata-cancel').remove()
                }
                $('.count-cancel').html(parseInt($('.count-cancel').html()) + 1)

                break;
            case 11:
                var message = `
                <li class="isseenoperation">
                    <a tabindex="-1" href="#" onclick="OpenNotificationandSeen('`+ notificationID + `', '` + orderGUID + `', '` + type + `', '` + statusID + `')">
                        <h6> `+ title + `</h6>
                        <p class="remove-margin">
                            `+ message + `
                                                </p>
                    </a>
                </li>`;
                $('.box-noti-notfound').append(message);
                if (parseInt($('.count-notfound').html()) <= 0) {
                    $('.noti-nodata-notfound').remove()
                }
                $('.count-notfound').html(parseInt($('.count-notfound').html()) + 1)

                break;
           
            default:
        }

    });

connection.on("updateMonitoringStatus",
    function (tripmasterid, attempt, status, tripmasterGuid, tripmastertime, branchLogo, branchName, Receivername, Receivermobile) {
        debugger;
        // NotFound = 2,
        //Accept = 3,
        //Received = 4,
        updateIconLocation();

        switch (status) {
            case "2": // NotFound = 2
                stopTimerMain();
                if (attempt == 1) {
                    NewNotFound();
                    var data = `<tr id="tr_` + tripmasterid + `" role="row" class="odd tr_tbl_1" style="background: rgb(221, 247, 232);"><td class="sorting_1"><input type="hidden" value="` + tripmasterid + `"><label class="arName"><a href="/Trip/TripHistory/NotFoundOrder/` + tripmasterGuid + `">` + tripmasterid + `</a></label></td><td><label>` + tripmastertime + `</label></td><td><img class="border img-avatar img-avatar-thumb remove-margin" src="` + branchLogo + `"><label class="enName">` + branchName + `</label></td></tr>`;
                    $(data).hide().prependTo("#tbl_NotFound_1 tbody").fadeIn(2000);

                    if ($(".tr_tbl_1").length > 5) {
                        $($(".tr_tbl_1")[$(".tr_tbl_1").length - 1]).remove();
                    }
                } else if (attempt == 2) {
                    var tdata = $("#tr_" + tripmasterid + "").fadeOut(1000);
                    setTimeout(function () { tdata.remove(); }, 1000);

                    var data = `<tr id="tr_` + tripmasterid + `" role="row" class="odd tr_tbl_2" style="background: rgb(243, 243, 132);"><td class="sorting_1"><input type="hidden" value="` + tripmasterid + `"><label class="arName"><a href="/Trip/TripHistory/NotFoundOrder/` + tripmasterGuid + `">` + tripmasterid + `</a></label></td><td><label>` + tripmastertime + `</label></td><td><img class="border img-avatar img-avatar-thumb remove-margin" src="` + branchLogo + `"><label class="enName">` + branchName + `</label></td></tr>`;
                    $(data).hide().prependTo("#tbl_NotFound_2 tbody").fadeIn(2000);

                    if ($(".tr_tbl_2").length > 5) {
                        $($(".tr_tbl_2")[$(".tr_tbl_2").length - 1]).remove();
                    }
                } else {
                    var tdata = $("#tr_" + tripmasterid + "").fadeOut(1000);
                    setTimeout(function () { tdata.remove(); }, 1000);

                    var data = `<tr id="tr_` + tripmasterid + `" role="row" class="odd tr_tbl_3" style="background: rgb(249, 234, 232);"><td class="sorting_1"><input type="hidden" value="` + tripmasterid + `"><label class="arName"><a href="/Trip/TripHistory/NotFoundOrder/` + tripmasterGuid + `">` + tripmasterid + `</a></label></td><td><label>` + tripmastertime + `</label></td><td><img class="border img-avatar img-avatar-thumb remove-margin" src="` + branchLogo + `"><label class="enName">` + branchName + `</label></td><td><label>` + attempt + `</label></td></tr>`;
                    $(data).hide().prependTo("#tbl_NotFound_3 tbody").fadeIn(2000);

                    if ($(".tr_tbl_3").length > 5) {
                        $($(".tr_tbl_3")[$(".tr_tbl_3").length - 1]).remove();
                    }
                }
                startTimerMain();

                break;

            case "3": // Accept = 3,
                stopTimerMain();

                var background = "";
                if (attempt == 1) {
                     background = 'background:#ddf7e8';
                }
                else if (attempt == 2) {
                     background = 'background:#f3f384';

                }
                else {
                     background = 'background:#f9eae8';
                }

                var tdata = $("#tr_" + tripmasterid + "").fadeOut(1000);
                setTimeout(function () { tdata.remove(); }, 1000);

                var data = `<tr id="tr_` + tripmasterid + `" role="row" class="odd tr_tbl_accepted" style="` + background + `"><td class="sorting_1"><input type="hidden" value="` + tripmasterid + `"><label class="arName"><a href="/Trip/TripDetails/Index/` + tripmasterGuid + `">` + tripmasterid + `</a></label></td><td><label>` + tripmastertime + `</label></td><td><img class="border img-avatar img-avatar-thumb remove-margin" src="` + branchLogo + `"><label class="enName">` + branchName + `</label></td><td><label>` + attempt + `</label></td></tr>`;
                $(data).hide().prependTo("#tbl_accepted tbody").fadeIn(2000);

                if ($(".tr_tbl_accepted").length > 5) {
                    $($(".tr_tbl_accepted")[$(".tr_tbl_accepted").length - 1]).remove();
                }

                startTimerMain();

                break;

            case "4": // Received = 4,
                stopTimerMain();

                var background = "";
                if (attempt == 1) {
                    background = 'background:#ddf7e8';
                }
                else if (attempt == 2) {
                    background = 'background:#f3f384';
                }
                else {
                    background = 'background:#f9eae8';
                }
                var tdata = $("#tr_" + tripmasterid + "").fadeOut(1000);
                setTimeout(function () { tdata.remove(); }, 1000);

                var data = `<tr id="tr_` + tripmasterid + `" role="row" class="odd tr_tbl_received" style="` + background + `"><td class="sorting_1"><input type="hidden" value="` + tripmasterid + `"><label class="arName"><a href="/Trip/TripDetails/Index/` + tripmasterGuid + `">` + tripmasterid + `</a></label></td><td><label>` + tripmastertime + `</label></td><td><img class="border img-avatar img-avatar-thumb remove-margin" src="` + branchLogo + `"><label class="enName">` + branchName + `</label></td></tr>`;
                $(data).hide().prependTo("#tbl_recived tbody").fadeIn(2000);

                if ($(".tr_tbl_received").length > 5) {
                    $($(".tr_tbl_received")[$(".tr_tbl_received").length - 1]).remove();
                }
                startTimerMain();

                break;

            case "5": // finish = 4,
            case "6": // cancel = 4,
                stopTimerMain();

                Finish();
                var tdata = $("#tr_" + tripmasterid + "").fadeOut(1000);
                setTimeout(function () {
                    tdata.remove();
                    startTimerMain();
                }, 1000);
               

                break;

            default:
        }
        debugger;

        var loc = location.href.split("/")[location.href.split("/").length-2]
        if (loc !== "Monitoring") {

            // SignalR notification Box
            if ($('.itemwavelist').length >= 5) {
                $('.itemwavelist')[$('.itemwavelist').length - 1].remove();
            }
            if (getCookie("Language") == "en") {
                var htmlmessage = "<div class='itemwavelist' id='item_" + tripmasterid + "' style='cursor:pointer' onclick='OpenNotFoundOrder(" + '"' + tripmasterGuid + '"' + "," + tripmasterid + ")'>" +
                    "<span class='spntxtwave'> There is a Order number #" + tripmasterid + " Not accepted by any driver</span>" +
                    "<hr />" +
                    "</div>" +
                    "<div style='clear:both'></div>";

                $('.div_noti_message').append(htmlmessage);
                $("#popup").addClass("open");
            } else {
                var htmlmessage = "<div class='itemwavelist' id='item_" + tripmasterid + "' style='cursor:pointer' onclick='OpenNotFoundOrder(" + '"' + tripmasterGuid + '"' + "," + tripmasterid + ")'>" +
                    "<span class='spntxtwave'> يوجد طلب رقم #" + tripmasterid + " لم يتم قبوله من أي سائق   </span>" +
                    "<hr />" +
                    "</div>" +
                    "<div style='clear:both'></div>";

                $('.div_noti_message').append(htmlmessage);
                $("#popup").show();
            }

        }


        //setTimeout(function () {

        //    $("#item_" + tripmasterid + "").remove();
        //    if ($('.itemwavelist').length <= 0) {
        //        $("#popup").removeClass("open");

        //    }
        //}, 20000);
    });
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
}
function NewNotFound() {
    var audio = new Audio('/sounds/notice.mp3');
    audio.play();
}
function Finish() {
    var audio = new Audio('/sounds/system.mp3');
    audio.play();
}

function updateIconLocation() {
    $.ajax({
        type: "GET",
        url: "/home/getIconsValues",

    }).done(function (data) {
        $('.menu-notfound').html(data.notfound);
        $('.count-notfound').html(data.notfound);
        
        $('.menu-active-trip').html(parseInt(data.accepted) + parseInt(data.received));
        $('.menu-cancel-requests').html(data.canceled);
        $('.count-cancel').html(data.canceled);
        
        $('.menu-schedule-requests').html(data.schedule);
        $('.count-schedule').html(data.schedule);
        
        $('.menu-pending-requests').html(data.pending);
        $('.menu-delivered-history').html(data.end);
        $('.menu-requests-history').html(data.trips);
    });
}
function OpenNotFoundOrder(tripmasterGuid, id) {
    $("#item_" + id + "").fadeOut();
    setTimeout(function () {
        $("#item_" + id + "").remove();
    }, 1000);
    window.open('/Trip/TripHistory/NotFoundOrder/' + tripmasterGuid, '_blank');
}