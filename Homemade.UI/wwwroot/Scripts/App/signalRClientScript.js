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

connection.on("pushnotificationclient",
    function (orderID, orderGUID, type, statusID, message, title, notificationID) {

       // Trip_Cancel = 10,
       // Trip_NotFound = 11,
         
        debugger;
        switch (parseInt(type.toString())) {
            case 10:  
                var message = `
                <li class="isseenoperation">
                    <a tabindex="-1" href="#" onclick="OpenNotificationandSeen('`+ notificationID+`', '`+ orderGUID + `', '` + type + `', '` + statusID+`')">
                        <h6> `+ title+`</h6>
                        <p class="remove-margin">
                            `+ message+`
                                                </p>
                    </a>
                </li>`;

                $('.box-noti-notfound').append(message);
                 
                 
                $('.count-notfound').html(parseInt($('.count-notfound').html()) + 1);
                break;

            case 11:  
                var message = `
                <li class="isseenoperation">
                    <a tabindex="-1" href="#" onclick="OpenNotificationandSeen('`+ notificationID + `', '` + orderGUID + `', '` + type + `', '` + statusID +`')">
                        <h6> `+ title + `</h6>
                        <p class="remove-margin">
                            `+ message + `
                                                </p>
                    </a>
                </li>`;
                $('.box-noti-cancel').append(message);
                
                $('.count-cancel').html(parseInt($('.count-cancel').html()) + 1)

                break;
           
            default:
        }

    });

connection.on("updateMonitoringStatus",
    function (tripmasterid, attempt, status, tripmasterGuid, tripmastertime, branchLogo, branchName, Receivername, Receivermobile) {
        debugger;
        updateIconLocationClient();
    }
);
function updateIconLocationClient() {
    $.ajax({
        type: "GET",
        url: "/home/getClientIconsValues",

    }).done(function (data) {
        $('.countPending').html(data.countPending);
        $('.countActive').html(data.countActive);
        $('.countCancel').html(data.countCancel);
        $('.countNotFound').html(data.countNotFound);
        $('.countRes').html(data.countRes);
        $('.countDelivered').html(data.countDelivered);

    });
}
