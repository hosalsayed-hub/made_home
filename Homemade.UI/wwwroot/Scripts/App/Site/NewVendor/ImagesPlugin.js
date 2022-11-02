


function ValidImage() {
    var fupownerlogo = document.getElementById("fupownerlogo").files;
    var fupstorelogo = document.getElementById("fupstorelogo").files;
    var fupcommercialcumber = document.getElementById("fupcommercialcumber").files;
    if (fupownerlogo.length > 0 &&
        fupstorelogo.length > 0 &&
        fupcommercialcumber.length > 0
    ) {
        return true;

    } else {
        return false;

    }
}


// img
function ConvertToBase64(e, imgid) {
    debugger;
    var fileUpload = $(e);
    var pID = '#' + fileUpload.attr('id').replace('fup', '');
    var imgID = fileUpload.attr('id').replace('fup', 'img');
    var devID = fileUpload.attr('id').replace('fup', 'Dev_');
    if (e.files && e.files[0]) {
        debugger;

        var FR = new FileReader();
        FR.addEventListener("load", function (e) {
            debugger;

            // $('.formError').hide();
            // $(pID).val(e.target.result.replace(/^data:.+;base64,/, ''));
            var srcData = e.target.result; // <--- data: base64
            var imgWrap = document.createElement('div');
            imgWrap.className = 'img_wrap';

            //var newImage = document.createElement('img');
            //newImage.className = 'img_set';
            //newImage.src = srcData;
            //document.getElementById(imgID).innerHTML = "";
            //document.getElementById(imgID).append(newImage);
            document.getElementById(imgid).src = srcData;
            $('.removebtn' + imgid.replace("img_", "") + '').removeClass("d-none");


            $("#" + devID).addClass('img_uploaded');

            $("#" + imgid).css("border-radius", "15px");
        }, false);

        FR.readAsDataURL($(fileUpload)[0].files[0]);
        processdata(imgid)
    }
    else {
        document.getElementById(imgID).innerHTML = "";
        $(pID).val('');
        document.getElementById(fileUpload.attr('id')).value = "";
    }
}

function processdata(id) {
    debugger;
    const file = document.querySelector("#" + id.replace("img_", "fup") + "").files[0];

    if (!file) return;

    const reader = new FileReader();

    reader.readAsDataURL(file);

    reader.onload = function (event) {
        debugger;

        const imgElement = document.createElement("img");
        imgElement.src = event.target.result;
        // document.querySelector("#input").src = event.target.result;

        imgElement.onload = function (e) {
            debugger;

            const canvas = document.createElement("canvas");
            const MAX_WIDTH = 400;

            const scaleSize = MAX_WIDTH / e.target.width;
            canvas.width = MAX_WIDTH;
            canvas.height = e.target.height * scaleSize;

            const ctx = canvas.getContext("2d");

            ctx.drawImage(e.target, 0, 0, canvas.width, canvas.height);

            const srcEncoded = ctx.canvas.toDataURL(e.target, "image/jpeg");

            // you can send srcEncoded to the server
            //document.querySelector("#output").src = srcEncoded;
            // process(id, srcEncoded);
            return srcEncoded;
        };
    };
}

function dataURLtoFile(dataurl, filename) {
    debugger;
    var arr = dataurl.split(','),
        mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]),
        n = bstr.length,
        u8arr = new Uint8Array(n);

    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }

    return new File([u8arr], filename, { type: mime });
}


function RemoveImage(id) {

    var img = "img_" + id;
    var fup = "fup" + id;
    var dev = "Dev_" + id;
    document.getElementById(fup).value = "";

    document.getElementById(img).src = "data:image/svg+xml;base64,ICAgIDxzdmcgeG1sbnM9Imh0dHA6Ly93d3cudzMub3JnLzIwMDAvc3ZnIiB3aWR0aD0iNTIuOTYiIGhlaWdodD0iNTIuOTYiIHZpZXdCb3g9IjAgMCA1Mi45NiA1Mi45NiI+DQogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDxwYXRoIGlkPSJQYXRoXzE2MDYiIGRhdGEtbmFtZT0iUGF0aCAxNjA2IiBkPSJNMjYuNDgsMEEyNi40OCwyNi40OCwwLDEsMSwwLDI2LjQ4LDI2LjQ4LDI2LjQ4LDAsMCwxLDI2LjQ4LDBaIiBmaWxsPSIjZjFlM2JlIiAvPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8cGF0aCBpZD0iSWNvbl9hd2Vzb21lLWNsb3VkLXVwbG9hZC1hbHQiIGRhdGEtbmFtZT0iSWNvbiBhd2Vzb21lLWNsb3VkLXVwbG9hZC1hbHQiIGQ9Ik0yMi44NDksMTAuNTIxQTQuMDg1LDQuMDg1LDAsMCwwLDE5LjA0MSw0Ljk3YTQuMDU5LDQuMDU5LDAsMCwwLTIuMjY1LjY4OUE2LjgsNi44LDAsMCwwLDQuMDgsOS4wNWMwLC4xMTUsMCwuMjMuMDA5LjM0NGE2LjEyMiw2LjEyMiwwLDAsMCwyLjAzMiwxMS45SDIxLjc2MWE1LjQ0LDUuNDQsMCwwLDAsMS4wODgtMTAuNzdaTTE2LjcyLDEzLjEzSDEzLjk0djQuNzZhLjY4Mi42ODIsMCwwLDEtLjY4LjY4SDExLjIyYS42ODIuNjgyLDAsMCwxLS42OC0uNjhWMTMuMTNINy43NjFhLjY3OS42NzksMCwwLDEtLjQ4LTEuMTZsNC40OC00LjQ4YS42ODIuNjgyLDAsMCwxLC45NjEsMGw0LjQ4LDQuNDhhLjY4LjY4LDAsMCwxLS40OCwxLjE2WiIgdHJhbnNmb3JtPSJ0cmFuc2xhdGUoMTIuODggMTQuNzEpIiBmaWxsPSIjNjgyMzAwIiAvPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIDwvc3ZnPg==";

    $(".removebtn" + id).addClass('d-none');
    $("#" + id).val('');



}

function RemoveImageBox(id) {
    debugger;
    var img = "img_" + id;
    var fup = "fup" + id;
    var dev = "Dev_" + id;
    document.getElementById(fup).value = "";

    document.getElementById(img).src = "data:image/svg+xml;base64,PHN2ZyB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHdpZHRoPSIyMSIgaGVpZ2h0PSIyNS41IiB2aWV3Qm94PSIwIDAgMjEgMjUuNSI+DQogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8cGF0aCBpZD0iSWNvbl9tYXRlcmlhbC1maWxlLXVwbG9hZCIgZGF0YS1uYW1lPSJJY29uIG1hdGVyaWFsLWZpbGUtdXBsb2FkIiBkPSJNMTMuNSwyNGg5VjE1aDZMMTgsNC41LDcuNSwxNWg2Wm0tNiwzaDIxdjNINy41WiIgdHJhbnNmb3JtPSJ0cmFuc2xhdGUoLTcuNSAtNC41KSIgZmlsbD0iIzY4MjMwMCI+PC9wYXRoPg0KICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICA8L3N2Zz4=";
    $("#" + img).css("border-radius", "0px");

    $(".removebtn" + id).addClass('d-none');
    $("#" + id).val('');
}

