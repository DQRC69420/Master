namespace RedditPostAndCommentArchiver.Data
{
	public class CommentBase
	{
		public CommentBase(IEnumerable<CommentBase> replies, string commentBody)
		{
			Replies = replies ?? throw new ArgumentNullException(nameof(replies));
			CommentBody = commentBody ?? throw new ArgumentNullException(nameof(commentBody));
		}
		public string CommentBody { get; } = string.Empty;

		public IEnumerable<CommentBase> Replies { get; }
	}
}