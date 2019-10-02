(function upcommingDeals() {
    this.oRecentDeals = [];
    this.getRecentlyView = function () {
        
        var oData = localStorage.getItem(constdealName);
        return oData !== null ? $.unique(JSON.parse(oData)) : [];
    };

    this.addRecentlyViewDeal = function (packageid) {
        if (packageid != undefined) {
            var oData = this.getRecentlyView() || [];
            oData.push(packageid);
            localStorage.setItem(constdealName, JSON.stringify(oData));
        }
    };

    this.init = (function () {
        var packageIds = this.getRecentlyView();
        if (packageIds.length > 0 && $("#home-recentlyview").length) {
            $.ajax({
                url: '/home/recentlyview',
                method: "POST",
                data: { dealsId: packageIds },
                success: function (data) {
                    $("#home-recentlyview").html(data);
                    $("#home-recentlyview").hide();
                    if ($.fn.initSlickSlider != undefined) {
                        initSlickSlider();
                    }
                    else {
                        //$('.hotel-text').hide();
                        setTimeout(function () { initSlickSlider(); }, 1500);
                        setTimeout(function () { $("#home-recentlyview").show(); }, 5000);
                    }
                }
            })
        }
        $(document).on("click", ".availability", function () {
            var dealUrl = window.location.origin + $(this).attr("href");
            var dealPrice = $(this).data("dealprice");
            var dealEndDate = $(this).data("dealenddate");
            var dealName = $(this).data("dealname");
            var packageId = $(this).data("dealid");
            var dealimg = location.origin + $(this).find('img').attr('src')
            if (packageId !== null && packageId != '') {
                addRecentlyViewDeal(packageId);
            }
            if (dealPrice != undefined && dealEndDate != undefined && dealName != undefined) {
                smartech('create', 'ADGMOT35CHFLVDHBJNIG50K969M2CP4N22UB4JPT4SRLRK91B7CG');
                smartech('register', '7118711369bb8028c468d27da6066644');
                smartech('identify', email);   //pass email in identify section of logged in user, keep empty for non logged users
                smartech('dispatch', 102, {
                    "s^dealname": dealName,
                    "s^dealurl": dealUrl,
                    "i^dealprice": dealPrice,
                    "d^deal_end_date": dealEndDate,
                    "s^deal_image": dealimg
                });
            }

            //var isLoggedin = $(':hidden[name="UserLoggedIn"]').length > 0;
            //if (isLoggedin) {
                
            //} else {
            //    $("#dvLoginModal").find("form#frmLogin").find(":hidden[name='ReturnUrl']")
            //        .val(dealUrl);
            //    $("#dvLoginModal").modal({ keyboard: false, backdrop: false }).show();
            //    return false;
            //}
        });
       
        
        // recently view deals
        

        // recently view for deatils
        if (packageIds.length > 0 && $("#recently-view-box").length) {
            $.ajax({
                url: '/deal/recentlyview',
                method: "POST",
                data: { ids: packageIds },
                success: function (data) {
                    $("#recently-view-box").html(data);
                    if ($.fn.initSlickSlider != undefined) {
                        initSlickSlider();
                    }
                },
            })
        }
        if (packageIds.length > 0 && $("#package-recently-view-box").length) {
            $.ajax({
                url: '/package/recentlyview',
                method: "POST",
                data: { ids: packageIds },
                success: function (data) {
                    $("#package-recently-view-box").html(data);
                    if ($.fn.initSlickSlider != undefined) {
                        initSlickSlider();
                    }
                },
            })
        }

        // get upcomming deals
        if ($("#upcomingDeals").length) {
            $.ajax({
                url: '/home/UpcomingDeals',
                method: "POST",
                success: function (data) {
                    $("#upcomingDeals").html(data);
                },
            })
        }
    })();
})();