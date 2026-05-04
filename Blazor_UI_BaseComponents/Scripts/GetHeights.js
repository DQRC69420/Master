window.getNavbarHeight = function (element) {
    if (!element || !(element instanceof HTMLElement)) return 0;
    return element.children[0]?.offsetHeight ?? 0;
};


window.resetAllRowHeights = function (tableEl) {
    if (!tableEl || typeof tableEl.querySelectorAll !== 'function') return;
    tableEl.querySelectorAll('[data-row-id]').forEach(div => {
        div.style.transition = 'none';
        div.style.height = '0px';
        requestAnimationFrame(() => { div.style.transition = ''; });
    });
};

window.setRowHeight = function (tableEl, rowId, expand) {
    if (!tableEl || typeof tableEl.querySelector !== 'function') return;
    const div = tableEl.querySelector(`[data-row-id="${rowId}"]`);
    if (!div) {
        return
    };

    if (expand) {
        div.style.height = div.scrollHeight + 'px';
        div.addEventListener('transitionend', () => { div.style.height = 'auto'; }, { once: true });
    } else {
        div.style.height = div.offsetHeight + 'px';
        requestAnimationFrame(() => { div.style.height = '0px'; });
    }
};

window.getPageHorizontalScrollbarHeight = function () {
    const scrollDiv = document.createElement("div");

    scrollDiv.style.position = "absolute";
    scrollDiv.style.top = "-9999px";
    scrollDiv.style.left = "0";
    scrollDiv.style.width = "100vw";
    scrollDiv.style.height = "100px";
    scrollDiv.style.overflowX = "scroll";
    scrollDiv.style.overflowY = "hidden";

    document.body.appendChild(scrollDiv);
    const scrollbarHeight = scrollDiv.offsetHeight - scrollDiv.clientHeight;
    document.body.removeChild(scrollDiv);

    return scrollbarHeight;
};