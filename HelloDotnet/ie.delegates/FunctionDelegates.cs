using System;
using System.Threading.Tasks; // for Task usage
using ie.exceptions;
using ie.errorcodes;
using ie.models;

namespace ie.delegates.reactives 
{
    public interface IRunnable {
        void run();
    }

    public interface ICallable<out R> {
        R call();
    }

    public interface IApiResult<in R> {
        void onResult(R result);
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

    public abstract class AbsMapFunction<T, R> : IMapFunction<T, R> {
        public abstract R convertIntoData(T input);
    }

    // public abstract class IeTestBaseConverter<T, R>: IMapFunction<T, R> {
            // R IMapFunction<T,R>.convertIntoData(T input) { // such statement will incur a building error    

            // public sealed override R convertIntoData(T input) { } // error here!!

            // public R convertIntoData(T input) { } // valid case!!
    // }

    public abstract class IeBaseConverter<T, R>: AbsMapFunction<T, R> {
        public sealed override R convertIntoData(T input) {        
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

    ///

    public interface IeAsyncCallable {
        Task run(); // note: no async here
    }

    public interface IeAsyncCallable2<T> {
        Task<T> run(); // note: no async here
    }

    public abstract class AbsAsyncAwaitTask : AbsCancellationTask, IeAsyncCallable {
        protected AbsAsyncAwaitTask(): base() { }
        public abstract Task run();
    }

    public abstract class AbsAsyncAwaitTask2<T> : AbsCancellationTask, IeAsyncCallable2<T> {
        protected AbsAsyncAwaitTask2(): base() { }
        public abstract Task<T> run();
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