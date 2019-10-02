$(function (e) {
    BindHotelCalendar();
    var checkForMobileView = ($('input[name="IsMobileView"]').val() == 'true');
    var $BookingContainer = $('.booking-date-container');
    var flightLoader = '<img src="/images/ajax-loader.gif" class="flight-price-loader">';
    var flightLoaderGrey = '<img src="/images/stampede_loader_grey.gif" class="flight-price-loader grey">';
    var $RoomSelectContainer = $('.Room-handling');
    var $VisaSelectContainer = $('.flight-visa-handling');
    var $FlightContainer = $('.flight-handling-container');
    var loaderTemplate = '<div class="loader"><img src="/images/ajax-loader.gif" /></div>';
    var rightArrowTemplate = '<span><img src="/images/chevron_right_white.svg" alt="right_arrow" style="float:right;" /></span>';
    var $stepDate = $('.step_booking_date a');
    var $stepRoom = $('.step_hotel_room a');
    var $stepVisa = $('.step_visa a');
    var $stepFlight = $('.step_flight a');
    var navListItems = $('div.setup-panel div a'),
        allWells = $('.setup-content');
        //allNextBtn = $('.nextBtn');

        allWells.hide();

    navListItems.click(function (e) {
        
        e.preventDefault();
        var $target = $($(this).attr('href')),
            $item = $(this);

            if (!$item.is('[disabled=disabled]')) {
            //navListItems.removeClass('btn-success').addClass('btn-default');
            $item.addClass('btn-success');
            allWells.hide();
            $target.show();
            $target.find('input:eq(0)').focus();
        }
    });

    //allNextBtn.click(function () {
    //    var curStep = $(this).closest(".setup-content"),
    //        curStepBtn = curStep.attr("id"),
    //        nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
    //        curInputs = curStep.find("input[type='text'],input[type='url']"),
    //        isValid = true;

    //    $(".form-group").removeClass("has-error");
    //    for (var i = 0; i < curInputs.length; i++) {
    //        if (!curInputs[i].validity.valid) {
    //            isValid = false;
    //            $(curInputs[i]).closest(".form-group").addClass("has-error");
    //        }
    //    }

    //    if (isValid) nextStepWizard.removeAttr('disabled').trigger('click');
    //});

    $('div.setup-panel div a.btn-success').trigger('click');
    //Proceed from Calender Selection
    $(document).on('click', '.calendar-proceed-btn.active button', function (e) {
        
        var startDateElement = $('.calendar').find(".selected-start").first();
        var endDateElement = $('.calendar').find('.selected-end').first();

        var dateParts = $(startDateElement).data('date').split("/");
        // month is 0-based, that's why we need dataParts[1] - 1
        var date1 = new Date(+dateParts[2], dateParts[1] - 1, + dateParts[0]); 
        dateParts = $(endDateElement).data('date').split("/");

        var date2 = new Date(+dateParts[2], dateParts[1] - 1, + dateParts[0]); 
        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
        var htmlTemplate = '<div>Check In on <span>' + $(startDateElement).data('day') + ' ' + $(startDateElement).data("monthName").substr(0, 3) + ', ' + $(startDateElement).data("year") + '</span></div><div>Check Out on <span>' + $(endDateElement).data('day') + ' ' + $(endDateElement).data("monthName").substr(0, 3) + ', ' + $(endDateElement).data("year") + '</span> (' + diffDays + ' Nights)</div >';
            $BookingContainer.addClass('completed');
            $('.booking-date-container .rendered').html(htmlTemplate);
        $('.Room-handling').removeClass('collapsed');
        $('.Room-handling .selection').html(loaderTemplate);
        $('html, body').animate({
            scrollTop: $RoomSelectContainer.offset().top - 130
        }, 1000);
        if ($FlightContainer.length > 0)
        {
            $('.flight-container .journey-date .onward_date').html($(startDateElement).data('day') + ' ' + $(startDateElement).data("monthName").substr(0, 3) + ', ' + $(startDateElement).data("year"));
            $('.flight-container .journey-date .return_date').html($(endDateElement).data('day') + ' ' + $(endDateElement).data("monthName").substr(0, 3) + ', ' + $(endDateElement).data("year"));
        }
        $.get('/Deal/GetHotelRoomConfiguration', { 'dealId': $('#dealId').val(), 'nightId': $("#nightId").val(), 'inclusionId': $('#inclusionId').val(), 'startDate': $('.booking-date-container .check-in-date').val(), 'endDate': $('.booking-date-container .check-out-date').val() }, function (result) {
            $('.Room-handling .selection').html(result);
            ////Gallery Thumbs Start
            $('.Room-handling .selection .gallery_carousel_thumbnail .item').each(function () {
                var next = $(this).next();
                if (!next.length) {
                    next = $(this).siblings(':first');
                }
                next.children(':first-child').clone().appendTo($(this));
            });
            $('.Room-handling .selection .gallery_carousel_thumbnail .item').each(function () {
                var prev = $(this).prev();
                if (!prev.length) prev = $(this).siblings(':last');
                prev.children(':nth-last-child(2)').clone().prependTo($(this));
            });
            ////Gallery Thumbs End
        });
        if (checkForMobileView)
        {
            if ($stepVisa.length > 0) {
                $('.Room-handling .proceed-button button').html("PROCEED TO VISA SELECTION" + rightArrowTemplate);
            }
            else if ($stepFlight.length > 0) {
                $('.Room-handling .proceed-button button').html("PROCEED TO FLIGHT SELECTION" + rightArrowTemplate);
            }
            else {
                $('.Room-handling .proceed-button').html('<button type="submit" class="btn checkout-btn">PROCEED TO CHECKOUT</button>');
            }
            $stepDate.html("&#10004;").addClass('btn-success');
            $stepRoom.removeAttr("disabled");
            $stepRoom.click();

        }
    });
    $(document).on('click', '.rate-plans .btn-add-room', function (e) {
        var data = {
            'max': $(this).data('max'),
            'adults': 1,
            'child': 0,
            'infants': 0,
            'freeChild': $(this).data('freechild'),
            'freeInfant': $(this).data('freeinfant'),
            'roomTypeId': $(this).data('room-type-id'),
            'ratePlanName': $(this).data('rate-plan-name'),
            'roomName': $(this).data('room-name'),
            'priceText': $(this).data('price-text'),
            'roomConfigId': $(this).data('rc-id'),
            'ratePlanId': $(this).data('rateplan-id'),
            'aPriceDbo': $(this).data('aprice-dbo'),
            'aPriceSbo': $(this).data('aprice-sbo'),
            'exAPrice': $(this).data('exaprice'),
            'exCPrice': $(this).data('excprice'),
            'exIPrice': $(this).data('exiprice'),
            'markup': $(this).data('markup'),
            'supplement': $(this).data('supplement'),
            'inventorySerialized': JSON.stringify($(this).data('inventory')),
            'roomConfigSerialized': JSON.stringify($(this).data('rc-serialized')),
            'phSupplement': $(this).data('phsupplement')
        };
        var target = $(this).data('target');
        $(target).append(loaderTemplate);
        $.post('/Deal/GetRoomPassengerBreakdown', data, function (result) {
            $(target).find('.loader').remove();
            $(target).append(result);
            if (!$('.Room-handling .proceed-button').hasClass('active'))
            {
                $('.Room-handling .proceed-button').addClass('active');
            }
        });
    });
    $(document).on('click', '.room-breakdown .decrease', function (e) {
        
        var inputMin = $(this).closest('.room-quantity').find('input.input-passenger').data('min');
        if ($(this).closest('.room-quantity').find('input.input-passenger').val() <= inputMin) {
            return;
        }
        else
        {
            $(this).closest('.room-quantity').find('input.input-passenger').val(parseInt($(this).closest('.room-quantity').find('input.input-passenger').val()) - 1);
            if (parseInt($(this).closest('.room-quantity').find('input.input-passenger').val()) === inputMin)
            {
                $(this).addClass('disabled');
            }

            $(this).closest('.room-breakdown').find('.adult-configuration').find('.increase').removeClass('disabled');
            $(this).closest('.room-breakdown').find('.kid-configuration').find('.increase').removeClass('disabled');
            $(this).closest('.room-breakdown').find('.infant-configuration').find('.increase').removeClass('disabled');

        } 

    });
    $(document).on('click', '.room-breakdown .increase', function (e) {
        
        var maxPassenger = $(this).closest('.room-breakdown').data('max');
        var AdultValue = $(this).closest('.room-breakdown').find('.adult-configuration').find('.input-passenger').val();
        var ChildValue = $(this).closest('.room-breakdown').find('.kid-configuration').find('.input-passenger').val();
        var InfantValue = $(this).closest('.room-breakdown').find('.infant-configuration').find('.input-passenger').val();
        if ((parseInt(AdultValue) + parseInt(ChildValue) + parseInt(InfantValue)) >= parseInt(maxPassenger)) {
            return;
        }
        else {
            $(this).closest('.room-quantity').find('input.input-passenger').val(parseInt($(this).closest('.room-quantity').find('input.input-passenger').val()) + 1);
            if (parseInt($(this).closest('.room-quantity').find('input.input-passenger').val()) > parseInt($(this).closest('.room-quantity').find('input.input-passenger').data('min'))) {
                $(this).parent().find('.decrease').removeClass('disabled');
            }

            AdultValue = $(this).closest('.room-breakdown').find('.adult-configuration').find('.input-passenger').val();
            ChildValue = $(this).closest('.room-breakdown').find('.kid-configuration').find('.input-passenger').val();
            InfantValue = $(this).closest('.room-breakdown').find('.infant-configuration').find('.input-passenger').val();
            if ((parseInt(AdultValue) + parseInt(ChildValue) + parseInt(InfantValue)) === parseInt(maxPassenger)) {
                $(this).closest('.room-breakdown').find('.increase').addClass('disabled');
            }
        }
    });
    $(document).on('click', '.room-breakdown .remove-icon', function (e) {
        $(this).closest('.room-breakdown').remove();
        if ($('.Room-handling .room-breakdown').length > 0)
        {
            return;
        }
        else
        {
            $('.Room-handling .proceed-button').removeClass('active');
        }
    });
    //Proceed from Room Selection
    $(document).on('click', '.room-selection-proceed.active button', function (e) {
        var html = '';
        var totalPassenger = CalculateTotalPassenger();
        $('.total-passenger').val(totalPassenger);
        $('.select-room .room-expand').each(function (e) {
            if ($(this).find('.room-breakdown').length > 0) {
                html = html + '<div class="room">' + '<span class="room-name">' + $(this).data('room-name') + '</span>';
                $(this).find('.room-breakdown').each(function (s) {
                    html = html + '<span class="room-detail"><span class="room-count">Room ' + (s + 1) + ':</span>  ' + $(this).data('plan-name') + ' / ' + $(this).find('.adult-configuration').find('input.input-passenger').val() + ' Adults / ' + $(this).find('.kid-configuration').find('input.input-passenger').val() + ' Kid / ' + $(this).find('.infant-configuration').find('input.input-passenger').val() + ' Infant</span>';
                });
                html = html + '</div>';
            }
        });
        $('.Room-handling .rendered').html(html);
        $RoomSelectContainer.addClass('completed');
        if ($VisaSelectContainer.length > 0) {
            $('html, body').animate({
                scrollTop: $VisaSelectContainer.offset().top - 130
            }, 1000);
            $('.flight-visa-handling').removeClass('collapsed');
            $('.flight-visa-handling .visa-quantity-input input[type=text]').val(totalPassenger);
            $('.flight-visa-handling .increase').data('max', totalPassenger);
        }
        else if ($FlightContainer.length > 0) {
            ProceedToFlightSelection();
            //$('html, body').animate({
            //    scrollTop: $FlightContainer.offset().top - 130
            //}, 1000);
            //$('input[name=Isflight][type=radio]').prop("checked", false);
            //$('.flight-container .flights-details .passengers .quantity .number').html(totalPassenger);
            
            //var passengerBreakdown = GetPassengerBreakDown();
            //var passengerBreakdownString = passengerBreakdown.Adults + " - Adult<br>";
            //if (passengerBreakdown.Childs > 0) {
            //    passengerBreakdownString = passengerBreakdownString + passengerBreakdown.Childs + " - Child <br>";
            //}
            //if (passengerBreakdown.Infants > 0) {
            //    passengerBreakdownString = passengerBreakdownString + passengerBreakdown.Infants + " - Infant";
            //}
            //if ($('.flight-container .flights-details .passengers .quantity .number-hint').attr('data-original-title') != undefined) {
            //    $('.flight-container .flights-details .passengers .quantity .number-hint').tooltip('destroy').tooltip({ title: passengerBreakdownString, html: true, placement: "bottom" });
            //    $('.flight-container .flights-details .passengers .quantity .number-hint').tooltip('destroy').tooltip({ title: passengerBreakdownString, html: true, placement: "bottom" });
            //}
            //else
            //{
            //    $('.flight-container .flights-details .passengers .quantity .number-hint').tooltip({ title: passengerBreakdownString, html: true, placement: "bottom" });
            //}

        }
        else
        {
            $('.booking-summary').css({ 'display': 'block' });
            $('html, body').animate({
                scrollTop: $('.booking-summary').offset().top - 130
            }, 1000);
            
        }
        CalculatePriceSummary();
        if (checkForMobileView)
        {
            $stepRoom.html("&#10004;").addClass('btn-success');
            if ($stepVisa.length > 0) {
                if ($stepFlight.length == 0) {
                    $('.flight-visa-handling .proceed-visa').html('<button type="submit" class="btn checkout-btn">PROCEED TO CHECKOUT</button>');
                }
                else {
                    $('.flight-visa-handling .proceed-visa button').html("PROCEED TO FLIGHT SELECTION" + rightArrowTemplate);
                }
                $stepVisa.removeAttr("disabled");
                $stepVisa.click();
            }
            else if ($stepFlight.length > 0) {
                $stepFlight.removeAttr("disabled");
                $stepFlight.click();
            }
        }
    });
    $(document).on('change', 'input[name=IsVisa][type=radio]', function (e) {
        var value = $(this).val();
        
        if (value == 'true')
        {
            $('.book-without-visa .container-header').addClass('disable-visa');
            $('.flight-visa-handling .book-with-visa').addClass('hooked');
            $('.visa-options .visa-selector span.checkmark').trigger('click');
        }
        else
        {
            $('.book-without-visa .container-header').removeClass('disable-visa');
            $('.flight-visa-handling .book-with-visa').removeClass('hooked');
            $('.visa-options .visa-selector span.checkmark').trigger('click');
        }
    });
    $(document).on('click', '.nav-pills .visa-tab-list', function (e) {
        if ($(this).hasClass('active')) {
            return;
        }
        var target = $(this).data('target');
        var value = $(this).data('value');
        $('.visa-tab-container input[name="isVisa"]').val(value);
        $('.visa-nav-tab .visa-tab-list').removeClass('active');
        $(this).addClass('active');
        $(".visa-tab-content .tab-pane").removeClass('active').removeClass('in');
        $(".visa-tab-content .tab-pane" + target).addClass('active').addClass('in');

    });
    $(".visa-mobile-policy-button").click(function () {
        $(".visa-policy-modal").show();
        $(".visa-policy-modal.fade").addClass('in');
    });
    $(".visa-policy-modal-close").click(function () {
        $(".visa-policy-modal").hide();
        $(".visa-policy-modal.fade").removeClass('in');
    });
    $(document).on('shown.bs.tab', '.visa-tab-list a[data-toggle="tab"]', function (e) {
        
        $('.visa-options .visa-selector span.checkmark').trigger('click');
    });
    $(document).on('click', '.visa-options .decrease', function (e) {
        var min = parseInt($(this).data('min'));

        if ($(this).parent('li').find('.visa-quantity-input').find('input[type=text]').val() <= min) {
            return;
        }
        else {
            $(this).parent('li').find('.visa-quantity-input').find('input[type=text]').val(parseInt($(this).parent('li').find('.visa-quantity-input').find('input[type=text]').val()) - 1);
            $(this).siblings('.increase').removeClass('disabled');
            if (parseInt($(this).parent('li').find('.visa-quantity-input').find('input[type=text]').val()) === min) {
                
                $(this).addClass('disabled');
            }
        }
    });
    $(document).on('click', '.visa-options .increase', function (e) {

        var max = parseInt($(this).data('max'));

        if ($(this).parent('li').find('.visa-quantity-input').find('input[type=text]').val() >= max) {
            return;
        }
        else {
            $(this).parent('li').find('.visa-quantity-input').find('input[type=text]').val(parseInt($(this).parent('li').find('.visa-quantity-input').find('input[type=text]').val()) + 1);
            $(this).siblings('.decrease').removeClass('disabled');
            if (parseInt($(this).parent('li').find('.visa-quantity-input').find('input[type=text]').val()) === max) {
               
                $(this).addClass('disabled');
            }
        }
    });
    //Proceed To flight selection
    $(document).on('click', '.proceed-visa.active button', function (e) {
        var value;
        if (!checkForMobileView) {
            value = $(".visa-handling input[type='radio']:checked").val();
        }
        else {
            value = $('.visa-tab-container input[name="isVisa"]').val();
        }
        
        var html = '<div class="visa-result">';
        if (value == 'true')
        {
            html = html + '<div class="visa-selection-result">Book <span>with visa</span></div>';
            $('.visa-options li.visa-input').each(function (e) {
                html = html + '<div class="visa-breakdown-result">' + $(this).find('.visa-name').data('country') + ": <span>" + $(this).find('.visa-quantity-input').find('input[type=text]').val() + ' Passenger</span></div>';
            });
            html = html + '</div>';
        }
        else
        {
            html = html + '<div class="visa-selection-result">Book <span>without visa</span></div></div>';
        }
        $('.flight-visa-handling .rendered').html(html);
        $VisaSelectContainer.addClass('completed');
        if ($FlightContainer.length > 0)
        {
            ProceedToFlightSelection();
        }
        else
        {
            $('.booking-summary').css({ 'display': 'block' });
            $('html, body').animate({
                scrollTop: $('.booking-summary').offset().top - 130
            }, 1000);
        }
        $('.flight-handling-container').removeClass("collapsed");
        CalculatePriceSummary();
        if (checkForMobileView) {
            $stepVisa.html("&#10004;").addClass('btn-success');
            if ($stepFlight.length > 0)
            {
                $stepFlight.removeAttr("disabled");
                $stepFlight.click();
            }
        }
    });
    function ProceedToFlightSelection() {
        $('.flight-handling-container .book-with-flight').removeClass('hooked');
        $('html, body').animate({
            scrollTop: $FlightContainer.offset().top - 130
        }, 1000);
        $('input[name=Isflight][type=radio]').prop("checked", false);
        $('.flight-handling-container').removeClass("collapsed");
        $('.flight-handling-container').removeClass('completed');
        $('.flight-handling-container .sub-selection').hide();

        $('.flight-container .flights-details .passengers .quantity .number').html(CalculateTotalPassenger());
        var passengerBreakdown = GetPassengerBreakDown();
        var passengerBreakdownString = passengerBreakdown.Adults + " - Adult<br>";
        if (passengerBreakdown.Childs > 0) {
            passengerBreakdownString = passengerBreakdownString + passengerBreakdown.Childs + " - Child <br>";
        }
        if (passengerBreakdown.Infants > 0) {
            passengerBreakdownString = passengerBreakdownString + passengerBreakdown.Infants + " - Infant";
        }
        if ($('.flight-container .flights-details .passengers .quantity .number-hint').attr('data-original-title') != undefined) {
            $('.flight-container .flights-details .passengers .quantity .number-hint').attr('data-original-title', passengerBreakdownString);
        }
        else {
            $('.flight-container .flights-details .passengers .quantity .number-hint').tooltip({ title: passengerBreakdownString, html: true, placement: "bottom" });
        }
        $('#ddlAirportDeparture').val('0').change();
        $('.flight-content .select-dropdown .ltd-select .select-selected').html($("#ddlAirportDeparture option[value='0']").text());
    }

    $(document).on('change', 'input[name=Isflight][type=radio]', function (e) {
        var value = $(this).val();
        if (value == 'true') {
            $('.flight-handling-container .book-with-flight').addClass('hooked');
            $('.flight-handling-container .sub-selection').show();
            //$('.flight-handling-container .flight-proceed-button').removeClass('active');
        }
        else {
            $('.flight-handling-container .book-with-flight').removeClass('hooked');
            $('.flight-handling-container .sub-selection').hide();
            //$('.flight-handling-container .flight-proceed-button').addClass('active');
           
            ProceedFromFlight();
        }
    });
    $(document).on('click', '.flight-nav-tab .flight-tab-list', function (e) {
        if ($(this).hasClass('active')) {
            return;
        }
        var target = $(this).data('target');
        var value = $(this).data('value');
        $('.flight-tab-container input[name="Isflight"]').val(value);
        $('.flight-nav-tab .flight-tab-list').removeClass('active');
        $(this).addClass('active');
        $(".flight-tab-content .tab-pane").removeClass('active').removeClass('in');
        $(".flight-tab-content .tab-pane" + target).addClass('active').addClass('in');
        if (value.toString() == 'false')
        {
            $('.flight-handling-container .proceed-button').addClass('active');
            $('.flight-handling-container .sub-selection').hide();
        }
        else {
            if ($('.flight-result-single .select-flight-btn.selected').length == 0)
            {
                $('.flight-handling-container .proceed-button').removeClass('active');
            }
            $('.flight-handling-container .sub-selection').show();
        }
    });
    $(document).off('change', '#ddlAirportDeparture').on('change', '#ddlAirportDeparture', function (e) {
        var _control = $(this);
        if (_control.val() !== '0') {
            $('.origin-port').html(_control.val());
            $('.origin-location').html(_control.find(':selected').data('location'));
            $('.flight-container').removeClass('hidden');
            $('#flightSearchResults').html(loaderTemplate);
            if (checkForMobileView) {
                $('.flight-handling-container .sub-selection').show();
            }
            
            GetFlightSearchData(_control.val());
        }
        else {
            $('.flight-container').addClass('hidden');
        }
    });
    var FilterData = true;

    $(document).off('change', '#speed-slider').on('change', '#speed-slider', function (e) {
        $("#minpricerange").html(e.value.newValue[0]);
        $("#maxpricerange").html(e.value.newValue[1]);

        if (FilterData) {
            FilterData = false;
            setTimeout(function () {
                GetFlightFilterData();
            }, 3000);
        }

        //setInterval(function () {
        //    GetFlightFilterData();
        //}, 3000);

        //$(this).delay(3000).queue(function () {  
        //    
        //    GetFlightFilterData();
        //});        


        //$("#speed-slider").on("change", function (event, ui) {
        //    
        //    $("#minpricerange").html(event.value.newValue[0]);
        //    $("#maxpricerange").html(event.value.newValue[1]);

        //    GetFlightFilterData();
        //});
    });

    $(document).off('change', '#ddlPreferedAirlines').on('change', '#ddlPreferedAirlines', function (e) {
        var _val = $(this).val();
        
        GetFlightFilterData();

        //$('.show-more-button-div').addClass('hidden');        
        //if (_val == null || _val == undefined) {
        //    $('.show-more-button-div').removeClass('hidden');
        //    $(".flight-result-single").each(function (index, element) {
        //        if (index >= 0 && index < 5) {
        //            $(element).removeClass('hidden');
        //        }
        //        else {
        //            $(element).addClass('hidden');
        //        }
        //    });
        //    return;
        //}
        //$(".flight-result-single").each(function (index, element) {
        //    var flightAirlines = $(element).data('flights').split(',');
        //    if (flightAirlines.some(r => _val.includes(r))) {
        //        $(element).removeClass('hidden');
        //    }
        //    else {
        //        $(element).addClass('hidden');
        //    }
        //    return;
        //});
    });
    $(document).off('change', '#chkRefundable').on('change', '#chkRefundable', function (e) {
        var value = $(this).prop("checked");
        
        GetFlightFilterData();

        //$('.show-more-button-div').addClass('hidden');
        //if (value) {
        //    $(".flight-result-single").each(function (index, element) {
        //        if ($(element).data('refundable') === 'True') {
        //            $(element).removeClass('hidden');
        //        }
        //        else {
        //            $(element).addClass('hidden');
        //        }
        //    });
        //}
        //else {
        //    $(".flight-result-single").each(function (index, element) {
        //        $('.show-more-button-div').removeClass('hidden');
        //        if (index >= 0 && index < 5) {
        //            $(element).removeClass('hidden');
        //        }
        //        else {
        //            $(element).addClass('hidden');
        //        }
        //    });
        //}
    });
    $(document).off('change', '#ddlFlightStopOver').on('change', '#ddlFlightStopOver', function (e) {
        var _val = $(this).val();
        
        GetFlightFilterData();

        //$('.show-more-button-div').addClass('hidden');
        //if (_val == null || _val == undefined) {
        //    $('.show-more-button-div').removeClass('hidden');
        //    $(".flight-result-single").each(function (index, element) {
        //        if (index >= 0 && index < 5) {
        //            $(element).removeClass('hidden');
        //        }
        //        else {
        //            $(element).addClass('hidden');
        //        }
        //    });
        //    return;
        //}
        //$(".flight-result-single").each(function (index, element) {
        //    var flightAirlines = $(element).data('stopover').split(',');
        //    if (flightAirlines.some(r => _val.includes(r))) {
        //        $(element).removeClass('hidden');
        //    }
        //    else {
        //        $(element).addClass('hidden');
        //    }
        //    return;
        //});
    });
    $(document).off('change', "#ddlreturnflightTime").on('change', "#ddlreturnflightTime", function (e) {
        
        GetFlightFilterData();
    });
    $(document).off('change', "#flightTime").on('change', "#flightTime", function (e) {
        
        GetFlightFilterData();

        //if (action == "0") {
        //    $('.show-more-button-div').removeClass('hidden');
        //    $(".flight-result-single").each(function (index, element) {
        //        if (index >= 0 && index < 5) {
        //            $(element).removeClass('hidden');
        //        }
        //        else {
        //            $(element).addClass('hidden');
        //        }
        //    });
        //    return;
        //}

        //var minTime;
        //var maxTime;
        //$('.show-more-button-div').addClass('hidden');
        //switch (action) {
        //    case "1": minTime = Date.parse("01/01/2019 03:00:00");
        //        maxTime = Date.parse("01/01/2019 09:00:00");
        //        break;
        //    case "2": minTime = Date.parse("01/01/2019 09:00:00");
        //        maxTime = Date.parse("01/01/2019 15:00:00");
        //        break;
        //    case "3": minTime = Date.parse("01/01/2019 15:00:00");
        //        maxTime = Date.parse("01/01/2019 21:00:00");
        //        break;
        //    case "4": minTime = Date.parse("01/01/2019 21:00:00");
        //        maxTime = Date.parse("01/01/2019 03:00:00");
        //        break;
        //    default: break;
        //}
        //$(".flight-result-single").each(function (index, element) {
        //    var thisFlightTime = Date.parse("01/01/2019 " + ("0" + $(element).data('hour')).slice(-2) + ":" + ("0" + $(element).data('minute')).slice(-2) + ":00");
        //    if (thisFlightTime >= minTime && thisFlightTime <= maxTime) {
        //        $(element).removeClass('hidden');
        //    }
        //    else {
        //        $(element).addClass('hidden');
        //    }
        //});
    });

    function GetFlightSearchData(origin, reload) {
        
        var passengers = GetPassengerBreakDown();
        $.get('/deal/GetFlightDetails',
            {
                'departure': origin,
                'inclusionId': parseInt($('#inclusionId').val()),
                'startDate': $('.booking-date-container .check-in-date').val(),
                'returnDate': $('.booking-date-container .check-out-date').val(),
                'adults': passengers.Adults,
                'childs': passengers.Childs,
                'infants': passengers.Infants,
                'reload': reload
            },
            function (result) {
                $('#flightSearchResults').html(result);
            }
        );
    }

    function GetFlightFilterData() {
        
        var origin = $('#ddlAirportDeparture').val();
        var passengers = GetPassengerBreakDown();
        var cabinclass = $('#cabin-class-select').val();
        var stopover = $('#ddlFlightStopOver').val();
        var airline = $('#ddlPreferedAirlines').val();
        var pricerange = $("#minpricerange").html() + "#" + $("#maxpricerange").html();
        var timings = $("#flightTime option:selected").text();
        var returntimings = $("#ddlreturnflightTime option:selected").text();
        var refundable = $('#chkRefundable').val();

        $('#flights-sub-result').html('');

        $.get('/deal/GetFlightFilter',
            {
                'departure': origin,
                'inclusionId': parseInt($('#inclusionId').val()),
                'startDate': $('.booking-date-container .check-in-date').val(),
                'returnDate': $('.booking-date-container .check-out-date').val(),
                'adults': passengers.Adults,
                'childs': passengers.Childs,
                'infants': passengers.Infants,
                'cabinclass': cabinclass,
                'stopover': stopover != null ? stopover.join() : null,
                'airline': airline != null ? airline.join() : null,
                'pricerange': pricerange,
                'timings': timings,
                'returntimings': returntimings,
                'refundable': refundable
            },
            function (result) {
                
                FilterData = true;
                $('#flights-sub-result').html(result);
                CallMe();
            }
        );
    }

    $(document).on('click', '.btn-reload-flights', function (e) {
        $('#flightSearchResults').html(loaderTemplate);
        GetFlightSearchData($('#ddlAirportDeparture').val(), 'reload');
    });
    $(document).on('click', '.flight-handling-container .select-flight-btn', function (e) {
        var target = $(this);
        if (target.hasClass('selected')) {
            return;
        }
        else
        {
            $('#flightSearchResults .select-flight-btn').each(function (index, element) {
                if ($(element).hasClass('selected'))
                {
                    $(element).removeClass('selected');
                    $(element).html('SELECT');
                    return false;
                }
            });
        }
        var traceId = target.data("traceid");
        var tokenId = target.data("tokenid");
        var flightIndex = target.data("flightindex");
        var flightPrice = target.data("price");
        var origin = target.data("origin");
        var isLcc = target.data('lcc');
        var destination = target.data("destination");
        var length = target.data("length");
        var cabinClass = 'Economy'; //$("#cabin-class-select").select2('data')[0].text;
        var passengers = GetPassengerBreakDown();
        var adult = passengers["Adults"];
        var child = passengers["Childs"];
        var infants = passengers["Infants"];
        $("#BookingFlightViewModel_TraceId").val(traceId);
        $("#BookingFlightViewModel_FlightIndex").val(flightIndex);
        $("#BookingFlightViewModel_TokenId").val(tokenId);
        $("#BookingFlightViewModel_IsLCCString").val(isLcc);
        $("#BookingFlightViewModel_Origin").val(origin);
        $("#BookingFlightViewModel_Destination").val(destination);
        $("#BookingFlightViewModel_Length").val(length);
        $(this).addClass('selected');
        $(this).html("SELECTED");
        ProceedFromFlight();
        if (checkForMobileView)
        {
            $('.flight-handling-container .flight-proceed-button').addClass('active');
        }
    });
    $(document).on('click', '.flight-expended-content .select-flight-btn', function (e) {
        
        $(".travel-plan").show();
        $(".product-banner-xs-head").show();
        $('.expended-flight-section').addClass('hidden');
        
        var clickClass = '#flights-sub-result .' + $('.flight-expended-content .select-flight-btn').data('click-source').split(' ').slice(0, 2).join('.') + " .select-flight-btn";
        $(clickClass).click();
        $('.expended-flight-section .flight-expended-content').html("");
        //$("html, body").animate({ scrollTop: $('.flightdetailclose').data('offset') }, "fast");
    });
    function ProceedFromFlight () {
        var value;
        if (checkForMobileView)
        {
            value = $('.flight-tab-container input[name="Isflight"]').val();
        }
        else
        {
            value = $('input[name=Isflight][type=radio]:checked').val();
        }
        if ( value == 'false') {
            $('.flight-rendered-results').html('<span class="description">Book Without Flight</span>');
        }
        else
        {
            var $target = $('.flight-handling-container .select-flight-btn.selected');
            var flightDesignTemplate = '<span class="flight-heading">ONWARD FLIGHT:</span><span class="description">'
                + $target.closest('.flight-result-single').find('.flight-detail-time').val() + '</span><span class="description">'
                + $target.closest('.flight-result-single').find('.flight-first-leg').val() + '</span>';

            if ($target.closest('.flight-result-single').find('.onwards').find('.stop-over-time').length > 0) {
                $target.closest('.flight-result-single').find('.onwards').find('.stop-over-time').each(function (index, element) {
                    flightDesignTemplate = flightDesignTemplate + '<span class="description">' + $(element).val() + '</span>';
                    flightDesignTemplate = flightDesignTemplate + '<span class="description">' + $(element).siblings('.stop-over-trip').val() + '</span>';
                });
            }
            flightDesignTemplate = flightDesignTemplate + '<div class="seperator"></div>';
            flightDesignTemplate = flightDesignTemplate + '<span class="flight-heading">RETURN FLIGHT:</span><span class="description">' + $target.closest('.flight-result-single').find('.flight-detail-time-return').val() + '</span>'
                + '<span class="description">' + $target.closest('.flight-result-single').find('.flight-first-leg-return').val() + '</span>';
            if ($target.closest('.flight-result-single').find('.return').find('.stop-over-time').length > 0)
            {
                    $target.closest('.flight-result-single').find('.return').find('.stop-over-time').each(function (index, element) {
                        flightDesignTemplate = flightDesignTemplate + '<span class="description">' + $(element).val() + '</span>';
                        flightDesignTemplate = flightDesignTemplate + '<span class="description">' + $(element).siblings('.stop-over-trip').val() + '</span>';
                    });
            }
            $('.flight-rendered-results').html(flightDesignTemplate);
        }
        CalculatePriceSummary();
        $('.flight-handling-container').addClass('completed');
        $('.booking-summary').css({ 'display': 'block' });
        if (!checkForMobileView)
        {
            $('html, body').animate({
                scrollTop: $('.booking-summary').offset().top - 130
            }, 1000);
        }
    }
    $(document).on('click', '.booking-date-container .edit', function (e) {
        //if ($RoomSelectContainer.hasClass('completed')) {
            
        //}
        $RoomSelectContainer.removeClass('completed').addClass('collapsed');
        if ($VisaSelectContainer.length > 0) {
            $VisaSelectContainer.removeClass('completed').addClass('collapsed');
        }
        if ($FlightContainer.length > 0) {
            $FlightContainer.removeClass('completed').addClass('collapsed');
        }
        $('.booking-summary').css({ 'display': 'none' });
        $('.booking-date-container').removeClass('completed');
        if (checkForMobileView) {
            $stepDate.html("1").removeClass('btn-success');
            $stepRoom.html("2").removeClass('btn-success');
            if ($stepVisa.length > 0) {
                $stepVisa.html("3").removeClass('btn-success');
                if ($stepFlight.length > 0) {
                    $stepFlight.html("4").removeClass('btn-success');
                }
            }
            else if ($stepFlight.length > 0) {
                $stepFlight.html("3").removeClass('btn-success');
            }

        }
    });
    $(document).on('click', '.visa-policy-body .visa-nav-tabs li', function (e) {
        
        if ($(this).hasClass('active')) {
            return;
        }
        var parentTab = $(this).data('parent-tab');
        $(parentTab + '.visa-policy-body .visa-nav-tabs li').removeClass('active');
        $(parentTab + ' .visa-policy-tab-content .tab-pane').removeClass('in active');
        var targetWindow = $(this).find('a').first().attr('href');
        $(targetWindow).addClass('in active');
        $(this).addClass('active');
        $('.modal').scrollTop(0);
        return false;
    });
    $(document).on('click', '.Room-handling .edit', function (e) {
        if ($VisaSelectContainer.length > 0) {
            $VisaSelectContainer.removeClass('completed').addClass('collapsed');
        }
        if ($FlightContainer.length > 0) {
            $FlightContainer.removeClass('completed').addClass('collapsed');
        }
        $('.booking-summary').css({ 'display': 'none' });
        $('.Room-handling').removeClass('completed');
        if (checkForMobileView) {
            $stepRoom.html("2");
            if ($stepVisa.length > 0) {
                $stepVisa.html("3").removeClass('btn-success');
                if ($stepFlight.length > 0) {
                    $stepFlight.html("4").removeClass('btn-success');
                }
            }
            else if ($stepFlight.length > 0) {
                $stepFlight.html("3").removeClass('btn-success');
            }

        }
    });
    $(document).on('click', '.flight-visa-handling .edit', function (e) {
        if ($FlightContainer.length > 0 ) {
            $FlightContainer.removeClass('completed').addClass('collapsed');
        }
        $('.booking-summary').css({ 'display': 'none' });
        $('.flight-visa-handling').removeClass('completed');
        if (checkForMobileView) {
            if ($stepVisa.length > 0) {
                $stepVisa.html("3");
                if ($stepFlight.length > 0) {
                    $stepFlight.html("4").removeClass('btn-success');
                }
            }
            else if ($stepFlight.length > 0) {
                $stepFlight.html("3").removeClass('btn-success');
            }

        }
    });
    $(document).on('click', '.flight-handling-container .edit', function (e) {
        //$('input[name=Isflight][type=radio]').prop("checked", false);
        ProceedToFlightSelection();
        $('.booking-summary').css({ 'display': 'none' });
        if (checkForMobileView) {
            if ($stepVisa.length > 0) {
                if ($stepFlight.length > 0) {
                    $stepFlight.html("4");
                }
            }
            else if ($stepFlight.length > 0) {
                $stepFlight.html("3");
            }

        }
        //$('.flight-handling-container').removeClass('completed');
    });
    $(document).on('click', '.booking-summary .price-content .price-summary', function (e)
    {
        $("#summaryModal .summary-modal-body").html(loaderTemplate);
        $('#summaryModal').modal('toggle');
        BuildSummaryView();
    });
    $(document).on('click', '.checkout-content .hold-btn', function (e) {
        
        $('input[name="BookingInformationViewModel.IsHold"]').val(true);
        $('#hotelbookingForm').submit();
    });
    $(document).on('click', '.proceed-button.active .checkout-btn,.Total-Summary .checkout-btn', function (e) {
        BuildSummaryView();
        $('#hotelbookingForm').submit();
    });
    $(document).on('submit', '#hotelbookingForm', function (e) {
        BuildSummaryView();
        if (checkForMobileView) {
            if ($('.flight-tab-container input[name="Isflight"]').val() == 'true') {
                $('#hotelbookingForm').find('input[name=FlightRequired]').val(true);
                $('#hotelbookingForm').find('input[name=FlightRender]').val($('.flight-rendered-results').html());
            }
            else {
                $('#hotelbookingForm').find('input[name=FlightRequired]').val(false);
            }
        }
        else {
            if ($('input[name=Isflight][type=radio]:checked').val() == 'true') {
                $('#hotelbookingForm').find('input[name=FlightRequired]').val(true);
                $('#hotelbookingForm').find('input[name=FlightRender]').val($('.flight-rendered-results').html());
            }
            else {
                $('#hotelbookingForm').find('input[name=FlightRequired]').val(false);
            }
        }
        
        $('#hotelbookingForm').find('input[name=SummaryRendered]').val($("#summaryModal .summary-modal-body .main-summary").html());
        $('#hotelbookingForm').find('input[name=BookingSummaryViewModelString]').val(JSON.stringify(CalculatePriceSummary()));
        ////if ($('input[name=_UserId_]').val() === "")
        ////{
        ////    SignInSignUp('signin', false);
        ////    return false;
        ////}
        return true;
    });
    function BuildSummaryView()
    {
        showWaitProcess();
        var model = CalculatePriceSummary();
        $.ajax({
            type: 'POST',
            url: '/Deal/GetBookingSummary',
            data: model,
            async: false,
            success: function (result) {
                hideWaitProcess();
                $("#summaryModal .summary-modal-body").html(result);
            }
        });
        //$.post('/Deal/GetBookingSummary', model, function (result) {
        //    $("#summaryModal .summary-modal-body").html(result);
        //});
    }
    function CalculatePriceSummary() {
        var model = {};
        var totalPrice = 0.0;
        var totalTax = 0.0;
        var totalServiceCost = 0.0;
        var roomPrice = 0.0;
        var visaSelected = false;
        var flightSelected = false;
        var visaBreakDown = [];
        var numberOfNights = 0;
        var flightPrice = 0.0;

        
        //var passengerBreakDown = GetPassengerBreakDown();
        //var totalPassengerBreakDown = CalculateTotalPassenger();
        //Calculate Number of Nights
        var startDateElement = $('.calendar').find(".selected-start").first();
        var endDateElement = $('.calendar').find('.selected-end').first();
        var dateParts = $(startDateElement).data('date').split("/");
        var date1 = new Date(+dateParts[2], dateParts[1] - 1, + dateParts[0]);
        dateParts = $(endDateElement).data('date').split("/");
        var date2 = new Date(+dateParts[2], dateParts[1] - 1, + dateParts[0]);
        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
        numberOfNights = Math.ceil(timeDiff / (1000 * 3600 * 24));
        //Calculate Room Price
        var HotelRoomBreakDown = [];
        $('.select-room .room-expand').each(function (e) {
            if ($(this).find('.room-breakdown').length > 0) {
                $(this).find('.room-breakdown').each(function (index, element)
                {
                    var adults = parseInt($(this).find('.adult-configuration').find('input.input-passenger').val());
                    var childs = parseInt($(this).find('.kid-configuration').find('input.input-passenger').val());
                    var infants = parseInt($(this).find('.infant-configuration').find('input.input-passenger').val());
                    var inventory = $(this).data('inventory');
                    var subRoomPrice = 0.0;
                    var subRoomService = 0.0;
                    var subRoomTax = 0.0;
                    var HotelRoomObject = {};
                    var freeChild = parseInt($(this).data('freechild'));
                    var freeInfant = parseInt($(this).data('freeinfant'));
                    var chargableChilds = (childs - freeChild < 0 ? 0 : childs - freeChild);
                    var chargableInfants = (infants - freeInfant < 0 ? 0 : infants - freeInfant);
                    if (adults === 1)
                    {

                        inventory.forEach(function (item)
                        {
                            subRoomPrice = subRoomPrice + (parseFloat(item.SingleSupplement) + parseFloat(item.Surgcharge)); // Single Adult Price
                            subRoomPrice = subRoomPrice + ((parseFloat(item.ExtraChild_WB) + parseFloat(item.Surgcharge)) * chargableChilds); // Extra Child Price
                            subRoomPrice = subRoomPrice + ((parseFloat(item.ExtraInfant) + parseFloat(item.Surgcharge)) * chargableInfants); // Extra Infant Price
                        });
                        subRoomPrice = subRoomPrice + parseFloat($(this).data('supplement'));
                        subRoomPrice = subRoomPrice + (parseFloat($(this).data('phsupplement')) * adults) + (parseFloat($(this).data('phsupplement')) * chargableChilds) + (parseFloat($(this).data('phsupplement')) * chargableInfants);
                        subRoomService = (subRoomPrice * parseFloat($(this).data('markup'))) / 100;
                        subRoomTax = (subRoomService * HotelTax) / 100;
                        
                        HotelRoomObject = {
                            'RatePlanId': parseInt($(this).find('.rate_plan_id').val()),
                            'RoomConfigId': parseInt($(this).find('.room_config_id').val()),
                            'Adult': adults,
                            'Child': childs,
                            'Infant': infants,
                            'FreeChild': freeChild,
                            'FreeInfant': freeInfant,
                            'ChargableChild': chargableChilds,
                            'ChargableInfant': chargableInfants,
                            'Nights': numberOfNights,
                            'RoomName': $(this).data('room-name'),
                            'RatePlanName': $(this).data('plan-name'),
                            'InventorySerialized': JSON.stringify(inventory),
                            'RoomConfigSerialized': JSON.stringify($(this).data('rc-serialized')),
                            'SupplementPh': parseFloat($(this).data('phsupplement')),
                            'CheckinDate': $('.booking-date-container .check-in-date').val(),
                            'CheckoutDate': $('.booking-date-container .check-out-date').val(),
                            'PriceAdult': parseFloat($(this).data('pricesbo')),
                            'PriceChild': parseFloat($(this).data('exchildprice')),
                            'PriceInfant': parseFloat($(this).data('exinfantprice')),
                            'MarkUp': parseFloat($(this).data('markup')),
                            'Supplement': parseFloat($(this).data('supplement')),
                            'ServiceFee': subRoomService,
                            'TotalPrice': subRoomPrice,
                            'Tax': subRoomTax
                        };
                        roomPrice = roomPrice + subRoomPrice;
                        
                        
                    }
                    else
                    {
                        
                        inventory.forEach(function (item) {
                            //subRoomPrice = subRoomPrice + ((parseFloat(item.Price) + (parseFloat(item.Surgcharge) * 2) + (parseFloat(item.ExtraAdult) * (adults - 2)) + (parseFloat(item.Surgcharge) * (adults - 2)) + (childs * (parseFloat(item.ExtraChild_WB) + parseFloat(item.Surgcharge))) + (infants * (parseFloat(item.ExtraInfant) + parseFloat(item.Surgcharge)))));
                            subRoomPrice = subRoomPrice + parseFloat(item.Price) + (parseFloat(item.Surgcharge) * 2);
                            if ((adults - 2) >= 1)
                            {
                                subRoomPrice = subRoomPrice + (parseFloat(item.ExtraAdult) + parseFloat(item.Surgcharge)) * (adults - 2); // Extra Adult price
                            }
                            subRoomPrice = subRoomPrice + ((parseFloat(item.ExtraChild_WB) + parseFloat(item.Surgcharge)) * chargableChilds); // Extra Child Price
                            subRoomPrice = subRoomPrice + ((parseFloat(item.ExtraInfant) + parseFloat(item.Surgcharge)) * chargableInfants); // Extra Infant Price
                        });
                        subRoomPrice = subRoomPrice + parseFloat($(this).data('supplement')); // One Time Extra Supplement
                        subRoomPrice = subRoomPrice + (($(this).data('phsupplement')) * adults) + (parseFloat($(this).data('phsupplement')) * chargableChilds) + (parseFloat($(this).data('phsupplement')) * chargableInfants); //Per Head Supplement
                        subRoomService = (subRoomPrice * parseFloat($(this).data('markup'))) / 100;
                        subRoomTax = (subRoomService * HotelTax) / 100;
                        
                        HotelRoomObject = {
                            'RatePlanId': parseInt($(this).find('.rate_plan_id').val()),
                            'RoomConfigId': parseInt($(this).find('.room_config_id').val()),
                            'Adult': adults,
                            'Child': childs,
                            'Infant': infants,
                            'FreeChild': freeChild,
                            'FreeInfant': freeInfant,
                            'ChargableChild': chargableChilds,
                            'ChargableInfant': chargableInfants,
                            'Nights': numberOfNights,
                            'RoomName': $(this).data('room-name'),
                            'RatePlanName': $(this).data('plan-name'),
                            'InventorySerialized': JSON.stringify(inventory),
                            'RoomConfigSerialized': JSON.stringify($(this).data('rc-serialized')),
                            'SupplementPh': parseFloat($(this).data('phsupplement')),
                            'CheckinDate': $('.booking-date-container .check-in-date').val(),
                            'CheckoutDate': $('.booking-date-container .check-out-date').val(),
                            'PriceAdult': parseFloat($(this).data('pricedbo')),
                            'PriceChild': parseFloat($(this).data('exchildprice')),
                            'PriceInfant': parseFloat($(this).data('exinfantprice')),
                            'MarkUp': parseFloat($(this).data('markup')),
                            'Supplement': parseFloat($(this).data('supplement')),
                            'ServiceFee': subRoomService,
                            'TotalPrice': subRoomPrice,
                            'Tax': subRoomTax
                        };
                    }
                    totalPrice = totalPrice + subRoomPrice;
                    totalServiceCost = totalServiceCost + HotelRoomObject.ServiceFee;
                    totalTax = totalTax + HotelRoomObject.Tax;
                    HotelRoomBreakDown.push(HotelRoomObject);
                });

            }
        });
        roomPrice = roomPrice * numberOfNights;
        //Calculate Visa Summary 
        if ($('.flight-visa-handling').length > 0) {
            if (checkForMobileView) {
                visaSelected = $('.visa-tab-container input[name="isVisa"]').val();
            }
            else
            {
                visaSelected = $('input[name=IsVisa][type=radio]:checked').val();
            }
            
            if (visaSelected == 'true') {
                $('.visa-options li').each(function (e) {
                    if ($(this).find('input[type=checkbox]:checked').length > 0)
                    {
                        
                        var visaPrice = (parseInt($(this).find('input[type=text]').val()) * parseFloat($(this).find('.visa-price').val()));
                        var visaService = (parseFloat($(this).find('.visa-markup').val()) * parseInt($(this).find('input[type=text]').val()));
                        var visaTax = visaService * HotelTax / 100;
                        visaBreakDown.push({
                            'CountryName': $(this).find('.visa-name').data('country'),
                            'VisaId': $(this).find('input.visa-id').val(),
                            'Price': $(this).find('.visa-price').val(),
                            'Count': $(this).find('input[type=text]').val(),
                            'ServiceFee': visaService,
                            'TotalPrice': visaPrice,//Original Price
                            'Tax': visaTax,
                            'TotalPriceITax': visaPrice + visaTax + visaService
                        });
                        totalPrice = totalPrice + visaPrice;
                        totalTax = totalTax + visaTax;
                        totalServiceCost = totalServiceCost + visaService;
                    }
                });
            }
        }
        var flightTax = 0.0;
        //Calculate Flight Price
        if ($('.flight-handling-container').length > 0)
        {
            if (checkForMobileView)
            {
                flightSelected = $('.flight-tab-container input[name="Isflight"]').val();
            }
            else {
                flightSelected = $('input[name=Isflight][type=radio]:checked').val();
            }
            
            if (flightSelected == 'true')
            {
                flightPrice = parseFloat($('.flight-handling-container .select-flight-btn.selected').data('price'));
                flightTax = FlightServiceFeeHotel * HotelTax / 100;
                totalPrice = totalPrice + flightPrice;
                totalServiceCost = totalServiceCost + FlightServiceFeeHotel;
                totalTax = totalTax + flightTax;
            }
        }

        $('.booking-summary .price-content .price span').html(Math.round(totalPrice + totalTax + totalServiceCost).toLocaleString('en-IN'));
        
        model = {
            'Type':1,
            'Nights': numberOfNights,
            'TotalPrice': totalPrice,
            'TotalTax': totalTax,
            'TotalServiceFee': totalServiceCost,
            'RoomPrice': roomPrice,
            'TaxPercentage': HotelTax,
            'IsVisa': visaSelected,
            'VisaInformation': visaBreakDown,
            'IsFlight': flightSelected,
            'FlightPrice': flightPrice,
            'FlightService': FlightServiceFeeHotel,
            'FlightTax': flightTax,
            'PassengerBreakDown': GetPassengerBreakDown(),
            'BookingHotelRooms': HotelRoomBreakDown
        };
        return model;
    }
    function BindHotelCalendar() {
        var calenderType = $('.calendar').data('calendar-type');
        var dealType = $('.calendar .deal-type').val();
        var minDays = $('.calendar .min-days').val();
        if (parseInt(minDays) == 0)// Free Calendar
        {
            $('.calendar-day').on('mouseenter', function (e) {
                if ($(".calendar .calendar-day").hasClass("selected-start") && $(".calendar .calendar-day").hasClass("selected-end")) { //// Dates Are Selected
                    return false;
                }
                if (!$(this).hasClass('notAvailable') && $('.calendar').find('.calendar-day.selected-start').length > 0 && !$(this).hasClass('selected-start')) {
                    $(".calendar .calendar-day").removeClass("hover-mid");
                    if ($('.calendar .calendar-day.selected-start').data('cal-date-index') < $(this).data('cal-date-index')) {
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
        }///Free Calendar
        if (parseInt(minDays) != 0) {
            if (dealType == 1) //// Hotel
            {
                $('.calendar-day').on('mouseenter', function (e) {
                    if ($(".calendar .calendar-day").hasClass("selected-start") && $(".calendar .calendar-day").hasClass("selected-end")) {
                        return false;
                    }
                    if (!$(this).hasClass('notAvailable') && $('.calendar').find('.calendar-day.selected-start').length > 0 && !$(this).hasClass('selected-start')) {
                        $(".calendar .calendar-day").removeClass("hover-mid");
                        if ($('.calendar .calendar-day.selected-start').data('cal-date-index') < $(this).data('cal-date-index')) {
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
                $('.calendar-day').on('mouseleave', function (e) {
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
                $('.calendar-day').on('click', function (e) {
                    
                    if (!$(this).hasClass('notAvailable') && $(this).hasClass('hover-start')) {
                        $(this).addClass('selected-start');
                        return false;
                    }
                    if ($(".calendar .calendar-day").hasClass("selected-start") && $(".calendar .calendar-day").hasClass("selected-end")) {
                        $(".calendar .calendar-day").removeClass("hover-mid");
                        $(".calendar .calendar-day").removeClass("selected-start");
                        $(".calendar .calendar-day").removeClass("selected-end");
                        if (calenderType === "product") {
                            $('.calendar-proceed-btn').removeClass('active');
                            $('.booking-date-container .check-in-date').val(null);
                            $('.booking-date-container .check-out-date').val(null);
                        }
                        $(this).trigger('mouseenter');
                        return false;
                    }
                    if ($('.calendar .calendar-day.selected-start').data('cal-date-index') > $(this).data('cal-date-index')) {
                        $(".calendar .calendar-day").removeClass("hover-mid");
                        $(".calendar .calendar-day").removeClass("selected-start");
                        $(".calendar .calendar-day").removeClass("selected-end");
                        return false;
                    }
                    if (!$(this).hasClass('notAvailable') && $(this).hasClass('hover-end')) {
                        var countNight = 0;
                        var startCheck = false;
                        $(".calendar .calendar-day").each(function (index, element) {
                            // element == this
                            if ($(element).hasClass('selected-start')) {
                                startCheck = true;
                                countNight++;
                            }
                            else if (startCheck) {
                                if ($(element).hasClass('hover-end')) {
                                    return false;
                                }
                                countNight++;
                            }
                        });
                        if (countNight < parseInt(minDays)) {
                            $('.calendar .calendar-day.hover-end').addClass('tooltip-enabled');
                            $(this).tooltip({ selector: '.calendar .calendar-day.hover-end', title: "Minimum " + minDays + " nights Required" });
                            $(this).tooltip("show");
                            setTimeout(function () {
                                $('.calendar .calendar-day.tooltip-enabled').tooltip('hide');
                                $('.calendar .calendar-day.tooltip-enabled').removeClass('tooltip-enabled')
                            }, 1000);
                            return false;
                        }

                        $(this).removeClass('hover-end');
                        $(this).addClass('selected-end');
                        var startDateElement;
                        var endDateElement;
                        //For Product Page Calender
                        if (calenderType === "product") {
                            startDateElement = $('.calendar').find(".selected-start").first();
                            endDateElement = $('.calendar').find('.selected-end').first();
                            $('.calendar-proceed-btn').addClass('active');
                            $('.booking-date-container .check-in-date').val($(startDateElement).data('date'));
                            $('.booking-date-container .check-out-date').val($(endDateElement).data('date'));
                            $('#lblPriceWithFlight').html(flightLoader);
                            $('#lblFlightIndicativePrice').html(flightLoader);
                            $('#lblRackPriceWithFlight').html('&nbsp;');
                            $.get('/deal/GetLowestFlightPrice',
                                {
                                    'inclusionId': parseInt($('#inclusionId').val()),
                                    'startDateBooking': $(startDateElement).data('date')
                                },
                                function (result) {
                                    var lblPriceWithFlight = $('#lblPriceWithFlight');
                                    var lblRackPriceWithFlight = $('#lblRackPriceWithFlight');
                                    var lblFlightIndicativePrice = $('#lblFlightIndicativePrice');
                                    var pricewithFlight = Math.round(parseFloat(lblPriceWithFlight.data('price')) + parseFloat(result));
                                    var rackPricewithFlight = Math.round(parseFloat(lblRackPriceWithFlight.data('price')) + parseFloat(result));

                                    lblPriceWithFlight.data('price', pricewithFlight);
                                    lblRackPriceWithFlight.data('price', rackPricewithFlight);
                                    lblFlightIndicativePrice.data('price', pricewithFlight);
                                    lblPriceWithFlight.html('₹' + pricewithFlight.toLocaleString('en-IN') + '<span class="person-span">/pers<sup>*</sup></span>');
                                    lblRackPriceWithFlight.html('<span class="instead-span">Instead of </span><span class="rack-price">₹' + rackPricewithFlight.toLocaleString('en-IN') + '</span> ');
                                    if (parseFloat(result) != 0)
                                    {
                                        lblFlightIndicativePrice.html('₹' + Math.round(parseFloat(result)).toLocaleString('en-IN'));
                                    }

                                }
                            );
                        }
                        return false;
                    }
                });
            }
        }//// Minimum Days Stay
        var $firstSeperator = $('.calendar .month-seperator').first();
        var $lastSeperator = $('.calendar .month-seperator').last();
        $('.nav-left').on('click', function (e) {
            e.stopPropagation();
            if ($firstSeperator.offset().left > $(".calender-container").offset().left) {
                return;
            }
            else {
                $('.calendar').animate({
                    scrollLeft: "-=" + 335 + "px"
                }, "fast");
            }

        });
        $('.nav-right').on('click', function (e) {
            
            e.stopPropagation();
            if (($lastSeperator.offset().left - 335) < ($(".calender-container").offset().left + 780)) {
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
                if ($('input[name="ScrollNormally"]').val() == "true") {
                    $('.calendar').animate({
                        scrollLeft: $firstSeperator.offset().left - $(".calender-container").offset().left - 50
                    }, "fast");
                }
                else
                {
                    var monthNumber = parseInt($('.calendar').find('.selected-start').data('month'));
                    monthNumber = monthNumber - 1;
                    $('.calendar').animate({
                        scrollLeft: $('.month-seperator[data-month="' + monthNumber.toString() + '"]').offset().left - $(".calender-container").offset().left - 50
                    }, "fast");
                }
                
            }
        });
    }
    function CalculateTotalPassenger() {
        var totalPassenger = 0;
        $('.select-room .room-expand').each(function (e) {
            if ($(this).find('.room-breakdown').length > 0) {
                $(this).find('.room-breakdown').each(function (s) {
                    totalPassenger = totalPassenger + parseInt($(this).find('.adult-configuration').find('input.input-passenger').val()) + parseInt($(this).find('.kid-configuration').find('input.input-passenger').val()) + parseInt($(this).find('.infant-configuration').find('input.input-passenger').val());
                });
            }
        });
        return totalPassenger;
    }
    function GetPassengerBreakDown() {
        var passengerBreakDown =
        {
            'Adults': 0,
            'Childs': 0,
            'Infants': 0
        };
        $('.select-room .room-expand').each(function (e) {
            if ($(this).find('.room-breakdown').length > 0) {
                $(this).find('.room-breakdown').each(function (s) {
                    passengerBreakDown.Adults = passengerBreakDown.Adults + parseInt($(this).find('.adult-configuration').find('input.input-passenger').val());
                    passengerBreakDown.Childs = passengerBreakDown.Childs + parseInt($(this).find('.kid-configuration').find('input.input-passenger').val());
                    passengerBreakDown.Infants = passengerBreakDown.Infants + parseInt($(this).find('.infant-configuration').find('input.input-passenger').val());
                });
            }
        });
        return passengerBreakDown;
    }
});

