using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Core.Interfaces;

namespace IntroToUniWinPlat_Lab1.Model
{
    class Tweets
    {
        private static IEnumerable<ITweet> _tweets { get; set; }
        private static ObservableCollection<Tweet> TweetsPrinted { get; set; }

        public Tweets()
        {
            _tweets = new List<ITweet>();
            
        }

        private static ObservableCollection<Tweet> GetTweets(TwitterCredentials credentials)
        {
            if (credentials == null) throw new ArgumentNullException("credentials");
            _tweets = Auth.ExecuteOperationWithCredentials(credentials, () => Search.SearchTweets("vigofoxtrot"));

            if (TweetsPrinted == null)
            {
                TweetsPrinted = new ObservableCollection<Tweet>();
                _tweets.ForEach(tweet => TweetsPrinted.Add(new Tweet
                {
                    Id = tweet.Id,
                    CreatedBy = tweet.CreatedBy,
                    CreatedAt = tweet.CreatedAt,
                    Text = tweet.Text, 
                    Urls = tweet.Urls
                }));
            }
            else
            {
                foreach (var tweet in _tweets)
                {
                    var exist = TweetsPrinted.FirstOrDefault(q => q.Id == tweet.Id);
                    if (exist == null)
                    {
                        TweetsPrinted.Add(new Tweet
                        {
                            Id = tweet.Id,
                            CreatedBy = tweet.CreatedBy,
                            CreatedAt = tweet.CreatedAt,
                            Text = tweet.Text,
                            Urls = tweet.Urls
                        });
                    }
                }
            }

            return TweetsPrinted;
        }

        public static ObservableCollection<GroupInfoList> GetGrouped(TwitterCredentials credentials)
        {
            var groups = new ObservableCollection<GroupInfoList>();
            var tweetsList = GetTweets(credentials);

            var query = from item in tweetsList
                        group item by item.CreatedAt.ToString("d") into g
                        orderby g.Key ascending 
                        select new { GroupName = g.Key, Items = g };

            foreach (var g in query)
            {
                var info = new GroupInfoList
                {
                    Key = g.GroupName
                };
                info.AddRange(g.Items);
                groups.Add(info);
            }

            return groups;
        }
    }
}
