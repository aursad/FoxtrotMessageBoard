using System;
using System.Threading;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using IntroToUniWinPlat_Lab1.Model;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Interfaces;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IntroToUniWinPlat_Lab1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Messages : Page
    {
        private const int TimerExecutionTime = 30;
        private TwitterCredentials _credentials;

        public Messages()
        {
            this.InitializeComponent();

            _credentials = new TwitterCredentials("CvVZEBkSFREN7Laf6nMzhEXf2", "GJJBwGKcNGwXY91lyCbqyAThVNI1UGdCZSsOwMCFaJyIGT1RnF",
     "252973845-MS9X8nCd3E6C79IX4Au6KLI9sV0amUd8z5pGEOB3", "QMfjlqM7Bfno4obOhI8hvSKgWFKUmYsHvdE6kdl2qfxqq");

            ContactsCVS.Source = Tweets.GetGrouped(_credentials);
        }

        private new async void RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            Tweet tweet = null;
            var clickBorder = e.OriginalSource as TextBlock;
            if (clickBorder != null)
            {
                tweet = (Tweet)clickBorder.DataContext;
            }
            // Create a menu and add commands specifying a callback delegate for each.
            // Since command delegates are unique, no need to specify command Ids.
            var menu = new PopupMenu();
            menu.Commands.Add(new UICommand("open tweet", (command) =>
            {
                OpenPage(tweet);
            }));
            menu.Commands.Add(new UICommand("copy to clipboard", (command) =>
            {
                CopyText();
            }));


            var chosenCommand = await menu.ShowForSelectionAsync(GetElementRect((FrameworkElement)sender));
            if (chosenCommand == null) // The command is null if no command was invoked.
            {

            }
        }

        public static Rect GetElementRect(FrameworkElement element)
        {
            var buttonTransform = element.TransformToVisual(null);
            var point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private void CopyText()
        {
            var item = 1;
        }

        private async void OpenPage(Tweet tweet)
        {
            var url = string.Format("https://twitter.com/{0}/status/{1}", tweet.CreatedBy.ScreenName, tweet.Id);
            await Windows.System.Launcher.LaunchUriAsync(new Uri(url));
        }

        private void RefreshButton(object sender, RoutedEventArgs e)
        {
            var tweets = Tweets.GetGrouped(_credentials);
            ContactsCVS.Source = tweets;
        }
    }
}
