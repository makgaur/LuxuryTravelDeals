(function oApi() {
    this.api = { keys: $.extend(oApiKeys || {}) };
    this.getRequest = function () {
        if (this.api.request === undefined) {
            var result = oApiRequest !== undefined ? oApiRequest : {};
            oApiRequest = {};
            return result;
        }
        return this.api.request;
    };
    this.getResponse = function () {
        if (this.api.response === undefined) {
            var result = oApiResponse !== undefined ? oApiResponse : {};
            if (oApiResponse.Response &&
                oApiResponse.Response.Error &&
                oApiResponse.Response.Error.ErrorMessage
                    .indexOf("Your session (TraceId) is expired.") == 0) {
                api.clipBoard[api.keys.airSearch] = [];
                api.clipBoard[api.keys.fairQuoteSearch] = [];
                api.clipBoard[api.keys.traceid] = '';
            }

            oApiResponse = {};
            return result;
        }
        return this.api.response;
    };
    this.getClipBoard = function () {
        if (this.api.clipBoard === undefined) {
            this.api.clipBoard = {};
            if (this.getJSON(this.api.keys.clipBoard) !== null) {
                this.api.clipBoard = this.getJSON(this.api.keys.clipBoard);
            }
            $.extend(this.api.clipBoard, oClipBoard);
            oClipBoard = {};
        }
        return this.api.clipBoard;
    };

    this.bindResultIndex = function (element, key) {
        var $node = $(element || '');
        if (Object.keys(api.clipBoard).length > 0 && $node.length) {
            var searchResult = api.clipBoard[key] || [];
            if ($node.data("llc") != undefined) {
                var llc = $(this).data("llc");
                searchResult = searchResult.filter(function (item) { return item.IsLCC === llc; })
            }
            var indexes = Enumerable.From(searchResult)
                .Select(function (item) {
                    return { ResultIndex: item.ResultIndex, IsLCC: item.IsLCC };
                }).ToArray();

            var selectedvalue = $node.data("selected") || '';
            for (var i in indexes) {
                var $option = $(new Option(indexes[i].ResultIndex, indexes[i].ResultIndex));
                if (selectedvalue == indexes[i].ResultIndex) {
                    $option.prop("selected", true);
                }
                $option.data("IsLCC", indexes[i].IsLCC)
                $node.append($option);
            }
        }
    }

    this.getJSON = function (key) {
        if (localStorage.getItem(key) !== null) {
            return JSON.parse(localStorage.getItem(key));
        }
        return null;
    };

    this.setClipBoard = function () {
        if (Object.keys(this.api.clipBoard).length > 0) {
            localStorage.setItem(api.keys.clipBoard, JSON.stringify(this.api.clipBoard));
        } else if (this.getJSON(this.api.keys.clipBoard) !== null) {
            this.api.clipBoard = this.getJSON(this.api.keys.clipBoard);
        }
    };

    this.clearClipBoard = function () {
        this.api.clipBoard = {};
        localStorage.removeItem(this.api.keys.clipBoard);
    };

    this.getLLCPassengers = function (resultIndex) {
        
        if (resultIndex != undefined && resultIndex !== null) {
            window.element = '[data-bind="llcpassengers"]';
            var searchResult = api.clipBoard[api.keys.airSearch] || [];
            var fareQuoteResult = api.clipBoard[api.keys.fairQuoteSearch] || [];
            if (searchResult.length > 0) {
                ////searchResult = searchResult.filter(function (item) { return item.ResultIndex === resultIndex; })
            }
            if (searchResult.length == 0) {
                $(window.element).html('');
            } else {
                if (api.clipBoard.Passengers == undefined) {
                    api.clipBoard.Passengers = {};
                }

                $.ajax({
                    url: '/tbo/airservice/getllcpassengers',
                    method: 'post',
                    dataType: 'html',
                    data: {
                        adults: api.clipBoard.Passengers.AdultCount,
                        childs: api.clipBoard.Passengers.ChildCount,
                        infants: api.clipBoard.Passengers.InfantCount,
                        jsonResult: JSON.stringify(searchResult || {}),
                        fareQuote: JSON.stringify(fareQuoteResult || {})
                    },
                    success: function (html) {
                        $(window.element).html(html);
                        $("form").removeData("validator");
                        $("form").removeData("unobtrusiveValidation");
                        $.validator.unobtrusive.parse("form");
                        initPluggins();
                    },
                    error: function (xhr, request, error) {
                    }
                });
            }
        }
    };

    this.init = (function () {
        this.api.clipBoard = this.getClipBoard();
        this.api.request = this.getRequest();
        this.api.response = this.getResponse();
        this.api.airSource = {}

        this.setClipBoard();

        if ($('[name="clearclipboard"]').length && $('[name="clearclipboard"]').val() == "true") {
            this.clearClipBoard();
        }

        $(".json-view").JSONView(this.api.response, {
            collapsed: false,
            recursive_collapser: true
        });
        $(".json-action").removeClass("hide")
        $(".download-json").removeClass("hide");
        $('[data-jsonview]').on("click", function () {
            $(".json-view").JSONView($(this).data("jsonview"));
        });
        $(".download-json").on("click", function () {
            $(api.keys.downloadForm).removeAttr("action");
            $(api.keys.downloadForm).find(":hidden").val();
            if (Object.keys(api.request).length > 0 && Object.keys(api.response).length > 0) {
                $(".download-json").prop("disable", "disable")
                $(api.keys.downloadForm).find("[name=Request]").val(JSON.stringify(api.request));
                $(api.keys.downloadForm).find("[name=Response]").val(JSON.stringify(api.response));
                $(api.keys.downloadForm).find("[name=Action]").val(actionName);
                $(api.keys.downloadForm).attr("action", "/tbo/shared/downloadjsonfile");
                $(api.keys.downloadForm).submit();
            }
        });

        $(".password-mode").attr("type", "password");
        $(".toggle-password").on("click", function () {
            var $password = $(this).prev("input");
            var type = $password.attr("type")
            $password.attr("type", type == "text" ? "password" : "text");
            $(this).find("i").toggleClass("fa-eye", type == "password");
            $(this).find("i").toggleClass("fa-eye-slash", type == "text");
        });
        $(":text").filter("[value='0']").val('');
        $(":text").filter("[value='01/01/0001']").val('');

        // autocomplete clipboard data
        for (var key in api.clipBoard) {
            var $element = $("[data-autocomplete='" + key + "']");
            if ($element.length) {
                $element.val(api.clipBoard[key]);
                if (api.clipBoard[key].length) {
                    $element.attr("readonly", "readonly");
                }
            }
        }

        $('[data-bind="fareQuotes"]').each(function () {
            bindResultIndex('[data-bind="fareQuotes"]', api.keys.fairQuoteSearch);
        });

        $('[data-bind="searchresult"]').each(function () {
            debugger;
            bindResultIndex('[data-bind="searchresult"]', api.keys.airSearch);
        });

        $("form").submit(function () {
            if ($(this).data("validator") != undefined && $(this).valid() && $(this).attr("id") != $(api.keys.downloadForm).attr("id")) {
                showWaitProcess();
            }
        });
        $('[data-bind="llcpassengers"]').each(function () {
            if (!($(this).data("autotrigger") || false)) {
                getLLCPassengers($('[data-bind="fareQuotes"]').val() || '');
            }
        });

        $(document).on("change", "#JourneyType", function () {
            if ($(this).val() === "2" || $(this).val() === "5") {
                $(".addnew-segment").click();
            } else {
                $(".segments-item.section-row").eq(1).remove()
            }
        })


        $('[name="ResultIndex"]').on("change", function () {
            if ($(this).find("option:selected").length &&
                $(':hidden[name="IsLCC"]').length &&
                $(this).find("option:selected").data("IsLCC") != undefined) {
                $(':hidden[name="IsLCC"]').val($(this).find("option:selected").data("IsLCC"));
            }

        });

        $('[name="ResultIndex"]').trigger("change");
    })();
})();