package toolgood.textfilter.api.Datas.Texts;

public class TextFilterContactItem {

    /**
     * 联系方式类型 1) 账号，2）邮箱，3）网址，4）手机号， 5) QQ号, 6) 微信号, 7) Q群号
     */
    public String contactType;
    /**
     * 联系方式串
     */
    public String contactString;
    /**
     * 联系方式串位置，例：1,3,5,7-12,15
     */
    public String position;

}
