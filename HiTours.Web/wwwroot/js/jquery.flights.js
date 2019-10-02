$(function (e) {
    var loaderTemplate = '<div class="loader"><img src="/images/ajax-loader.gif" /></div>';
    $(document).on('click', '.showmore.show-moreless', function (e) {
        var flightIndex = $(this).data('flightindex');
        var isLcc = $(this).data('lcc');
        var traceId = $(this).data('traceid');
        var tokenId = $(this).data('tokenid');
        var length = $(this).data('length');
        var targetIdentifier = $(this).data('target-identifier');
        $('#baggage_' + targetIdentifier).html(loaderTemplate);
        $.post('/Deal/GetBaggage',
            {
                'flightIndex': flightIndex,
                'isLcc': isLcc,
                'traceId': traceId,
                'tokenId': tokenId,
                'length': length
            },
            function (result) {
                $('#baggage_' + targetIdentifier).html(result);
            }
        );
        //$('#cancel_' + targetIdentifier).html(loaderTemplate);
        //$.post('/Deal/GetCancellation',
        //    {
        //        'flightIndex': flightIndex,
        //        'isLcc': isLcc,
        //        'traceId': traceId,
        //        'tokenId': tokenId,
        //        'length': length
        //    },
        //    function (result)
        //    {

        //    }
        //);
    });

});