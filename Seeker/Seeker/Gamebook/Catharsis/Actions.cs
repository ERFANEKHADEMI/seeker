﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.Catharsis
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public string Bonus { get; set; }

        public override List<string> Representer()
        {
            if (!String.IsNullOrEmpty(Bonus))
            {
                int currentStat = (int)protagonist.GetType().GetProperty(Bonus).GetValue(protagonist, null);

                Dictionary<string, int> startValues = Constants.GetStartValues();

                int diff = (currentStat - startValues[Bonus]);

                string diffLine = (diff > 0 ? String.Format(" (+{0})", diff) : String.Empty);

                return new List<string> { String.Format("{0}{1}", Text, diffLine) };
            }

            return new List<string>();
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Здоровье: {0}", protagonist.Life),
            String.Format("Аура: {0}", protagonist.Aura),
        };

        public override List<string> AdditionalStatus() =>  new List<string>
        {
            String.Format("Стелс: {0}", protagonist.Stealth),
            String.Format("Рукопашный бой: {0}", protagonist.Fight),
            String.Format("Меткость: {0}", protagonist.Accuracy),
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Life, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled() => !((!String.IsNullOrEmpty(Bonus) && (protagonist.Bonuses <= 0)));

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
                return option.Split('|').Where(x => Game.Data.Triggers.Contains(x.Trim())).Count() > 0;

            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("="))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (oneOption.Contains("СТЕЛС") && (level > protagonist.Stealth))
                            return false;

                        else if (oneOption.Contains("МЕТКОСТЬ") && (level > protagonist.Accuracy))
                            return false;

                        else if (oneOption.Contains("РУКОПАШКА") && (level > protagonist.Fight))
                            return false;

                        else if (oneOption.Contains("АУРА <") && (level <= protagonist.Aura))
                            return false;

                        else if (oneOption.Contains("АУРА >") && (level > protagonist.Aura))
                            return false;

                        else if (oneOption.Contains("ЗДОРОВЬЕ") && (level > protagonist.Life))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(Bonus) && (protagonist.Bonuses >= 0))
            {
                int currentStat = (int)protagonist.GetType().GetProperty(Bonus).GetValue(protagonist, null);

                currentStat += 1;

                protagonist.GetType().GetProperty(Bonus).SetValue(protagonist, currentStat);

                protagonist.Bonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public override bool IsHealingEnabled() => protagonist.Life < protagonist.MaxLife;

        public override void UseHealing(int healingLevel) => protagonist.Life += healingLevel;
    }
}
