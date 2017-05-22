using Tweetinvi.Models;

namespace Twitter.Extentions
{
    public static class TwitterCredential
    {
        public static string CONSUMER_KEY = "ov2ZsHhZvTU1vNUPoUCzPRjuh";
        public static string CONSUMER_SECRET = "WlUHyLGEXRjfKJgzGr7DKAn8ED6L29NR5WXT74LDLKN5xVv1WE";
        public static string ACCESS_TOKEN = "636209692-xaJSr5JPQbPJEASneNPGhzX8bV00JCKHaUoHLIs1";
        public static string ACCESS_TOKEN_SECRET = "sz3bM8zOZTjI7OAVL6itWfjSet5HLWla2G4Mu14vQzZgK";

        public static ITwitterCredentials GenerateCredentials()
        {
            return new TwitterCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);
        }
    }
}