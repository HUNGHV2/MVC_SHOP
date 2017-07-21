/// <reference path="app2.js" />

$(document).ready(function () {
    var check = true;
    var url = window.location.href;
    if (url.indexOf("Edit")!=-1) {
        $(".img_View").show();
    } else {
        $(".img_View").hide();
    }
    $("#map_canvas").hide();
    $("#image").change(function () {
        $(".img_View").show();
        var preview = document.querySelector(".img_View");
        var file = document.querySelector('input[type=file]').files[0];
        var reader = new FileReader();

        reader.addEventListener("load", function () {
            preview.src = reader.result;
        }, false);

        if (file) {
            reader.readAsDataURL(file);
            
        }
    });
    $("#ms-map").click(function () {
        if (check) {
            $("#map_canvas").show();
            check = !check;
        } else {
            $("#map_canvas").hide();
            check = !check;
        }
     }
    );

});


