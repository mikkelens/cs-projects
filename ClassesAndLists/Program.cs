using System;
using System.Collections.Generic;

namespace ClassesAndLists
{
    class Program
    {
        static void Main(string[] args)
        {
            Array<int> ourArray = new Array<int>(5);
            int[] numbers = new int[]
            {
                2, 13, 5, 11, 7
            };
            for (int i = 0; i < ourArray.Lenght(); i++)
            {
                ourArray.SetValue(i, numbers[i]);
            }

            DynamicList<int> ourDynamicList = new DynamicList<int>();
            TestArray(ourArray);
            TestDynamicList();
        }

        static void TestArray(Array<int> staticArray)
        {
            Array<string> names = new Array<string>(5);
            names.SetValue(0, "Hans");
            Console.WriteLine($"Element 0 er: \"{names.Element(0)}\"");
            Console.WriteLine($"Element 2 er: \"{names.Element(2)}\"");
            Console.WriteLine(names.Lenght());
        }

        static void TestDynamicList()
        {
            List<int> standardList = new List<int>();
            DynamicList<int> numberSequence = new DynamicList<int>();
            for (int i = 0; i < 9; i++)
            {
                numberSequence.Add(i);
                standardList.Add(i);
            }
            standardList.RemoveAt(3);
            numberSequence.Remove(3);
            Console.WriteLine("C# standard list:");
            Console.WriteLine(standardList.Capacity);
            Console.WriteLine(string.Join(", ", standardList));
            Console.WriteLine("Own dynamic list:");
            Console.WriteLine(numberSequence.Capacity());
            Console.WriteLine(numberSequence.ToString());
        }
    }

    class Array<T>
    {
        public T[] actualArray;

        public Array(int count)
        {
            actualArray = new T[count];
        }

        public void SetValue(int index, T value)
        {
            actualArray[index] = value;
        }

        public T Element(int index)
        {
            return actualArray[index];
        }

        public int Lenght()
        {
            return actualArray.Length;
        }
    }

    class DynamicList<T>
    {
        private T[] actualList;
        //private int count = 0;

        public void Add(T element)
        {
            int oldLenght = actualList?.Length ?? 0;
            T[] newList = new T[oldLenght + 1];
            for (int i = 0; i < oldLenght; i++)
            {
                newList[i] = actualList[i];
            }
            newList[oldLenght] = element;
        }

        private T[] CopyToNew(T[] oldList, int newLenght)
        {
            T[] newList = new T[newLenght];
            for (int i = 0; i < actualList.Length; i++)
            {
                newList[i] = actualList[i];
            }
            return newList;
        }

        public int Lenght()
        {
            return actualList.Length;
        }

        public int Capacity()
        {
            if (actualList != null)
            {
                return actualList.Length;
            }
            else
            {
                return default;
            }
        }

        public T Element(int index)
        {
            return actualList[index];
        }

        public void Remove(int index)
        {
            if (actualList != null)
            {
                if (actualList.Length >= index)
                {
                    actualList[index] = default;
                }
            }
        }

        public override string ToString()
        {
            if (actualList is not null)
            {
                return string.Join(", ", actualList);
            }
            else
            {
                return "";
            }
        }
    }
}

