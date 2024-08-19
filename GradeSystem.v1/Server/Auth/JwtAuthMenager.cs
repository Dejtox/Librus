
using GradeSystem.v1.Client.Services.TeacherService;
using GradeSystem.v1.Client.Services.UserService;
using GradeSystem.v1.Server.Data;
using GradeSystem.v1.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using static System.Net.WebRequestMethods;

namespace GradeSystem.v1.Server.Auth
{
    public class JwtAuthMenager
    {
        public const string JWT_SECURITY_KEY = "huyibyVUYB21313iB7h7H98m87h87HGIUnnuy8b78b8B7N7n78";
        private const int JWT_TOKEN_VALIDITY_MINS = 30;
        private const int JWT_TOKEN_VALIDITY_MINS_RE = 43200;//30dni

        private readonly GradeSystemv1ServerContext _context;
        public JwtAuthMenager(GradeSystemv1ServerContext context)
        {
            _context = context;
        }

        public async Task<UserSession?> GenerateJwtTokenAsync(string login, string password, bool rememberMe)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return null;

            var userAcc = _context.User.Include(r=>r.Roles).FirstOrDefault(x => x.Login == login);
            if (userAcc == null) return null;
            if (userAcc.PasswordHash != password)
                return null;
            var tokenexptime=DateTime.Now;
            if (rememberMe) tokenexptime = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS_RE);
            else tokenexptime = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity();
            string name = string.Empty;
            int ID;
            int userID;
            if (userAcc.Roles.Any(r=>r.Role=="Teacher"|| r.Role=="Admin"|| r.Role=="Principal"||r.Role=="Librarian"||r.Role== "Secretary"))
            {
                Teacher Teacher = await _context.Teacher.FirstOrDefaultAsync(t => t.UserID == userAcc.UserID);
                name = Teacher.FirstName + " " + Teacher.LastName;
                ID = Teacher.TeacherID;
                userID = Teacher.UserID;
                var claims = new List<Claim>();
                foreach (var role in userAcc.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role));
                }
                claims.Add(new Claim(ClaimTypes.Name, name));
                claims.Add(new Claim(ClaimTypes.Email, ID.ToString()));
                claims.Add(new Claim(ClaimTypes.Gender, userID.ToString()));
                claimsIdentity = new ClaimsIdentity(claims);
            }
            else if (userAcc.Roles.Any(r=>r.Role=="Student"))
            {
                Student Student = await _context.Student.FirstOrDefaultAsync(t => t.UserID == userAcc.UserID);
                name = Student.FirstName + " " + Student.LastName;
                ID = Student.StudentID;
                userID = Student.UserID;
                var claims = new List<Claim>();
                foreach (var role in userAcc.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role));
                }
                claims.Add(new Claim(ClaimTypes.Name, name));
                claims.Add(new Claim(ClaimTypes.Email, ID.ToString()));
                claims.Add(new Claim(ClaimTypes.Gender, userID.ToString()));
                claimsIdentity = new ClaimsIdentity(claims);
            }
            else
            {

                Parent Parent = await _context.Parent.FirstOrDefaultAsync(t => t.UserID == userAcc.UserID);
                name = Parent.FirstName + " " + Parent.LastName;
                ID = Parent.ParentID;
                userID = Parent.UserID;
                var claims = new List<Claim>();
                foreach (var role in userAcc.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Role));
                }
                claims.Add(new Claim(ClaimTypes.Name, name));
                claims.Add(new Claim(ClaimTypes.Email, ID.ToString()));
                claims.Add(new Claim(ClaimTypes.Gender, userID.ToString()));
                claimsIdentity = new ClaimsIdentity(claims);
            }

            var securityCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);
            var securityTokenSescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenexptime,
                SigningCredentials = securityCredentials,
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securitytoken = jwtSecurityTokenHandler.CreateToken(securityTokenSescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securitytoken);
            var userSession = new UserSession
            {
                UserID = ID,
                ID = userID,
                UserName = name,
                Roles= userAcc.Roles.Select(r=>r.Role).ToList(),
                Token = token,
                ExpiresIn = (int)tokenexptime.Subtract(DateTime.Now).TotalSeconds
            };

            return userSession;
        }
    }
}
