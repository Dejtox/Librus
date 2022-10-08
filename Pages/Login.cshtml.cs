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
//using System.Configuration; 

namespace Dziennik.Pages.Students
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration Configuration;

        public LoginModel(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
         public class InputModel
        {
            [Required]
            public string Login { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IActionResult OnGetAsync()
        {
            return Page();
        }
        
        public bool FindPassword(string connectionString, string userName, string password, string selectSQL)
        {
            string passwordC = null;
            bool loggedIn = false;
            
            using (var connection = new SqlConnection(connectionString))  
            {  
                SqlCommand cmd = new SqlCommand(selectSQL, connection);
                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        passwordC = dr["Password"].ToString();
                    }
                }
            }
            PasswordHasher<string> pw = new PasswordHasher<string>();
            var verificationResult = PasswordVerificationResult.Failed;
            if (passwordC != null)
            {
                
                verificationResult = pw.VerifyHashedPassword(userName, passwordC, password);
            }

            if (verificationResult == PasswordVerificationResult.Success)
                loggedIn = true;
            else
                loggedIn = false;
            return loggedIn;
            }

        public async Task<IActionResult> OnPostAsync()
        {
            
            string connectionString = this.Configuration.GetConnectionString("SchoolContext");
            string comandS = ("SELECT Password From Student WHERE Login='" + Input.Login + "'");
            string comandP = ("SELECT Password From Parent WHERE Login='" + Input.Login + "'");
            string comandT = ("SELECT Password From Teacher WHERE Login='" + Input.Login + "'");
            bool validUser = false;
            if (FindPassword(connectionString, Input.Login, Input.Password, comandS))
            {
                validUser=true;
            }
            else
            {
                if (FindPassword(connectionString, Input.Login, Input.Password, comandP))
                {
                    validUser = true;
                }
                else
                {
                    if (FindPassword(connectionString, Input.Login, Input.Password, comandT))
                    {
                        validUser = true;
                    }
                    else
                    {
                        validUser = false;
                    }
                }
            }

            
            if (validUser == true)
            {
                
                HttpContext.Session.SetInt32("Login", 1);
                HttpContext.Session.SetString("UserName", Input.Login);
                RedirectToPage("./Index");
            }
            else
            {
                HttpContext.Session.SetInt32("Login", 0);
                HttpContext.Session.SetString("UserName", "");
                RedirectToPage("./Login1");
            }
            return RedirectToPage("./Index");
        }
    }
}
