#region

using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Universal.Design
{
    public class DesignPlayer : Player
    {
        public DesignPlayer() : base("DesignPlayer")
        {
            Camp = new DesignCamp();
            Hand = new DesignHand();
            CompletedBuildings = new CompletedBuildings
            {
                new OrderCardModel("Dock", "sdfsdgsdfg", RoleType.Craftsman),
                new OrderCardModel("Bar", "sdfsdgsdfg", RoleType.Laborer)
            };

            ConstructionZone = new ConstructionZone();
            BuildingSite site = new BuildingSite(MaterialType.Brick)
            {
                BuildingFoundation = { Building = new OrderCardModel("Fountain", "asiodasdifj", RoleType.Merchant) }
            };
            ConstructionZone.Add(site);
            site = new BuildingSite(MaterialType.Concrete)
            {
                BuildingFoundation = { Building = new OrderCardModel("Aqueduct", "asiodasdifj", RoleType.Merchant) }
            };
            ConstructionZone.Add(site);

            DemandArea.Add(new OrderCardModel("aiojsdjiasof", "asdasjilf", RoleType.Architect));
            DemandArea.Add(new OrderCardModel("aiojsdjiasof", "asdasjilf", RoleType.Merchant));

            PlayArea.OrderCards.Add(new OrderCardModel("asfdasdfsdf", "Sdfsdfgsdg", RoleType.Craftsman));
            PlayArea.OrderCards.Add(new OrderCardModel("asfdasdfsdf", "Sdfsdfgsdg", RoleType.Legionnaire));
        }
    }
}