using System;
using System.Collections.Generic;   // need for IList<T>, List<T>, IDictionary<K, V> Dictionary<K, V>
using System.ComponentModel;
using System.Linq;
using ie.delegates.reactives;
using ie.delegates.WiFiScans;
using ie.models.WiFiScans;
using ie.structures.WiFiScans;


namespace ie.collections 
{
    public class TestListManipulations : IRunnable {
        public void run() {
            //List<int> primeNumbers = new List<int>();
            IList<int> primeNumbers = new List<int>();
            primeNumbers.Add(1); // adding elements using add() method
            primeNumbers.Add(3);
            primeNumbers.Add(5);
            primeNumbers.Add(7);

            for(int i = 0; i < primeNumbers.Count; i++) {
                Console.WriteLine("primeNumber: {0}", primeNumbers[i]);
            }
            Console.WriteLine("*****************************************");
            
            primeNumbers.Insert(1, 11);
            for(int i = 0; i < primeNumbers.Count; i++) {
                Console.WriteLine("primeNumber: {0}", primeNumbers[i]);
            }
            Console.WriteLine("*****************************************");
            
            primeNumbers.Remove(3); // removes the first 3 from a list
            primeNumbers.RemoveAt(2); //removes the 3rd element (index starts from 0)
            try {
                primeNumbers.RemoveAt(10); //throws ArgumentOutOfRangeException
            }
            catch(ArgumentOutOfRangeException error) {
                Console.WriteLine("error on RemoveAt: {0}", error.Message);
            }
            
            for(int i = 0; i < primeNumbers.Count; i++) {
                Console.WriteLine("primeNumber: {0}", primeNumbers[i]);
            }
            Console.WriteLine("*****************************************");
            Console.WriteLine("primeNumber Contains(3)? : {0}", primeNumbers.Contains(3));
            Console.WriteLine("primeNumber Contains(11)? : {0}", primeNumbers.Contains(11));

            Console.WriteLine("primeNumber IndexOf(11)? : {0}", primeNumbers.IndexOf(11));
            
            Console.WriteLine("=========================================");

            //var cities = new List<string>();
            var cities = new List<string>();
            cities.Add("New York");
            cities.Add("London");
            cities.Add("Mumbai");
            cities.Add("Chicago");
            cities.Add(null);// nulls are allowed for reference type list

            foreach(var theCity in cities) {
                Console.WriteLine("City: {0}", theCity);
            }
            Console.WriteLine("=========================================");

            var bigCities = new List<string>() {
                "New York",
                "London",
                "Mumbai",
                "Chicago"
            };

            foreach(var bigCity in bigCities) {
                Console.WriteLine("bigCity: {0}", bigCity);
            }
            Console.WriteLine("=========================================");

            var sAccessPoints = new List<SAccessPointImpl>() {
                new SAccessPointImpl("", 1, "L4JSGS0061", "L4JSGS0061", "20", "psk2", "1600SGSJ4L"),
                new SAccessPointImpl("", 2, "L4JSGS0063", "L4JSGS0063", "40", "psk2", "3600SGSJ4L"),
                new SAccessPointImpl("", 3, "L4JSGS0065", "L4JSGS0065", "60", "psk2", "5600SGSJ4L"),
                new SAccessPointImpl("", 4, "L4JSGS0067", "L4JSGS0067", "80", "psk2", "7600SGSJ4L"),
            };
            
            for(int i = 0; i < sAccessPoints.Count; i++) {
                Console.WriteLine("SAccessPoints: {0}", sAccessPoints[i]);
            }
            Console.WriteLine("=========================================");

            var mAccessPoints = new List<MAccessPointImpl>() {
                new MAccessPointImpl("", 1, "L4JSGS0061", "L4JSGS0061", "20", "psk2", "1600SGSJ4L"),
                new MAccessPointImpl("", 2, "L4JSGS0063", "L4JSGS0063", "40", "psk2", "3600SGSJ4L"),
                new MAccessPointImpl("", 3, "L4JSGS0065", "L4JSGS0065", "60", "psk2", "5600SGSJ4L"),
                new MAccessPointImpl("", 4, "L4JSGS0067", "L4JSGS0067", "80", "psk2", "7600SGSJ4L"),
            };

            foreach(var accessPoints in mAccessPoints) {
                Console.WriteLine("MAccessPoint: {0}", accessPoints);
            }
            Console.WriteLine("=========================================");

            var gAccessPoints = new List<IeDelegate>() {
                new SAccessPointImpl("", 1, "L4JSGS0061", "L4JSGS0061", "20", "psk2", "1600SGSJ4L"),
                new MAccessPointImpl("", 2, "L4JSGS0063", "L4JSGS0063", "40", "psk2", "3600SGSJ4L"),
                new SAccessPointImpl("", 3, "L4JSGS0065", "L4JSGS0065", "60", "psk2", "5600SGSJ4L"),
                new MAccessPointImpl("", 4, "L4JSGS0067", "L4JSGS0067", "80", "psk2", "7600SGSJ4L"),
            };

            // using foreach LINQ method
            gAccessPoints.ForEach(accessPoint => Console.WriteLine("IeDelegate: {0}", accessPoint));    
            Console.WriteLine("=========================================");
            
            //get all students whose name is Bill
            //var ieDelegateResult = from accessPoint in gAccessPoints
            IEnumerable<IeDelegate> ieDelegateResult = from accessPoint in gAccessPoints            
                where accessPoint.theSSID == "L4JSGS0065"
                select accessPoint;
            
            Console.WriteLine("ieDelegateResult: {0}", TypeDescriptor.GetClassName(ieDelegateResult));
            foreach(var accessPoints in ieDelegateResult) {
                Console.WriteLine("MAccessPoint: {0}", accessPoints);
            }        

            //compile error: IEnumerable<IeDelegate>' does not contain a definition for 'ForEach' and no accessible extension method 'ForEach' accepting a first argument of type 'IEnumerable<IeDelegate>' could be found (are you missing a using directive or an assembly reference?) 
            //ieDelegateResult.ForEach(accessPoint => Console.WriteLine("IeDelegate: {0}", accessPoint));
            Console.WriteLine("=========================================");

            // # Adding an Array in a List

            string[] cityArray = new string[3]{ "Mumbai", "London", "New York" };
            var popularCities = new List<string>();
            // adding an array in a List
            popularCities.AddRange(cities);
            popularCities.ForEach(popularCity => Console.WriteLine("popularCity: {0}", popularCity));    
            Console.WriteLine("=========================================");

            var favouriteCities = new List<string>();
            // adding a List 
            favouriteCities.AddRange(popularCities);
            favouriteCities.ForEach(favouriteCity => Console.WriteLine("favouriteCity: {0}", favouriteCity));    
            Console.WriteLine("=========================================");
        }
    }
}