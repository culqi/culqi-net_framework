using Culqui.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Culqui.Exceptions
{
    public class CulquiException : Exception
    {
        public CulquiException()
        {
        }

        public CulquiException(string message)
            : base(message)
        {
        }

        public CulquiException(string message, Exception err)
            : base(message, err)
        {
        }

        public CulquiException(HttpStatusCode httpStatusCode, CulquiError culquiError, string message)
            : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
            this.CulquiError = culquiError;
        }

        public HttpStatusCode HttpStatusCode { get; set; }

        public CulquiError CulquiError { get; set; }

        public CulquiResponse CulquiResponse { get; set; }
    }
}
