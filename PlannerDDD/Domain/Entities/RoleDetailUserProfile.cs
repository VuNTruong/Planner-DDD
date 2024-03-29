﻿using Domain.Base;

namespace Domain.Entities
{
    public class RoleDetailUserProfile : BaseEntity
    {
        // Id of the connection
        public int Id { get; set; }

        // User id that gets the role
        public int UserProfileId { get; set; }

        // Role id that is assigned to the user
        public int RoleDetailId { get; set; }

        // This will link with one user
        public UserProfile UserProfile { get; set; }

        // This will link with one corresponding role
        public RoleDetail RoleDetail { get; set; }
    }
}
