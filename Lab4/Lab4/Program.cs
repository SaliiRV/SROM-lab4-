using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4 {
    public class Program {
        static void Main(string[] args) {
            string a = "00111001111000010101001111100110111011001010110000101111010011100111111111110010101011001010000111000111000101111100001001000001000101111101100100110000011010101110101110011";
            var b = ToByte(a);

            Console.WriteLine(ToStr(VKvadrat(b)));

            Console.ReadKey();

        }


        public static int[] ToByte(string a) {
            int l = a.Length;
            int[] result = new int[l];
            for (int i = 0; i < l; i++) {
                result[l - 1 - i] = Convert.ToByte(a.Substring(l - (i + 1), 1), 2);
            }
            return result;
        }

        public static string ToStr(int[] a) {
            string result = null;
            for (int i = a.Length - 1; i >= 0; i--) {
                result = a[i].ToString() + result;
            }
            return result;
        }


        public static int[] Addition(int[] a, int[] b) {
            var maxlenght = Math.Max(a.Length, b.Length);
            Array.Resize(ref a, maxlenght);
            Array.Resize(ref b, maxlenght);

            int[] result = new int[maxlenght];
            for (int i = 0; i < a.Length; i++) {
                result[i] = (a[i] ^ b[i]);
            }
            return result;
        }


        public static int[] VKvadrat(int[] a) {
            int[] result = new int[a.Length];
            for (int i = 0; i < a.Length - 1; i++) {
                result[i + 1] = a[i];
            }
            result[0] = a[a.Length - 1];
            return result;
        }


        public static int Tr(int[] a) {
            int result = 0;
            for (int i = 0; i < a.Length; i++)
                result = result ^ a[i];
            return result;
        }

        public static int[] ShiftLeft(int[] a) {
            int[] result = new int[a.Length];
            for (int i = 1; i < a.Length; i++) {
                result[i - 1] = a[i];
            }
            result[a.Length - 1] = a[0];
            return result;
        }

        public static int[] Multiply(int[] a, int[] b) {
            int[] result = new int[a.Length];
            int[,] M = Lyambda(a.Length);
            for (int z = 0; z < a.Length; z++) {
                int[] temp = new int[a.Length];
                for (int j = 0; j < a.Length; j++) {
                    for (int i = 0; i < a.Length; i++)
                        temp[j] = (temp[j]) ^ (a[i] & M[i, j]);
                }
                int k = 0;
                for (int i = 0; i < a.Length; i++)
                    k = (k) ^ (temp[i] & b[i]);
                result[z] = k;
                a = ShiftLeft(a);
                b = ShiftLeft(b);
            }
            return result;
        }

        public static int[,] Lyambda(int m) {
            int p = 2 * m + 1;
            int[,] result = new int[m, m];
            int[] pp = new int[m];
            pp[0] = 1;
            for (int i = 1; i < m; i++)
                pp[i] = (pp[i - 1] * 2) % p;

            int pi, pj;

            for (int i = 0; i < m; i++) {
                pi = pp[i];

                for (int j = 0; j < m; j++) {
                    pj = pp[j];

                    if ((((pi - pj) + p) % p) == 1 || ((pi + pj) % p) == 1 || (((-pi - pj) + p) % p) == 1 || ((pj - pi + p) % p) == 1) {
                        result[i, j] = 1;
                        continue;
                    }
                    result[i, j] = 0;
                }
            }
            return result;
        }


        public static int[] Power(int[] a, int[] n) {
            int[] result = new int[a.Length];
            string One = "11111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111";
            result = ToByte(One);
            for (var i = a.Length - 1; i >= 0; i--) {
                if (n[i] == 1) {
                    result = Multiply(result, a);
                }
                a = VKvadrat(a);
            }
            return result;
        }


        public static int[] Beta(int[] a, int k) {
            int[] result = new int[a.Length];
            result = a;
            for (int i = 1; i <= k; i++) {
                result = VKvadrat(result);
            }
            return result;
        }


        public static int[] Inv(int[] a) {
            string l = "110111010";
            int[] m = new int[l.Length];
            m = ToByte(l);
            int k = 1;
            int[] result = new int[a.Length];
            result = a;
            for (int i = 1; i < l.Length; i++) {
                result = Multiply(Beta(result, k), result);
                k = 2 * k;
                if (m[i] == 1) {
                    result = Multiply(VKvadrat(result), a);
                    k = k + 1;
                }
            }
            result = VKvadrat(result);
            return result;
        }

    }
}
