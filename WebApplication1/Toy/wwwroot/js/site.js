// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function ChangeView(id) {
    var fullURL = window.location.href;
    var page = fullURL.substr(fullURL.length - 1);
    GetCheck(id, page);
}

function GetCheck(id, page) {

    $.ajax({
        url: '/Product/ChangeViewProducts',
        type: 'POST',
        dataType: 'json',
        data: { id: id, page: page },
        success: function (data) {
            var fullURL = window.location.href;
            var page = fullURL.substr(fullURL.length - 1);
            if (data == "1") {

                var url = "../Product/ListProduct?Curentpage=" + page;
                $('.products').load(url)
            }

            else if (data == "2") {
                var url = "../Product/GaleryProduct?Curentpage=" + page;
                $('.products').load(url);
            }


        },
        error: function () {
            alert('error');
        }
    });
}

function ChangePriceTo() {
    var element = parseInt($("#PriceTo").val());
    ChangeProductByFilter(element)
}
function ChangeProductByFilter(element) {

    $.ajax({
        url: '/Product/FilterPriceTo',
        type: 'POST',
        dataType: 'json',
        data: { element: element },
        success: function (data) {
            var fullURL = window.location.href;
            var page = fullURL.substr(fullURL.length - 1);
            if (data == "1") {

                var url = "../Product/ListProduct?Curentpage=" + page;
                $('.products').load(url)
            }

            else if (data == "2") {
                var url = "../Product/GaleryProduct?Curentpage=" + page;
                $('.products').load(url);
            }


        },
        error: function () {
            alert('error');
        }
    });
}


function ChangePriceFrom() {
    var element = parseInt($("#prifeOF").val());
    if (element < 0) {
        alert("Price to can't negative");
    }
    ChangeProductByFilter(element)
}
function ChangeProductByFilter(element) {

    $.ajax({
        url: '/Product/FilterPriceFrom',
        type: 'POST',
        dataType: 'json',
        data: { element: element },
        success: function (data) {
            var fullURL = window.location.href;
            var page = fullURL.substr(fullURL.length - 1);
            if (data == "1") {

                var url = "../Product/ListProduct?Curentpage=" + page;
                $('.products').load(url)
            }

            else if (data == "2") {
                var url = "../Product/GaleryProduct?Curentpage=" + page;
                $('.products').load(url);
            }


        },
        error: function () {
            alert('error');
        }
    });
}




function PagerJ(id) {
    $.ajax({
        url: '/Product/ChangeFiltredResult',
        type: 'POST',
        dataType: 'json',
        data: { id: id },
        success: function (data) {
            var fullURL = window.location.href;
            var page = fullURL.substr(fullURL.length - 1);
            if (data == "1") {

                var url = "../Product/ListProduct?Curentpage=" + page;
                $('.products').load(url)
            }

            else if (data == "2") {
                var url = "../Product/GaleryProduct?Curentpage=" + page;
                $('.products').load(url);
            }


        },
        error: function () {
            alert('error');
        }
    });
}
