$(document).ready(function () {
    $("#search").mouseenter(function () {
        $("#search").css("background-color", "rgba(100, 120, 140, 0.5)");
    });
    $("#search").mouseleave(function () {
        $("#search").css("background-color", "rgba(52, 58, 64, 0.5)");
    });

    $("#user").mouseenter(function () {
        $("#user").css("color", "lightcyan");
    });
    $("#user").mouseleave(function () {
        $("#user").css("color", "lightblue");
    });

    $("#cart").mouseenter(function () {
        $("#cart").css("background-color", "rgba(52, 95, 115, 0.5)");
    });
    $("#cart").mouseleave(function () {
        $("#cart").css("background-color", "rgba(52, 58, 64, 0.5)");
    });

    $("#ListItem").mouseenter(function () {
        $("#ListItem").css("background-color", "#ddffff");
    });
    $("#ListItem").mouseleave(function () {
        $("#ListItem").css("background-color", "lightgray");
    });

    $("#ListItem2").mouseenter(function () {
        $("#ListItem2").css("background-color", "#ddffff");
    });
    $("#ListItem2").mouseleave(function () {
        $("#ListItem2").css("background-color", "lightgray");
    });
});