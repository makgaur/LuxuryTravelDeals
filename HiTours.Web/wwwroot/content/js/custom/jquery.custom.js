/// <reference path="jquery.constants.js" />
$(function () {
    $(".remove-package-image").each(function () {
        $(this).data("swal", { fnCallBackSuccess: onDeletePackageImages });
    });
    $(document).on("click", '.delete-confirm', function (event) {
        event.preventDefault();
        var href = $(this).attr('href');
        var oSetting = $(this).data("swal") || {};
        var swalOptions = {
            title: oSetting.title || 'Are you sure?',
            text: oSetting.text || "You won't be able to revert this!",
            type: oSetting.type || 'warning',
            showCancelButton: oSetting.showCancelButton || true,
            confirmButtonColor: oSetting.confirmButtonColor || '#3085d6',
            cancelButtonColor: oSetting.cancelButtonColor || '#d33',
            confirmButtonText: oSetting.confirmButtonText || 'Yes, delete it!',
            fnCallBackSuccess: oSetting.fnCallBackSuccess || function () { }
        };
        swal(swalOptions).then(function () {
            $.ajax({
                type: "POST",
                url: href,
                dataType: 'JSON',
                success: swalOptions.fnCallBackSuccess
            });
        });
        return false;
    });

    $(document).on("click",
        '[data-delete]',
        function (e) {
            debugger;
            var href = $(this).attr('href');
            //function () {
            //    swal("Deleted!", "Your imaginary file has been deleted.", "success");
            //});
            swal({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then(function () {
                showWaitProcess();
                $.ajax({
                    type: "POST",
                    url: href,
                    dataType: 'JSON',
                    success: function (response) {
                        debugger;
                        //swal('Deleted!', 'Your file has been deleted.', 'success');
                        location.reload();
                    }
                });
            })
            return false;
        });

    $(document).on("click",
        '[data-delete-PackageImage]',
        function (e) {
            var href = $(this).attr('href');
            //function () {
            //    swal("Deleted!", "Your imaginary file has been deleted.", "success");
            //});
            swal({
                title: 'Are you sure?',
                text: "You won't be able to revert this!",
                type: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then(function () {
                $.ajax({
                    type: "POST",
                    url: href,
                    dataType: 'JSON',
                    success: function (response) {
                        
                        //swal('Deleted!', 'Your file has been deleted.', 'success');
                        $("#image-order").find(".fa-check-circle-o").each(function () {
                            if ($(this).parent().hasClass("checked"))
                            {
                                $(this).parent().remove();
                            }
                            
                        })
                        
                    }
                });
            })
            return false;
        });
});


function onDeletePackageImages(json) {
    if (json.Status) {
        $("a[href='" + this.url + "']").parent().remove();
        $.bootstrapGrowl(json.Message, { type: "success", delay: 2000 });
    }
}