#region

using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Windows.Design
{
    /// <summary>
    ///     Sample data used only to view XAML at design-time.
    /// </summary>
    internal class DesignCamp : Camp
    {
        public DesignCamp()
        {
            CompletedFoundations.Add(new BuildingSite(MaterialType.Brick));
            CompletedFoundations.Add(new BuildingSite(MaterialType.Concrete));
            CompletedFoundations.Add(new BuildingSite(MaterialType.Rubble));
            CompletedFoundations.Add(new BuildingSite(MaterialType.Marble));
            CompletedFoundations.Add(new BuildingSite(MaterialType.Stone));
            CompletedFoundations.Add(new BuildingSite(MaterialType.Wood));

            Clientele.Add(new OrderCardModel("Blah", "Blah", RoleType.Architect));
            Clientele.Add(new OrderCardModel("Blah", "Blah", RoleType.Craftsman));
            Clientele.Add(new OrderCardModel("Blah", "Blah", RoleType.Laborer));
            Clientele.Add(new OrderCardModel("Blah", "Blah", RoleType.Legionnaire));
            Clientele.Add(new OrderCardModel("Blah", "Blah", RoleType.Merchant));
            Clientele.Add(new OrderCardModel("Blah", "Blah", RoleType.Patron));

            Stockpile.Add(new OrderCardModel("Blah", "Blah", RoleType.Architect));
            Stockpile.Add(new OrderCardModel("Blah", "Blah", RoleType.Craftsman));
            Stockpile.Add(new OrderCardModel("Blah", "Blah", RoleType.Laborer));
            Stockpile.Add(new OrderCardModel("Blah", "Blah", RoleType.Legionnaire));
            Stockpile.Add(new OrderCardModel("Blah", "Blah", RoleType.Merchant));
            Stockpile.Add(new OrderCardModel("Blah", "Blah", RoleType.Patron));

            Vault.Add(new OrderCardModel("Blah", "Blah", RoleType.Architect));
            Vault.Add(new OrderCardModel("Blah", "Blah", RoleType.Craftsman));
            Vault.Add(new OrderCardModel("Blah", "Blah", RoleType.Laborer));
            Vault.Add(new OrderCardModel("Blah", "Blah", RoleType.Legionnaire));
            Vault.Add(new OrderCardModel("Blah", "Blah", RoleType.Merchant));
            Vault.Add(new OrderCardModel("Blah", "Blah", RoleType.Patron));
        }
    }
}