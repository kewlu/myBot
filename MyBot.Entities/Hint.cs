using System;
using System.Collections.Generic;
using System.Text;

namespace MyBot.Entities
{
    public class Hint
    {
        public StringBuilder _currentHint { get; set; }

        public StringBuilder _answer { get; set; }

        public int AmountHinted { get; set; }

        public Hint(string answer)
        {
            StringBuilder _answer = new StringBuilder(answer);
            StringBuilder _currentHint = new StringBuilder(""); 
            AmountHinted = 0;
            for (int i = 0; i<answer.Length; i++)
            {
                _currentHint.Append("*");
            }
        }

        private StringBuilder UpdateHint(StringBuilder updateHint)
        {
            Random rnd = new Random();
            var i = rnd.Next(0, (_currentHint.Length - 1));

            updateHint[i] = _answer[i];
            if(_currentHint.Equals(updateHint))
            {
                _currentHint = UpdateHint(updateHint);
            }
            AmountHinted++;
            return updateHint;
        }

        public void UpdateHint()
        {
            UpdateHint(_currentHint);
        }
    }
}
