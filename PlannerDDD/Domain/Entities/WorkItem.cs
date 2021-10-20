using Domain.Base;

namespace Domain.Entities
{
    /// <summary>
    /// This part of the class will contain attributes of the object
    /// </summary>
    public class WorkItem : BaseEntity
    {
        public int Id { get; set; }

        // Work item title 
        public string Title { get; private set; }

        // Work item content
        public string Content { get; private set; }

        // Date created of work item
        public string DateCreated { get; private set; }

        // Done status
        public bool DoneStatus { get; private set; }

        // Creator Id
        public int CreatorId { get; private set; }

        // Creator
        public UserProfile Creator { get; set; }

        // Constructor
        public WorkItem (string title, string content, string dateCreated, int creatorId)
        {
            Title = title;
            Content = content;
            DateCreated = dateCreated;
            CreatorId = creatorId;
        }

        // The function to update title and content of the work item
        public void UpdateTitleAndWorkItem(string newTitle, string newContent)
        {
            Title = newTitle;
            Content = newContent;
        }

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
