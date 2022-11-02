var connection = new signalR.HubConnectionBuilder().withUrl("/payingHub").build();
connection.on("orderPaid", function (message) {
    debugger;
    if (message == "OK") {
        debugger;
        $("#PaidMessage").hide()
        $("#PaidBlock").html("تمت عمليه الدفع")
        $("#PaidBlock").show()
        ExecuteSendOrder(message);
    } else {
        $("#PaidBlock").hide()
        $("#PaidMessage").html("فشلت عمليه الدفع" + " <br />" + message)
        $("#PaidMessage").show()
    }
});
connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});

