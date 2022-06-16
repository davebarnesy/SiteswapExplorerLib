using System;

namespace SiteswapLib
{

    public class Throw
    {

        public readonly static char[] allowedCharacters = new char[] {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
            'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
            'u', 'v', 'w', 'x', 'y', 'z'
        };

        public static Throw Create(char value)
        {
            var index = Array.IndexOf(Throw.allowedCharacters, value);
            if (index == -1)
            {
                // invalid character
                return null;
            }
            return new Throw(index);
        }

        public static Throw Create(int duration)
        {
            if (duration < 0 || duration >= allowedCharacters.Length)
            {
                return null;
            }
            return new Throw(duration);
        }

        public int Duration { get; private set; }

        public Throw(int duration)
        {
            this.Duration = duration;
        }

        public string AsString
        {
            get => allowedCharacters[Duration].ToString();
        }

        public Throw IncreasedBy(int duration)
        {
            return Create(this.Duration + duration);
        }

        public Throw DecreasedBy(int duration)
        {
            return Create(this.Duration - duration);
        }

        internal void ReplaceDuration(int newDuration)
        {
            Duration = newDuration;
        }
    }
}
