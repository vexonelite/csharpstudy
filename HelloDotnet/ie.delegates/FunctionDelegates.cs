using System;
using System.Threading.Tasks; // for Task usage
using ie.exceptions;
using ie.errorcodes;
using ie.extension.methods;
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
                Console.WriteLine("{0} - {1}", this.getLogTag(), "Error on doConversion()");                
                if (cause is IeRuntimeException) { 
                    throw ((IeRuntimeException) cause); 
                }
                else {
                    throw new IeRuntimeException(this.getLogTag() + " - Error on doConversion()", cause, Base.INTERNAL_CONVERSION_ERROR);
                }
            }
        }
        
        protected abstract R doConversion(T input);
    }

    ///

    public interface IeAsyncCallable {
        Task runAsync(); // note: no async here
    }

    public interface IeAsyncCallable2<T> {
        Task<T> runAsync(); // note: no async here
    }

    public abstract class AbsAsyncAwaitTask<T> : AbsCancellationTask, IeAsyncCallable {

        private readonly Action<T> successCallback;
        private readonly Action<IeRuntimeException> errorCallback;

        protected AbsAsyncAwaitTask(Action<T> successCallback, Action<IeRuntimeException> errorCallback) : base() { 
            this.successCallback = successCallback;
            this.errorCallback = errorCallback;
        }
       
        protected abstract Task<T> runTaskAsync();

        /**
         * [Cancel async tasks after a period of time (C#)](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/cancel-async-tasks-after-a-period-of-time)
         */
        public async Task runAsync() {
            try {
                Console.WriteLine("{0} - run", this.getLogTag());
                tokenSource.CancelAfter(timeout);
                T result = await runTaskAsync().ConfigureAwait(false);                
                if (null != successCallback) { successCallback.Invoke(result); }
            }
            catch (Exception cause) {
                IeRuntimeException error;
                if (cause is TaskCanceledException) {
                    Console.WriteLine("{0} - Task has been cancelled!", this.getLogTag());
                    error = new IeRuntimeException("Task has been cancelled!!", cause, Base.ASYNC_TASK_TIMEOUT);
                }
                else if (cause is IeRuntimeException) {
                    error = cause as IeRuntimeException;
                    Console.WriteLine("{0} - IeRuntimeException errorCode: {1}, message: {2}", this.getLogTag(), error.exceptionCode, error.Message);
                }
                else {
                    Console.WriteLine("{0} - Unknown error {1}", this.getLogTag(), cause.Message);
                    error = new IeRuntimeException("Unknown Error on runAsync()", cause, "99999");
                }
                if (null != errorCallback) { errorCallback.Invoke(error); }
            }
            finally { 
                disposeTask(); 
            }
        }
    }

    public abstract class AbsAsyncAwaitTask2<T> : AbsCancellationTask, IeAsyncCallable2<T> {
        protected AbsAsyncAwaitTask2() : base() { }

        public abstract Task<T> runTaskAsync();

        /**
         * [Cancel async tasks after a period of time (C#)](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/cancel-async-tasks-after-a-period-of-time)
         */
        public async Task<T> runAsync() {
            try {
                Console.WriteLine("{0} - run", this.getLogTag());
                tokenSource.CancelAfter(timeout);
                return await runTaskAsync().ConfigureAwait(false);
            }
            catch (Exception cause) {
                if (cause is IeRuntimeException) { throw cause; }
                else if (cause is TaskCanceledException) {
                    Console.WriteLine("{0} - Task has been cancelled!", this.getLogTag());
                    throw new IeRuntimeException("Task has been cancelled!!", cause, Base.ASYNC_TASK_TIMEOUT); 
                }
                else { 
                    throw new IeRuntimeException("Error on runAsync()", cause, "99999"); 
                }
            }
            finally { 
                disposeTask(); 
            }
        }
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