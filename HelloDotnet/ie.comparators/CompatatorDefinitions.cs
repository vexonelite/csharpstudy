using System.Collections.Generic;
using ie.delegates;

namespace ie.comparators {

    public class DateDelegateComparator<T> : IComparer<T> where T : DateDelegate {

        private readonly bool isAscending;

        public DateDelegateComparator(bool isAscending) { this.isAscending = isAscending; }

        // Returns:
        //     A signed integer that indicates the relative values of x and y, as shown in the
        //     following table. Value Meaning Less than zero x is less than y. Zero x equals
        //     y. Greater than zero x is greater than y.
        public int Compare(T itemX, T itemY) {
            if ( (null == itemX) || (null == itemY) ) {
                return 0;
            }

            int result = itemX.theDate.CompareTo(itemY.theDate);
            if (result > 0) { // dateX is after dateY
                //for order by ascending
                // return 1;
                // for order by descending
                //return -1;
                return isAscending ? 1 : -1;                        
            }
            else if (result < 0) { // dateX is before dateY
                //for order by ascending
                //return -1;
                // for order by descending
                //return 1;
                return isAscending ? -1 : 1;                        
            } else { return 0; } // dateX is equal to dateY
        }
    }

    public sealed class DescriptionDelegateComparator<T> : IComparer<T> where T : DescriptionDelegate {

        private readonly bool isAscending;

        public DescriptionDelegateComparator(bool isAscending) { this.isAscending = isAscending; }
        
        public int Compare(T itemX, T itemY) {
            if ( (null == itemX) || (null == itemY) ) {
                return 0;
            }
            
            int testResult = itemX.theDescription.CompareTo(itemY.theDescription);
            return isAscending ? testResult : (0 - testResult);
        }
    }
}
