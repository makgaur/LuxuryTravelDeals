var calenderType = $('.calendar').data('calendar-type');
$(document).on('mouseenter', '.calendar-day', function (e) {
    if ($(".calendar .calendar-day").hasClass("selected-start") && $(".calendar .calendar-day").hasClass("selected-end")) {
        return false;
    }
    if (!$(this).hasClass('notAvailable') && $('.calendar').find('.calendar-day.selected-start').length > 0 && !$(this).hasClass('selected-start'))
    {
        $(".calendar .calendar-day").removeClass("hover-mid");
        if ($('.calendar .calendar-day.selected-start').data('cal-date-index') < $(this).data('cal-date-index'))
        {
            $(this).addClass('hover-end');
            var startFlag = false;
            
            $(".calendar .calendar-day").each(function (index, element) {
                // element == this
                if ($(element).hasClass('selected-start')) {
                    startFlag = true;
                }
                else if (startFlag) {
                    if ($(element).hasClass('hover-end')) {
                        return false;
                    }
                    $(element).addClass('hover-mid');
                }
            });
        }
    }
    else if (!$(this).hasClass('notAvailable')) {
        $(this).addClass('hover-start');
    }
});
$(document).on('mouseleave', '.calendar-day', function (e) {
    if ($(".calendar .calendar-day").hasClass("selected-start") && $(".calendar .calendar-day").hasClass("selected-end")) {
        return false;
    }
    $(".calendar .calendar-day").removeClass("hover-mid");
    if ($(this).hasClass('hover-start')) {
        $(this).removeClass('hover-start');
    }
    if ($(this).hasClass('hover-end')) {
        $(this).removeClass('hover-end');
    }
});
$(document).on('click', '.calendar-day', function (e) {
    //
    if (!$(this).hasClass('notAvailable') && $(this).hasClass('hover-start')) {
        $(this).addClass('selected-start');
        return false;
    }
    if ($(".calendar .calendar-day").hasClass("selected-start") && $(".calendar .calendar-day").hasClass("selected-end")) {
        $(".calendar .calendar-day").removeClass("hover-mid");
        $(".calendar .calendar-day").removeClass("selected-start");
        $(".calendar .calendar-day").removeClass("selected-end");
        if (calenderType === "home") {
            $("#searchDatepicker").data("startdate", null);
            $("#searchDatepicker").data("enddate", null);
            $("#searchDatepicker").data("selected", false);
            $('#searchDatepicker').val(null);
        }
        if (calenderType === "search") {
            $('.checkin_input').data('date', null);
            $('.checkin_input').val(null);
            $('.checkout_input').data('date', null);
            $('.checkout_input').val(null);
            $('.date_input_text').val(null);
        }
        if (calenderType === "product")
        {
            $('.calendar-proceed-btn').removeClass('active');
            $('.booking-date-container .check-in-date').val(null);
            $('.booking-date-container .check-out-date').val(null);
        }
        return false;
    }
    if ($('.calendar .calendar-day.selected-start').data('cal-date-index') > $(this).data('cal-date-index')) {
        $(".calendar .calendar-day").removeClass("hover-mid");
        $(".calendar .calendar-day").removeClass("selected-start");
        $(".calendar .calendar-day").removeClass("selected-end");
        return false;
    }
    if (!$(this).hasClass('notAvailable') && $(this).hasClass('hover-end')) {
        $(this).removeClass('hover-end');
        $(this).addClass('selected-end');
        var startDateElement;
        var endDateElement;
        
        //For Home Page Calender
        if (calenderType === "home") {
            startDateElement = $('.calendar').find(".selected-start").first();
            endDateElement = $('.calendar').find('.selected-end').first();
            var displayString = $(startDateElement).data("day") + " " + $(startDateElement).data("monthName") + ", " + $(startDateElement).data("year") + " - " + $(endDateElement).data("day") + " " + $(endDateElement).data("monthName") + ", " + $(endDateElement).data("year");
            $('#searchDatepicker').val(displayString);
            $("#searchDatepicker").data("startdate", $(startDateElement).data("date"));
            $("#searchDatepicker").data("enddate", $(endDateElement).data("date"));
            $("#searchDatepicker").data("selected", true);
            $('#searchDatepicker').parent('.filtersearch').removeClass("calender-open");
            if (checkForMobileView())
            {
                $('.destinationSearchBar').removeClass('hidden');
                $(".guest_room_selector").css({ "display": "block" });
                $(".recent_deals").css({ "display": "block" });
            }
        }
        //For Search Page Calender
        if (calenderType === "search") {
            startDateElement = $('.calendar').find(".selected-start").first();
            endDateElement = $('.calendar').find('.selected-end').first();
            var displayStringStartDate = $(startDateElement).data("day") + " " + $(startDateElement).data("monthName") + ", " + $(startDateElement).data("year");
            var displayStringEndDate = $(endDateElement).data("day") + " " + $(endDateElement).data("monthName") + ", " + $(endDateElement).data("year");
            $('.checkin_input').val($(startDateElement).data("date")).data('date', $(startDateElement).data("date"));
            $('.checkout_input').val($(endDateElement).data("date")).data('date', $(endDateElement).data("date"));
            $('.date_input_text').val(displayStringStartDate + "-" + displayStringEndDate);
            $('.dates .dropdown').removeClass('open');
        }
        //For Product Page Calender
        if (calenderType === "product") {
            startDateElement = $('.calendar').find(".selected-start").first();
            endDateElement = $('.calendar').find('.selected-end').first();
            $('.calendar-proceed-btn').addClass('active');
            $('.booking-date-container .check-in-date').val($(startDateElement).data('date'));
            $('.booking-date-container .check-out-date').val($(endDateElement).data('date'));
            
        }
        return false;
    }
});
var $firstSeperator = $('.calendar .month-seperator').first();
var $lastSeperator = $('.calendar .month-seperator').last();
$(document).on('click', '.nav-left', function (e) {
    e.stopPropagation();
    if ($firstSeperator.offset().left > $(".calender-container").offset().left)
    {
        return;
    }
    else {
        $('.calendar').animate({
            scrollLeft: "-=" + 335 + "px"
        }, "fast");
    }
    
});
$(document).on('click', '.nav-right', function (e) {
    e.stopPropagation();
    if (($lastSeperator.offset().left - 335) < ($(".calender-container").offset().left + 780))
    {
        return;
    }
    else {
        $('.calendar').animate({
            scrollLeft: "+=" + 335 + "px"
        }, "fast");
    }
});
$(document).ready(function (e) {
    if ($(".calender-container").length > 0) {
        $('.calendar').animate({
            scrollLeft: $firstSeperator.offset().left - $(".calender-container").offset().left - 50
        }, "fast");
    }
});



