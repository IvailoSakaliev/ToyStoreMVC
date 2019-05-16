// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function ChangeView(id) {
    GetCheck(id);
}

function GetCheck(id) {
    $.ajax({
        url: '/Product/ChangeViewProducts',
        type: 'POST',
        dataType: 'json',
        data: { id: id },
        success: function (data) {
            if (data == "1") {
                $('.products').load("Product/ListProduct")
            }
            else if (data == "2") {
                $('.products').load("Product/GaleryProduct");
            } 

            
        },
        error: function () {
            alert('error');
        }
    });
}