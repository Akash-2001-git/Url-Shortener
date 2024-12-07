using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Url_Shortener.Filters
{
    public class ValidUrlAttribute : ActionFilterAttribute
    {
        private readonly string _parameterName;

        public ValidUrlAttribute(string parameterName)
        {
            _parameterName = parameterName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.ContainsKey(_parameterName))
            {
                var url = context.ActionArguments[_parameterName] as string;

                if (string.IsNullOrEmpty(url) || !Uri.TryCreate(url, UriKind.Absolute, out _))
                {
                    context.Result = new BadRequestObjectResult($"The {_parameterName} parameter is not a valid URL.");
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
