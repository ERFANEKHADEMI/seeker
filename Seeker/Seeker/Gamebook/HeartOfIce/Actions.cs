﻿using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.HeartOfIce
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public string RemoveTrigger { get; set; }
        public string Skill { get; set; }
        public bool Choice { get; set; }
        public bool Sell { get; set; }
        public bool SellIfAvailable { get; set; }
        public string SellType { get; set; }
        public bool Split { get; set; }

        public override List<string> Representer()
        {
            if (Price > 0)
            {
                string money = Game.Other.CoinsNoun(Price, "скад", "скада", "скадов");
                return new List<string> { String.Format("{0}, {1} скад{2}", Text, Price, money) };
            }
            else if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            else if (!String.IsNullOrEmpty(Skill))
                return new List<string> { Skill };

            else
                return new List<string>();
        }

        public override List<string> Status()
        {
            Character hero = Character.Protagonist;

            List<string> statusLines = new List<string>
            {
                String.Format("Здоровье: {0}", hero),
                String.Format("Деньги: {0}", hero),
            };

            if (hero.Food > 0)
                statusLines.Add(String.Format("Еда: {0}", hero.Food));

            if (hero.Shots > 0)
                statusLines.Add(String.Format("Выстрелов: {0}", hero.Shots));

            return statusLines;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(Character.Protagonist.Life, out toEndParagraph, out toEndText);

        public List<string> Get()
        {
            Character hero = Character.Protagonist;

            if (Choice)
                hero.Chosen = true;

            if (!String.IsNullOrEmpty(Skill))
            {
                hero.Skills.Add(Skill);
                hero.SkillsValue -= 1;
            }

            if ((Price > 0) && (hero.Money >= Price))
            {
                hero.Money += ((Sell || SellIfAvailable) ? Price : (Price * -1));
                Game.Option.Trigger(RemoveTrigger, remove: true);

                if (SellIfAvailable && (SellType == "Пистолет"))
                    hero.Shots = 0;

                if (SellIfAvailable && (SellType == "Еда"))
                    hero.Food -= 1;
            }

            if (Split)
                hero.Split += 1;

            if (((Price > 0) || Split) && !Multiple)
                Used = true;

            if (BenefitList != null)
                foreach (Modification modification in BenefitList)
                    modification.Do();

            return new List<string> { "RELOAD" };
        }

        public override bool IsButtonEnabled()
        {
            Character hero = Character.Protagonist;

            bool disabledBySkills = (!String.IsNullOrEmpty(Skill) &&
                ((hero.SkillsValue <= 0) || hero.Skills.Contains(Skill)));

            bool disbledByChoice = (Choice && hero.Chosen);
            bool disabledByPrice = (Price > 0) && (hero.Money < Price);
            bool disabledBySplit = Split && (hero.Split >= 2);
            bool disabledByAvailable = SellIfAvailable && !Available();

            return !(disbledByChoice || disabledBySkills || disabledByPrice || disabledBySplit || disabledByAvailable || Used);
        }

        public bool Available()
        {
            if (!String.IsNullOrEmpty(RemoveTrigger))
                return Game.Data.Triggers.Contains(RemoveTrigger);

            else if (SellType == "Пистолет")
                return Character.Protagonist.Shots > 0;

            else if (SellType == "Еда")
                return Character.Protagonist.Food > 0;

            return false;
        }

        public override bool CheckOnlyIf(string option)
        {
            Character hero = Character.Protagonist;

            if (option.Contains("|"))
            {
                foreach (string oneOption in option.Split('|'))
                {
                    if (hero.Skills.Contains(oneOption.Trim()))
                        return true;

                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;
                }

                return false;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("="))
                    {
                        if (oneOption.Contains("ВЫСТРЕЛОВ >=") && (int.Parse(oneOption.Split('=')[1]) > hero.Shots))
                            return false;

                        if (oneOption.Contains("ДЕНЬГИ >=") && (int.Parse(oneOption.Split('=')[1]) > hero.Money))
                            return false;

                        else if (oneOption.Contains("ЕДА >=") && (int.Parse(oneOption.Split('=')[1]) > hero.Food))
                            return false;
                    }
                    else if (!Game.Data.Triggers.Contains(oneOption.Trim()) && !hero.Skills.Contains(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }
    }
}
