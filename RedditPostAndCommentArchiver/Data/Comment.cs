namespace RedditPostAndCommentArchiver.Data
{
	public class Comment : CommentBase
	{
		public Comment(string author, string commentID, long? creationDateUnixLong, string authorFlair, IEnumerable<Comment> replies, string link, string commentBody, string subredditName, string subredditNamePrefixed) : base(replies.Select(r => r.GetBase()), commentBody)
		{
			Author = author ?? throw new ArgumentNullException(nameof(author));
			CreationDateUnixLong = creationDateUnixLong ?? throw new ArgumentNullException(nameof(creationDateUnixLong));
			CreationDate = DateTimeOffset.FromUnixTimeSeconds(CreationDateUnixLong.GetValueOrDefault()).DateTime;
			CommentID = commentID ?? throw new ArgumentNullException(nameof(commentID));
			AuthorFlair = authorFlair ?? throw new ArgumentNullException(nameof(authorFlair));
			Link = link ?? throw new ArgumentNullException(nameof(link));
			SubredditName = subredditName ?? throw new ArgumentNullException(nameof(subredditName));
			SubredditNamePrefixed = subredditNamePrefixed ?? throw new ArgumentNullException(nameof(subredditNamePrefixed));
		}

		public string Author { get; } = string.Empty;

		public string CommentID { get; } = string.Empty;

		public string AuthorFlair { get; } = string.Empty;

		public long? CreationDateUnixLong { get; } = null;

		public DateTime? CreationDate { get; } = null;

		public string Link { get; } = string.Empty;

		public string SubredditName { get; } = string.Empty;
		public string SubredditNamePrefixed { get; } = string.Empty;

		public bool IsEqualTo(Comment other)
		{
			//return string.Equals(Author, other.Author) && string.Equals(CommentBody, other.CommentBody);
			return string.Equals(CommentID, other.CommentID, StringComparison.Ordinal);
		}

		public CommentBase GetBase()
		{
			return new CommentBase(Replies, CommentBody);
		}
	}
}


