var gameField;
var scoreElement;
var isStoppedElement;
var gameId;
var isStopped = true;
var height = 0;
var width = 0;
var cellValues;
var cellElements;
var cellClasses = ["empty-cell", "bonus-cell", "wall-cell", "snake-cell", "snake-head-cell", "tetris-figure-cell"];

function createGameField(state) {
    height = state.length;
    width = state[0].length;
    while (gameField.firstChild) {
        gameField.removeChild(gameField.firstChild);
    }
    var table = document.createElement("table");
    gameField.appendChild(table);
    cellElements = new Array(height);
    for (var i = height - 1; i >= 0; i--) {
        var row = table.insertRow();
        cellElements[i] = new Array(width);
        for (var j = 0; j < width; j++) {
            cellElements[i][j] = row.insertCell();
            cellElements[i][j].classList.add("field-cell");
        }
    }
    cellValues = null;
}

function renderGameField(state) {
    if (state.field.length !== height || state.field[0].length !== width) {
        createGameField(state.field);
    }
    for (var i = 0; i < height; i++) {
        for (var j = 0; j < width; j++) {
            if (cellValues == null || cellValues[i][j] !== state.field[i][j]) {
                if (cellValues != null) {
                    cellElements[i][j].classList.remove(cellClasses[cellValues[i][j]]);
                }
                cellElements[i][j].classList.add(cellClasses[state.field[i][j]]);
            }
        }
    }
    cellValues = state.field;
    scoreElement.innerHTML = state.score;
    isStopped = state.isStopped;
    isStoppedElement.style.display = isStopped
        ? ""
        : "none";
}

function sendAction(action) {
    var data = {
        gameId: gameId,
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
        .done(function (state) {
            renderGameField(state);
        });
}

function refresh() {
    sendRefreshRequest("/Refresh", { gameId: gameId }, () => setTimeout(refresh, isStopped ? 500 : 50));
}

var xDown = null;
var yDown = null;
var hasMove = false;
const touchDiff = 10;

function determineActionByTouchMove(e) {

    if (!xDown || !yDown) {
        return -1;
    }

    var xMove = e.touches[0].clientX;
    var yMove = e.touches[0].clientY;

    var xDiff = xDown - xMove;
    var yDiff = yDown - yMove;

    if (Math.abs(xDiff) > Math.abs(yDiff)) {
        if (Math.abs(xDiff) > touchDiff) {
            if (xDiff > 0) {
                /* left swipe */
                xDown -= touchDiff;
                return 4;
            } else {
                /* right swipe */
                xDown += touchDiff;
                return 3;
            }
        }
    } else {
        if (Math.abs(yDiff) > touchDiff) {
            if (yDiff > 0) {
                /* up swipe */
                yDown -= touchDiff;
                return 1;
            } else {
                /* down swipe */
                yDown += touchDiff;
                return 2;
            }
        }
    }
}

function setTouchEvents(field) {
    field.ontouchstart = e => {
        xDown = e.touches[0].clientX;
        yDown = e.touches[0].clientY;
        hasMove = false;
    };
    field.addEventListener("touchmove", e => {
        var action = determineActionByTouchMove(e);
        if (action >= 0) {
            sendAction(action);
            hasMove = true;
        }
        e.preventDefault();
    }, { passive: false, capture: true });
    field.ontouchcancel = () => {
        xDown = null;
        yDown = null;
    };
    field.ontouchend = () => {
        xDown = null;
        yDown = null;
        if (!hasMove) {
            sendAction(0);
        }
    };
}




function determinaActionByKeyCode(keyCode) {
    switch (keyCode) {
        case 37:
        case 65:
            return 4; //Left
        case 39:
        case 68:
            return 3; //Right
        case 40:
        case 83:
            return 2; //Down
        case 38:
        case 87:
            return 1; //Up
        case 32:
            return 0;
        default:
            return -1;
    }
};

$(function () {
    gameField = document.getElementById("game-field");
    scoreElement = document.getElementById("score");
    isStoppedElement = document.getElementById("is-stopped");
    gameId = document.getElementById("game-id").innerHTML;

    $(".action-btn").on("click", (e) => {
        var action = $(e.target).attr("data-action");
        sendAction(action);
    });
    document.onkeydown = e => {
        var action = determinaActionByKeyCode(e.keyCode);
        if (action >= 0) {
            sendAction(action);
            e.preventDefault();
        }
    };
    setTouchEvents(gameField);
    setTimeout(refresh, 50);
})