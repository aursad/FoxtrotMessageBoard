using System;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

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
        }

        public static Rect GetElementRect(FrameworkElement element)
        {
            var buttonTransform = element.TransformToVisual(null);
            var point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private async void AttachmentImage_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            // Create a menu and add commands specifying a callback delegate for each.
            // Since command delegates are unique, no need to specify command Ids.
            var menu = new PopupMenu();
            menu.Commands.Add(new UICommand("Open with", (command) =>
            {
                OutputTextBlock.Text = "'" + command.Label + "' selected";
            }));
            menu.Commands.Add(new UICommand("Save attachment", (command) =>
            {
                OutputTextBlock.Text = "'" + command.Label + "' selected";
            }));

            // We don't want to obscure content, so pass in a rectangle representing the sender of the context menu event.
            // We registered command callbacks; no need to handle the menu completion event
            OutputTextBlock.Text = "Context menu shown";
            var chosenCommand = await menu.ShowForSelectionAsync(GetElementRect((FrameworkElement)sender));
            if (chosenCommand == null) // The command is null if no command was invoked.
            {
                OutputTextBlock.Text = "Context menu dismissed";
            }
        }
    }
}
