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
    this.hideCalander = false;
    this.oSettings = {
        key: "hi-tours-booking",
        urls: {
            getCalendarDates: "/package/getcalendar",
            checkOut: '/package/booking/',
            getRoomTypes: '/package/getroomtypes/'
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
        fnOData();
        if (!hideCalander) {

            //Scroll Code Start
            var $window = $(window),
                $element = $(oSelectors.calendars),
                elementTop = $element.offset().top,
                elementHeight = $element.height(),
                viewportHeight = $window.height(),
                scrollIt = elementTop - ((viewportHeight - elementHeight) / 2);
            $('html, body').stop().animate({
                scrollTop: scrollIt
            }, 1000);
            //Scroll Code End
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
                    bindRoomDetail();
                },
                complete: function () {
                    fnSpinner(false);
                }
            });
        }
        else {
            $("#loadingLayer").html('Please select your package to view dates.').addClass('loading-layer');
            $('#checkInCalendar').html('');
        }
        
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
        return { checkIn: undefined, checkInDate: '', checkOut: undefined, checkOutDate: '' };
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
        fnOData();
        var nights = $('li.package-nights.active:first').data() || {};
        var packageValidity = $('.room-type-box.active:first').data() || {};
        var flightIncluded = $('[name="Addflight"]').length > 0 ? $('[name="Addflight"]').prop("checked") : false;
        var roomdetail = [];
        $(".roomWisePax").each(function () {
            var $ele = $(this);
            roomdetail.push({
                "RoomNo": Number($ele.find('[name="RoomNo"]').val()),
                "Adult": Number($ele.find('[name="Adult"]').val()),
                "Child": Number($ele.find('[name="Child"]').val())
            })
        })
        var oData = {
            email: email || '',
            packageName: $('#hPackageName').data('name') || '',
            packageimg: location.origin + $(".first-slide").attr('src') || '',
            dealEnd: $('[name="calendarTo"]').eq(0).val() || '',
            dealStart: $('[name="calendarfrom"]').eq(0).val() || '',
            travelStyle: $('[name="TravelStyleName"]').val() || '',
            checkInDate: oSummary.checkInDate,
            checkOutDate: oSummary.checkOutDate,
            nights: oSummary.nights,
            packageId: oSettings.packageId,
            packageNightId: oSettings.packageNightId,
            packageNightsValidityId: oSettings.packageNightsValidityId,
            packageamount: String(nights.packageDisamount),
            url: window.location.href,
            discountPercent: Math.ceil(nights.packageDiscount || 0),
            roomtype: packageValidity.roomtype || '',
            packagecity: packageValidity.cityname || '',
            hotelroomtypeid: packageValidity.hotelroomtypeid || 0,
            rooms: 0,
            adults: Number($('[name="Adult"]').val()),
            childs: Number($('[name="Child"]').val()),
            infants: Number($('[name="Infant"]').val()),
            roomdetail: JSON.stringify(roomdetail), //$(".passengerType").find('select').serialize(),
            addflight: flightIncluded
        };
        this.oSummary = $.extend(oSummary, oData);
        localStorage.setItem(this.oSettings.key, JSON.stringify(oData));
        $("#checkInDateLabel").removeClass('hide');
        var depdate = '';
        if ($(this.oSummary.checkIn).length) {
            depdate = $(this.oSummary.checkIn).data("month") + "-" +
                $(this.oSummary.checkIn).data('day') + "-" +
                $(this.oSummary.checkIn).data("year")
        }
        var getFlight = $('[data-flight="get"]');
        if (getFlight.length > 0) {
            var url = '';
            var origin = packageValidity.cityname || '';
            var destination = $(getFlight).data("destination") || '';
            url = "/AirService/SearchFlights" + "?o=" + origin + "&d=" + destination + "&dtDep=" + depdate;
            $(getFlight).attr('href', url);
        }
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
        $('.day.checkInDay,.day.checkOutDay,.day.ghost').each(function () {
            $(this).find(".rate").html($(this).find('.rate').data("price"));
        })

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
            $(".day[data-day-sequence=" + dayId + "]").addClass('ghost hideprice');
        }
        $(".day[data-day-sequence=" + endDayId + "]").addClass('checkOutDay');

        oSummary.checkIn = $(oSelectors.calendars).find(".day.checkInDay").eq(0);
        oSummary.checkOut = $(oSelectors.calendars).find(".day.checkOutDay").eq(0);
        oSummary.nights = (oSummary.checkIn.length + (+$(oSelectors.calendars).find(".day.ghost").length));

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
        var priceApplied = $(".checkOutDay").eq(0).prev().find('.rate').html();
        $(".checkInDay").eq(0).find(".rate").data("price-applied", priceApplied).html(priceApplied);
        $(".checkInDay").find(".rate").html("").html($(".checkInDay").find(".rate").data("price") + "<br>Check In");
        $(".checkOutDay").find(".rate").html("").html("Check Out");
    };

    this.fnInitDateSelection = function () {
        var _self = this;
        $(this.oSelectors.calendars).find('.day.allocationStartDay').mouseover(function () {
           
            if ($("#checkInCalendar").find(".checkInDay").length <= 0)   //Changes Task 14
            {
                var nights = $(this).data("nights");
                var extraNight = $(this).data("extra-nights") || false;
                var overNight = $(this).data("overnight");
                var currentDayId = +$(this).data('day-sequence');
                var endDayId = currentDayId + nights;
                var checkOutDay = $(oSelectors.calendars).find(".checkOutDay");
                if (checkOutDay.length > 0 && extraNight && ((+$(checkOutDay).data("day-sequence")) + 1) == currentDayId && !checkOutDay.hasClass("notAvailable")) {
                }
                else {

                    for (var i = currentDayId; i < endDayId; i++) {
                        var $element = $(".day[data-day-sequence=" + i + "]").filter(oSelectors.excludeNotAvailable).filter(oSelectors.excludeNotAvailable);
                        if (i == currentDayId) {
                            var rateElement = $element.find(".rate").first();
                            $(rateElement).html("").html($(rateElement).data("price") + "<br>Check In");
                            $element.addClass('hoverCheckIn');
                        }
                        else {
                            $element.find(".rate").html($element.find('.rate').data("price"));
                        }
                        var $endDayElement = $(".day[data-day-sequence=" + endDayId + "]");
                        $element.addClass('hoverGhost');
                        $element.removeClass('hideprice');
                        //$element.find(".rate").html($element.find('.rate').data("price"));
                        $endDayElement.addClass('hoverCheckOut');
                        $endDayElement.find(".rate").html("<span style='color:black;'>Check Out</span>");
                    }
                }
            }
            else {
                $(this).addClass('hoverCheckIn');
            }
                //var nights = $(this).data("nights");
                //var extraNight = $(this).data("extra-nights") || false;
                //var overNight = $(this).data("overnight");
                //var currentDayId = +$(this).data('day-sequence');
                //var endDayId = currentDayId + nights;
                //var checkOutDay = $(oSelectors.calendars).find(".checkOutDay");
                //if (checkOutDay.length > 0 && extraNight && ((+$(checkOutDay).data("day-sequence")) + 1) == currentDayId && !checkOutDay.hasClass("notAvailable")) {
                //}
                //else {
                //    for (var i = currentDayId; i < endDayId; i++) {
                //        var $element = $(".day[data-day-sequence=" + i + "]").filter(oSelectors.excludeNotAvailable).filter(oSelectors.excludeNotAvailable);
                //        var $endDayElement = $(".day[data-day-sequence=" + endDayId + "]");
                //        $element.addClass('hoverGhost');
                //        $element.removeClass('hideprice');
                //        $element.find(".rate").html($element.find('.rate').data("price"));
                //        $endDayElement.addClass('hoverCheckOut');
                //        $endDayElement.find(".rate").html($endDayElement.find('.rate').data("price"));
                //    }
                //}         
        });
        $(document).on("mouseout", $(this.oSelectors.calendars).find('.day.allocationStartDay'), function () {
            $('.day.hoverGhost,.day.hoverCheckOut').each(function () {
                if (!$(this).hasClass("checkInDay") && !$(this).hasClass("checkOutDay")) {
                    $(this).find(".rate").html($(this).find('.rate').data("price-applied"));
                }
            })
            $('.day.hoverCheckIn').removeClass('hoverCheckIn');
            $('.day.hoverGhost').removeClass('hoverGhost');
            $('.day.hoverCheckOut').removeClass('hoverCheckOut');
        });
        $(_self.oSelectors.calendars).find(".day.allocationStartDay").on("click touchend", this.fnDayAllocation);
    };

    this.fnOData = function () {
        var packageValidity = $('.room-type-box.active:first').data() || {};
        var nights = $('li.package-nights.active:first').data() || {};
        if ($('li.package-nights.active').length == 0) {
            //$('li.package-nights:first').addClass('active');
            //packageValidity = $('.room-type-box:first').data() || {};
            nights = $('li.package-nights:first').data() || {};
            //return false;
        }
        if ($('.room-type-box.active').length == 0) {
            //$('.room-type-box:first').addClass('active');
            packageValidity = $('.room-type-box:first').data() || {};
            //nights = $('li.package-nights:first').data() || {};
            hideCalander = true;
            //this.oSettings = $.extend(oSettings, {
            //    packageId: nights.packageid || '',
            //    night: nights.night || 0,
            //    packageNightId: nights.nightid || '',
            //    packageNightsValidityId: packageValidity.packagenightsvalidityid || '',
            //    hotelroomtypeid: packageValidity.hotelroomtypeid
            //});
            //var strikePrice = String(packageValidity.packagediscountprice || '');
            //var packagePrice = String(packageValidity.packageprice || '');
            //var deposit = String(packageValidity.depositamount || '');

            //var validityPrice = String(packageValidity.validityprice || '');
            //var disPrice = $("#spnPackageDiscountPrice");
            //var isHotel = disPrice.data('ishotel');
            //if (isHotel == "True") {
            //    $("#spnPackageDiscountPrice").html(numberWithCommas((validityPrice)).replace('.00', ''));
            //    $("#spnPackagePrice").html(numberWithCommas((packagePrice)).replace('.00', ''));
            //}
            //else {

            //    $("#spnPackageDiscountPrice").html(numberWithCommas((validityPrice * 0.5)).replace('.00', ''));
            //    $("#spnPackagePrice").html(numberWithCommas((packagePrice * 0.5)).replace('.00', ''));
            //}
            //$("#spnDepositAmount").html(numberWithCommas(deposit.replace('.00', '')));
            //if (deposit <= 0) {
            //    $("#spnDepositAmount").parents(".depositDetails").hide();
            //}
            //else {
            //    $("#spnDepositAmount").parents(".depositDetails").show();
            //}
            //var noOfNights = parseInt($(".package-nights.active").data('night'));
            //var night = noOfNights > 1 ? "nights" : " night";
            //$("#spNoOfNights").html("   " + noOfNights + " " + night);
            //return false;
        }
        
        this.oSettings = $.extend(oSettings, {
            packageId: nights.packageid || '',
            night: nights.night || 0,
            packageNightId: nights.nightid || '',
            packageNightsValidityId: packageValidity.packagenightsvalidityid || '',
            hotelroomtypeid: packageValidity.hotelroomtypeid
        });

        var strikePrice = String(packageValidity.packagediscountprice || '');
        var packagePrice = String(packageValidity.packageprice || '');
        var deposit = String(packageValidity.depositamount || '');

        var validityPrice = String(packageValidity.validityprice || '');
        var disPrice = $("#spnPackageDiscountPrice");
        var isHotel = disPrice.data('ishotel');
        if (isHotel == "True") {
            $("#spnPackageDiscountPrice").html(numberWithCommas((validityPrice)).replace('.00', ''));
            $("#spnPackagePrice").html(numberWithCommas((packagePrice)).replace('.00', ''));
        }
        else {

            $("#spnPackageDiscountPrice").html(numberWithCommas((validityPrice * 0.5)).replace('.00', ''));
            $("#spnPackagePrice").html(numberWithCommas((packagePrice * 0.5)).replace('.00', ''));
        }
        $("#spnDepositAmount").html(numberWithCommas(deposit.replace('.00', '')));
        if (deposit <= 0) {
            $("#spnDepositAmount").parents(".depositDetails").hide();
        }
        else {
            $("#spnDepositAmount").parents(".depositDetails").show();
        }
        var noOfNights = parseInt($(".package-nights.active").data('night'));
        var night = noOfNights > 1 ? "nights" : " night";
        $("#spNoOfNights").html("   " + noOfNights + " " + night);
    }
    function numberWithCommas(number) {
        var parts = number.toString().split(".");
        parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return parts[0];
    }

    this.fnGetRoomTypes = function () {
        fnOData();
        var formData = {};
        for (var i in this.oSettings) {
            if (typeof this.oSettings[i] != "object") {
                formData[i] = this.oSettings[i]
            }
        }
        $('#tourPackageValidities').html(spinnerLoading)
        $.ajax({
            method: 'get',
            datatype: 'html',
            url: this.oSettings.urls.getRoomTypes,
            data: formData,
            success: function (html) {
                $('#tourPackageValidities').html(html);
                
                if (!oSettings.packageNightsValidityId) {
                    if (!hideCalander) {
                        fnGetCalendar();
                        fnOData();
                    }
                    else {
                        fnOData();
                        $("#loadingLayer").html('Please select your package to view dates.');
                    }
                }
                else {
                    setRoomNumberDetail($(".room-type-box.active select.room-number"));
                }
                //fnGetCalendar();

            },
            complete: function () {
            }
        });
    };
    this.fnInit = (function () {
        var roomType = $(".room-type-box.active").find("select.room-number").eq(0);
        var options = roomType.data("options") || {};
        localStorage.removeItem(this.oSettings.key);
        fnGetRoomTypes();

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
        $(document).on("change", ".room-type-box.active select.room-number", function () {
            setRoomNumberDetail($(this));
            //if (oSettings.packageNightsValidityId) {
            //    var options = $(this).data("options") || {};
            //    oSettings.packageNightsValidityId = options.packageNightsValidityId;
            //    oSettings.packageNightId = options.packageNightId;
            //    oSettings.rooms = $(this).val() == "" || $(this).val() === null ? 0 : parseInt($(this).val());
            //    fnGetCalendar();
            //}
            //bindRoomDetail();
        })

        //$("select.room-number").on("change", function () {
        //    if (oSettings.roomTypeId) {
        //        var options = $(this).data("options") || {};
        //        oSettings.roomTypeId = options.RoomTypeId;
        //        oSettings.hotelPriceId = options.HotelPriceId;
        //        oSettings.rooms = $(this).val() == "" || $(this).val() === null ? 0 : parseInt($(this).val());
        //        fnGetCalendar();
        //    }
        //});

        $(document).on("click", '[data-action="book"]', function () {
            fnUpdateSummary();

            var summary = fnGetSummary(oSummary);
            if (summary.checkInDate != undefined && summary.checkInDate != '' &&
                summary.checkOutDate != undefined && summary.checkOutDate != ''
            ) {
                if (summary.adults == 0 && summary.childs == 0 && summary.infants == 0) {
                    $(".persons").addClass('input-validation-error')
                } else if (summary.adults > 0 || summary.childs > 0 && summary.infants == 0) {
                    $(".persons").removeClass('input-validation-error')
                    fnOData();
                    var isLoggedin = $(':hidden[name="UserLoggedIn"]').length > 0;
                    window.location = oSettings.urls.checkOut + oSettings.packageId;
                    //if (isLoggedin) {
                    //    window.location = oSettings.urls.checkOut + oSettings.packageId;
                    //} else {
                    //    //$("#dvLoginModal").find("form#frmLogin").find(":hidden[name='ReturnUrl']")
                    //    //    .val(oSettings.urls.checkOut + oSettings.packageId);
                    //    //$("#dvLoginModal").modal({ keyboard: false, backdrop: false }).show();
                    //}

                    //showWaitProcess();
                    // open pop up and window.loaction url
                    ///  window.location = oSettings.urls.checkOut + oSettings.packageId;
                }
            } else {
                $('html,body').animate({
                    scrollTop: $("#durationsCall").offset().top
                }, 'slow');
                $("#dvMandatoryDates").removeClass("hide");
                //if ((/android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()))) {
                //    $('html,body').animate({
                //        scrollTop: $("#durationsCall").offset().top
                //    }, 'slow');
                //} else {
                //    $('html,body').animate({
                //        scrollTop: $("#eanContainer").offset().top
                //    }, 'slow');
                //}
            }
        });
        $(document).on("click", '[data-action="guestbook"]', function () {
            fnUpdateSummary();
            var summary = fnGetSummary(oSummary);
            if (summary.checkInDate != undefined && summary.checkInDate != '' &&
                summary.checkOutDate != undefined && summary.checkOutDate != ''
            ) {
                if (summary.adults == 0 && summary.childs == 0 && summary.infants == 0) {
                    $(".persons").addClass('input-validation-error')
                } else if (summary.adults > 0 || summary.childs > 0 && summary.infants == 0) {
                    var $guestmodel = $('#guestModal');
                    $guestmodel.find("#Password").val(parseInt(Math.random() * 10000)).addClass('hide');
                    $guestmodel.find('[type="submit"]').html('Join as a Guest');
                    $guestmodel.find('[name="registrationType"]').val('Guest');
                    $guestmodel.modal('show');
                    ////$(".persons").removeClass('input-validation-error')
                    ////fnOData();
                    ////window.location = oSettings.urls.checkOut + oSettings.packageId;
                }
            } else {
                $("#dvMandatoryDates").removeClass("hide");
                $('html,body').animate({
                    scrollTop: $("#durationsCall").offset().top
                }, 'slow');
                //if ((/android|webos|iphone|ipad|ipod|blackberry|iemobile|opera mini/i.test(navigator.userAgent.toLowerCase()))) {
                //    $('html,body').animate({
                //        scrollTop: $("#durationsCall").offset().top
                //    }, 'slow');
                //} else {
                //    $('html,body').animate({
                //        scrollTop: $("#eanContainer").offset().top
                //    }, 'slow');
                //}

            }
        });

        $(document).on("click", ".room-type-header", function () {
            var $parent = $(this).parent(".room-type-box");
            if (!$parent.hasClass("active")) {
                hideCalander = false;
                $("#loadingLayer").html('');
                $(".room-type-box").removeClass("active")
                $(".room-type-box").find('.room-type-details').addClass("hide");
                $parent.addClass("active");
                $parent.find(".room-type-details").removeClass("hide");
                fnOData();
                var $dropdown = $parent.find(".room-number");
                setRoomNumberDetail($dropdown);
            }
        });

        $('.deal-countdown').each(function () {
            var isupcoming = $(this).data('upcoming') != undefined;
            var timmerseconds = $(this).data("seconds") || 0;
            $(this).timeTo({
                seconds: parseInt(timmerseconds),
                displayDays: 2,
                fontSize: 16,
                captionSize: 9,
                displayCaptions: true
            }, function () {
                swal({
                    text: (isupcoming ? "Deal Started" : "Deal Expired"),
                    type: (isupcoming ? "success" : "warning"),
                    showCancelButton: false,
                    confirmButtonColor: '#3085d6',
                    confirmButtonText: 'Ok'
                }).then(function () {
                    if (isupcoming) {
                        location.reload(true);
                    }
                    else {
                        window.location = "/"
                    }
                });
                return false;
            });
        })

        $(document).on('click', 'li.package-nights', function () {
            fnOData();
            $('li.package-nights').removeClass('active');
            $(this).addClass('active');
            $("#loadingLayer").html('Please select your package to view dates.').addClass('loading-layer');
            $('#checkInMonthText').val('');
            $('#checkInCalendar').html('');
            hideCalander = true;
            fnGetRoomTypes();
        });
    })();
    this.setRoomNumberDetail = function ($dropdown) {
        if (oSettings.packageNightsValidityId) {
            var options = $dropdown.data("options") || {};
            oSettings.packageNightsValidityId = options.packageNightsValidityId;
            oSettings.packageNightId = options.packageNightId;
            oSettings.rooms = $dropdown.val() == "" || $dropdown.val() === null ? 0 : parseInt($dropdown.val());
            if (!hideCalander) {
                fnGetCalendar();
            }
        }
    }
    this.bindRoomDetail = function () {
        var $roomtypebox = $(".room-type-box.active");
        var adultcount = $roomtypebox.find("[data-adult-count]").data("adult-count");
        var childcount = $roomtypebox.find("[data-child-count]").data("child-count");
        var roomcount = $roomtypebox.find(".room-number").val();
        $.ajax({
            method: 'get',
            datatype: 'html',
            url: "/package/roomdetail?roomcount=" + roomcount + "&adultcount=" + adultcount + "&childcount=" + childcount,
            success: function (html, textStatus, jqXHR) {
                $('#divRoomDetail').html(html);
            }
        });
    }
})();