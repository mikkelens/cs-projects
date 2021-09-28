using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals_OOP
{
    class Bunny : Pet
    {
        private const float defaultHungerTime = 6f;

        private string name;
        private int age;
        private bool likesHugs;

        private float secondsBeforeHungry;
        private DateTime lastFeedTime;

        public Bunny(string name, int age, bool likesHugs = true, float secondsBeforeHungry = defaultHungerTime)
        {
            this.name = name;
            this.age = age;
            this.likesHugs = likesHugs;
            this.secondsBeforeHungry = secondsBeforeHungry;

            lastFeedTime = DateTime.Now;
        }

        #region Public Methods

        public void Feed()
        {
            lastFeedTime = DateTime.Now;
            DisplaySentence($"{name} was fed at {lastFeedTime.TimeOfDay}.");
        }

        public bool IsHungry()
        {
            TimeSpan timeSinceFeed = DateTime.Now - lastFeedTime;
            return timeSinceFeed.TotalSeconds > secondsBeforeHungry;
        }

        public string GetDescription()
        {
            return $"{name} is {age} years old and does{(likesHugs ? " " : " NOT ")}like hugs.";
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

        #region Internal Methods
        void DisplaySentence(string message)
        {
            Console.WriteLine(message);
        }
        #endregion
    }
}
