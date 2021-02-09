using System;
using System.Collections;           // need for Hashtable
using System.Collections.Generic;   // need for IList<T>, List<T>, IDictionary<K, V> Dictionary<K, V>
using System.ComponentModel;
using System.Linq;
using ie.comparators;
using ie.delegates;
using ie.delegates.reactives;
using ie.delegates.WiFiScans;
using ie.models;
using ie.structures;
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

    ///

    public class TestListSorting : IRunnable {
        public void run() {
            //DateDelegateComparator<SDateDescriptionImpl> sDateComparator = new DateDelegateComparator<SDateDescriptionImpl>(true);
            //DateDelegateComparator<MDateDescriptionImpl> mDateComparator = new DateDelegateComparator<MDateDescriptionImpl>(true);

                // return 1;
                // for order by descending

            DateDelegateComparator<DateDescriptionDelegate> dateAscendingComparator = new DateDelegateComparator<DateDescriptionDelegate>(true);
            DateDelegateComparator<DateDescriptionDelegate> dateDescendingComparator = new DateDelegateComparator<DateDescriptionDelegate>(false);

            DescriptionDelegateComparator<DateDescriptionDelegate> descriptionAscendingComparator = new DescriptionDelegateComparator<DateDescriptionDelegate>(true);
            DescriptionDelegateComparator<DateDescriptionDelegate> descriptionDescendingComparator = new DescriptionDelegateComparator<DateDescriptionDelegate>(false);

            DateTime currentTime = DateTime.Now;
                    
            //IList<DateDescriptionDelegate> dataSet = new List<DateDescriptionDelegate>() {
            List<DateDescriptionDelegate> dataSet = new List<DateDescriptionDelegate>() {            
                new SDateDescriptionImpl(currentTime.AddDays(-1), "Alice"),
                new MDateDescriptionImpl(currentTime.AddDays(-2), "Bob"),
                new SDateDescriptionImpl(currentTime.AddDays(-3), "Cammy"),
                new MDateDescriptionImpl(currentTime.AddDays(-4), "Daniel"),
            };


            dataSet.Sort(dateAscendingComparator);
            foreach(DateDescriptionDelegate item in dataSet) {
                Console.WriteLine("dateAscending: {0}", item);
            }
            Console.WriteLine("=========================================");

            dataSet.Sort(dateDescendingComparator);
            foreach(DateDescriptionDelegate item in dataSet) {
                Console.WriteLine("ateDescending: {0}", item);
            }
            Console.WriteLine("=========================================");

            dataSet.Sort(descriptionAscendingComparator);
            foreach(DateDescriptionDelegate item in dataSet) {
                Console.WriteLine("descriptionAscending: {0}", item);
            }
            Console.WriteLine("=========================================");

            dataSet.Sort(descriptionDescendingComparator);
            foreach(DateDescriptionDelegate item in dataSet) {
                Console.WriteLine("descriptionDescending: {0}", item);
            }
            Console.WriteLine("=========================================");
        }
    }
    
    ///

    public class IDictionaryWrapper<Tkey, Tvalue> {
        
        private readonly IDictionary<Tkey, Tvalue> theDictionary = new Dictionary<Tkey, Tvalue>();
            
        // To enable client code to validate input // when accessing your indexer.
        public int Count => theDictionary.Count;
    
        // Indexer declaration.        
        public Tvalue this[Tkey theKey] {
            get => theDictionary[theKey];
            set => theDictionary[theKey] = value;
        }
    }

    public class TestDictionaryWithIndexer1 : IRunnable {
        public void run() {
            Console.WriteLine("TestDictionaryWithIndexer1");
            IDictionaryWrapper<string, int> wrapper = new IDictionaryWrapper<string, int>();
            wrapper["One"] = 1; //adding a key/value using the Add() method
            wrapper["Two"] = 2;
            wrapper["Three"] = 3;
            Console.WriteLine("One: {0}, Two: {1}, Three: {2}", wrapper["One"],  wrapper["Two"], wrapper["Three"]);
        }
    }

    ///

    public class TestDictionaryManipulations : IRunnable {
        public void run() {

            IDictionary<int, string> numberNames = new Dictionary<int, string>();
            numberNames.Add(1,"One"); //adding a key/value using the Add() method
            numberNames.Add(2,"Two");
            numberNames.Add(3,"Three");

            try {
                //The following throws run-time exception: key already added.
                numberNames.Add(3, "Three"); 
            }
            catch(ArgumentException error) {
                Console.WriteLine("error on Add with an existing key: {0}", error.Message);
            }

            foreach(KeyValuePair<int, string> kvp in numberNames) {
                Console.WriteLine("Key: {0}, Value: {1} in numberNames", kvp.Key, kvp.Value);
            }
            Console.WriteLine("=========================================");
            
            //var cities = new Dictionary<string, string>(){
            IDictionary<string, string> cityDict = new Dictionary<string, string>() {
                {"UK", "London, Manchester, Birmingham"},
                {"USA", "Chicago, New York, Washington"},
                {"India", "Mumbai, New Delhi, Pune"}
            };

            // KeyValuePair: kvp
            foreach(var kvp in cityDict) {
                //Console.WriteLine("Key: {0}, Value: {1} in cityDict - class name: {2}", kvp.Key, kvp.Value, TypeDescriptor.GetClassName(kvp));
                Console.WriteLine("Key: {0}, Value: {1} in cityDict", kvp.Key, kvp.Value);
            }
            Console.WriteLine("=========================================");

            // access via indexer
            Console.WriteLine(cityDict["UK"]); //prints value of UK key
            Console.WriteLine(cityDict["USA"]);//prints value of USA key
            try {
                //The following throws run-time exception: key already added.
                Console.WriteLine(cityDict["France"]); // run-time exception: Key does not exist
            }
            catch(Exception error) {
                Console.WriteLine("error on access with a non-existing key: {0}", error.Message);
            }
            Console.WriteLine("*****************************************");

            //use ContainsKey() to check for an unknown key
            if(cityDict.ContainsKey("France")){  
                Console.WriteLine(cityDict["France"]);
            } else {
                Console.WriteLine("cityDict does not ContainsKey [France]");
            }
            Console.WriteLine("*****************************************");

            //use TryGetValue() to get a value of unknown key
            string result;
            if(cityDict.TryGetValue("France", out result)) {
                Console.WriteLine(result);
            }
            else {
                Console.WriteLine("[TryGetValue] cityDict does not Contains Key [France]");
            }
            Console.WriteLine("*****************************************");

            //use ElementAt() to retrieve key-value pair using index
            for(int i = 0; i < cityDict.Count; i++) {
                KeyValuePair<string, string> kvp = cityDict.ElementAt(i);
                Console.WriteLine("[ElementAt] Key: {0}, Value: {1}", kvp.Key, kvp.Value);
            }
            Console.WriteLine("*****************************************");

            cityDict["UK"] = "Liverpool, Bristol"; // update value of UK key
            cityDict["USA"] = "Los Angeles, Boston"; // update value of USA key
            try {
                // the teach blog put it: throws run-time exception: KeyNotFoundException
                // but this is a valid statement after test - 2020/12/07
                cityDict["France"] = "Paris";
                Console.WriteLine("update [Paris] with key [France]");
            }
            catch(Exception error) {
                Console.WriteLine("error on update with a non-existing key: {0}", error.Message);
            }
            if(cityDict.TryGetValue("France", out result)) {
                Console.WriteLine("Get France [1]: {0}", result);
            }
            Console.WriteLine("*****************************************");

            if(cityDict.ContainsKey("France")) {
                // we can update multiple time with same key - 2020/12/07
                cityDict["France"] = "ParisX";
                cityDict["France"] = "ParisXY";
                cityDict["France"] = "ParisXYZ";
                Console.WriteLine("update [ParisX] with key [France]");
            }
            else {
                Console.WriteLine("[Update] cityDict does not Contains Key [France]");
            }
            if(cityDict.TryGetValue("France", out result)) {
                Console.WriteLine("Get France [2]: {0}", result);
            }
            Console.WriteLine("*****************************************");
            
            // Shanghai
            cityDict["China"] = "Beijing";
            cityDict["Japan"] = "Tokyo";            
            cityDict.Remove("UK"); // removes UK 

            try {
                // the teach blog put it: throws run-time exception: KeyNotFoundException
                // but this is a valid statement after test - 2020/12/07
                bool removedResult = cityDict.Remove("Canada"); 
                Console.WriteLine("remove with a non-existing key [Canada]: {0}", removedResult);
            }
            catch(Exception error) {
                Console.WriteLine("error on remove with a non-existing key: {0}", error.Message);
            }
            Console.WriteLine("*****************************************");

            if(cityDict.ContainsKey("Canada")) { // check key before removing it
                cityDict.Remove("Canada");
            }
            else {
                Console.WriteLine("[Remove] cityDict does not Contains Key [Canada]");
            }
            Console.WriteLine("*****************************************");

            foreach(var kvp in cityDict) {
                Console.WriteLine("[after removal] Key: {0}, Value: {1} in cityDict", kvp.Key, kvp.Value);
            }

            cityDict.Clear(); //removes all elements
            Console.WriteLine("[after clear] cityDict.Count: {0}", cityDict.Count);

            Console.WriteLine("=========================================");

            // Console.WriteLine("*****************************************");
            // Console.WriteLine("primeNumber Contains(3)? : {0}", primeNumbers.Contains(3));
            // Console.WriteLine("primeNumber Contains(11)? : {0}", primeNumbers.Contains(11));
            // Console.WriteLine("primeNumber IndexOf(11)? : {0}", primeNumbers.IndexOf(11));        
            // Console.WriteLine("=========================================");

            IeDelegate delegate1 = new SAccessPointImpl("", 1, "L4JSGS0061", "L4JSGS0061", "20", "psk2", "1600SGSJ4L");
            IeDelegate delegate2 = new MAccessPointImpl("", 2, "L4JSGS0063", "L4JSGS0063", "40", "psk2", "3600SGSJ4L");
            IeDelegate delegate3 = new SAccessPointImpl("", 3, "L4JSGS0065", "L4JSGS0065", "60", "psk2", "5600SGSJ4L");
            IeDelegate delegate4 = new MAccessPointImpl("", 4, "L4JSGS0067", "L4JSGS0067", "80", "psk2", "7600SGSJ4L");

//         IDictionary<string, IeDelegate> gAccessPointDict = new Dictionary<string, IeDelegate>();
//         openWith.Add("txt", "notepad.exe");
//         openWith.Add("bmp", "paint.exe");
//         openWith.Add("dib", "paint.exe");
//         openWith.Add("rtf", "wordpad.exe");
//         }
        }
    }

    ///

    public class Scorecard {
	    private Dictionary<string, int> players = new Dictionary<string, int>();
	    public void Add(string name, int score) {
		    players.Add(name, score);
	    }

        public int this[string name] {
	        get { return players[name];}
        }
	
    }

    ///

    public class TestHashtable : IRunnable {
        public void run() {
            Hashtable openWith = new Hashtable();
            openWith.Add("EEE", "Eggplant");
            openWith.Add("III", "illusion");
            openWith.Add("AAA", "Alice");
            openWith.Add("RRR", "Roll out");

             // When you use foreach to enumerate hash table elements,
            // the elements are retrieved as KeyValuePair objects.
            Console.WriteLine();
            foreach(DictionaryEntry de in openWith) {
                Console.WriteLine("foreach DictionaryEntry - Key = {0}, Value = {1}", de.Key, de.Value);
            }

            // To get the values alone, use the Values property.
            ICollection valueColl = openWith.Values;
            // The elements of the ValueCollection are strongly typed
            // with the type that was specified for hash table values.
            Console.WriteLine();
            foreach(string s in valueColl) {
                Console.WriteLine("Foreach Value = {0}", s);
            }

            // To get the keys alone, use the Keys property.
            ICollection keyColl = openWith.Keys;
            // The elements of the KeyCollection are strongly typed
            // with the type that was specified for hash table keys.
            Console.WriteLine();
            foreach(string s in keyColl) {
                Console.WriteLine("Foreach Key = {0}", s);
            }
        }
    }

    public class TestSortedList : IRunnable {
        public void run() {
            // Create a new sorted list of strings, with string keys.
            SortedList<string, string> openWith = new SortedList<string, string>();
            openWith.Add("EEE", "Eggplant");
            openWith.Add("III", "illusion");
            openWith.Add("AAA", "Alice");
            openWith.Add("RRR", "Roll out");

            // When you use foreach to enumerate list elements,
            // the elements are retrieved as KeyValuePair objects.
            Console.WriteLine();
            foreach(KeyValuePair<string, string> kvp in openWith) {
                Console.WriteLine("foreach KeyValuePair - Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            }

            // To get the values alone, use the Values property.
            IList<string> ilistValues = openWith.Values;

            // The elements of the list are strongly typed with the
            // type that was specified for the SorteList values.
            Console.WriteLine();
            foreach(string s in ilistValues) {
                Console.WriteLine("foreach Value = {0}", s);
            }

            // To get the keys alone, use the Keys property.
            IList<string> ilistKeys = openWith.Keys;
            // The elements of the list are strongly typed with the
            // type that was specified for the SortedList keys.
            Console.WriteLine();
            foreach(string s in ilistKeys) {
                Console.WriteLine("foreach Key = {0}", s);
            }
        }
    }
}
