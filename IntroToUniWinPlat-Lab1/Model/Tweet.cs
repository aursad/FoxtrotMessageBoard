using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using IntroToUniWinPlat_Lab1.Annotations;
using Tweetinvi.Core.Enum;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Interfaces.DTO;
using Tweetinvi.Core.Interfaces.Models;
using Tweetinvi.Core.Interfaces.Models.Entities;

namespace IntroToUniWinPlat_Lab1.Model
{
    public class Tweet : INotifyPropertyChanged
    {
        private string _idStr;
        private long _id;
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public ICoordinates Coordinates { get; set; }
        public string Source { get; set; }
        public bool Truncated { get; set; }
        public long? InReplyToStatusId { get; set; }
        public string InReplyToStatusIdStr { get; set; }
        public long? InReplyToUserId { get; set; }
        public string InReplyToUserIdStr { get; set; }
        public string InReplyToScreenName { get; set; }
        public IUser CreatedBy { get; set; }
        public ITweetIdentifier CurrentUserRetweetIdentifier { get; set; }
        public int[] ContributorsIds { get; set; }
        public IEnumerable<long> Contributors { get; set; }
        public int RetweetCount { get; set; }
        public ITweetEntities Entities { get; set; }
        public bool Favourited { get; set; }
        public int FavouriteCount { get; set; }
        public bool Retweeted { get; set; }
        public bool PossiblySensitive { get; set; }
        public Language Language { get; set; }
        public IPlace Place { get; set; }
        public Dictionary<string, object> Scopes { get; set; }
        public string FilterLevel { get; set; }
        public bool WithheldCopyright { get; set; }
        public IEnumerable<string> WithheldInCountries { get; set; }
        public string WithheldScope { get; set; }
        public ITweetDTO TweetDTO { get; set; }
        public int PublishedTweetLength { get; set; }
        public DateTime TweetLocalCreationDate { get; set; }
        public List<IHashtagEntity> Hashtags { get; set; }
        public List<IUrlEntity> Urls { get; set; }
        public List<IMediaEntity> Media { get; set; }
        public List<IUserMentionEntity> UserMentions { get; set; }
        public List<ITweet> Retweets { get; set; }
        public bool IsRetweet { get; set; }
        public ITweet RetweetedTweet { get; set; }
        public long? QuotedStatusId { get; set; }
        public string QuotedStatusIdStr { get; set; }
        public ITweet QuotedTweet { get; set; }
        public bool IsTweetPublished { get; set; }
        public bool IsTweetDestroyed { get; set; }

        public long Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(nameof(Id)); }
        }

        public string IdStr
        {
            get { return _idStr; }
            set { _idStr = value; OnPropertyChanged(nameof(IdStr)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}