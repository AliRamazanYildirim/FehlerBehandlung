using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc;

namespace FehlerBehandlung.Filter
{
    public class BenutzerdefiniertesHandleAusnahmeFilterAttribut : ExceptionFilterAttribute
    {
        public string ErrorPage { get; set; }
        public override void OnException(ExceptionContext context)
        {
            //var resultat = new ViewResult()
            //{
            //    ViewName = "Fehler1",
            //};
            var resultat = new ViewResult()
            {
                ViewName = ErrorPage,
            };

            resultat.ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState);
            resultat.ViewData.Add("Exception", context.Exception);
            resultat.ViewData.Add("Url", context.HttpContext.Request.Path.Value);
            context.Result = resultat;
        }
    }
}
