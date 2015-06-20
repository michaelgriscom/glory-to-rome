#region

using GTR.Core.Util;

#endregion

namespace GTR.Core.Model
{
    public class PlayerBoard : ObservableObject
    {
        private Camp _camp;

        public PlayerBoard(Camp camp)
        {
            Camp = camp;
        }

        public Camp Camp
        {
            get { return _camp; }
            private set
            {
                _camp = value;
                RaisePropertyChanged();
            }
        }
    }
}