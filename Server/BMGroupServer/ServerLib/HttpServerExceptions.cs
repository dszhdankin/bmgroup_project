using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Server
{
    class BadRequestException : HttpException
    {
        public BadRequestException()
            : this("Bad Request Exception (400)") { }

        public BadRequestException(string message, Exception innerException = null)
            : base(400, message, innerException) { }

        protected BadRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            HResult = 400;
        }
    }

    class ForbiddenException : HttpException
    {
        public ForbiddenException()
            : this("Forbidden Exception (403)") { }

        public ForbiddenException(string message, Exception innerException = null)
            : base(403, message, innerException) { }

        protected ForbiddenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            HResult = 403;
        }
    }

    public class PageNotFoundException : HttpException
    {
        public PageNotFoundException()
            : this("Page Not Found (404)") { }

        public PageNotFoundException(string message, Exception innerException = null)
            : base(404, message, innerException) { }

        protected PageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            HResult = 404;
        }
    }

    public class MethodNotAllowedException : HttpException
    {
        public MethodNotAllowedException()
            : this("Method Not Allowed (405)") { }
        
        public MethodNotAllowedException(string message, Exception innerException = null)
            : base(405, message, innerException) { }

        protected MethodNotAllowedException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            HResult = 405;
        }
    }

    class InternalServerException : HttpException
    {
        public const int statusCode = 500;

        public InternalServerException() 
            : this("Internal Server Exception (500)") { }

        public InternalServerException(string message, Exception innerException = null) 
            : base(500, message, innerException) { }

        protected InternalServerException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            HResult = 500;
        }
    }
}
