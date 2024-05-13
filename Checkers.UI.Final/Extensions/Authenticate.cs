﻿using Checkers.BL.Models;
using Microsoft.AspNetCore.Http;

namespace Checkers.UI.Final.Extensions
{
    public static class Authenticate
    {

        public static bool IsAuthenticated(HttpContext context)
        {
            if (context.Session.GetObject<User>("user") != null)
            {
                User user = context.Session.GetObject<User>("user");
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
