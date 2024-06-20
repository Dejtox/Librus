using GradeSystem.v1.Server.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GradeSystem.v1.Server.Filters
{
    public class LogRegisterFilter : Microsoft.AspNetCore.Mvc.Filters.IActionFilter
    {
        private readonly GradeSystemv1LogRegisterContext _context;
        public LogRegisterFilter(GradeSystemv1LogRegisterContext context) 
        { 
            _context = context; 
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Przed wykonaniem akcji - tutaj można umieścić kod przed akcją
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Po wykonaniu akcji - tutaj umieść kod po akcji

            // Sprawdź, czy to nie jest żądanie typu GET
            if (!context.HttpContext.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                // Tutaj możesz wykonać zapis do bazy danych
                //_context.Logi.Add(new LogModel
                //{
                //    // Dodaj informacje o logu, np. ścieżka, metoda HTTP, itp.
                //});

                _context.SaveChanges();
            }
        }
    }
}
