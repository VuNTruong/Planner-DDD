﻿using System.Collections.Generic;
using Domain.Base;

namespace Domain.Entities
{
    public class RoleDetail : BaseEntity
    {
        // Role description id (this will be used to connect with Role table)
        public int Id { get; set; }

        // Role description
        public string RoleDescription { get; set; }

        // One RoleDetail will describe only one role
        public Role Role { get; set; }

        // Create many to many relationship with User
        public List<RoleDetailUserProfile> RoleDetailUserProfiles { get; set; }
    }
}
