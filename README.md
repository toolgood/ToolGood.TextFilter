# 《ToolGood 内容审核系统》

官网：https://toolgood.com/


《ToolGood 内容审核系统》由多组高性能算法组成：
- （1）在`ToolGood.Words`高性能算法基础上再次改进，十几处修改，性能更高；
- （2）优化`繁体与简体`、`全角半角`、`英文大小写`匹配性能；
- （3）改良算法，在`不减性能`情况下，压缩字典量，减少内存使用量；
- （4）加入`多组敏感词检测`，减少误杀；
- （5）加入`NLP分词功能`减少误杀概率，NLP算法使用`动态规划`，功能增加性能不降多少；
- （6）改良`联系方式匹配`，减少跳词的误测； 

## 文件夹说明
- `src`文件夹:  ToolGood.TextFilter源码，`C#`语言。
- `dataBuilder`文件夹:  程序数据包生成器源码，`C#`语言。
- `api`文件夹:  API接口源码，目前有`C#`、`JAVA`、`Python`、`GO`版本。
- `manager`文件夹: 敏感词库管理工具源码，(开发中)

注：`C#`语言编译环境`VS2022`，核心源码只有`C#`语言版本，其他语言本人并不精通。

## 相关下载
- 编译后程序下载（非异步版）： https://pan.baidu.com/s/1FLH7U3Nw2zE0Q3Vb-GtWZg?pwd=bqgn 
- 测试数据包（只有谩骂识别）：https://pan.baidu.com/s/12JjvSG1lAifdpRHH1GL1gA?pwd=tofy
- 相关文档：https://toolgood.com/FAQ
- 常见问题解答：https://github.com/toolgood/ToolGood.TextFilter/issues/4

## 领取【敏感词库】
- 该敏感词库从`70W多词组`中整理出来的，外加18W多组常用分词、2W多字的拼音扩展、14066个左右拆字、4000组繁简转化，4850组同音同形字，1160组异形字…… 
- 免费领取方法： 加QQ`1665690808`，发送申请人照片，要求手执【免费领取敏感词库】纸条，并且站在带公司Logo的墙前（或学校大门口）。
-
- **如不想拍照，可花200元赞助【敏感词库】，并赠送【程序数据包】[赞助网址](https://mbd.pub/o/bread/mbd-YpaXmZdv)**。
-
- 为什么会有免费领取【敏感词库】？因为一套敏感词库无法满足各种场景，而每个公司使用的场景又不同。如`你妈`，在熟人环境下是`正常词`，在商品评价绝对是`脏词`，在游戏中绝大数为`脏词`。


本人不是老师，加我QQ后，请不要提关于项目使用、加载等简单问题，也不要问词库有多少条敏感词（因为使用类正则，能匹配上十亿条）。

常见问题解答：https://github.com/toolgood/ToolGood.TextFilter/issues/4

## 特别声明
源码为GPL-3.0 许可，商业用途请购买商业许可，商业授权费1000元

购买商业许可后，二次开发及相关代码可以不开源。

购买步骤：

1、进入面包多购买 https://mbd.pub/o/bread/YpaXmZdw

2、将公司名称、组织机构代码、授权码、面包多订单号发送给我。
-    a) 邮件发送至toolgood@qq.com
-    b) 加我QQ1665690808

商业授权名单：https://github.com/toolgood/ToolGood.TextFilter/issues/3


开票说明：
- 金额超过1000元，可开票。
-    面包多上其他产品，需提供购买截图。

### 探讨敏感信息过滤：

敏感信息过滤研究会，Q群：128994346（已满）

本人不是老师，请不要提关于项目使用、加载等简单问题。

## 敏感词相关文章
1、[敏感词过滤方案那些事](https://www.cnblogs.com/toolgood/p/15208734.html)

2、[普通公司敏感词审核制度](https://www.cnblogs.com/toolgood/p/15213549.html)

3、[新人小白过滤敏感词方案](https://www.cnblogs.com/toolgood/p/15251918.html)

4、[网络常用敏感词过滤方法](https://www.cnblogs.com/toolgood/p/15261554.html)

5、[ToolGood.Words算法过滤敏感词优化原理](https://mbd.pub/o/bread/YZ2Yk5hy)  （收费30元，一顿KFC）

6、[ToolGood.TextFilter开源代码优化详解](https://mbd.pub/o/bread/YpWXlp9u)  （收费300元）
与IllegalWordsSearch算法进行对比，阐述了ToolGood.TextFilter过滤算法优化点，如何减少内存使用量。
还有一小部分未写好，心急的人可以先买，我会持续更新。

7、[正则转DFA算法（C#版、JAVA版）](https://mbd.pub/o/bread/Y5ubl5w=) （收费30元，一顿KFC）
ToolGood.TextFilter的一个核心算法就使用到正则转DFA。（目前只有C#代码）

8、JAVA版ToolGood.TextFilter

9、[C#版图片鉴黄](https://mbd.pub/o/bread/mbd-YZ2Yk5hw)（收费30元，一顿KFC）

## LICENSE
>您可以在GPLv3许可证下使用它。请参阅LICENSE。

## 吐槽
>代码写了那么多年，还没50行通达信代码价值高。。。


