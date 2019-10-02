$(function () {
    var recentSearchCollection = [];
    $(document).on('click', '.footer-row .country_destinations li,  .country-destination .footer_list li', function (e) {
        debugger;
        var value = $(this).data('value');
        var text = $(this).data('text');
        if (value !== null && value !== '') {
            var data = {
                'startDate': null,
                'endDate': null,
                'adults': 1,
                'kids': 0,
                'infants': 0,
                'rooms': 1,
                'searchTerm': text != null && text != undefined ? text.replace(/ /g, '-') : null,
                'showSearchTerm': true,
                'searchType': 3, //// Enums.SearchType.Country
                'value': value
            };
            RecentSearchCollection(data);
            window.location = BuildSearchUrl(data);
        }
    });
    $(document).on('click', '.slider-popular-destination a, .pd-slick-slider a', function (e) { //Popular Destination
        var value = $(this).data('value');
        var text = $(this).data('text');
        var type = $(this).data('type');
        if (value !== null && value !== '') {
            var data = {
                'startDate': null,
                'endDate': null,
                'adults': 1,
                'kids': 0,
                'infants': 0,
                'rooms': 1,
                'searchTerm': text != null && text != undefined ? text.replace(/ /g, '-') : null,
                'subSearchTerm': $(this).data('subtext') != null && $(this).data('subtext') != undefined ? $(this).data('subtext').replace(/ /g, '-') : null,
                'showSearchTerm': true,
                'searchType': type, //// Enums.SearchType.Country
                'value': value
            };
            RecentSearchCollection(data);
            window.location = BuildSearchUrl(data);
        }
    });

    $(document).on('click', '.country-deals-count li a', function (e) {
        debugger;
        var value = $(this).data('value');
        var text = $(this).data('text');
        if (value !== null && value !== '') {
            var data = {
                'startDate': null,
                'endDate': null,
                'adults': 1,
                'kids': 0,
                'infants': 0,
                'rooms': 1,
                'searchTerm': text != null && text != undefined ? text.replace(/ /g, '-') : null,
                'showSearchTerm': true,
                'searchType': 3, //// Enums.SearchType.Country
                'value': value
            };
            RecentSearchCollection(data);
            window.location = BuildSearchUrl(data);
        }
    });
    $(document).on('click', '.city-deal-counts li a', function (e) { //// Changed to statewise on 18th July 2019
        debugger;
        var value = $(this).data('value');
        var text = $(this).data('text');
        if (value !== null && value !== '') {
            var data = {
                'startDate': null,
                'endDate': null,
                'adults': 1,
                'kids': 0,
                'infants': 0,
                'rooms': 1,
                'searchTerm': text != null && text != undefined ? text.replace(/ /g, '-') : null,
                'showSearchTerm': true,
                'subSearchTerm': $(this).data('subtext') != null && $(this).data('subtext') != undefined ? $(this).data('subtext').replace(/ /g, '-') : null,
                'searchType': 9, //// Enums.SearchType.State
                'value': value
            };
            RecentSearchCollection(data);
            window.location = BuildSearchUrl(data);
        }
    });
    $(document).on('click', '.footer-row #interests_list li', function (e) {

        var value = $(this).data('value');
        var text = $(this).data('text');
        if (value !== null && value !== '') {
            var data = {
                'startDate': null,
                'endDate': null,
                'adults': 1,
                'kids': 0,
                'infants': 0,
                'rooms': 1,
                'searchTerm': text != null && text != undefined ? text.replace(/ /g, '-') : null,
                'showSearchTerm': true,
                'searchType': 5, //// Enums.SearchType.Country
                'value': value
            };
            RecentSearchCollection(data);
            window.location = BuildSearchUrl(data);
        }
    });
    $(document).on('click', '.interests_lists li,.interests_list .footer_list li', function (e) {
        debugger;

        var value = $(this).data('value');
        var text = $(this).data('text');
        if (value !== null && value !== '') {
            var data = {
                'startDate': null,
                'endDate': null,
                'adults': 1,
                'kids': 0,
                'infants': 0,
                'rooms': 1,
                'searchTerm': text != null && text != undefined ? text.replace(/ /g, '-') : null,
                'showSearchTerm': false,
                'searchType': 5, //// Enums.SearchType
                'value': value
            };
            RecentSearchCollection(data);
            window.location = BuildSearchUrl(data);

        }
    });
    function BuildSearchUrl(data) {
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
    function ReturnSearchUrl(data, searchTermRequired, subSearchTermRequired) {
        var url = '/search';
        if (data.subSearchTerm != null && data.subSearchTerm != '' && data.subSearchTerm != undefined && subSearchTermRequired) {
            url = url + '/' + data.subSearchTerm.toLowerCase();
        }

        if (data.searchTerm != null && data.searchTerm != '' && data.searchTerm != undefined && searchTermRequired) {

            url = url + '/' + data.searchTerm.toLowerCase();
        }
        if (!((data.adults == 1 || data.adults == '1') &&
            (data.kids == 0 || data.kids == '0') &&
            (data.infants == 0 || data.infants == '0') &&
            (data.rooms == 1 || data.rooms == '1')) || (data.startDate != null && data.startDate != '' && data.startDate != undefined)) {
            url = url + "/" + data.adults;
            url = url + "/" + data.kids;
            url = url + "/" + data.infants;
            url = url + "/" + data.rooms;

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
        else {
            if (data.startDate != null && data.startDate != '' && data.startDate != undefined) {
                url = url + '/' + data.startDate;
            }
            if (data.endDate != null && data.endDate != '' && data.endDate != undefined) {
                url = url + '/' + data.endDate;
            }
        }

        return url;
    }
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
});