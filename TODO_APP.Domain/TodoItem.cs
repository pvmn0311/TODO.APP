namespace TODO_APP.Domain
{
    public class TodoItem
    {
        public int Id { get;  set; }
        public string Title { get;  set; }
        public string? Description { get;  set; }
        public bool IsCompleted { get;  set; }
        public DateTime CreatedDate { get;  set; }
        public int UserId { get; set; }
        protected TodoItem() { } // cho EF

        public TodoItem(string title, string? description)
        {
            Title = title;
            Description = description;
            IsCompleted = false;
            CreatedDate = DateTime.UtcNow;
        }

        public void MarkCompleted()
        {
            IsCompleted = true;
        }

        public void Update(string title, string? description)
        {
            Title = title;
            Description = description;
        }
    }
}
