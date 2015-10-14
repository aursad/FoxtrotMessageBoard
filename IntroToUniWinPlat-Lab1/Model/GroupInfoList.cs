using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using IntroToUniWinPlat_Lab1.Annotations;

namespace IntroToUniWinPlat_Lab1.Model
{
    public class GroupInfoList : List<object>, INotifyPropertyChanged
    {
        private object _key;

        public object Key
        {
            get { return _key; }
            set { _key = value; OnPropertyChanged(nameof(Key)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}