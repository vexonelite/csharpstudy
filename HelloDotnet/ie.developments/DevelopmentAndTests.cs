using System;
using System.Collections.Generic;   // need for List<T>
using System.Diagnostics;           // need for Stopwatch
using System.Threading.Tasks;       // need for Parallel.For() Method
using System.Linq;                  // need for usage of Enumerable.Sum() Method, e.g., List<int>.Sum()
using ie.delegates.reactives;

namespace ie.developments
{
    public class TestRepository: IRunnable {
        private readonly Action<int> callback;

        public TestRepository(Action<int> callback) {
            this.callback = callback;
        }

        //void IRunnable.run() { // such statement will incur a building error
        public void run() {        
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

    public abstract class AbsPrimeNumberFinder: IRunnable {
        protected List<int> GetPrimeNumbers(int minimum, int maximum) {
            List<int> result = new List<int>();
            for (int i = minimum; i <= maximum; i++) {
                if (IsPrimeNumber(i)){
                    result.Add(i);
                }
            }
            return result;           
        }

        protected async Task<List<int>> GetPrimeNumbersAsync(int minimum, int maximum) {
            var count = maximum - minimum + 1;
            List<int> result = new List<int>();

            return await Task.Factory.StartNew(() =>
            {
                for (int i = minimum; i <= maximum; i++) {
                    if (IsPrimeNumber(i)) {
                        result.Add(i);
                    }
                }
                return result;
            });
        }

        protected bool IsPrimeNumber(int number) {
            if (number % 2 == 0) {
                return number == 2;
            }
            else {
                var topLimit = (int)Math.Sqrt(number);            
                for (int i = 3; i <= topLimit; i += 2) {
                    if (number % i == 0) return false;
                }
                return true;
            }
        }

        public abstract void run();
    }

    public class SyncPrimeNumberFinder: AbsPrimeNumberFinder {
        public override void run() {
            var sw = new Stopwatch();
            sw.Start();
            var primes = GetPrimeNumbers(2, 10000000);
            Console.WriteLine("Total prime numbers: {0}\nProcess time: {1}", primes.Count, sw.ElapsedMilliseconds);
        }
    }

    public class ParallelPrimeNumberFinder: AbsPrimeNumberFinder {
        public override void run() {
            var sw = new Stopwatch();
            sw.Start();
            const int numParts = 10;
            var primes = new List<int>[numParts];
            Parallel.For(0, numParts, i => primes[i] = GetPrimeNumbers(i == 0 ? 2 : i * 1000000 + 1, (i + 1) * 1000000));
            var result = primes.Sum(p => p.Count);
            Console.WriteLine("Total prime numbers: {0}\nProcess time: {1}", result, sw.ElapsedMilliseconds);
        }
    }
        
}