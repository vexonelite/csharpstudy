using System;
using ie.delegates.WiFiScans;

namespace ie.widgets
{
    public sealed class IeUtils {
        public static string generateRandomStringViaGuid() {
            /* [Guid.NewGuid Method](https://docs.microsoft.com/en-us/dotnet/api/system.guid.newguid?view=netcore-3.1) */
            Guid guid = Guid.NewGuid();
            //Console.WriteLine("New Guid: {0}", guid.ToString());
      
            /* [DateTime Struct](https://docs.microsoft.com/en-us/dotnet/api/system.datetime?view=netcore-3.1) */
            long timeStamp = DateTime.Now.ToUniversalTime().Ticks;
            //Console.WriteLine("Current time stamp: {0}", timeStamp.ToString());
         
            return guid.ToString() + "_" + timeStamp.ToString();
        }
    }
    
}