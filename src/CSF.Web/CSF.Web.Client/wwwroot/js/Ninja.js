function trigerInputFileButton()
{
    document.getElementById("Document").click();
}



var onloadCallback = function (dotNetReference)
{
    var recaptcha_tag = document.getElementById("reCapture_element");
    // if reCapture_element is not rendered yet, we will render this element.
    if (recaptcha_tag.hasChildNodes.length == 0)
    {
        grecaptcha.render('reCapture_element', {
            'sitekey': '6LfWoygaAAAAANbwE6tYQNzSWbFXme-cILTYB5RS',
            'callback': () => { dotNetReference.invokeMethodAsync('UpdateRecaptcha'); },
            'theme': 'light'
        }
        );
    }
};


function isRecaptchaChecked()
{
    var isChecked = null;

    if (grecaptcha && grecaptcha.getResponse().length > 0)
    {
        //alert('Well, recaptcha is checked !');
        isChecked = "YES";
    }

    return isChecked;
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