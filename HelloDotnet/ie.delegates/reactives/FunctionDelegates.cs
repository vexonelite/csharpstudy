using System;
using System.Threading.Tasks; // for Task usage
using ie.exceptions;
using ie.errorcodes;

namespace ie.delegates.reactives 
{
    public interface IRunnable {
        void run();
    }

    public interface ICallable<out R> {
        R call();
    }

    public interface IMapFunction<in T, out R> {
        R convertIntoData(T input);
    }

    public interface IFilterFunction<in T> {    
        Boolean predicate(T input);
    }

    public interface IFilterFunction2<in T, in R> {        
        Boolean predicate(T input1, R input2);
    }

    public abstract class IeBaseConverter<T, R>: IMapFunction<T, R> {
        
        //R IMapFunction<T,R>.convertIntoData(T input) { // such statement will incur a building error
        public R convertIntoData(T input) {        
            try {
                return doConversion(input);
            }
            catch (Exception cause) {
                Console.WriteLine("{0} - {1}", "IeBaseConverter", "Error on doConversion()");                
                if (cause is IeRuntimeException) {
                    throw ((IeRuntimeException) cause);
                }
                else {
                    throw new IeRuntimeException(
                        "IeBaseConverter - Error on doConversion()", cause, Base.INTERNAL_CONVERSION_ERROR);
                }
            }
        }
        
        protected abstract R doConversion(T input);
    }

    public interface IeAsyncCallable<T> {
        Task<T> call(); // note: no async here
    }

    // public abstract class IeBaseAsyncCallable<T>: IeAsyncCallable<T> {
        
    //     //R IMapFunction<T,R>.convertIntoData(T input) { // such statement will incur a building error
    //     public Task<T> call() {        
    //         try {
    //             return doConversion(input);
    //         }
    //         catch (Exception cause) {
    //             Console.WriteLine("{0} - {1}", "IeBaseConverter", "Error on doConversion()");                
    //             if (cause is IeRuntimeException) {
    //                 throw ((IeRuntimeException) cause);
    //             }
    //             else {
    //                 throw new IeRuntimeException(
    //                     "IeBaseConverter - Error on doConversion()", cause, Base.INTERNAL_CONVERSION_ERROR);
    //             }
    //         }
    //     }
        
    //     protected abstract R doConversion(T input);
    // }
}