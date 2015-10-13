using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace IntroToUniWinPlat_Lab1
{
    public partial class MainPage : Page
    {
        public const string FeatureName = "Simple context menu";

        readonly List<Scenario> _scenarios = new List<Scenario>
        {
            new Scenario
            {
                Title = "Messages",
                ClassType = typeof(Messages)
            },
            new Scenario
            {
                Title = "Settings",
                ClassType = typeof(Settings)
            },
        };
    }

    public class Scenario
    {
        public string Title { get; set; }
        public Type ClassType { get; set; }
    }
}
