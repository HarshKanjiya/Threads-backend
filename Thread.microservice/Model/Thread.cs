﻿using System.ComponentModel.DataAnnotations;

namespace Thread.Model
{
    public class ThreadModel
    {
        [Key] public Guid ThreadId { get; set; }
        public DateTime CreatedAt = DateTime.Now;
        public required string AuthorId { get; set; }

        public required string Type { get; set; } = "PARENT";
        public string? ReferenceId { get; set; }
        public required string ReplyAccess { get; set; } = "ANY";

        public required ThreadContent Content { get; set; }

    }

    public class ThreadContent
    {
        [Key] public Guid ContentId { get; set; }
        public string ContentType { get; set; } = "TEXT";
        public required string Text { get; set; }
        public List<String>? Files { get; set; }
        public ICollection<ThreadContentOptions>? Options { get; set; } = new List<ThreadContentOptions>();
        public ThreadContentRatings? Ratings { get; set; }
    }

    public class ThreadContentOptions
    {
        [Key] public Guid OptionId { get; set; }
        public string Option { get; set; }
        public string Value { get; set; }
    }
    public class ThreadContentRatings
    {
        [Key] public Guid RatingsId { get; set; }
        public int TotalResponse { get; set; } = 0;
        public List<int> Responses { get; set; } = new List<int>();
    }
}