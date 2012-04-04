var m_currentUser;
var m_boards = [];
var m_lastDate = null;
var m_currentDate = null;
var m_lastManual = null;

var DATE_FORMAT = "YY-MM-DD HH:mm";

$(function () {

    $(".activity").live("click", onCardClicked);

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

                $("#user").html("User: " + m_currentUser.fullName);

                $("#autoRefresh").attr("checked", "checked");

                m_lastManual = new Date();
                m_lastDate = new Date();
                m_currentDate = new Date();

                autoLoadActivity();
            },
            function (e) {
                alert("Could not authenticate. Sorry.");
            });
    }

});

function btnLoadActivity() {
    m_currentDate = m_lastDate;
    m_lastManual = m_lastDate;
    loadActivity();
}

function autoLoadActivity() {
    m_currentDate = m_lastManual;

    if ($("#autoRefresh").is(":checked")) {
        loadActivity();
    }

    setTimeout(function () { autoLoadActivity() }, 60000);
}

function loadActivity() {

    var updates = "";

    if (m_lastManual != null) {
        updates += "Your last interaction: " + moment(m_lastManual).format(DATE_FORMAT);
    }

    $("#lastRefresh").html(updates);
    
    m_boards = [];


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

                if (numboards == 1) {
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

    for (var i = 0; i < m_boards.length; i++) {

        var r = m_boards[i];

        if ($("#board-" + r.id).length == 0) {

            var newBoard = $("<div/>").append($("#template").html());
            newBoard.addClass("board");
            newBoard.attr("id", "board-" + r.id);
            newBoard.attr("boardid", r.id);
            newBoard.find(".name").html(r.name);
            var activities = newBoard.find(".activities");

            $("#boards").append(newBoard);
        }

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

                        var cardid = r[idx].data.card.id;

                        if ($("#card-" + cardid).length == 0) {
                            var act = $("<div/>").addClass("activity " + changed)
                                        .attr("cardid", cardid)
                                        .attr("timestamp", d.getTime())
                                        .attr("id", "card-" + cardid)
                                .append("<div class='who'>" + r[idx].memberCreator.fullName + " <small>(" + displayNameForType(r[idx]) + ")</small></div>")
                                .append("<div class='what'>" + displayContentForType(r[idx]) + "</div>")
                                .append("<div class='when'>" + moment(d).format(DATE_FORMAT) + "</div>");

                            var boardid = r[idx].data.board.id;
                            var activities = $("#board-" + boardid).find(".activities");
                            var addBefore = activities.find(".activity").filter(function () {
                                return $(this).attr("timestamp") < d.getTime().toString();
                            });

                            if (addBefore.length == 0) {
                                activities.append(act);
                            } else {
                                addBefore.first().before(act);
                            }
                        }
                    }
                }
            },
            function (e) {
                alert("Could not retrieve actions. Sorry.");
            });
    }
}

function displayContentForType(r) {
    var type = r.type
    switch (type) {
        case "createCard":
        case "commentCard":
        case "updateCard":
        case "commentCard":
        case "addMemberToCard":
        case "removeMemberFromCard":
        case "updateCheckItemStateOnCard":
        case "addAttachmentToCard":
        case "removeAttachmentFromCard":
        case "addChecklistToCard":
        case "removeChecklistFromCard":
        case "createList":
        case "updateList":
            return "Card: " + r.data.card.name;
            
            break;

    }

}

function displayNameForType(r) {
    var type = r.type
    switch (type) {
        case "createCard":
            return "Created";
            break;
        case "commentCard":
            return "New Comment";
            break;
        case "updateCard":
            return "Updated";
            break;

        case "commentCard":
            return "New Comment";
            break;
        case "addMemberToCard":
            return "Assigned";
            break;
        case "removeMemberFromCard":
            return "De-Assigned";
            break;
        case "updateCheckItemStateOnCard":
            return "De-Assigned";
            break;
        case "addAttachmentToCard":
            return "Attachment added";
            break;
        case "removeAttachmentFromCard":
            return "Attachment removed";
            break;
        case "addChecklistToCard":
            return "Checklist added";
            break;
        case "removeChecklistFromCard":
            return "Checklist removed";
            break;
        case "createList":
            return "List added";
            break;
        case "updateList":
            return "List updated";
            break;


    }

}

function onCardClicked() {

    var cardid = $(this).attr("cardid");
    var boardid = $(this).closest(".board").attr("boardid");

    Trello.get(
            '/boards/' + boardid + '/cards/' + cardid, 
            { 
                actions: "all", 
                members: "true"
            },
            function (r) {

                alert(r.url);
                window.open(r.url, "_trello");

            },
            function (e) {
                alert("Could not retrieve card. Sorry.");
            });

    
    

}