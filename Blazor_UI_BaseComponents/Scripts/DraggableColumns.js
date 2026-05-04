let draggedColumnIndex;

window.onDragStart = function (ev) {
    draggedColumnIndex = ev.target.cellIndex;
};

window.onDragOver = function (ev) {
    ev.preventDefault();
};

window.onDrop = function (ev, targetColumnIndex) {
    if (draggedColumnIndex === targetColumnIndex) return;

    const table = document.getElementById("_messagesTable");
    if (!table) return;
    for (let row of table.rows) {
        const cells = Array.from(row.cells);
        const draggedCell = cells[draggedColumnIndex];
        const targetCell = cells[targetColumnIndex];

        if (draggedColumnIndex < targetColumnIndex) {
            row.insertBefore(targetCell, draggedCell);
        }
        else {
            row.insertBefore(draggedCell, targetCell);
        }
    }
};