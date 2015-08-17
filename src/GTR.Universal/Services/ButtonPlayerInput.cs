#region

using System.Collections.Generic;
using System.Threading.Tasks;
using GTR.Core.Action;
using GTR.Core.AIController;
using GTR.Core.CardCollections;
using GTR.Core.Game;
using GTR.Core.Model;
using GTR.Core.Services;
using GTR.Core.Util;

#endregion

namespace GTR.Universal.Services
{
    public class ButtonPlayerInput : IPlayerInput
    {
        private readonly AiPlayerInput _aiInput;
        private TaskCompletionSource<bool> ReadyToLeadTask;

        public ButtonPlayerInput()
        {
            LeadCommand = new RelayCommand<object>(Execute, CanExecute);
            _aiInput = new AiPlayerInput();
        }

        public RelayCommand<object> LeadCommand { get; private set; }

        public async Task<RoleType> GetLeadRole(ICollection<RoleType> availableLeads)
        {
            return await _aiInput.GetLeadRole(availableLeads);
        }

        public async Task<ICollection<HandCardModel>> SelectCards(ICollection<HandCardModel> cards)
        {
            return await _aiInput.SelectCards(cards);
        }

        public async Task<RoleType> GetRole(ICollection<RoleType> collection)
        {
            return await _aiInput.GetRole(collection);
        }

        public async Task<ICardSource<HandCardModel>> GetSource(List<ICardSource<HandCardModel>> availableSources)
        {
            return await _aiInput.GetSource(availableSources);
        }

        public async Task<ActionType> GetLead()
        {
            ReadyToLeadTask = new TaskCompletionSource<bool>();
            //await ReadyToLeadTask.Task;
            await Task.Delay(1000);
            return await _aiInput.GetLead();
        }

        public async Task<ActionType> GetFollow()
        {
            return await _aiInput.GetFollow();
        }

        public async Task<ICollection<HandCardModel>> SelectLeadCards(List<HandCardModel> cardOptions)
        {
            return await _aiInput.SelectLeadCards(cardOptions);
        }

        public async Task<ICollection<HandCardModel>> SelectFollowCards(List<HandCardModel> cardOptions, RoleType role)
        {
            return await _aiInput.SelectFollowCards(cardOptions, role);
        }

        public async Task<IAction> GetMove(MoveSpace moveSpace)
        {
            return await _aiInput.GetMove(moveSpace);
        }

        public void Execute(object o)
        {
            if (ReadyToLeadTask != null)
            {
                ReadyToLeadTask.SetResult(true);
                ReadyToLeadTask = null;
            }
        }

        private bool CanExecute(object o)
        {
            return ReadyToLeadTask != null;
        }
    }
}