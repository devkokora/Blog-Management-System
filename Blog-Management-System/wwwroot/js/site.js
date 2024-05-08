// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let count = 0;
let onclick = true;

$(window).on('load', addNoise);

function addNoise() {
    if (count === 1) {
        $('.loader').text('Text here..');
    } else {
        $('.loader').text('Loading...');
    }
    $('.noise-wrapper').css('opacity', 1);
    $('.modal-footer-textarea').css('opacity', 0);
    $('.modal-footer-textarea-btn').css('display', "none");
    count++;
}

const textAreas = document.querySelectorAll(".modal-footer-textarea");
textAreas.forEach((textArea) => {
    textArea.addEventListener("click", (event) => {
        event.stopPropagation();
        $('.loader').text('');
        $('.noise-wrapper').css('opacity', 0);
        $('.modal-footer-textarea').css('opacity', 1);
        $('.modal-footer-textarea-btn').css('display', "block");
        onclick = true;
    });

    textArea.addEventListener("mouseenter", () => {
        if (onclick === false) {
            $('.loader').text('Loading...');
        }
    });
});

document.addEventListener("click", () => {
    addNoise();
    onclick = false;
});


const openedModals = document.querySelectorAll(".Opened-Modal");
const modals = document.querySelectorAll(".modal-trigger");

for (let i = 0; i < openedModals.length; i++) {
    openedModals[i].addEventListener("click", () => {

        let getModalShowId = "";

        for (let j = 0; j < modals.length; j++) {
            const getModalHideId = modals[j].id;
            if (i === j) {
                getModalShowId = getModalHideId;
            }
            $(`#${getModalHideId}`).modal('hide');
        }
        setTimeout(function () {
            $(`#${getModalShowId}`).modal('show');
        }, 500);
    });
}