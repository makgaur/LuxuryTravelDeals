var limit = 6;
var offset = 0;
var searchRequirements =
{
    Adults: 0,
    Kids: 0,
    Infants: 0,
    Rooms: 0,
    SearchTerm: '',
    StartDate: null,
    EndDate: null,
    IsFlashDeal: false
};
UpdateSearchVariables();
    //getCurrentDeals(limit, offset);
//window.getCurrentDeals = function (lim, off, isViewMore) {
//    UpdateSearchVariables();
//    $.ajax({
//        type: "POST",
//        url: '/home/searchdeals',
//        data: { limit: lim, offset: off, searchTerms: JSON.stringify(searchRequirements) },
//        cache: false,
//        beforeSend: function () {
//            $(".loader").css({'display':'block'});
//            //$("#loader_message").html("").hide();
//            //$("#search_message").html("").hide();
//            //$('#loader_image').removeClass("display-none").addClass("display-inline-block");
//            //if (isViewMore == true) {
//            //    $(".btn-view-more").addClass("display-none");
//            //}
//        },
//        success: function (html) {
//            $('.listing-header').html('<span class="search-deal-city">' + searchRequirements.SearchTerm + ' </span><span class="search-deal-quantity"> 25 Deals Found</span>')
//            $(".loader").css({ 'display': 'none' });
//            $(".result-deals-container").append(html);
//            //$('#loader_image').removeClass("display-inline-block").addClass("display-none");
//            //if (isViewMore == true) {
//            //    $(".btn-view-more").removeClass("display-none");
//            //}
//            //if ($.trim(html).length == 0) {
//            //    //$("#search_message").html('<h4> To be notified as soon as any flash sale matches your criteria</h4><br><button class="btn" style="background: #fca32b;color: #FFF;" data-toggle="modal" data-target="#myModalrequestcallback">Request a call back </button> ').show();
//            //    if (isSearch) {
//            //        //$("#search_message").html('Sorry, no flash sales match your search criteria.').show()
//            //        //$("#search_message").html('<h4> To be notified as soon as any flash sale matches your criteria</h4><br><button class="btn" style="background: #fca32b;color: #FFF;" data-toggle="modal" data-target="#myModalrequestcallback">Request a call back </button> ').show();
//            //        $("#search_message").html(formRequestCallback).show();
//            //        $("#frmTalkExpertSearch").validate({
//            //            rules: {
//            //                Email: {
//            //                    required: true,
//            //                    email: true
//            //                }
//            //            }
//            //        });
//            //    }
//            //    else {
//            //        //$("#loader_message").html('<button class="btn btn-default" type="button">No more records.</button>').show()
//            //    }
//            //    if (!$(".btn-view-more").hasClass('display-none')) {
//            //        $(".btn-view-more").addClass("display-none").addClass("block-view");
//            //    }
//            //}

//            //if ($.fn.imageloader != undefined) {
//            //    $('[data-src]').imageloader();
//            //}

//            //$('.deal-countdown').each(function () {
//            //    var timmerseconds = $(this).data("seconds") || 0;
//            //    $(this).timeTo({
//            //        seconds: parseInt(timmerseconds),
//            //        displayDays: 2,
//            //        fontSize: 16,
//            //        captionSize: 9,
//            //        displayCaptions: true
//            //    }, function () {
//            //        swal({
//            //            text: "Some Deals are Expired",
//            //            type: 'warning',
//            //            showCancelButton: false,
//            //            confirmButtonColor: '#3085d6',
//            //            confirmButtonText: 'Ok'
//            //        }).then(function () {
//            //            window.location = "/"
//            //        });
//            //        return false;
//            //    });
//            //});

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
//};
function UpdateSearchVariables()
{
    searchRequirements.Adults = $('#addAdults').val();
    searchRequirements.Kids = $('#addKids').val();
    searchRequirements.Infants = $('#addInfants').val();
    searchRequirements.Rooms = $('#addRoom').val();
    searchRequirements.SearchTerm = $('#searchTerm').val();
    searchRequirements.StartDate = $('.checkin_input').data('date');
    searchRequirements.EndDate = $('.checkout_input').data('date');
}
// apply-sort-in-mobile-view

$("#sort-filter-mobile").click(function () {
    $(".sort-xs-container").css("display", "block");
    $(".shadow_box").show();
});

$("#sort-xs-close").click(function () {
    $(".sort-xs-container").css("display", "none");
    $(".shadow_box").hide();
});


// apply-filter-in-mobile-view

$("#apply-filter-mobile").click(function (e) {
    $(".search-filter").css("display", "block");
    $(".search-deals-container").css("display", "none");
    $("footer").css("display", "none");
    $(".footer_bottom").css("display", "none");
});
$("#apply-xs-close").click(function (e) {
    $(".search-filter").css("display", "none");
    $(".search-deals-container").css("display", "block");
    $("footer").css("display", "block");
    $(".footer_bottom").css("display", "block");
}); 

//open-searchbox-in-mobile-view

$("#openSearchBox").click(function () {
    $(".deal-search-container").show();
    $(".search-deals-container").hide();
    $("footer").hide();
    $(".footer_bottom").hide();
    $(".search-deal-xs").hide();

});

$("#closeSearchBox").click(function () {
    $(".deal-search-container").hide();
    $(".search-deals-container").show();
    $("footer").show();
    $(".search-deal-xs").show();

});
$.typeahead({
    input: "#searchTerm",
    minLength: 3,
    hint: true,
    maxItem: 8,
    group: true,
    cancelButton: false,
    highlight: true,
    dynamic: true,
    maxItemPerGroup: 2,
    order: "asc",
    emptyTemplate: "No result for {{query}}",
    source: {
        country: {
            display: "Item1",
            template:
                ' <div class="{{group}} res" id="{{Item2}}"  data-type="2"><div class="img-cls"><img src="/images/TypeLocation.svg"/></div>{{Item1}}</div>',
            ajax: {
                url: "/selectlist/filtercountries",
                data: { search: '{{query}}' },
                success: function (result) {
                }
            }
        },
        city: {
            display: "Item1",
            template: '<div class="{{group}} res" id="{{Item2}}" data-type="1"><div class="img-cls"><img src="/images/TypeLocation.svg"/></div>{{Item1}}</div>',
            ajax: {
                url: "/selectlist/filtercity",
                data: { search: '{{query}}' },
                success: function (result) {
                }
            }
        },
        hotel: {
            template: '<div class="{{group}} res" id="{{Item2}}" data-type="3"><div class="img-cls"><img src="/images/TypeHotel.svg"/></div>{{Item1}}</div>',
            display: "Item1",
            ajax: {
                url: "/selectlist/filterhotel",
                data: { search: '{{query}}' },
                success: function (result) {
                }
            }

        },
        product: {
            display: "Item1",
            template: '<div class="{{group}} res" id="{{Item2}}" data-type="4"><div class="img-cls"><img src="/images/TypeProduct.svg"/></div>{{Item1}}</div>',
            ajax: {
                url: "/selectlist/filterproduct",
                data: { search: '{{query}}' },
                success: function (result) {
                }
            }
        }
    },
    callback: {
        onClickAfter(node, a, item, event) {
            $('#searchTerm').data('search-type', item.Item3);
            $('#searchTerm').data('search-value', item.Item2);
            if (item.Item4) {
                $('#searchTerm').data('sub-search-term', item.Item4);
            }
            if (item.Item5) {
                $('#searchTerm').val(item.Item5);
            }
        },
        onCancel(node, event) {
            $('#searchTerm').data('search-type', 6); //Enums.SearchType.Query
            $('#searchTerm').data('search-value', 0);
            $('#searchTerm').data('sub-search-term', null);
        },
        onSearch(node, query) {
            $('#searchTerm').data('search-type', 6); //Enums.SearchType.Query
            $('#searchTerm').data('search-value', 0);
            $('#searchTerm').data('sub-search-term', null);
        },
        onSendRequest(node, query) {
            $('.search-img').addClass('hidden');
        },
        onResult(node, query, result, resultCount, resultCountPerGroup) {
            $('.search-img').removeClass('hidden');
        }
    },
    debug: true
});