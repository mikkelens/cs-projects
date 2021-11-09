using System;
using System.Linq;
using System.Threading;

namespace InterfaceIntro
{
    class Program
    {
        // start //

        static void Main(string[] args)
        {
            //Example();
            //A2();
            //B2();
            C2();

            Thread.Sleep(750);
            Console.WriteLine("\n\n\nPress any key to exit.");
            Console.ReadKey();
        }

        // assignments //
        
        #region C2
        static void C2()
        {

        }

        #endregion

        #region B2
        static void B2()
        {
            int value1 = 7;
            int value2 = 4;
            Console.WriteLine($"Values: {value1} & {value2}");
            
            int addSum = new Calculator().Add(value1, value2);
            Console.WriteLine($"Added together: {addSum}");

            int minValue = new Calculator().Min(value1, value2);
            Console.WriteLine($"Smallest value: {minValue}");
        }

        interface IIntegerTool
        {
            /// <summary>
            /// Compute the sum of two integers.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            int Add(int a, int b);

            /// <summary>
            /// Determines the minimum of two integers.
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            int Min(int a, int b);
        }

        class Calculator : IIntegerTool
        {
            public int Add(int a, int b)
            {
                return a + b;
            }

            public int Min(int a, int b)
            {
                return a <= b ? a : b;
            }
        }
        #endregion

        #region A2
        static void A2()
        {
            float[] values = new float[6]
            {
                1.3f,
                5.7f,
                9.1f,
                3.141592f,
                17f,
                -13.8f
            };

            //float[] newValues = SwapArrayValues(values, 0, 4);

            float[] sortedValues = SortValues(values);
            Console.WriteLine($"Sorted array: \n{string.Join(";\n", sortedValues)}");
        }

        static float[] SortValues(float[] values) // sorts ascending (lowest first)
        {
            for (int i = 0; i < values.Length; i++)
            {
                for (int j = i; j < values.Length; j++)
                {
                    if (values[i] > values[j])
                    {
                        values = SwapArrayValues(values, i, j);
                        //Console.WriteLine($"Swapped index {i} ({values[i]}) and {j} ({values[j]}).");
                    }
                }
            }

            return values;
        }

        static float[] SwapArrayValues(float[] values, int i1, int i2)
        {
            float temp = values[i1];
            values[i1] = values[i2];
            values[i2] = temp;
            return values;
        }
        #endregion

        #region Example area
        static void Example()
        {
            IProduct[] shoppingCart = new IProduct[]
            {
                new Watermelon(),
                new Toothbrush()
            };

            float total = 0;
            for (int i = 0; i < shoppingCart.Length; i++)
            {
                total += shoppingCart[i].GetPrice();
            }
            Console.WriteLine($"The total amount will be {total}.");
        }
        #endregion
    }
}
