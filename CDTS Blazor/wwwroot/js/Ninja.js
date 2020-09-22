function trigerInputFileButton()
{
    document.getElementById("Document").click();
}

$(function () {
    $('[data-toggle="popover"]').popover({
        placement: 'right',
        toggle: "popover",
        container: 'body',
        template: '<div class="popover popover-medium"><div class="arrow"></div><div class="popover-inner"><h3 class="popover-title"></h3><div class="popover-content"><p></p></div></div></div>',
    });
});

//$(document).ready(function () {
//    $('[data-toggle="popover"]').popover();
//});

//$(function () {
//    $('[data-toggle="popover"]').popover({
//        container: 'body'
//    })
//});