using System;
using System.Collections.Generic;


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

    public static class DictExtensions {
        /** 
         * [Reference](https://www.techiedelight.com/add-contents-of-dictionary-to-another-dictionary-csharp/)
         */
        public static void Append<Key, Value>(this IDictionary<Key, Value> destnation, IDictionary<Key, Value> source) {
            foreach (KeyValuePair<Key, Value> item in source) {
                if (destnation.ContainsKey(item.Key)) {  
                    Console.WriteLine("DictExtensions - Append: Dictionary destnation has contained key: {0} --> override !!", item.Key);
                }             
                destnation[item.Key] = item.Value;
            }
        }
    }
}