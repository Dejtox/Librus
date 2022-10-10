#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Dziennik.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Data.SqlClient;
using System.Web.Mvc;
//using System.Configuration; 

namespace Dziennik.Shared
{
    public class PartialModel : PageModel
    {
        public int validUser { get; set;}
        public string userName { get; set;}

        
        public async Task<IActionResult> OnGetAsync()
        {
            VerifyLogin();
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            VerifyLogin();
            return Page();
        }
        void VerifyLogin()
        {
            if (HttpContext.Session.GetInt32("Login") != null)
                validUser = (int)HttpContext.Session.GetInt32("Login");
            else
                validUser = 0;
            if (HttpContext.Session.GetString("UserName") != null)
                userName = HttpContext.Session.GetString("UserName");
            else
                userName = null;

        }
    }
}