using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ServerLib
{
    class PageNotFoundException : HttpException
    {
        

        public PageNotFoundException() : base ("Page Not Found Exception (404)  ")
        {
            HResult = 404;
        }

        public PageNotFoundException(string message) : base(message)
        {
            HResult = 404;
        }

        public PageNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            HResult = 404;
        }

        protected PageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            HResult = 404;
        }
    }

    class InternalServerException : HttpException
    {
        public const int statusCode = 500;

        public InternalServerException() : base("Internal Server Exception (500)")
        {
            HResult = 500;
        }

        public InternalServerException(string message) : base(message)
        {
            HResult = 500;
        }

        public InternalServerException(string message, Exception innerException) : base(message, innerException)
        {
            HResult = 500;
        }

        protected InternalServerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            HResult = 500;
        }
    }

    class ForbiddenException: HttpException
    {
        public const int statusCode = 403;

        public ForbiddenException() : base ("Forbidden Exception (403)")
        {
            HResult = 403;
        }

        public ForbiddenException(string message) : base(message)
        {
            HResult = 403;
        }

        public ForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
            HResult = 403;
        }

        protected ForbiddenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            HResult = 403;
        }
    }

    class BadRequestException: HttpException
    {
        public const int statusCode = 400;

        public BadRequestException() : base("Bad Request Exception (400)")
        {
            HResult = 400;
        }

        public BadRequestException(string message) : base(message)
        {
            HResult = 400;
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
            HResult = 400;
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            HResult = 400;
        }
    }
}
