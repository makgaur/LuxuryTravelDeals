(function hotelBooking() {
    this.oSummary = { Status: false, Message: '' };

    this.oSettings = { url: "/deal/booking", key: "hi-tours-booking" }

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
    this.fnInit = (function () {
        this.fnGetInformation();
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
})();