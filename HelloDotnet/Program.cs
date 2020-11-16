﻿using System;
using ie.delegates;
using ie.delegates.reactives;
using ie.structures;
using ie.extension.methods;
using ie.developments;

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

      public int theCellType { get => this.cellType; }
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

   class ExecuteRectangle {
      static void Main(string[] args) {
         Rectangle r = new Rectangle();
         r.Acceptdetails();
         r.Display();
         //Console.ReadLine(); 

         Console.WriteLine("[Structure]==================================");
         Books book1 = new Books("Hello world", "John", "Hard", 1);
         Books book2 = new Books("iPhone", "Apple", "Mac", 2);
         Console.WriteLine(book1.ToString());
         Console.WriteLine(book2.ToString());

         Console.WriteLine("[Interface]==================================");
         FooDelegate fooDelegate = new FooImpl(1, "Foo_2020_11_13", "I know how to do");
         Console.WriteLine("CellType: {0}, Identifier: {1}, Description: {2}", fooDelegate.theCellType, fooDelegate.theIdentifier, fooDelegate.theDescription);

         //==============================
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

         //==============================

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

         //==============================

         Console.WriteLine("[Func Delegate]==================================");
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

         //==============================

         Console.WriteLine("[Action Delegate]==================================");      
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

         //==============================

         Console.WriteLine("[Extension Method]==================================");
         int i = 10;
         bool result4 = i.IsGreaterThan(100); 
         Console.WriteLine("Test extension method [int.IsGreaterThan]: {0}", result4);

         string sentence = "one beer, please!";
         int wordCount1 = sentence.WordCount();        
         Console.WriteLine("Test extension method [string.WordCount] - for [{0}]: {1}", sentence, wordCount1);

         //==============================

         Action<int> callback1 = (int result) => {
            Console.WriteLine("TestRepository#Callback [via Action]: {0}", result);
         };
         TestRepository repository1 = new TestRepository(callback1);
         repository1.run();

         //==============================

         TestStringToInt stringToIntConverter = new TestStringToInt();
         stringToIntConverter.convertIntoData("The penny dropped!");
      }
   }

   
}
