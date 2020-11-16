using System;

namespace ie.exceptions
{
    public class IeRuntimeException: Exception {
        // 自訂錯誤碼
        public readonly string exceptionCode;

        //fun getExceptionCode(): String = exceptionCode

        public IeRuntimeException(string message, string exceptionCode): base(message) {
            this.exceptionCode = exceptionCode;
        }

        // System.Exception no such constructor!!
        // public IeRuntimeException(Exception cause, string exceptionCode): base(cause) {
        //     this.exceptionCode = exceptionCode;
        // }

        public IeRuntimeException(string message, Exception cause, string exceptionCode): base(message, cause) {
            this.exceptionCode = exceptionCode;
        }
    }
}
