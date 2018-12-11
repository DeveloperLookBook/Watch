﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Watch.Models.Users
{
    [Owned]
    public class Credentials : ICredentials
    {
        [Required]
        public string Login    { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
