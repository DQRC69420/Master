namespace RedditPostAndCommentArchiver.Data
{
    public class Comment
    {
        public Comment(string author, string commentID, long? creationDateUnixLong, string authorFlair, IEnumerable<Comment> replies, string link, string commentBody, string subredditName, string subredditNamePrefixed)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
            CreationDateUnixLong = creationDateUnixLong ?? throw new ArgumentNullException(nameof(creationDateUnixLong));
            CreationDate = DateTimeOffset.FromUnixTimeSeconds(CreationDateUnixLong.GetValueOrDefault()).DateTime;
            CommentID = commentID ?? throw new ArgumentNullException(nameof(commentID));
            AuthorFlair = authorFlair ?? throw new ArgumentNullException(nameof(authorFlair));
            Replies = replies ?? throw new ArgumentNullException(nameof(replies));
            Link = link ?? throw new ArgumentNullException(nameof(link));
            CommentBody = commentBody ?? throw new ArgumentNullException(nameof(commentBody));
            SubredditName = subredditName ?? throw new ArgumentNullException(nameof(subredditName));
            SubredditNamePrefixed = subredditNamePrefixed ?? throw new ArgumentNullException(nameof(subredditNamePrefixed));
        }

        public string Author { get; } = string.Empty;

        public string CommentID { get; } = string.Empty;

        public string AuthorFlair { get; } = string.Empty;

        public long? CreationDateUnixLong { get; } = null;

        public DateTime? CreationDate { get; } = null;

        public IEnumerable<Comment> Replies { get; }

        public string Link { get; } = string.Empty;

        public string CommentBody { get; } = string.Empty;

        public string SubredditName { get; } = string.Empty;
        public string SubredditNamePrefixed { get; } = string.Empty;

        public bool IsEqualTo(Comment other)
        {
            //return string.Equals(Author, other.Author) && string.Equals(CommentBody, other.CommentBody);
            return string.Equals(CommentID, other.CommentID, StringComparison.Ordinal);
        }
    }
}


