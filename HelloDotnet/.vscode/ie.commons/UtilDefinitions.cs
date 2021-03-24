using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ie.errorcodes;
using ie.exceptions;

namespace ie.commons
{
    public static class IeUtils {
        public static IeRuntimeException IeAppErrorHandler1(Exception cause) {
            if (cause is TaskCanceledException) {
                Console.WriteLine("{0} - Task has been cancelled!", "Utils-IeAppErrorHandler1");
                return new IeRuntimeException("Task has been cancelled!!", cause, Base.ASYNC_TASK_TIMEOUT);
            }
            else if (cause is IeRuntimeException) {
                IeRuntimeException error = cause as IeRuntimeException;
                Console.WriteLine("{0} - IeRuntimeException errorCode: {1}, message: {2}", "Utils-IeAppErrorHandler1", error.exceptionCode, error.Message);
                return error;
            }
            else {
                Console.WriteLine("{0} - Unknown error {1}", "Utils-IeAppErrorHandler1", cause.Message);
                return new IeRuntimeException("Unknown Error on runAsync()", cause, "99999");
            }
        }
    }
}
