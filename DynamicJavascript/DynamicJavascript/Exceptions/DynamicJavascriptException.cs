using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DynamicJavascript.Exceptions
{
    public enum ErrorCode
    {
        OK,
        MISSING_PARAMETER,
        VIEW_NOT_FOUND,
        SCRIPT_TAG_NOT_FOUND
    }

    public class DynamicJavascriptException : Exception
    {
        private ErrorCode ErrorCode;

        public DynamicJavascriptException(ErrorCode code)
            : base()
        {
            this.ErrorCode = code;
        }

        public DynamicJavascriptException(ErrorCode code, string message)
            : base(message)
        {
            this.ErrorCode = code;
        }
    }
}