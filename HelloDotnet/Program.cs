using System;
using ie.delegates;
using ie.delegates.reactives;
using ie.structures;
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

   class ClassFooA {
      public static void MethodA(string message) {
         Console.WriteLine("Called ClassA.MethodA() with parameter: " + message);
      }
   }

   class ClassFooB {
      public static void MethodB(string message) {
         Console.WriteLine("Called ClassB.MethodB() with parameter: " + message);
      }
   }
   
   class ExecuteRectangle {
      static void Main(string[] args) {
         Rectangle r = new Rectangle();
         r.Acceptdetails();
         r.Display();
         //Console.ReadLine(); 

         Books book1 = new Books("Hello world", "John", "Hard", 1);
         Books book2 = new Books("iPhone", "Apple", "Mac", 2);
         Console.WriteLine(book1.ToString());
         Console.WriteLine(book2.ToString());

         FooDelegate fooDelegate = new FooImpl(1, "Foo_2020_11_13", "I know how to do");
         Console.WriteLine("CellType: {0}, Identifier: {1}, Description: {2}", fooDelegate.theCellType, fooDelegate.theIdentifier, fooDelegate.theDescription);

         MyDelegate del1 = new MyDelegate(ClassFooA.MethodA);
         del1.Invoke("I can help1");
         // or 
         MyDelegate del2 = ClassFooB.MethodB; 
         del2("I can help2");

         // or set lambda expression 
         MyDelegate del3 = (string msg) => Console.WriteLine(msg);
         del3("I can help3");

         // Func<int, int> callback = (int input, int output) => {}
         // TestRepository repository = new TestRepository()

      }
   }

   
}
