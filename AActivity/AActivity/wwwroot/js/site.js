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

$(function () {

    function getNotificationCount() {
        $.ajax({
            url: "/NotiCount/getSignutreCount",
            method: "GET",
            success: function (resault) {
                $("#notiCount").html(resault.count);
                console.log(resault.count);
            },
            error: function (err) {
                console.log(err);
            }
        });
    };

    getNotificationCount();
})