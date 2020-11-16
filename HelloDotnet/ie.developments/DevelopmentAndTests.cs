using System;
using System.Collections.Generic; // need for List<T>
using ie.delegates.reactives;

namespace ie.developments
{
    public class TestRepository: IRunnable {
        private readonly Func<int, int> callback;

        public TestRepository(Func<int, int> callback) {
            this.callback = callback;
        }

        void IRunnable.run() {
            int[] intArray = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            for(int i = 0; i < intArray.Length; i++) {
                int item = intArray[i];
                Console.WriteLine($"Element #{i}: {item}");            
            }
            int j = 0;
            foreach(int item in intArray) {
                Console.WriteLine($"Element #{j}: {item}");            
            }

            var fibNumbers = new List<int> { 0, 1, 1, 2, 3, 5, 8, 13 };
            int count = 0;
            foreach(int element in fibNumbers) {
                count++;
                Console.WriteLine($"Element #{count}: {element}");
            }
            Console.WriteLine($"Number of elements: {count}");

            if (null != callback) {
                callback.Invoke(count);
            }
        }
    }
}