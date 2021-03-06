using System;
using ie.delegates;
using ie.exceptions;


namespace ie.structures
{
   public struct IeApiResponse<T> {
      public readonly T result;

      public readonly IeRuntimeException error;

      public IeApiResponse(T result, IeRuntimeException error) { 
         this.result = result;
         this.error = error;         
      }
   }

   ///

   public struct SDateDescriptionImpl: DateDescriptionDelegate {
      public readonly DateTime dateTime;

      public readonly string description;

      public SDateDescriptionImpl(DateTime dateTime, string description) { 
         this.dateTime = dateTime;
         this.description = description;         
      }

      public DateTime theDate => this.dateTime;

      public string theDescription => this.description;

      public override string ToString() {
         return "SDateDescriptionImpl {Description: " + theDescription + ", Date: " + theDate + "}";
      }
   }

   ///

   public struct Books {
      private readonly string title;
      private readonly string author;
      private readonly string subject;
      private readonly int bookId;

      //public Books() { } //default constructor is not allowed!!

      public Books(string title, string author, string subject, int bookId) { 
         this.title = title;
         this.author = author;
         this.subject = subject;
         this.bookId = bookId;
      }

      public override string ToString() {
         return "Books {Title: " + title + ", Author: " + author + ", Subject: " + subject + ", Book Id: " + bookId + "}";
      }
      
      public void display() {
         Console.WriteLine("Title : {0}", title);
         Console.WriteLine("Author : {0}", author);
         Console.WriteLine("Subject : {0}", subject);
         Console.WriteLine("Book_id :{0}", bookId);
      }
   }
}

