(function () {
    ZoomMtg.setZoomJSLib('https://jssdk.zoomus.cn/1.8.6/lib', '/av');
    ZoomMtg.preLoadWasm();
    ZoomMtg.prepareJssdk();
    $(".close, .popup-overlay").on("click", function () {
        $("#chooseForm").modal("toggle");
    });
    debugger;
    var role = $("#meeting_role").val();
    if (role == "1") {
        //$("#chooseForm").modal('show');
        //$("#dialog").dialog();
        SetZoomUI();
    }
    else {


        SetZoomUI();

    }

    window.onbeforeunload = function () {
        if (performance.navigation.type == performance.navigation.TYPE_RELOAD) {

            debugger;
            //ZoomMtg.leaveMeeting({});
        }
        else {
            debugger;
            //ZoomMtg.endMeeting({});
        }
    }
    ZoomMtg.inMeetingServiceListener('onUserLeave', function (data) {
        debugger;
        console.log(data);
    });
})();
function SetZoomUI() {


    // it's option if you want to change the WebSDK dependency link resources. setZoomJSLib must be run at first
    // if (!china) ZoomMtg.setZoomJSLib('https://source.zoom.us/1.7.8/lib', '/av'); // CDN version default
    // else 

    var API_KEY = 'cjv-2-JaSW2_mlHcb54AoQ';

    /**
     * NEVER PUT YOUR ACTUAL API SECRET IN CLIENT SIDE CODE, THIS IS JUST FOR QUICK PROTOTYPING
     * The below generateSignature should be done server side as not to expose your api secret in public
     * You can find an eaxmple in here: https://marketplace.zoom.us/docs/sdk/native-sdks/web/essential/signature
     */
    var API_SECRET = 'G2jQaXYN3qwNvTfMSH0ujnFzCy4Uhqer6j72';

    //testTool = window.testTool;
    //if (testTool.getCookie("meeting_lang"))
    //    document.getElementById("meeting_lang").value = testTool.getCookie(
    //        "meeting_lang"
    //    );

    //document.getElementById("meeting_lang").addEventListener("change", (e) => {
    //    testTool.setCookie(
    //        "meeting_lang",
    //        document.getElementById("meeting_lang").value
    //    );
    //    ZoomMtg.i18n.load(document.getElementById("meeting_lang").value);
    //    ZoomMtg.i18n.reload(document.getElementById("meeting_lang").value);
    //    ZoomMtg.reRender({ lang: document.getElementById("meeting_lang").value });
    //});

    //document.getElementById('clear_all').addEventListener('click', function (e) {
    //    testTool.deleteAllCookies();
    //    document.getElementById('display_name').value = '';
    //    document.getElementById('meetingId').value = '';
    //    document.getElementById('meeting_pwd').value = '';
    //    // document.getElementById('meeting_lang').value = 'ar-EG';
    //    document.getElementById('meeting_role').value = 0;
    //});

    //document.getElementById('join_meeting').addEventListener('click', );
    //document.getElementById("join_meeting").click();


    function join_meeting() {
        //console.log(e.keyCode);
        //e.preventDefault();

        if (document.getElementById('meetingId').value == "") {
            alert("Enter Name and Meeting Number");
            return false;
        }
         var meetingNumber = parseInt(document.getElementById('meetingId').value);
        var meetConfig = {
            apiKey: API_KEY,
            apiSecret: API_SECRET,
            meetingNumber: parseInt(document.getElementById('meetingId').value),
            userName: document.getElementById('display_name').value,
            passWord: document.getElementById('meeting_pwd').value,
            leaveUrl: ("/Appointment/Appointments/finishvideo?MeetingId=" + meetingNumber),
            role: parseInt(document.getElementById('meeting_role').value, 10)
        };
        testTool.setCookie("meetingId", meetConfig.meetingNumber);
        testTool.setCookie("meeting_pwd", meetConfig.passWord);


        var signature = ZoomMtg.generateSignature({
            meetingNumber: meetConfig.meetingNumber,
            apiKey: meetConfig.apiKey,
            apiSecret: meetConfig.apiSecret,
            role: meetConfig.role,
            success: function (res) {
                console.log(res.result);
            }
        });

        ZoomMtg.init({
            leaveUrl: ("/Appointment/Appointments/finishvideo?MeetingId=" + meetingNumber),
            success: function () {
                ZoomMtg.join(
                    {
                        meetingNumber: meetConfig.meetingNumber,
                        userName: meetConfig.userName,
                        signature: signature,
                        apiKey: meetConfig.apiKey,
                        passWord: meetConfig.passWord,
                        success: function (res) {
                            debugger;

                            console.log('join meeting success');
                        },
                        error: function (res) {
                            console.log(res);
                        }
                    }
                );
            },
            error: function (res) {
                console.log(res);
            }
        });
        
        ZoomMtg.inMeetingServiceListener('onUserJoin', function (data) {
            console.log(data);
        });


        ZoomMtg.inMeetingServiceListener('onUserLeave', function (data) {
            console.log(data);
        });

        ZoomMtg.inMeetingServiceListener('onUserIsInWaitingRoom', function (data) {
            console.log(data);
        });

        ZoomMtg.inMeetingServiceListener('onMeetingStatus', function (data) {
            // {status: 1(connecting), 2(connected), 3(disconnected), 4(reconnecting)}
            console.log(data);
        });
    }

    join_meeting();
}


//function copyToClipboard(elementId) {
//    debugger;
//    var aux = document.createElement("input");
//    aux.setAttribute("value", document.getElementById(elementId).getAttribute('link'));
//    document.body.appendChild(aux);
//    aux.select();
//    document.execCommand("copy");
//    document.body.removeChild(aux);
//    $("#btncopyurl").hide();
//    $("#meetingcopylink").append(`<div class="meeting-info-icon__copy-button-row" id="msgbtnDone"><span class="meeting-info-icon__checked-icon"></span><span class="meeting-info-icon__copied-text">URL copied to Clipboard</span></div>`)
//    setTimeout(function () {
//        $("#btncopyurl").show();
//        $("#msgbtnDone").remove();

//    },10000)
//}


function copyToClipboard(containerid) {
    if (document.selection) {
        var range = document.body.createTextRange();
        range.moveToElementText(document.getElementById(containerid));
        range.select().createTextRange();
        document.execCommand("copy");
    } else if (window.getSelection) {
        var range = document.createRange();
        range.selectNode(document.getElementById(containerid));
        window.getSelection().addRange(range);
        document.execCommand("copy");
        $("#btncopyurl").hide();
        $("#meetingcopylink").append(`<div class="meeting-info-icon__copy-button-row" id="msgbtnDone"><span class="meeting-info-icon__checked-icon"></span><span class="meeting-info-icon__copied-text">URL copied to Clipboard</span></div>`)
        setTimeout(function () {
            $("#btncopyurl").show();
            $("#msgbtnDone").remove();

        }, 10000)
    }
}
