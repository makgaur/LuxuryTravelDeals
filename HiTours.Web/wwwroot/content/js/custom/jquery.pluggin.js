var overlayTemplate = '<div class="overlay"><div class="fa fa-refresh fa-spin"></div></div>';
$(function () {
    initPluggins();
    $("#btnSubmit").on("click", function () {
        //showWaitProcess();
        setTimeout(function () {
            hideWaitProcess();
        }, 2000);
        
        if ($(this).parents('form').valid() && $("#form-submit").val() == undefined) {
            $(this).parents('form').append("<input type='hidden' value='1' id='form-submit' />")
            showWaitProcess();
        }
        else {
            return false;
        }
       
    })
    //showWaitProcess();
});

function truncateDate(date) {
    return new Date(date.getFullYear(), date.getMonth(), date.getDate());
}
 
function initPluggins() {
   
    var newDate = new Date((year || (new Date()).getFullYear()), (month || (new Date()).getMonth()), (day || (new Date()).getDate()));
    initSelect2();
    initTinymce();
    if ($.fn.iCheck != undefined) {
        $('input[type="checkbox"].iCheck, input[type="radio"].iCheck').iCheck({ checkboxClass: 'icheckbox_minimal-blue', radioClass: 'iradio_minimal-blue' });
    }
    $(".datepicker-multiple").each(function () {
        if ($(this).data('datepicker')) {
            $(this).datepicker('destroy');
        }
        $(this).datepicker({ multidate: true,format: dateformat,todayHighlight: true,autoclose: false,startDate: truncateDate(newDate)});
    })
   
    $(':checkbox[data-toggle="toggle"]').each(function () {
        var $checkbox = $(this);
        var oSettings = $checkbox.data('toggle-options') || {};
        $checkbox.removeAttr("data-toggle-options");
        $checkbox.data("toggle-options", oSettings);
        if ($.fn.bootstrapToggle != undefined) {
            $checkbox.bootstrapToggle({ on: oSettings.on || "Yes", off: oSettings.off || "No", size: oSettings.size || 'mini' });
        }
    });
    if ($.fn.datepicker != undefined) {
        $(".datepicker").each(function () {
            if ($(this).data('datepicker')) {
                $(this).datepicker('destroy');
            }
            $(this).datepicker({ format: dateformat, todayHighlight: true, autoclose: true, startDate: truncateDate(newDate) });
        })

        $(".input-daterange").each(function () {
            if ($(this).data('datepicker')) {
                $(this).datepicker('destroy');
            }
            $(this).datepicker({ format: dateformat, autoclose: true, startDate: truncateDate(newDate) });
        })


        $(document)
            .off("changeDate", $(".input-daterange").find('.validate-to'))
            .on("changeDate", $(".input-daterange").find('.validate-to'), function (ev) {
                var targetTo = $(this).data("target-minDate") || '';
                if ($(targetTo).length) {
                    if ($(targetTo).data('datepicker')) {
                        $(targetTo).datepicker('destroy');
                    }
                    $(targetTo).datepicker({
                        format: dateformat,
                        todayHighlight: true,
                        autoclose: true,
                        startDate: ev.date
                    });
                    $(targetTo).val('');

                }
            });

        $(".dob").datepicker({ format: dateformat, todayHighlight: true, autoclose: true });
    }


    if ($.fn.format != undefined) {
        $(".numericOnly").format({ precision: 0, autofix: true, allow_negative: false });
        $(".decimalOnly").format({ precision: 2, autofix: true });
        $(".numericOnly,.decimalOnly").not(".skip-blank").each(function () {
            $(this).val($(this).val().replace(".00", ""));
            if ($(this).val() === "0") {
                $(this).val("");
            }
        });
    }


    if ($.fn.format != undefined) {
        $(".numericOnly").format({ precision: 0, autofix: true, allow_negative: false });
        $(".decimalOnly").format({ precision: 2, autofix: true });
        $(".numericOnly,.decimalOnly").not(".skip-blank").each(function () {
            $(this).val($(this).val().replace(".00", ""));
            if ($(this).val() === "0") {
                $(this).val("");
            }
        });
    }



    $("[data-val-length-max]").each(function () {
        $(this).attr("maxlength", $(this).data("val-length-max"));
    });

    if ($.fn.imageloader != undefined) {
        $('body').imageloader({ selector: '[data-src]' });
    }

    if ($("[data-sweetalert]").length > 0) {
        var container = $("[data-sweetalert]");
        if (container.data("message") != undefined && container.data("message-type") != undefined && container.data("message").length > 0) {
            //$.bootstrapGrowl(container.data("message"), {
            //    type: container.data("message-type"),
            //    delay: 2000,
            //});
            swal('', container.data("message"), container.data("message-type"));
            $("[data-sweetalert]").remove();
        }
        $(".copylinkToclipboard").on("click", function () {

            var $element = $(this).prev("span").find('img');
            if ($element.length) {
                copyToClipboard(window.location.origin + "/" + $element.attr('src'));
            }
        });

    }

    $("[data-val-required]").parents('.form-group').find('label').not(".skip-required").each(function () {
        if ($(this).find(".danger").length == 0) {
            $(this).append('<span class="danger"> *</span>')
        }
    });


    //$("[data-val-required]").parents('.form-group').find('label').not(".skip-required").append('<span class="danger"> *</span>')

    $("[type='text']").not(".html-required").on("blur", function (e) {
        var box = this;
        var reg = /<(.|\n)*?>/g;
        if (reg.test($(box).val()) == true) {
            alert('HTML Tag are not allowed');
            $(box).val('');
        }
        e.preventDefault();
    });



    $(document).off('click', 'td.show-nested').on('click', 'td.show-nested', function () {
        var tr = $(this).parents('tr');
        if ($(tr).hasClass("shown")) {
            $(tr).next('.nested-row').addClass('hide');
            $(tr).removeClass('shown');
        } else {
            $(tr).parents('table').find('tr.shown').each(function () {
                $(this).next('.nested-row').addClass('hide');
                $(this).removeClass('shown');
            });
            $(tr).next('.nested-row').removeClass('hide');
            $(tr).addClass('shown');
        }
    });


}

function initTinymce() {
    if ($.fn.tinymce != undefined) {
        tinymce.remove();
        tinymce.init({
            ////forced_root_block: "",
            //// force_br_newlines: true,
            force_p_newlines: false,
            selector: '.tinymceTextarea',
            height: 400,
            toolbar: "media",
            menubar: false,
            content_css: [
                '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
                '//www.tinymce.com/css/codepen.min.css'],

            plugins: [
                'advlist autolink lists link image charmap print preview anchor',
                'searchreplace visualblocks code fullscreen',
                'insertdatetime media table contextmenu paste code'
            ],

            toolbar: 'undo redo | insert | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | media | code |codesample|mybutton',
            toolbar2: 'print preview media | forecolor backcolor emoticons | media |test |code',
            setup: function (editor) {
                editor.addButton('media', {
                    plugins: "code",
                    toolbar: "code",
                    selector: "textarea.tinymce",
                    plugins: "media",
                    toolbar: "media",
                    selector: 'textarea',
                    plugins: "paste",
                    toolbar: "paste",
                    menubar: "edit",
                    media_url_resolver: function (data, resolve/*, reject*/) {
                        if (data.url.indexOf('YOUR_SPECIAL_VIDEO_URL') !== -1) {
                            var embedHtml = '<iframe src="' + data.url +
                                '" width="400" height="400" ></iframe>';
                            resolve({ html: embedHtml });
                        } else {
                            resolve({ html: '' });
                        }
                    }
                });
            }
        });
    }
}

function initSelect2() {
    if ($.fn.select2 != undefined) {
        $("[data-pluggin-select2]").each(function () {
            var $element = $(this), options = {};
            var oSettings = $element.data("pluggin-select2");
            if (oSettings) {
                $.extend(oSettings, options);
                $element.removeAttr("data-pluggin-select2");
                $element.data("pluggin-select2", oSettings);
            }
            if (oSettings.disabled) {
                $(this).before("<input type='hidden' name=" + $(this).attr("name") + " value='" + $(this).val() + "' />");
            }

            if (oSettings.url) {
                var data = $(this).data("param");
                oSettings.ajax = {
                    url: oSettings.url,
                    dataType: 'json',
                    delay: oSettings.delay,
                    data: function (params) {
                        
                        var options = $(this).data("plugginSelect2") || {};
                        var addition = $(this).data("param") || {};
                        addition[$(this).attr("name")] = options.currentValue || "";
                        var query = $.extend(addition, {
                            search: params.term || "", page: params.page || 1,
                        });
                        return query;
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

                oSettings.escapeMarkup = function (markup) { return markup; };
                oSettings.templateResult = function (oData) { return oData.text; };
                oSettings.templateSelection = function (data) { return data.text; }                
            }

            if (!$element.hasClass("skip-select2")) {
                $element
                    .select2(oSettings);
                if (oSettings.partialUrl.length > 0) {
                    $element.on('select2:open', function () {
                        var add = '';
                        add += '<a style="cursor:pointer; padding: 3px 0px;" onclick="fnAddEditItem(&quot;#' + $(this).attr('id') + '&quot;,&quot;defaultadd&quot;)">';
                        add += '    <i class="fa fa-plus-circle" ></i > ' + oSettings.partialTitle;
                        add += '</a> ';
                        var $element = $('<div style="border-top: solid 1px #27638a;text-align:center;"/>')
                        $element.append(add);
                        $(".select2-results:not(:has(a))").append($element);
                    });
                    //$element.on("change", function () {
                    //    var $select = $(this);
                    //    $select.siblings().find('.edit-partial').remove();
                    //    $select.next('.select2-container').removeClass('select2-container-addchild')
                    //    if ($select.val() != '' && $select.val() != null) {
                    //        var edit = '';
                    //        edit += '<a class="form-control btn-form-control" onclick="fnAddEditItem(this,&quot;edit&quot;)">';
                    //        edit += '   <i class="fa fa-edit"></i>';
                    //        edit += '</a>';
                    //        var $element = $('<span class="edit-partial field-group field-btn-group selectedit" />');
                    //        $element.append(edit);
                    //        $select.next('.select2-container').append($element);
                    //        $select.next('.select2-container').addClass('select2-container-addchild')
                    //    }
                    //});
                }

            }
            $element.css({ "width": "auto", "display": "block !important" });

            if (oSettings.dependent != undefined && oSettings.dependent) {
                
                setDependent($element);
                $element.on("change", function () {
                    setDependent($(this), true);
                });
            }

            if (oSettings.hold && oSettings.hold !== null && oSettings.hold !== '') {
                $element.on("change", function () {
                    var text = $(this).find("option:selected").text()
                    var options = $(this).data('plugginSelect2');
                    if (options && options.hold && options.hold !== null && options.hold != '') {
                        $(options.hold).val(text);
                    }
                });
            }


        });
    }  
}

function fnAddEditItem(element, action) {
    var select;
    var value;
    if ((action || '').toLowerCase() == "edit") {
        select = $(element).parents(".select2-container").prev("select");
        value = select.val();
    }
    else if ((action || '').toLowerCase() == "defaultadd") {
        select = $(element);
        value = null;
        searchtext = null;
    }
    else {
        select = $("span.select2-container--below.select2-container--open").prev("select");
        select.select2('close');
        value = null;
    }
    if ($(select).length > 0) {
        $(document).data('partial-action', { action: action, selector: "#" + $(select).attr('id') });
        var options = $(select).data('pluggin-select2') || {};
        var url = options.partialUrl || '';
        var title = options.partialTitle || '';
        if (url != null && url != '') {
            if ($(select).data('select2') != undefined)
                $(select).select2('close');
            fnModal(title, url, { id: value });
            //fnModal(title, url, null);

            
        }
    }

}

function fnModal(title, url, formData) {
    var $modal = $('#application-modal');
    if ($modal) {
        $modal.find(".modal-title").html((title || ''));
        $modal.find('.modal-body').html(overlayTemplate);
        $modal.modal({ keyboard: false, backdrop: false }).show();
        $.ajax({
            url: url,
            data: formData || {},
            method: 'get',
            dataType: 'html',
            success: function (html) {
                $('#application-modal').find('.modal-body')
                    .html(html);
                initSelect2();
               //$('#application-modal').find("form").each(function () {
               //    if ($.validator.unobtrusive != undefined) {
               //        $.validator.unobtrusive.parse($(this));
               //    }
               //});


               var $divPartial = $('#application-modal');
                $divPartial.find("#btnSubmit").on("click", function () {                   
                   var _form = $(this).parents('form');
                   if ($.validator.unobtrusive != undefined) {
                       $.validator.unobtrusive.parse(_form);
                   }
                   if ($(_form).valid()) {
                       _form.append('<input name="method" type="hidden" value="ajax" />');
                       $.ajax({
                           type: "POST",
                           url: _form.attr('action'),
                           data: _form.serialize(),
                           success: function (json) {
                               if (json.success) {
                                   $divPartial.find(".close").click()
                                   var option = $(document).data('partial-action');
                                   if (option != undefined) {
                                       if (option.action == "edit") { 
                                           $(option.selector).siblings()
                                               .find(".select2-selection__rendered")
                                               .html(json.Name);
                                           $(option.selector).parents('tr').find(".roomtypedes").val(json.Description)
                                           swal('', 'Successfully Updated', 'success');                                           
                                       }
                                       else {
                                           $(option.selector).select2("trigger", "select", {
                                               data: { id: json.Id, text: json.Name, Description: json.Description }
                                           });
                                           swal('', 'Successfully Inserted', 'success');
                                          
                                           // $(option.selector).append(new Option(json.Name, json.Id, false, false)).trigger('change');
                                       }
                                   }
                                   return false;

                               }
                               $divPartial.find(".close").click();
                               if ($(json).find('[name="TrIndentifier"]').length) {
                                   var val = $(json).find('[name="TrIndentifier"]').val();
                                   if (val != "") {
                                       $(".nested-row").find('tbody').append(json);
                                   }
                                   else
                                   {
                                       $(".clsBindResult").append(json);
                                   }

                               }
                             
                           },
                           error: function (response) {
                           },
                       });
                   }


                   return false;


                   //$(document).on('submit', $('#application-modal').find("form"), function (event) {
                   //    event.preventDefault();
                   //    var $form = $(event.target);
                   //    if ($form.length > 0) {
                   //        $form.append('<input name="method" type="hidden" value="ajax" />');
                   //        if ($form.valid()) {
                   //            //$(document).data("form-data", $form.data());
                   //            $.ajax({
                   //                type: "POST",
                   //                url: $form.attr('action'),
                   //                data: $form.serialize(),
                   //                error: function (xhr, error, thrown) { },
                   //                success: function (json) {

                   //                    if (json.success && json.success === true) {
                   //                        var option = $(document).data('partial-action');
                   //                        if (option && option.selector) {
                   //                            if (option.action == "edit")
                   //                                $(option.selector).siblings()
                   //                                    .find(".select2-selection__rendered")
                   //                                    .html(json.Name);
                   //                            else {
                   //                                $(option.selector).select2("trigger", "select", {
                   //                                    data: { id: json.Id, text: json.Name }
                   //                                });
                   //                                // $(option.selector).append(new Option(json.Name, json.Id, false, false)).trigger('change');
                   //                            }
                   //                        }
                   //                        $(document).removeData('partial-action');
                   //                        $('#application-modal').modal('hide');
                   //                        //$('#application-modal').find(".modal-body").html('');
                   //                        //window.location.href = window.location;

                   //                    }
                   //                }
                   //            });
                   //        }
                   //    }
                   //    else {
                   //        return true;
                   //    }

               });
            },
            error: function (xhr, request, error) { }
        });
    }
};

function setDependent($select, trigger) {
    
    var options = $($select).data("pluggin-select2");
    if (options != undefined && options instanceof Object) {
        var control = options.control;
        var othercontrols = options.otherControls;
        var value = $($select).val();
        var id = $($select).attr('id');
        var postData = {};
        postData[id] = value == "" ? 0 : value;
        if ($($select).data('alias')) {
            postData[($($select).data('alias') || 'a')] = value == "" ? 0 : value;
        }
        if (othercontrols != null && othercontrols != undefined && othercontrols.length > 0) {
            var ids = othercontrols.split(',');
            for (var i = 0; i < ids.length; i++) {
                postData[$(ids[i]).attr('id')] = $(ids[i]).val();
            }
        }

        if ($(control).length > 0) {
            $(control).data("param", postData);
            if (trigger) {
                $(control).val('');
                $(control).html('');
                $(control).change()
            }
        }
    }
}

function copyToClipboard(text) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val(text).select();
    document.execCommand("copy");
    $temp.remove();
}