using System;
//using ie.delegates;

namespace ie.structures
{
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
   };  
}

