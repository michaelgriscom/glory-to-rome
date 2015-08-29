#region

using System.Collections.Generic;
using System.Collections.Specialized;
using GTR.Core.Action;
using GTR.Core.Buildings;
using GTR.Core.Engine;
using GTR.Core.Game;
using GTR.Core.Model;

#endregion

namespace GTR.Core.ManipulatableRules
{
    public class ConstructionManager
    {
        private readonly GameTable _gameTable;
        private HashSet<PlayerEngine> _playerEngines;

        public ConstructionManager(GameTable gameTable)
        {
            _gameTable = gameTable;
        
        }

        public void Attach(PlayerEngine playerEngine)
        {
            _playerEngines.Add(playerEngine);

            var constructionZone = playerEngine.Player.ConstructionZone;
            constructionZone.CollectionChanged +=
                (sender, args) => ConstructionZoneOnCollectionChanged(playerEngine, args);

            var completedFoundations = playerEngine.Player.Camp.CompletedFoundations;
            completedFoundations.CollectionChanged +=
                (sender, args) => CompletedFoundationsOnCollectionChanged(playerEngine, args);
        }

        private void CompletedFoundationsOnCollectionChanged(PlayerEngine playerEngine,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }

            foreach (BuildingSite buildingSite in notifyCollectionChangedEventArgs.NewItems)
            {
                playerEngine.Player.Camp.InfluencePoints += buildingSite.MaterialType.MaterialWorth();
            }
        }

        private void ConstructionZoneOnCollectionChanged(PlayerEngine playerEngine,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }
            foreach (BuildingSite newSite in notifyCollectionChangedEventArgs.NewItems)
            {
                newSite.Complete += (sender, args) => BuildingOnComplete(playerEngine, args);
                BuildingEffectFactory effectService = new BuildingEffectFactory();
                var buildingEffect = effectService.Create(newSite.BuildingFoundation.Building.Name);
                buildingEffect.CompleteBuilding(playerEngine, _gameTable);
                buildingEffect.ActivateBuilding(playerEngine, _gameTable);
            }
        }

        private void BuildingOnComplete(PlayerEngine playerEngine, BuildingCompletedEventArgs args)
        {
            // move foundation into influence area
            var buildingSite = args.BuildingSite;
            var completedFoundations = playerEngine.Player.Camp.CompletedFoundations;
            var constructionZone = playerEngine.Player.ConstructionZone;
            var siteMove = new Move<BuildingSite>(buildingSite, constructionZone, completedFoundations);
            siteMove.Perform();

            // move building into completed building zone
            var buildingCard = args.BuildingSite.BuildingFoundation.Building;
            var completedBuildings = playerEngine.Player.CompletedBuildings;
            var buildingMove = new Move<OrderCardModel>(buildingCard, buildingSite.BuildingFoundation,
                completedBuildings);
            buildingMove.Perform();

            // material cards no longer needed,
            // let the garbage collector get rid of them
            buildingSite.Materials = null;

            // TODO, we need to remove these event listeners but that will require rearchitecting
            //buildingSite.Complete -= BuildingOnComplete;
        }
    }
}