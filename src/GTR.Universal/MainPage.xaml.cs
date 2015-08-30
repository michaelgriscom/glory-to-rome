#region

using System;
using System.Collections.Generic;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using GTR.Core.AIController;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Serialization;
using GTR.Core.Services;
using GTR.Universal.Services;
using GTR.Windows.Services;
using Newtonsoft.Json;

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
            var deckIo = new DeckIo();
            var resourceProvider = new ResourceProvider();
            var messageProvider = new MessageProvider();

            _delayedPlayerInput = new DelayedPlayerInput();

       
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            const string endpoint = "http://localhost:51291/";
            GameOptions gameOptions = new GameOptions
            {
                DeckName = "republic",
                MaxPlayers = 2
            };

            CreateGameRequest gr = new CreateGameRequest()
            {
                GameOptions = gameOptions
            };

            string createGameUrl = endpoint + "api/matchmaking";

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(gr);
                var stringContent = new HttpStringContent(json, UnicodeEncoding.Utf8, "application/json");
                var uri = new Uri(createGameUrl);
                var response = await client.PostAsync(uri, stringContent);
                var gameInfoJson = await response.Content.ReadAsStringAsync();
                var gameInfo = JsonConvert.DeserializeObject<CreateGameResponseSerialization>(gameInfoJson);

                 uri = new Uri(createGameUrl + "?GameId=" + gameInfo.GameId);
                response = await client.GetAsync(uri);
                var gameJson = await response.Content.ReadAsStringAsync();
                var game = JsonConvert.DeserializeObject<StartGameResponseSerialization>(gameJson);

                var deckIo = new DeckIo();
                var resourceProvider = new ResourceProvider();

                GameMarshaller marshaller = new GameMarshaller(deckIo, resourceProvider);
                var gamePoco = marshaller.UnMarshall(game.Game);
            }

            StartButton.IsEnabled = false;
            StartButton.Visibility = Visibility.Collapsed;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _delayedPlayerInput.Execute(null);
        }
    }
}