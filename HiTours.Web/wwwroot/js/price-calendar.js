(function calendarManager() {
    this.navigationType = { Next: "next", Previous: "previous", All: "all" };

    this.oSummary = {};

    this.oSelectors = {
        container: ".price-calender",
        calendars: "#checkInCalendar",
        calendarMonthText: "#checkInMonthText",
        moreCheckIn: "#moreCheckInAvailability",
        loadingLayer: "#loadingLayer",
        nextMonthLink: '#checkInFollowingMonth',
        prevMonthLink: '#checkInPreviousMonth',
        nextMonthJump: '#movesToNextMonth',
        prevMonthJump: '#movesToPreviousMonth',
        nightsInfo: "#nightsLabel",
        checkInInfo: "#checkIn",
        checkOutInfo: "#checkOut",
        excludeNotAvailable: ':not(.notAvailable)',
        excludeRoomLeft: ':not(.rooms-left)',
    };

    this.oSettings = {
        key: "hi-tours-booking",
        urls: {
            getCalendarDates: "/deal/getcalendar",
            checkOut: '/deal/booking/'
        }
    };

    this.fnToggleLink = function (navigationtype, active) {
        switch (navigationtype) {
            case this.navigationType.Next:
                $(this.oSelectors.nextMonthLink).toggleClass('is-inactive', !active);
                break;

            case this.navigationType.Previous:
                $(this.oSelectors.prevMonthLink).toggleClass('is-inactive', !active);
                break;

            case this.navigationType.All:
                $(this.oSelectors.nextMonthLink).toggleClass('is-inactive', !active);
                $(this.oSelectors.prevMonthLink).toggleClass('is-inactive', !active);
                break;
        }
    };

    this.fnGetCalendar = function () {
        this.fnSpinner(true);
        this.oSummary = this.fnGetSummary();
        var formData = {};
        for (var i in this.oSettings) {
            if (typeof this.oSettings[i] != "object") {
                formData[i] = this.oSettings[i]
            }
        }
        $.ajax({
            method: 'get',
            datatype: 'html',
            url: this.oSettings.urls.getCalendarDates,
            data: formData,
            success: function (html, textStatus, jqXHR) {
                fnRenderCalendar(html);

                fnSpinner(false);
            },
            complete: function () {
                fnSpinner(false);
            }
        });
    };

    this.fnRenderCalendar = function (html) {
        if (html != "") {
            $(this.oSelectors.calendars).html(html);
            if ($(this.oSelectors.calendars).find('.month.active').length == 0) {
                $(this.oSelectors.calendars).find('.month').eq(0).addClass("active");
            }
            $(this.oSelectors.moreCheckIn).html(html)
            $(this.oSelectors.calendars).find('.month').children("p").hide();
            $(this.oSelectors.calendars).find('.month').children("p").hide();

            $(this.oSelectors.calendars)
                .find(".calendar-day")
                .filter(this.oSelectors.excludeNotAvailable)
                .each(function () {
                    var nights = $(this).data("nights");
                    var currentDayId = +$(this).data('day-sequence');
                    var endDayId = currentDayId + nights;
                    var rooms = oSettings.rooms || 0;
                    var availableRooms = $(this).data("room-available") || 0;
                    var canAllocate = true;
                    for (var i = currentDayId; i <= endDayId; i++) {
                        var otherDay = $(".day[data-day-sequence=" + i + "]");
                        if (i < endDayId) {
                            otherDay = otherDay.filter(oSelectors.excludeNotAvailable)
                                .filter(oSelectors.excludeRoomLeft)
                        }
                        if (otherDay.length == 0) {
                            canAllocate = false;
                        }
                    }
                    if (!canAllocate) {
                        $(this).removeClass("allocationStartDay");
                    }
                });
            this.fnUpdateMonthHeader();
            this.fnUpdateMonthNavigation();
            this.fnInitDateSelection();
            this.fnSpinner(false);
        }
        else {
            $(this.oSelectors.container).html("<div align='center'>Calendar Not Available</div>");
        }
    };

    this.fnGetSummary = function (oData) {
        if (oData) {
            return this.oSummary;
        }
        return { nights: 0, checkIn: undefined, checkInDate: '', checkOut: undefined, checkOutDate: '', rooms: 0, roomType: '' };
    };

    this.fnUpdateSummary = function () {
        $(oSelectors.nightsInfo).removeClass('hide');
        $(oSelectors.nightsInfo).children("span").html((oSummary.nights || 0) + " Nights");
        oSummary.rooms = oSettings.rooms;
        oSummary.roomType = ($(".room-type-box.active").find(".room-type > h3").html() || '').trim();
        if (oSummary.checkIn) {
            oSummary.checkInDate = $(oSummary.checkIn).data("formatted-date");
            $(oSelectors.checkInInfo).html(oSummary.checkInDate);
        }
        if (oSummary.checkOut) {
            oSummary.checkOutDate = $(oSummary.checkOut).data("formatted-date");
            $(oSelectors.checkOutInfo).html(oSummary.checkOutDate);
        }
        var oData = {
            packageId: oSettings.packageid,
            roomTypeId: oSettings.roomTypeId,
            hotelPriceId: oSettings.hotelPriceId,
            rooms: oSummary.rooms,
            checkInDate: oSummary.checkInDate,
            checkOutDate: oSummary.checkOutDate
        };

        localStorage.setItem(this.oSettings.key, JSON.stringify(oData));
        $("#checkInDateLabel").removeClass('hide');
    };

    this.fnUpdateCalendar = function (dayClickedOn, lastDaySelected, isEanSelection) {
        var lastDaySelectedMonth = $(lastDaySelected).parents('.month');
        var ghostClass = isEanSelection ? '.day.eanGhost' : '.day.ghost';
        var checkInDayClass = isEanSelection ? '.day.eanCheckInDay' : '.day.checkInDay';
        var checkOutDayClass = isEanSelection ? '.day.eanCheckOutDay' : '.day.checkOutDay';
        $(oSelectors.nextMonthJump).remove();
        $(oSelectors.prevMonthJump).remove();

        if (+lastDaySelected.data('month') != +$(dayClickedOn).data('month')) {
            var lastDaySelectedFromCurrentMonth = $("#checkInCalendar").find('.month.active ' + ghostClass + ',.month.active ' + checkInDayClass).last();
            $(oSelectors.nextMonthJump).remove();
            lastDaySelectedFromCurrentMonth.append('<div id="movesToNextMonth" class="move-to-next-month"></div>');
            $(oSelectors.nextMonthJump).on("click", function (e) {
                $(oSelectors.nextMonthLink).click();
                e.stopPropagation();
            });

            var firstSelectedDayFromFollowingMonth = lastDaySelectedMonth.find(ghostClass + ',' + checkOutDayClass).first();
            $(oSelectors.prevMonthJump).remove();
            firstSelectedDayFromFollowingMonth.append('<div id="movesToPreviousMonth" class="move-to-previous-month"></div>');
            $(oSelectors.prevMonthJump).on("click", function (e) {
                $(oSelectors.prevMonthLink).click();
                e.stopPropagation();
            });
        }

        if (!$(dayClickedOn).parents('.month').hasClass('active')) {
            $('#checkInCalendar .month').removeClass('active');
            var monthId = $(dayClickedOn).parents('.month').data('allmonthssequence');
            $(".month[data-allmonthssequence=" + monthId + "]").addClass('active');
        }

        $("#dvMandatoryDates").addClass("hide");
    };

    this.fnGetFormattedDate = function (day) {
        return $(day).data('year') + "/" + $(day).data('month') + "/" + $(day).data('day');
    };

    this.fnSpinner = function (visible) {
        if (visible) {
            $(".calendar-spinner").removeClass('hide');
            $(this.loadingLayer).addClass('loading-layer');
        }
        else {
            $(".calendar-spinner").addClass('hide');
            $(this.loadingLayer).removeClass('loading-layer');
        }
    };

    this.fnUpdateMonthHeader = function () {
        var $activeMonth = $(this.oSelectors.calendars).find('.month.active .month-details');
        if ($activeMonth.length) {
            var month = $activeMonth.data("month");
            var year = $activeMonth.data("year");
            if (month && year) {
                $(this.oSelectors.calendarMonthText).text(month + " " + year);
            }
        }
    };

    this.fnUpdateMonthNavigation = function () {
        var activeMonths = $("#checkInCalendar .month.active");
        var months = $("#checkInCalendar .month:not(.disabled)");
        var firstAvailableMonthIndex = months.first().data('month-sequence');
        var lastAvailableMonthIndex = months.last().data('month-sequence');
        var firstActiveMonthIndex = activeMonths.first().data('month-sequence');
        var lastActiveMonthIndex = activeMonths.last().data('month-sequence');

        this.fnToggleLink(this.navigationType.All, false);
        if (firstActiveMonthIndex > firstAvailableMonthIndex) {
            this.fnToggleLink(this.navigationType.Previous, true);
        }
        if (lastActiveMonthIndex < lastAvailableMonthIndex) {
            this.fnToggleLink(this.navigationType.Next, true);
        }

        fnUpdateMonthHeader();
    };

    this.fnClearDateSelection = function () {
        function clearCheckInCalendarFixedEanSelection() {
            $(oSelectors.calendars).find(".day.eanCheckInDay").removeClass('eanCheckInDay');
            $(oSelectors.calendars).find(".day.eanCheckOutDay").removeClass('eanCheckOutDay');
            $(oSelectors.calendars).find(".day.eanGhost").removeClass("eanGhost");
        }
    };

    this.fnDayAllocation = function () {
        var numberOfNights = +$(this).data('nights');
        var extraNight = $(this).data("extra-nights") || false;
        var overnights = +$(this).data("overnight");
        var startDayId = +$(this).data('day-sequence');
        var endDayId = startDayId + numberOfNights;
        if ($(this).hasClass("available-extranight")) {
            var checkInDay = $(oSelectors.calendars).find(".day.checkInDay");
            var checkOutDay = $(oSelectors.calendars).find(".day.checkOutDay");
            startDayId = +$(checkInDay).data('day-sequence');
            endDayId = +$(this).data('day-sequence');
        }
        var lastDaySelected = $(oSelectors.calendars).find(".day[data-day-sequence='" + endDayId + "']");
        $(oSelectors.calendars).find(".day.checkInDay").removeClass('checkInDay');
        $(oSelectors.calendars).find(".day.checkOutDay").removeClass('checkOutDay');
        $(oSelectors.calendars).find(".day.ghost").removeClass("ghost");

        $('.available-extranight:not(.allocationStartDay)').unbind('click');
        $(oSelectors.calendars).find(".day.available-extranight").removeClass('available-extranight');
        var lastDaySelectedMonth = $(lastDaySelected).parents('.month');
        if (lastDaySelectedMonth.hasClass('disabled')) {
            lastDaySelectedMonth.removeClass('disabled');
        }

        $(".day[data-day-sequence=" + startDayId + "]").addClass('checkInDay');

        for (var dayId = startDayId + 1; dayId < endDayId; dayId++) {
            $(".day[data-day-sequence=" + dayId + "]").addClass('ghost');
        }
        $(".day[data-day-sequence=" + endDayId + "]").addClass('checkOutDay');

        oSummary.checkIn = $(oSelectors.calendars).find(".day.checkInDay").eq(0);
        oSummary.checkOut = $(oSelectors.calendars).find(".day.checkOutDay").eq(0);
        oSummary.nights = (oSummary.checkIn.length + (+ $(oSelectors.calendars).find(".day.ghost").length));

        var $endDay = $(".day[data-day-sequence=" + endDayId + "]");

        if (extraNight && !$endDay.hasClass("notAvailable")) {
            var $extraDay = $(".day[data-day-sequence=" + (endDayId + 1) + "]").eq(0);
            if ($extraDay.hasClass("notAvailable") &&
                !($(".day[data-day-sequence=" + (endDayId) + "]").eq(0).hasClass("notAvailable"))) {
                $extraDay.addClass('available-extranight');
            } else {
                $extraDay = $extraDay.filter(oSelectors.excludeRoomLeft).filter(oSelectors.excludeNotAvailable).eq(0);
                $extraDay.addClass('available-extranight');
            }
            $('.available-extranight:not(.allocationStartDay)').on("click", fnDayAllocation);
        }

        fnUpdateSummary();
        fnUpdateCalendar($(this), lastDaySelected, false);
        fnUpdateMonthNavigation();
    };

    this.fnInitDateSelection = function () {
        var _self = this;
        $(this.oSelectors.calendars).find('.day.allocationStartDay').mouseover(function () {
            var nights = $(this).data("nights");
            var extraNight = $(this).data("extra-nights") || false;
            var overNight = $(this).data("overnight");
            var currentDayId = +$(this).data('day-sequence');
            var endDayId = currentDayId + nights;
            var checkOutDay = $(oSelectors.calendars).find(".checkOutDay");
            if (checkOutDay.length > 0 && extraNight && ((+$(checkOutDay).data("day-sequence")) + 1) == currentDayId && !checkOutDay.hasClass("notAvailable")) {
            } else {
                for (var i = currentDayId; i < endDayId; i++) {
                    var $element = $(".day[data-day-sequence=" + i + "]").filter(oSelectors.excludeNotAvailable).filter(oSelectors.excludeNotAvailable);
                    $element.addClass('hoverGhost');
                    $(".day[data-day-sequence=" + endDayId + "]").addClass('hoverCheckOut');
                }
            }
        });
        $(document).on("mouseout", $(this.oSelectors.calendars).find('.day.allocationStartDay'), function () {
            $('.day.hoverGhost').removeClass('hoverGhost');
            $('.day.hoverCheckOut').removeClass('hoverCheckOut');
        });
        $(_self.oSelectors.calendars).find(".day.allocationStartDay").on("click", this.fnDayAllocation);
    };

    this.fnInit = (function () {
        var roomType = $(".room-type-box.active").find("select.room-number").eq(0);
        var options = roomType.data("options") || {};
        localStorage.removeItem(this.oSettings.key);
        this.oSettings = $.extend(oSettings, {
            packageid: $("[name='PackageId']").val() || '',
            roomTypeId: options.RoomTypeId || '',
            hotelPriceId: options.HotelPriceId || '',
            rooms: roomType.val() == "" ? 0 : parseInt(roomType.val()),
        });
        
        this.fnGetCalendar();
        $(this.oSelectors.prevMonthLink).on("click", function () {
            var currentMonth = $("#checkInCalendar .month.active");
            var prevMonth = currentMonth.prev();

            if (prevMonth.find('.day:not(.disabled)').length != 0 || prevMonth.find('.day.eanStartDay').length != 0) {
                currentMonth.removeClass('active');
                prevMonth.addClass('active');
            }
            fnUpdateMonthNavigation();
        });
        $(this.oSelectors.nextMonthLink).on("click", function () {
            if (!$(this).hasClass('is-inactive')) {
                var currentMonth = $("#checkInCalendar .month.active");
                var nextMonth = currentMonth.next();

                if (nextMonth.find('.day:not(.disabled)').length != 0) {
                    currentMonth.removeClass('active');
                    nextMonth.addClass('active');
                }
                fnUpdateMonthNavigation();
            }
        });

        $("select.room-number").each(function () {
            var options = $(this).data("options") || {};
            $(this).removeAttr("data-options");
            $(this).data(options);
        });

        $("select.room-number").on("change", function () {
            if (oSettings.roomTypeId) {
                var options = $(this).data("options") || {};
                oSettings.roomTypeId = options.RoomTypeId;
                oSettings.hotelPriceId = options.HotelPriceId;                
                oSettings.rooms = $(this).val() == "" || $(this).val() === null ? 0 : parseInt($(this).val());
                fnGetCalendar();
            }
        });

        $(document).on("click", '[data-action="book"]', function () {
            var summary = fnGetSummary(oSummary);
            if (summary.checkInDate != undefined && summary.checkInDate != '' &&
                summary.checkOutDate != undefined && summary.checkOutDate != '' &&
                summary.rooms > 0
            ) {
                window.location = oSettings.urls.checkOut + oSettings.packageid;
            } else {

                $("#dvMandatoryDates").removeClass("hide");
                $('html,body').animate({
                    scrollTop: $("#eanContainer").offset().top
                }, 'slow');
            }
        });

        $(document).on("click", ".room-type-header", function () {
            if (!$(this).parent(".room-type-box").hasClass("active")) {
                $(".room-type-box").removeClass("active")
                $(".room-type-box").find('.room-type-details').addClass("hide");
                $(this).parent(".room-type-box").addClass("active");
                $(this).parent(".room-type-box").find(".room-type-details").removeClass("hide");
                $(this).parent(".room-type-box").find(".room-type-details").find("select.room-number").val('1').change();
            }
        });

        $('.deal-countdown').each(function () {
            var timmerseconds = $(this).data("seconds") || 0;
            $(this).timeTo({
                seconds: parseInt(timmerseconds),
                displayDays: 2,
                fontSize: 16,
                captionSize: 9,
                displayCaptions: true
            }, function () {
                swal({
                    text: "Deal Expired",
                    type: 'warning',
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok'
                }).then(function () {
                    window.location = "/"
                });
                return false;
            });
        })
    })();
})();