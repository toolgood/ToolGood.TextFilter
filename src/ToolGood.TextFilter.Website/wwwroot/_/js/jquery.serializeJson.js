/*!
 * Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved.
 * GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html  
 */
(function (factory) {
    if (typeof define === 'function' && define.amd) { // AMD. Register as an anonymous module.
        define(['jquery'], factory);
    } else if (typeof exports === 'object') { // Node/CommonJS
        var jQuery = require('jquery');
        module.exports = factory(jQuery);
    } else { // Browser globals (zepto supported)
        factory(window.jQuery || window.Zepto || window.$); // Zepto supported on browsers as well
    }
}(function ($) {
    function standardSearchText(str) {
        // \u202D\u202c 从excel表格直接复制到input框
        // \u0085 代表下一行的字符
        // \u00A0 不换行空格 相当与  看上去和空格一样，但是在HTML中不自动换行， 主要用在office中,让一个单词在结尾处不会换行显示,快捷键ctrl+shift+space ;
        // \u2028 行分隔符
        // \u2029 段落分隔符
        // \uFEFF 字节顺序标记(零宽非连接符)
        // \u200E 从左至右书写标记
        // \u200F 从右至左书写标记
        // \u200D 零宽连接符
        // \u2006 另一种空格符
        // \u3000 全角空格(中文符号)
        str = str.replace(/[\x00-\x1F\x7f]/ig, '');// ASCII码 不可见字符
        str = str.replace(/[\u00A0\u1680\u2000-\u200a\u202F\u205F\u3000]/ig, ' ');// 换成普通空格符
        return str.replace(/[\u180E\u200b-\u200f\u2028-\u202e\u2060\uFEFF]/ig, '').trim(); //清空两端空格
    }
    function standardText(str) {
        str = str.replace(/[\x00-\x08\x0B\x0C\x0E-\x1F\x7f\u0085]/ig, '');// ASCII码 排除 \t \r \n
        str = str.replace(/[\u00A0\u1680\u2000-\u200a\u202F\u205F\u3000]/ig, ' ');// 换成普通空格符
        return str.replace(/[\u180E\u200b-\u200f\u2028-\u202e\u2060\uFEFF]/ig, '').trim(); //清空两端空格
    }
    /**
     * 序列化 
     * @param {any} type 0、直接获取文本，1、获取文本格式化，2、获取文本格式化 去除 \t\r\n
     */
    $.fn.serializeJson = function (type) {
        var serializeObj = {};
        var map = {};
        var array = this.serializeArray();
        var txtFunc = standardText;
        if (type) {
            if (type == 0) {
                txtFunc = getText;
            } else if (type == 1) {
                txtFunc = standardText;
            } else if (type == 2) {
                txtFunc = standardSearchText;
            }
        }
        $(array).each(function () {
            var names = splitName(this.name);
            setData(serializeObj, map, names, 0, this.value, txtFunc);
        })
        return serializeObj;
    };
    /**
     * 去除禁用
     * */
    $.fn.removeDisabled = function () {
        $(this).prop("disabled", false);
        $(this).removeClass("layui-disabled");
        $(this).removeClass("disabled");

    }
    /**
     * 禁用
     * */
    $.fn.addDisabled = function () {
        $(this).prop("disabled", true);
        $(this).addClass("layui-disabled");
        $(this).addClass("disabled");
    }

    function setData(tarData, map, names, index, value, txtFunc) {
        var name = names[index];
        if (index == names.length - 1) {
            if (name.isArray) {
                if (tarData[name.name] == null) { tarData[name.name] = new Array(); }
                setValue(tarData, name.name, value, txtFunc);
            } else {
                setValue(tarData, name.name, value, txtFunc);
            }
        } else if (name.isArray && name.hasTag) {
            if (tarData[name.name] == null) { tarData[name.name] = new Array(); }
            if (map[name.allPath] == null) {
                var obj = {};
                tarData[name.name].push(obj);
                map[name.allPath] = obj;
            }
            setData(map[name.allPath], map, names, index + 1, value, txtFunc);
        } else if (name.isArray) {
            console.log("input name is error, no tag, set the input name like 'a[tag].b' ");
            if (tarData[name.name] == null) { tarData[name.name] = new Array(); }
            setValue(tarData, name.name, value, txtFunc);
        } else {
            if (tarData[name.name] == null) { tarData[name.name] = {}; }
            setData(tarData[name.name], map, names, index + 1, value, txtFunc);
        }
    }
    function setValue(tarData, name, value, txtFunc) {
        if (tarData[name] == null || tarData[name] == undefined) {
            tarData[name] = txtFunc(value);
        } else if ($.isArray(tarData[name])) {
            tarData[name].push(txtFunc(value));
        } else {
            tarData[name] = [tarData[name], txtFunc(value)];
        }
    }
    function splitName(txt) {
        var ts = txt.split(".");
        var names = [];
        var path = "";
        for (var i = 0; i < ts.length; i++) {
            names.push(getName(ts[i], path));
            path += ts[i] + ".";
        }
        return names;
    }
    function getName(txt, path) {
        var m = /^(.*)\[([^\]]+)\]$/.exec(txt);
        if (m != null) {
            var dataName = m[1];
            return { isArray: true, hasTag: true, name: dataName, allPath: path + "." + txt };
        }
        var m2 = /^(.*)\[\]$/.exec(txt);
        if (m2 != null) {
            var dataName = m2[1];
            return { isArray: true, hasTag: false, name: dataName };
        }
        return { isArray: false, name: txt };
    }

    function getText(str) {
        return str;
    }
}));