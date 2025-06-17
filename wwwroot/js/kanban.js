function addColumnPrompt() {
    var columnName = prompt("Enter the name for the new column:");
    if (columnName) {
        addColumn(columnName);
    }
}

function addColumn(columnName) {
    fetch('/Project/AddColumn', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ projectId: projectId, columnName: columnName })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                location.reload();  // Перезагружаем страницу для обновления колонок
            } else {
                alert(data.message);
            }
        });
}

function deleteColumn(columnId) {
    if (confirm("Are you sure you want to delete this column?")) {
        fetch(`/Project/DeleteColumn?columnId=${columnId}`, { method: 'POST' })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    location.reload();
                } else {
                    alert("Failed to delete column: " + data.message);
                }
            });
    }
}

function renameColumnPrompt(columnId, currentName) {
    var newName = prompt("Enter the new name for the column:", currentName);
    if (newName) {
        renameColumn(columnId, newName);
    }
}

function renameColumn(columnId, newName) {
    fetch('/Project/RenameColumn', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ columnId: columnId, newName: newName })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                location.reload();
            } else {
                alert("Failed to rename column: " + data.message);
            }
        });
}