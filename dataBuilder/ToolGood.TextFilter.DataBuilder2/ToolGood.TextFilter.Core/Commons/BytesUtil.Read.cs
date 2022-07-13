using System;
using System.IO;

namespace ToolGood.TextFilter
{
    static partial class BytesUtil
    {
        internal static Int32[] ByteArrToIntArr(byte[] btArr)
        {
            Int32 intSize = (int)Math.Ceiling(btArr.Length / (double)sizeof(Int32));
            Int32[] intArr = new Int32[intSize];
            Buffer.BlockCopy(btArr, 0, intArr, 0, btArr.Length);
            return intArr;
        }
        internal static ushort[] ByteArrToUshortArr(byte[] btArr)
        {
            Int32 intSize = (int)Math.Ceiling(btArr.Length / (double)sizeof(ushort));
            ushort[] intArr = new ushort[intSize];
            Buffer.BlockCopy(btArr, 0, intArr, 0, btArr.Length);
            return intArr;
        }



        public static string[] ReadStringArray(this BinaryReader br)
        {
            var length = br.ReadInt32();
            var array = new string[length];
            for (int i = 0; i < length; i++) {
                array[i] = br.ReadString();
            }
            return array;
        }
        public static int[] ReadIntArray(this BinaryReader br)
        {
            var length = br.ReadInt32();
            var array = new int[length];
            for (int i = 0; i < length; i++) {
                array[i] = br.ReadInt32();
            }
            return array;
        }
        public static ushort[] ReadUshortArray(this BinaryReader br)
        {
            var length = br.ReadInt32();
            var array = new ushort[length];
            for (int i = 0; i < length; i++) {
                array[i] = br.ReadUInt16();
            }
            return array;
        }
        public static byte[] ReadByteArray(this BinaryReader br)
        {
            var length = br.ReadInt32();
            var array = new byte[length];
            for (int i = 0; i < length; i++) {
                array[i] = br.ReadByte();
            }
            return array;
        }
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
        public static bool[] ReadBoolArray(this BinaryReader br)
        {
            var length = br.ReadInt32();
            var array = new bool[length];
            for (int i = 0; i < length; i++) {
                array[i] = br.ReadBoolean();
            }
            return array;
        }

        //public static UInt16Dictionary[] ReadIntDictionaryArray(this BinaryReader br)
        //{
        //    var length = br.ReadInt32();
        //    var array = new UInt16Dictionary[length];
        //    for (int i = 0; i < length; i++) {
        //        var key = br.ReadUshortArray();
        //        var value = br.ReadUshortArray();
        //        array[i] = new UInt16Dictionary(key, value);
        //    }
        //    return array;
        //}


    }
}
