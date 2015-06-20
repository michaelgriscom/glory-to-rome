#region

using System;
using GTR.Core.Game;

#endregion

namespace GTR.Core.ManipulatableRules.Actions
{
    internal class OrderActions
    {
        public readonly ArchitectAction Architect;
        public readonly CraftsmanAction Craftsman;
        public readonly LaborerAction Laborer;
        public readonly LegionnaireAction Legionnaire;
        public readonly MerchantAction Merchant;
        public readonly PatronAction Patron;
        public readonly ThinkerAction Thinker;

        public OrderActions(Player player, GameTable gameTable)
        {
            Craftsman = new CraftsmanAction(player, gameTable);
            Merchant = new MerchantAction(player, gameTable);
            Architect = new ArchitectAction(player, gameTable);
            Patron = new PatronAction(player, gameTable);
            Laborer = new LaborerAction(player, gameTable);
            Thinker = new ThinkerAction(player, gameTable);
            Legionnaire = new LegionnaireAction(player, gameTable);
        }

        public OrderActionBase GetAction(RoleType role)
        {
            switch (role)
            {
                case RoleType.Craftsman:
                    return Craftsman;

                case RoleType.Laborer:
                    return Laborer;

                case RoleType.Legionnaire:
                    return Legionnaire;

                case RoleType.Architect:
                    return Architect;

                case RoleType.Patron:
                    return Patron;

                case RoleType.Merchant:
                    return Merchant;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}