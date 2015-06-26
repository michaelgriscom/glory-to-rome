#region

using System.Collections.Specialized;
using System.Linq;
using GTR.Core.Buildings;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.ManipulatableRules
{
    public class ConstructionManager
    {
        private readonly GameTable _gameTable;
        private readonly Player _player;

        public ConstructionManager(Player player, GameTable gameTable)
        {
            _player = player;
            _gameTable = gameTable;
            var constructionZone = player.ConstructionZone;
            constructionZone.CollectionChanged += ConstructionZoneOnCollectionChanged;

            var completedFoundations = player.Camp.CompletedFoundations;
            completedFoundations.CollectionChanged += CompletedFoundationsOnCollectionChanged;
        }

        private void CompletedFoundationsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }

            foreach (BuildingSite buildingSite in notifyCollectionChangedEventArgs.NewItems)
            {
                _player.Camp.InfluencePoints += buildingSite.MaterialType.MaterialWorth();
            }
        }

        private void ConstructionZoneOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }
            foreach (BuildingSite newSite in notifyCollectionChangedEventArgs.NewItems)
            {
                newSite.Complete += BuildingOnComplete;
                BuildingEffectFactory effectService = new BuildingEffectFactory();
                var buildingEffect = effectService.Create(newSite.BuildingFoundation.Building.Name);
                buildingEffect.CompleteBuilding(_player, _gameTable);
                buildingEffect.ActivateBuilding(_player, _gameTable);
            }
        }

        private void BuildingOnComplete(object sender, BuildingCompletedEventArgs args)
        {
            // move foundation into influence area
            var buildingSite = args.BuildingSite;
            var completedFoundations = _player.Camp.CompletedFoundations;
            var constructionZone = _player.ConstructionZone;
            _gameTable.MoveCard(buildingSite, constructionZone, completedFoundations);

            // move building into completed building zone
            var buildingCard = args.BuildingSite.BuildingFoundation.Building;
            var completedBuildings = _player.CompletedBuildings;
            _gameTable.MoveCard(buildingCard, buildingSite.BuildingFoundation, completedBuildings);

            // material cards no longer needed,
            // let the garbage collector get rid of them
            buildingSite.Materials = null;
            buildingSite.Complete -= BuildingOnComplete;
        }
    }
}