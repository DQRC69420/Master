using RedditPostAndCommentArchiver.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Data.SqlTypes;
using RedditPostAndCommentArchiver.Providers;

namespace RedditPostAndCommentArchiver.RedditServices
{
    public class RedditServices
    {
        public static void ConvertToPost(string postAsJSon, string fullPath)
        {
            var postAsJObject = JsonConvert.DeserializeObject<dynamic>(postAsJSon) ?? null;
            if (postAsJObject != null)
            {
                var title = GetPostStringProperty(postAsJObject, "title");
                var author = GetPostStringProperty(postAsJObject, "author");
                var id = GetPostStringProperty(postAsJObject, "id");
                var body = GetPostStringProperty(postAsJObject, "selftext");
                var postFlair = GetPostStringProperty(postAsJObject, "link_flair_text");
                var subreddit = GetPostStringProperty(postAsJObject, "subreddit");
                var subredditWithPrefix = GetPostStringProperty(postAsJObject, "subreddit_name_prefixed");
                (string permaLink, string url) links = (GetPostStringProperty(postAsJObject, "permalink"), GetPostStringProperty(postAsJObject, "url"));
                var commentCount = GetPostIntProperty(postAsJObject, "num_comments");
                var creationDateUnixLong = GetPostLongProperty(postAsJObject, "created_utc");
                var comments = GetPostComments(postAsJObject[1]["data"]["children"] as JArray ?? []);
                Flair postFlairEnum = GetFlairFromString(postFlair as string ?? "");
                var post = new Post(postAsJSon, id, creationDateUnixLong, author, title, body, commentCount, comments, postFlairEnum, subreddit, subredditWithPrefix, links);

                JsonDataProvider jsonDataProvider = new(fullPath);
                jsonDataProvider.Serialize(new Dictionary<string, Post>()
                {
                    [post.PostID] = post,
                }, post.SubredditName);
            }


            //var title = postAsJObject.Value<string>("title");
            //var body = postAsJObject.Value<string>("selftext");
            //var postFlair = postAsJObject.Value<string>("link_flair_text");
            //var subreddit = postAsJObject.Value<string>("subreddit");
            //var subredditWithPrefix = postAsJObject.Value<string>("subreddit_name_prefixed");
            //(string permaLink, string url) = (postAsJObject.Value<string>("permalink") ?? "", postAsJObject.Value<string>("url") ?? "");
            //var commentCount = postAsJObject.Value<int>("num_comments");

            //var comments = postAsJObject.Value<string>("comment");
        }

        private static Flair GetFlairFromString(string postFlair)
        {
            postFlair = postFlair.Trim().ToLower().Replace(" ", "");
            switch (postFlair)
            {
                case @"turkishalternative":
                    return Flair.TurkishAlternative;
                case @"wordcollection":
                    return Flair.WordCollection;
                case @"etymology":
                    return Flair.Etymology;
                case @"discussion":
                    return Flair.Discussion;
                case @"arabic->turkish":
                    return Flair.ArabicToTurkish;
                case @"persian/iranic->turkish":
                    return Flair.PersianIranicToTurkish;
                case @"english->turkish":
                    return Flair.EnglishToTurkish;
                case @"french->turkish":
                    return Flair.FrenchToTurkish;
                case @"proposal:newword":
                    return Flair.ProposalNewWord;
                case @"proposal:homonym":
                    return Flair.ProposalHomonym;
                case @"proposal:synonym":
                    return Flair.ProposalSynonym;
                case @"memes":
                    return Flair.Memes;
                case @"information":
                    return Flair.Information;
                case @"universal->turkish":
                    return Flair.UniversalToTurkish;
                case @"greek->turkish":
                    return Flair.GreekToTurkish;
                case @"slavic->turkish":
                    return Flair.SlavicToTurkish;
                case @"italian->turkish":
                    return Flair.ItalianToTurkish;
                case @"chinese->turkish":
                    return Flair.ChineseToTurkish;
                case @"multiplelanguages":
                    return Flair.MultipleLanguagesToTurkish;
                case @"duplicate":
                    return Flair.Duplicate;
                case @"rejected":
                    return Flair.Rejected;
                case @"addition":
                    return Flair.Addition;
                case @"unknownorigin/vagueetymology":
                    return Flair.UnknownOriginVagueEtymology;
                case @"poetry":
                    return Flair.Poetry;
                case @"dialect":
                    return Flair.Dialect;
                case @"folksong/lyrics":
                    return Flair.FolkSongOrLyrics;
                case @"phonetics":
                    return Flair.Phonetics;
                case @"weeklypost":
                    return Flair.WeeklyPost;
                case @"crosspost":
                    return Flair.Crosspost;
                case @"none":
                case null:
                default:
                    return Flair.None;
            }
        }


        private static IEnumerable<Comment> GetPostComments(JArray children)
        {
            List<Comment> comments = [];
            foreach (JObject jObject in children.Cast<JObject>())
            {
                var author = GetStringValueFromJObject(jObject, "author");
                var id = GetStringValueFromJObject(jObject, "id");
                var authorFlair = GetStringValueFromJObject(jObject, "author_flair_text");
                var link = GetStringValueFromJObject(jObject, "permalink");
                var commentBody = GetStringValueFromJObject(jObject, "body");
                var subredditName = GetStringValueFromJObject(jObject, "subreddit");
                var subredditNamePrefixed = GetStringValueFromJObject(jObject, "subreddit_name_prefixed");
                var creationDateUnixLong = GetLongValueFromJObject(jObject, "created_utc");

                List<Comment> replies = [];
                if (jObject["data"]?["replies"]?.Count() != 0)
                {
                    replies.AddRange(GetPostComments(jObject["data"]?["replies"]?["data"]?["children"] as JArray ?? []));
                }

                comments.Add(new Comment(author ?? "",
                                         id ?? "",
                                         creationDateUnixLong.GetValueOrDefault(),
                                         authorFlair ?? "",
                                         replies,
                                         link ?? "",
                                         commentBody ?? "",
                                         subredditName ?? "",
                                         subredditNamePrefixed ?? ""));
            }
            return comments;
        }

        private static long? GetLongValueFromJObject(JObject jObject, string jsonPropertyName)
        {
            return jObject["data"]?.Value<long>(jsonPropertyName);
        }

        private static string? GetStringValueFromJObject(JObject jObject, string jsonPropertyName)
        {
            return jObject["data"]?.Value<string>(jsonPropertyName);
        }

        private static string? GetPostStringProperty(dynamic postAsJObject, string jsonPropertyName)
        {
            return (postAsJObject[0]["data"]["children"][0]["data"][jsonPropertyName] as JToken ?? null)?.Value<string>();
        }


        private static int? GetPostIntProperty(dynamic postAsJObject, string jsonPropertyName)
        {
            return (postAsJObject[0]["data"]["children"][0]["data"][jsonPropertyName] as JToken ?? null)?.Value<int>();
        }


        private static long? GetPostLongProperty(dynamic postAsJObject, string jsonPropertyName)
        {
            return (postAsJObject[0]["data"]["children"][0]["data"][jsonPropertyName] as JToken ?? null)?.Value<long>();
        }


        private static void SaveAsFile(Task<string> result, Post subreddit)
        {


            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SubredditContents", $"{subreddit.JSonText}_Contents");
            path = Path.ChangeExtension(path, ".json");
            File.WriteAllText(path, result.Result);
            result.Dispose();
        }
    }
}
