#region

using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GTR.Core.AIController;
using GTR.Core.Game;
using GTR.Core.Services;
using GTR.Core.ViewModel;
using GTR.Universal.Services;
using GTR.Windows.Services;

#endregion

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GTR.Universal
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly DelayedPlayerInput _delayedPlayerInput;
        private readonly Game game;

        public MainPage()
        {
            InitializeComponent();
            //GameViewModel vm = new GameViewModel();
            //DesignGameTable dgt = new DesignGameTable();
            //vm.GameTable = dgt;
            //this.DataContext = vm;
            int playerCount = 3;
            GameOptions gameOptions = new GameOptions("republic", 2);
            var deckIo = new DeckIo();
            var resourceProvider = new ResourceProvider();
            var messageProvider = new MessageProvider();

            _delayedPlayerInput = new DelayedPlayerInput();

            var playerInputs = new Dictionary<string, IPlayerInput>();
            playerInputs.Add("player1", _delayedPlayerInput);

            game = new Game(playerInputs, gameOptions, deckIo, resourceProvider, messageProvider);
            var gameViewModel = new GameViewModel(game);
            DataContext = gameViewModel;
            //PlayerBoardDetailControl.DataContext = board;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            StartButton.Visibility = Visibility.Collapsed;
            await game.PlayGame();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _delayedPlayerInput.Execute(null);
        }
    }
}