using System.Threading.Tasks;
using ToolGood.TextFilter.Api.Datas;
using ToolGood.TextFilter.Api.Datas.Sys;

namespace ToolGood.TextFilter.Api.Interfaces
{
    public interface ISysProvider
    {
        /// <summary>
        /// 更新系统
        /// </summary>
        /// <param name="textFilterNoticeUrl">默认 文本检测异步地址</param>
        /// <param name="textReplaceNoticeUrl">默认 文本替换异步地址</param>
        /// <param name="skipword">自定义跳词</param>
        /// <returns></returns>
        CommonResult UpdateSystem(string textFilterNoticeUrl, string textReplaceNoticeUrl,string skipword);
        /// <summary>
        /// 更新系统
        /// </summary>
        /// <param name="textFilterNoticeUrl">默认 文本检测异步地址</param>
        /// <param name="textReplaceNoticeUrl">默认 文本替换异步地址</param>
        /// <param name="skipword">自定义跳词</param>
        Task<CommonResult> UpdateSystemAsync(string textFilterNoticeUrl, string textReplaceNoticeUrl, string skipword);
        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <returns></returns>
        CommonResult Refresh();
        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <returns></returns>
        Task<CommonResult> RefreshAsync();

        /// <summary>
        /// 产品信息
        /// </summary>
        /// <returns></returns>
        SysInfo Info();
        /// <summary>
        /// 产品信息
        /// </summary>
        /// <returns></returns>
        Task<SysInfo> InfoAsync();

        /// <summary>
        /// 更新许可
        /// </summary>
        /// <param name="lic"></param>
        /// <returns></returns>
        CommonResult UpdateLicence(string lic);
        /// <summary>
        /// 更新许可
        /// </summary>
        /// <param name="lic"></param>
        /// <returns></returns>
        Task<CommonResult> UpdateLicenceAsync(string lic);

        /// <summary>
        /// 重载数据
        /// </summary>
        /// <returns></returns>
        CommonResult InitData();
        /// <summary>
        /// 重载数据
        /// </summary>
        /// <returns></returns>
        Task<CommonResult> InitDataAsync();

        /// <summary>
        /// GC垃圾回收
        /// </summary>
        /// <returns></returns>
        CommonResult GCCollect();
        /// <summary>
        /// GC垃圾回收
        /// </summary>
        /// <returns></returns>
        Task<CommonResult> GCCollectAsync();
    }
}
