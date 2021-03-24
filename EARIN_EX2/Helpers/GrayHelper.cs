using Numpy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EARIN_EX2.Helpers {
    public class GrayHelper {
        private readonly int offset;
        private readonly int bitsToCut;

        public GrayHelper(int maxAbsoluteValue) {
            this.offset = maxAbsoluteValue;

            var ba = ToGray(maxAbsoluteValue);
            int shiftCount = 0;
            while (!ba.Get(ba.Length - 1) && shiftCount < 32) {
                ba = ba.LeftShift(1);
                shiftCount++;
            }

            bitsToCut = shiftCount + 1;
        }

        public int ToInt(BitArray grayBa) {
            byte[] bytes = new byte[4];
            grayBa.CopyTo(bytes, 0);
            uint grayed = BitConverter.ToUInt32(bytes);
            int numWithOffset = InverseGrayCode(grayed);
            return numWithOffset - offset;

            int InverseGrayCode(uint num) {
                uint inv = 0;

                for (; num != 0; num >>= 1)
                    inv ^= num;

                return (int)inv;
            }
        }

        public BitArray ToGray(int num) {
            var numWithOffset = (uint)(offset + num);
            var grayed = (numWithOffset >> 1) ^ numWithOffset;

            return new BitArray(BitConverter.GetBytes(grayed)).SkipLast(bitsToCut);
        }


        public NDarray GrayToNDarray(IEnumerable<BitArray> grayedBitArrays) {
            return (NDarray)grayedBitArrays.Select(gBa => ToInt(gBa)).ToArray();
        }

        public BitArray[] NDarrayToGray(NDarray array) {
            var size = array.size;
            BitArray[] bitArrays = new BitArray[size];

            for (int i = 0; i < size; i++) {
                bitArrays[i] = ToGray((int)array[i]);
            }

            return bitArrays;
        }

    }
}
