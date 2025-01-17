﻿using Bazaro.Web.Models.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bazaro.Web.Models
{
    public class User : IdentityUser, IEntity
    {
        public string AvatarUrl { get; set; }

        [Required]
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
