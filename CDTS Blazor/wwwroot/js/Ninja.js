function trigerInputFileButton()
{
    document.getElementById("UploadedFiles").click();
    var fileDescriptionText = document.getElementById("FileDescription");
    fileDescriptionText.focus();
}

function changeCursorToBusy()
{
    document.body.style.cursor = 'wait';
}

function changeCursorToDefault()
{
    document.body.style.cursor = 'default';
}


window.initPopover = () => {
    $('[data-toggle="popover"]').popover({
        html:true,
        placement: 'right',
        toggle: "popover",
        container: 'body',
        template: '<div class="popover popover-medium"><div class="arrow"></div><div class="popover-inner"><h3 class="popover-title"></h3><div class="popover-content"><p></p></div></div></div>',
    });
};