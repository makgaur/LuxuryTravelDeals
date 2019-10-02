
$(function () {

    $('body').on('click', '[data-description-click]', function () {
        var id = $(this).data('description-click');
        var value = $('[data-description="' + id + '"]').val();
        var html = '';
        html += '<div class="row">';
        html += '   <div class="col-md-12">';
        html += '      <div class="box no-top-border">';
        html += '         <div class="form-group col-sm-12 view-image-btn">';
        html += '            <label></label>';
        html += '            <textarea class="tinymceTextarea" data-area-desc="' + id + '" placeholder="Place some text here" style="width: 100%; height: 100px; font-size: 14px; line-height: 18px; border: 1px solid #dddddd; padding: 10px;">';
        html += value + '</textarea>';
        html += '         </div>';
        html += '      </div>';
        html += '   </div>';
        html += '<div class="box-footer" align="center"><span type="button" data-save-desc="'+id+'" class="btn btn-primary">OK</span></div>';
        html += '</div>';


        $('#dvModalDynmic').find(".modal-title").html('Room Type Description')
        $('#dvModalDynmic').find(".modal-body").html(html);
        $('#dvModalDynmic').css('z-index', '9999');
        $('#dvModalDynmic').modal('show');
        initTinymce();
    });

    $('body').on('click', '[data-save-desc]', function () {
        var id = $(this).data("save-desc");
        var value = tinyMCE.activeEditor.getContent(); //$('[data-area-desc="' + id + '"]').val();
        $('[data-description="' + id + '"]').val(value);
        $('#dvModalDynmic').modal('hide');
    });

});