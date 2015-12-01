using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nintenlord.Feditor.Core.Public_API
{
    public enum GameEnum
    {
        FE6,
        FE6Trans,
        FE7J,
        FE7U,
        FE7E,
        FE8J,
        FE8U,
        FE8E
    }

    public static class GameEnumHelpers
    {
        public static bool TryGetGameCode(this GameEnum gameEnum, out string name)
        {
            bool result;
            switch (gameEnum)
            {
                case GameEnum.FE6:  
                    name = "AFEJ";
                    result = true;
                    break;
                case GameEnum.FE7J: 
                    name = "AE7J";
                    result = true;
                    break;
                case GameEnum.FE7U: 
                    name = "AE7E";
                    result = true;
                    break;
                case GameEnum.FE8J: 
                    name = "BE8J";
                    result = true;
                    break;
                case GameEnum.FE8U: 
                    name = "BE8E";
                    result = true;
                    break;
                case GameEnum.FE7E:
                case GameEnum.FE8E:
                case GameEnum.FE6Trans:
                default:
                    name = "Never Gonna Give You Up";
                    result = false;
                    break;
            }
            return result;
        }

        public static GameEnum GetGame(string gameCode)
        {
            switch (gameCode)
            {
                case "AFEJ":
                    return GameEnum.FE6;
                case "AE7J":
                    return GameEnum.FE7J;
                case "AE7E":
                    return GameEnum.FE7U;
                case "BE8J":
                    return GameEnum.FE8J;
                case "BE8E":
                    return GameEnum.FE8U;
                default:
                    throw new ArgumentException();
                    break;
            }
        }
    }
}
