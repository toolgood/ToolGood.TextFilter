using System;

namespace ToolGood.ReadyGo3.Attributes
{
    /// <summary>
    /// 列长度
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    class FieldLengthAttribute : Attribute
    {
        /// <summary>
        /// 列长度
        /// </summary>
        protected FieldLengthAttribute() { IsText = true; }

        /// <summary>
        /// 最大长度
        /// </summary>
        /// <param name="length">长度</param>
        public FieldLengthAttribute(int length)
        {
            IsText = false;
            FieldLength = length.ToString();
        }

        /// <summary>
        /// 适用字段
        /// </summary>
        /// <param name="length"></param>
        /// <param name="pointLength"></param>
        public FieldLengthAttribute(int length, int pointLength)
        {
            IsText = false;
            FieldLength = length.ToString() + "," + pointLength.ToString();
        }

        /// <summary>
        /// 是否TEXT
        /// </summary>
        public bool IsText;

        /// <summary>
        ///
        /// </summary>
        public string FieldLength;
    }
    /// <summary>
    /// 文本 长度无限
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    class TextAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// 文本类型
        /// </summary>
        public TextAttribute() : base()
        {
            IsText = true;
        }
    }

    /// <summary>
    /// 手机长度 20位
    /// </summary>
    class PhoneLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// 文本类型
        /// </summary>
        public PhoneLengthAttribute() : base(20)
        {
        }
    }

    /// <summary>
    /// 用户名长度 20位
    /// </summary>
    class UserNameLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// 用户名长度 20位
        /// </summary>
        public UserNameLengthAttribute() : base(20)
        {
        }
    }

    /// <summary>
    /// 密码长度 32位
    /// </summary>
    class PasswrodLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// 密码长度 32位
        /// </summary>
        public PasswrodLengthAttribute() : base(32)
        {
        }
    }

    /// <summary>
    /// 注释长度 500位
    /// </summary>
    class CommentLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// 注释长度 500位
        /// </summary>
        public CommentLengthAttribute() : base(500)
        {
        }
    }

    /// <summary>
    /// GUID长度 40位
    /// </summary>
    class GuidLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// GUID长度 40位
        /// </summary>
        public GuidLengthAttribute() : base(40)
        {
        }
    }

    /// <summary>
    /// Url长度 200位
    /// </summary>
    class UrlLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// Url长度 200位
        /// </summary>
        public UrlLengthAttribute() : base(200)
        {
        }
    }

    /// <summary>
    /// 标题长度 100位
    /// </summary>
    class TitleNameLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// 标题长度 100位
        /// </summary>
        public TitleNameLengthAttribute() : base(100)
        {
        }
    }

    /// <summary>
    /// 短名称 50位
    /// </summary>
    class ShortNameLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// 短名称 50位
        /// </summary>
        public ShortNameLengthAttribute() : base(50)
        {
        }
    }

    /// <summary>
    /// Ip地址长度 46位
    /// </summary>
    class IpLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// Ip地址长度 46位
        /// </summary>
        public IpLengthAttribute() : base(46)
        {
        }
    }

    /// <summary>
    /// UserAgent长度 250位
    /// </summary>
    class UserAgentLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// UserAgent长度 250位
        /// </summary>
        public UserAgentLengthAttribute() : base(250)
        {
        }
    }

    /// <summary>
    /// Email地址长度 50位
    /// </summary>
    class EmailLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// Email地址长度 50位
        /// </summary>
        public EmailLengthAttribute() : base(50)
        {
        }
    }

    /// <summary>
    /// 标签 长度 500位
    /// </summary>
    class TagsLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// 标签 长度 500位
        /// </summary>
        public TagsLengthAttribute() : base(500)
        {
        }
    }

    /// <summary>
    /// MAC 地址 18位
    /// </summary>
    class MacAddressLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// MAC 地址 18位
        /// </summary>
        public MacAddressLengthAttribute() : base(18)
        {
        }
    }

    /// <summary>
    /// 错误信息长度 200位
    /// </summary>
    class ErrorMessageLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// 错误信息长度 200位
        /// </summary>
        public ErrorMessageLengthAttribute() : base(200)
        {
        }
    }

    /// <summary>
    /// ParentIds 长度 250位
    /// </summary>
    class ParentIdsLengthAttribute : FieldLengthAttribute
    {
        /// <summary>
        /// ParentIds 长度 250位
        /// </summary>
        public ParentIdsLengthAttribute() : base(250)
        {
        }
    }

}