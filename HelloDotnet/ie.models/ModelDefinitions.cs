using System;
using System.Collections.Generic;   // need for IList<T>, List<T>, IDictionary<K, V> Dictionary<K, V>
using ie.delegates;
using ie.delegates.reactives;


namespace ie.models 
{
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

   public class IeGameModel {

      public IeGameModel() { this.score = 0; }

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

   public class IeGameMode2 {

      public IeGameModel2() { this.score = 0; }

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