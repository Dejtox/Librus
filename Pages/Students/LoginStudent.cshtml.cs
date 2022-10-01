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

namespace Dziennik.Pages.Students
{
    public class LoginModel : PageModel
{
        private readonly IConfiguration _configuration;

        public InputModel Input { get; set; }
        public class InputModel
        {
            [Required]
            public string Login { get; set; }
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
        
        public bool CheckPassword(string connectionString, string userName, string password)
        {
            string passwordDB = null;
            bool loggedIn = false;
            using (SqlConnection con = new SqlConnection(connectionString))
            { 
                string selectSQL = "select Password from Student where Login='" + userName + "'";
                con.Open();
                SqlCommand cmd = new SqlCommand(selectSQL, con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        passwordDB = dr["Password"].ToString();
                    }
                }
            }
            
            PasswordHasher<string> pw = new PasswordHasher<string>();
            var verificationResult = pw.VerifyHashedPassword(userName, passwordDB, password);
            if (verificationResult == PasswordVerificationResult.Success)
                loggedIn = true;
            else
                loggedIn = false;
            return loggedIn;
            }

        public void OnPost()
        {
            string password;
            string userName;
            userName = Input.Login;
            password = Input.Password;

            Student person = new Student();

            var connectionString = _configuration.GetConnectionString("SchoolContext");
            bool validUser = CheckPassword(connectionString, userName, password);


            if (validUser == true)
            {
                HttpContext.Session.SetInt32("Login", 1);
                HttpContext.Session.SetString("UserName", userName);
                RedirectToPage("./Index");
            }
            else
            {
                HttpContext.Session.SetInt32("Login", 0);
                HttpContext.Session.SetString("UserName", "");
                RedirectToPage("./Login");
            }
            }
        }
    }
