using System;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace IntroToUniWinPlat_Lab1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        // returns a rect for selected text
        // if no text is selected, returns caret location
        // textbox should not be empty
        private Rect GetTextboxSelectionRect(TextBox textbox)
        {
            Rect rectFirst, rectLast;
            if (textbox.SelectionStart == textbox.Text.Length)
            {
                rectFirst = textbox.GetRectFromCharacterIndex(textbox.SelectionStart - 1, true);
            }
            else
            {
                rectFirst = textbox.GetRectFromCharacterIndex(textbox.SelectionStart, false);
            }

            var lastIndex = textbox.SelectionStart + textbox.SelectionLength;
            if (lastIndex == textbox.Text.Length)
            {
                rectLast = textbox.GetRectFromCharacterIndex(lastIndex - 1, true);
            }
            else
            {
                rectLast = textbox.GetRectFromCharacterIndex(lastIndex, false);
            }

            var buttonTransform = textbox.TransformToVisual(null);
            var point = buttonTransform.TransformPoint(new Point());

            // Make sure that we return a valid rect if selection is on multiple lines
            // and end of the selection is to the left of the start of the selection.
            double x, y, dx, dy;
            y = point.Y + rectFirst.Top;
            dy = rectLast.Bottom - rectFirst.Top;
            if (rectLast.Right > rectFirst.Left)
            {
                x = point.X + rectFirst.Left;
                dx = rectLast.Right - rectFirst.Left;
            }
            else
            {
                x = point.X + rectLast.Right;
                dx = rectFirst.Left - rectLast.Right;
            }

            return new Rect(x, dx, y, dy);
        }

        private async void ReadOnlyTextBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true;
            var textbox = (TextBox)sender;
            if (textbox.SelectionLength > 0)
            {
                // Create a menu and add commands specifying an id value for each instead of a delegate.
                var menu = new PopupMenu();
                menu.Commands.Add(new UICommand("Copy", null, 1));
                menu.Commands.Add(new UICommandSeparator());
                menu.Commands.Add(new UICommand("Highlight", null, 2));
                menu.Commands.Add(new UICommand("Look up", null, 3));

                // We don't want to obscure content, so pass in a rectangle representing the selection area.
                // NOTE: this code only handles textboxes with a single line. If a textbox has multiple lines,
                //       then the context menu should be placed at cursor/pointer location by convention.
                OutputTextBlock.Text = "Context menu shown";
                var rect = GetTextboxSelectionRect(textbox);
                var chosenCommand = await menu.ShowForSelectionAsync(rect);
                if (chosenCommand != null)
                {
                    switch ((int)chosenCommand.Id)
                    {
                        case 1:
                            var selectedText = ((TextBox)sender).SelectedText;
                            var dataPackage = new DataPackage();
                            dataPackage.SetText(selectedText);
                            Clipboard.SetContent(dataPackage);
                            OutputTextBlock.Text = "'" + chosenCommand.Label + "'(" + chosenCommand.Id.ToString() + ") selected; '" + selectedText + "' copied to clipboard";
                            break;

                        case 2:
                            OutputTextBlock.Text = "'" + chosenCommand.Label + "'(" + chosenCommand.Id.ToString() + ") selected";
                            break;

                        case 3:
                            OutputTextBlock.Text = "'" + chosenCommand.Label + "'(" + chosenCommand.Id.ToString() + ") selected";
                            break;
                    }
                }
                else
                {
                    OutputTextBlock.Text = "Context menu dismissed";
                }
            }
            else
            {
                OutputTextBlock.Text = "Context menu not shown because there is no text selected";
            }
        }
    }
}
