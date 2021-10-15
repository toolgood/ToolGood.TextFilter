/*!
 * layui-v2.5.7 MIT License
 */
layui.define(['layer', 'laypage', 'table'], function (exports) {
    // var $ = layui.jquery;
    var layer = layui.layer;
    var table = layui.table;
    var laypage = layui.laypage;

    var treetable = {
        // 渲染树形表格
        render: function (param) {
            // 检查参数
            if (!treetable.checkParam(param)) {
                return;
            }
            // 获取数据
            if (param.data) {
                treetable.init(param, param.data);
            } else {
                this.reload(param, param);
            }
        },
        reload: function (p1, p2) {
            var param = $.extend({}, p1, p2)
            param.where = param.where || {};
            var data = param.where || {};

            if (param.page) {
                if (data[param.request.pageName] == undefined) {
                    data[param.request.pageName] = 1;
                    param.where[param.request.pageName] = 1;
                }
                if (data[param.request.limitName] == undefined) {
                    data[param.request.limitName] = param.limit;
                    param.where[param.request.limitName] = param.limit;
                }
            }

            var headers = {};
            $.ajax({
                type: param.method,
                url: param.url,
                contentType: "application/json;charset=UTF-8",
                data: JSON.stringify(data),
                dataType: "JSON",
                headers: headers,
                success: function (res, textStatus, jqXHR) {
                    if (res.data) {
                        treetable.init(param, res.data, res.count);
                    } else {
                        param.url = undefined;
                        param.data = [];
                        param.height = 300;
                        param.text = { none: '<span>'+res.message+'</span>' };
                        table.render(param);
                    }
                },
            });
        },

        // 渲染表格
        init: function (param, data, datacount) {
            var mData = [];
            var doneCallback = param.done;
            var tNodes = data;
            var $Url = param.url;
            var usepage = param.page;

            // 补上id和pid字段
            for (var i = 0; i < tNodes.length; i++) {
                var tt = tNodes[i];
                if (!tt.id) {
                    if (!param.treeIdName) {
                        layer.msg('参数treeIdName不能为空', { icon: 5 });
                        return;
                    }
                    tt.id = tt[param.treeIdName];
                }
                if (!tt.pid) {
                    if (!param.treePidName) {
                        layer.msg('参数treePidName不能为空', { icon: 5 });
                        return;
                    }
                    tt.pid = tt[param.treePidName];
                }
            }

            // 对数据进行排序
            var sort = function (s_pid, data) {
                for (var i = 0; i < data.length; i++) {
                    if (data[i].pid == s_pid) {
                        var len = mData.length;
                        if (len > 0 && mData[len - 1].id == s_pid) {
                            mData[len - 1].isParent = true;
                        }
                        mData.push(data[i]);
                        sort(data[i].id, data);
                    }
                }
            };
            sort(param.treeSpid, tNodes);

            // 重写参数

            param.url = undefined;
            param.data = mData;
            param.page = { count: mData.length, limit: mData.length };

            if (param.cols.length > 0) {
                param.cols[0][param.treeColIndex].templet = function (d) {
                    var mId = d.id;
                    var mPid = d.pid;
                    var isDir = d.isParent;
                    var emptyNum = treetable.getEmptyNum(mPid, mData);
                    var iconHtml = '';
                    for (var i = 0; i < emptyNum; i++) {
                        iconHtml += '<span class="treeTable-empty"></span>';
                    }
                    if (isDir) {
                        iconHtml += '<i class="layui-icon layui-icon-triangle-d"></i> <i class="layui-icon layui-icon-layer"></i>';
                    } else {
                        iconHtml += '<i class="layui-icon layui-icon-file"></i>';
                    }
                    iconHtml += '&nbsp;&nbsp;';
                    var ttype = isDir ? 'dir' : 'file';
                    var vg = '<span class="treeTable-icon open" lay-tid="' + mId + '" lay-tpid="' + mPid + '" lay-ttype="' + ttype + '">';
                    return vg + iconHtml + d[param.cols[0][param.treeColIndex].field] + '</span>'
                };
            }
            var $this = this;
            param.done = function (res, curr, count) {
                $(param.elem).next().addClass('treeTable');
                $('.treeTable .layui-table-page').css('display', 'none');
                $(param.elem).next().attr('treeLinkage', param.treeLinkage);
                // 绑定事件换成对body绑定
                /*$('.treeTable .treeTable-icon').click(function () {
                    treetable.toggleRows($(this), param.treeLinkage);
                });*/
                if (usepage) {
                    if ($(param.elem).next().children(".layui-treetable-page").length == 0) {
                        var html = '<div class="layui-table-page layui-treetable-page" id="' + param.elem.replace("#", "") + '-treetable-page"></div>';
                        $(html).appendTo($(param.elem).next());
                    }
                    laypage.render({
                        elem: param.elem.replace("#", "") + '-treetable-page',
                        limits: param.limits || [10, 20, 30, 40, 50, 60, 70, 80, 90],
                        limit: param.where[param.request.limitName],
                        groups: 3,
                        count: datacount,
                        curr: param.where[param.request.pageName],
                        prev: '<i class="layui-icon">&#xe603;</i>',
                        next: '<i class="layui-icon">&#xe602;</i>',
                        layout: ['prev', 'page', 'next', 'skip', 'count', 'limit'],
                        jump: function (obj, first) {
                            if (!first) {
                                //分页本身并非需要做以下更新，下面参数的同步，主要是因为其它处理统一用到了它们
                                //而并非用的是 options.page 中的参数（以确保分页未开启的情况仍能正常使用）
                                param.where[param.request.pageName] = obj.curr; //更新页码
                                param.where[param.request.limitName] = obj.limit; //更新每页条数
                                param.page = true;
                                param.url = $Url;
                                delete (param.data);
                                $this.reload(param, param);
                            }
                        }
                    });
                    $('.layui-treetable-page').css('display', '');
                }
                if (param.treeDefaultClose) {
                    treetable.foldAll(param.elem);
                }
                if (doneCallback) {
                    doneCallback(res, curr, count);
                }
            };

            // 渲染表格
            table.render(param);
        },
        // 计算缩进的数量
        getEmptyNum: function (pid, data) {
            var num = 0;
            if (!pid) {
                return num;
            }
            var tPid;
            for (var i = 0; i < data.length; i++) {
                if (pid == data[i].id) {
                    num += 1;
                    tPid = data[i].pid;
                    break;
                }
            }
            return num + treetable.getEmptyNum(tPid, data);
        },
        // 展开/折叠行
        toggleRows: function ($dom, linkage) {
            var type = $dom.attr('lay-ttype');
            if ('file' == type) {
                return;
            }
            var mId = $dom.attr('lay-tid');
            var isOpen = $dom.hasClass('open');
            if (isOpen) {
                $dom.removeClass('open');
            } else {
                $dom.addClass('open');
            }
            $dom.closest('tbody').find('tr').each(function () {
                var $ti = $(this).find('.treeTable-icon');
                var pid = $ti.attr('lay-tpid');
                var ttype = $ti.attr('lay-ttype');
                var tOpen = $ti.hasClass('open');
                var index = $(this).attr('data-index');
                if (mId == pid) {
                    if (isOpen) {
                        $dom.parents(".treeTable").find("tr[data-index='" + index + "']").hide();
                        // $(this).hide();
                        if ('dir' == ttype && tOpen == isOpen) {
                            $ti.trigger('click');
                        }
                    } else {
                        $dom.closest(".treeTable").find("tr[data-index='" + index + "']").show();
                        // $(this).show();
                        if (linkage && 'dir' == ttype && tOpen == isOpen) {
                            $ti.trigger('click');
                        }
                    }
                }
            });
        },
        // 检查参数
        checkParam: function (param) {
            if (!param.treeSpid && param.treeSpid != 0) {
                layer.msg('参数treeSpid不能为空', { icon: 5 });
                return false;
            }

            if (!param.treeColIndex && param.treeColIndex != 0) {
                layer.msg('参数treeColIndex不能为空', { icon: 5 });
                return false;
            }
            return true;
        },
        // 展开所有
        expandAll: function (dom) {
            $(dom).next('.treeTable').find('.layui-table-body tbody tr').each(function () {
                var $ti = $(this).find('.treeTable-icon');
                var ttype = $ti.attr('lay-ttype');
                var tOpen = $ti.hasClass('open');
                if ('dir' == ttype && !tOpen) {
                    $ti.trigger('click');
                }
            });
        },
        // 折叠所有
        foldAll: function (dom) {
            $(dom).next('.treeTable').find('.layui-table-body tbody tr').each(function () {
                var $ti = $(this).find('.treeTable-icon');
                var ttype = $ti.attr('lay-ttype');
                var tOpen = $ti.hasClass('open');
                if ('dir' == ttype && tOpen) {
                    $ti.trigger('click');
                }
            });
        },
        on: function (filter, fun) {
            table.on(filter, fun);
        }
    };

    // 给图标列绑定事件
    $('body').on('click', '.treeTable .treeTable-icon', function () {
        var treeLinkage = $(this).parents('.treeTable').attr('treeLinkage');
        if ('true' == treeLinkage) {
            treetable.toggleRows($(this), true);
        } else {
            treetable.toggleRows($(this), false);
        }
    });

    exports('treetable', treetable);
});
