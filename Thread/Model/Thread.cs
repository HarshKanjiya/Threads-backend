using System.ComponentModel.DataAnnotations;
using Thread.Constants;
using static Thread.Constants.Constants;

namespace Thread.Model
{
    public class ThreadModel
    {
        [Key] public Guid ThreadId { get; set; }
        public DateTime CreateAt = DateTime.Now;
        public required string AuthorId { get; set; }

        public required ThreadType Type { get; set; } = ThreadType.PARENT;
        public string? ReferenceId { get; set; }
        public required ThreadReplyAccessType ReplyAccess { get; set; } = ThreadReplyAccessType.ANY;

        public required ThreadContent Content { get; set; }

    }
}
