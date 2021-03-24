using System.Collections;

namespace EARIN_EX2.Helpers {
    public static class BitArrayHelper {
        public static BitArray SkipLast(this BitArray bitArray, int skip) {
            BitArray ba = new BitArray(bitArray.Length - skip);

            for (int i = 0; i < bitArray.Length - skip; i++) {
                ba[i] = bitArray[i];
            }

            return ba;
        }
    }
}
