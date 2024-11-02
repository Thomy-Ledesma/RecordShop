using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PasswordService
    {
        
        
            private readonly PasswordHasher _passwordHasher;

            public PasswordService()
            {
                _passwordHasher = new PasswordHasher();
            }

            public string HashPassword(string password)
            {
                return _passwordHasher.HashPassword(password);
            }

            public bool VerifyPassword(string hashedPassword, string providedPassword)
            {
                var verificationResult = _passwordHasher.VerifyHashedPassword(hashedPassword, providedPassword);
                return verificationResult == PasswordVerificationResult.Success;
            }
        
    }
}
