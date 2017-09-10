﻿using System.Collections.Generic;

namespace OneTrueError.Web.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string State { get; set; }

        public string Url { get; set; }
    }

    public class ManageInfoViewModel
    {
        public string Email { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
        public string LocalLoginProvider { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}