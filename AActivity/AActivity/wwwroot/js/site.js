function confirmDelete(uniqueId,isTrue) {
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;
    var deleteSpan = 'deleteSpan_' + uniqueId;

    if (isTrue) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }
}