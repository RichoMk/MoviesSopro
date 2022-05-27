$(document).ready(function () {
    console.log("Document Ready from included JS Script !!!")
});

$("#CategoryId").change(function () {
    var optionSelected = $("option:selected", this);
    /*console.log(optionSelected);*/
    // nacin 1 za vadenje na imeto
    //var optionName1 = optionSelected[0].innerHTML;
    //console.log("OptionName 1 :" + optionName1);
    // nacin 2 za vadenje na imeto
    var optionName2 = optionSelected.text();
    //console.log("OptionName 2:" + optionName2);

    //$("#CategoryName").val(optionSelected);
    //$("#CategoryName").val(optionName1);
    $("#CategoryName").val(optionName2);
    $("#CategoryName").text(optionName2);
});
$("#PublisherId").change(function () {
    var optionSelected = $("option:selected", this);
    //console.log(optionSelected);
    // nacin 1 za vadenje na imeto
    //var optionName1 = optionSelected[0].innerHTML;
    //console.log("OptionName 1 :" + optionName1);
    // nacin 2 za vadenje na imeto
    var optionName2 = optionSelected.text();
    //console.log("OptionName 2:" + optionName2);

    //$("#PublisherName").val(optionSelected);
    //$("#PublisherName").val(optionName1);
    $("#PublisherName").val(optionName2);
    $("#PublisherName").text(optionName2);

});
$("#DirectorId").change(function () {
    var optionSelected = $("option:selected", this);
    //console.log(optionSelected);
    // nacin 1 za vadenje na imeto
    //var optionName1 = optionSelected[0].innerHTML;
    //console.log("OptionName 1 :" + optionName1);
    // nacin 2 za vadenje na imeto
    var optionName2 = optionSelected.text();
    //console.log("OptionName 2:" + optionName2);

    //$("#DirectorName").val(optionSelected);
    //$("#DirectorName").val(optionName1);
    $("#DirectorName").val(optionName2);
    $("#DirectorName").text(optionName2);

});
$("#ActorId").change(function () {
    var optionSelected = $("option:selected", this);
    //console.log(optionSelected);
    // nacin 1 za vadenje na imeto
    //var optionName1 = optionSelected[0].innerHTML;
    //console.log("OptionName 1 :" + optionName1);
    // nacin 2 za vadenje na imeto
    var optionName2 = optionSelected.text();
    //console.log("OptionName 2:" + optionName2);

    //$("#ActorName").val(optionSelected);
    //$("#ActorName").val(optionName1);
    $("#ActorName").val(optionName2);
    $("#ActorName").text(optionName2);
});


$("#uploadPhoto").click(function () {
    var data = new FormData();
    var files = $("#PhotoUpload").get(0);
});
$("#uploadPhoto").click(function () {
    //console.log("#upload click");
    var data = new FormData();
    var files = $("#PhotoUpload").get(0).files;

    if (files.length > 0) {
        data.append("UploadedPhoto", files[0]);
        //console.log(data);
    }
    $.ajax({
        type: "POST",
        url: "/Movies/UploadPhoto",
        data: data,
        contentType: false,
        processData: false,
        success: function (data) {
            //console.log(data.dbPath);
            $("#PhotoURL").val(data.dbPath);
            alert("Photo is successfully uploaded");
        },
        error: function () {
            alert("Error Uploading Photo!");
        }
    });
});

$("#uploadActorPhoto").click(function () {
    var data = new FormData();
    var files = $("#ActorPhotoUpload").get(0);
});
$("#uploadActorPhoto").click(function () {
    //console.log("#upload click");
    var data = new FormData();
    var files = $("#ActorPhotoUpload").get(0).files;

    if (files.length > 0) {
        data.append("UploadedPhoto", files[0]);
        //console.log(data);
    }
    $.ajax({
        type: "POST",
        url: "/Actor/UploadActorPhoto",
        data: data,
        contentType: false,
        processData: false,
        success: function (data) {
            //console.log(data.dbPath);
            $("#PhotoURL").val(data.dbPath);
            alert("Photo is successfully uploaded");
        },
        error: function () {
            alert("Error Uploading Photo!");
        }
    });
});


