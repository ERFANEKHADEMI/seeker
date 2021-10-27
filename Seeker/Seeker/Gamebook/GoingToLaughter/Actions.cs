﻿using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public bool Advantage { get; set; }
        public bool Disadvantage { get; set; }

        public string Target { get; set; }
        public int Dices { get; set; }
        public int DiceBonus { get; set; }
        public int ResultBonus { get; set; }
        public bool DiceOfDice { get; set; }

        public List<string> Get()
        {
            if (Advantage)
            {
                protagonist.Advantages.Add(this.Button);
                protagonist.Balance += 1;
            }

            else if (Disadvantage)
            {
                protagonist.Disadvantages.Add(this.Button);
                protagonist.Balance -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        public override List<string> Status() => new List<string>
        {
            String.Format("Героизм: {0}", protagonist.Heroism),
            String.Format("Злодеяние: {0}", protagonist.Villainy),
            String.Format("Шутовство: {0}", protagonist.Buffoonery),
            String.Format("Вдохновение: {0}", protagonist.Inspiration)
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            if (protagonist.Buffoonery <= 0)
            {
                toEndParagraph = 1392;
                toEndText = "Это уже другая история...";

                return true;
            }
            else
            {
                toEndParagraph = 0;
                toEndText = String.Empty;

                return false;
            }
        }

        private bool Incompatible(string disadvantage)
        {
            if (!Constants.IncompatiblesDisadvantages.ContainsKey(disadvantage))
                return false;

            string incompatibles = Constants.IncompatiblesDisadvantages[disadvantage];

            foreach (string incompatible in incompatibles.Split(','))
                if (protagonist.Advantages.Contains(incompatible.Trim()) || protagonist.Disadvantages.Contains(incompatible.Trim()))
                    return true;

            return false;
        }
            
        public override bool IsButtonEnabled()
        {
            if (Advantage && protagonist.Advantages.Contains(this.Button))
                return false;

            else if (Disadvantage && (protagonist.Balance == 0))
                return false;

            else if (Disadvantage && Incompatible(this.Button))
                return false;

            else if (Disadvantage && protagonist.Disadvantages.Contains(this.Button))
                return false;

            else
                return true;
        }

        public override bool CheckOnlyIf(string option)
        {
            if (option.Contains("|"))
            {
                foreach (string oneOption in option.Split('|'))
                {
                    if (protagonist.Disadvantages.Contains(oneOption.Trim()))
                        return true;
                }

                return false;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Other.LevelParse(oneOption);

                        if (oneOption.Contains("БАЛАНС <=") && (level < protagonist.Balance))
                            return false;

                        if (oneOption.Contains("ГЕРОИЗМ >=") && (level > protagonist.Heroism))
                            return false;

                        if (oneOption.Contains("ЗЛОДЕЙСТВО >=") && (level > protagonist.Villainy))
                            return false;

                        if (oneOption.Contains("ЗЛОДЕЙСТВО <") && (level <= protagonist.Villainy))
                            return false;
                    }
                    else if (option.Contains("!"))
                    {
                        if (protagonist.Disadvantages.Contains(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!protagonist.Advantages.Contains(option) && !protagonist.Disadvantages.Contains(option))
                        return false;
                };

                return true;
            }
        }

        public List<string> HeroismCheck()
        {
            List<string> luckCheck = new List<string> { String.Format("Уровень героизма: {0}.", protagonist.Heroism) };

            if (protagonist.Heroism >= 5)
            {
                luckCheck.Add("В броске даже нет необходимости!");
                luckCheck.Add("BIG|GOOD|УСПЕХ :)");

                return luckCheck;
            }
            else
            {
                int level = 6 - protagonist.Heroism;
                int dice = Game.Dice.Roll();

                luckCheck.Add(String.Format("Для прохождения проверки нужно выбросить {0} или больше.", level));
                luckCheck.Add(String.Format("Бросок: {0}", Game.Dice.Symbol(dice)));
                luckCheck.Add(dice >= level ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

                return luckCheck;
            }
        }

        public List<string> DiceValues()
        {
            List<string> diceValues = new List<string> { };

            int dicesResult = 0;
            int dicesCount = (Dices > 0 ? Dices : 1);

            if (DiceOfDice)
            {
                dicesCount = Game.Dice.Roll();
                diceValues.Add(String.Format("Количество кубиков: {0}", Game.Dice.Symbol(dicesCount)));
                diceValues.Add(String.Empty);
            }

            for (int i = 1; i <= dicesCount; i++)
            {
                string bonus = String.Empty;
                int dice = Game.Dice.Roll();
                dicesResult += dice;

                if (DiceBonus > 0)
                {
                    dicesResult += DiceBonus;
                    bonus = String.Format(" + {0} по условию", DiceBonus);
                }

                diceValues.Add(String.Format("На {0} выпало: {1}{2}", i, Game.Dice.Symbol(dice), bonus));
            }

            if (ResultBonus > 0)
            {
                dicesResult += ResultBonus;
                diceValues.Add(String.Format(" +{0} по условию", ResultBonus));
            }

            int currentValue = (int)protagonist.GetType().GetProperty(Target).GetValue(protagonist, null);
            protagonist.GetType().GetProperty(Target).SetValue(protagonist, (currentValue + dicesResult));

            diceValues.Add(String.Format("BIG|BOLD|Вы добавили +{0} к {1}",
                dicesResult, Constants.ParamNames()[Target]));

            return diceValues;
        }
    }
}
