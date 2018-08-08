﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.DTOs
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}
