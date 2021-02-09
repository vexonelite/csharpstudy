using System;
using System.Threading;
using ie.delegates;
using ie.exceptions;
using ie.extension.methods;


namespace ie.models 
{
   public class IeApiResponse<T> {
      public readonly T result;

      public readonly IeRuntimeException error;

      public IeApiResponse(T result, IeRuntimeException error) { 
         this.result = result;
         this.error = error;         
      }
   }

   ///

   public abstract class AbsCancellationTask {
      protected readonly CancellationTokenSource tokenSource;

      protected virtual int timeout { 
            get { return 60000; }             
        }

        protected AbsCancellationTask() { this.tokenSource = new CancellationTokenSource(); }

        public void cancelTask() {
            if (!tokenSource.IsCancellationRequested) {
                tokenSource.Cancel();
                Console.WriteLine("{0} - tokenSource.Cancel", this.getLogTag());
            }
        }

        public void disposeTask() {
            tokenSource.Dispose();
            Console.WriteLine("{0} - tokenSource.Dispose", this.getLogTag());
        }
   }

   ///


   public class MDateDescriptionImpl: DateDescriptionDelegate {
      public readonly DateTime dateTime;

      public readonly string description;

      public MDateDescriptionImpl(DateTime dateTime, string description) { 
         this.dateTime = dateTime;
         this.description = description;         
      }

      public DateTime theDate => this.dateTime;

      public string theDescription => this.description;

      public override string ToString() {
         return "MDateDescriptionImpl {Description: " + theDescription + ", Date: " + theDate + "}";
      }
   }

   /**
    * [What Is Faster In C#: A Struct Or A Class?](https://medium.com/csharp-architects/whats-faster-in-c-a-struct-or-a-class-99e4761a7b76)
    */
   public class PointClass{
      public int X { get; set; }
      public int Y { get; set; }
      public PointClass(int x, int y) {
         this.X = x;
         this.Y = y;
      }
   }

   public class TestGameModel {

      public TestGameModel() { this.score = 0; }

	   public int score { 
		   get { return score; }
		   set {
			   if ( (value >= 0) && (value <= 100) ) {
				   score = value;
			   }
			   else {
               Console.WriteLine("value {0} is out of range < 0 or > 100!!", value);
               score = 0;
			   }
		   } 
	   }	
   }
}