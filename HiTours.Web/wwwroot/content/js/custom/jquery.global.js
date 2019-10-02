function showWaitProcess(oSettings) {
    oSettings = jQuery.extend({ Text: 'Please Wait...', Effect: 'facebook', background: 'rgba(160, 160, 160, 0.48)', ColorCode: '#000', SizeW: '', SizeH: '', id: 'dvLoading' }, oSettings);
    var $element = $('#' + oSettings.id);
    if ($element != undefined) {
        $element.removeAttr('class').hide().children().remove();
        $element.css({ 'width': '100%', 'height': '100%', 'position': 'absolute', 'top': '0', 'left': '0', 'z-index': '99999', 'display': 'none' });
        $element.waitMe({
            effect: oSettings.Effect,
            text: oSettings.Text,
            bg: oSettings.background,
            color: oSettings.ColorCode,
            sizeW: oSettings.SizeW,
            sizeH: oSettings.SizeH
        });
        $element.show();
    }
};

function hideWaitProcess() {
    $('#dvLoading').removeAttr('class').hide().children().remove();
};

$.extend({
    getUrlVars: function () {
        var vars = [], hash;
        if (window.location.search.substring(1).length >= 1) {
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
        }
        return vars;
    },
    getUrlVar: function (name) {
        return $.getUrlVars()[name];
    },
    clearURLVar: function () {
        history.pushState("", document.title, window.location.pathname);
    }
});


; (function ($, window, document) {
    var pluginName = 'checkAll';

    var defaults = {
        container: document,
        childCheckboxes: 'input[type=checkbox]',
        showIndeterminate: false
    };

    function checkAll(element, options) {
        this.$el = $(element);
        this.options = $.extend({}, defaults, this.$el.data(), options);
        this.init();
    }

    checkAll.prototype.init = function () {
        this._checkChildren();

        var plugin = this;

        this.$el.on('change', function (e) {
            var $children = $(plugin.options.childCheckboxes, plugin.options.container).not(plugin.$el);
            $children.prop('checked', $(this).prop('checked'));
        });

        $(this.options.container).on('change', plugin.options.childCheckboxes, function (e) {
            plugin._checkChildren();
        });
    };

    $.fn[pluginName] = function (options) {
        return this.each(function () {
            if (!$.data(this, 'plugin_' + pluginName)) {
                $.data(this, 'plugin_' + pluginName,
                    new checkAll(this, options));
            }
        });
    }

    checkAll.prototype._checkChildren = function () {
        var totalCount = $(this.options.childCheckboxes, this.options.container).not(this.$el).length;
        var checkedCount = $(this.options.childCheckboxes, this.options.container)
            .filter(':checked').not(this.$el).length;

        var indeterminate = this.options.showIndeterminate &&
            checkedCount > 0 && checkedCount < totalCount;

        this.$el.prop('indeterminate', indeterminate);
        this.$el.prop('checked', checkedCount === totalCount && totalCount !== 0);
    }

})(jQuery, window, document);