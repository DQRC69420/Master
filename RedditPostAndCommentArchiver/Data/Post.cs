using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditPostAndCommentArchiver.Data
{
	public class Post : PostBase
	{
		public Post(string jSonText, string postID, long? creationDateUnixLong, string author, string postTitle, string postBody, int? commentCount, IEnumerable<Comment> comments, Flair postFlair, string subredditName, string subredditNamePrefixed, (string permalink, string url) links) : base(postBody, postTitle, comments.Select(c=>c.GetBase()), postFlair, links)
		{
			JSonText = jSonText ?? throw new ArgumentNullException(nameof(jSonText));
			PostID = postID ?? throw new ArgumentNullException(nameof(postID));
			Author = author ?? throw new ArgumentNullException(nameof(author));
			CommentCount = commentCount ?? throw new ArgumentNullException(nameof(commentCount));
			PostFlair = postFlair;
			SubredditName = subredditName ?? throw new ArgumentNullException(nameof(subredditName));
			SubredditNamePrefixed = subredditNamePrefixed ?? throw new ArgumentNullException(nameof(subredditNamePrefixed));
			CreationDateUnixLong = creationDateUnixLong ?? throw new ArgumentNullException(nameof(creationDateUnixLong));
			CreationDate = DateTimeOffset.FromUnixTimeSeconds(CreationDateUnixLong.GetValueOrDefault()).DateTime;
		}

		public string JSonText { get; } = string.Empty;

		public string Author { get; } = string.Empty;

		public string PostID { get; } = string.Empty;

		public long? CreationDateUnixLong { get; } = 0;

		public DateTime? CreationDate { get; } = null;

		public Flair PostFlair { get; } = Flair.None;

		public string SubredditName { get; } = string.Empty;
		public string SubredditNamePrefixed { get; } = string.Empty;


		public int? CommentCount { get; } = 0;

		public bool IsEqualTo(Post post)
		{
			return string.Equals(PostID, post.PostID, StringComparison.Ordinal);
		}
	}
}


