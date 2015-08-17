using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GTR.Core.Game;
using GTR.Core.Services;
using GTR.Core.ViewModel;
using GTR.Universal.Design;
using GTR.Universal.Services;
using GTR.Windows.Services;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GTR.Universal
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Game game;
        private ButtonPlayerInput buttonPlayerInput;

        public MainPage()
        {
            this.InitializeComponent();
            //GameViewModel vm = new GameViewModel();
            //DesignGameTable dgt = new DesignGameTable();
            //vm.GameTable = dgt;
            //this.DataContext = vm;
            int playerCount = 3;
            GameOptions gameOptions = new GameOptions("republic");
            var deckIo = new UniversalDeckIo();
            var resourceProvider = new UniversalResourceProvider();
            var messageProvider = new NullMessageProvider();

            buttonPlayerInput = new ButtonPlayerInput();
            game = new Game(playerCount, gameOptions, deckIo, resourceProvider, messageProvider, () => buttonPlayerInput);
            var gameViewModel = new GameViewModel(game);
            this.DataContext = gameViewModel;
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
            buttonPlayerInput.Execute(null);
        }
    }
}
