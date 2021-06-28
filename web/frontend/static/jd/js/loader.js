var loader = window.loader || {};

loader.show = function(opts) {
    loaderMethod('show', opts)
}

loader.hide = function(opts) {
    loaderMethod('hide', opts)
}

function loaderMethod(action, opts) {
    $loader = $('#cat-loader');
    opts = opts || {
        text: 'loading...'
    };
    $loader.find('.t').text(opts.text);
    action == 'show' ? $loader.show() : $loader.hide();
}

function loaderInit() {
    var html = '';
    html += '<link rel="stylesheet" type="text/css" href="./static/jd/css/loader.css" />';
    html += '<div id="cat-loader" style="display:none;">';
    html += '    <div class="mask"></div>';
    html += '    <div class="loader-area">';
    html += '        <div class="loader">';
    html += '            <div class="loader-inner ball-spin-fade-loader">';
    html += '                <div></div>';
    html += '                <div></div>';
    html += '                <div></div>';
    html += '                <div></div>';
    html += '                <div></div>';
    html += '                <div></div>';
    html += '                <div></div>';
    html += '                <div></div>';
    html += '            </div>';
    html += '        </div>';
    html += '        <div class="t">';
    html += '            loading...';
    html += '        </div>';
    html += '    </div>';
    html += '</div>';
    $('body').append(html);
}

$(function() {
    loaderInit();
});