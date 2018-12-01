using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevelopmentConsole
{
    public class Hint
    {
        public StringBuilder Answer { get; set; }
        public List<int> numbers { get; set;}
        public Hint(string str)
        {
            StringBuilder Answer = new StringBuilder(str);

            List<int> numbers = new List<int>();
            numbers.AddRange(Enumerable.Range(1, str.Length).ToList());/* = Enumerable.Range(1, str.Length).ToList();*/
            //foreach (var i in numbers)
            //{
            //    Console.WriteLine(i);
            //}

        }
        
        public bool UpdateHint()
        {

            foreach(var i in numbers)
            {
                Console.WriteLine(i);
            }
            return true;
        }
    }
}
