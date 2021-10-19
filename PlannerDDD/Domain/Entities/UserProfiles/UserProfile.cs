using System.Collections.Generic;
using Domain.Base;
using Domain.Entities.RoleDetailUserProfiles;
using Domain.Entities.WorkItems;
using Domain.Interfaces;

namespace Domain.Entities.UserProfiles
{
    public class UserProfile : BaseEntity, IAggregateRoot
    {
        // User profile id (this will be used to connect with User table)
        public int Id { get; set; }

        // User full name
        public string FullName { get; set; }

        // One UserProfile will have only one Identity
        public User User { get; set; }

        // One UserProfile will have many work items
        public virtual List<WorkItem> WorkItems { get; set; }

        // Create many to many relationship with Role
        public List<RoleDetailUserProfile> RoleDetailUserProfiles { get; set; }
    }
}
