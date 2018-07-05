using System;

namespace BusinessLayer.Examples
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MinMaxAttribute : Attribute
    {

        public MinMaxAttribute(int min, int max)
        {
            Minimum = min;
            Maximum = max;
            Message = "";
        }

        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public string Message { get; set; }
    }

    public class Person
    {
        [MinMax(15, 75)]
        public int Age { get; set; }

        [MinMax(15, 75, Message = "customMess")]
        public int Height { get; set; }
    }
}
