﻿using System;

namespace Seeker.Gamebook.Sheriff
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
                return true;

            foreach (string oneOption in option.Split(','))
            {
                if (oneOption.Contains(">") || oneOption.Contains("<"))
                {
                    int level = Game.Services.LevelParse(oneOption);

                    if (oneOption.Contains("ВЖУХ >=") && (level > protagonist.Whoosh))
                        return false;

                    if (oneOption.Contains("ВЖУХ <") && (level <= protagonist.Whoosh))
                        return false;

                    return true;
                }
                else if (oneOption.Contains("!"))
                {
                    if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                        return false;
                }
                else if (!Game.Option.IsTriggered(oneOption.Trim()))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
