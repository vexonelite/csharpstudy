using System;
using System.Collections.Generic;   // need for List<T>
using System.Diagnostics;           // need for Stopwatch
using System.Threading;             // need for CancellationTokenSource
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

    public class TestRepository2: IRunnable {
        private readonly Action<int> callback;

        public TestRepository2(Action<int> callback) {
            this.callback = callback;
        }

        //void IRunnable.run() { // such statement will incur a building error
        public  void run() {        
            
        }
    }

    ///

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

    ///

    public class ActionDelegateTest<T> {
        
        public void testAction1(Action action) {
            action.Invoke();
        }

        public void testAction2(Action<T> action, T item) {
            action.Invoke(item);
        }
    }

    public class FuncDelegateTest<T, R> {
        
        public R testFunc1(Func<R> function) {
            return function.Invoke();
        }

        public R testFunc2(Func<T, R> function, T item) {
            return function.Invoke(item);
        }
    }

    ///

    /** 
     * [Singleton Design Pattern In C#](https://www.c-sharpcorner.com/UploadFile/8911c4/singleton-design-pattern-in-C-Sharp/)
     */
    public sealed class SimpleSealedFoo {
        public readonly string name = "Suresh Dasari";
        public readonly string location = "Hyderabad";

        public void GetInfo() {
            Console.WriteLine("Name: {0}", name);
            Console.WriteLine("Location: {0}", location);
        }
    }

    /**
     * class ``ie.developments.SimpleSealedFoo``
     * 'DerivedSealedFoo': cannot derive from sealed type 'SimpleSealedFoo' [HelloDotnet]csharp(CS0509)
     */    
    // public class DerivedSealedFoo : SimpleSealedFoo {
    //     public int age = 32;
    //     public void GetAge(){ 
    //         Console.WriteLine("Age: {0}", age);
    //     }
    // }

    ///

    /**
     * A Singleton class
     */
    public sealed class FooManager {  

        /** private constructor */
        private FooManager() { }  

        private static readonly object padlock = new object();  
        /** 
         * A static variable that holds a reference to the single created instance,
         */
        private static FooManager instance = null;  
        /** 
         * A public static means of getting the reference to the single created instance
         */
        public static FooManager Instance  
        {  
            get  
            {  
                if (null == instance) {  
                    lock (padlock) {  
                        if (null == instance) {  
                            instance = new FooManager();  
                        }  
                    }  
                }  
                return instance;  
            }  
        }  

        public double ValueOne { get; set; }  
        public double ValueTwo { get; set; }  
        public double Addition() { return ValueOne + ValueTwo;  }  
        public double Subtraction() { return ValueOne - ValueTwo; }  
        public double Multiplication() { return ValueOne * ValueTwo; }  
        public double Division() { return ValueOne / ValueTwo; }
    }

    public class TestTaskCancellation {

        private readonly CancellationTokenSource tokenSource = new CancellationTokenSource();

        public void cancelTask() {
            tokenSource.Cancel();
        }

        public void disposeTask() {
            tokenSource.Dispose();
        }

        public async Task runTask() {
            var tokenSource2 = new CancellationTokenSource();
            CancellationToken ct = tokenSource2.Token;

            var task = Task.Run(() =>
                    {
                        // Were we already canceled?
                        ct.ThrowIfCancellationRequested();

                        bool moreToDo = true;
                        while (moreToDo)
                        {
                            // Poll on this property if you have to do
                            // other cleanup before throwing.
                            if (ct.IsCancellationRequested)
                            {
                                // Clean up here, then...
                                ct.ThrowIfCancellationRequested();
                            }
                        }
                    }, tokenSource2.Token); // Pass same token to Task.Run.

                    tokenSource2.Cancel();

                    // Just continue on this thread, or await with try-catch:
                    try
                    {
                        await task;
                    }
                    catch (OperationCanceledException e)
                    {
                        Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");
                    }
                    finally
                    {
                        tokenSource2.Dispose();
                    }

                    Console.ReadKey();
        }
    }
}

