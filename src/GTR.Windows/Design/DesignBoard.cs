#region

using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Windows.Design
{
    public class DesignBoard : PlayerBoard
    {
        public DesignBoard() : base("Test Player")
        {
            Camp = new DesignCamp();
            Hand = new DesignHand();
            CompletedBuildings = new CompletedBuildings();
            CompletedBuildings.Add(new OrderCardModel("Dock", "sdfsdgsdfg", RoleType.Craftsman));
            CompletedBuildings.Add(new OrderCardModel("Bar", "sdfsdgsdfg", RoleType.Laborer));

            ConstructionZone = new ConstructionZone();
            BuildingSite site = new BuildingSite(MaterialType.Brick);
            site.BuildingFoundation.Building = new OrderCardModel("Fountain", "asiodasdifj", RoleType.Merchant);
            ConstructionZone.Add(site);
            site = new BuildingSite(MaterialType.Concrete);
            site.BuildingFoundation.Building = new OrderCardModel("Aqueduct", "asiodasdifj", RoleType.Merchant);
            ConstructionZone.Add(site);

            DemandArea.Add(new OrderCardModel("aiojsdjiasof", "asdasjilf", RoleType.Architect));
            DemandArea.Add(new OrderCardModel("aiojsdjiasof", "asdasjilf", RoleType.Merchant));

            LeaderCardLocation.Add(new LeaderCardModel());

            PlayArea.OrderCards.Add(new OrderCardModel("asfdasdfsdf", "Sdfsdfgsdg", RoleType.Craftsman));
            PlayArea.OrderCards.Add(new OrderCardModel("asfdasdfsdf", "Sdfsdfgsdg", RoleType.Legionnaire));
        }
    }
}