using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;

using NLog;

using RB.Staff.Common.Pub.Extensions;

namespace RB.Staff.Web
{
    internal class ErrorHandlerAttribute : HandleErrorAttribute
    {
        private readonly Logger _errorLogger = LogManager.GetLogger( "Errors" );

        public override void OnException(
            ExceptionContext filterContext )
        {
            var message = string.Empty;
            var dbEntityValidationException = filterContext.Exception as DbEntityValidationException;
            if( dbEntityValidationException != null ) {
                message =
                    dbEntityValidationException.EntityValidationErrors.SelectMany( e => e.ValidationErrors )
                        .Select( e => e.ErrorMessage )
                        .JoinAsStrings( Environment.NewLine );
            }
            _errorLogger.Error( filterContext.Exception, message );

            base.OnException( filterContext );
        }
    }
}