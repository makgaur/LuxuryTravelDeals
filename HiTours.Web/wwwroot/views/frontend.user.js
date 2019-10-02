$(function () {
    $(document).on('submit', '#formSignInOtp', function (event) {
        debugger;
        var form = $('#formSignInOtp');
        form.data('validator', null);
        $.validator.unobtrusive.parse(form);
        event.preventDefault();
        if ($(this).valid()) {
            showWaitProcess();
            $.ajax({
                url: '/user/LoginOtp',
                method: "POST",
                data: $(this).serialize(),
                success: function (data) {
                    debugger;
                    hideWaitProcess();
                    if (data.Status) {
                        location.reload(true);
                    }
                    else {
                        swal('', data.Message, 'error');
                        if (data.Message == 'OTP has Expired') {
                            //$('#mob').removeClass('hidden');
                            //$('#otp').addClass('hidden');
                            //$('#sendOtp').removeClass('hidden');
                            //$('#sign-in-otp').addClass('hidden');
                        }
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    hideWaitProcess();
                    swal('', errorThrown, 'error');
                }
            })
        }
    });
    $(document).on('submit', '#formSignIn', function (event) {
        var form = $('#formSignIn');
        form.data('validator', null);
        $.validator.unobtrusive.parse(form);
        event.preventDefault();
        if ($(this).valid()) {
            showWaitProcess();
            $.ajax({
                url: '/user/login',
                method: "POST",
                data: $(this).serialize(),
                success: function (data) {
                    hideWaitProcess();
                    if (data.Status) {
                        //smartech('create', 'ADGMOT35CHFLVDHBJNIG50K969M2CP4N22UB4JPT4SRLRK91B7CG');
                        //var formData = {
                        //    'pk^email': $('#Email').val(),
                        //    'LEADSCORE': 1
                        //};
                        //smartech('contact', 8, formData);
                        //if (!data.Redirect) {
                        //    $('input[name=_UserId_]').val(data.Id);
                        //    $('.deal-form').submit();
                        //    return;
                        //}
                        //window.location = data.RedirectUrl;
                        location.reload(true);
                    }
                    else {
                        swal('', data.Message, 'error');
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    hideWaitProcess();
                    swal('', errorThrown, 'error');
                }
            })
        }
    });
    $(document).on('click', '.sign-in-external.google', function (event) {
        window.location = '/User/SignInWithGoogle';
    });
    $(document).on('click', '.sign-in-external.facebook', function (event) {
        window.location = '/User/LoginWithFacebook';
    });

    //$(document).on('submit', '#formSignUp', function (event) {
    //    Sign_Up();
    //});




    //$(document).on('submit', '#formSignUp',function (event) {
    //    debugger;
    //    event.preventDefault();
    //    if ($(this).valid()) {
    //        showWaitProcess();
    //        $.ajax({
    //            url: '/user/register',
    //            method: "POST",
    //            data: $(this).serialize(),
    //            success: function (data) {
    //                debugger;
    //                hideWaitProcess();

    //                if (data.Status) {

    //                    var registrationType = $('[name="registrationType"]')
    //                    var isNotGuest = registrationType.val() == null || registrationType.val() == "";
    //                    if (isNotGuest) {
    //                        smartech('create', 'ADGMOT35CHFLVDHBJNIG50K969M2CP4N22UB4JPT4SRLRK91B7CG');
    //                        var formData = {
    //                            'pk^email': $('#EmailId').val(), // ('pk^ should be prefixed again primary key, eg here email is PK)
    //                            'mobile': $('#MobileNo').val(),
    //                            'FIRSTNAME': $('#FirstName').val(),
    //                            'LASTNAME': $('#LastName').val(),
    //                            'SOURCE': '',
    //                            'TRAVEL_REQ': '',
    //                            'LEADSCORE': 10
    //                        };
    //                        smartech('contact', 8, formData);
    //                        $("#frmUserSignUp")[0].reset();
    //                        swal('', data.Message, 'success');
    //                    }
    //                    else {
    //                        swal('', data.Message, 'warning');
    //                    }
    //                }
    //            },
    //            error: function (xhr, textStatus, errorThrown) {
    //                hideWaitProcess()
    //                swal('', errorThrown, 'error');
    //            }
    //        })
    //    }
    //});

    $("#frmRequstCallBack").submit(function (event) {
        event.preventDefault();
        if ($(this).valid()) {
            showWaitProcess()
            $.ajax({
                url: '/home/requestcallback',
                method: "POST",
                data: $(this).serialize(),
                success: function (data) {
                    hideWaitProcess()
                    if (data.Status) {

                        smartech('create', 'ADGMOT35CHFLVDHBJNIG50K969M2CP4N22UB4JPT4SRLRK91B7CG');

                        var formData = {

                            'pk^email': $("#frmRequstCallBack #Email").val(),

                            'mobile': $("#frmRequstCallBack #Phone").val(),

                            'FULLNAME': $("#frmRequstCallBack #Name").val(),

                            'BEST_TIME_TO_CALL': $("#BestTimeToCall:checked").val(),

                            'LEADSCORE': 10

                        };

                        smartech('contact', 74, formData);
                        if ($("#frmRequstCallBack").length > 0) {
                            $("#frmRequstCallBack")[0].reset();
                        }
                        
                        //swal('', data.Message, 'success');
                        $("#myModalrequestcallback").find("[data-dismiss]").click();

                        ////$("[data-dismiss]").click();
                        $("#modalThankYou").modal('show');
                    }
                    else {
                        swal('', data.Message, 'error');
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    hideWaitProcess()
                    ////$.bootstrapGrowl(errorThrown, { type: 'error', delay: 2000, });
                    swal('', errorThrown, 'error');
                }
            })
        }
    });
    $("#frmTalkExpert").submit(function (event) {
        event.preventDefault();
        if ($(this).valid()) {
            showWaitProcess()
            $.ajax({
                url: '/Package/TalktoExpert',
                method: "POST",
                data: $(this).serialize(),
                success: function (data) {

                    hideWaitProcess()

                    if (data.Status) {
                        //swal('', data.Message, 'success');
                        smartech('create', 'ADGMOT35CHFLVDHBJNIG50K969M2CP4N22UB4JPT4SRLRK91B7CG');

                        var formData = {
                            'pk^email': $("#frmTalkExpert #Email").val(),
                            'mobile': $("#frmTalkExpert #Mobile").val(),
                            'LEADSCORE': 20
                        };
                        smartech('contact', 75, formData);
                        $("#frmTalkExpert")[0].reset();
                        location.href = "/home/thankyou";
                    }
                    else {
                        swal('', data.Message, 'error');
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    hideWaitProcess()
                    swal('', errorThrown, 'error');
                }
            })
        }
    });
    $(".feedBackform").on("submit", function () {
        event.preventDefault();
        if ($(this).valid()) {
            showWaitProcess();
            $.ajax({
                url: '/home/contactus',
                method: "POST",
                data: $(this).serialize(),
                success: function (data) {

                    hideWaitProcess()

                    if (data.Status) {
                        swal('', data.Message, 'success');
                        smartech('create', 'ADGMOT35CHFLVDHBJNIG50K969M2CP4N22UB4JPT4SRLRK91B7CG');
                        var formData = {
                            'pk^email': $('[name="conemail"]').val(),
                            'FULLNAME': $('[name="confullname"]').val(),
                            'LEADSCORE': 10
                        };
                        smartech('contact', 9, formData);

                        $(".feedBackform")[0].reset();

                    }
                    else {
                        swal('', data.Message, 'error');
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    hideWaitProcess();
                    swal('', errorThrown, 'error');
                }
            })
        }



    })



});


//function Sign_Up() {
//    //var form = $('#formSignUp');
//    //form.data('validator', null);
//    //$.validator.unobtrusive.parse(form);   

//    //var form = $('#formSignUp');
//    //form.data('validator', null);
//    //$.validator.unobtrusive.parse(form);
//    //event.preventDefault();
//    //if ($(this).valid()) {
//    //showWaitProcess();
//    $.ajax({
//        url: '/user/register',
//        method: "POST",
//        data: $(this).serialize(),
//        success: function (data) {
//            hideWaitProcess();
//            if (data.Status) {
//                location.reload(true);
//            }
//            else {
//                swal('', data.Message, 'warning');
//            }
//        },
//        error: function (xhr, textStatus, errorThrown) {
//            hideWaitProcess();
//            swal('', errorThrown, 'error');
//        }
//    })
//    //}
//}
