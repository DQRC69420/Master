using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditPostAndCommentArchiver.Data
{
    public class Post
    {
        public Post(string jSonText, string postID,long? creationDateUnixLong, string author, string postTitle, string postBody, int? commentCount, IEnumerable<Comment> comments, Flair postFlair, string subredditName, string subredditNamePrefixed, (string permalink, string url) links)
        {
            JSonText = jSonText ?? throw new ArgumentNullException(nameof(jSonText));
            PostID = postID ?? throw new ArgumentNullException(nameof(postID));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            PostTitle = postTitle ?? throw new ArgumentNullException(nameof(postTitle));
            PostBody = postBody ?? throw new ArgumentNullException(nameof(postBody));
            CommentCount = commentCount ?? throw new ArgumentNullException(nameof(commentCount));
            Comments = comments ?? throw new ArgumentNullException(nameof(comments));
            PostFlair = postFlair;
            PostFlairText = postFlair.ToString();
            SubredditName = subredditName ?? throw new ArgumentNullException(nameof(subredditName));
            SubredditNamePrefixed = subredditNamePrefixed ?? throw new ArgumentNullException(nameof(subredditNamePrefixed));
            CreationDateUnixLong = creationDateUnixLong ?? throw new ArgumentNullException(nameof(creationDateUnixLong));
            CreationDate = DateTimeOffset.FromUnixTimeSeconds(CreationDateUnixLong.GetValueOrDefault()).DateTime;
            Links = links;
        }

        public string JSonText { get; } = string.Empty;

        public string Author { get; } = string.Empty;

        public string PostID { get; } = string.Empty;

        public string PostTitle { get; } = string.Empty;

        public string PostBody { get; } = string.Empty;

        public long? CreationDateUnixLong { get; } = 0;

        public DateTime? CreationDate { get; } = null;

        public IEnumerable<Comment> Comments { get; } = Enumerable.Empty<Comment>();

        public Flair PostFlair { get; } = Flair.None;

        public string PostFlairText { get; } = string.Empty;

        public string SubredditName { get; } = string.Empty;
        public string SubredditNamePrefixed { get; } = string.Empty;

        public (string Link, string URL) Links { get; } = (string.Empty, string.Empty);

        public int? CommentCount { get; } = 0;

        public bool IsEqualTo(Post post)
        {
            return string.Equals(PostID, post.PostID, StringComparison.Ordinal);
        }
    }
}


