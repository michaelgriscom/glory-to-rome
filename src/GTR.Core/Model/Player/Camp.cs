#region

#endregion

namespace GTR.Core.Model
{
    public class Camp : ObservableModel
    {
        private const int StartingInfluence = 2;
        private Clientele _clientele;
        private CompletedFoundations _completedFoundations;
        private int _influencePoints;
        private Stockpile _stockpile;
        private Vault _vault;

        public Camp()
        {
            Clientele = new Clientele();
            Vault = new Vault();
            CompletedFoundations = new CompletedFoundations();
            Stockpile = new Stockpile();
            InfluencePoints = StartingInfluence;
        }

        public CompletedFoundations CompletedFoundations
        {
            get { return _completedFoundations; }
            private set
            {
                _completedFoundations = value;
                RaisePropertyChanged();
            }
        }

        public Clientele Clientele
        {
            get { return _clientele; }
            private set
            {
                _clientele = value;
                RaisePropertyChanged();
            }
        }

        public int InfluencePoints
        {
            get { return _influencePoints; }
            set
            {
                _influencePoints = value;
                RaisePropertyChanged();
            }
        }

        public Stockpile Stockpile
        {
            get { return _stockpile; }
            private set
            {
                _stockpile = value;
                RaisePropertyChanged();
            }
        }

        public Vault Vault
        {
            get { return _vault; }
            private set
            {
                _vault = value;
                RaisePropertyChanged();
            }
        }
    }
}