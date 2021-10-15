/*! 
 * Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved.
 * GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
function openWindow(title, url, success, end) {
    function QueryString(val, url) {
        var re = new RegExp("" + val + "=([^&?#]*)", "ig");
        return ((url.match(re)) ? (decodeURI(url.match(re)[0].substr(val.length + 1))) : '');
    }

    var w = QueryString('w', url), h = QueryString('h', url);
    w = ((w == null || w == '')) ? ($('body').width() - 100) : w;
    h = ((h == null || h == '')) ? ($('body').height() - 100) : h;
    if ($('body').width() * 0.95 < w) { w = $('body').width() * 0.95; }
    if ($('body').height() * 0.95 < h) { h = $('body').height() * 0.95; }

    layui.layer.open({
        type: 2,
        area: [w + 'px', h + 'px'],
        fix: true,
        shade: 0.4,
        title: title,
        anim: -1,
        content: [url],
        scrollbar: true,
        shadeClose: false,
        maxmin: true,
        success: function (layero, index) {
            success && success(layero, index, window[layero.find('iframe')[0]['name']]);
        },
        end: function () { end && end(); }
    });
}

/**
 * 关闭窗口并刷新上一级页面
*/
function closeWindowAndReload(funName, index) {
    if (window != top) {
        index = parent.layer.getFrameIndex(window.name);
        top.closeWindowAndReload(funName, index);
        parent.layer.close(index);
    } else {
        var $ = jQuery;
        //判断是否有上级的弹层，如果有，就刷新上级弹层
        if ($("#layui-layer" + index).prevAll('.layui-layer').length > 0) {
            var id = $($("#layui-layer" + index).prevAll('.layui-layer')[0]).attr('id').replace('layui-layer', '');
            var target = $('#layui-layer-iframe' + id);
            if (funName && target[0].contentWindow[funName]) {
                target[0].contentWindow[funName]();
            } else if (target[0].contentWindow.reload) {
                target[0].contentWindow.reload();
            } else {
                target[0].contentWindow.location.reload();
            }
        } else {
            $(".sub-window-container").each(function () {
                if ($(this).css("display") != "none") {
                    var target = $(this).children("iframe");
                    if (funName && target[0].contentWindow[funName]) {
                        target[0].contentWindow[funName]();
                    } else if (target[0].contentWindow.reload) {
                        target[0].contentWindow.reload();
                    } else {
                        target[0].contentWindow.location.reload();
                    }
                }
            })
        }
        parent.layer.close(index);
    }
}
/**
 * 关闭窗口
*/
function closeWindow() {
    var index = parent.layer.getFrameIndex(window.name);
    parent.layer.close(index);
}

$(document).on("click", ".openwin", function (event) {
    event && event.stopPropagation();
    $(this).prop("disenable", true);
    var t = $(this).attr("title") || $(this).attr("name") || $(this).text();
    var a = $(this).attr("href") || $(this).attr("src") || $(this).attr("alt");

    $(this).each(function () {
        $.each(this.attributes, function () {
            if (this.specified && this.name.indexOf("data-") == 0) {
                a += a.indexOf("?") >= 0 ? "&" : "?";
                a += this.name.replace("data-", "") + "=" + encodeURI(this.value);
            }
        });
    });
    openWindow(t, a, null, null)
    $(this).prop("disenable", false);
    return false;
});