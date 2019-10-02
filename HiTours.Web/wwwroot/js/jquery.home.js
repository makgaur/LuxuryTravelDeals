$(function () {

    setRecentSearch();
    setFlashDealTimer($('#fd_ExpiryDate').val());
    var recentSearchCollection = [];
    $(document).on('click', '.recent-search .recent-search-item', function (e) {
        var display = $(this).data('display');
        var recentSrArray = getRecentDearchLocalSt();
        recentSrArray.filter(function (item, index, arr) {
            if (item.display === display) {
                var data = {
                    'startDate': item.startDate,
                    'endDate': item.endDate,
                    'adults': item.adults,
                    'kids': item.kids,
                    'infants': item.infants,
                    'rooms': item.rooms,
                    'searchTerm': item.display,
                    'subSearchTerm': item.subSearchTerm,
                    'showSearchTerm': true,
                    'searchType': 6, //// Enums.SearchType
                    'value': item.value
                };
                window.location = BuildSearchUrl(data);  
            }
        });
    });

    $(document).on('click', '.search-container.hooked #searchButton,.home-mobile-search-button button', function (e)
    {
        var data =
        {
            'startDate': $('#searchDatepicker').data('startdate'),
            'endDate': $('#searchDatepicker').data('enddate'),
            'adults': $('#addAdults').val(),
            'kids': $('#addChilds').val(),
            'infants': $('#addInfants').val(),
            'rooms': $('#addRoom').val(),
            'searchTerm': $('#searchInput').val() != null && $('#searchInput').val() != undefined ? $('#searchInput').val().toString().replace(/ /g,'-') : null,
            'subSearchTerm': $('#searchInput').data('sub-search-term') != null && $('#searchInput').data('sub-search-term') != undefined ? $('#searchInput').data('sub-search-term').replace(/ /g, '-') : null,
            'showSearchTerm': true,
            'searchType': ($('#searchInput').val().toString() != null && $('#searchInput').val().toString() != undefined && $('#searchInput').val().toString() != '') ? $('#searchInput').data('search-type'):10, //// Enums.SearchType.Query
            'value': $('#searchInput').data('search-value'),
            'kidsage': $('#txtKidsAge').val()
        };
        RecentSearchCollection(data);
        window.location = BuildSearchUrl(data);
    });
    function BuildSearchUrl(data)
    {
        var url = '';
        if (data.searchType == 10) {
            url = ReturnSearchUrl(data, false, false);
        }
        else if (data.searchTerm != '' && data.subSearchTerm != '' && data.searchTerm != null && data.subSearchTerm != null) {
            url = ReturnSearchUrl(data, true, true);
        }
        else {
            url = ReturnSearchUrl(data, true, false);
        }
        return url;
    }
    function ReturnSearchUrl(data, searchTermRequired, subSearchTermRequired)
    {
        var url = '/search';
        if (data.subSearchTerm != null && data.subSearchTerm != '' && data.subSearchTerm != undefined && subSearchTermRequired)
        {
            url = url + '/' + data.subSearchTerm.toLowerCase();
        }

        if (data.searchTerm != null && data.searchTerm != '' && data.searchTerm != undefined && searchTermRequired) {
            
            url = url + '/' + data.searchTerm.toLowerCase();
        }
        if (!((data.adults == 1 || data.adults == '1') &&
            (data.kids == 0 || data.kids == '0') &&
            (data.infants == 0 || data.infants == '0') &&
            (data.rooms == 1 || data.rooms == '1')) || (data.startDate != null && data.startDate != '' && data.startDate != undefined)) {
            url = url + "/"+data.adults;
            url = url + "/"+data.kids;
            url = url +"/"+ data.infants;
            url = url + "/"+data.rooms;
            
            if (data.startDate != null && data.startDate != '' && data.startDate != undefined) {
                url = url + '/' + data.startDate;
            }
            if (data.endDate != null && data.endDate != '' && data.endDate != undefined) {
                url = url + '/' + data.endDate;
            }
            if (data.kidsage != null && data.kidsage != '' && data.kidsage != undefined) {
                url = url + '/' + data.kidsage;
            }
            
        }
        else
        {
            if (data.startDate != null && data.startDate != '' && data.startDate != undefined) {
                url = url + '/' + data.startDate;
            }
            if (data.endDate != null && data.endDate != '' && data.endDate != undefined) {
                url = url + '/' + data.endDate;
            }
        }

        return url;
    }
    if (!checkForMobileView()) {
        $(document).bind('keypress', function (e) {
            if ($('.search-container.hooked').length > 0 && e.which == 13) {
                $('#searchButton').click();
            }
        });
       //$(document).on('keypress', '#searchInput', function (e) {
           
       //    if (e.which == 13) {
       //        debugger;
       //         $('.search-container.hooked #searchButton').click();
       //     }
       // });
    }

    $.typeahead({
        input: "#searchInput",
        minLength: 3,
        hint: true,
        cancelButton: false,
        maxItem: 8,
        group: true,
        highlight: true,
        dynamic: true,
        maxItemPerGroup: 2,
        order: "asc",
        emptyTemplate: "No result for {{query}}",
        source: {
            country: {
                display: "Item1",
                template:
                    ' <div class="{{group}}" id="{{Item2}}"  data-type="2"><div class="img-cls"><img src="/images/TypeLocation.svg"/></div>{{Item1}}</div>',
                ajax: {
                    url: "/selectlist/filtercountries",
                    data: { search: '{{query}}' },
                    success: function (result) {
                    },
                }
            },
            city: {
                display: "Item1",
                template: '<div class="{{group}}" id="{{Item2}}" data-type="1"><div class="img-cls"><img src="/images/TypeLocation.svg"/></div>{{Item1}}</div>',
                ajax: {
                    url: "/selectlist/filtercity",
                    data: { search: '{{query}}' },
                    success: function (result) {
                    },
                }
            },
            hotel: {
                template: '<div class="{{group}}" id="{{Item2}}" data-type="3"><div class="img-cls"><img src="/images/TypeHotel.svg"/></div>{{Item1}}</div>',
                display: "Item1",
                ajax: {
                    url: "/selectlist/filterhotel",
                    data: { search: '{{query}}' },
                    success: function (result) {
                    },
                },

            },
            product: {
                display: "Item1",
                template: '<div class="{{group}}" id="{{Item2}}" data-type="4"><div class="img-cls"><img src="/images/TypeProduct.svg"/></div>{{Item1}}</div>',
                ajax: {
                    url: "/selectlist/filterproduct",
                    data: { search: '{{query}}' },
                    success: function (result) {
                    },
                }
            }
        },
        callback: {
            onClickAfter(node, a, item, event) {
                debugger;
                $('#searchInput').data('search-type', item.Item3);
                $('#searchInput').data('search-value', item.Item2);
                if (item.Item4)
                {
                    $('#searchInput').data('sub-search-term', item.Item4 != null ? item.Item4.replace(/ /g, "-"): null);
                }
                if (item.Item5)
                {
                    $('#searchInput').val(item.Item5);
                }
                $('#searchInput').blur();
                
            },
            onSearch(node, query) {
                $('#searchInput').data('search-type', 6); //Enums.SearchType.Query
                $('#searchInput').data('search-value', 0);
                $('#searchInput').data('sub-search-term', null);
            },
            onCancel(node, event) {
                $('#searchInput').data('search-type', 6); //Enums.SearchType.Query
                $('#searchInput').data('search-value', 0);
                $('#searchInput').data('sub-search-term', null);
                //$('#searchButton').removeClass('hidden');
            },
            onSendRequest(node, query) {
                $('#searchButton').addClass('hidden');
            },
            onResult(node, query, result, resultCount, resultCountPerGroup) {
                $('#searchButton').removeClass('hidden');
            }
        },
        debug: true
    });



    $(document).on('click', '.travel-style-model-body .travel-style, .filters .travel-style', function (e) {
        var data = {
            'startDate': null,
            'endDate': null,
            'adults': $('#addAdults').val(),
            'kids': $('#addKids').val(),
            'infants': $('#addInfants').val(),
            'rooms': $('#addRoom').val(),
            'searchTerm': $(this).data('name') != null && $(this).data('name') != undefined ? $(this).data('name').replace(/ /g, '-'): null,
            'subSearchTerm': null,
            'showSearchTerm': false,
            'searchType': 5, //// Enums.SearchType.TravelStyle
            'value': $(this).data('value')
        };
        RecentSearchCollection(data);
        window.location = BuildSearchUrl(data);
    });
    $('.flash-deal-container .show-more-home').on('click', function (e) {
        var data = {
            'startDate': null,
            'endDate': null,
            'adults': $('#addAdults').val(),
            'kids': $('#addKids').val(),
            'infants': $('#addInfants').val(),
            'rooms': $('#addRoom').val(),
            'searchTerm': $(this).data('search-term') != null && $(this).data('search-term') != undefined ? $(this).data('search-term').replace(/ /g, '-'): null,
            'subSearchTerm': null,
            'showSearchTerm': false,
            'searchType': $(this).data('search-type'),
            'value': 0
        };
        RecentSearchCollection(data);
        window.location = BuildSearchUrl(data);
    });
    $('.city-deal-container .show-more-home').on('click', function (e) {
        var data = {
            'startDate': null,
            'endDate': null,
            'adults': $('#addAdults').val(),
            'kids': $('#addKids').val(),
            'infants': $('#addInfants').val(),
            'rooms': $('#addRoom').val(),
            'searchTerm': $(this).data('search-term') != null && $(this).data('search-term') != undefined ? $(this).data('search-term').replace(/ /g, '-'):null,
            'subSearchTerm': $(this).data('sub-search-term') != null && $(this).data('sub-search-term') != undefined ? $(this).data('sub-search-term').replace(/ /g, '-'):null,
            'showSearchTerm': false,
            'searchType': $(this).data('search-type'),
            'value': $(this).data('city-id')
        };
        RecentSearchCollection(data);
        window.location = BuildSearchUrl(data);
    });
    function RecentSearchCollection(data) {
        if (data.searchTerm != '' && data.searchTerm != null) {
            var previousSearch = localStorage.getItem(recentSearchHistory);
            if (previousSearch != null && previousSearch.length > 0) {
                var prev = b64DecodeUnicode(previousSearch);
                recentSearchCollection = JSON.parse(prev);
            }
            recentSearchCollection.push({
                "display": data.searchTerm,
                "id": recentSearchCollection.length,
                "startDate": data.startDate,
                "endDate": data.endDate,
                'adults': data.adults,
                'kids': data.kids,
                'infants': data.infants,
                'rooms': data.rooms,
                'searchType': data.searchType,
                'searchTerm': data.searchTerm,
                'showSearchTerm': data.showSearchTerm,
                'subSearchTerm': data.subSearchTerm,
                'value': data.value
            });
            localStorage.setItem(recentSearchHistory, b64EncodeUnicode(JSON.stringify(recentSearchCollection)));
        }
    }
    function setRecentSearch() {
        var $dvRecent = $('.recent-search');
        $dvRecent.html('');
        
        var filteredData = getRecentDearchLocalSt();
        if (filteredData && filteredData != null) {
            $.each(filteredData, function (n, i) {
                if (n > 7) {
                    return false;
                }
                $dvRecent.append('<div class="recent-search-item" data-display="' + i.display + '"><div class="recent-item-content"><img src="/images/Recent_search_Icon.svg" alt="recent_icon" /><span>' + i.display + '</span></div></div>');
            });

            //$divDestination.show();
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
    function removeDuplicates(arr, prop) {
        var obj = {};
        for (var i = 0, len = arr.length; i < len; i++) {
            if (!obj[arr[i][prop]]) obj[arr[i][prop]] = arr[i];
        }
        var newArr = [];
        for (var key in obj) newArr.push(obj[key]);
        return newArr;
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
    function setFlashDealTimer(date) {
        // Set the date we're counting down to
        //var countDownDate = new Date("March 7, 2019 16:06:25").getTime();
        var countDownDate = new Date("April 16, 2019 12:00:00");
        for (var i = 0; ; i++) {
            countDownDate.setDate(countDownDate.getDate() + 3);
            if (countDownDate >= new Date()) {
                break;
            }
        }
        countDownDate = countDownDate.getTime();

        // Update the count down every 1 second
        var x = setInterval(function () {

            // Get todays date and time
            var now = new Date().getTime();

            // Find the distance between now and the count down date
            var distance = countDownDate - now;

            // Time calculations for days, hours, minutes and seconds
            //var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.floor((distance / (1000 * 60 * 60 * 24)) * 24).toLocaleString('en-IN', { minimumIntegerDigits: 2, useGrouping: false });
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60)).toLocaleString('en-IN', { minimumIntegerDigits: 2, useGrouping: false });
            var seconds = Math.floor((distance % (1000 * 60)) / 1000).toLocaleString('en-IN', { minimumIntegerDigits: 2, useGrouping: false });

            // Output the result in an element with id="demo"
            $('.fd_Timer span').html(hours + ":"+ minutes + ":" + seconds + " Left");

            // If the count down is over, write some text 
            if (distance < 0) {
                clearInterval(x);
                $('.fd_Timer span').html("EXPIRED");
            }
        }, 1000);
    }   

    function ValidateAge() {
        for (var i = 1; i <= $('#addKids').val(); i++) {
            if ($("#ddlKidsAge" + i).val() == "0") {
                $("#errormessage").html('Please select age for Kid ' + i + '.');
                $("#errormessage").removeClass("hidden");
                return false;
            }
        }
        
        $("#errormessage").addClass("hidden");
        $("#errormessage").html('');
        return true;
    }

    $(document).on('click', '#closeMobileSearch', function (e) {
        $("#search_container").removeClass("hooked");
        $(".searchInputDestination").removeClass("hooked_icon_image");
        $('.carousel-caption').removeClass('pull-up');
        $('.search-panel').removeClass('pull-up');
        $(".searchInputDestination").css("padding-left", "40px");
        if (checkForMobileView()) {
            $('.curation-container').css({ 'display': 'block' });
            $('.flash-deal-container').css({ 'display': 'block' });
            $('.popular-destination').css({ 'display': 'block' });
            $('.image-article').css({ 'display': 'block' });
            $('.featured-logo-mobile-container').css({ 'display': 'block' });
            $('footer').css({ 'display': 'block' });
            $('.city-deal-container').css({ 'display': 'block' });
            $('.filters').css({ 'display': 'block' });
            $(".searchInputDestination").removeClass('stop-blur');
        }
    });
    $("#close_search").click(function () {
        $("#search_container").removeClass("hooked");
        //$(".searchInputDestination").addClass("hooked_icon_image");
        $(".searchInputDestination").removeClass("hooked_icon_image");
        $(".nav_search").css("display", "none");
        $("#divSearchExtend").css("display", "none");
        $(".search-panel").css({ "width": "95%", "margin": "0 10px" });
    });
    $(document).ready(function (e) {
        $('input').blur();
        $('#searchInput').attr("placeholder", $('#carouselFade .item.active').data('placeholder'));
        $('#carouselFade').bind('slide.bs.carousel', function (e) {
            $('#searchInput').attr("placeholder", $(e.relatedTarget).data('placeholder'));
        })
        
        if ($('#addChilds').val() != undefined && $('#addChilds').val() != '' && $('#addInfants').val() != undefined && $('#addInfants').val() != '') {
            $('#addKids').val(parseInt($('#addChilds').val()) + parseInt($('#addInfants').val()));
            for (var i = 1; i <= parseInt($('#addKids').val()); i++) {
                $('#li_Infant').removeClass('hidden');
                var selectedvalue = $('#txtKidsAge').val().split('#');
                addkidsddl(i, selectedvalue[i]);
            }
        }
        var $roomBreakDownContainer = $('.filtersearch.room-filter');
        $("#btnApply").on('click', function (e) {
            if (ValidateAge()) {
                ChildInfantCalculate();
                if ($roomBreakDownContainer.hasClass('open')) {
                    $roomBreakDownContainer.removeClass('open');
                    return;
                }
            }
        });
        
        UpdateSteppersString();

        //Dropdown close click on outside searchbox
        if (!checkForMobileView()) {
            $(document).on("click", function (event) {
                var $searchContainer = $(".search-container");
                var $searchTrigger = $('.searchInputDestination');
                var $calendarContainer = $(".filtersearch-calender");
                var $calendarTrigger = $("#searchDatepicker");
                var $roomBreakDownTrigger = $('#stepperString');
                
                if (event.target.id == $searchTrigger.attr('id')) {
                    var animateup = $('.carousel-inner').height() - 265;
                    var animateupheight = animateup / 2;
                    var captionanimateup = animateupheight - 74;
                    $('.search-panel').animate({
                        top: animateupheight
                    }, "300");

                    $('.carousel-caption').animate({
                        top: captionanimateup
                    }, "300");
                    $("#search_container").addClass("hooked");
                    $searchTrigger.addClass("hooked_icon_image");
                    $('.carousel-caption').addClass('pull-up');
                    $('.search-panel').addClass('pull-up');
                    $searchTrigger.css("padding-left", "15px");

                    return;
                }

                if ($searchContainer.hasClass('hooked')) {
                    if (event.target.id == $roomBreakDownTrigger.attr('id') || event.target.id == $('#triggerPassSelect').attr('id')) {
                        if ($calendarTrigger.parent('.filtersearch').hasClass('calender-open')) {
                            $calendarTrigger.parent('.filtersearch').removeClass('calender-open');
                            //return;
                        }

                        $roomBreakDownContainer.toggleClass('open');

                        return;
                    }
                    if (event.target.id == $calendarTrigger.attr('id') || event.target.id == $('#calendarTriggerIcon').attr('id')) {
                        if ($roomBreakDownTrigger.parent('.filtersearch').hasClass('open')) {
                            $roomBreakDownTrigger.parent('.filtersearch').removeClass('open');
                            //return;
                        }
                        if ($calendarTrigger.parent('.filtersearch').hasClass('calender-open')) {
                            $calendarTrigger.parent('.filtersearch').removeClass('calender-open');

                            return;
                        }
                        else {
                            $calendarTrigger.parent('.filtersearch').addClass('calender-open');
                            if (!$calendarTrigger.data('scrolled')) {
                                $('.calendar').animate({
                                    scrollLeft: 288
                                }, "fast");
                                $calendarTrigger.data('scrolled', true);
                            }
                            return;
                        }
                    }
                    if ($calendarContainer.hasClass('calender-open')) {
                        $calendarContainer.removeClass('calender-open');
                        $('.destinationSearchBar').removeClass('hidden');
                        return;
                    }
                    
                    if ($("#search_container").hasClass("hooked") && $searchContainer.has(event.target).length === 0) {

                        $("#search_container").removeClass("hooked");
                        $(".searchInputDestination").removeClass("hooked_icon_image");
                        $('.carousel-caption').removeClass('pull-up');
                        $('.search-panel').removeClass('pull-up');
                        $(".searchInputDestination").css("padding-left", "40px");
                        if (!checkForMobileView()) {
                            var animatedown = $('.carousel-inner').height() - 50;
                            var animatedownheight = animatedown / 2;
                            var captionanimatedown = animatedownheight - 74;
                            $('.search-panel').animate({
                                top: animatedownheight
                            }, "300");

                            $('.carousel-caption').animate({
                                top: captionanimatedown
                            }, "300");
                        }

                    }
                }
                else {
                    return;
                }
            });
        }

        if (checkForMobileView()) {
            $('.searchInputDestination').on('click', function (e) {
                $('.curation-container').css({ 'display': 'none' });
                $('.flash-deal-container').css({ 'display': 'none' });
                $('.popular-destination').css({ 'display': 'none' });
                $('.image-article').css({ 'display': 'none' });
                $('.featured-logo-mobile-container').css({ 'display': 'none' });
                $('footer').css({ 'display': 'none' });
                $('.city-deal-container').css({ 'display': 'none' });
                $('.filters').css({ 'display': 'none' });
                if (!$('.searchInputDestination').hasClass('stop-blur')) {
                    $('.searchInputDestination').addClass('stop-blur');
                    $('.searchInputDestination').blur();
                }
                $('html, body').animate({
                    scrollTop: 0
                }, 1000);

            });
            //Calendar functionality for Mobile View
            $("#searchDatepicker").on('click', function (e) {
                $(".guest_room_selector").css({ "display": "none" });
                $(".recent_deals").css({ "display": "none" });
                $("#searchDatepicker").parent('.filtersearch').addClass('calender-open');
                $('.destinationSearchBar').addClass('hidden');

            });
            $('.calendar-xs-head .back-nav').on('click', function (e) {
                $(".guest_room_selector").css({ "display": "block" });
                $(".recent_deals").css({ "display": "block" });
                $("#searchDatepicker").parent('.filtersearch').removeClass('calender-open');
                $('.destinationSearchBar').removeClass('hidden');
            });
        }

        //Dropdown close click on outside searchbox

        //Dropdown on hover start
        $('.dropdown-list-icon').hover(function () {
            $(this).find('.dropdown-deal-custom-style').stop(true, true).delay(200).fadeIn(500);
        }, function () {
            $(this).find('.dropdown-deal-custom-style').stop(true, true).delay(200).fadeOut(500);
        });

        $('.dropdown-list-icon').hover(function () {
            $(this).find('.dropdown-recent-view-custom-style').stop(true, true).delay(200).fadeIn(500);
        }, function () {
            $(this).find('.dropdown-recent-view-custom-style').stop(true, true).delay(200).fadeOut(500);
        });
        //Dropdown on hover end


        if (checkForMobileView()) {
            $(".searchInputDestination").on('click', function () {
                if ($("#search_container").hasClass("hooked")) {
                    if (!checkForMobileView()) {
                        $("#search_container").removeClass("hooked");
                        $(".searchInputDestination").removeClass("hooked_icon_image");
                        $('.carousel-caption').removeClass('pull-up');
                        $('.search-panel').removeClass('pull-up');
                        $(".searchInputDestination").css("padding-left", "40px");
                    }
                }
                else {
                    if (!checkForMobileView()) {
                        var animateup = $('.carousel-inner').height() - 265;
                        var animateupheight = animateup / 2;
                        var captionanimateup = animateupheight - 74;
                        $('.search-panel').animate({
                            top: animateupheight
                        }, "300");

                        $('.carousel-caption').animate({
                            top: captionanimateup
                        }, "300");
                    }


                    $("#search_container").addClass("hooked");
                    $(".searchInputDestination").addClass("hooked_icon_image");
                    $('.carousel-caption').addClass('pull-up');
                    $('.search-panel').addClass('pull-up');
                    $(".searchInputDestination").css("padding-left", "15px");

                }
            });
        }




        $("#internationaldealMobile").click(function () {

            $(".internationaldeal-xs").css("display", "block");

            $('.curation-container').css({ 'display': 'none' });
            $('.flash-deal-container').css({ 'display': 'none' });
            $('.popular-destination').css({ 'display': 'none' });
            $('.image-article').css({ 'display': 'none' });
            $('.featured-logo-mobile-container').css({ 'display': 'none' });
            $('footer').css({ 'display': 'none' });
            $('.city-deal-container').css({ 'display': 'none' });
            $('.filters').css({ 'display': 'none' });

        });
        $("#mobile-nav-deal-close").click(function () {

            $(".internationaldeal-xs").css("display", "none");

            $('.curation-container').css({ 'display': 'block' });
            $('.flash-deal-container').css({ 'display': 'block' });
            $('.popular-destination').css({ 'display': 'block' });
            $('.image-article').css({ 'display': 'block' });
            $('.featured-logo-mobile-container').css({ 'display': 'block' });
            $('footer').css({ 'display': 'block' });
            $('.city-deal-container').css({ 'display': 'block' });
            $('.filters').css({ 'display': 'block' });

        });

        $("#dealIndiaMobile").click(function () {

            $(".india-deal-xs").css("display", "block");

            $('.curation-container').css({ 'display': 'none' });
            $('.flash-deal-container').css({ 'display': 'none' });
            $('.popular-destination').css({ 'display': 'none' });
            $('.image-article').css({ 'display': 'none' });
            $('.featured-logo-mobile-container').css({ 'display': 'none' });
            $('footer').css({ 'display': 'none' });
            $('.city-deal-container').css({ 'display': 'none' });
            $('.filters').css({ 'display': 'none' });

        });
        $("#mobile-nav-deal-india-close").click(function () {

            $(".india-deal-xs").css("display", "none");

            $('.curation-container').css({ 'display': 'block' });
            $('.flash-deal-container').css({ 'display': 'block' });
            $('.popular-destination').css({ 'display': 'block' });
            $('.image-article').css({ 'display': 'block' });
            $('.featured-logo-mobile-container').css({ 'display': 'block' });
            $('footer').css({ 'display': 'block' });
            $('.city-deal-container').css({ 'display': 'block' });
            $('.filters').css({ 'display': 'block' });

        });


        $("#recentViewMobile").click(function () {

            $(".dropdown-recent-view-custom-style").css("display", "block");

            $('.curation-container').css({ 'display': 'none' });
            $('.flash-deal-container').css({ 'display': 'none' });
            $('.popular-destination').css({ 'display': 'none' });
            $('.image-article').css({ 'display': 'none' });
            $('.featured-logo-mobile-container').css({ 'display': 'none' });
            $('footer').css({ 'display': 'none' });
            $('.city-deal-container').css({ 'display': 'none' });
            $('.filters').css({ 'display': 'none' });
        });
        $("#mobile-nav-recentview-close").click(function () {

            $(".dropdown-recent-view-custom-style").css("display", "none");

            $('.curation-container').css({ 'display': 'block' });
            $('.flash-deal-container').css({ 'display': 'block' });
            $('.popular-destination').css({ 'display': 'block' });
            $('.image-article').css({ 'display': 'block' });
            $('.featured-logo-mobile-container').css({ 'display': 'block' });
            $('footer').css({ 'display': 'block' });
            $('.city-deal-container').css({ 'display': 'block' });
            $('.filters').css({ 'display': 'block' });
        });
        $('.carousel-slick').slick({
            centerMode: true,
            dots: true,
            infinite: true,
            arrows: false,
            centerPadding: '14.5px'
        });

        $('.pd-slick-slider').slick({
            centerMode: true,
            dots: false,
            infinite: true,
            arrows: false,
            centerPadding: '22.5px'
        });
        $('.slider-popular-destination').slick({
            infinite: true,
            slidesToShow: 4,
            slidesToScroll: 4,
            prevArrow: $('.popular-destination-left'),
            nextArrow: $('.popular-destination-right'),
        });



        $('.featured_logo_slider').slick({
            dots: true,
            arrows: false,

        });
    });
    $(document).on('click', 'a[data-slide="prev"]', function () {

    });
    $(document).on('click', 'a[data-slide="next"]', function () {

    });

    function ChildInfantCalculate() {        
        var Child = 0, Infants = 0;
        var KidsAge = '';        
        for (var i = 1; i <= $('#addKids').val(); i++) {

            if ($("#ddlKidsAge" + i + " option:selected").html() > 1) {
                Child++;
            }
            else {
                Infants++;
            }
            KidsAge = KidsAge + "#" + $("#ddlKidsAge" + i).val();
        }

        $('#txtKidsAge').val(KidsAge);
        $('#addChilds').val(Child);
        $('#addInfants').val(Infants);
    }
    function addkidsddl(currentkid, selectedvalue) {        
        var likidage = document.getElementById('likidage');
        var kids_html = "<div class='col-lg-6 col-md-6  col-sm-6 col-xs-6'><div class='row'><div class='box-description'><p>Kid " + currentkid + "</p></div></div><div class='row'><div class='box-value'><div class='select-dropdown' id='ddl_kids" + currentkid + "'></div></div></div></div>";
        likidage.insertAdjacentHTML('beforeend', kids_html);

        var kids_ddl = "<div class='ltd-select' ><select id='ddlKidsAge" + currentkid + "' select2-stop-render='true' class='scrollbar-style'><option value='0'>Select Age</option></select></div>";
        var ddl_kids = document.getElementById('ddl_kids' + currentkid);
        ddl_kids.insertAdjacentHTML('beforeend', kids_ddl);

        var ddlkids = document.getElementById('ddlKidsAge' + currentkid);
        for (var i = 0; i <= 12; i++) {
            ddlkids.options[i + 1] = new Option(i, i + 1);
        }

        if (selectedvalue != undefined && selectedvalue != '') {
            $('#ddlKidsAge' + currentkid).val(selectedvalue);
        }
        
        custom_dropdown();        
    }

    function removekidsddl(currentkid) {        
        $('#ddl_kids' + currentkid).parents().eq(2).remove();        
    }
    

    
    $(document).on("click", ".increase", function (e) {        
        e.stopPropagation();
        var target = $(this).data('target')
        var $target = $(target);
        var minVal = $target.data('min');
        var currentVal = $target.val();

        $target.val(parseInt(currentVal) + 1);
        if ($target.val() > minVal) {
            $(this).siblings('.decrease').removeClass('disabled');
        }
        if (target == "#addKids") {
            if ($target.val() > minVal) {
                $('#li_Infant').removeClass('hidden');
            }
            addkidsddl((parseInt(currentVal) + 1), '');
        }            
        UpdateSteppersString();
    });
    $(document).on("click", ".decrease", function (e) {
        e.stopPropagation();
        var target = $(this).data('target');
        var $target = $(target);
        var minVal = $target.data('min');
        var currentVal = $target.val();
        if (currentVal == minVal) {

        }
        else {
            $target.val(parseInt(currentVal) - 1);
            if (target == "#addKids") {
                if ($target.val() == minVal) {
                    $("#errormessage").addClass("hidden");
                    $("#errormessage").html('');
                    $('#li_Infant').addClass('hidden');
                }
                removekidsddl(parseInt(currentVal));
            }            
            UpdateSteppersString();
            if ($target.val() == minVal) {
                $(this).addClass('disabled');
            }
        }

    });

    function UpdateSteppersString() {
        var adults = parseInt($('#addAdults').val());
        var kids = parseInt($('#addKids').val());
        //var infants = parseInt($('#addInfants').val());
        var rooms = parseInt($('#addRoom').val());
        var totalGuests = adults + kids;
        $('#stepperString').val(totalGuests + " Guests / " + rooms + " Rooms");
    }
});
