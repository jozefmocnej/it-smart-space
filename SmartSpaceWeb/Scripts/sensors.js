$(document).ready(function () {

    //initialize the loop
    RefreshPartial();
});

function RefreshPartial() {
    //this will wait 5 seconds and then fire the load partial function
    setTimeout(function () {
        loadPartialView();
        //recall this function so that it will continue to loop
        RefreshPartial();
    }, 10000);
}

function loadPartialView() {
    $.ajax({
        url: urlPartialVariable,
        dataType: 'html',
        type: 'get',
        cache: false,
        async: true,
        success: function (result) {
            $('#divPartial').html(result);
        }
    });
}