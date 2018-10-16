
function refresh() {
    $.ajax({
        method: "POST",
        url: window.location.href + "/Refresh",
        headers: {
            "RequestVerificationToken": $("input[name=__RequestVerificationToken]").val()
        },
        data: { gameId: $('#GameId').text()}
    })
        .always(function () {
            setTimeout(refresh, 100);
        })
        .done(function (html) {
            $("#GameField").html(html);
        });
}

$(function () {
    $(".action-btn").on('click', function () {
        $.ajax({
            method: "POST",
            url: window.location.href + "/Action",
            headers: {
                "RequestVerificationToken": $("input[name=__RequestVerificationToken]").val()
            },
            data: { gameId: $('#GameId').text(), action: $(this).attr('data-action') }
        })
            .done(function (html) {
                $("#GameField").html(html);
            });
    });
    setTimeout(refresh, 100);
})