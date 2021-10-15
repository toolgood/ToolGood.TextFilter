/* Copyright (c) 2021 温岭城东知君软件开发工作室 All rights reserved. */
/* GPLv3 License - http://www.gnu.org/licenses/gpl-3.0.html   */
using System.IO;
using System.Runtime.CompilerServices;

namespace ToolGood.TextFilter
{
    static partial class BytesUtil
    {
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[] ReadIntArray(this BinaryReader br)
        {
            var length = br.ReadInt32();
            var array = new int[length];
            for (int i = 0; i < length; i++) {
                array[i] = br.ReadInt32();
            }
            return array;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort[] ReadUshortArray(this BinaryReader br)
        {
            var length = br.ReadInt32();
            var array = new ushort[length];
            for (int i = 0; i < length; i++) {
                array[i] = br.ReadUInt16();
            }
            return array;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int[][] ReadIntArray2(this BinaryReader br)
        {
            var length = br.ReadInt32();
            var array = new int[length][];
            for (int i = 0; i < length; i++) {
                var len = br.ReadInt32();
                var arr = new int[len];
                for (int j = 0; j < len; j++) {
                    arr[j] = br.ReadInt32();
                }
                array[i] = arr;
            }
            return array;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool[] ReadBoolArray(this BinaryReader br)
        {
            var length = br.ReadInt32();
            var array = new bool[length];
            for (int i = 0; i < length; i++) {
                array[i] = br.ReadBoolean();
            }
            return array;
        }
 


    }
}
