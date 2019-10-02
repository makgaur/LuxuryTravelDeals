(function flightSearch() {
    this.oSettings = {
        accessToken: '',
        urls: {
            getDestinations: '/selectlist/getflightdestinations',
            searchflights: '/airservice/searchflights',
            flights: '/airservice/flights',
            getFlights: '/airservice/getflights',
            getFlightDetail: '/airservice/getflightdetail',
            getFlightSearchCriteria: '/airservice/getflightsearchcriteria',
            getJourneyTypeSearchCriteria: '/airservice/getjourneytypesearchcriteria',
            addNewSegment: '/airservice/addnewsegment',
            flightBooking: '/airservice/flightbooking',
            getPassengers: '/airservice/getpassengers',
            getBookingSummary: '/airservice/getbookingsummary',
            makepayment: '/airservice/makepayment',
            proccedtopayment: '/airservice/ProccedToPayment',
        },
        on: {
            journeyType: 'li[data-flight="JourneyType"]',
            personCounter: '[data-flight="passenger-counter"]',
            togglePersons: '[data-flight="pessengers-toggle"]',
            search: '[data-flight="search"]',
            locations: '[data-flight="location"]',
            flightDetail: '[data-flight="details"]',
            addNewSegment: '[data-flight="addsegment"]',
            flightBook: '[data-flight="book"]',
            flightBookPayment: '[data-flight="makepayment"]'
        },
        keys: {
            searchRequest: 'flight-search-request',
            flightBook: 'flight-book',
            flightBookSummary: 'flight-book-summary',
            flightDestinations: 'flight-destinations',
            accessToken: 'flight-token',
            totalPersons: '[data-flight="totalPassengers"]',
            dvPessons: '[data-flight="pessengers"]',
            dvSiglePassengers: '[data-flight="signle-passengers"]',
            dvSearchSegments: '[data-flight="search-segment"]',
            modal: "#flight-modal",
            searchcriteria: '[data-flight="search-criteria"]',
            searchList: '[data-flight="flight-list"]',
            searchExpire: ":hidden[name='ClearSearch']"
        },
        html: {
            loading: '<div class="overlay"><i class="fa fa-refresh fa-spin" ></i ></div>',
            missingSearchRequest: '<div class="text-center missing-content"> Search Criteria is Missing. </br> Please Search Again with Proper Data. </div>',
            searchSessionExpire: "<div class='text-center missing-content'>Sorry! Looks like this session has timed out and the information you see may be outdated.<br/> Please Search Flights Again!</div>",
            refresh: '<div class="box"><div class="overlay"><i class="fa fa-refresh fa-spin" ></i ></div></div>'
        }
    };

    this.flights = [];

    this.destinations = [];

    this.filterBy = {
        airlines: [],
    };

    this.flightSearchRequestData = {};

    this.fnGetSearchUrl = function () {
        var oData = this.fnGetSearchDetails(this.flightSearch);
    };

    this.fnGetDestinations = function (isDelete) {
        if (destinations instanceof Array && destinations.length > 0) {
            localStorage.setItem(oSettings.keys.flightDestinations, JSON.stringify(destinations));
        }
        if (isDelete === true) {
            localStorage.removeItem(oSettings.keys.flightDestinations);
        }
        if (localStorage.getItem(oSettings.keys.flightDestinations) !== null) {
            destinations = JSON.parse(localStorage.getItem(oSettings.keys.flightDestinations));
        }
    };

    this.fnUpdateModal = function (title, state, body) {
        if (title)
            $(this.oSettings.keys.modal).find('.modal-title').html(title);
        if (state == 'loading') {
            $(this.oSettings.keys.modal)
                .find('.modal-body').addClass("box")
                .html(oSettings.html.loading);
        }
        if (body) {
            $(this.oSettings.keys.modal).find('.modal-body').removeClass("box").html(body);
        }
    };

    this.fnHideModel = function () {
        $(this.oSettings.keys.modal).modal("hide");
    };

    this.fnShowModal = function () {
        $(this.oSettings.keys.modal).modal({ keyboard: false, backdrop: "static" }).show()
    };

    this.fnInitFilters = function () {
        if (flights.length > 0) {
            var segments = Enumerable.From(flights).Select(function (item) {
                return item.Segments;
            }).ToArray();
            for (var i in segments) {
                for (var j in segments[i][0]) {
                    this.filterBy.airlines.push(segments[i][0][j].Airline);
                }
            }
            this.filterBy.airlines = Enumerable
                .From(this.filterBy.airlines)
                .GroupBy(
                function (item) { return item.AirlineCode; },
                function (item) { return item; },
                function (code, grouping) {
                    return {
                        AirlineCode: code,
                        PriceList: fnGetPriceListOfAirline(code),
                        Records: grouping.source,
                    };
                })
                .ToArray();

            this.fnRenderFilters();
        }
    };

    this.fnGetPriceListOfAirline = function (airlinecode) {
        var prices = [];
        if (flights.length > 0) {
            prices = Enumerable.From(flights)
                .Where(function (item) {
                    return item.AirlineCode === airlinecode;
                }).Select(function (item) {
                    return item.Fare.BaseFare || 0;
                }).ToArray()
        }
        return prices;
    };

    this.fnUpdateModalFooterToRedirect = function (btnText, recirectUrl) {
        var footer = '';
        footer += '<div class="modal-footer">';
        footer += '    <button type="button" class="btn btn-primary redirectTo" data-url="' + (recirectUrl || "/") + '">' + (btnText || 'Button') + '</button>';
        footer += '</div>';
        $(this.oSettings.keys.modal).find('[data-dismiss="modal"]').remove();
        $(this.oSettings.keys.modal).find('.modal-body').after(footer);
        $(this.oSettings.keys.modal).find('.modal-footer').find(".redirectTo").on("click", function () {
            var url = $(this).data("url") || '';
            if (flightSearchRequestData && flightSearchRequestData.HotelBookingId) {
                url = url + "?bi=" + flightSearchRequestData.HotelBookingId
            }
            window.location = url;
        })
    }

    this.fnGetflightDetails = function (resultindex, hold) {
        if (resultindex) {
            var flight = this.flights.filter(function (flight) {
                return $.inArray(flight.ResultIndex, resultindex.split(',')) != -1;
            })
            if (flight.length > 0) {
                if (hold == undefined) {
                    this.fnUpdateModal('Flight Details', 'loading');
                    this.fnShowModal();
                    $.ajax({
                        url: oSettings.urls.getFlightDetail,
                        method: 'post',
                        dataType: 'html',
                        data: flight[0],
                        success: function (html) {
                            fnUpdateModal(undefined, undefined, html);
                        },
                        error: function (xhr, request, error) {
                        }
                    });
                }
                if (hold == "hold") {
                    var holdData = {
                        TraceId: $(":hidden[name='SearchTraceId']").val(),
                        TokenId: $(":hidden[name='SearchTokenId']").val(),
                        ResultIndex: resultindex,
                        AdultCount: flightSearchRequestData.AdultCount,
                        ChildCount: flightSearchRequestData.ChildCount,
                        InfantCount: flightSearchRequestData.InfantCount,
                        AirSearchResults: [],
                        Passengers: []
                    };
                    localStorage.setItem(oSettings.keys.flightBook, JSON.stringify(holdData));
                }
            }
        }
        if (hold == "get-holded") {
            var holded = localStorage.getItem(oSettings.keys.flightBook);
            return holded !== null ? JSON.parse(holded) : {};
        }
    }

    this.fnGetPassengers = function () {
        if ($(":hidden[name='flightBooking']").length) {
            var formData = fnGetflightDetails(undefined, 'get-holded');
            if (Object.keys(formData).length > 0) {
                formData.AirSearch = fnGetSearchRequest();
                $.ajax({
                    url: oSettings.urls.getPassengers
                        + "?hbid=" + fnGetSearchRequest({}).HotelBookingId,
                    method: 'post',
                    dataType: 'html',
                    data: formData,
                    success: function (html) {
                        var key = '[data-flight="dvFlightBooking"]';
                        $(key).html($(html).html());

                        $("#flightFareAmount").html($(key).find("#fareDetails").html());
                        $("#travellerDetails").html($(key).find("#passengerDetails").html());

                        $('.panel-collapse').last().collapse('show');

                        if ($(oSettings.keys.searchExpire).length == 0) {
                            var form = $(key).find('form').eq(0)
                            $(form).removeData("validator");
                            $(form).removeData("unobtrusiveValidation");
                            $.validator.unobtrusive.parse(form);
                            initPluggins();
                            localStorage.removeItem(oSettings.keys.flightBookSummary);
                            $(".dob").datepicker("destroy");
                            $(".dob").datepicker({ format: dateformat, todayHighlight: true, autoclose: true, endDate: new Date() });
                            if (($("[name='AutoBooking']").val() || '').toLowerCase() === "true") {
                                $("#frmFlightBook").submit();
                            }
                        }
                        else {
                            fnSearchSessionExpire();
                        }
                    },
                    error: function (xhr, request, error) {
                        if ($('[name="apierror"]').val().length > 0) {
                            error = error + $('[name="apierror"]').val();
                        }

                        fnUpdateModal('Error', undefined, "<div class='text-center missing-content'> " + error + "</div>");
                        fnUpdateModalFooterToRedirect('Retry', oSettings.urls.flightBooking);
                        fnShowModal();
                    }
                });
            }
            else {
                $(function () {
                    fnUpdateModal('Warning', undefined, "<div class='text-center missing-content'> Could not find Flight Detail. <br/> Please Try Again!</div>");
                    fnUpdateModalFooterToRedirect('Go To Search Flights', oSettings.urls.searchflights);
                    fnShowModal();
                });
            }
        }
    };

    this.fnGetBookingSummary = function (keys) {
        if ($('#frmFlightBook').length > 0) {
            if (oFlightBooking && Object.keys(oFlightBooking).length > 0) {
                localStorage.setItem(oSettings.keys.flightBookSummary, JSON.stringify(oFlightBooking));
                oFlightBooking = {};
            }
            var flightBooking = {};
            try {
                flightBooking = JSON.parse(localStorage.getItem(oSettings.keys.flightBookSummary));
            } catch (e) {
            }
            var hPayment = $('[name="hidepayment"]').length > 0;
            if (flightBooking !== null) {
                $.ajax({
                    url: oSettings.urls.getBookingSummary + "?hide=" + hPayment,
                    method: 'post',
                    dataType: 'html',
                    data: flightBooking,
                    success: function (html) {
                         
                        $(".dvNewBookingSummary").html(html)
                    },
                    error: function (xhr, request, error) {
                    }
                });
            }
            else {
                $(function () {
                    fnUpdateModal('Warning', undefined, "<div class='text-center missing-content'> Could not find Flight Detail. <br/> Please Try Again!</div>");
                    fnUpdateModalFooterToRedirect('Go To Search Flights', oSettings.urls.searchflights);
                    fnShowModal();
                });
            }
        }

        if (keys == oSettings.keys.flightBookSummary) {
            return JSON.parse(localStorage.getItem(oSettings.keys.flightBookSummary));
        }
    }

    this.fnModifySearch = function (oData) {
        if (oData instanceof Object && Object.keys(oData).length > 0) {
            localStorage.setItem(oSettings.keys.searchRequest, JSON.stringify(oData));
        }
    }

    this.fnUpdateSegments = function () {
        var oData = fnGetSearchRequest();
        if (oData.Segments instanceof Array) {
            for (var i = 0; i < oData.Segments.length; i++) {
                $('[data-segments="' + i + '"]').filter('[data-origincityname]').html(oData.Segments[i].OriginCityName);
                $('[data-segments="' + i + '"]').filter('[data-origin]').html(oData.Segments[i].Origin);
                $('[data-segments="' + i + '"]').filter('[data-destinationcityname]').html(oData.Segments[i].DestinationCityName);
                $('[data-segments="' + i + '"]').filter('[data-destination]').html(oData.Segments[i].Destination);
                $("select.change-cabin").val(oData.Segments[i].FlightCabinClass);
                $cabinClass = $("select.change-cabin").find("option[value='" + oData.Segments[i].FlightCabinClass + "']");
                if ($cabinClass.length > 0) {
                    $('.spncabin-class').html($cabinClass.text());
                }
            }
            var isRoundTrip = oData.Segments[0].Destination === oData.Segments[1].Origin &&
                            oData.Segments[0].Origin === oData.Segments[1].Destination;
            $(".box_col.isRoundTrip").toggleClass("hide", isRoundTrip);
        }
    }

    this.fnGetSearchRequest = function (oData) {
        if (oData instanceof Object) {
            if (oSearchFlightRequest !== null &&
                oSearchFlightRequest instanceof Object &&
                Object.keys(oSearchFlightRequest).length > 0) {
                localStorage.setItem(oSettings.keys.searchRequest, JSON.stringify(oSearchFlightRequest));
                oSearchFlightRequest = {};
            }
            if (localStorage.getItem(oSettings.keys.searchRequest) !== null) {
                this.flightSearchRequestData = JSON.parse(localStorage.getItem(oSettings.keys.searchRequest));
                var token = fnGetAccessToken();
                if (token != null && token != '' && this.flightSearchRequestData.TokenId === null) {
                    flightSearchRequestData.TokenId = token;
                    localStorage.setItem(oSettings.keys.searchRequest, JSON.stringify(flightSearchRequestData));
                }
            }
            else {
            }
        }
        return this.flightSearchRequestData;
    };

    this.fnSearchFlights = function () {
        if (Object.keys(flightSearchRequestData).length > 0) {
            $(oSettings.keys.searchList).html(oSettings.html.refresh);
            $.ajax({
                url: oSettings.urls.getFlights,
                method: 'post',
                dataType: 'html',
                data: flightSearchRequestData,
                success: function (html) {
                    $(oSettings.keys.searchList).html($(html).html());
                    $(':radio').iCheck({ checkboxClass: 'icheckbox_minimal-orange', radioClass: 'iradio_minimal-orange' })
                    $(':radio[name="OB"]:first').iCheck("check").iCheck('update')
                    $(':radio[name="IB"]:first').iCheck("check").iCheck('update')

                    var apiResult = $(oSettings.keys.searchList).find(':hidden[name="flight-list"]').val();
                    var oResult = [];
                    try {
                        oResult = JSON.parse(apiResult);
                    } catch (e) {
                    }
                    flights = oResult;
                    fnInitComponent();
                    fnGetAccessToken();
                    selectFlight($(':radio[name="OB"]:first'));
                    selectFlight($(':radio[name="IB"]:first'));
                    if ($('#selectedFlights').length > 0) {
                        $('#selectedFlights').affix({
                            offset: {
                                top: $('.flight-heading').eq(0).offset().top, bottom: function () {
                                    return (this.bottom = $('#footer').outerHeight(true))
                                }
                            }
                        })
                    }
                },
                error: function (xhr, request, error) {
                    if ($('[name="apierror"]').val().length > 0) {
                        error = error + $('[name="apierror"]').val();
                    }
                    fnUpdateModal('Error', undefined, "<div class='text-center missing-content'> " + error + "</div>");
                    fnUpdateModalFooterToRedirect('Retry', oSettings.urls.flights);
                    fnShowModal();
                }
            });
        } else {
            $(function () {
                fnUpdateModal('Warning', undefined, oSettings.html.missingSearchRequest);
                fnUpdateModalFooterToRedirect('Go To Search Flights', oSettings.urls.searchflights);
                fnShowModal();
            });
        }
    }

    this.fnGetSearchSegments = function () {
        if (Object.keys(flightSearchRequestData).length > 0) {
            $(oSettings.keys.searchcriteria).html(oSettings.html.refresh);
            $.ajax({
                url: oSettings.urls.getFlightSearchCriteria,
                method: 'post',
                dataType: 'html',
                data: flightSearchRequestData,
                success: function (html) {
                    $(oSettings.keys.searchcriteria).html($(html).html())
                    fnInitComponent();
                    fnInitlizeDestinations();
                },
                error: function (xhr, request, error) {
                }
            });
        } else {
            $(function () {
                fnUpdateModal('Warning', undefined, oSettings.html.missingSearchRequest);
                fnUpdateModalFooterToRedirect('Go To Search Flights', oSettings.urls.searchflights);
                fnShowModal();
            });
        }
    }

    this.fnSearch = function () {
        if (Object.keys(flightSearchRequestData).length > 0) {
            fnGetSearchSegments();
            fnSearchFlights();
        } else {
            $(function () {
                fnUpdateModal('Warning', undefined, oSettings.html.missingSearchRequest);
                fnUpdateModalFooterToRedirect('Go To Search Flights', oSettings.urls.searchflights);
                fnShowModal();
            });
        }
    }

    this.fnRenderFilters = function () {
        if (this.filterBy.airlines.length > 0) {
            var $ul = $("<ul>");
            for (var i = 0; i < this.filterBy.airlines.length; i++) {
                var airline = this.filterBy.airlines[i];
                var html = '';
                if (i == 0) {
                    html += '<li>';
                    html += '    <span class="air-list">';
                    html += '        <input type="checkbox" checked class="checkbox-all" data-flight-filter="airline-all">';
                    html += '        <label>All Airlines</label>';
                    html += '    </span>';
                    html += '</li>';
                }

                html += '<li>';
                html += '     <span class="air-list">';
                html += '         <input type="checkbox" checked data-flight-filter="airline" name="chk_' + airline.AirlineCode + '" />';
                html += '         <label for="chk_' + airline.AirlineCode + '">' + airline.Records[0].AirlineName + '</label>';
                html += '         <span class="air-count">(' + airline.Records.length + ' Flights)</span>';
                html += '     </span>';
                html += '';
                html += '     <span class="air-list-price"><i class="fa fa-inr" aria-hidden="true"></i> ';
                html += Enumerable.From(airline.PriceList || []).MinBy(function (item) { return item }) || 0;
                html += '        </span>';
                html += ' </li>';
                $ul.append(html);
            }
            $('#dvAirlineCodeList').html($ul);
        }
        $(':checkbox[data-flight-filter="airline-all"]').on("change", function () {
            var $checkboxes = $(':checkbox[data-flight-filter="airline"]');
            var checked = $checkboxes.filter(':checked').length;
            var unchecked = $checkboxes.filter(':unchecked').length;
            if ($(this).prop("checked") == true && checked != $checkboxes.length) {
                $checkboxes.prop("checked", false);
            }
            if ($(this).prop("checked") == false && unchecked != $checkboxes.length) {
                $(this).prop("checked", false);
            }
        })

        $(':checkbox[data-flight-filter="airline"]').on("change", function () {
            var code = $(this).attr("name").replace("chk_", "")
            if ($(this).prop("checked") == true) {
                $(':checkbox[data-flight-filter="airline-all"]').prop("checked", false);
            }
        })

        $(".checkbox-all").each(function () {
            $(this).checkAll({ container: $(this).closest('ul'), showIndeterminate: true });
        });
    };

    this.fnfilter = function () {
        if (flights.length > 0) {
        }
    }

    this.fnAppendError = function ($element, message) {
        if ($element.length) {
            $element.next('.field-validation-error').remove();
            $element.addClass("input-validation-error");
            $element.after($("<span />", {
                class: 'field-validation-error',
            }).append("<span>" + (message || "") + "</span>"));
        }
    }

    this.fnRemoveError = function ($element, message) {
        if ($element.length) {
            $element.next('.field-validation-error').remove();
            $element.removeClass("input-validation-error");
        }
    }

    this.fnGetSearchCriteria = function (journeyType) {
        $("#frmFlightSearch").find(".formWrap").addClass("search-criteria")
            .html(oSettings.html.loading);
        if (journeyType > 0) {
            $.ajax({
                url: oSettings.urls.getJourneyTypeSearchCriteria,
                method: 'get',
                dataType: 'html',
                data: { journeyType: journeyType },
                success: function (html) {
                    $(".flight-search-section")
                        .html($(html).html());
                    fnInitlize();
                    initPluggins();
                },
                error: function (xhr, request, error) {
                }
            });
        }
    }

    this.fnflightBookNow = function () {
        ////$(document).on("click", oSettings.on.flightBookPayment, function () {
        ////    var formData = fnGetBookingSummary(oSettings.keys.flightBookSummary);
        ////    if (Object.keys(formData).length > 0) {
        ////        showWaitProcess()
        ////        $.ajax({
        ////            url: oSettings.urls.makepayment,
        ////            method: 'post',
        ////            data: formData,
        ////            timeout: 300000, // 5 min
        ////            success: function (json) {
        ////                if (json.Status == false && json.Error instanceof Object && json.Error.ErrorCode > 0) {
        ////                    if (json.Error.ErrorMessage == "Your session (TraceId) is expired.") {
        ////                        fnSearchSessionExpire("forced");
        ////                    } else {
        ////                        $(function () {
        ////                            fnUpdateModal('Warning', undefined, "<div class='text-center missing-content'>" + json.Error.ErrorMessage + "</div>");
        ////                            fnShowModal();
        ////                        });
        ////                    }
        ////                }
        ////                else if (json.Status && json.Status == true && json.Bookings.length > 0) {
        ////                    $(function () {
        ////                        var message = "Your Boooking hasbeen Successfully done. <br/>";
        ////                        if (json.Bookings && json.Bookings.length > 0) {
        ////                            for (var i = 0; i < json.Bookings; i++) {
        ////                                message += "Origin : " + json.Bookings[i].Origin + " - " + "Destination : " + json.Bookings[i].Destination
        ////                                message += "BookingId : " + json.Bookings[i].BookingId + "<br/>";
        ////                                message += "PNR Number: " + json.Bookings[i].Pnr + "<br/>";
        ////                                message += "<hr />";

        ////                            }
        ////                        }

        ////                        fnUpdateModal('Success', undefined, "<div class='text-center missing-content'>" + message + "</div>");
        ////                        fnUpdateModalFooterToRedirect('Go To MyAccount', oSettings.urls.flightSearch);
        ////                        fnShowModal();
        ////                        fnReset()
        ////                    });
        ////                }

        ////                hideWaitProcess();
        ////            },
        ////            error: function (xhr, request, error) { }
        ////        });
        ////    }
        ////})

        $(document).on('click', '[data-flight="confirmpayment"]', function () {
            if (oFlightBooking && Object.keys(oFlightBooking).length > 0) {
                localStorage.setItem(oSettings.keys.flightBookSummary, JSON.stringify(oFlightBooking));
                oFlightBooking = {};
            }
            var formData = {};
            try {
                formData = JSON.parse(localStorage.getItem(oSettings.keys.flightBookSummary));
            } catch (e) {
            }
            if (Object.keys(formData).length > 0) {
                showWaitProcess()
                $.ajax({
                    url: oSettings.urls.proccedtopayment,
                    method: 'post',
                    data: formData,
                    success: function (json) {
                        window.location = oSettings.urls.proccedtopayment;
                    },
                    error: function (xhr, request, error) { }
                });
            }
        });
    };

    this.fnAddNewSegment = function () {
        $.ajax({
            url: oSettings.urls.addNewSegment,
            method: 'get',
            dataType: 'html',
            success: function (html) {
                $(oSettings.keys.dvSearchSegments).last().after(html);
                fnInitlizeDestinations();
                initPluggins();
                fnCountSegments();
            },
            error: function (xhr, request, error) {
            }
        });
    }

    this.fnCountSegments = function () {
        $(this.oSettings.on.addNewSegment).toggle($(oSettings.keys.dvSearchSegments).length < 6);
    };

    this.fnDestinationFormat = function (oData) {
        if (oData.loading) {
            return oData.text;
        }
        var markup = "";
        markup += '<div class="select2-result-repository clearfix">';
        markup += ' <div class="flt-detas destinations">';
        markup += '    <div class="row">';
        markup += '        <div class="col-sm-12">';
        markup += '            <div class="lft-flt">';
        markup += '                <div>' + oData.CityName + ' (' + oData.CityCode + ')</div>';
        markup += '        <div>';
        markup += '                <span>' + oData.ShortDetail + '</span>';
        markup += '            <span class="show-country">' + oData.CountryName + '';
        markup += '             <span class="flag-sprt vMid dib ' + oData.CountryCode.toLowerCase() + '"></span></div> ';
        markup += '            </span>';
        markup += '        </div>';
        markup += '            </div>';
        markup += '        </div>';
        markup += '        <div class="col-sm-3">';
        markup += '        </div>';
        markup += '  </div>';
        markup += '</div>';
        return markup;
    };

    this.fnDestinationFormatSelection = function (oData) {
        return (oData.CityCode != undefined ? (oData.CityName + "(" + oData.CityCode + ")") : oData.text);
    };

    this.fnGetAccessToken = function () {
        var $token = $(":hidden[name='SearchTokenId']").val();
        if ($token && $token !== null && $token !== '') {
            localStorage.setItem(oSettings.keys.accessToken, $token);
        }
        var token = localStorage.getItem(oSettings.keys.accessToken);

        if ($('[name="Response.SessionExpired"]').length > 0 &&
            ($('[name="Response.SessionExpired"]').val() || '').toLowerCase() == "true") {
            localStorage.removeItem(oSettings.keys.accessToken)
            token = null;

            fnUpdateModal('<i class="far fa-clock"></i> Session Timed Out', undefined, oSettings.html.searchSessionExpire);
            fnUpdateModalFooterToRedirect('Go To Search Flights', oSettings.urls.searchflights);
            fnShowModal();
        }

        return token;
    };

    this.fnInitlizeDestinations = function () {
        $(this.oSettings.on.locations).each(function () {
            var $element = $(this);
            var oSelect2 = {};
            oSelect2.ajax = {
                url: oSettings.urls.getDestinations,
                dataType: 'json',
                delay: oSettings.delay,
                data: function (params) {
                    return { search: params.term || "", page: params.page || 1 };
                },
                processResults: function (data, params) {
                    params.page = params.page || 1;
                    return {
                        results: data.results,
                        pagination: {
                            more: data.pagination.more
                        }
                    };
                },
                cache: true
            }
            oSelect2.placeholder = $element.data("placeholder") || "";
            oSelect2.escapeMarkup = function (markup) { return markup; };
            oSelect2.templateResult = fnDestinationFormat;
            oSelect2.templateSelection = fnDestinationFormatSelection;
            $element.select2(oSelect2);
            $element.css({ "width": "auto", "display": "block !important" });
        });

        $(this.oSettings.on.locations).on("select2:select", function (e) {
            var oSelectData = e.params.data;
            var cityCode = e.params.data.CityCode;
            var searchIn = e.params.data.SearchIn;
            destinations = destinations.filter(function (item) { return item.CityCode !== cityCode; })
            destinations.push({ CityCode: cityCode, SearchIn: searchIn });
            if (!isNaN($(e.currentTarget).data("index"))) {
                var oData = fnGetSearchRequest();
                if (oData != undefined && oData.Segments instanceof Array && oData.Segments.length > 0) {
                    oData.Segments[$(e.currentTarget).data("index")].DisplayOrigin = oSelectData.SearchIn;
                    oData.Segments[$(e.currentTarget).data("index")].Origin = oSelectData.CityCode;
                    oData.Segments[$(e.currentTarget).data("index")].OriginCityName = oSelectData.CityName;
                }
            }
        })

        $(this.oSettings.on.search).parents('form').removeData("validator");
        $(this.oSettings.on.search).parents('form').removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse($(this.oSettings.on.search).parents('form'));

        $(this.oSettings.on.search).on("click", function () {
            var isValid = true;
            var journeyType = $(":hidden[name='JourneyType']").val();
            var totalPassengers = 0;
            $(oSettings.on.personCounter).each(function () {
                totalPassengers += parseInt($(this).val())
            });

            if (journeyType == 1) {
                if ($('[name="DepartureDateTime"]').val() == "") {
                    isValid = false;
                    fnAppendError($('[name="DepartureDateTime"]'), "Please Select Departure Date");
                }
            }

            $(oSettings.keys.dvSearchSegments).each(function () {
                var $Origin = $(this).find(".location-from").eq(0);
                var $Destination = $(this).find(".location-to").eq(0);
                var $DepartureDate = $(this).find('.datepicker:text')
                if (($Origin.val() === null) || ($Origin.val() === "")) {
                    isValid = false;
                    fnAppendError($Origin.next('.select2-container'), "Enter Origin City or Airport");
                } else {
                    fnRemoveError($Origin.next('.select2-container'))
                }
                if (($Destination.val() === null) || ($Destination.val() === "")) {
                    isValid = false;
                    fnAppendError($Destination.next('.select2-container'), "Enter Destination City or Airport");
                } else {
                    fnRemoveError($Destination.next('.select2-container'))
                }
                if ($DepartureDate.val() === "") {
                    isValid = false;
                    fnAppendError($DepartureDate, "Select Date");
                }
                else {
                    fnRemoveError($DepartureDate)
                }
            });

            if (totalPassengers > 9) {
                isValid = false;
                $(oSettings.keys.dvPessons).show();
                fnAppendError($(oSettings.keys.dvPessons).find("ul"), "Maximum of 9 travellers allowed");
            }

            if (totalPassengers == 0) {
                isValid = false;
                $(oSettings.keys.dvPessons).show();
                fnAppendError($(oSettings.keys.dvPessons).find("ul"), "Total travellers should be grater than 0");
            }

            if (isValid) {
                $(this).parents('form').find(".overlay").removeClass("hide");
                fnGetDestinations();
            }
            return isValid;
        });
    };

    this.fnReset = function () {
        localStorage.removeItem(oSettings.keys.flightDestinations);
        localStorage.removeItem(oSettings.keys.flightBook);
        localStorage.removeItem(oSettings.keys.searchRequest);
        localStorage.removeItem(oSettings.keys.flightBookSummary);
    };
    this.fnSearchSessionExpire = function (key) {
        if ($(oSettings.keys.searchExpire).length > 0 || key == "forced") {
            fnReset();
            if ($("[name='NoModal']").length == 0) {
                $(function () {
                    fnUpdateModal('Warning', undefined, oSettings.html.searchSessionExpire);
                    fnUpdateModalFooterToRedirect('Go To Search Flights', oSettings.urls.searchflights);
                    fnShowModal();
                });
            }
        }
    };

    this.fnInitComponent = function () {
        $(this.oSettings.on.flightDetail).on("click", function () {
            if ($(this).data("result-index") || $(this).data("result-index") !== "") {
                fnGetflightDetails($(this).data("result-index"));
            }
        });

        this.fnGetDestinations();

        $('[data-flight="CityCode"]').each(function () {
            var code = $(this).data("code") || "";
            var location = destinations.filter(function (item) { return item.CityCode === code; });
            if (location.length > 0) {
                $(this).html(location[0].SearchIn);
            }
        });
        $(this.oSettings.on.flightBook).off("click").on("click", function (event) {
            event.stopPropagation();
            var obIndex = $(this).data('result-index-ob');
            var ibIndex = $(this).data('result-index-ib');
            var index = $(this).data("result-index");
            if (obIndex && ibIndex) {
                index = obIndex + "," + ibIndex;
            }
            fnGetflightDetails(index || "", 'hold');
            window.location = oSettings.urls.flightBooking;
        });
    };

    this.selectFlight = function ($radio) {
        var flightType = $($radio).attr("name") || '';
        var container = '';
        switch (flightType) {
            case "OB": container = 'li.outbond-flight';
                $('[data-flight="book"]').data("result-index-ob", $($radio).data('index'));
                break;
            case "IB": container = 'li.inbond-flight';
                $('[data-flight="book"]').data("result-index-ib", $($radio).data('index'));
                break;
        }

        var $li = $(container);
        if ($li.length > 0) {
            $li.find('.airline_details > i').attr('class', 'flightImagesNewdib flight-carrier-sprite ' + $($radio).data('icon'));
            $li.find('.airline_details__name > span').eq(0).html($($radio).data('airlinename'));
            $li.find('.airline_code > span').html($($radio).data('airlinecode'));
            $li.find('.result_flight__departure_and_stops__departure').html($($radio).data('departuretime'));
            $li.find('.result_flight__arrival__wrapper > span').html($($radio).data('arrivaltime'));
 
            var price = 0;
            if ($(':radio[name="OB"]:checked').length) {
                price = parseFloat($(':radio[name="OB"]:checked').data('price') || 0);
            }
            if ($(':radio[name="IB"]:checked').length) {
                price += parseFloat($(':radio[name="IB"]:checked').data('price') || 0);
            }

            $("#dvflightPrice").html(addCommas(price));

            fnToggleBook()
        }
    };

    this.fnInitlize = function () {
        this.fnInitlizeDestinations();
        this.fnGetSearchRequest(this.flightSearchRequestData);
        this.fnGetAccessToken();
        this.fnGetPassengers();
        this.fnGetBookingSummary();
        this.fnflightBookNow();

        this.fnSearchSessionExpire();

        if ($(this.oSettings.keys.searchcriteria).length && $(this.oSettings.keys.searchcriteria).length) {
            this.fnSearch();
        }
        if ($('[name="cleartoken"]').length > 0 && ($('[name="cleartoken"]').val() || '').toLowerCase() == 'true') {
            localStorage.removeItem(oSettings.keys.accessToken);
        }

        $(this.oSettings.on.journeyType).on("click", function () {
            var journeyType = $(this).data("journeytype");
            $(oSettings.on.journeyType).removeClass("active");
            $(this).addClass("active");
            $(":hidden[name='JourneyType']").val(journeyType)
            $(".journey-datetime").toggle(journeyType == 1 || journeyType == 2);

            fnGetSearchCriteria(journeyType);
        });

        $(this.oSettings.on.togglePersons).on("click", function () {
            $(oSettings.keys.dvPessons).toggle();
        });

        $('[data-flight="pessengers-hide"]').on("click", function () {
            $(oSettings.keys.dvPessons).hide();
        });

        $(this.oSettings.on.addNewSegment).on("click", function () {
            fnAddNewSegment();
        });
        $(document).on("click", ".remove-segment", function () {
            $(this).parents(oSettings.keys.dvSearchSegments).remove();
            fnCountSegments();
        });

        $(this.oSettings.on.personCounter).on("change", function () {
            var total = 0;
            $(oSettings.on.personCounter).each(function () {
                total += parseInt($(this).val())
                $(":hidden[name='" + $(this).data("name") + "']").val($(this).val());
            });
            $(oSettings.keys.totalPersons).html(total);
        });

        $(this.oSettings.on.personCounter).eq(0).trigger("change");

        $(this.oSettings.keys.dvSiglePassengers).addClass("hide");

        $(this.oSettings.keys.dvSiglePassengers).eq(0).removeClass("hide");

        $(document).on("ifChecked", ':radio.selectflight', function () {
            selectFlight($(this));
        });

        $(document).on("submit", "#frmFlightBook", function (e) {
            showWaitProcess();
        });

        $(document).on("click", "div.bhoechie-tab-menu>div.list-group>a", function (e) {
            e.preventDefault();
            $(this).siblings('a.active').removeClass("active");
            $(this).addClass("active");
            var index = $(this).index();
            $("div.bhoechie-tab>div.bhoechie-tab-content").removeClass("active");
            $("div.bhoechie-tab>div.bhoechie-tab-content").eq(index).addClass("active");
        });

        $("#search-fromdate").on('changeDate', function (ev) {
            $('#search-returndate').datepicker("destroy");
            $("#search-returndate").datepicker({ format: dateformat, todayHighlight: true, autoclose: true, startDate: ev.date });

            if ($("#search-returndate").hasClass("not-blank")) {
            } else {
                $("#search-returndate").val('');
            }
        });

        $("#search-returndate").on('blur', function (ev) {
            var journeyType = $(this).val() != '' && $(this).val().split('/').length == 3 ? 2 : 1;
            $(oSettings.on.journeyType).removeClass("active");
            $('[data-journeytype="' + journeyType + '"]').addClass("active");
            $(":hidden[name='JourneyType']").val(journeyType)
        });
        $("#search-returndate").on('change', function (ev) {
            var journeyType = $(this).val() != '' && $(this).val().split('/').length == 3 ? 2 : 1;
            $(oSettings.on.journeyType).removeClass("active");
            $('[data-journeytype="' + journeyType + '"]').addClass("active");
            $(":hidden[name='JourneyType']").val(journeyType)
        });

        $(oSettings.on.personCounter).on("change", function () {
            var totalPassengers = 0;
            $(oSettings.on.personCounter).each(function () {
                totalPassengers += parseInt($(this).val())
            });
            if (totalPassengers <= 9 && totalPassengers > 0) {
                $(oSettings.keys.dvPessons).show();
                $(oSettings.keys.dvPessons).find("ul").next('.field-validation-error').remove();
            }
        });

        $(document).on("change", '.flightcountry', function () {
            var code = $(this).data("code")
            if ($(code).length) {
                $(code).val($(this).val())
            }
        });

        $(document).on('click', '[data-flight="search-modify"]', function (event) {
            event.preventDefault();
            var oData = fnGetSearchRequest();
            $('[name="OB-From"]').next(".select2-container")
                .find(".select2-selection__rendered").html(oData.Segments[0].DisplayOrigin);
            $('[name="IB-From"]').next(".select2-container")
                .find(".select2-selection__rendered").html(oData.Segments[1].DisplayOrigin);

            $("select.change-cabin").val(oData.Segments[0].FlightCabinClass);

            $(".flight-section").addClass("flight-overlay");

            $(".search-segments").hide();
            $(".search-modify").show();
        });
        $(document).on('click', '[data-flight="search-cancle"]', function (event) {
            event.preventDefault();
            $(".flight-section").removeClass("flight-overlay");
            $(".search-modify").hide();
            $(".search-segments").show();
        });

        $(document).on('click', '[data-flight="search-newdata"]', function (event) {
            event.preventDefault();
            var oData = fnGetSearchRequest();
            var obFrom = $('[name="OB-From"]').val();
            var ibFrom = $('[name="IB-From"]').val();
            var cabinClass = $("select.change-cabin").val();
            obFrom = obFrom === null ? oData.Segments[0].Origin : obFrom;
            ibFrom = ibFrom === null ? oData.Segments[1].Origin : ibFrom;
            $('[name="OB-From"]').parent().toggleClass('blank-origin', (obFrom == ''));
            $('[name="IB-From"]').parent().toggleClass('blank-origin', (ibFrom == ''));
            if (obFrom !== null && obFrom != '' && ibFrom !== null && ibFrom != '') {
                if (oData != undefined && oData.Segments instanceof Array && oData.Segments.length > 0) {
                    oData.Segments[0].Origin = obFrom;
                    oData.Segments[1].Origin = ibFrom;
                    if (!isNaN(cabinClass)) {
                        oData.Segments[0].FlightCabinClass = parseInt(cabinClass);
                        oData.Segments[1].FlightCabinClass = parseInt(cabinClass);
                    }
                    fnModifySearch(oData);
                    fnUpdateSegments();
                    $(".flight-section").removeClass("flight-overlay");
                    $(".search-modify").hide();
                    $(".search-segments").show();
                    fnSearchFlights();
                }
            }
        });

        $(document).on('click', '.result_flight', function (event) {
            $('.result_flight').not($(this)).removeClass('live');
            $('.flight_tab_detail').not($(this).find('.flight_tab_detail')).hide();

            $(this).toggleClass('live');
            $(this).find('.flight_tab_detail').slideToggle('slow');

            event.stopPropagation();
        });

        $(document).on('click', '.flight_tab_detail > div', function (event) {
            event.stopPropagation();
        });

        $(document).on('click', '[data-scroll]', function (e) {
            e.preventDefault();

            if ($($(this).data('scroll')).length) {
                var offset = $($(this).data("scroll")).offset().top - $(".selectedFlightBox").outerHeight();
                console.log(offset);
                $('html, body').animate({
                    scrollTop: offset
                }, 2000);
            }
        });
    };

    this.fnToggleBook = function () {
        var $book = $('[data-flight="book"]');
        if ($book.data("result-index-ib") && $book.data("result-index-ob")) {
            $('.book-virtual').hide();
            $book.removeClass('hide');
        }
        else {
            $('.book-virtual').show();
            $book.addClass('hide');
        }
    }

    this.autoInitilize = (function () {
        this.fnInitlize();
    })();
})();

function addCommas(x) {
    var parts = x.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}