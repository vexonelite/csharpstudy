using System;
using ie.delegates;
using ie.delegates.reactives;
using ie.developments;
using ie.errorcodes;
using ie.exceptions;
using ie.extension.methods;
using ie.structures;
using System.Net.Http;           // need for HttpClient
using System.Threading;          // need for CancellationTokenSource
using System.Threading.Tasks;    // need for Parallel.For() Method, Task

// namespace HelloDotnet
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             Console.WriteLine("Hello Dotnet!");
//         }
//     }
// }

namespace RectangleApplication {
   class Rectangle {
      
      // member variables
      double length;
      double width;
      
      public void Acceptdetails() {
         length = 4.5;    
         width = 3.5;
      }
      public double GetArea() {
         return length * width; 
      }
      public void Display() {
         Console.WriteLine("Length: {0}", length);
         Console.WriteLine("Width: {0}", width);
         Console.WriteLine("Area: {0}", GetArea());
      }
   }

   ///

   public interface FooDelegate : CellTypeDelegate, IdentifierDelegate, DescriptionDelegate {}

   class FooImpl: FooDelegate {
      private readonly int cellType;
      private readonly string identifier;
      private readonly string description;

      public FooImpl(int cellType, string identifier, string description) {
         this.cellType = cellType;
         this.identifier = identifier;
         this.description = description;
      }

      //public int theCellType { get => this.cellType; }
      public int theCellType { 
         get {
            return this.cellType;
         } 
      }

      public string theIdentifier { get => this.identifier; } 
      public string theDescription { get => this.description; } 
   }

   class TestStringToInt: IeBaseConverter<string, int> {
      protected override int doConversion(string input) {
         return input.Length;
      }
   }

   public delegate void MyDelegate(string msg); // declare a delegate within a namespace?
   public delegate int MyDelegate2(); //declaring a delegate

   class ClassFooA {
      public static void stringMethodA(string message) {
         Console.WriteLine("Called ClassA.MethodA() with parameter: " + message);
      }

      public static int intMethodA() {
         return 100;
      }
   }

   class ClassFooB {
      public static void stringMethodB(string message) {
         Console.WriteLine("Called ClassB.MethodB() with parameter: " + message);
      }

      public static int intMethodB() {
         return 200;
      }
   }
   
   class ClassDelegation {
      public static bool IsUpperCase(string str) {
         return str.Equals(str.ToUpper());
      }

      public static int Sum(int x, int y) {
         return x + y;
      }

      public static void ConsolePrint(int i) {
         Console.WriteLine("ConsolePrint: {0}", i);
      }
   }

   class TeskAsyncAwaitTask5Callback: IApiResult<IeApiResponse<int?>> {
      public void onResult(IeApiResponse<int?> response) {
         if (Thread.CurrentThread.Name == null) {
            Thread.CurrentThread.Name = "Thread CallerAA";
         }         
         if (null != response.result) {
            Console.WriteLine("TeskAsyncAwaitTask5Callback - result :{0} on {1}", response.result, Thread.CurrentThread.Name);
         }
         else if (null != response.error) {
            Console.WriteLine("TeskAsyncAwaitTask5Callback - Error Message :{0} on {1}", response.error.Message, Thread.CurrentThread.Name);
         }
      }
   }
   
   class ExecuteRectangle {

      // HttpClient is intended to be instantiated once per application, rather than per-use. See Remarks.
      static readonly HttpClient client = new HttpClient();

      static void Main(string[] args) {
         Rectangle r = new Rectangle();
         r.Acceptdetails();
         r.Display();
         //Console.ReadLine(); 
      
         // runTestStructure();
         // runTestInterface();
         // runTestExtensionMethod();
         // runTestPredicate();
         // runTestFuncDelegate();
         // runTestActionDelegate();
         // runTestActionDelegateAsCallback();
         
         //runTestSingleton();

         //runTaskDelay();         
         //
         
         while (true) {
            // Start computation.
            
            //TeskAsyncAwaitTask1.Example();         
            //new TeskAsyncAwaitTask2().run().GetAwaiter().GetResult();

            //TeskAsyncAwaitTask3.runTaskDelay();
            //TeskAsyncAwaitTask4.runTaskDelay2();

            //new TeskAsyncAwaitTask5().run().GetAwaiter().GetResult(); // block current thread
            //new TeskAsyncAwaitTask5(null).run();  // issue task and work concurrently
            //new TeskAsyncAwaitTask5(new TeskAsyncAwaitTask5Callback().onResult).run(); // issue task and work concurrently (by using callback)
            
            new IeHttpGetTask(client).run();
            //runHttpAsync();
            // Handle user input.
            string result = Console.ReadLine();
            Console.WriteLine("You typed: " + result);
         }
      }

      

      private static async Task runHttpAsync() {
         // Call asynchronous network methods in a try/catch block to handle exceptions.
         try {
            HttpResponseMessage response = await client.GetAsync("http://www.contoso.com/");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(uri);

            Console.WriteLine(responseBody);
         }
         catch(HttpRequestException e) {
            Console.WriteLine("\nException Caught!");	
            Console.WriteLine("Message :{0} ",e.Message);
         }
      }

      private static void runTestSingleton() {
         Console.WriteLine("[Singleton]==================================");
         FooManager.Instance.ValueOne = 10.5;  
         FooManager.Instance.ValueTwo = 5.5;  
         Console.WriteLine("Addition : " + FooManager.Instance.Addition());  
         Console.WriteLine("Subtraction : " + FooManager.Instance.Subtraction());  
         Console.WriteLine("Multiplication : " + FooManager.Instance.Multiplication());  
         Console.WriteLine("Division : " + FooManager.Instance.Division());  
         Console.WriteLine("\n----------------------\n");  
         FooManager.Instance.ValueTwo = 10.5;  
         Console.WriteLine("Addition : " + FooManager.Instance.Addition());  
         Console.WriteLine("Subtraction : " + FooManager.Instance.Subtraction());  
         Console.WriteLine("Multiplication : " + FooManager.Instance.Multiplication());  
         Console.WriteLine("Division : " + FooManager.Instance.Division());  
         Console.WriteLine("Singleton: {0}", FooManager.Instance.GetHashCode());
      }

      private static void runTestStructure() {
         Console.WriteLine("[Structure]==================================");
         Books book1 = new Books("Hello world", "John", "Hard", 1);
         Books book2 = new Books("iPhone", "Apple", "Mac", 2);
         Console.WriteLine(book1.ToString());
         Console.WriteLine(book2.ToString());
      }

      private static void runTestInterface() {
         Console.WriteLine("[Interface]==================================");
         FooDelegate fooDelegate = new FooImpl(1, "Foo_2020_11_13", "I know how to do");
         Console.WriteLine("CellType: {0}, Identifier: {1}, Description: {2}", fooDelegate.theCellType, fooDelegate.theIdentifier, fooDelegate.theDescription);

         TestStringToInt stringToIntConverter = new TestStringToInt();
         stringToIntConverter.convertIntoData("The penny dropped!");
      }

      private static void runTestExtensionMethod() {
         Console.WriteLine("[Extension Method]==================================");
         int i = 10;
         bool result4 = i.IsGreaterThan(100); 
         Console.WriteLine("Test extension method [int.IsGreaterThan]: {0}", result4);

         string sentence = "one beer, please!";
         int wordCount1 = sentence.WordCount();        
         Console.WriteLine("Test extension method [string.WordCount] - for [{0}]: {1}", sentence, wordCount1);
      }

      private static void runTestDelegate1() {         
         Console.WriteLine("[Delegate]==================================");

         MyDelegate del1 = new MyDelegate(ClassFooA.stringMethodA);
         del1.Invoke("I can help1");
         // or 
         MyDelegate del2 = ClassFooB.stringMethodB; 
         del2("I can help2");

         // or set lambda expression 
         MyDelegate del3 = (string msg) => Console.WriteLine(msg);
         del3("I can help3");

         Console.WriteLine("[Multicast Delegate]==================================");
         MyDelegate multicastDel1 = del1 + del2;
         multicastDel1 += del3;
         multicastDel1("[Multicast 1]: I can fly!!");

         MyDelegate2 del4 = ClassFooA.intMethodA;
         MyDelegate2 del5 = ClassFooB.intMethodB;
         MyDelegate2 multicastDel2 = del4 + del5;
         Console.WriteLine("[Multicast 2] - return value: {0}", multicastDel2.Invoke());
      }

      private static void runTestPredicate() {
         
         Console.WriteLine("[Predicate]==================================");
         Predicate<string> isUpper1 = ClassDelegation.IsUpperCase;
         bool result1 = isUpper1("hello world!!");
         Console.WriteLine("Test Predicate#1 - [IsUpperCase]: {0}", result1);

         // Predicate delegate with anonymous method
         Predicate<string> isUpper2 = delegate(string s) { return s.Equals(s.ToUpper());};
         bool result2 = isUpper2("hello world!!");
         Console.WriteLine("Test Predicate#2 - [anonymous method]: {0}", result2);

         // Predicate delegate with lambda expression
         Predicate<string> isUpper3 = s => s.Equals(s.ToUpper());
         bool result3 = isUpper3("HELLO WORLD!!");
         Console.WriteLine("Test Predicate#3 - [lambda expression]: {0}", result3);
      }
      
      private static void runTestFuncDelegate() {
         Console.WriteLine("[Func Delegate #1]==================================");
         Func<int, int, int> sumDel1 = ClassDelegation.Sum;
         Console.WriteLine("Test Func#1 - [Sum]: {0}", sumDel1(10, 10));

         Func<int, int, int>  sumDel2  = (x, y) => x + y;
         Console.WriteLine("Test Func#1 - [Sum - lambda expression]: {0}", sumDel2(5, 6));

         Func<int> getRandomNumber1 = delegate() {
            Random rnd = new Random();
            return rnd.Next(1, 100);
         };
         Console.WriteLine("Test Func#3 - [getRandomNumber - Anonymous Method]: {0}", getRandomNumber1());

         Func<int> getRandomNumber2 = () => new Random().Next(1, 100);
         Console.WriteLine("Test Func#4 - [getRandomNumber - lambda expression]: {0}", getRandomNumber2());

         Console.WriteLine("[Func Delegate #2]==================================");

         FuncDelegateTest<string, int> funcDelegateTest = new FuncDelegateTest<string, int>();
         Func<int> noParamFunc = () => { return 100; };
         int funcResult1 = funcDelegateTest.testFunc1(noParamFunc);
         Console.WriteLine("funcResult1: {0}", funcResult1);

         Func<string, int> stringFunc = (string item) => { return item.Length; };
         int funcResult2 = funcDelegateTest.testFunc2(stringFunc, "I enjoy running!");
         Console.WriteLine("funcResult2: {0}", funcResult2);
      }

      private static void runTestActionDelegate() {
         Console.WriteLine("[Action Delegate #1]==================================");      
         Action<int> printActionDel1 = ClassDelegation.ConsolePrint;
         Console.WriteLine("Test Action#1");
         printActionDel1(10);

         // or
         Action<int> printActionDel2 = new Action<int>(ClassDelegation.ConsolePrint);
         Console.WriteLine("Test Action#2");
         printActionDel2(10);

         Action<int> printActionDel3 = delegate(int i) { Console.WriteLine("ConsolePrint via [Anonymous Method]: {0}", i); };
         Console.WriteLine("Test Action#3");
         printActionDel3(10);

         Action<int> printActionDel4 = i => Console.WriteLine("ConsolePrint via [lambda expression]: {0}", i);
         Console.WriteLine("Test Action#4");
         printActionDel4(10);

         Console.WriteLine("[Action Delegate #2]==================================");      

         ActionDelegateTest<string> actionDelegateTest = new ActionDelegateTest<string>();
         Action noParamAction = () => { Console.WriteLine("noParamAction"); };
         actionDelegateTest.testAction1(noParamAction);
         Action<string> stringAction = (string input) => { Console.WriteLine("stringAction: {0}", input); };
         actionDelegateTest.testAction2(stringAction, "What the Fuck");
      }

      private static void runTestActionDelegateAsCallback() {
         Console.WriteLine("[Action As Callback]==================================");      
         Action<int> callback1 = (int result) => {
            Console.WriteLine("TestRepository#Callback [via Action]: {0}", result);
         };
         TestRepository repository1 = new TestRepository(callback1);
         repository1.run();
      }
   }
}
