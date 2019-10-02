$(document).ready(function () {
    //Task ID 18
    var date = new Date();
    var day = date.getDay();
    if (day == '1' || day == '2' || day == '3' || day == '4' || day == '5') {
        var time = date.getHours();
        if (time >= 7 && time <= 19) {
            $('.ClientCallClick').show();
            //$('.ClientCallClick').remove();
            //$('#reqToCallBackTxt').show();
        }
        else {
            $('.ClientCallClick').remove();
            $('#reqToCallBackTxt').show();
        }
    }
    else {
        $('.ClientCallClick').remove();
        $('#reqToCallBackTxt').show();
    }
})

function GetQuickSearch(e) {
    var type = $(e).data('type');
    var name = $(e).data('name');
    if (type != null && name != null) {
        var item;
        if (type == destination) {
            item = { display: name, group: $(e).data('group'), matchedkey: "display" };
            setSearch(destination, item);
            updateURL(item.display, item.group);
        }
        else if (type == filterstyle) {
            item = { travelstyle: name, travelstyleid: $(e).data('group') };
            setSearch(filterstyle, item);
            updateURL(item.travelstyleid, filterstyle); 
        }

    }
}