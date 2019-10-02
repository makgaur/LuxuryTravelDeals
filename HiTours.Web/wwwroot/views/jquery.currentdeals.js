(function () {
    var limit = 6;
    var offset = 0;
    var scroll = 600;
    var searchBy = 1;
    var isSearch = false;
    searchTerm = [];
    var searchYear = year;
    var searchMonth = month;
    var searchDate;
    var isFocus = false;
    var monthNames = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    var deal = 0;
    var formRequestCallback = '';
    
    $(document).on("click", ".getdeals", function () {
        $(".replacedeals").text($(this).text());
        $("[data-deals]").each(function () {
            $(this).removeClass("active");
        });
        $(this).addClass("active");
        searchBy = $(this).data('deals');
        $("#dCurrentDeals").html('');
        isSearch = true;
        offset = 0;
        scroll = 600;
        getCurrentDeals(limit, offset);
    });

    $(document).on("click", ".typeahead__cancel-button", function () {
        if ($("#txtCRC").val().length == 0) {
            $(".destinationSearchBar > .typeahead__container").removeClass("cancel").removeClass("result");
        }
    })
    $(document).on("click", ".btn-view-more", function () {
        offset++;
        isSearch = false;
        getCurrentDeals(12, offset, true);
        scroll = scroll + 500;
    })
    $(window).scroll(function () {
        if ($("#dCurrentDeals").length) {
            if (offset >= 2)
            {
                if ($(".btn-view-more").hasClass('display-none') && $('#loader_image').hasClass('display-none') && !$(".btn-view-more").hasClass("block-view")) {
                    $(".btn-view-more").removeClass('display-none');
                }
            }
            else if($(window).scrollTop() > scroll) {
                offset++;
                isSearch = false;
                getCurrentDeals(limit, offset);
                scroll = scroll + 500;
            }
            else {
                $("#loader_message").html("").hide();
            }
        }
    });
    //if ($("#dCurrentDeals").length && (typeof isDeafultLoad === "undefined" || isDeafultLoad == true)) {
    //    getCurrentDeals(limit, offset, deal);
    //}

    var $searchInput = $(".searchInputDestination");
    var $divDestination = $("#divDestination");
    var $filterBox = $(".btn-filter");
    var $divFilter = $("#divFilter");
    var $btnDate = $(".btn-date");
    var $divDeparture = $("#divDeparture");

    //searchInput.on("focus", function () {
    //    divFilter.hide();
    //    divDeparture.hide();
    //    setRecentSearch();
    //    isFocus = true;

    //})
    $searchInput.on("keyup", function (e) {
        //if (e.which == 13 && $(".typeahead__item.active").length == 0) {                
        //    $(".typeahead__item:first").click()
        //}
       
        var txt = $(this);
        if (txt.val().length <= 0) {
            setRecentSearch();
            $divFilter.hide();
            $divDeparture.hide();
        }
        else { $divDestination.hide() }
    }).on("click", function () {
        $divFilter.hide();
        $divDeparture.hide();
        if ($(".typeahead__list").css("display") != "block") {
            if (!isFocus) {
                if ($divDestination.css("display") == "block") {
                    $divDestination.toggle();
                }
                else {
                    setRecentSearch();
                    $divDestination.toggle();
                    setRecentSearch();
                }
            }
            isFocus = false;
        }
    });
   
    $filterBox.on("click", function () { $divDestination.hide(); $divDeparture.hide(); $divFilter.show(); });

    $btnDate.on("click", function () { $divDestination.hide(); $divFilter.hide(); $divDeparture.show(); });

    $('[role="presentation"]').on("click", function () {
        var $navEle = $(this);
        $('[role="presentation"]').removeClass("active");
        $navEle.addClass("active");
        $("[role='presentation']").each(function () {
            var $ele = $(this);
            if ($navEle.find("#dateFilterPeriodTab").length) {
                $(".period-picker").show();
                $(".date-picker").hide();
            }
            else {
                $(".period-picker").hide();
                $(".date-picker").show();
            }
        })
    })

    typeof $.typeahead === 'function' && $.typeahead({
        input: ".js-typeahead",
        minLength: 3,
        order: "asc",
        group: true,
        maxItemPerGroup: 3,
        groupOrder: function (node, query, result, resultCount, resultCountPerGroup) {
            var scope = this,
                sortGroup = [];

            for (var i in result) {

                sortGroup.push({
                    group: i,
                    length: result[i].length
                });
            }

            sortGroup.sort(
                scope.helper.sort(
                    ["length"],
                    true, // false = desc, the most results on top // task id 4
                    function (a) {
                        return a.toString().toUpperCase()
                    }
                )
            );

            return $.map(sortGroup, function (val, i) {
                return val.group
            });
        },
        hint: true,
        template: "<i class='fa fa-globe' aria-hidden='true'></i>{{display}}",
        emptyTemplate: "no result for {{query}}",
        source: {
            country: {
                ajax: {
                    url: "/selectlist/filtercountries",
                }
            },
            city: {
                ajax: {
                    url: "/selectlist/filtercity",
                }
            },
            state: {
                ajax: {
                    url: "/selectlist/filterstate",
                }
            },
            region: {
                ajax: {
                    url: "/selectlist/filterregion",
                }
            }
        },
        callback: {
            onClickAfter: function (node, a, item, event) {
                event.preventDefault();
                updateURL(item.display,item.group);
                $divDestination.hide();
                setSearch(destination, item);
                $('.js-result-container').text('');
            },
            onResult: function (node, query, obj, objCount) {
                var text = "";
                if (query !== "") {
                    text = objCount + ' elements matching "' + query + '"';
                }
                $('.js-result-container').text(text);
            },
            // Replace all underscore "_" by spaces in the result list
            //onLayoutBuiltBefore: function (node, query, result, resultHtmlList) {
            //    var mresult = [];
            //    $.each(result, function (i, e) {
            //        var matchingItems = $.grep(mresult, function (item) {
            //            return item.display == e.display && e.group == "state";
            //        });
            //        if (matchingItems.length === 0) {
            //            mresult.push(e);
            //        }
            //    });
            //    debugger;
            //    return mresult;
            //},
            onCancel: function (node, event) {
                if (event.originalEvent.type === "mousedown") {
                    setSearch(destinationCancel);
                }
            }
        },
        debug: true
    });


    $(".date-anyday").html("Anytime in " + monthNames[month]);

    $(".date-anyday").on("click", function () {
        var record = { anyday: true, month: searchMonth + 1, year: searchYear, isDepartureCancel: false };
        setSearch(anyday, record);
    })

    $(".cal-seach-close").on("click", function () {
        setSearch(departureCancel);
    })
    $(".cal-seach-close-filter").on("click", function () {
        setSearch(filterCancel);
    })
    $(document).on("click", "#dvRecent>.destination span", function () {
        var $text = $(this);
        var recentSrArray = getRecentDearchLocalSt();
        recentSrArray.filter(function (item, index, arr) {
            if (item.display == $text.html().replace('<i class="fa fa-globe" aria-hidden="true"></i>', '')) {
                switch (item.type) {
                    case destination:
                        var record = { matchedKey: "display", display: item.display, group: item.group };
                        setSearch(item.type, record);
                        var txt = $("#txtCRC");
                        txt.val(item.display);
                        $(".typeahead__container").addClass("cancel");
                        $(".typeahead__cancel-button").show();
                        updateURL(item.display, item.group); 
                        break;
                    case departure:
                        var record = { date: item.value, flexible: item.value2, anyday: false, isDepartureCancel: false };
                        setSearch(item.type, record);
                        updateURL(item.display, item.group); 
                        break;
                    case anyday:
                        var record = { anyday: true, month: item.value, year: item.value2, isDepartureCancel: false };
                        setSearch(item.type, record);
                        updateURL(item.display, item.group); 
                        break;
                    case filterstyle:
                        
                        var record = { travelstyle: item.display, travelstyleid: item.value };
                        setSearch(item.type, record);
                        updateURL(item.display, item.group); 
                        break;
                }
                $divDestination.hide();
            }
        });
    })

    $(document).on("click", ".widthdoublehalf", function () {
        var $text = $(this);
        var record = { travelstyle: $text.html(), travelstyleid: $text.data("style-id") };
        $divDestination.hide();
        setSearch(filterstyle, record);
        updateURL($text.data("style-id"), filterstyle); 
    })

    $(document).on("click", function (e) {
        //e.preventDefault();
        var $target = $(e.target);
        if ($target.parents('.search-container').length == 0) {
            $divDeparture.hide();
            $divFilter.hide();
            $divDestination.hide();
        }
        else if ($target.parents('.searchDestination').length == 0) {
            var txt = $("#txtCRC");
            if (txt.val().length <= 0 && searchTerm.length > 0 && (searchTerm[0].isDestinationCancel == undefined || !searchTerm[0].isDestinationCancel)) {
                txt.val(searchTerm[0].display);
                //$(".typeahead__container").addClass("cancel");
                $(".typeahead__cancel-button").show();
            }
        }
        //else {
        //    $('html, body').animate({ scrollTop: 390 }, 900, 'linear');
        //}
    })
    window.setSearch = function (searchType, item) {
        recentSearchCollection = [];
        var previousSearch = localStorage.getItem(recentSearchHistory);
        if (previousSearch != null && previousSearch.length > 0) {
            var prev = b64DecodeUnicode(previousSearch);
            recentSearchCollection = JSON.parse(prev);
        }

        var $dpCancel = $(".cal-seach-close");
        var $spDeparture = $("#spDepartureDate");

        var $filterCancel = $(".cal-seach-close-filter");
        var $spFilters = $("#spFilters");
        offset = 0;
        scroll = 600;
        if (searchType == destination) {
            searchTerm[0] = item
            searchTerm[0].isDestinationCancel = false;
            recentSearchCollection.push({ "display": item.display, "id": recentSearchCollection.length, "value": item.display, "value2": "", "group": item.group, "type": destination });
        }
        else if (searchType == departure) {
            $divDeparture.hide();

            ////item = { date: $("#datepicker_west").datepicker("getDate"), flexible: $drpFlexible.val(), anyday: false, isDepartureCancel: false };

            if (searchTerm.length == 0) {
                searchTerm[0] = item;
            }
            else {
                searchTerm[0].anyday = item.anyday;
                searchTerm[0].date = item.date;
                searchTerm[0].flexible = item.flexible;
                searchTerm[0].isDepartureCancel = item.isDepartureCancel;
            }
            $spDeparture.html(GetFormattedDate(item.date) + " " + getFlexible(item.flexible));
            recentSearchCollection.push({ "display": $spDeparture.html(), "id": recentSearchCollection.length, "value": item.date, "value2": item.flexible, "group": departure, "type": departure });
            $dpCancel.show();
            $("#drpFlexible").val(item.flexible);
        }
        else if (searchType == anyday) {
            $divDeparture.hide();
            //item = { anyday: true, month: searchMonth + 1, year: searchYear, isDepartureCancel: false };
            if (searchTerm.length == 0) {
                searchTerm[0] = item;
            }
            else {
                searchTerm[0].anyday = item.anyday;
                searchTerm[0].month = item.month;
                searchTerm[0].year = item.year;
                searchTerm[0].isDepartureCancel = false;
            }

            $spDeparture.html(monthNames[item.month - 1] + " " + item.year);
            recentSearchCollection.push({ "display": $spDeparture.html(), "id": recentSearchCollection.length, "value": item.month, "value2": item.year, "group": departure, "type": anyday });
            $dpCancel.show();
        }
        else if (searchType == departureCancel) {
            searchTerm[0].isDepartureCancel = true;
            $dpCancel.hide();
            $spDeparture.html(departureDatePlaceHolder);
        }
        else if (searchType == destinationCancel) {
            searchTerm[0].isDestinationCancel = true;
        }
        else if (searchType == filterstyle) {
            if (searchTerm.length == 0) {
                searchTerm[0] = item;
            }
            else {
                searchTerm[0].travelstyle = item.travelstyle;
                searchTerm[0].travelstyleid = item.travelstyleid;
                searchTerm[0].isFilterCancel = item.isFilterCancel;
            }
            $spFilters.html(item.travelstyle);
            recentSearchCollection.push({ "display": item.travelstyle, "id": recentSearchCollection.length, "value": item.travelstyleid, "value2": "", "group": filterstyle, "type": filterstyle });
            $filterCancel.show();
        }
        else if (searchType == filterCancel) {
            searchTerm[0].isFilterCancel = true;
            $filterCancel.hide();
            $spFilters.html(filterPlaceHolder);
        }

        localStorage.setItem(recentSearchHistory, b64EncodeUnicode(JSON.stringify(recentSearchCollection)));
        $("#dCurrentDeals").html('');
        isSearch = true;
        getCurrentDeals(limit, offset);
    }
    window.updateURL = function (display, group) {
        if (history.pushState) {
            var newurl = window.location.protocol + "//" + window.location.host + window.location.pathname.replace("Home","") + 'Home?value=' + encodeURI(display) + '&key=' + encodeURI(group);
            window.history.pushState({ path: newurl }, '', newurl);
        }
    }

    
    function BindDestinationForRequest(destination) {
        formRequestCallback ='<div class="search-notify-head"><h4> Sorry currently no deals available matching your criteria.<br>To get notified please submit your email address.</h4></div>'+
            '<div class="search-notify-form"><form id="frmTalkExpertSearch" action="https://crm.zoho.com/crm/WebToLeadForm" name="WebToLeads2951943000000505001s" method="POST" accept-charset="UTF-8" novalidate="novalidate">' +
            '                    <!-- Do not remove this code. -->' +
            '                    <input type="text" style="display:none;" name="xnQsjsdp" value="b81b0a9bd05df24e119e2cbfd8701905841260953794e923e7a3883a8f0c75bb">' +
            '                    <input type="hidden" name="zc_gad" id="zc_gad" value="">' +
            '                    <input type="text" style="display:none;" name="xmIwtLD" value="877c0b66da7493104c834b7d71fa26298443b704fca78de91585eabccb4a548a">' +
            '                    <input type="text" style="display:none;" name="actionType" value="TGVhZHM=">' +
            '' +
            '                    <input type="text" style="display:none;" name="returnURL" value="https://www.luxurytravel.deals/home/thankyou">' +
            '                    <!-- Do not remove this code. -->' +
            '                    <input id="requestPageUrl" type="hidden" name="PageUrl" value="Destination Search">' +

            '                       <div class="search-email-input"> <input type="email" class="form-control" data-val="true" placeholder="Email" data-val-email="Email Address is not valid" data-val-required="The Email field is required." id="Email" name="Email" value="">' +
            '                        <span class="field-validation-valid" data-valmsg-for="Email" data-valmsg-replace="true"></span></div>' +

            '                    <div style="display:none;">' +
            '                        <div style="nowrap:nowrap;text-align:left;font-size:12px;font-family:Arial;width:50%">IP</div>' +
            '                        <div style="width:250px;">' +
            '                            <input type="text" style="width:250px;" maxlength="255" name="LEADCF22" value="true">' +
            '                        </div>' +
            '                    </div>' +
            '' +
            '                    <div style="display:none;">' +
            '                        <div style="wrap:nowrap;text-align:left;font-size:12px;font-family:Arial;width:50%">Lead Status</div>' +
            '                        <div style="width:250px;">' +
            '' +
            '                            <select style="width: auto;" name="Lead Status" tabindex="-1" class="select2-hidden-accessible" aria-hidden="true">' +
            '' +
            '                                <option value="-None-">-None-</option>' +
            '' +
            '                                <option value="Open">Open</option>' +
            '' +
            '                                <option value="Callback">Callback</option>' +
            '' +
            '                                <option value="Contacted">Contacted</option>' +
            '' +
            '                                <option value="CNP">CNP</option>' +
            '' +
            '                                <option value="Not Qualified">Not Qualified</option>' +
            '' +
            '                                <option selected="" value="Not Contacted">Not Contacted</option>' +
            '' +
            '                                <option value="Lost Lead">Lost Lead</option>' +
            '' +
            '                            </select>' +
            '                        </div>' +
            '                    </div>' +
            '                    <div style="display:none;">' +
            '                        <div style="nowrap:nowrap;text-align:left;font-size:12px;font-family:Arial;width:50%">Destination</div>' +
            '                        <div style="width:250px;">' +
            '                            <input style="width:250px;" maxlength="1005" id="LEADCF21" name="LEADCF21" value="' + destination + '">' +
            '                            <input style="width:250px;" maxlength="1005" id="DESTINATION" name="DESTINATION" value="' + destination + '">' +
            '                        </div>' +
            '                    </div>' +
            '                    <div style="display:none;">' +
            '                        <div style="nowrap:nowrap;text-align:left;font-size:12px;font-family:Arial;width:50%">Lead Source</div>' +
            '                        <div style="width:250px;">' +
            '' +
            '                            <select style="width: auto;" name="Lead Source" tabindex="-1" class="select2-hidden-accessible" aria-hidden="true">' +
            '' +
            '                                <option value="-None-">-None-</option>' +
            '' +
            '                                <option selected="" value="Website">Website</option>' +
            '' +
            '                                <option value="Google">Google</option>' +
            '' +
            '                                <option value="Facebook">Facebook</option>' +
            '' +
            '                                <option value="LinkedIn">LinkedIn</option>' +
            '' +
            '                                <option value="Walkin">Walkin</option>' +
            '' +
            '                                <option value="Referral">Referral</option>' +
            '' +
            '                                <option value="Newspaper">Newspaper</option>' +
            '' +
            '                                <option value="Travel&#x20;Agency">Travel Agency</option>' +
            '' +
            '                                <option value="Google&#x20;AdWords">Google AdWords</option>' +
            '' +
            '                            </select>' +
            '                        </div>' +
            '                    </div>' +
            '' +
            '' +
            '' +
            '                    <div style="display:none;">' +
            '                        <div style="nowrap:nowrap;text-align:left;font-size:12px;font-family:Arial;width:50%">Web Source</div>' +
            '                        <div style="width:250px;">' +
            '' +
            '                            <select style="width: auto;" name="LEADCF8" tabindex="-1" class="select2-hidden-accessible" aria-hidden="true">' +
            '' +
            '                                <option value="-None-">-None-</option>' +
            '' +
            '                                <option selected="" value="luxurytravel.deals">luxurytravel.deals</option>' +
            '' +
            '                                <option value="hi.tours">hi.tours</option>' +
            '' +
            '                                <option value="hitours.in">hitours.in</option>' +
            '' +
            '                                <option value="hi-tours.com">hi-tours.com</option>' +
            '' +
            '                                <option value="hi-tours.es">hi-tours.es</option>' +
            '' +
            '                                <option value="hi-tours.de">hi-tours.de</option>' +
            '' +
            '                                <option value="hi-tours.fr">hi-tours.fr</option>' +
            '' +
            '                                <option value="hi-tours.it">hi-tours.it</option>' +
            '' +
            '                                <option value="hi-tours.nl">hi-tours.nl</option>' +
            '' +
            '                                <option value="hi-tours.pt">hi-tours.pt</option>' +
            '' +
            '                                <option value="hi-tours.ru">hi-tours.ru</option>' +
            '' +
            '                                <option value="hi-tours.cn">hi-tours.cn</option>' +
            '' +
            '                                <option value="hi-tours.ae">hi-tours.ae</option>' +
            '' +
            '                            </select>' +
            '                        </div>' +
            '' +
            '                    </div>' +
            '                    <div class="callbtn">' +
            '                        <button type="submit" class="btn btn-default">Notify Me</button>' +
            '                    </div>' +
            '                </form></div>';
    }

    //window.getCurrentDeals = function (lim, off, isViewMore) {
    //    if (searchTerm.length > 0) {
    //        $("#home-recentlyview").hide();
    //        BindDestinationForRequest(searchTerm[0].display);
    //    }
    //    $.ajax({
    //        type: "POST",
    //        url: '/home/currentdeals',
    //        data: { limit: lim, offset: off, searchBy: searchBy, searchTerms: JSON.stringify(searchTerm) },
    //        cache: false,
    //        beforeSend: function () {
    //            $("#loader_message").html("").hide();
    //            $("#search_message").html("").hide();
    //            $('#loader_image').removeClass("display-none").addClass("display-inline-block");
    //            if (isViewMore == true) {
    //                $(".btn-view-more").addClass("display-none");
    //            }
    //        },
    //        success: function (html) {
    //            $("#dCurrentDeals").append(html);
    //            $('#loader_image').removeClass("display-inline-block").addClass("display-none");
    //            if (isViewMore == true) {
    //                $(".btn-view-more").removeClass("display-none");
    //            }
    //            if ($.trim(html).length == 0) {
    //                //$("#search_message").html('<h4> To be notified as soon as any flash sale matches your criteria</h4><br><button class="btn" style="background: #fca32b;color: #FFF;" data-toggle="modal" data-target="#myModalrequestcallback">Request a call back </button> ').show();
    //                if (isSearch) {
    //                    //$("#search_message").html('Sorry, no flash sales match your search criteria.').show()
    //                    //$("#search_message").html('<h4> To be notified as soon as any flash sale matches your criteria</h4><br><button class="btn" style="background: #fca32b;color: #FFF;" data-toggle="modal" data-target="#myModalrequestcallback">Request a call back </button> ').show();
    //                    $("#search_message").html(formRequestCallback).show();
    //                    $("#frmTalkExpertSearch").validate({
    //                        rules: {
    //                            Email: {
    //                                required: true,
    //                                email: true
    //                            }
    //                        }
    //                    });
    //                }
    //                else {
    //                    //$("#loader_message").html('<button class="btn btn-default" type="button">No more records.</button>').show()
    //                }
    //                if (!$(".btn-view-more").hasClass('display-none')) {
    //                    $(".btn-view-more").addClass("display-none").addClass("block-view");
    //                }
    //            }

    //            if ($.fn.imageloader != undefined) {
    //                $('[data-src]').imageloader();
    //            }

    //            $('.deal-countdown').each(function () {
    //                var timmerseconds = $(this).data("seconds") || 0;
    //                $(this).timeTo({
    //                    seconds: parseInt(timmerseconds),
    //                    displayDays: 2,
    //                    fontSize: 16,
    //                    captionSize: 9,
    //                    displayCaptions: true
    //                }, function () {
    //                    swal({
    //                        text: "Some Deals are Expired",
    //                        type: 'warning',
    //                        showCancelButton: false,
    //                        confirmButtonColor: '#3085d6',
    //                        confirmButtonText: 'Ok'
    //                    }).then(function () {
    //                        window.location = "/"
    //                    });
    //                    return false;
    //                });
    //            });

    //            smartech('create', 'ADGMOT35CHFLVDHBJNIG50K969M2CP4N22UB4JPT4SRLRK91B7CG');
    //            smartech('register', '7118711369bb8028c468d27da6066644');
    //            smartech('identify', '');
    //            smartech('dispatch', 27, {
    //                "s^SEARCH_TERM": $("#txtCRC").val(),
    //                "s^SEARCH_DATE": $("#spDepartureDate").html(),
    //                "s^FILTER_VALUE": $("#spFilters").html()
    //            });
    //        }
    //    });
    //}

    function GetFormattedDate(dt) {
        var currentDt = new Date(dt);
        var mm = currentDt.getMonth() + 1;
        mm = (mm < 10) ? '0' + mm : mm;
        var dd = currentDt.getDate();
        dd = (dd < 10) ? '0' + dd : dd;
        var yyyy = currentDt.getFullYear();
        return dd + '/' + mm + '/' + yyyy;
    }
    function getFlexible(d) {
        switch (d) {
            case '0':
                return '';
            case '1':
                return '±1d';
            case '3':
                return '±3d';
            case '7':
                return '±7d';
            case '14':
                return '±14d';
        }
    }
    function b64EncodeUnicode(str) {
        return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g,
            function toSolidBytes(match, p1) {
                return String.fromCharCode('0x' + p1);
            }));
    }
    function b64DecodeUnicode(str) {
        return decodeURIComponent(atob(str).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));
    }
    function removeDuplicates(arr, prop) {
        var obj = {};
        for (var i = 0, len = arr.length; i < len; i++) {
            if (!obj[arr[i][prop]]) obj[arr[i][prop]] = arr[i];
        }
        var newArr = [];
        for (var key in obj) newArr.push(obj[key]);
        return newArr;
    }
    ////function removeDuplicates(arr, key) {
    ////    if (!(arr instanceof Array) || key && typeof key !== 'string') {
    ////        return false;
    ////    }
    ////    if (key && typeof key === 'string') {
    ////        return arr.filter((obj, index, arr) => {
    ////            return arr.map(mapObj => mapObj[key]).indexOf(obj[key]) === index;
    ////        });
    ////    } else {
    ////        return arr.filter(function (item, index, arr) {
    ////            return arr.indexOf(item) == index;
    ////        });
    ////    }
    ////}

    function setRecentSearch() {
        var $dvRecent = $("#dvRecent");
        $dvRecent.html('');
        var filteredData = getRecentDearchLocalSt();
        if (filteredData && filteredData != null) {
            $.each(filteredData, function (n, i) {
                if (n > 2) {
                    return false;
                }
                $dvRecent.append("<div class='destination'><span><i class='fa fa-globe' aria-hidden='true'></i>" + i.display + "</span></div>");
            });

            $divDestination.show();
        }
    }
    function getRecentDearchLocalSt() {
        var data = localStorage.getItem(recentSearchHistory);
        if (data != null && data.length > 0) {
            var recentSearch = b64DecodeUnicode(localStorage.getItem(recentSearchHistory));
            recentSearch = JSON.parse(recentSearch).sort(function (a, b) { return b.id - a.id });
            return removeDuplicates(recentSearch, "display");
        }
    }
    $(function () {
        var newDate = new Date((year || (new Date()).getFullYear()), (month || (new Date()).getMonth()), (day || (new Date()).getDate()));
        var dp = $("#datepicker_west").datepicker({
            format: dateformat,
            startDate: truncateDate(newDate),
        });
        dp.on('changeMonth', function (e) {
            $(".date-anyday").html("Anytime in " + monthNames[e.date.getMonth()]);
            searchYear = e.date.getFullYear();
            searchMonth = e.date.getMonth();
        });
        dp.on("changeDate", function (e) {
            searchDate = GetFormattedDate(e.date);
            var record = { date: $("#datepicker_west").datepicker("getDate"), flexible: $("#drpFlexible").val(), anyday: false, isDepartureCancel: false };
            setSearch(departure, record);
            //works
        });
    })
})();