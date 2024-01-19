using System.ComponentModel.DataAnnotations;
using static Thread.Constants.Constants;

namespace Thread.Constants
{
    public class Constants
    {
        public enum ThreadType
        {
            PARENT,
            CHILD,
            REPOST,
            QUOTE
        }

        public enum ThreadReplyAccessType
        {
            ANY,
            FOLLOWING,
            MENTIONED
        }

        public enum ThreadContentType
        {
            TEXT,
            POLL
        }
    }




    public class ThreadContent
    {
        [Key] public Guid ContentId { get; set; }
        public ThreadContentType ContentType { get; set; } = ThreadContentType.TEXT;
        public required string Text { get; set; }
        public List<String>? Images { get; set; }
        public List<ThreadContentOptions>? Options { get; set; }
        public ThreadContentRatings? Ratings { get; set; }
    }

    public class ThreadContentOptions
    {
        [Key] public Guid OptionId { get; set; }
        public required string Option { get; set; }
        public required string Value { get; set; }
    }
    public class ThreadContentRatings
    {
        [Key] public Guid RatingsId { get; set; }
        public required int Total { get; set; } = 0;
        public required List<int> ResponseCounts { get; set; } = new List<int>();
    }
}
