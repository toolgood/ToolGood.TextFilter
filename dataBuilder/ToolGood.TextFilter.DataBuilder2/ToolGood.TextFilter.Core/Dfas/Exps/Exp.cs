using System;
using System.Collections.Generic;
using System.Text;

namespace ToolGood.DFAs
{
    public abstract class Exp
    {


        #region BuildENfa
        /// <summary>
        /// 根据当前的正则表达式构造 NFA。
        /// </summary>
        /// <param name="nfa">要构造的 NFA。</param>
        internal abstract void BuildENfa(ENfa nfa);
        #endregion

        #region GetChars SetTarChars
        public List<string> GetChars()
        {
            List<string> list = new List<string>();
            this.GetChars(list);
            return list;
        }
        public abstract void GetChars(List<string> list);

        public abstract void GetChars(List<Tuple<string, int>> list, ref int layer);

        public abstract void GetChars(Action<List<char>, int> action, ref int layer);

        public abstract void SetFirst(bool[] endKeys, ref int layer);

 
        public abstract void SetOnlyEnd(bool[] onlyEndKeys, bool once, ref int layer);


        public abstract int GetCharExpCount();
        /// <summary>
        /// 是否只有字符
        /// </summary>
        /// <returns></returns>
        public abstract bool IsOnlyChars();

        #endregion





        #region 生成EXP
        /// <summary>
        /// 返回表示单个字符的正则表达式。
        /// </summary>
        /// <param name="ch">要表示的字符。</param>
        /// <returns>表示单个字符的正则表达式。</returns>
        public static Exp CreateSymbol(char ch)
        {
            return new CharExp(ch.ToString());
        }


        /// <summary>
        /// 返回表示字符类的正则表达式。
        /// </summary>
        /// <param name="cc">正则表达式表示的字符类。</param>
        /// <returns>表示字符类的正则表达式。</returns>
        /// <overloads>
        /// <summary>
        /// 返回表示字符类的正则表达式。
        /// </summary>
        /// </overloads>
        public static Exp CreateCharClass(string cc)
        {
            return new CharExp(cc);
        }

        /// <summary>
        /// 返回表示 Kleene 闭包的正则表达式。
        /// </summary>
        /// <param name="innerExp">Kleene 闭包的内部正则表达式。</param>
        /// <returns>表示 Kleene 闭包的正则表达式。</returns>
        /// <overloads>
        /// <summary>
        /// 返回表示 Kleene 闭包的正则表达式。
        /// </summary>
        /// </overloads>
        public static Exp CreateStar(Exp innerExp)
        {
            return new RepeatExp(innerExp, 0, int.MaxValue);
        }
        /// <summary>
        /// 返回表示正闭包的正则表达式。
        /// </summary>
        /// <param name="innerExp">正闭包的内部正则表达式。</param>
        /// <returns>表示正闭包的正则表达式。</returns>
        /// <overloads>
        /// <summary>
        /// 返回表示正闭包的正则表达式。
        /// </summary>
        /// </overloads>
        public static Exp CreatePositive(Exp innerExp)
        {
            return new RepeatExp(innerExp, 1, int.MaxValue);
        }
        /// <summary>
        /// 返回表示可选的正则表达式。
        /// </summary>
        /// <param name="innerExp">可选的内部正则表达式。</param>
        /// <returns>表示可选的正则表达式。</returns>
        /// <overloads>
        /// <summary>
        /// 返回表示可选的正则表达式。
        /// </summary>
        /// </overloads>
        public static Exp CreateOptional(Exp innerExp)
        {
            return new RepeatExp(innerExp, 0, 1);
        }
        /// <summary>
		/// 返回表示重复多次的正则表达式。
		/// </summary>
		/// <param name="innerExp">重复的的内部正则表达式。</param>
		/// <param name="times">重复次数。</param>
		/// <returns>表示重复多次的正则表达式。</returns>
		/// <overloads>
		/// <summary>
		/// 返回表示重复多次的正则表达式。
		/// </summary>
		/// </overloads>
		public static Exp CreateRepeat(Exp innerExp, int times)
        {
            return new RepeatExp(innerExp, times, times);
        }
        /// <summary>
        /// 返回表示重复多次的正则表达式。
        /// </summary>
        /// <param name="innerExp">重复的的内部正则表达式。</param>
        /// <param name="minTimes">最少的重复次数。</param>
        /// <param name="maxTimes">最多的重复次数。</param>
        /// <returns>表示重复多次的正则表达式。</returns>
        public static Exp CreateRepeat(Exp innerExp, int minTimes, int maxTimes)
        {
            return new RepeatExp(innerExp, minTimes, maxTimes);
        }
        /// <summary>
        /// 返回表示至少重复 <paramref name="minTimes"/> 次的正则表达式。
        /// </summary>
        /// <param name="innerExp">重复的的内部正则表达式。</param>
        /// <param name="minTimes">最少的重复次数。</param>
        /// <returns>表示至少重复 <paramref name="minTimes"/> 次的正则表达式。</returns>
        /// <overloads>
        /// <summary>
        /// 返回表示至少重复 <paramref name="minTimes"/> 次的正则表达式。
        /// </summary>
        /// </overloads>
        public static Exp CreateRepeatMinTimes(Exp innerExp, int minTimes)
        {
            return new RepeatExp(innerExp, minTimes, int.MaxValue);
        }
        /// <summary>
        /// 返回表示至多重复 <paramref name="maxTimes"/> 次的正则表达式。
        /// </summary>
        /// <param name="innerExp">重复的的内部正则表达式。</param>
        /// <param name="maxTimes">最多的重复次数。</param>
        /// <returns>表示至多重复 <paramref name="maxTimes"/> 次的正则表达式。</returns>
        /// <overloads>
        /// <summary>
        /// 返回表示至多重复 <paramref name="maxTimes"/> 次的正则表达式。
        /// </summary>
        /// </overloads>
        public static Exp CreateRepeatMaxTimes(Exp innerExp, int maxTimes)
        {
            return new RepeatExp(innerExp, 0, maxTimes);
        }
        /// <summary>
        /// 返回表示两个正则表达式连接的正则表达式。
        /// </summary>
        /// <param name="left">要连接的第一个正则表达式。</param>
        /// <param name="right">要连接的第二个正则表达式。</param>
        /// <returns>表示两个正则表达式连接的正则表达式。</returns>
        /// <overloads>
        /// <summary>
        /// 返回表示两个正则表达式连接的正则表达式。
        /// </summary>
        /// </overloads>
        public static Exp CreateConcat(Exp left, Exp right)
        {
            return new ConcatenationExp(left, right);
        }
        /// <summary>
        /// 返回表示两个正则表达式并联的正则表达式。
        /// </summary>
        /// <param name="left">要并联的第一个正则表达式。</param>
        /// <param name="right">要并联的第二个正则表达式。</param>
        /// <returns>表示两个正则表达式并联的正则表达式。</returns>
        /// <overloads>
        /// <summary>
        /// 返回表示两个正则表达式并联的正则表达式。
        /// </summary>
        /// </overloads>
        public static Exp CreateUnion(Exp left, Exp right)
        {
            return new AlternationExp(left, right);
        }

        /// <summary>
        /// 返回表示当前正则表达式的 Kleene 闭包的正则表达式。
        /// </summary>
        /// <returns>表示当前正则表达式的 Kleene 闭包的正则表达式。</returns>
        public Exp Star()
        {
            return CreateStar(this);
        }
        /// <summary>
        /// 返回表示当前正则表达式的正闭包的正则表达式。
        /// </summary>
        /// <returns>表示当前正则表达式的正闭包的正则表达式。</returns>
        public Exp Positive()
        {
            return CreatePositive(this);
        }
        /// <summary>
        /// 返回表示当前正则表达式可选的正则表达式。
        /// </summary>
        /// <returns>表示当前正则表达式可选的则表达式。</returns>
        public Exp Optional()
        {
            return CreateOptional(this);
        }
        /// <summary>
        /// 返回表示当前正则表达式重复多次的正则表达式。
        /// </summary>
        /// <param name="times">重复次数。</param>
        /// <returns>表示当前正则表达式重复多次的正则表达式。</returns>
        public Exp Repeat(int times)
        {
            return CreateRepeat(this, times);
        }
        /// <summary>
        /// 返回表示当前正则表达式重复多次的正则表达式。
        /// </summary>
        /// <param name="minTimes">最少的重复次数。</param>
        /// <param name="maxTimes">最多的重复次数。</param>
        /// <returns>表示当前正则表达式重复多次的正则表达式。</returns>
        public Exp Repeat(int minTimes, int maxTimes)
        {
            return CreateRepeat(this, minTimes, maxTimes);
        }
        /// <summary>
        /// 返回表示当前正则表达式至少重复 <paramref name="minTimes"/> 次的正则表达式。
        /// </summary>
        /// <param name="minTimes">最少的重复次数。</param>
        /// <returns>表示当前正则表达式至少重复 <paramref name="minTimes"/> 次的正则表达式。</returns>
        public Exp RepeatMinTimes(int minTimes)
        {
            return CreateRepeat(this, minTimes, int.MaxValue);
        }
        /// <summary>
        /// 返回表示当前正则表达式至多重复 <paramref name="maxTimes"/> 次的正则表达式。
        /// </summary>
        /// <param name="maxTimes">最多的重复次数。</param>
        /// <returns>表示当前正则表达式至多重复 <paramref name="maxTimes"/> 次的正则表达式。</returns>
        public Exp RepeatMaxTimes(int maxTimes)
        {
            return CreateRepeat(this, 0, maxTimes);
        }
        /// <summary>
        /// 返回表示当前正则表达式与指定的正则表达式连接的正则表达式。
        /// </summary>
        /// <param name="right">要连接的正则表达式。</param>
        /// <returns>表示两个正则表达式连接的正则表达式。</returns>
        public Exp Concat(Exp right)
        {
            return CreateConcat(this, right);
        }
        /// <summary>
        /// 返回表示当前正则表达式与指定的正则表达式并联的正则表达式。
        /// </summary>
        /// <param name="right">要并联的个正则表达式。</param>
        /// <returns>表示两个正则表达式并联的正则表达式。</returns>
        public Exp Union(Exp right)
        {
            return CreateUnion(this, right);
        }
        #endregion


        public abstract bool HasRepeat();

        public abstract void Reverse();

        internal protected abstract void ToString(StringBuilder stringBuilder);

        public abstract bool EqualExp(Exp exp);

        public abstract void SetActionFindFalse();
        public abstract bool HasInfinite();
    }

}

