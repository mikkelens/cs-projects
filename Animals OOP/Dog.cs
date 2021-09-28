using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals_OOP
{
    class Dog : Pet
    {
        #region Fields
        private readonly string name;
        private readonly int age;
        private DateTime feedTime;
        #endregion

        // Constructor
        public Dog(string name = "", int age = 0)
        {
            this.name = name;
            this.age = age;
            feedTime = DateTime.Now;
        }

        #region Metoder

        public void Feed()
        {
            feedTime = DateTime.Now;
        }

        public bool IsHungry()
        {
            TimeSpan timeSinceLastMeal = DateTime.Now - feedTime;

            return timeSinceLastMeal.TotalSeconds > 5;
        }
        public string GetDescription()
        {
            return $"Min hund hedder {name}.\n"
                   + $"{name} er {age} år gammel.";
        }

        public string GetName()
        {
            return name;
        }

        public int GetAge()
        {
            return age;
        }
        #endregion
    }

}
