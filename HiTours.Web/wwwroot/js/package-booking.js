(function hotelBooking() {
    this.oSummary = { Status: false, Message: '' };

    this.oSettings = { url: "/package/booking", key: "hi-tours-booking" }

    this.oSelectors = { container: '' };

    this.fnSpinner = function (visible) {
        if (visible === true) {
            $(".deal-booking-loading").removeClass('hide');
        }
        else {
            $(".deal-booking-loading").addClass('hide');
        }
    };

    this.fnGetSummary = function () {
        var summary = localStorage.getItem(this.oSettings.key);
        this.oSummary.Status = summary !== null
        this.oSummary.Message = summary === null
            ? 'Please Select Checkin-CheckOut Dates'
            : '';
        if (summary !== null) {
            var jsonData = {};
            try {
                jsonData = JSON.parse(summary);
            } catch (e) {
                jsonData = {};
            }
            this.oSummary = $.extend(this.oSummary, jsonData);
        }
        return this.oSummary;
    };

    this.fnGetSmartTechInfo = function () {
        this.fnGetSummary();
         
        smartech('create', 'ADGMOT35CHFLVDHBJNIG50K969M2CP4N22UB4JPT4SRLRK91B7CG');
        smartech('register', '7118711369bb8028c468d27da6066644');
        smartech('identify', oSummary.email || '');
        smartech('dispatch', 103, {
            "s^dealname": oSummary.packageName || '',
            "i^dealprice": oSummary.packageamount,
            "s^dealurl": oSummary.url,
            "d^deal_end_date": oSummary.dealEnd,
            "f^deal_discount": String(oSummary.discountPercent),
            "s^room_type": oSummary.roomtype,
            "s^deal_category": oSummary.travelStyle,
            "i^no_of_rooms": String(oSummary.rooms),
            "s^deal_highlights": '',
            "d^Check_in_date": oSummary.checkInDate,
            "d^Check_out_date": oSummary.checkOutDate,
            "i^no_of_nights": String(oSummary.nights),
            "i^no_of_travellers": String(oSummary.adults + oSummary.childs + oSummary.infants),
            "s^book_flight": String(oSummary.addflight)
        });
    }

    this.fnGetInformation = function () {
        this.fnSpinner(true);
        this.oSummary = this.fnGetSummary();
        if (this.oSummary.Status && this.oSummary.Message === '') {
            var formData = {};
            for (var i in this.oSummary) {
                if (typeof this.oSummary[i] != "object") {
                    formData[i] = this.oSummary[i]
                }
            }
            $.ajax({
                method: 'post',
                datatype: 'html',
                url: this.oSettings.url,
                data: formData,
                success: function (html, textStatus, jqXHR) {
                    fnRenderHtml(html);
                },
                complete: function () {
                    fnSpinner(false);
                }
            });
        }
        else {
            window.location = "/deal/details/" + $("[name='_packageid_']").val() + "/" + $("[name='_packagename_']").val() || '';
            this.fnSpinner(false);
        }
    };
    this.fnRenderHtml = function (html) {
        if (html != undefined && html != "") {
            $('.deal-booking-loading').parent().html(html);
            var _form = $('.booking-container').find('form').eq(0);
            if ($.validator.unobtrusive != undefined) {
                $.validator.unobtrusive.parse(_form);
            }
            $(".contact-box-details > hr:last").remove();
            $('.deal-countdown').each(function () {
                var timmerseconds = $(this).data("seconds") || 0;
                $(this).timeTo({
                    seconds: parseInt(timmerseconds),
                    displayDays: 2,
                    fontSize: 23,
                    captionSize: 10,
                    displayCaptions: true
                }, function () { alert('Countdown finished'); });
            });
            initPluggins();
            $(".select-room-numbers").change();
            $("#frmBookNow").on("submit", function () {
                if (!$(this).find(':checkbox[name="TermsAndConditions"]').prop("checked")) {
                    $('[data-valmsg-for="TermsAndConditions"]')
                        .removeClass("field-validation-valid")
                        .addClass("field-validation-error")
                        .html('Please Select Terms & Conditions');
                    return false;
                }
            });
        }

        fnSpinner(false);
    };
    function StartPaymentWithRazorPay(data) {
        
        var options = {
            "key": data.DataKey,
            "amount": data.DataAmount, // 2000 paise = INR 20
            "name": data.DataName,
            "description": data.DataDescription,
            "image": data.DataImage,
            "order_id": data.DataOrderId,
            "handler": function (response) {
                
                $.ajax({
                    type: "POST",
                    url: '/package/bookingconfirmation',
                    data: {
                        "razorpay_payment_id": response.razorpay_payment_id,
                        "razorpay_order_id": response.razorpay_order_id,
                        "razorpay_signature": response.razorpay_signature
                    },
                    success: function (result) {
                        window.location = "/Account/MyBooking";
                    }
                });
                ////alert(response.razorpay_payment_id);
            },
            "prefill": {
                "name": data.DataPrefillName,
                "email": data.DataPrefillEmail,
                "contact": data.DataPrefillContact
            },

            "modal": {
                "ondismiss": function () { },
                "escape": false
            },
            "theme": {
                "color": data.DataThemeColor
            }
        };
        var rzp1 = new Razorpay(options);
        rzp1.open();
        hideWaitProcess();
    }
    this.fnInit = (function () {
      
        this.fnGetSmartTechInfo();
        this.fnGetInformation();

        $(document).on("submit", "form#payment-booknow", function (event) {
            event.preventDefault();
            
            showWaitProcess();
            smartech('create', 'ADGMOT35CHFLVDHBJNIG50K969M2CP4N22UB4JPT4SRLRK91B7CG');
            //var formData = {
            //    'pk^email': email,
            //    'mobile': $("#HotelBooking_Mobile").val(),
            //    'FIRSTNAME': $("#HotelBooking_FirstName").val(),
            //    'LASTNAME': $("#HotelBooking_LastName").val(),
            //    'ADDRESS': $("#HotelBooking_BillingAddress").val(),
            //    'COUNTRY': $("#CountryId option:selected").text(),
            //    'CITY': $("#CityId option:selected").text(),
            //    'ZIPCODE': $("#HotelBooking_PinCode").val()
            //};
            var formData = {
                'pk^email': $('#BookingEmail').val(),
                'mobile': $("#HotelBooking_Mobile").val(),
                'FIRSTNAME': $("#firstname").val(),
                'LASTNAME': $("#lastnamecaps").val(),
                'ADDRESS': $("#HotelBooking_BillingAddress").val(),
                'COUNTRY': $("#CountryId option:selected").text(),
                'CITY': $("#CityId option:selected").text(),
                'ZIPCODE': $("#HotelBooking_PinCode").val()
            };
            smartech('contact', 6, formData);
            
            smartech('create', 'ADGMOT35CHFLVDHBJNIG50K969M2CP4N22UB4JPT4SRLRK91B7CG');
            smartech('register', '7118711369bb8028c468d27da6066644');
            smartech('identify', formData["pk^email"]);
            smartech('dispatch', 3, {
                "s^dealname": oSummary.packageName,
                "f^dealprice": $("#RoomPrice").val(),
                "s^dealurl": oSummary.url,
                "s^deal_image": oSummary.packageimg,
                "f^booking_amount": $("#hfBookingAmount").val(),
                "s^book_flight": String(oSummary.addflight),
                "d^deal_end_date": String(String.dealEnd),
                "s^deal_highlights": '',
                "s^city": oSummary.packagecity,
                "d^tour_start_date": $("#CheckInDate").data('start-date'),
                "d^tour_end_date": $("#CheckInDate").data('end-date'),
                "s^tour_duration": String(oSummary.nights) + " nights",
                "i^no_of_travellers": String(oSummary.adults + oSummary.childs + oSummary.infants),
                "f^holiday_price": $("#RoomPrice").val(),
                "f^weekend_surcharge": $("#hfWeekend").val(),
                "f^tax": $("#hfTax").val(),
                "f^Total_including_taxes": $("#hfTotalincludingtaxes").val()
            });
            //Submit And Serialize form
            var $form = $(this);
            
            if ($form.valid()) {
                $.ajax({
                    url: '/package/booknow',
                    type: "POST", // type of action POST || GET
                    dataType: 'json', // data type
                    data: $form.serialize(),
                    success: function (data) {
                        
                        if (data.ErrorCode != "0") {
                            hideWaitProcess();
                            return false;
                            //Handle Errors.
                        }
                        else {
                            StartPaymentWithRazorPay(data.Result);
                        }
                        hideWaitProcess();
                        return false;  
                    }    
                });
            }
        });
        $(document).on("change", ".select-room-numbers", function () {
            var adults = parseInt($(this).val() || 0);
            var roomNo = parseInt($(this).data("room") || 0);
            if (adults > 0) {
                $('[data-roomno="' + roomNo + '"]').each(function () {
                    var adult = parseInt($(this).data('adults') || 0);
                    if (adult <= adults) {
                        $(this).find(":text").attr("required", "required");
                        $(this).removeClass("hide");
                    } else {
                        $(this).find(":text").removeAttr("required");
                        $(this).find(":text").val('');
                        $(this).addClass("hide");
                    }
                });
                var totalAdults = 0;
                $(".select-room-numbers").each(function () {
                    if (!isNaN($(this).val()))
                        totalAdults += parseInt($(this).val());
                });
                $("#spnTotalAdults").html(totalAdults);
            }

            var label = $("[required='required']").parent(".form-group").find('label').not(".skip-required");
            label.find('.danger').remove();

            label.append('<span class="danger"> *</span>')
        });
    })();

    $(document).on("change", ".dob", function (e) {
        e.preventDefault();
         
        if ($(this).val().length == 10) {
            var dates = "";
            $(".dob").each(function () {
                if (dates.length > 0) {
                    dates = dates + "," + $(this).val();
                }
                else {
                    dates = $(this).val();
                }
            })
            $.ajax({
                method: 'Get',
                datatype: 'html',
                url: "/package/holidayprice",
                data: {
                    price: $("#RoomPrice").val(),
                    weekendprice: $("#RateWeekend").val(),
                    checkInDate: $("#CheckInDate").data('date'),
                    checkOutDate: $("#CheckOutDate").data('date'),
                    dates: dates,
                    deposit: $("#DepositAmt").val(),
                },
                success: function (html) {
                    $(".total-amout-box").html(html);
                    //var hfDeposite = parseFloat($("#DepositAmt").val());
                    //var deposti = parseFloat($("#coutAdults").val()) * hfDeposite;
                    //if (!isNaN(hfDeposite) && hfDeposite > 0) {
                    //    if (!isNaN(deposti) && deposti > 0) {
                    //        $("[data-depositamt='deposit']").html(numberWithCommas(deposti.toString().replace('.00', '')));
                    //    }
                    //    else {
                    //        $("[data-depositamt='deposit']").html('0');
                    //    }
                    //}

                    if ($("#coutAdults").val() == "0") {
                        swal("", "Atleast One Passenger should be Adult.", "warning");
                    }
                }
            });
        }
    });
})();
function numberWithCommas(number) {
    var parts = number.toString().split(".");
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return parts.join(".");
}