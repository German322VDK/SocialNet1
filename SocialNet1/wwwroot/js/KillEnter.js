﻿////$(document).ready(function () {
////    $(form).keydown(function (event) {
////        if (event.keyCode == 13) {
////            event.preventDefault();
////            return false;
////        }
////    });
////});

document.getElementById('form').addEventListener('keydown', function (event) {
    if (event.keyCode == 13)
        event.preventDefault();
});