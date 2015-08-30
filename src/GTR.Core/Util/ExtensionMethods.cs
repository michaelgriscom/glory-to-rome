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

        // this is pretty hacked, it's a result of trying to keep covariance; a refactor will be needed to change it
        public static bool Remove<T>(this ICardLocation<T> source, CardModelBase card) where T : CardModelBase
        {
            int index = -1;
            bool success = false;
            foreach (var existingCard in source)
            {
                index++;
                if (existingCard == card)
                {
                    success = true;
                    break;
                }
            }
            if (success)
            {
                source.RemoveAt(index);
            }
            
            return success;
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