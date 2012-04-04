var m_currentUser;
var m_boards = [];
var m_lastDate = null;
var m_currentDate = null;

$(function () {
    var opt = {
        scope: {
            write: false
        },
        success: onAuthorized
    };

    if (!Trello.authorized()) {
        return Trello.authorize(opt);
    }
    else {
        onAuthorized();
    }

    function onAuthorized() {

        Trello.get(
            '/members/me', {},
            function (u) {
                m_currentUser = u;

                $("#user").html(m_currentUser.fullName);

                loadActivity();
            },
            function (e) {
                alert("Could not authenticate. Sorry.");
            });
    }
});

function loadActivity() {

    $("#boards").html('');
    m_boards = [];

    m_currentDate = m_lastDate;

    if (m_lastDate != null) {
        $("#lastRefresh").html("Last update: " + m_lastDate.toString());
    }
    else {
        $("#lastRefresh").html("First update");
    }
    var boards = m_currentUser.idBoards;
    var maxboards = boards.length;
    var numboards = maxboards;
    for (var i = 0; i < maxboards - 1; i++) {

        var boardID = boards[i];

        Trello.get(
            '/boards/' + boardID, {},
            function (r) {

                numboards--;

                if (!r.closed) {
                    m_boards.push(r);
                }
                
                if (numboards==1) {
                    loadBoards();
                }

            },
            function (e) {
                alert("Could not retrieve boards. Sorry.");
            });

    }
}

function loadBoards() {

    m_boards.sort(function (a, b) { return a.name.toLowerCase() > b.name.toLowerCase() });

    for(var i=0;i<m_boards.length; i++) {

        var r = m_boards[i];

        var newBoard = $("<div/>").append($("#template").html());

        newBoard.attr("id", "board-" + r.id);
        newBoard.find(".name").html(r.name);
        var activities = newBoard.find(".activities");

        $("#boards").append(newBoard);
                 
        Trello.get(
            '/boards/' + r.id + '/actions', {},
            function (r) {
                for (var idx = 0; idx < r.length - 1; idx++) {
                    if (r[idx].data.card != null) {
                        var d = new Date(r[idx].date);

                        var changed = '';
                        if (m_currentDate != null && d > m_currentDate) {
                            changed = 'changed';
                        }

                        if (d > m_lastDate) m_lastDate = d;

                        var act = $("<div class='activity " + changed + "'/>")
                                .append("<div class='who'>" + r[idx].memberCreator.fullName + " <small>(" + r[idx].type + ")</small></div>")
                                .append("<div class='what'>" + r[idx].data.card.name + "</div>")
                                .append("<div class='when'>" + d.toString() + "</div>")

                        var board = r[idx].data.board.id;
                        $("#board-" + board).find(".activities").append(act);

                    }
                }
            },
            function (e) {
                alert("Could not retrieve actions. Sorry.");
            });
    }
}