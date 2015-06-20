#region

using System;
using System.Collections.Generic;
using System.Diagnostics;

#endregion

namespace GTR.Core.Game
{
    public enum MaterialType
    {
        Brick,
        Concrete,
        Rubble,
        Marble,
        Stone,
        Wood
    }

    public enum ActionType
    {
        HandPlay,
        Thinker
    }

    public enum RoleType
    {
        Craftsman,
        Laborer,
        Legionnaire,
        Architect,
        Patron,
        Merchant
    }

    public enum SiteType
    {
        OutOfTown,
        InsideRome
    }

    public enum BuiltInDeck
    {
        Republic,
        Imperium
    }

    public static class EnumExtensions
    {
        /// <summary>
        ///     Code adapted from http://stackoverflow.com/questions/801029/get-all-values-of-enums
        /// </summary>
        public static IList<T> GetEnumList<T>()
        {
            T[] array = (T[]) Enum.GetValues(typeof (T));
            List<T> list = new List<T>(array);
            return list;
        }

        public static int MaterialWorth(this MaterialType type)
        {
            int worth;
            switch (type)
            {
                case MaterialType.Rubble:
                case MaterialType.Wood:
                    worth = 1;
                    break;

                case MaterialType.Brick:
                case MaterialType.Concrete:
                    worth = 2;
                    break;

                case MaterialType.Marble:
                case MaterialType.Stone:
                    worth = 3;
                    break;

                default:
                    worth = 0;
                    Debug.Assert(false, "Invalid material type: " + type);
                    break;
            }
            return worth;
        }

        public static MaterialType ToMaterial(this RoleType role)
        {
            var material = MaterialType.Brick;
            switch (role)
            {
                case RoleType.Legionnaire:
                    material = MaterialType.Brick;
                    break;

                case RoleType.Architect:
                    material = MaterialType.Concrete;
                    break;

                case RoleType.Craftsman:
                    material = MaterialType.Wood;
                    break;

                case RoleType.Patron:
                    material = MaterialType.Marble;
                    break;

                case RoleType.Merchant:
                    material = MaterialType.Stone;
                    break;

                case RoleType.Laborer:
                    material = MaterialType.Rubble;
                    break;

                default:
                    Debug.Assert(false, "Invalid role: " + role);
                    break;
            }
            return material;
        }

        public static RoleType ToRole(this MaterialType type)
        {
            var role = RoleType.Craftsman;
            switch (type)
            {
                case MaterialType.Brick:
                    role = RoleType.Legionnaire;
                    break;

                case MaterialType.Concrete:
                    role = RoleType.Architect;
                    break;

                case MaterialType.Rubble:
                    role = RoleType.Laborer;
                    break;

                case MaterialType.Marble:
                    role = RoleType.Patron;
                    break;

                case MaterialType.Stone:
                    role = RoleType.Merchant;
                    break;

                case MaterialType.Wood:
                    role = RoleType.Craftsman;
                    break;

                default:
                    Debug.Assert(false, "Invalid type: " + type);
                    break;
            }
            return role;
        }
    }
}