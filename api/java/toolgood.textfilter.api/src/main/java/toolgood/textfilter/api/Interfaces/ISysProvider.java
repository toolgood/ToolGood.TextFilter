package toolgood.textfilter.api.Interfaces;

import toolgood.textfilter.api.Datas.CommonResult;
import toolgood.textfilter.api.Datas.Sys.SysInfo;

public interface ISysProvider {

    /**
     * 更新系统
     * 
     * @param textFilterNoticeUrl    默认 文本检测异步地址
     * @param textReplaceNoticeUrl   默认 文本替换异步地址
     * @param skipword               自定义跳词
     * @return
     */
    CommonResult UpdateSystem(String textFilterNoticeUrl, String textReplaceNoticeUrl, String skipword);

    /**
     * 刷新缓存
     * 
     * @return
     */
    CommonResult Refresh();

    /**
     * 产品信息
     * 
     * @return
     */
    SysInfo Info();

    /**
     * 重载数据
     * 
     * @return
     */
    CommonResult InitData();

    /**
     * GC垃圾回收
     * 
     * @return
     */
    CommonResult GCCollect();

}
