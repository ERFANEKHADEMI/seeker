﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Seeker.Gamebook.LordOfTheSteppes
{
    class Actions : Abstract.IActions
    {

        public string ActionName { get; set; }
        public string ButtonName { get; set; }
        public string Aftertext { get; set; }
        public string Trigger { get; set; }
        public string Text { get; set; }
        public string Stat { get; set; }
        public int StatStep { get; set; }
        public Character.SpecialTechniques SpecialTechnique { get; set; }

        public int Dices { get; set; }
        public bool Odd { get; set; }
        public bool Initiative { get; set; }

        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public bool GroupFight { get; set; }
        public bool NotToDeath { get; set; }
        public int Coherence { get; set; }
        public bool StoneGuard { get; set; }

        public int Price { get; set; }
        public bool Used { get; set; }
        public bool Multiple { get; set; }
        public List<Modification> Benefit { get; set; }

        public List<string> Do(out bool reload, string action = "", bool trigger = false)
        {
            if (trigger)
                Game.Option.Trigger(Trigger);

            string actionName = (String.IsNullOrEmpty(action) ? ActionName : action);
            List<string> actionResult = typeof(Actions).GetMethod(actionName).Invoke(this, new object[] { }) as List<string>;

            reload = (actionResult.Count >= 1) && (actionResult[0] == "RELOAD");

            return actionResult;
        }

        public List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (!String.IsNullOrEmpty(Stat))
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);

                Dictionary<string, int> startValues = Constants.GetStartValues();

                int diff = (currentStat - startValues[Stat]);

                string diffLine = (diff > 0 ? String.Format(" (+{0})", diff) : String.Empty);

                return new List<string> { String.Format("{0}{1}", Text, diffLine) };
            }

            else if (!String.IsNullOrEmpty(Text) || (ActionName == "Get"))
                return new List<string> { Text };

            else if (Enemies == null)
                return enemies;

            if ((Allies != null) && GroupFight)
            {
                foreach (Character ally in Allies)
                    if (ally.Name == Character.Protagonist.Name)
                    {
                        enemies.Add(String.Format("Вы\nнападение {0}  защита {1}  жизнь {2}  инициатива {3}{4}",
                            Character.Protagonist.Attack, Character.Protagonist.Defence, Character.Protagonist.Endurance,
                            Character.Protagonist.Initiative, Character.Protagonist.GetSpecialTechniques()));
                    }
                    else
                    {
                        enemies.Add(String.Format("{0}\nнападение {1}  защита {2}  жизнь {3}  инициатива {4}{5}",
                            ally.Name, ally.Attack, ally.Defence, ally.Endurance, ally.Initiative, ally.GetSpecialTechniques()));
                    }

                enemies.Add("SPLITTER|против");
            }

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nнападение {1}  защита {2}  жизнь {3}  инициатива {4}{5}",
                    enemy.Name, enemy.Attack, enemy.Defence, enemy.Endurance, enemy.Initiative, enemy.GetSpecialTechniques()));

            return enemies;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Жизнь: {0}", Character.Protagonist.Endurance),
                String.Format("Монеты: {0}", Character.Protagonist.Coins),
            };

            return statusLines;
        }

        public List<string> AdditionalStatus()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Инициатива: {0}", Character.Protagonist.Initiative),
                String.Format("Защита: {0}", Character.Protagonist.Defence),
                String.Format("Нападение: {0}", Character.Protagonist.Attack),
            };

            return statusLines;
        }

        public List<string> StaticButtons() => new List<string> { };

        public bool StaticAction(string action) => false;

        public bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = "Начать сначала";

            return (Character.Protagonist.Endurance <= 0);
        }

        public bool IsButtonEnabled()
        {
            bool disabledByChoosed = (SpecialTechnique != Character.SpecialTechniques.Nope) &&
                (Character.Protagonist.SpecialTechnique.Count > 0);

            bool disabledByBonuses = (!String.IsNullOrEmpty(Stat)) && (Character.Protagonist.Bonuses <= 0);
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Coins < Price);
            bool disabledByUsed = ((Price > 0) ||(Price < 0)) && Used;

            return !(disabledByChoosed || disabledByBonuses || disabledByPrice || disabledByUsed);
        }

        public static bool CheckOnlyIf(string option)
        {
            foreach (Character.SpecialTechniques technique in Character.Protagonist.SpecialTechnique)
                if (option == technique.ToString())
                    return true;

            if (option.Contains("|"))
            {
                foreach (string oneOption in option.Split('|'))
                    if (Game.Data.Triggers.Contains(oneOption.Trim()))
                        return true;

                return false;
            }
            else
            {
                if (option.Contains(">") || option.Contains("<"))
                {
                    if (option.Contains("МОНЕТ >=") && (int.Parse(option.Split('=')[1]) > Character.Protagonist.Coins))
                        return false;

                    return true;
                }
                else
                    return Game.Data.Triggers.Contains(option);
            }
        }

        public List<string> Get()
        {
            if ((SpecialTechnique != Character.SpecialTechniques.Nope) && (Character.Protagonist.SpecialTechnique.Count == 0))
                Character.Protagonist.SpecialTechnique.Add(SpecialTechnique);

            else if ((StatStep > 0) && (Character.Protagonist.Bonuses >= 0))
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);

                currentStat += StatStep;

                Character.Protagonist.GetType().GetProperty("Max" + Stat).SetValue(Character.Protagonist, currentStat);
                Character.Protagonist.GetType().GetProperty(Stat).SetValue(Character.Protagonist, currentStat);

                Character.Protagonist.Bonuses -= 1;
            }

            else if (((Price > 0) || (Price < 0)) && (Character.Protagonist.Coins >= Price))
            {
                Character.Protagonist.Coins -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                    foreach (Modification modification in Benefit)
                        modification.Do();
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> FunnyFight()
        {
            List<string> fight = new List<string>();

            const int ENEMY_STRENGTH = 6;

            int heroWounds = 0, enemyWounds = 0;

            while (true)
            {
                int firstRoll = Game.Dice.Roll();
                int secondRoll = Game.Dice.Roll();
                int heroStrength = firstRoll + secondRoll + Character.Protagonist.Attack;

                fight.Add(String.Format(
                    "Ваша сила удара: {0} + {1} + {2} = {3}",
                    Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(secondRoll), Character.Protagonist.Attack, heroStrength
                ));

                firstRoll = Game.Dice.Roll();
                secondRoll = Game.Dice.Roll();
                int enemyStrength = firstRoll + secondRoll + ENEMY_STRENGTH;

                fight.Add(String.Format(
                    "Cила удара удальца: {0} + {1} + {2} = {3}",
                    Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(secondRoll), ENEMY_STRENGTH, enemyStrength
                ));

                if (heroStrength > enemyStrength)
                {
                    fight.Add("GOOD|Вы нанесли ему удар");
                    fight.Add(String.Empty);

                    enemyWounds += 1;

                    if (enemyWounds >= 3)
                    {
                        fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                        return fight;
                    }
                }
                else if (heroStrength < enemyStrength)
                {
                    fight.Add("BAD|Он нанёс вам удар");
                    fight.Add(String.Empty);

                    heroWounds += 1;

                    if (heroWounds >= 4)
                    {
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        return fight;
                    }
                }
                else
                    fight.Add("Вы парировали удары друг друга");
            }
        }

        public List<string> GameOfDice()
        {
            if (Character.Protagonist.Coins < 5)
                return new List<string> { "BAD|У вас не достаточно денег, чтобы играть..." };

            List<string> diceGame = new List<string> { };

            int myResult, enemyResult;

            do
            {
                int hisFirstDice = Game.Dice.Roll();
                int hisSecondDice = Game.Dice.Roll();
                enemyResult = hisFirstDice + hisSecondDice + 4;

                diceGame.Add(String.Format(
                    "Он бросил: {0} + {1} = {2}",
                    Game.Dice.Symbol(hisFirstDice), Game.Dice.Symbol(hisSecondDice), enemyResult
                ));

                int firstDice = Game.Dice.Roll();
                int secondDice = Game.Dice.Roll();
                myResult = firstDice + secondDice;

                diceGame.Add(String.Format(
                    "Вы бросили: {0} + {1} = {2}",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), myResult
                ));

                diceGame.Add(String.Empty);
            }
            while (myResult == enemyResult);

            if (myResult > enemyResult)
            {
                diceGame.Add("BIG|GOOD|ВЫ ВЫИГРАЛИ 5 МОНЕТ:)");
                Character.Protagonist.Coins += 5;
            }
            else
            {
                diceGame.Add("BIG|BAD|ПРОИГРАЛИ 5 МОНЕТ :(");
                Character.Protagonist.Coins -= 5;
            }

            return diceGame;
        }

        public List<string> DiceCheck()
        {
            List<string> diceCheck = new List<string> { };

            int firstDice = Game.Dice.Roll();
            int dicesResult = firstDice;

            string size = (Odd ? String.Empty : "BIG|");

            if (Dices == 1)
                diceCheck.Add(String.Format("{0}На кубикe выпало: {1}", size, Game.Dice.Symbol(firstDice)));
            else
            {
                int secondDice = Game.Dice.Roll();
                dicesResult += secondDice + (Initiative ? Character.Protagonist.Initiative : 0);
                string initLine = (Initiative ? String.Format(" + {0} Инициатива", Character.Protagonist.Initiative) : String.Empty);

                diceCheck.Add(String.Format("{0}На кубиках выпало: {1} + {2}{3} = {4}",
                    size, Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), initLine, dicesResult));
            }

            if (Odd)
                diceCheck.Add(dicesResult % 2 == 0 ? "BIG|ЧЁТНОЕ ЧИСЛО!" : "BIG|НЕЧЁТНОЕ ЧИСЛО!");

            return diceCheck;
        }

        private bool IsHero(string name) => name == Character.Protagonist.Name;

        private Character FindEnemyIn(List<Character> Enemies)
        {
            foreach (Character enemy in Enemies)
                if (enemy.Endurance > 0)
                    return enemy;

            return null;
        }

        private Character FindEnemy(Character fighter, List<Character> Allies, List<Character> Enemies)
        {
            if (Allies.Contains(fighter))
                return FindEnemyIn(Enemies);

            else if (Enemies.Contains(fighter))
                return FindEnemyIn(Allies);

            else
                return null;
        }

        private Dictionary<string, bool> GetSpecialRules(Character attacker, Character defender, int round)
        {
            Dictionary<string, bool> specialRules = new Dictionary<string, bool>();

            specialRules["firstStrike"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.FirstStrike);
            specialRules["powerfulStrike"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.PowerfulStrike);
            specialRules["ignoreReaction"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.IgnoreReaction);
            specialRules["ignoreFirstStrike"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.IgnoreFirstStrike);
            specialRules["poisonBlade"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.PoisonBlade);

            specialRules["enemyFirstStrike"] = defender.SpecialTechnique.Contains(Character.SpecialTechniques.FirstStrike) && (round <= 3);
            specialRules["enemyIgnoreFirstStrike"] = defender.SpecialTechnique.Contains(Character.SpecialTechniques.IgnoreFirstStrike);
            specialRules["enemyIgnorePowerfulStrike"] = defender.SpecialTechnique.Contains(Character.SpecialTechniques.IgnorePowerfulStrike);
            specialRules["enemyReaction"] = defender.SpecialTechnique.Contains(Character.SpecialTechniques.Reaction);
            specialRules["enemyTotalProtection"] = defender.SpecialTechnique.Contains(Character.SpecialTechniques.TotalProtection);

            specialRules["extendedDamage"] = attacker.SpecialTechnique.Contains(Character.SpecialTechniques.ExtendedDamage);

            if (specialRules["ignoreReaction"])
                specialRules["enemyReaction"] = false;

            if (specialRules["ignoreFirstStrike"])
                specialRules["enemyFirstStrike"] = false;

            if (specialRules["enemyIgnoreFirstStrike"])
                specialRules["firstStrike"] = false;

            if (specialRules["enemyIgnorePowerfulStrike"])
                specialRules["powerfulStrike"] = false;

            specialRules["aggressive"] = attacker.FightStyle == Character.FightStyles.Aggressive;
            specialRules["defensive"] = attacker.FightStyle == Character.FightStyles.Defensive;
            specialRules["fullback"] = attacker.FightStyle == Character.FightStyles.Fullback;


            specialRules["aggressiveEnemy"] = defender.FightStyle == Character.FightStyles.Aggressive;
            specialRules["defensiveEnemy"] = defender.FightStyle == Character.FightStyles.Defensive;
            specialRules["fullbackEnemy"] = defender.FightStyle == Character.FightStyles.Fullback;

            return specialRules;
        }

        private Character.FightStyles ChangeFightStyle(string motivation, ref List<string> fight, string direction, Character.FightStyles newFightStyles)
        {
            bool goodDirectionUp = ((direction == "upTo") && ((int)Character.Protagonist.FightStyle < (int)newFightStyles));
            bool goodDirectionDown = ((direction == "downTo") && ((int)Character.Protagonist.FightStyle > (int)newFightStyles));

            if (goodDirectionUp || goodDirectionDown)
            {
                Character.Protagonist.FightStyle = newFightStyles;
                fight.Add(String.Empty);
                fight.Add(String.Format("GRAY|{0} Меняем стиль боя на {1}", motivation, Constants.FightStyles()[newFightStyles]));
            }

            return newFightStyles;
        }

        private Character.FightStyles ChooseFightStyle(ref List<string> fight, Dictionary<string, List<int>> AttackStory, List<Character> Enemies)
        {
            Character.FightStyles newFightStyles = Character.Protagonist.FightStyle;

            if (Character.Protagonist.Endurance < (Character.Protagonist.MaxEndurance / 2))
                return ChangeFightStyle("Дело дрянь!", ref fight, "downTo", Character.FightStyles.Fullback);

            int enemyCount = 0;

            foreach (Character enemy in Enemies)
                if (enemy.Endurance > 0)
                    enemyCount += 1;

            if (enemyCount > 2)
                return ChangeFightStyle("Ох, сколько их набежало!", ref fight, "downTo", Character.FightStyles.Fullback);
            else if (enemyCount > 1)
                return ChangeFightStyle("Надо аккуратно быть их по очереди!", ref fight, "downTo", Character.FightStyles.Defensive);

            List<int> story = AttackStory[Character.Protagonist.Name];

            int fightBalance = 0;
            int storyCount = (story.Count > 3 ? 3 : story.Count);

            for (int i = 1; i <= storyCount; i++)
                fightBalance += story[story.Count - i];

            if (fightBalance < -1)
                return ChangeFightStyle("Дела неважно заладились...", ref fight, "downTo", Character.FightStyles.Defensive);
            else if (fightBalance > 3)
                return ChangeFightStyle("Раздаю люлей!!", ref fight, "upTo", Character.FightStyles.Aggressive);
            else
                return ChangeFightStyle("Надо потихоньку.", ref fight, "upTo", Character.FightStyles.Counterattacking);
        }

        private int InitiativeAndDices(Character character, out string line)
        {
            int firstRoll = Game.Dice.Roll();
            int secondRoll = Game.Dice.Roll();
            int initiative = firstRoll + secondRoll + character.Initiative;

            line = String.Format("{0} + {1} + {2}, итого {3}",
                character.Initiative, Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(secondRoll), initiative
            );

            return initiative;
        }

        private void OutputInitiative(ref List<string> fight, List<Character> FightEnemies, List<Character> FightOrder,
            string iTemplate, string heroLine, string enemyLine, bool reverse = false, bool special = false)
        {
            string fisrtTemplate = (special ? "{0} (особый приём Первый удар)" : iTemplate);

            if (!reverse)
            {
                fight.Add(String.Format(fisrtTemplate, Character.Protagonist.Name, heroLine));
                fight.Add(String.Format(iTemplate, FightEnemies[0].Name, enemyLine));

                FightOrder.Add(FightEnemies[0]);
            }
            else
            {
                fight.Add(String.Format(fisrtTemplate, FightEnemies[0].Name, enemyLine));
                fight.Add(String.Format(iTemplate, Character.Protagonist.Name, heroLine));

                FightOrder.Insert(0, FightEnemies[0]);
            }
        }

        private string Add(bool condition, string line) => condition ? line : String.Empty;

        private int Attack(Character attacker, Character defender, ref List<string> fight, List<Character> Allies,
            ref Dictionary<string, int> WoundsCount, ref Dictionary<string, List<int>> AttackStory, int round, int coherenceIndex,
            out bool reactionSuccess, bool supplAttack = false)
        {
            reactionSuccess = false;

            const int SUCCESSFUL_ATTACK = 3, WOUND = -3, SUCCESSFUL_BLOCK = 1, FAIL_ATTACK = -1;

            int firstRoll = Game.Dice.Roll();
            int secondRoll = Game.Dice.Roll();
            int attackStrength = firstRoll + secondRoll + attacker.Attack;

            Dictionary<string, bool> specialRules = GetSpecialRules(attacker, defender, round);

            int coherence = (coherenceIndex * Coherence);
            bool coherenceBonus = !Allies.Contains(attacker) && (coherenceIndex >= 1) && !specialRules["enemyTotalProtection"];

            if (specialRules["enemyFirstStrike"])
                attackStrength -= 1;

            if (coherenceBonus)
                attackStrength += coherence;

            if (specialRules["aggressive"])
                attackStrength += 1;

            if (specialRules["defensive"] || specialRules["fullback"])
                attackStrength -= 1;

            bool success = false;

            if (specialRules["firstStrike"] && (round == 1))
            {
                fight.Add("Первая атака (особый приём)");
                success = true;
            }
            else
            {
                string bonuses = String.Empty;

                bonuses += Add(specialRules["enemyFirstStrike"], " - 1 за Первую атаку (особый приём) противника");
                bonuses += Add(specialRules["aggressive"], " + 1 за Агрессивный стиль боя");
                bonuses += Add(specialRules["defensive"], " - 1 за Оборонительный стиль боя");
                bonuses += Add(specialRules["fullback"], " - 1 за Глухую оборону");

                fight.Add(
                    String.Format(
                        "Мощность удара: {0} + {1} + {2}{3}{4} = {5}",
                        Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(secondRoll), attacker.Attack, bonuses,
                        (coherenceBonus ? String.Format(" {0} {1} за Слаженность", (coherence > 0 ? "+" : "-"), Math.Abs(coherence)) : String.Empty),
                        attackStrength
                    )
                );

                bool defenceBonus = defender.SpecialTechnique.Contains(Character.SpecialTechniques.TotalProtection);
                int defence = (defender.Defence + (defenceBonus ? 1 : 0) + (specialRules["defensiveEnemy"] ? 1 : 0) + (specialRules["fullbackEnemy"] ? 2 : 0) - (specialRules["aggressiveEnemy"] ? 1 : 0));

                success = attackStrength > defence;

                bonuses = String.Empty;

                bonuses += Add(defenceBonus, " + 1 за Веерную защиту (особый приём)");
                bonuses += Add(specialRules["aggressiveEnemy"], " - 1 за Агрессивный стиль боя");
                bonuses += Add(specialRules["defensiveEnemy"], " + 1 за Оборонительный стиль боя");
                bonuses += Add(specialRules["fullbackEnemy"], " + 2 за Глухую оборону");

                fight.Add(String.Format(
                    "Защита: {0}{1} {2} {3}",  defender.Defence, bonuses, (success ? "это меньше" : "это больше или равно"), attackStrength
                ));
            }

            if (success)
            {
                if (!supplAttack)
                    WoundsCount[defender.Name] += 1;

                if (specialRules["enemyReaction"] && (WoundsCount[defender.Name] == 3))
                {
                    fight.Add(String.Format("{0}|Уклонение от атаки благодаря Реакции (особый приём)", (Allies.Contains(defender) ? "GOOD" : "BAD")));
                    reactionSuccess = true;
                    WoundsCount[defender.Name] = 0;

                    return 0;
                }

                if (specialRules["extendedDamage"] && !supplAttack)
                    defender.Endurance -= 3;
                else if (supplAttack)
                    defender.Endurance -= 1;
                else
                    defender.Endurance -= 2;

                if (specialRules["firstStrike"] && (round == 1))
                {
                    fight.Add("+2 дополнительный урон от Первого удара (особый приём)");
                    defender.Endurance -= 2;
                }

                if (specialRules["powerfulStrike"] && (round < 3))
                {
                    fight.Add("+3 дополнительный урон от Мощного выпада (особый приём)");
                    defender.Endurance -= 3;
                }

                string defenderName = (IsHero(defender.Name) ? "Вы ранены" : String.Format("{0} ранен", defender.Name));

                if (defender.Endurance > 0)
                    defenderName += String.Format(" (осталось жизней: {0})", defender.Endurance);

                fight.Add(String.Format("{0}|{1}", (Allies.Contains(defender) ? "BAD" : "GOOD"), defenderName));

                AttackStory[attacker.Name].Add(SUCCESSFUL_ATTACK);
                AttackStory[defender.Name].Add(WOUND);

                return (Allies.Contains(defender) ? 0 : 1);
            }
            else
            {
                fight.Add(String.Format("{0}|Атака отбита", (Allies.Contains(defender) ? "GOOD" : "BAD")));

                if (specialRules["powerfulStrike"] && (round < 3))
                {
                    fight.Add("Урон всё равно нанесён от Мощного выпада (особый приём)");
                    defender.Endurance -= 3;
                }

                if (specialRules["poisonBlade"] && (defender.MaxEndurance > defender.Endurance))
                {
                    fight.Add("Урон нанесён от яда отравленного клинка");
                    defender.Endurance -= 1;
                }

                AttackStory[attacker.Name].Add(FAIL_ATTACK);
                AttackStory[defender.Name].Add(SUCCESSFUL_BLOCK);

                return 0;
            }
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int iHero, iEnemy, round = 1, enemyWounds = 0;
            string heroLine, enemyLine, iTemplate = "{0} (инициатива {1})";

            List<Character> FightAllies = new List<Character>();
            List<Character> FightEnemies = new List<Character>();
            List<Character> FightAll = new List<Character>();
            List<Character> FightOrder = null;
            Dictionary<string, int> WoundsCount = new Dictionary<string, int>();
            Dictionary<string, List<int>> AttackStory = new Dictionary<string, List<int>>();

            foreach (Character enemy in Enemies)
            {
                Character enemyClone = enemy.Clone();
                FightEnemies.Add(enemyClone);
                FightAll.Add(enemyClone);
            };

            if (Allies == null)
            {
                FightAllies.Add(Character.Protagonist);
                FightAll.Add(Character.Protagonist);
            }
            else
                foreach (Character ally in Allies)
                    if (ally.Name == Character.Protagonist.Name)
                    {
                        FightAllies.Add(Character.Protagonist);
                        FightAll.Add(Character.Protagonist);
                    }
                    else
                    {
                        Character allyClone = ally.Clone();
                        FightAllies.Add(allyClone);
                        FightAll.Add(allyClone);
                    }

            foreach (Character fighter in FightAll)
            {
                WoundsCount[fighter.Name] = 0;
                AttackStory[fighter.Name] = new List<int>();
            }

            fight.Add("ОЧЕРЁДНОСТЬ УДАРОВ:");

            if (GroupFight)
            {
                FightOrder = FightAll.OrderByDescending(o => o.Initiative).ToList();

                foreach (Character fighter in FightOrder)
                    fight.Add(String.Format(iTemplate, fighter.Name, fighter.Initiative));
            }
            else
            {
                bool firstStrike = Character.Protagonist.SpecialTechnique.Contains(Character.SpecialTechniques.FirstStrike);
                bool enemyFirstStrike = FightEnemies[0].SpecialTechnique.Contains(Character.SpecialTechniques.FirstStrike);
                bool enemyIgnoreFirstStrike = FightEnemies[0].SpecialTechnique.Contains(Character.SpecialTechniques.IgnoreFirstStrike);

                FightOrder = new List<Character>();

                do
                {
                    iHero = InitiativeAndDices(Character.Protagonist, out heroLine);
                    iEnemy = InitiativeAndDices(FightEnemies[0], out enemyLine);
                }
                while (iHero == iEnemy);

                FightOrder.Add(Character.Protagonist);

                if (firstStrike && !enemyFirstStrike && !enemyIgnoreFirstStrike)
                    OutputInitiative(ref fight, FightEnemies, FightOrder, iTemplate, heroLine, enemyLine, special: true);

                else if (!firstStrike && enemyFirstStrike)
                    OutputInitiative(ref fight, FightEnemies, FightOrder, iTemplate, heroLine, enemyLine, reverse: true, special: true);

                else if (iHero > iEnemy)
                    OutputInitiative(ref fight, FightEnemies, FightOrder, iTemplate, heroLine, enemyLine);

                else
                    OutputInitiative(ref fight, FightEnemies, FightOrder, iTemplate, heroLine, enemyLine, reverse: true);
            }

            fight.Add(String.Empty);

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                int coherenceIndex = 0;

                Character.Protagonist.FightStyle = ChooseFightStyle(ref fight, AttackStory, FightEnemies);

                foreach (Character fighter in FightOrder)
                {
                    if (fighter.Endurance <= 0)
                        continue;

                    Character enemy = FindEnemy(fighter, FightAllies, FightEnemies);

                    if (enemy == null)
                        continue;

                    fight.Add(String.Empty);

                    if (GroupFight)
                        fight.Add(String.Format("BOLD|{0} выбрает противника для атаки: {1}", fighter.Name, enemy.Name));
                    else
                        fight.Add(String.Format("BOLD|{0} атакует", fighter.Name));

                    enemyWounds += Attack(fighter, enemy, ref fight, FightAllies, ref WoundsCount, ref AttackStory, round,
                        coherenceIndex, out bool reactionSuccess);

                    if (fighter.SpecialTechnique.Contains(Character.SpecialTechniques.TwoBlades))
                    {
                        fight.Add("Дополнительная атака (особый приём):");

                        if (reactionSuccess)
                            fight.Add(String.Format("{0}|Уклонение от атаки благодаря Реакции (особый приём)", (FightAllies.Contains(enemy) ? "GOOD" : "BAD")));
                        else
                            enemyWounds += Attack(fighter, enemy, ref fight, FightAllies, ref WoundsCount, ref AttackStory, round,
                                coherenceIndex, out bool _, supplAttack: true);
                    }

                    if (FightEnemies.Contains(fighter))
                        coherenceIndex += 1;
                }

                bool enemyLost = true;

                foreach (Character e in FightEnemies)
                    if ((e.Endurance > 0) || (e.MaxEndurance == 0))
                        enemyLost = false;

                if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                {
                    fight.Add(String.Empty);
                    fight.Add("BIG|GOOD|ВЫ ПОБЕДИЛИ :)");
                    return fight;
                }

                bool allyLost = true;

                foreach (Character a in FightAllies)
                    if (a.Endurance > 0)
                        allyLost = false;

                if (allyLost)
                {
                    fight.Add(String.Empty);
                    fight.Add("BIG|BAD|ВЫ ПРОИГРАЛИ :(");

                    if (NotToDeath)
                        Character.Protagonist.Endurance += 1;

                    return fight;
                }

                if ((RoundsToWin > 0) && (RoundsToWin <= round))
                {
                    fight.Add(String.Empty);
                    fight.Add("BAD|Отведённые на победу раунды истекли.");
                    fight.Add("BIG|BAD|ВЫ ПРОИГРАЛИ :(");
                    return fight;
                }

                fight.Add(String.Empty);

                round += 1;
            }
        }

        public bool IsHealingEnabled() => Character.Protagonist.Endurance < Character.Protagonist.MaxEndurance;

        public void UseHealing(int healingLevel) => Character.Protagonist.Endurance += healingLevel;

        public string TextByOptions(string option) => String.Empty;
    }
}
