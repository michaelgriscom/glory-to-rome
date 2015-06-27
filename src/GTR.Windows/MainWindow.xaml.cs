#region

using System.Windows;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.ViewModel;

#endregion

namespace GTR.Windows
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Camp mat = new Camp();
            // This is temporary
            mat.Stockpile.Add(new OrderCardModel("Latrine", "Drop card during think action", RoleType.Laborer));
            mat.Stockpile.Add(new OrderCardModel("Palasade", "Immune to Legionary", RoleType.Craftsman));
            mat.Stockpile.Add(new OrderCardModel("Shrine", "Hand size increased by 2", RoleType.Legionnaire));
            mat.Stockpile.Add(new OrderCardModel("Aquaduct", "Patron from hand", RoleType.Architect));
            mat.Stockpile.Add(new OrderCardModel("Colloseum", "Evil building that should't be included",
                RoleType.Merchant));
            mat.Stockpile.Add(new OrderCardModel("Forum", "You win if you have one of each patron", RoleType.Patron));

            mat.Clientele.Add(new OrderCardModel("Bar", "Patron from deck", RoleType.Laborer));
            mat.Clientele.Add(new OrderCardModel("Dock", "Labor from hand to stockpile", RoleType.Craftsman));
            mat.Clientele.Add(new OrderCardModel("Archway", "Architect from pool", RoleType.Legionnaire));
            mat.Clientele.Add(new OrderCardModel("Wall", "Every 2 cards in stockpile counts as 1 point",
                RoleType.Architect));
            mat.Clientele.Add(new OrderCardModel("Garden", "Patron action for each influence", RoleType.Merchant));
            mat.Clientele.Add(new OrderCardModel("Temple", "Hand size increases by 4", RoleType.Patron));

            mat.Vault.Add(new OrderCardModel("Something", "Something", RoleType.Laborer));
            mat.Vault.Add(new OrderCardModel("Something", "Something", RoleType.Craftsman));
            mat.Vault.Add(new OrderCardModel("Something", "Something", RoleType.Legionnaire));
            mat.Vault.Add(new OrderCardModel("Something", "Something", RoleType.Architect));
            mat.Vault.Add(new OrderCardModel("Something", "Something", RoleType.Patron));
            mat.Vault.Add(new OrderCardModel("Something", "Something", RoleType.Merchant));

            mat.CompletedFoundations.Add(new BuildingSite(MaterialType.Brick));
            mat.CompletedFoundations.Add(new BuildingSite(MaterialType.Concrete));
            mat.CompletedFoundations.Add(new BuildingSite(MaterialType.Rubble));
            mat.CompletedFoundations.Add(new BuildingSite(MaterialType.Marble));
            mat.CompletedFoundations.Add(new BuildingSite(MaterialType.Stone));
            mat.CompletedFoundations.Add(new BuildingSite(MaterialType.Wood));

            var board = new PlayerBoard("player");
            board.Camp = mat;
            var viewModel = new GameViewModel(board);
            DataContext = viewModel;
        }
    }
}