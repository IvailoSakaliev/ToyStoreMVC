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
        }
    });
}

function ChangePriceTo() {
    var element = $("#PriceTo").val();
    if (element == 0) {
        alert("Please enter number > 0");
    }
    else {
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
            }
        });
    }
}


function ChangePriceFrom() {
    var element = $("#priceOF").val();
    if (element < 0) {
        alert("Price to can't negative");
    } else {
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
            }
        });
    }
}




function Restore() {
    $.ajax({
        url: '/Product/RestorePage',
        type: 'POST',
        dataType: 'json',
        data: { id :1 },
        success: function (data) {
            


        },
        error: function () {
        }
    });
}


function DeleteImage(id) {
    var fullURL = window.location.href;
    var last = fullURL.lastIndexOf('/');
    var differenceBetweenLastAndFullUrl = fullURL.length+1 - last;
    var page = fullURL.substr(last + 1, differenceBetweenLastAndFullUrl);
    $.ajax({
        url: '/Admin/DeleteImage',
        type: 'POST',
        dataType: 'json',
        data: { id: id },
        success: function (data) {
            if (data == "ok") {
                window.location.href = fullURL;
            }
            

        },
        error: function () {
        }
    });

}
function SetOrderInSession() {
    var fullURL = window.location.href;
    var last = fullURL.lastIndexOf('/');
    var differenceBetweenLastAndFullUrl = fullURL.length+1 - last;
    var page = fullURL.substr(last + 1, differenceBetweenLastAndFullUrl);
    var quantity = $(".quantity").val();

    $.ajax({
        url: '/Order/SaveOrderInSession',
        type: 'POST',
        dataType: 'json',
        data: { page: page , quantity: quantity},
        success: function (data) {
            if (data == "ok") {
                $(".alertBox").fadeIn(500);
                setTimeout(function() { $(".alertBox").fadeOut(500); }, 4000);
                $('.circle').css("display","block");
            }
            else if (data == "colision") 
            {
                $(".dangerBox").fadeIn(500);
                setTimeout(function() { $(".dangerBox").fadeOut(500); }, 4000);
            }

        },
        error: function () {
        }
    });

}


function leaveChange() {
    var quantity = parseInt($(".quantity").val());
    var price = $(".priceOfProduct").text();
    var total = $(".total");
    var result = quantity * price;
    total.html("Общо: " + result+ "лв.");
}


function OpenGalery()
{
    $('.galery').fadeIn(600);
    $('.navbar').fadeOut(600);
}
function CloseGalery() {
    $('.galery').fadeOut(600);
    $('.navbar').fadeIn(600);
}


function DeleteOrderProduct(id) {
    var fullURL = window.location.href;
    var last = fullURL.lastIndexOf('/');
    var differenceBetweenLastAndFullUrl = fullURL.length+1 - last;
    var page = fullURL.substr(last + 1, differenceBetweenLastAndFullUrl);
    $.ajax({
        url: '/Order/DeleteOrderProduct',
        type: 'POST',
        dataType: 'json',
        data: { id: id },
        success: function (data) {
            if (data == "ok") {
                window.location.href = fullURL;
            }
            

        },
        error: function () {
        }
    });

}

function ChangeQuantityOfProduct(id) {
        var element = $(".quantity").val();
        var fullURL = window.location.href;
         $.ajax({
                url: '/Order/ChangeQuantityOfProduct',
                type: 'POST',
                dataType: 'json',
                data: { id: id, element:element },
                success: function (data) {
                    if (data == "ok") {
                        window.location.href = fullURL;
                    }
                    

                },
                error: function () {
                }
            });


}
$(document).ready(function () {
    var sessionValue = $('.session').text().trim();
    if (sessionValue === null || sessionValue ==="") {
        $('.circle').css("display","none");
    }
    else
    {
        $('.circle').css("display","block")
    }
});