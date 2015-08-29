#region

using System;
using System.Threading.Tasks;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Services;

#endregion

namespace GTR.Core.Util
{
    public static class ExtensionMethods
    {
        public static MaterialType GetMaterialType(this OrderCardModel card)
        {
            return card.RoleType.ToMaterial();
        }

        public static bool Remove<T>(this ICardSource<T> source, CardModelBase card) where T : CardModelBase
        {
            for (int i = 0; i < source.Count; i++)
            {
                if (source.ElementAt(i) == card)
                {
                    source.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public static async Task TimeoutAfter(this Task task, int millisecondsTimeout)
        {
            if (task == await Task.WhenAny(task, Task.Delay(millisecondsTimeout)))
                await task;
            else
                throw new TimeoutException();
        }

        public static void Display(this IMessageProvider messageProvider, string messageText)
        {
            var message = new UserMessage(messageText);
            messageProvider.Display(message);
        }

        public static void Display(this IMessageProvider messageProvider, string messageText, MessageType messageType)
        {
            UserMessage message = new UserMessage(messageText, messageType);
            messageProvider.Display(message);
        }
    }
}