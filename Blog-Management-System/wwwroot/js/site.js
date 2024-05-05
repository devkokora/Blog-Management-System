﻿// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let count = 0;
let onclick = true;
$(window).on('load', addNoise);

function addNoise() {
    if (count === 1) {
        $('.loader').text('Text here..');
    } else {
        $('.loader').text('Signal loss..');
    }
    $('.noise-wrapper').css('opacity', 1);
    $('.modal-footer-textarea').css('opacity', 0);
    $('.modal-footer-textarea-btn').css('display', "none");
    count++;
}

const textArea = document.querySelector(".modal-footer-textarea");
textArea.addEventListener("click", (event) => {
    event.stopPropagation();
    $('.loader').text('');
    $('.noise-wrapper').css('opacity', 0);
    $('.modal-footer-textarea').css('opacity', 1);
    $('.modal-footer-textarea-btn').css('display', "block");
    onclick = true;
});

textArea.addEventListener("mouseenter", (event) => {
    if (onclick === false) {
    $('.loader').text('Loading...');

    }
});

textArea.addEventListener("mouseleave", (event) => {
    if (onclick === false) {
        addNoise();
    }
});

document.addEventListener("click", () => {
    addNoise();
    onclick = false;
});