using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToolGood.ReadyGo3;
using ToolGood.TextFilter.Core;
using ToolGood.TextFilter.DataBuilder2.Datas;
using ToolGood.TextFilter.Datas;
using ToolGood.Words;

namespace ToolGood.TextFilter.DataBuilder2
{
    partial class Startup
    {
        private const string _tempTranslateDictPath = "temp/TranslateDict.dat";
        private const string _tempFenciPath = "temp/Fenci.dat";
        private const string _tempFenciKeywordPath = "temp/FenciKeyword.dat";
        private const string _tempFenciKeywordTextPath = "temp/FenciKeyword.txt";


        #region CreateTranslateDict
        static CharDictionary CreateTranslateDict(SqlHelper helper)
        {
            Dictionary<char, char> dict = new Dictionary<char, char>();
            var extends = helper.Select<DbTxtExtend>("where TxtExtendTypeId in (1,2) ");// 英文数字 繁体简体

            var simplifiedChinese1 = new HashSet<string>();
            var simplifiedChinese2 = new HashSet<string>();
            var simplifiedChinese3 = new HashSet<string>();
            #region 查找
            var t = "咄疆嘴换一丁七十万四丈三上下不与丑专且世丘丙业丛东丝丢两严丧个中丰串临丸丹为主丽举乃久么义之乌乎乏乐乒乓乔乖乘乙九乞也习乡书买乱乳了予争事二于亏云互五井亚些亡交亦产亩享京亭亮亲人亿什仁仅仆仇今介仍从仓仔他仗付仙代令以仪们仰件价任份仿企伍伏伐休众优伙会伞伟传伤伪伯估伴伶伸似但位低住体何余佛作你佣佩佳使侄例侍供依侦侧侨侮侵便促俊俗俘保信俩俭修俯俱倍倒倘候倚借倡倦债值倾假偏做停健偶偷偿傅傍储催傲傻像僚儿允元兄充兆先光克免兔党入全八公六兰共关兴兵其具典养兼兽内冈册再冒写军农冠冤冬冰冲决况冶冷冻净准凉减凑几凡凤凭凯凶出击刀刃分切刊刑划列刘则刚创初删判利别刮到制刷券刺刻剂剃削前剑剖剥剧剩剪副割力劝办功加务劣动助努劫励劲劳势勇勉勒勤勺勾勿匀包匆化北匙匠匪匹区医十千升午半华协单卖南博卜占卡卧卫印危即却卵卷卸厂厅历厉压厌厕厘厚原厦厨去县参又叉及友双反发叔取受变叙叛叠口古句另叨只叫召叮可台史右叶号司叹叼吃各合吉吊同名后吐向吓吗君吞否吧吨吩含听启吴吵吸吹吼呀呆呈告员呜呢周味呼命和咏咐咨咬咱咳咸咽哀品哄哈响哑哗哥哨哪哭哲唇唉唐唤售唯唱啄商啊啦喂善喇喉喊喘喜喝喷嗓嗽嘉四回因团园困围固国图圆圈土圣在地场圾址均坊坏坐坑块坚坛坝坟坡坦垂垃垄型垒垦垫垮埋城域培基堂堆堡堤堪堵塌塑塔塘塞填境墓墙士壮声壳壶处备复夏夕外多夜够大天太夫央失头夸夹夺奇奉奋奏奔奖套奥女奴奶奸她好如妄妇妈妖妙妥妨妹妻始姐姑姓委姜姥姨姻姿威娃娇娘娱婆婚婶嫁嫂嫌子孔孕字存孙孝孟季孤学孩宁它宅宇守安宋完宏宗官宙定宜宝实审客宣室宪宫宰害宴宵家容宽宾宿寄密寇富寒寸对寺寻导寿封射将尊小少尖尘尚尝尤就尸尺尼尽尾尿局层居屈届屋屑展属屠屡屯山屿岁岂岔岗岛岩岭岸峡峰崇崖崭川州巡工左巧巨巩差己已巴巷巾币市布帅帆师希帐帖帘帜帝带席帮常帽幅幕干平年并幸幻幼广庄庆床序库应底店庙府废度座庭康庸廉廊延建开异弃弄弊式弓引弟张弦弯弱弹强归当录形彩役彻彼往征径待很律徐徒得御循微心必忆忌忍志忘忙忠忧快念忽怀态怎怒怕怖怜思怠急性怨怪总恋恐恒恢恨恩恭息恰恳恶恼悄悉悔悟悠患悦您悬悲悼情惊惑惕惜惠惧惨惩惭惯惰想惹愁愈愉意愚感愤愧愿慈慌慎慕慨戏成我戒或战戚截户房所扁扇手才扎扑扒打扔托扛扣执扩扫扬扭扮扯扰扶批找承技抄把抓投抖抗折抚抛抢护报披抬抱抵抹押抽担拆拉拌拍拐拒拔拖拘招拜拢拣拥拦拨择括拳拴拼拾拿持挂指按挎挑挖挠挡挣挤挥挨挪振挺挽捆捉捎捏捐捕捞损捡换捧据捷掀授掉掌掏排掘掠探接控推掩揉描提插握揪揭援搁搂搅搏搜搞搬搭携摄摆摇摊摔摘摧摸撇支收改攻放政故效敌敏救教敞敢散敬数敲文斑斗料斜斤斥斧斩断斯新方施旁旅旋族无既日旦旧旨早旬旱时旷旺昂昆昌明昏易星映春昨是昼显晃晋晌晒晓晕晚晨普景晴晶智暂暑暖暗暮曲更曾替最月有朋服朗望朝期木未末本术朱朴朵机朽杀杂权杆李杏材村杜束杠条来杨杯杰松板极构析枕林果枝枣枪枯架柄柏某染柔柜查柱柳柴柿标栋栏树栗校株样核根格栽桂桃框案桌桐桑档桥桨桶梁梅梢梦梨梯械梳检棉棋棍棒棕棚森棵椅植椒楚楼概榆榜榨榴槐模欠次欢欣欧欲欺款歇歌止正此步武歪死歼殃殊残殖段殿毁母每毒比毕毙毛毫毯氏民气氧水永汁求汇汉汗江池污汤汪汽沃沈沉沙沟没沫河沸油治沾沿泄泉泊法泛泡波泥注泪泰泳泻泼泽洁洋洒洗洞津洪洲活洽派流浅浆浇浊测济浑浓浙浩浪浮浴海浸涂消涉涌涛涝润涨液淋淘淡深混淹添清渐渔渗渠渡渣温港渴游湖湾湿溉源溜溪滋滑滔滚满滤滥滨滩漠火灭灯灰灵灶灾灿炉炊炎炒炕炭炮炸点炼烂烈烘烛烟烤烦烧烫热焦焰然煌煎煤照煮爪爬爱父爷爸爹爽片版牌牙牛牢牧物牲牵特牺犁犬犯状犹狂狐狗狠狡独狭狮狱狸狼猎猛猜猪猫献猴猾率玉王玩环现玻珍珠班球理琴瑞璃瓜瓦瓶甘甚甜生用甩田由甲申电男画畅界畏留畜略番疏疑疗疤疫疮疯疲疼疾病症痒痕痛痰瘦登白百皂的皆皇皮皱盆盈益盏盐监盒盖盗盘盛盟目盯盲直相盼盾省眉看真眠眨眯眼着睁睛睡督睬矛知矩短矮石矿码砌砍研砖破础硬确碌碍碎碑碗碧碰磁示礼社祖祝神祥票祸禁福离禽禾秀私秃秆秋种科秒秘租秤秧秩积称移稀程稍税稠稳穴究穷空穿突窃窄窑窗窜窝立竖站竞竟章童竹竿笋笑笔笛符笨第笼等筋筐筑筒答策筛筝筹签简算管箩米类粉粒粗粘粥粪粮粱系素索紧紫累絮纠红纤约级纪纯纱纲纳纵纷纸纹纺纽线练组细织终绍经绑绒结绕绘给络绝绞统绢绣继绩绪续绳维绵绸绿缎缓编缘缝缠缸缺网罗罚罢罩罪置羊美羞羡群羽翁翅老考者而耍耐耕耗耳耻耽聋职联聚肃肆肉肌肚肝肠股肢肤肥肩肯育肺肾肿胀胁胃胆背胖胜胞胡胳胶胸能脂脆脉脊脏脑脖脚脱脸脾腊腐腔腥腰腹腾腿膀膊膏膜臣自臭至致舅舌舍舒舞舟航般舰舱船艇良艰色艳艺节芒芝芦芬花芳芹芽苍苏苗若苦英苹茂范茄茅茎茧茫茶草荐荒荡荣药荷莫莲获菊菌菜菠萄萌萍萝营落著葛葡董葬葱葵蒙蒜蒸蓄蓝蓬蔑蔽虎虏虑虚虫虹虽虾蚀蚁蚂蚊蚕蛇蛋蛙蛛蛮蛾蜂蜓蜘蜡蜻蝇血行衔街衣补表衫衬衰袄袋袍袖袜被袭裁裂装裕裙裤裳裹西要见观规视览觉角解触言誉誓计订认讨让训议讯记讲许论讽设访证评识诉诊词译试诗诚话诞询该详语误诱说诵请诸读课谁调谅谈谊谋谎谜谢谣谦谨谷豆象豪貌贝贞负贡财责贤败货质贩贪贫购贯贱贴贵贷贸费贺贼贿资赌赏赔赖赚赤走赴赵赶起趁超越趋足趴跃跌跑距跟跨跪路跳践身躬躲车轧轨转轮软轰轻载轿较辅辆辈辉输辛辜辞辟辰辱边辽达迁迅过迈迎运近返还这进远违连迟迫述迷迹追退送适逃逆选透逐递途逗通逝速造逢逮逼遇遍道遗遣遥遭遮那邪邮邻郊郎郑部都鄙配酒酬酱酷酸酿采释里重野量金鉴针钉钓钞钟钢钥钩钱钳钻铁铃铅铜铲银铸铺链销锁锄锅锈锋锐错锡锣锤锦键锯锹锻长门闪闭问闯闲间闷闸闹闻阀阁阅阔队防阳阴阵阶阻阿附际陆陈降限陕陡院除险陪陵陶陷隆随隐隔隙障隶难雀雁雄雅集雨雪零雷雹雾需霸青静非面革音韵页顶顷项顺须顽顾顿颂预领颈颗风飞食饥饭饮饰饱饲饶饺饼饿馅馆馋馒首香馨马驰驱驳驴驶驻驼驾骂骄骆验骑骗骨高鬼魂魄鱼鲁鲜鸟鸡鸣鸦鸭鸽鹅鹊鹿麦麻黄黑鼓鼠鼻齐齿龄龙龟";
            foreach (var c in t) {
                var ch = c.ToString();
                var ch2 = WordsHelper.ToSimplifiedChinese(ch.ToString());
                if (ch == ch2) {
                    ch2 = WordsHelper.ToSimplifiedChinese(ch.ToString(), 1);
                    if (ch == ch2) {
                        ch2 = WordsHelper.ToSimplifiedChinese(ch.ToString(), 2);
                    }
                }
                simplifiedChinese1.Add(ch2);
            }
            var t2 = "一丁七万丈三上下不与丐丑专且丕世丘丙业丛东丝丞丢两严丧个丫中丰串临丸丹为主丽举乃久么义之乌乍乎乏乐乒乓乔乖乘乙乜九乞也习乡书乩买乱乳乾了予争事二于亏云互亓五井亘亚些亟亡亢交亥亦产亨亩享京亭亮亲亳亵人亿什仁仃仄仅仆仇今介仍从仑仓仔仕他仗付仙仞代令以仨仪们仰仲仵件价任份仿企伉伊伍伎伏伐休众优伙会伞伟传伢伤伥伦伧伪伫伯估伴伶伸伺似伽佃但位低住佐佑体何佗佘余佚佛作佞佟你佣佥佩佬佯佳佶佻佼佾使侃侄侈例侍侏侑侔侗供依侠侣侥侦侧侨侩侪侬侮侯侵便促俄俅俊俎俏俐俑俗俘俚俜保俞俟信俣俦俨俩俪俭修俯俱俳俸俺俾倌倍倏倒倔倘候倚倜借倡倥倦倨倩倪倬倭债值倾偃假偈偌偎偏偕做停健偬偶偷偻偾偿傀傅傍傣傥傧储傩催傲傻像僖僚僦僧僬僭僮僵僻儆儋儒儡儿兀允元兄充兆先光克免兑兔兕兖党兜兢入全八公六兮兰共关兴兵其具典兹养兼兽冀内冈冉册再冒冕冗写军农冠冢冤冥冬冯冰冲决况冶冷冻冽净凄准凇凉凋凌减凑凛凝几凡凤凫凭凯凰凳凶凸凹出击凼函凿刀刁刃分切刈刊刍刎刑划刖列刘则刚创初删判刨利别刮到刳制刷券刹刺刻刽剁剂剃削剌前剐剑剔剖剜剡剥剧剩剪副割剽剿劈劓力劝办功加务劢劣动助努劫劬劭励劲劳劾势勃勇勉勋勐勒勖勘募勤勰勺勾勿匀包匆匈匍匏匐匕化北匙匝匠匡匣匦匪匮匹区医匾匿十千卅升午卉半华协卑卒卓单卖南博卜卞卟占卡卢卣卤卦卧卫卮卯印危即却卵卷卸卺卿厂厄厅历厉压厌厍厕厘厚厝原厢厣厥厦厨厩厮去县叁参又叉及友双反发叔取受变叙叛叟叠口古句另叨叩只叫召叭叮可台叱史右叵叶号司叹叻叼叽吁吃各吆合吉吊同名后吏吐向吓吕吗君吝吞吟吠吡吣否吧吨吩含听吭吮启吱吴吵吸吹吻吼吾呀呃呆呈告呋呐呓呔呕呖呗员呙呛呜呢呤呦周呱味呵呶呷呸呻呼命咀咂咄咆咋和咎咏咐咒咔咕咖咙咚咛咝咣咤咦咧咨咩咪咫咬咭咯咱咳咴咸咻咽咿哀品哂哄哆哇哈哉响哎哐哑哗哙哚哝哟哥哦哧哨哩哪哭哮哲哺哼哽哿唁唆唇唉唏唐唑唔唠唤唧唬售唯唱唳唷唾啃啄商啊啐啕啖啜啡啤啥啦啧啪啬啭啮啰啵啶啷啸啻啼啾喀喁喂喃善喇喉喊喋喏喑喔喘喙喜喝喟喧喳喵喷喻喽喾嗄嗅嗉嗌嗍嗑嗒嗓嗔嗖嗜嗝嗟嗡嗣嗤嗥嗦嗨嗪嗫嗬嗯嗲嗳嗵嗷嗽嗾嘀嘁嘈嘉嘌嘎嘏嘘嘛嘞嘟嘣嘤嘧嘭嘱嘲嘴嘶嘹嘻嘿噎噔噗噙噜噢噤器噩噪噫噬噱噶噼嚅嚎嚏嚓嚣嚷嚼囊囔囚四回囟因囡团囤囫园困囱围囵囹固国图囿圃圄圆圈圉圜土圣在圩圪圭圮地圳圹场圻圾址坂均坊坌坍坎坏坐坑块坚坛坝坞坟坠坡坤坦坪坯坳坷坻垂垃垄垅型垒垓垛垠垡垢垣垦垩垫垮埂埃埋城埒埔埙域埠埭培基埽堂堆堇堋堑堕堙堞堡堤堪堰堵塌塑塔塘塞填塾墀境墅墉墒墓墙增墟墨墩墼壁壅壑壕壤士壬壮声壳壶壹处备复夏夔夕外夙多夜够夤夥大天太夫夭央夯失头夷夸夹夺奁奂奄奇奈奉奋奎奏契奔奕奖套奘奚奠奢奥女奴奶奸她好如妃妄妆妇妈妊妍妒妓妖妗妙妞妣妤妥妨妩妪妫妮妯妲妹妻妾姆姊始姐姑姒姓委姗姘姚姜姝姣姥姨姬姹姻姿威娃娄娅娆娇娉娌娑娓娘娜娟娠娣娥娩娱娲娴娶娼婀婆婉婊婕婚婢婧婪婴婵婶婷婺婿媒媚媛媪媲媳媵媸媾嫁嫂嫉嫌嫒嫔嫖嫘嫜嫠嫡嫣嫦嫩嫫嫱嬉嬖嬗嬴嬷孀子孑孔孕字存孙孚孛孜孝孟孢季孤孥学孩孪孰孱孳孵孺孽宁它宄宅宇守安宋完宏宓宕宗官宙定宛宜宝实宠审客宣室宥宦宪宫宰害宴宵家宸容宽宾宿寂寄寅密寇富寐寒寓寝寞察寡寤寥寨寮寰寸对寺寻导寿封射将尉尊小少尔尖尘尚尝尤尥尧尬就尴尸尹尺尼尽尾尿局屁层居屈屉届屋屎屏屐屑展属屠屡履屦屯山屹屿岁岂岌岐岑岔岖岗岘岚岛岢岣岩岫岬岭岱岳岵岷岸岿峁峄峋峒峙峡峤峥峦峨峪峭峰峻崂崃崆崇崎崔崖崛崞崤崦崩崭崮崴崽嵇嵊嵋嵌嵘嵛嵝嵩嵫嵬嵯嵴嶂嶙嶷巅巍川州巡巢工左巧巨巩巫差巯己已巳巴巷巽巾币市布帅帆师希帏帐帑帔帕帖帘帙帚帛帜帝带帧席帮帷常帻帼帽幂幄幅幌幔幕幞幡幢干平年并幸幺幻幼幽广庀庄庆庇床庋序庐庑库应底庖店庙庚府庞废庠庥度座庭庵庶康庸庹庾廉廊廓廖廛廨廪延廷建廿开弁异弃弄弈弊弋式弑弓引弗弘弛弟张弥弦弧弩弭弯弱弹强弼彀归当录彖彗彘彝形彤彦彩彪彬彭彰影彷役彻彼往征徂径待徇很徊律徐徒徕得徘徙御徨循徭微徵德徼徽心必忆忌忍忏忐忑忒忖志忘忙忝忠忡忤忧忪快忱念忸忻忽忾忿怀态怂怄怅怆怍怎怏怒怔怕怖怙怛怜思怠怡急怦性怨怩怪怫怯怵总怼怿恁恂恃恋恍恐恒恕恙恚恢恣恤恨恩恪恫恬恭息恰恳恶恸恹恺恻恼恽恿悄悉悌悍悒悔悖悚悛悝悟悠患悦您悫悬悭悯悲悴悸悻悼情惆惊惋惑惕惘惚惜惟惠惦惧惨惩惫惬惭惮惯惰想惴惶惹惺愀愁愆愈愉愎意愕愚感愠愣愤愦愧愫愿慈慊慌慎慑慕慝慢慧慨慰慵慷憋憎憔憧憨憩憬憷憾懂懈懊懋懑懒懦懵懿戆戈戊戌戍戎戏成我戒戕或战戚戛戟戡戢截戬戮戳戴户戾房所扁扃扇扈扉手才扎扑扒打扔托扛扣执扩扪扫扬扭扮扯扰扳扶批扼找承技抄抉把抑抒抓投抖抗折抚抛抟抠抡抢护报抨披抬抱抵抹抻押抽抿拂拄担拆拇拈拉拊拌拍拎拐拒拓拔拖拗拘拙拚招拜拟拢拣拥拦拧拨择括拭拮拯拱拳拴拶拷拼拽拾拿持挂指挈按挎挑挖挚挛挝挞挟挠挡挣挤挥挨挪挫振挹挺挽捂捅捆捉捋捌捍捎捏捐捕捞损捡换捣捧据捶捷捺捻掀掂掇授掉掊掌掎掏掐排掖掘掠探掣接控推掩措掬掮掰掳掷掸掺掼掾揄揆揉揍描提插揖握揣揩揪揭援揽揿搀搁搂搅搏搐搓搔搜搞搠搡搦搪搬搭搴携搽摁摄摆摇摈摊摒摔摘摞摧摩摭摸摹摺撂撅撇撑撒撕撙撞撤撩撬播撮撰撵撷撸撺撼擂擅操擎擐擒擘擞擢擦攀攒攘攥攫支收攸改攻放政故效敉敌敏救敕敖教敛敝敞敢散敦敫敬数敲整敷文斋斌斐斑斓斗料斛斜斟斡斤斥斧斩斫断斯新方於施旁旃旄旅旆旋旌旎族旒旖旗无既日旦旧旨早旬旭旮旯旰旱时旷旺昀昂昃昆昊昌明昏易昔昕昙昝星映春昧昨昭是昱昴昵昶昼显晁晃晋晌晏晒晓晔晕晖晗晚晟晡晤晦晨普景晰晴晶晷智晾暂暄暇暌暑暖暗暝暧暨暮暴暹暾曙曛曜曝曦曩曰曲曳更曷曹曼曾替最月有朊朋服朐朔朕朗望朝期朦木未末本札术朱朴朵机朽杀杂权杆杈杉杌李杏材村杓杖杜杞束杠条来杨杪杭杯杰杲杳杵杷杼松板极构枇枉枋析枕林枚果枝枞枢枣枥枨枪枫枭枯枰枳枵架枷枸柄柏某柑柒染柔柘柙柚柜柞柠柢查柩柬柯柰柱柳柴柿栅标栈栉栊栋栌栎栏树栓栖栗校栩株样核根格栽栾桀桁桂桃桅框案桉桌桎桐桑桓桔桡桢档桥桦桧桨桩桴桶梁梃梅梆梏梓梗梢梦梧梨梭梯械梳梵检棂棉棋棍棒棕棘棚棠棣森棰棱棵棹棺椁椅椋植椎椒椤椭椰椴椽椿楂楔楚楞楠楣楦楫楮楷楸楹楼概榄榆榇榈榉榍榔榕榖榛榜榧榨榫榭榴榷榻槁槊槌槎槐槔槛槟槠槭槲槽槿樊樗樘樟模横樯樱樵樽樾橄橇橐橘橙橛橡橥橱橹檀檄檎檐檠檩檬欠次欢欣欤欧欲欺款歆歇歉歌歙止正此步武歧歪歹死歼殁殂殃殄殆殇殉殊残殒殓殖殚殛殡殪殳殴段殷殿毁毂毅毋母每毒毓比毕毖毗毙毛毡毫毯氅氏氐民氓气氖氚氛氟氡氢氤氦氧氨氩氪氮氯氰氲水永汀汁求汇汉汊汐汕汗汛汜汝汞江池污汤汨汩汪汰汲汴汶汹汽汾沁沂沃沅沆沈沉沌沏沐沓沔沙沛沟没沣沤沥沦沧沪沫沮沱河沸油治沼沽沾沿泄泅泉泊泌泓法泗泛泞泠泡波泣泥注泪泫泮泯泰泱泳泵泷泸泺泻泼泽泾洁洄洋洌洎洒洗洙洛洞津洧洪洮洱洲洳洵洹活洼洽派流浃浅浆浇浊测济浏浑浒浓浔浙浚浜浞浠浣浦浩浪浮浯浴海浸浼涂涅消涉涌涎涑涓涔涕涛涝涞涟涠涡涣涤润涧涨涩涪涫涮涯液涵涸涿淀淄淅淆淇淋淌淑淖淘淙淝淞淠淡淤淦淫淬淮深淳混淹添清渊渌渍渎渐渑渔渗渚渝渠渡渣渤渥温渫渭港渲渴游渺湃湄湍湎湔湖湘湛湟湫湮湾湿溃溅溆溉源溜溟溢溥溧溪溯溱溲溴溶溷溺滁滂滇滋滏滑滓滔滕滚滞滟满滢滤滥滦滨滩滴滹漂漆漉漏漓演漕漠漩漪漫漱漳漾潇潍潘潜潞潢潦潭潮潴潸潺潼澄澈澍澎澜澡澧澳澶澹激濂濉濑濒濠濡濮濯瀑瀚瀛灌灏灞火灭灯灰灵灶灸灼灾灿炀炅炉炊炎炒炔炕炖炙炜炝炫炬炭炮炯炱炳炷炸点炻炼炽烀烁烂烈烊烘烙烛烟烤烦烧烨烩烫烬热烯烷烹烽焉焊焐焓焕焖焘焙焚焦焯焰焱然煅煊煌煎煜煞煤煦照煨煮煲煳煸煺煽熄熊熏熔熘熙熟熠熨熬熵熹燃燎燔燕燠燥燧燮爆爨爪爬爰爱爵父爷爸爹爻爽片版牌牍牒牖牙牛牝牟牡牢牦牧物牲牵特牺犀犁犄犊犍犒犟犬犯犴状犷犹狂狄狈狎狐狒狗狙狞狠狡狩独狭狮狰狱狲狷狸狻狼猊猎猕猖猗猛猜猝猢猥猩猪猫猬献猴猷猾猿獍獐獗獠獬獭獯獾玄率玉王玑玖玛玩玫玮环现玲玳玷玺玻珀珂珈珉珊珍珏珑珙珞珠珥珩班珲球琅理琉琏琐琚琛琢琥琦琨琪琬琮琰琳琴琵琶琼瑁瑕瑗瑙瑚瑛瑜瑞瑟瑭瑰瑶瑾璀璁璃璇璋璐璜璞璧璨璩瓒瓜瓠瓢瓣瓤瓦瓮瓯瓴瓶瓷瓿甄甏甑甓甘甙甚甜生甥用甩甫甬甭田由甲申电男甸町画畀畅畈畋界畎畏畔留畚畛畜略畦番畲畴畸畹畿疃疆疏疑疗疙疚疟疠疡疣疤疥疫疮疯疱疲疴疵疸疹疼疽疾痂病症痈痉痊痍痒痔痕痘痛痞痢痣痤痨痪痫痰痴痹痼痿瘁瘗瘙瘛瘟瘠瘢瘤瘦瘩瘪瘫瘳瘴瘸瘾瘿癌癖癜癞癣癫癯癸登白百皂的皆皇皈皋皎皑皓皖皮皱皿盂盅盆盈益盍盎盏盐监盒盔盖盗盘盛盟盥目盯盱盲直相盹盼盾省眄眇眈眉看眙眚真眠眦眨眩眯眶眷眸眺眼着睁睇睐睑睛睡睢督睦睨睫睬睹睽睾睿瞄瞅瞌瞎瞑瞒瞟瞠瞥瞧瞩瞪瞬瞭瞰瞳瞻瞽瞿矍矗矛矜矢矣知矧矩矫短矮石矶矸矾矿砀码砂砉砌砍砑砒研砖砗砘砚砝砣砥砧砭砰破砷砸砹砺砻砾础硅硇硌硎硐硒硕硖硗硝硪硫硬硭确硼碇碉碌碍碎碑碓碗碘碚碛碜碟碣碧碰碱碳碴碾磁磅磊磋磐磔磕磨磬磷磺礁礴示礼社祀祁祈祉祎祖祗祚祛祜祝神祟祠祢祥祧票祭祯祷祸祺禀禁禄禅福禧禳禹禺离禽禾秀私秃秆秉秋种科秒秕秘租秣秤秦秧秩秫积称秸移秽稀程稍税稔稗稚稠稣稳稷稻稼稽稿穆穑穗穰穴究穷穸穹空穿窀突窃窄窈窍窑窒窕窖窗窘窜窝窟窠窣窥窦窨窬窭窳窸窿立竑竖站竞竟章竣童竦竭端竹竺竽竿笃笄笆笈笊笋笏笑笔笕笙笛笞笠笤笥符笨笪笫第笮笱笳笸笺笼笾筇等筋筌筏筐筑筒答策筘筛筝筠筮筱筵筷筹签简箍箓箔箕算箜管箧箩箪箫箭箱箴箸篁篆篇篌篑篓篙篚篝篡篦篪篮篱篷篾簇簋簌簟簠簦簧簪簸簿籁籍米籴类籼籽粉粑粒粕粗粘粜粝粞粟粤粥粪粮粱粲粳粹粼粽精糁糇糊糕糖糗糙糜糟糠糯系紊素索紧紫累絮絷綦縻繁繇纂纛纠纡红纣纤纥约级纨纪纫纬纭纯纰纱纲纳纵纶纷纸纹纺纻纽纾线绀绂练组绅细织终绉绊绌绍绎经绑绒结绔绕绘给绚绛络绝绞统绡绢绣绥绦继绨绩绪绫续绮绯绰绲绳维绵绶绷绸绺绻综绽绾绿缀缁缂缃缄缅缆缇缈缉缌缎缑缒缓缔缕编缗缘缙缚缛缜缝缟缠缡缢缣缤缥缦缧缨缩缪缫缬缭缮缯缰缱缴缵缶缸缺罂罄罅罐网罔罕罗罘罚罟罡罢罨罩罪置罱署罴罹罽罾羁羊羌美羔羚羝羞羟羡群羧羯羰羲羸羹羽羿翁翅翊翌翎翔翕翘翚翟翠翡翥翦翩翮翰翱翳翻翼耀老考耄者耆而耍耐耒耕耗耘耙耜耦耨耪耳耶耷耸耻耽耿聂聃聆聊聋职聒联聘聚聩聪聿肃肄肆肇肉肋肌肓肖肘肚肛肝肟肠股肢肤肥肩肪肫肭肮肯肱育肴肷肺肼肽肾肿胀胁胂胃胄胆背胍胎胖胗胙胚胛胜胝胞胡胤胥胧胨胩胪胫胭胯胰胱胳胴胶胸胺能脂脆脉脊脍脏脐脑脓脔脖脚脯脱脸脾腆腊腋腌腐腑腓腔腕腚腥腮腰腱腴腹腺腻腼腾腿膀膂膈膊膏膑膘膛膜膝膨膳膺膻臀臁臂臃臆臊臣臧自臬臭至致臻臼臾舀舂舄舅舆舌舍舐舒舔舛舜舞舟舢舨航舫般舰舱舳舴舵舶舷舸船舻舾艄艇艋艘艚艟艨艮良艰色艳艺艽艾艿节芄芈芊芋芍芎芏芑芒芗芙芜芝芟芡芥芦芨芩芪芫芬芭芮芯芰花芳芴芷芸芹芽芾苁苄苇苈苊苋苌苍苎苏苑苒苓苔苕苗苛苜苞苟苡苣苤若苦苫苯英苴苷苹苻茁茂范茄茅茉茎茏茑茔茕茗茜茧茨茫茬茯茱茴茵茶茸茹荀荃荆荇草荏荐荒荔荚荛荜荞荟荠荡荣荤荥荧荨荩荪荫荭药荷荸荻荼荽莅莆莉莎莒莓莘莞莠莨莩莫莱莲莶获莸莹莺莼莽菀菁菅菇菊菌菏菔菖菘菜菝菟菠菡菥菩菪菰菱菲菹菽萁萃萄萋萌萍萎萏萑萘萜萝萤营萦萧萨萱萸萼落葆葑著葛葜葡董葩葫葬葭葱葵葺蒂蒋蒙蒜蒯蒲蒸蒺蒿蓄蓉蓍蓐蓑蓓蓖蓝蓟蓦蓬蓼蓿蔑蔓蔗蔚蔡蔫蔬蔷蔺蔻蔼蔽蕃蕉蕊蕖蕙蕤蕨蕲蕴蕹蕾薄薅薇薏薛薜薨薪薮薯薰薷薹藁藉藏藐藓藕藜藤藩藻藿蘅蘑蘖蘧蘩蘸蘼虎虏虐虑虔虚虞虢虫虬虮虱虹虺虻虼虽虾虿蚀蚁蚂蚊蚋蚌蚍蚓蚕蚜蚝蚣蚤蚧蚨蚩蚪蚬蚯蚰蚱蚴蚶蛀蛄蛆蛇蛉蛊蛋蛎蛏蛐蛑蛔蛘蛙蛛蛞蛟蛤蛩蛭蛮蛰蛱蛲蛳蛴蛹蛾蜀蜂蜃蜇蜈蜊蜍蜒蜓蜕蜗蜘蜚蜜蜡蜥蜮蜴蜷蜻蜿蝇蝈蝉蝌蝎蝗蝙蝠蝮蝴蝶蝼螂螃螅融螟螨螫螭螳螺螽蟀蟆蟊蟋蟑蟒蟛蟠蟥蟪蟮蟹蟾蠊蠓蠕蠖蠡蠢蠲蠹蠼血衄衅行衍衔街衙衡衢衣补表衩衫衬衮衰衲衷衽衾衿袁袂袄袅袈袋袍袒袖袜袢袤被袭袱袼裁裂装裆裉裎裒裔裕裘裙裟裢裣裤裥裨裰裱裳裴裸裹裼裾褂褊褐褒褓褙褚褛褡褥褪褫褰褴褶襁襄襟襦西要覃覆见观规觅视觇览觉觊觌觎觐觑角觚觜觞解觥触觯觳言訾詈詹誉誊誓謇警譬计订讣认讥讦讨让讪讫训议讯记讲讳讴讵讶讷许讹论讼讽设访诀证诂诃评诅识诈诉诊诋诌词诎诏译诒诓诔试诖诗诘诙诚诛诜话诞诟诠诡询诣诤该详诧诨诩诫诬语诮误诰诱诲诳说诵请诸诹诺读诼诽课诿谀谁谂调谄谅谆谇谈谊谋谌谍谎谏谐谑谒谓谔谕谖谗谙谚谛谜谝谟谠谡谢谣谤谥谦谧谨谩谪谬谭谮谯谰谱谲谳谴谶谷豁豆豌豕豚象豢豨豪豫豳豹豺貂貉貊貌貔贝贞负贡财责贤败账货质贩贪贫贬购贮贯贰贱贲贳贴贵贶贷贸费贺贻贼贽贾贿赁赂赃资赇赈赉赊赋赌赍赎赏赐赓赔赖赘赙赚赛赜赝赞赟赠赡赢赣赤赦赧赫赭走赳赴赵赶起趁趄超越趋趔趟趣趱足趴趵趸趺趼趾趿跃跄跆跋跌跎跏跑跖跗跚跛距跞跟跣跤跨跪跬路跳践跷跸跹跺跻跽踅踉踊踌踏踔踝踞踟踢踣踩踪踬踮踯踱踵踹踽蹀蹂蹄蹇蹈蹉蹊蹋蹑蹒蹙蹦蹩蹬蹭蹰蹲蹴蹶蹿躁躅躇躏躔身躬躯躲躺车轧轨轩转轭轮软轰轱轲轳轴轵轶轸轺轻轼载轿辂较辄辅辆辇辈辉辍辎辏辐辑输辔辕辖辗辘辙辛辜辞辟辣辨辩辫辰辱边辽达迁迂迄迅过迈迎运近迓返迕还这进远违连迟迢迤迥迦迨迩迪迫迭述迷迸迹追退送适逃逄逅逆选逊逋逍透逐逑递途逖逗通逛逝逞速造逡逢逦逭逮逯逵逶逸逻逼逾遁遂遄遇遍遏遐遑遒道遗遘遛遢遣遥遨遭遮遴遵遽避邀邂邃邈邋邑邓邕邙邛邝邢那邦邪邬邮邯邱邳邴邵邸邹邺邻邽邾郁郄郅郊郎郏郑郓郗郛郜郝郡郢郤郦郧部郭郯郴郸都郾郿鄂鄄鄙鄞鄢鄣鄯鄱鄹酃酆酉酊酋酌配酒酗酚酝酢酣酤酥酩酪酬酮酯酰酱酵酶酷酸酹酿醇醉醋醒醚醛醢醪醭醮醯醴醵醺采釉释里重野量金釜鉴銎銮鋆鋈錾鍪鎏鏊鏖鐾鑫钇针钉钊钋钌钍钏钐钒钓钔钕钗钙钚钛钝钞钟钠钡钢钣钤钥钦钧钨钩钪钫钭钮钯钰钱钲钳钴钵钹钺钻钾钿铀铁铂铃铄铅铆铉铎铐铙铛铜铝铠铡铢铣铤铨铩铬铭铮铰铱铲铳银铸铺链铿销锁锃锄锅锇锈锉锊锋锌锎锏锐锒锓锔锕锖锘错锚锛锜锝锞锟锡锢锣锤锥锦锨锩锪锫锬锭键锯锰锱锲锴锵锶锷锸锹锻锾锿镀镁镂镄镅镆镇镈镉镊镌镍镎镏镐镑镒镖镗镛镜镝镞镠镣镫镬镭镯镰镳镶长门闩闪闭问闯闰闱闲闳间闵闶闷闸闹闺闻闼闽闾闿阀阁阂阃阄阅阆阇阈阉阊阋阌阍阎阏阐阑阒阔阕阖阗阙阚阜队阡阢阪阮阱防阳阴阵阶阻阼阿陀陂附际陆陇陈陉陋陌降限陔陕陛陟陡院除陨险陪陬陲陴陵陶陷隅隆隋隍随隐隔隗隘隙障隧隰隳隶隼隽难雀雁雄雅集雇雉雌雍雎雏雒雕雠雨雩雪雯雳零雷雹雾需霁霄霆震霉霍霎霏霓霖霜霞霪霭霰露霸霹霾青靓靖静靛非靠靡面靥革靳靴靶靺靼鞅鞋鞍鞑鞒鞘鞠鞣鞨鞫鞭鞯鞲鞴韂韦韧韩韪韫韬韭音韵韶页顶顷项顺须顼顽顾顿颀颁颂预颅领颇颈颉颊颋颌颍颏颐频颓颔颖颗题颙颚颛颜额颠颡颢颤颦颧风飒飓飕飘飙飞食飧飨餍餐餮饔饕饥饧饨饩饪饫饬饭饮饯饰饱饲饴饵饶饷饺饼饽饿馀馁馄馅馆馇馈馊馋馍馏馐馑馒馓馔馕首馗馘香馥馨马驭驮驯驰驱驳驴驶驷驸驹驺驻驼驽驾驿骁骂骄骅骆骇骈骊骋验骏骐骑骓骖骗骘骚骛骜骝骞骠骡骢骤骥骧骨骰骶骷骸骺骼髀髁髂髅髋髌髑髓高髡髦髫髭髯髻鬃鬓鬟鬣鬯鬲鬻鬼魁魂魃魄魅魇魈魉魍魏魑魔鱼鱿鲁鲂鲅鲆鲇鲈鲋鲍鲎鲑鲔鲛鲜鲟鲠鲤鲧鲨鲫鲲鲸鳃鳄鳅鳌鳍鳎鳏鳖鳗鳝鳞鸟鸠鸡鸢鸣鸥鸦鸨鸩鸪鸫鸬鸭鸯鸱鸲鸳鸵鸶鸷鸸鸹鸺鸽鸾鸿鹁鹂鹃鹄鹅鹆鹇鹈鹉鹊鹌鹎鹏鹑鹕鹗鹘鹚鹛鹜鹞鹣鹤鹦鹧鹨鹩鹪鹫鹬鹭鹰鹱鹳鹿麂麈麋麒麓麝麟麦麸麻麽麾黄黍黎黏黑黔默黛黜黝黠黥黩黯黻黼黾鼋鼍鼎鼐鼓鼗鼙鼠鼻鼾齐齑齿龀龃龄龅龆龇龈龉龊龋龌龙龚龛龟龠";
            foreach (var c in t2) {
                var ch = c.ToString();
                var ch2 = WordsHelper.ToSimplifiedChinese(ch.ToString());
                if (ch == ch2) {
                    ch2 = WordsHelper.ToSimplifiedChinese(ch.ToString(), 1);
                    if (ch == ch2) {
                        ch2 = WordsHelper.ToSimplifiedChinese(ch.ToString(), 2);
                    }
                }
                simplifiedChinese1.Add(ch2);
            }

            for (int i = 0x4e00; i < 0x9fff; i++) {
                var ch = ((char)i).ToString();
                var ch2 = WordsHelper.ToSimplifiedChinese(ch.ToString());
                if (ch == ch2) {
                    ch2 = WordsHelper.ToSimplifiedChinese(ch.ToString(), 1);
                    if (ch == ch2) {
                        ch2 = WordsHelper.ToSimplifiedChinese(ch.ToString(), 2);
                    }
                }
                if (ch2 != ch && ch2.Length == 1) {
                    simplifiedChinese2.Add(ch2);
                }
            }

            #endregion

            #region 第一次转简体
            foreach (var item in extends) {
                var sp = item.TarTxt.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();
                sp.RemoveAll(q => q.Length > 1);
                if (sp.Count < 2) { continue; }
                string key = null;
                foreach (var s in sp) {
                    if (key == null && simplifiedChinese1.Contains(s)) { key = s; }
                }
                if (key == null) {
                    foreach (var s in sp) {
                        if (key == null && simplifiedChinese2.Contains(s)) { key = s; }
                    }
                }
                if (key == null) {
                    foreach (var s in sp) {
                        if (key == null && simplifiedChinese3.Contains(s)) { key = s; }
                    }
                }

                if (key == null) { key = sp[0]; }
                sp.Remove(key);
                foreach (var s in sp) {
                    dict[s[0]] = key[0];
                }
            }
            dict['拾'] = '十';
            dict['十'] = '十';
            #endregion





            var temp = new CharDictionary(dict);
            Dictionary<char, char> dict2 = new Dictionary<char, char>();
            foreach (var item in extends) {
                var sp = item.TarTxt.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();
                sp.RemoveAll(q => q.Length > 1);
                if (sp.Count < 2) { continue; }
                foreach (var s in sp) {
                    var ch = s[0];
                    if (ch == '仅') {

                    }// 堇
                    if (ch == '堇') {

                    }// 堇
                    var ch2 = temp.GetTranslate(temp.GetTranslate(temp.GetTranslate(temp.GetTranslate(temp.GetTranslate(temp.GetTranslate(temp.GetTranslate(ch)))))));
                    var ch3 = WordsHelper.ToSimplifiedChinese(ch2.ToString());
                    if (ch.ToString() == ch3) {
                        ch3 = WordsHelper.ToSimplifiedChinese(ch2.ToString(), 1);
                        if (ch.ToString() == ch3) {
                            ch3 = WordsHelper.ToSimplifiedChinese(ch2.ToString(), 2);
                        }
                    }
                    if (ch3.Length == 1 && ch2.ToString() != ch3.ToString()) {
                        ch2 = ch3[0];
                    }

                    if (ch != ch2) {
                        if (simplifiedChinese1.Contains(ch2.ToString())) {
                            dict2[ch] = ch2;
                        } else if (simplifiedChinese1.Contains(ch.ToString())) {
                            dict2[ch2] = ch;
                        } else if (simplifiedChinese2.Contains(ch2.ToString())) {
                            dict2[ch2] = ch2;
                        } else if (simplifiedChinese2.Contains(ch.ToString())) {
                            dict2[ch2] = ch;
                        } else if (simplifiedChinese3.Contains(ch2.ToString())) {
                            dict2[ch2] = ch2;
                        } else if (simplifiedChinese3.Contains(ch.ToString())) {
                            dict2[ch2] = ch;
                        } else {
                            dict2[ch] = ch2;
                        }
                    }
                }
            }
            dict2['拾'] = '十';
            dict2['十'] = '十';
            dict2['么'] = '么';
            dict2['幺'] = '么';

            HashSet<char> set = new HashSet<char>();
            var values = dict2.Select(q => q.Value).ToList();
            foreach (var item in values) {
                if (dict2.ContainsKey(item)) {
                    if (dict2[item] != item) {
                        set.Add(item);
                    }else {
                        dict2.Remove(item);
                    }
                }
            }



            WriteDictTranslate(dict2);
            return new CharDictionary(dict2);
        }

        private static void WriteDictTranslate(Dictionary<char, char> dict2)
        {
            var keys = dict2.Keys.OrderBy(q => q).ToList();
            var fs = File.Open(_tempTranslateDictPath, FileMode.Create);
            var bw = new BinaryWriter(fs);
            bw.Write((ushort)keys.Count);
            for (int i = 0; i < keys.Count; i++) {
                bw.Write((char)keys[i]);
            }
            for (int i = 0; i < keys.Count; i++) {
                bw.Write((char)dict2[keys[i]]);
            }
            bw.Close();
            fs.Close();
        }
        #endregion


        #region BuildFenci
        public void BuildFenci(SqlHelper helper, TranslateSearch translateSearch)
        {
            var translateDict = CreateTranslateDict(helper);
            var FenciKeywordInfos = GetFenciKeyword(helper, translateSearch, translateDict);
            var removes = GetKeywordInfos_0(helper);
            FenciKeywordInfos = RemoveFenciKeywordInfos(FenciKeywordInfos, removes);

            var newId = 0;
            Dictionary<string, int> set = new Dictionary<string, int>();
            List<FenciTempKeywordInfo> tempOuts = new List<FenciTempKeywordInfo>();
            foreach (var keywordInfo in FenciKeywordInfos) {
                var key = keywordInfo.ToHashSet();
                if (set.TryGetValue(key,out int val)) {
                    keywordInfo.NewId = val;
                } else {
                    tempOuts.Add(keywordInfo);
                    keywordInfo.NewId = newId;
                    set[key] = newId;
                    newId++;
                }
            }
            set = null;

            var skips = GetSkipwords(helper, 0);

            FenciSearch search = new FenciSearch();
            search.SetKeywords(FenciKeywordInfos, translateDict, skips);
            search.Save(_tempFenciPath);
            WriteFenciKeyword(tempOuts);
            WriteFenciKeywordTxt(FenciKeywordInfos);

            skips = null;
            translateDict = null;

            FenciKeywordInfos.Clear();
            FenciKeywordInfos = null;
        }

        private List<FenciTempKeywordInfo> RemoveFenciKeywordInfos(List<FenciTempKeywordInfo> src, List<FenciTempKeywordInfo> tar)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (var item in tar) {
                set.Add(item.Keyword);
            }
            List<FenciTempKeywordInfo> result = new List<FenciTempKeywordInfo>();
            foreach (var item in src) {
                if (item.Count == 1 && item.EmotionalColor == 0) {
                    if (set.Contains(item.Keyword) == false) {
                        result.Add(item);
                    }
                } else {
                    result.Add(item);
                }
            }
            HashSet<string> set2 = new HashSet<string>();
            for (int i = 0; i < result.Count; i++) {
                result[i].Id = i;
                set2.Add(result[i].Keyword);
            }

            return result;
        }



        private List<FenciTempKeywordInfo> GetKeywordInfos_0(SqlHelper helper)
        {
            var ids = helper.Select<int>("select Id from TxtCustomType where Name Not like '%单字%'");

            TxtCache txtCache = new TxtCache();
            TxtCommonCache txtCommonCache = new TxtCommonCache();
            LoadTxtCommon(helper, txtCommonCache);

            Dictionary<string, int> dict = new Dictionary<string, int>();
            for (int i = 0; i <= 2; i++) {
                var customs = helper.Select<DbTxtCustom>("where RiskLevel =@0 and TxtCustomTypeId in (" + string.Join(",", ids) + ")", i);
                foreach (var custom in customs) {
                    if (custom.Text.Contains("||")) { continue; }
                    if (tagAllRegex.IsMatch(custom.Text)) {
                        var txts = txtCommonCache.GetTxtCommon(custom.Text);
                        foreach (var txt in txts) {
                            var id = txtCache.TryAddTextString(custom, 0, txt);
                            if (dict.ContainsKey(txt) == false) {
                                dict[txt] = id;
                            }
                        }
                    } else {
                        var id = txtCache.TryAddTextString(custom, 0, custom.Text);
                        if (dict.ContainsKey(custom.Text) == false) {
                            dict[custom.Text] = id;
                        }
                    }
                }
            }
            List<FenciTempKeywordInfo> FenciKeywordInfos = new List<FenciTempKeywordInfo>();
            foreach (var item in dict) {
                FenciKeywordInfos.Add(new FenciTempKeywordInfo() {
                    Count = 1,
                    Keyword = item.Key,
                    Id = item.Value
                });
            }
            dict = null;

            return FenciKeywordInfos;
        }

        private List<FenciTempKeywordInfo> GetFenciKeyword(SqlHelper helper, TranslateSearch translateSearch, CharDictionary translateDict)
        {
            var fencis = helper.Select<DbTxtFenci>("where isDelete=0");
            int MiniKeywordInfoIndex = 0;
            List<FenciTempKeywordInfo> FenciKeywordInfos = new List<FenciTempKeywordInfo>();
            HashSet<string> set = new HashSet<string>();

            foreach (var item in fencis) {
                if (string.IsNullOrWhiteSpace(item.Text)) { continue; }
                var txt = translateSearch.Replace(item.Text);
                txt = translateDict.GetTranslate(txt);

                if (set.Add(txt)) {
                    var info = new FenciTempKeywordInfo() {
                        Id = MiniKeywordInfoIndex++,
                        Keyword = txt,
                        Count = item.Count,
                        EmotionalColor = item.EmotionalColor,
                    };
                    FenciKeywordInfos.Add(info);
                }
            }
            fencis = null;

            var fencis2 = helper.Select<DbTxtCustom>("where TxtCustomTypeId in (select Id from TxtCustomType where Name like '%单字%') and RiskLevel=0 ");
            var fencis3 = helper.Select<string>("select Text from TxtCustom where TxtCustomTypeId in (select Id from TxtCustomType where Name like '%单字%') and RiskLevel=3 ");
            ToolGood.Words.StringSearch stringSearch = new Words.StringSearch();
            stringSearch.SetKeywords(fencis3);

            foreach (var item in fencis2) {
                if (string.IsNullOrWhiteSpace(item.Text)) { continue; }
                var txt = translateSearch.Replace(item.Text);
                txt = translateDict.GetTranslate(txt);

                if (stringSearch.ContainsAny(txt)) {
                    if (set.Add(txt)) {
                        var info = new FenciTempKeywordInfo() {
                            Id = MiniKeywordInfoIndex++,
                            Keyword = txt,
                            Count = 1,
                        };
                        FenciKeywordInfos.Add(info);
                    }
                }
            }
            stringSearch = null;
            fencis2 = null;
            fencis3 = null;
            set = null;

            return FenciKeywordInfos;
        }


        private void WriteFenciKeyword(List<FenciTempKeywordInfo> FenciKeywordInfos)
        {
            var fs = File.Open(_tempFenciKeywordPath, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(FenciKeywordInfos.Count);
            foreach (var info in FenciKeywordInfos) {
                bw.Write((sbyte)info.Keyword.Length);
                bw.Write(info.Count);
                bw.Write((sbyte)info.EmotionalColor);
            }
            bw.Close();
            fs.Close();
        }

        private void WriteFenciKeywordTxt(List<FenciTempKeywordInfo> FenciKeywordInfos)
        {
            List<string> result = new List<string>();
            foreach (var info in FenciKeywordInfos) {
                result.Add($"{info.Id} {info.Keyword} {info.Count} {info.EmotionalColor}");
            }
            File.WriteAllLines(_tempFenciKeywordTextPath, result);
        }

        #endregion

    }
}
