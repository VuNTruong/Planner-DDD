using System;
using Domain.Base;
using Domain.Entities.RoleDetails;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Roles
{
    public class Role : IdentityRole
    {
        // Role description id which will be used to link this with RoleDescription table
        public int RoleDetailId { get; set; }

        // One Role object will only have one RoleDescription object
        public RoleDetail RoleDetail { get; set; }
    }
}
