//this only works for static tables and can only be initialized once. 
//Tables that have removable columns, the columns would not have resizable functionality
window.initResizableTable = function (table) {
    if (!table || typeof table.querySelectorAll !== 'function') return;
    const resizers = table.querySelectorAll(".resizer");

    resizers.forEach(resizer => {
        const th = resizer.parentElement.parentElement;
        let startX, startWidth;

        var onMouseDownAction = (e) => {
            startX = e.pageX;
            startWidth = th.offsetWidth;

            const onMouseMove = (e) => {
                var newWidth = startWidth + (e.pageX - startX);

                const maxColumnWidth = table.offsetWidth * 0.6;
                newWidth = Math.min(newWidth, maxColumnWidth);

                th.style.width = newWidth + "px";
            };

            const onMouseUp = () => {
                document.removeEventListener("mousemove", onMouseMove);
                document.removeEventListener("mouseup", onMouseUp);
            };

            document.addEventListener("mousemove", onMouseMove);
            document.addEventListener("mouseup", onMouseUp);
        }

        resizer.addEventListener("mousedown", onMouseDownAction);
    });
};


window.getColumnWidths = function (table, excluded) {
    const result = {};
    if (!table || typeof table.querySelector !== 'function') return result;
    const headerRow = table.querySelector("tr");

    if (!headerRow) return result;

    const columns = headerRow.children;
    for (let i = 0; i < columns.length; i++) {
        var column = columns[i];
        var columnIndex = column.dataset.index;
        result[columnIndex] = column.offsetWidth;
    }

    return result;
}


window.getColumnWidth = function (table, columnIndex) {
    if (!table || typeof table.querySelector !== 'function') return 0;
    const headerRow = table.querySelector("tr");

    if (!headerRow) return 0;

    const columns = headerRow.children;
    for (let i = 0; i < columns.length; i++) {
        var column = columns[i];
        var orderIndex = column.dataset.index;
        if (orderIndex == columnIndex) {
            return column.offsetWidth;
        }
    }
    return 0;
}


window.resetEqualColumnWidths = function (table) {
    if (!table) return;
    const ths = table.querySelectorAll("thead th");
    if (!ths.length) return;

    const pct = 100 / ths.length;
    ths.forEach(th => th.style.width = pct + "%");
};


window.showAlert = function (message) {
    alert(message);
}



window.resizePushPull = (function () {
    const wired = new WeakSet();

    function init(thead) {
        if (!thead || typeof thead.addEventListener !== 'function' || wired.has(thead)) return;
        wired.add(thead);

        thead.addEventListener('mousedown', (e) => {
            const handle = e.target.closest('.resizer');
            if (!handle || !thead.contains(handle)) return;

            e.preventDefault();

            const th = handle.closest('th');
            const row = th.parentElement;
            const ths = Array.from(row.children);
            const idx = ths.indexOf(th);
            const nextTh = ths[idx + 1];

            //if next column is null you cant push/pull it
            if (!nextTh) return;

            const startX = e.pageX;
            const startW = th.offsetWidth;
            const nextStartW = nextTh.offsetWidth;
            const minW = Number.parseInt(getComputedStyle(th).minWidth) || 50;
            const minWNext = Number.parseInt(getComputedStyle(nextTh).minWidth) || 50;

            function onMove(ev) {
                const delta = ev.pageX - startX;

                let newW = startW + delta;
                let newNextW = nextStartW - delta;

                //new Value needs to be clamped to max-values so that they dont exceed limits
                if (newW < minW) {
                    newNextW -= (minW - newW);
                    newW = minW;
                }
                if (newNextW < minWNext) {
                    newW -= (minWNext - newNextW);
                    newNextW = minWNext;
                }

                th.style.width = newW + 'px';
                nextTh.style.width = newNextW + 'px';
            }

            function onUp() {
                document.removeEventListener('mousemove', onMove);
                document.removeEventListener('mouseup', onUp);
                //could technically add localstorage saving of width values here
            }

            document.addEventListener('mousemove', onMove);
            document.addEventListener('mouseup', onUp);
        });
    }

    function dispose(thead) {
        const s = stateByThead.get(thead);
        if (!s) return;
        thead.removeEventListener('mousedown', s.onMouseDown);
        stateByThead.delete(thead);
    }

    return { init, dispose };
})();