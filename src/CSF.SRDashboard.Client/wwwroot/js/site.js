function BlazorFocusElement(element) {
    if (element instanceof HTMLElement) {
        element.focus();
    }
}