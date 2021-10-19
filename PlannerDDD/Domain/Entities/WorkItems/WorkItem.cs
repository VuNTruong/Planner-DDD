using Domain.Base;
using Domain.Entities.UserProfiles;

namespace Domain.Entities.WorkItems
{
    /// <summary>
    /// This part of the class will contain attributes of the object
    /// </summary>
    public class WorkItem : BaseEntity
    {
        public int Id { get; set; }

        // Work item title 
        public string Title { get; set; }

        // Work item content
        public string Content { get; set; }

        // Date created of work item
        public string DateCreated { get; set; }

        // Done status
        public bool DoneStatus { get; set; }

        // Creator Id
        public int CreatorId { get; set; }

        // Creator
        public UserProfile Creator { get; set; }

        // The function to update title of the work item
        public void UpdateTitle(string newTitle)
        {
            Title = newTitle;
        }

        // The function to update content of the work item
        public void UpdateContent(string newContent)
        {
            Content = newContent;
        }
    }
}
