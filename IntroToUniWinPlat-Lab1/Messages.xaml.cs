using System;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using IntroToUniWinPlat_Lab1.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IntroToUniWinPlat_Lab1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Messages : Page
    {
        public Messages()
        {
            this.InitializeComponent();
            ContactsCVS.Source = Contact.GetContactsGrouped(25);
        }

        private new async void RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            // Create a menu and add commands specifying a callback delegate for each.
            // Since command delegates are unique, no need to specify command Ids.
            var menu = new PopupMenu();
            menu.Commands.Add(new UICommand("Open url", (command) =>
            {
                OpenPage();
            }));
            menu.Commands.Add(new UICommand("Copy text", (command) =>
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

        private void OpenPage()
        {
            var item = 1;
        }
    }
}
