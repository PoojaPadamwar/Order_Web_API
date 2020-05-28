using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace Order_Web_API.Validators
{
    public class OrderValidator : ActionFilterAttribute
    {
        //Validation for Id parameter if it is null or invalid or alphanumeric
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
             foreach (var parameter in actionContext.ActionDescriptor.GetParameters())
            {
                object parameterValue;
                if (parameter.ParameterType.IsValueType && actionContext.ActionArguments.TryGetValue(parameter.ParameterName, out parameterValue))
                {                    
                    if (parameterValue == null)
                    {
                        AddNullModelError(parameter, actionContext.ModelState);
                    }
                    else if (int.Parse(parameterValue.ToString()) <= 0)
                    {
                        AddBadModelError(parameter, actionContext.ModelState);
                    }
                }
            }

            // Check if modelstate is valid
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }

        private void AddBadModelError(HttpParameterDescriptor parameter, ModelStateDictionary modelState)
        {
            modelState.AddModelError(parameter.ParameterName, string.Format("The {0} cannot be less than or equal to 0.", parameter.ParameterName));
        }

        private void AddNullModelError(HttpParameterDescriptor parameter, ModelStateDictionary modelState)
        {
            modelState.AddModelError(parameter.ParameterName, string.Format("The {0} cannot be null.", parameter.ParameterName));
        }
    }
}