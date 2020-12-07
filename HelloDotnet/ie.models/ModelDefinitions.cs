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
}