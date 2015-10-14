using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Core.Interfaces;

namespace IntroToUniWinPlat_Lab1
{
    class Tweets
    {
        private static IEnumerable<ITweet> _tweets { get; set; }
        private static IList<ITweet> TweetsPrinted { get; set; }
        private const int TimerExecutionTime = 30;

        public Tweets(IEnumerable<ITweet> tweets)
        {
            _tweets = tweets;
            TweetsPrinted = new List<ITweet>();
        }

        static void Main(string[] args)
        {
            var credentials = new TwitterCredentials("CvVZEBkSFREN7Laf6nMzhEXf2", "GJJBwGKcNGwXY91lyCbqyAThVNI1UGdCZSsOwMCFaJyIGT1RnF",
                 "252973845-MS9X8nCd3E6C79IX4Au6KLI9sV0amUd8z5pGEOB3", "QMfjlqM7Bfno4obOhI8hvSKgWFKUmYsHvdE6kdl2qfxqq");
            // Use the ExecuteOperationWithCredentials method to invoke an operation with a specific set of credentials
            // var tweet = Auth.ExecuteOperationWithCredentials(creds, () => Tweet.PublishTweet("Hello World from Windows 10 foxtrot app! #vigofoxtrot"));

            GetTweets(credentials);

            //start timer
            var timer = new Timer(e => GetTweets(credentials), null, TimeSpan.Zero, TimeSpan.FromSeconds(TimerExecutionTime));
        }

        private static void GetTweets(TwitterCredentials credentials)
        {
            if (credentials == null) throw new ArgumentNullException("credentials");
            _tweets = Auth.ExecuteOperationWithCredentials(credentials, () => Search.SearchTweets("vigofoxtrot"));

            if (TweetsPrinted == null)
            {
                TweetsPrinted = (IList<ITweet>)_tweets;
            }
            else
            {
                foreach (var tweet in _tweets)
                {
                    var exist = TweetsPrinted.FirstOrDefault(q => q.Id == tweet.Id);
                    if (exist == null)
                    {
                        TweetsPrinted.Add(tweet);
                    }
                }
            }

            TweetsPrinted.OrderByDescending(q => q.CreatedAt).ForEach(tweet =>
            {
             //   Console.WriteLine("{0}\n by {1} @ {2}\n\n", tweet.Text, tweet.CreatedBy, tweet.CreatedAt);
            });
        }
    }
}
