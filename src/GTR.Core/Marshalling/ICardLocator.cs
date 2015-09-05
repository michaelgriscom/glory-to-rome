using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GTR.Core.Marshalling.DTO;
using GTR.Core.Model;
using GTR.Core.Model.CardCollections;

namespace GTR.Core.Marshalling
{
    public interface ICardLocator //<T> where T : CardSerialization
    {
        T Locate<T>(int id) where T : CardModelBase;
        //OrderCardModel Locate(OrderCardSerialization orderCardDto);

        //JackCardModel Locate(JackCardSerialization jackCardDto);

        //BuildingSite Locate(BuildingFoundationSerialization buildingFoundationDto);
    }

    public interface ICardCollectionLocator // where T : CardModelBase
    {
        ICardCollection<T> Locate<T>(int id) where T : CardModelBase;
    }
}
