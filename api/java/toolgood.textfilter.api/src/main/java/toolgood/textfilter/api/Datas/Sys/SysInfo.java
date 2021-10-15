package toolgood.textfilter.api.Datas.Sys;

public class SysInfo {
    /**
     * 返回码：0) 成功，1) 失败
     */
    public int code;
    /**
     * 返回码详情描述
     */
    public String message;

    /**
     * 名称
     */
    public String name;

    /**
     * 版本号
     */
    public String version;

    /**
     * 机器码
     */
    public String machineCode;

    /**
     * 是否注册
     */
    public String isRegister;

    /**
     * 服务开始时间
     */
    public String serviceStart;

    /**
     * 服务结束时间
     */
    public String serviceEnd;

    /**
     * 注册码
     */
    public String licenceTxt;
}
