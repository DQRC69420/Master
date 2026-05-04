namespace RedditPostAndCommentArchiver.Data
{
	public class PostBase
	{
		public PostBase(Post post) : this(post.PostBody, post.PostTitle, post.Comments, post.PostFlair, post.Links)
		{

		}

		public PostBase(string postBody, string postTitle, IEnumerable<CommentBase> comments, Flair postFlairText, (string link, string url) links)
		{
			PostTitle = postTitle ?? throw new ArgumentNullException(nameof(postTitle));
			PostBody = postBody ?? throw new ArgumentNullException(nameof(postBody));
			Comments = comments ?? throw new ArgumentNullException(nameof(comments));
			PostFlairText = postFlairText.ToString();
			Links = links;
		}


		public string PostBody { get; } = string.Empty;

		public string PostFlairText { get; } = string.Empty;

		public string PostTitle { get; } = string.Empty;

		public (string Link, string URL) Links { get; } = (string.Empty, string.Empty);
		public IEnumerable<CommentBase> Comments { get; } = Enumerable.Empty<CommentBase>();
	}
}