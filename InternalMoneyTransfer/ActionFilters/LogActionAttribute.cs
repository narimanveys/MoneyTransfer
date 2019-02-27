using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace InternalMoneyTransfer.ActionFilters
{
    public class LogActionAttribute : ActionFilterAttribute
    {
        #region Fields 

        private readonly ILogger _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        #endregion

        #region Methods

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _logger.Info(filterContext.ActionDescriptor.DisplayName);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            _logger.Info(filterContext.ActionDescriptor.DisplayName + " Finished");
            if (filterContext.Exception != null)
            {
                _logger.Error(filterContext.Exception);
            }
        }

        #endregion
    }
}