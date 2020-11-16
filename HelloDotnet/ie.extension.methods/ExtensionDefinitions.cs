using System;

namespace ie.extension.methods
{    
    public static class IntExtensions {

        /** 
         * [Reference](https://www.tutorialsteacher.com/csharp/csharp-extension-method)
         */
        public static bool IsGreaterThan(this int i, int value) {
            return i > value;
        }
    }
    
    public static class StringExtensions {
        /** 
         * [Reference](https://www.codingame.com/playgrounds/5808/extension-methods-in-c)
         */
        public static int WordCount(this string s) {
            return s.Split(new char[] {' ', '.','?'}, StringSplitOptions.RemoveEmptyEntries)
                    .Length;
        }
    }
}