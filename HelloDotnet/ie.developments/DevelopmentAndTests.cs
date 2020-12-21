using System;
using System.Collections;           // need for ArrayList
using System.Collections.Generic;   // need for List<T>
using System.Diagnostics;           // need for Stopwatch
using System.Threading;             // need for CancellationTokenSource
using System.Threading.Tasks;       // need for Parallel.For() Method
using System.Linq;                  // need for usage of Enumerable.Sum() Method, e.g., List<int>.Sum()
using System.Net.Http;              // need for HttpClient
using System.Net.Http.Headers;      
using ie.delegates.reactives;
using ie.errorcodes;
using ie.exceptions;
using ie.models;
using ie.structures;


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

    /// show difference between override and new modifier associated with a function

    public abstract class BaseLogger {
        public virtual void foo(string msg) {
            Console.WriteLine("Base - foo: {0}", msg);  
        }

        public void bar() {
            Console.WriteLine("Base - bar");  
        }
    }

    public class LoggerA: BaseLogger {

        public override void foo(string msg) {
            Console.WriteLine("LoggerB - foo: {0}", msg);  	
        }
        
        public new void bar() {
            Console.WriteLine("LoggerA - bar");  
        }
    }

    public class LoggerB: BaseLogger {
        public override void foo(string msg) {
            Console.WriteLine("LoggerB - foo: {0}", msg);  	
        }
    }

    public class TestNewAndOverrideModifier: IRunnable {        
        
        private int cents = 0;
        private int dollars = 0;
        private int extraCents = 0;

        public void run() {        
            BaseLogger AA = new LoggerA();
            BaseLogger BB = new LoggerB();
            AA.foo("Log started");
            AA.foo("Log continuing");
            AA.bar(); // involve BaseLogger's bar method
            ((LoggerA)AA).bar(); // involve LoggerA's bar method
        }

        public void deposit(int dollars, int cents) {
            int totalCents = cents + this.cents;
            int extraDollars = totalCents / 100;
            this.cents = totalCents - 100 * extraCents;

            //C# ``integer`` operations don’t throw exceptions upon overflow by default
            this.dollars += dollars + extraDollars; 

            this.dollars += checked(dollars + extraDollars);
        }
    }

    public class TestOverflowException: IRunnable { 

        private readonly bool checkEnabled;

        public TestOverflowException(bool checkEnabled) { this.checkEnabled = checkEnabled; }
        public void run() {

            int value = 780000000;
            if (checkEnabled) {
                Console.WriteLine("check Enabled!!");
                checked {
                    try {
                        // Square the original value.
                        int square = value * value;
                        Console.WriteLine("{0} ^ 2 = {1}", value, square);
                    }
                    catch (OverflowException cause) {
                        double square = Math.Pow(value, 2);
                        Console.WriteLine("OverflowException: {0} > {1:E}, message: {2}", square, Int32.MaxValue, cause.Message);
                    } 
                }
                // The example displays the following output:
                //       Exception: 6.084E+17 > 2.147484E+009.
            } else {
                Console.WriteLine("check Disabled!!");
                int square = value * value;
                Console.WriteLine("{0} ^ 2 = {1}", value, square);
            }
            
        }
    }

    ///

    public class Employee {
        protected string Name { get; set; }
        protected string Identifier { get; private set; }
    }

    public class IeEmployee : Employee {
        public void foo() {
            string x = this.Name;
            this.Name = "qoo";

            string y = this.Identifier;
            //this.Identifier = "zoo"; // cannot do this because the private set property            
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

    public class TestRepository2: IRunnable {
        private readonly Action<int> callback;

        public TestRepository2(Action<int> callback) {
            this.callback = callback;
        }

        //void IRunnable.run() { // such statement will incur a building error
        public void run() {        
            
        }
    }

    ///

    public class TestGetterAndSetter1: IRunnable {
        public void run() {        
            //Stack overflow Exception
            // IeGameModel theGame = new IeGameModel();
            // Console.WriteLine("TestGetterAndSetter1 - score1: {0}", theGame.score);
            // theGame.score = -1;
            // Console.WriteLine("TestGetterAndSetter1 - score2: {0}", theGame.score);
            // theGame.score = 55;
            // Console.WriteLine("TestGetterAndSetter1 - score3: {0}", theGame.score);

            try {
                ArrayList array1 = new ArrayList();
                int var1 = 10;
                int var2; 
                array1.Add(var1);
                var2 = array1[0];
            }
            catch(Exception cause) {
                Console.WriteLine("Error on test: {0}", cause);
            }
        }
    }
    ///

    public class TeskAsyncAwaitTask1 {

        public static async void Example() {
            // This method runs asynchronously.
            int t = await Task.Run(() => Allocate());
            Console.WriteLine("Compute: " + t);
        }

        static int Allocate(){
            Console.WriteLine("Allocate");
            // Compute total count of digits in strings.
            int size = 0;
            for (int z = 0; z < 100; z++) {
                Console.WriteLine("Allocate z: {0}", z);
                for (int i = 0; i < 100; i++) {
                    Console.WriteLine("Allocate i: {0}", i);
                    string value = i.ToString();
                    size += value.Length;
                }
            }
            Console.WriteLine("Allocate end");
            return size;
        }
    }

    public class TeskAsyncAwaitTask2: IeAsyncCallable {
        public async Task run() {
            // This method runs asynchronously.
            int t = await Task.Run(() => Allocate()).ConfigureAwait(false);
            Console.WriteLine("Compute: " + t);
        }

        private int Allocate(){
            Console.WriteLine("Allocate");
            // Compute total count of digits in strings.
            int size = 0;
            for (int z = 0; z < 100; z++) {
                Console.WriteLine("Allocate z: {0}", z);
                //for (int i = 0; i < 100000; i++) {
                for (int i = 0; i < 100; i++) {
                Console.WriteLine("Allocate i: {0}", i);
                    string value = i.ToString();
                    size += value.Length;
                }
            }
            Console.WriteLine("Allocate end");
            return size;
        }
    }
    
    ///

    public class TeskAsyncAwaitTask3 {
        public static void runTaskDelay() {

            CancellationTokenSource source = new CancellationTokenSource();

            // var t = Task.Run(async delegate
            //      {
            //         await Task.Delay(1000, source.Token);
            //         return 42;
            //      });
            Task<int> task = Task.Run(async () => 
            {
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread In1";
                }
                Console.WriteLine("# 11 CurrentThread.name: {0}", Thread.CurrentThread.Name);
                await Task.Delay(2000, source.Token).ConfigureAwait(false);
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread In2";
                }
                Console.WriteLine("# 12 CurrentThread.name: {0}", Thread.CurrentThread.Name);
                Random random = new System.Random();
                int value = random.Next(0, 100); //returns integer of 0-100
                if (value % 2 == 0) {
                    return 42;
                }
                else {
                    throw new IeRuntimeException("Error Task Run AAA", Base.INTERNAL_CONVERSION_ERROR);
                }                  
            }, source.Token);

            try {
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread Caller1";
                }
                Console.WriteLine("runTaskDelay on {0}", Thread.CurrentThread.Name);
                int result = task.GetAwaiter().GetResult();            
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread Caller2";
                }
                Console.WriteLine("runTaskDelay - result :{0} on {1}", result, Thread.CurrentThread.Name);
            }
            catch (Exception cause) {
                Console.WriteLine("runTaskDelay - Error Message :{0} on {1}", cause.Message, Thread.CurrentThread.Name);
            }
            finally {
                source.Dispose();
            }
        }
    }

    public class TeskAsyncAwaitTask4 {
        public static async void runTaskDelay2() {

            CancellationTokenSource source = new CancellationTokenSource();

            try {
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread Caller1";
                }
                Console.WriteLine("runTaskDelay 2 on {0}", Thread.CurrentThread.Name);
                int result = await getIntWithDelayAsync(source).ConfigureAwait(false);
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread Caller2";
                }
                Console.WriteLine("runTaskDelay 2 - result :{0} on {1}", result, Thread.CurrentThread.Name);
            }
            catch (Exception cause) {
                Console.WriteLine("runTaskDelay 2 - Error Message :{0} on {1}", cause.Message, Thread.CurrentThread.Name);
            }
            finally {
                source.Dispose();
            }
        }

        private static Task<int> getIntWithDelayAsync(CancellationTokenSource source) {
            return Task.Run(async () => 
                {
                    if (Thread.CurrentThread.Name == null) {
                        Thread.CurrentThread.Name = "Thread In1";
                    }
                    Console.WriteLine("getIntWithDelayAsync # 11 CurrentThread.name: {0}", Thread.CurrentThread.Name);
                    await Task.Delay(2000, source.Token).ConfigureAwait(false);
                    if (Thread.CurrentThread.Name == null) {
                        Thread.CurrentThread.Name = "Thread In2";
                    }
                    Console.WriteLine("getIntWithDelayAsync # 12 CurrentThread.name: {0}", Thread.CurrentThread.Name);
                    Random random = new System.Random();
                    int value = random.Next(0, 100); //returns integer of 0-100
                    if (value % 2 == 0) {
                        return 42;
                    }
                    else {
                        throw new IeRuntimeException("getIntWithDelayAsync - Error Task Run AAA", Base.INTERNAL_CONVERSION_ERROR);                
                    }                  
                }, source.Token);    
        }
    }

    ///

    public abstract class AbsHttpAsyncAwaitTask : AbsAsyncAwaitTask {

        protected readonly HttpClient httpClient;

        public AbsHttpAsyncAwaitTask(HttpClient httpClient) {
            this.httpClient = httpClient;
        }
    }

        // public override async Task run() {
        //     try {
        //         // HttpResponseMessage response = await client.GetAsync("http://www.contoso.com/");
        //         // response.EnsureSuccessStatusCode();
        //         // string responseBody = await response.Content.ReadAsStringAsync();
            
        //         // Above three lines can be replaced with new helper method below
        //         string responseBody = await httpClient.GetStringAsync("https://api.github.com/users/vexonelite");
        //         Console.WriteLine(responseBody);
        //     }
        //     catch(HttpRequestException e) {
        //         Console.WriteLine("\nException Caught!");	
        //         Console.WriteLine("Message :{0} ",e.Message);
        //     }
        // }
    ///

    public class IeHttpGetTask : AbsHttpAsyncAwaitTask {

        public IeHttpGetTask(HttpClient httpClient): base(httpClient) { }

        public sealed override async Task run() {
            try {                
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/users/vexonelite");
                // Add our custom headers to avoid 403 forbidden!!
                // https://stackoverflow.com/questions/20581117/parsing-from-website-which-return-403-forbidden
                // https://docs.github.com/en/free-pro-team@latest/rest/overview/resources-in-the-rest-api#user-agent-required
                requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:71.0) Gecko/20100101 Firefox/71.0");
                HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
                Console.WriteLine("IeHttpGetTask - StatusCode: {0}", response.StatusCode);
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("IeHttpGetTask - responseBody: {0}", responseBody);                
            }
            catch(HttpRequestException cause) {            
                Console.WriteLine("IeHttpGetTask - Error on httpClient.GetStringAsync: {0}", cause.Message);
            }
        }
    }

    ///

    

    ///



    public class TeskAsyncAwaitTask5: IeAsyncCallable {

        private readonly CancellationTokenSource tokenSource;

        private readonly Action<IeApiResponse<int?>> callback;

        public TeskAsyncAwaitTask5(Action<IeApiResponse<int?>> callback) {
            this.tokenSource = new CancellationTokenSource();
            this.callback = callback;
        }

        public void cancelTask() {
            tokenSource.Cancel();
        }

        public void disposeTask() {
            tokenSource.Dispose();
        }
        
        public async Task run() {            
            try {
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread Caller1";
                }
                Console.WriteLine("TeskAsyncAwaitTask5 on {0}", Thread.CurrentThread.Name);
                int result = await getIntWithDelayAsync();
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread Caller2";
                }
                Console.WriteLine("TeskAsyncAwaitTask5 - result :{0} on {1}", result, Thread.CurrentThread.Name);
                IeApiResponse<int?> response = new IeApiResponse<int?>(result, null);
                if (null != callback) {
                    callback.Invoke(response);
                }
            }
            catch (Exception cause) {
                Console.WriteLine("TeskAsyncAwaitTask5 - Error Message :{0} on {1}", cause.Message, Thread.CurrentThread.Name);
                IeRuntimeException error;
                if (cause is IeRuntimeException) {
                    error = ((IeRuntimeException) cause);
                }
                else {                    
                    error = new IeRuntimeException("TeskAsyncAwaitTask5 - Error on run(): [" + cause.Message + "]", cause, "00000");
                }
                IeApiResponse<int?> response = new IeApiResponse<int?>(null, error);
                if (null != callback) {
                    callback.Invoke(response);
                }
            }
            finally {
                tokenSource.Dispose();
            }
        }

        private Task<int> getIntWithDelayAsync() {
            Random random = new System.Random();

            Task<int> task = Task.Run(async () => 
            {
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread In1";
                }
                Console.WriteLine("TeskAsyncAwaitTask5 - getIntWithDelayAsync # 11 CurrentThread.name: {0}", Thread.CurrentThread.Name);
                await Task.Delay(2000, tokenSource.Token).ConfigureAwait(false);
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread In2";
                }
                Console.WriteLine("TeskAsyncAwaitTask5 - getIntWithDelayAsync # 12 CurrentThread.name: {0}", Thread.CurrentThread.Name);

                int value = random.Next(0, 100); //returns integer of 0-100
                if (value % 2 == 1) {
                    if (!tokenSource.IsCancellationRequested) {
                        tokenSource.Cancel();
                        Console.WriteLine("TeskAsyncAwaitTask5 - getIntWithDelayAsync # issue Cancellation request: {0}", Thread.CurrentThread.Name);
                    }
                    else {
                        Console.WriteLine("TeskAsyncAwaitTask5 - getIntWithDelayAsync # Cancellation has been Requested: {0}", Thread.CurrentThread.Name);
                    }            
                }

                // tokenSource.Cancel();
                // Console.WriteLine("TeskAsyncAwaitTask5 - getIntWithDelayAsync # issue Cancellation request: {0}", Thread.CurrentThread.Name);

                await Task.Delay(2000, tokenSource.Token).ConfigureAwait(false);
                if (Thread.CurrentThread.Name == null) {
                    Thread.CurrentThread.Name = "Thread In3";
                }
                Console.WriteLine("TeskAsyncAwaitTask5 - getIntWithDelayAsync # 12 CurrentThread.name: {0}", Thread.CurrentThread.Name);

                int value2 = random.Next(0, 100); //returns integer of 0-100
                if (value2 % 2 == 0) {
                    return 42;
                }
                else {
                    throw new IeRuntimeException("getIntWithDelayAsync - Error Task Run AAA", Base.INTERNAL_CONVERSION_ERROR);                
                }                  
            }, tokenSource.Token);

            task.ConfigureAwait(false);
            
            return task;
        } 
    }

    
}



