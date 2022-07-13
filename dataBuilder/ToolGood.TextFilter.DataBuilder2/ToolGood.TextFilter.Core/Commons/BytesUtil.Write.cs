using System;
using System.IO;

namespace ToolGood.TextFilter
{
    static partial class BytesUtil
    {
        internal static byte[] IntArrToByteArr(Int32[] intArr)
        {
            Int32 intSize = sizeof(Int32) * intArr.Length;
            byte[] bytArr = new byte[intSize];
            Buffer.BlockCopy(intArr, 0, bytArr, 0, intSize);
            return bytArr;
        }
        internal static byte[] IntArrToByteArr(ushort[] intArr)
        {
            Int32 intSize = sizeof(ushort) * intArr.Length;
            byte[] bytArr = new byte[intSize];
            Buffer.BlockCopy(intArr, 0, bytArr, 0, intSize);
            return bytArr;
        }




        public static void Write(this BinaryWriter bw, string[] txts)
        {
            bw.Write(txts.Length);
            foreach (var txt in txts) {
                bw.Write(txt);
            }
        }
        public static void Write(this BinaryWriter bw, int[] array)
        {
            bw.Write(array.Length);
            foreach (var txt in array) {
                bw.Write(txt);
            }
        }
        public static void Write(this BinaryWriter bw, ushort[] array)
        {
            bw.Write(array.Length);
            foreach (var txt in array) {
                bw.Write((ushort)txt);
            }
        }
        public static void Write(this BinaryWriter bw, byte[] array)
        {
            bw.Write(array.Length);
            foreach (var txt in array) {
                bw.Write(txt);
            }
        }
        public static void Write(this BinaryWriter bw, int[][] array)
        {
            bw.Write(array.Length);
            foreach (var arr in array) {
                bw.Write(arr.Length);
                foreach (var a in arr) {
                    bw.Write(a);
                }
            }
        }

        public static void Write(this BinaryWriter bw, bool[] array)
        {
            bw.Write(array.Length);
            foreach (var txt in array) {
                bw.Write(txt);
            }
        }

    }
}
