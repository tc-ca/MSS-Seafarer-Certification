function BlazorFocusElement(element) {
    if (element instanceof HTMLElement) {
        element.focus();
    }
}

function DisableSeafarerSearchButton() {
    $("#SeafarerSearch").prop('disabled', true);
    $("#SpinnerContainer").show();
}