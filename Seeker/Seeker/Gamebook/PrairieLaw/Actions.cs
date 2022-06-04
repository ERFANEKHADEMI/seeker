﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.PrairieLaw
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public bool Firefight { get; set; }
        public bool HeroWoundsLimit { get; set; }
        public bool EnemyWoundsLimit { get; set; }

        public int Dices { get; set; }
        public bool Roulette { get; set; }
        public string SellPrices { get; set; }
        public string RemoveTrigger { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Ловкость: {0}", protagonist.Skill),
            String.Format("Сила: {0}/{1}", protagonist.Strength, protagonist.MaxStrength),
            String.Format("Обаяние: {0}", protagonist.Charm),
        };

        public override List<string> AdditionalStatus() => new List<string>
        {
            String.Format("Патронов: {0}", protagonist.Cartridges),
            String.Format("Долларов: {0:f2}", Services.ToDollars(protagonist.Cents))
        };

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Strength, out toEndParagraph, out toEndText);

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
                return new List<string> { String.Format("{0}, {1:f2}$", Text, Services.ToDollars(Price)) };

            else if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string line = String.Format("{0}\nловкость {1}  сила {2}", enemy.Name, enemy.Skill, enemy.Strength);

                if (Firefight)
                    line += String.Format("  патроны {0}", enemy.Cartridges);

                enemies.Add(line);
            }

            return enemies;
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains("|"))
            {
                return option.Split('|').Where(x => Game.Option.IsTriggered(x.Trim())).Count() > 0;
            }
            else
            {
                foreach (string oneOption in option.Split(','))
                {
                    if (oneOption.Contains("="))
                    {
                        int level = Game.Services.LevelParse(oneOption);

                        if (option.Contains("ЦЕНТОВ >=") && (level > protagonist.Cents))
                            return false;

                        else if (option.Contains("САМОРОДКОВ >=") && (level > protagonist.Nuggets))
                            return false;

                        else if (option.Contains("ПАТРОНОВ >=") && (level > protagonist.Cartridges))
                            return false;

                        else if (option.Contains("ШКУР >=") && (level > protagonist.AnimalSkins.Count))
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

        public List<string> Luck()
        {
            List<string> luckCheck = new List<string>
            {
                "Цифры удачи:",
                "BIG|" + Services.LuckNumbers()
            };

            int goodLuck = Game.Dice.Roll();

            luckCheck.Add(String.Format("Проверка удачи: {0} - {1}зачёркунтый",
                Game.Dice.Symbol(goodLuck), (protagonist.Luck[goodLuck] ? "не " : String.Empty)));

            luckCheck.Add(Result(protagonist.Luck[goodLuck], "УСПЕХ|НЕУДАЧА"));

            protagonist.Luck[goodLuck] = !protagonist.Luck[goodLuck];

            return luckCheck;
        }

        public List<string> LuckRecovery()
        {
            List<string> luckRecovery = new List<string> { "Восстановление удачи:" };

            bool success = false;

            for (int i = 1; i < 7; i++)
            {
                if (!protagonist.Luck[i])
                {
                    luckRecovery.Add(String.Format("GOOD|Цифра {0} восстановлена!", i));
                    protagonist.Luck[i] = true;
                    success = true;

                    break;
                }
            }

            if (!success)
            {
                luckRecovery.Add("BAD|Все цифры и так счастливые!");
            }

            luckRecovery.Add("Цифры удачи теперь:");
            luckRecovery.Add("BIG|" + Services.LuckNumbers());

            return luckRecovery;
        }

        public List<string> Charm()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodCharm = (firstDice + secondDice) <= protagonist.Charm;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка обаяния: {0} + {1} {2} {3}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodCharm ? "<=" : ">"), protagonist.Charm) };

            if (goodCharm)
            {
                luckCheck.Add("BIG|GOOD|УСПЕХ :)");
                luckCheck.Add("Вы увеличили своё обаяние на единицу");

                protagonist.Charm += 1;
            }
            else
            {
                luckCheck.Add("BIG|BAD|НЕУДАЧА :(");

                if (protagonist.Charm > 2)
                {
                    luckCheck.Add("Вы уменьшили своё обаяние на единицу");
                    protagonist.Charm -= 1;
                }
            }

            return luckCheck;
        }

        public List<string> Skill()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            bool goodSkill = (firstDice + secondDice) <= protagonist.Skill;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка ловкости: {0} + {1} {2} {3}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodSkill ? "<=" : ">"), protagonist.Skill) };

            luckCheck.Add(Result(goodSkill, "УСПЕХ|НЕУДАЧА"));

            return luckCheck;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dicesCount = (Dices == 0 ? 1 : Dices);
            int dices = 0;

            for (int i = 1; i <= dicesCount; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            protagonist.Strength -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledByUsed = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (protagonist.Cents < Price);
            bool disabledBySkins = (Type == "SellSkins") && (protagonist.AnimalSkins.Count == 0);
            bool disabledByNuggets = (Type == "SellNuggets") && (protagonist.Nuggets == 0);
            bool disabledByGame = Roulette && (protagonist.Cents < 100);

            return !(disabledByUsed || disabledByPrice || disabledBySkins || disabledByNuggets || disabledByGame);
        }

        public List<string> Get()
        {
            if (!String.IsNullOrEmpty(RemoveTrigger))
                Game.Option.Trigger(RemoveTrigger, remove: true);

            if ((Price > 0) && (protagonist.Cents >= Price))
            {
                protagonist.Cents -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> SellSkins()
        {
            List<string> salesReport = new List<string>();

            int cents = 0, sold = 0, index = 0;

            Dictionary<string, int> prices = new Dictionary<string, int>();
            List<int> saledIndexes = new List<int>();

            foreach(string price in SellPrices.Split(',').ToList())
            {
                string[] valuePrice = price.Split('=');
                prices.Add(valuePrice[0].Trim(), int.Parse(valuePrice[1]));
            }

            bool anySkin = prices.ContainsKey("Любая шкура");

            foreach (string skin in protagonist.AnimalSkins)
            {
                if (prices.ContainsKey(skin) || anySkin)
                {
                    int price = (anySkin ? prices["Любая шкура"] : prices[skin]);

                    salesReport.Add(String.Format("{0} - купил за {1:f2}$", skin, Services.ToDollars(price)));
                    cents += price;
                    saledIndexes.Add(index);
                    sold += 1;
                }
                else
                    salesReport.Add(String.Format("{0} - её не купит", skin));

                index += 1;
            }

            saledIndexes.Reverse();

            foreach (int removeIndex in saledIndexes)
                protagonist.AnimalSkins.RemoveAt(removeIndex);

            salesReport.Add(String.Empty);
            salesReport.Add("BIG|ИТОГО:");
            salesReport.Add(String.Format("Вы продали шкур: {0}", sold));
            salesReport.Add(String.Format("GOOD|Вы получили: {0:f2}$", Services.ToDollars(cents)));

            protagonist.Cents += cents;

            return salesReport;
        }
        
        public List<string> SellNuggets()
        {
            List<string> salesReport = new List<string>();

            int price = int.Parse(SellPrices);
            int cents = protagonist.Nuggets * price;
            protagonist.Cents += cents;

            salesReport.Add(String.Format("Вы продали самородков: {0}", protagonist.Nuggets));
            salesReport.Add(String.Format("Цена за один: {0:f2}$", Services.ToDollars(price)));
            salesReport.Add(String.Format("GOOD|Вы получили: {0:f2}$", Services.ToDollars(cents)));

            protagonist.Nuggets = 0;

            return salesReport;
        }

        public List<string> RedOrBlackGame()
        {
            List<string> gameReport = new List<string>();

            bool red = (Game.Dice.Roll() > 3);
            int dice = Game.Dice.Roll();
            bool even = (dice % 2 == 0);

            gameReport.Add(String.Format("Вы поставили на {0}", (red ? "красное (чёт)" : "чёрное (нечет)")));
            gameReport.Add(String.Format("На рулетке выпало: {0} - {1}", Game.Dice.Symbol(dice), (even ? "красное" : "чёрное")));

            if (red == even)
            {
                gameReport.Add("GOOD|Вы ВЫИГРАЛИ и получили 1$ :)");
                protagonist.Cents += 100;
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ и потеряли 1$ :(");
                protagonist.Cents -= 100;
            }

            return gameReport;
        }

        public List<string> DoubleGame()
        {
            List<string> gameReport = new List<string>();

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            gameReport.Add(String.Format("На рулетке выпали: {0} и {1}", Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice)));

            if (firstDice == secondDice)
            {
                gameReport.Add("GOOD|Цифры совпали - вы ВЫИГРАЛИ и получили 5$ :)");
                protagonist.Cents += 500;
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ и потеряли 1$ :(");
                protagonist.Cents -= 100;
            }

            return gameReport;
        }

        public List<string> DiceLtlGame()
        {
            List<string> gameReport = new List<string>();

            int dice = Game.Dice.Roll();
            bool even = (dice % 2 == 0);

            gameReport.Add(String.Format("На кубике выпало: {0} - {1}", Game.Dice.Symbol(dice), (even ? "чётное" : "нечётное")));

            if (even)
            {
                gameReport.Add("GOOD|Вы ВЫИГРАЛИ и получаете 1$ :)");
                protagonist.Cents += 100;
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ и потеряли 1$ :(");
                protagonist.Cents -= 100;
            }

            return gameReport;
        }

        public List<string> DiceGame()
        {
            List<string> gameReport = new List<string>();

            int dice = Game.Dice.Roll();
            bool even = (dice % 2 == 0);
            bool nuggetsGame = Game.Option.IsTriggered("Игра на самородок");

            gameReport.Add(String.Format("На кубике выпало: {0} - {1}", Game.Dice.Symbol(dice), (even ? "чётное" : "нечётное")));

            if (even)
            {
                gameReport.Add("GOOD|Вы ВЫИГРАЛИ :)");

                if (nuggetsGame)
                {
                    gameReport.Add("Самородок теперь ваш.");
                    protagonist.Nuggets += 1;
                }
                else
                {
                    gameReport.Add("Вы выиграли 3 доллара.");
                    protagonist.Cents += 300;
                }
            }
            else
            {
                gameReport.Add("BAD|Вы ПРОИГРАЛИ :(");

                if (nuggetsGame)
                {
                    gameReport.Add("Вы потеряли 1$");
                    protagonist.Cents -= 100;
                }
                else
                {
                    gameReport.Add("Вы потеряли 3$");
                    protagonist.Cents -= 300;
                }
            }

            return gameReport;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1;
            bool firefight = Firefight;

            while (true)
            {
                firefight = Services.FirefightContinue(FightEnemies, ref fight, firefight);

                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                bool attackAlready = false;
                int protagonistHitStrength = 0, enemyHitStrength = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    string cartridgesLine = (enemy.Cartridges > 0 ? String.Format(", патронов {0}", enemy.Cartridges) : String.Empty);
                    fight.Add(String.Format("{0} (сила {1}{2})", enemy.Name, enemy.Strength, cartridgesLine));

                    bool noCartridges = protagonist.Cartridges <= 0;

                    if (!attackAlready && (!firefight || !noCartridges))
                    {
                        Game.Dice.DoubleRoll(out int protagonistRollFirst, out int protagonistRollSecond);
                        protagonistHitStrength = protagonistRollFirst + protagonistRollSecond + protagonist.Skill;

                        string protagonistHitLine = (firefight ? "Ваш выстрел" : "Мощность вашего удара");

                        fight.Add(String.Format("{0}: {1} + {2} + {3} = {4}",
                            protagonistHitLine, Game.Dice.Symbol(protagonistRollFirst), Game.Dice.Symbol(protagonistRollSecond), protagonist.Skill, protagonistHitStrength));

                        if (firefight)
                            protagonist.Cartridges -= 1;
                    }

                    if (!firefight || (enemy.Cartridges > 0))
                    {
                        Game.Dice.DoubleRoll(out int enemyRollFirst, out int enemyRollSecond);
                        enemyHitStrength = enemyRollFirst + enemyRollSecond + enemy.Skill;

                        string enemyHitLine = (firefight ? "Его выстрел" : "Мощность его удара");

                        fight.Add(String.Format("{0}: {1} + {2} + {3} = {4}",
                            enemyHitLine, Game.Dice.Symbol(enemyRollFirst), Game.Dice.Symbol(enemyRollSecond), enemy.Skill, enemyHitStrength));

                        if (firefight)
                            enemy.Cartridges -= 1;
                    }
                    else
                        enemyHitStrength = 0;

                    if ((protagonistHitStrength == 0) && (enemyHitStrength == 0))
                    { 
                        // nothing to do here
                    }
                    else if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                        enemy.Strength -= (firefight ? 3 : 2);

                        bool enemyLost = Services.NoMoreEnemies(FightEnemies, EnemyWoundsLimit);

                        if (enemyLost)
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                            return fight;
                        }
                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));

                        protagonist.Strength -= (firefight ? 3 : 2);

                        if ((protagonist.Strength <= 0) || (HeroWoundsLimit && (protagonist.Strength <= 2)))
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                            return fight;
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    attackAlready = true;

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() => protagonist.Strength < protagonist.MaxStrength;

        public override void UseHealing(int healingLevel)
        {
            if (healingLevel == -1)
                protagonist.Strength = protagonist.MaxStrength;
            else
                protagonist.Strength += healingLevel;
        }
    }
}
