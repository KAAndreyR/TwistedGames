
function sendAction(action) {
    var data = {
        gameId: $('#GameId').text(),
        action: action
    };
    sendRefreshRequest("/Action", data);
}

function sendRefreshRequest(handler, data, callback = null) {
    $.ajax({
        method: "POST",
        url: window.location.href + handler,
        headers: {
            "RequestVerificationToken": $("input[name=__RequestVerificationToken]").val()
        },
        data: data
    })
        .always(function () {
            if (callback !== null) {
                callback();
            }
        })
        .done(function (html) {
            $("#GameField").html(html);
        });

}

function refresh() {
    sendRefreshRequest("/Refresh", { gameId: $('#GameId').text() }, () => setTimeout(refresh, 50));
}

$(function () {
    $(".action-btn").on('click', (e) => {
        var action = $(e.target).attr('data-action');
        sendAction(action);
    });
    document.onkeyup = (e) => {
        switch (e.keyCode) {
            case 37:
            case 65:
                sendAction(4); //Left
                break;
            case 39:
            case 68:
                sendAction(3); //Right
                break;
            case 40:
            case 83:
                sendAction(2); //Down
                break;
            case 38:
            case 87:
                sendAction(1); //Up
                break;
            case 32:
                sendAction(0);
                break;
        }
    }
    setTimeout(refresh, 50);
})