#region

using System;
using System.Collections;
using System.Collections.Generic;
using GTR.Core.Model;

#endregion

namespace GTR.Core.CardCollections
{
    public interface IBoundable
    {
        int Capacity { get; set; }
    }

    public interface IConditionalAddable<T> where T : CardModelBase
    {
        bool CanAdd(T card);
    }


    public class BoundedCardCollection<T> : ObservableCardCollection<T>, IBoundable where T : CardModelBase
    {
        private int _capacity;
     
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
    }
}