using AdsBoard.Domain.Entities.Identity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AdsBoard.WebUI.IdentityInf
{
    public class CustomUserValidator : IIdentityValidator<AppUser>
    {
        public async Task<IdentityResult> ValidateAsync(AppUser item)
        {
            List<string> errors = new List<string>();

            if (String.IsNullOrEmpty(item.UserName.Trim()))
                errors.Add("Ви не вказали ім'я");

            string userNamePattern = @"^[a-zA-Z0-9А-Яа-яёЁЇїІіЄєҐґ]+$";

            if (!Regex.IsMatch(item.UserName, userNamePattern))
                errors.Add("Ім'я може містити тільки літери української та англійської мов та цифри");

            if (errors.Count > 0)
                return IdentityResult.Failed(errors.ToArray());

            return IdentityResult.Success;
        }
    }
}