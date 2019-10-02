function openit(evt, Name,actionEle) {

    var i, tabcontent, tablinks;
    $('.tabcontent').each(function (index, element) {
        $(element).removeClass('active');
    });
    $('.tabbutton').each(function (index, element) {
        $(element).removeClass("active");
    });
    $(actionEle).addClass("active");
    $('#' + Name).addClass('active');
   
}
