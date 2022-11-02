

function processdata(id) {
    debugger;
    const file = document.querySelector("#" + id.replace("img_","fup")+"").files[0];

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
            process(id, srcEncoded);
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

function process(id, dataURL) {
    var picfiles = dataURLtoFile(dataURL, document.getElementById(id.replace("img_", "fup")).files[0].name);
    debugger;
    switch (id) {
        case "img_CarPicture":
            fupCarPicturefiles = picfiles; 
            return;
        case "img_BackPicture":
            fupBackPicturefiles = picfiles; 
            return;
        case "img_RightSidePicture":
            fupRightSidePicturefiles = picfiles;
            return;
        case "img_LeftSidePicture":
            fupLeftSidePicturefiles = picfiles;
            return;
        case "img_PersonalPicture":
            fupPersonalPicturefiles = picfiles;
            return;
        case "img_IDPicture":
            fupIDPicturefiles = picfiles;
            return;
        case "img_LicensePicture":
            fupLicensePicturefiles = picfiles;
            return;
        case "img_CarLicensePicture":
            fupCarLicensePicturefiles = picfiles;
            return;
        default:
    }

    console.log(fupCarPicturefiles);
}
var fupCarPicturefiles;
var fupBackPicturefiles;
var fupRightSidePicturefiles;
var fupLeftSidePicturefiles;
var fupPersonalPicturefiles;
var fupIDPicturefiles;
var fupLicensePicturefiles;
var fupCarLicensePicturefiles;


function onBegin() {
    debugger;
    //Usage example:
   // var file = dataURLtoFile('data:text/plain;base64,aGVsbG8gd29ybGQ=', 'hello.txt');
   // var fupCarPicturefiles = dataURLtoFile(process("upload"), document.getElementById("upload").files.filename);


    //var fupCarPicturefiles = document.getElementById("fupCarPicture").files;
    //var fupBackPicturefiles = document.getElementById("fupBackPicture").files;
    //var fupRightSidePicturefiles = document.getElementById("fupRightSidePicture").files;
    //var fupLeftSidePicturefiles = document.getElementById("fupLeftSidePicture").files;
    //var fupPersonalPicturefiles = document.getElementById("fupPersonalPicture").files;
    //var fupIDPicturefiles = document.getElementById("fupIDPicture").files;
    //var fupLicensePicturefiles = document.getElementById("fupLicensePicture").files;
    //var fupCarLicensePicturefiles = document.getElementById("fupCarLicensePicture").files;
     
    var formData = new FormData();
    formData.append("fupCarPicture", fupCarPicturefiles);
    formData.append("fupBackPicture", fupBackPicturefiles);
    formData.append("fupRightSidePicture", fupRightSidePicturefiles);
    formData.append("fupLeftSidePicture", fupLeftSidePicturefiles);
    formData.append("fupPersonalPicture", fupPersonalPicturefiles);
    formData.append("fupIDPicture", fupIDPicturefiles);
    formData.append("fupLicensePicture", fupLicensePicturefiles);
    formData.append("fupCarLicensePicture", fupCarLicensePicturefiles);

    var formDatas = JSON.parse(JSON.stringify(jQuery('#SignupForm').serializeArray())) // store json object

    for (var i = 0; i < formDatas.length; i++) {
        formData.append(formDatas[i].name, formDatas[i].value)
    }
    var antiToken = $('#__RequestVerificationToken').val();

    formData.append('__RequestVerificationToken', antiToken);

    
    debugger;
    var $signupForm = $("#SignupForm");
   var ddd =  $signupForm.validationEngine("validate");
    if (ValidImage()) {
        $.ajax({
            type: "POST",
            url: "/Site/Home/CaptainRegister",
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
    }
}

function ValidImage() {
    var fupCarPicturefiles = document.getElementById("fupBackPicture").files;
    var fupBackPicturefiles = document.getElementById("fupBackPicture").files;
    var fupRightSidePicturefiles = document.getElementById("fupRightSidePicture").files;
    var fupLeftSidePicturefiles = document.getElementById("fupLeftSidePicture").files;
    var fupPersonalPicturefiles = document.getElementById("fupPersonalPicture").files;
    var fupIDPicturefiles = document.getElementById("fupIDPicture").files;
    var fupLicensePicturefiles = document.getElementById("fupLicensePicture").files;
    var fupCarLicensePicturefiles = document.getElementById("fupCarLicensePicture").files;

    if (fupCarPicturefiles.length > 0 &&
        fupBackPicturefiles.length > 0 &&
        fupRightSidePicturefiles.length > 0 &&
        fupLeftSidePicturefiles.length > 0 &&
        fupPersonalPicturefiles.length > 0 &&
        fupIDPicturefiles.length > 0 &&
        fupLicensePicturefiles.length > 0 &&
        fupCarLicensePicturefiles.length > 0
    ) {
        return true;

    } else {
        return false;

    }
}

 