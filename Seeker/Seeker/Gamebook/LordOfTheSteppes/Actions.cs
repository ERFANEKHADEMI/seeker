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
        public Character.SpecialTechniques? SpecialTechnique { get; set; }

        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public bool GroupFight { get; set; }


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

            if (!String.IsNullOrEmpty(Text))
                return new List<string> { Text };
            else if (Enemies == null)
                return enemies;

            if ((Allies != null) && GroupFight)
            {
                foreach (Character ally in Allies)
                    if (ally.Name == Character.Protagonist.Name)
                        enemies.Add(String.Format(
                            "ВЫ\nатака {0}  защита {1}  жизнь {2}  инициатива {3}",
                            Character.Protagonist.Attack, Character.Protagonist.Defence,
                            Character.Protagonist.Endurance, Character.Protagonist.Initiative
                        ));
                    else
                        enemies.Add(String.Format(
                            "{0}\nатака {1}  защита {2}  жизнь {3}  инициатива {4}",
                            ally.Name, ally.Attack, ally.Defence, ally.Endurance, ally.Initiative
                        ));

                enemies.Add("SPLITTER|против");
            }

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format(
                    "{0}\nатака {1}  защита {2}  жизнь {3}  инициатива {4}",
                    enemy.Name, enemy.Attack, enemy.Defence, enemy.Endurance, enemy.Initiative
                ));

            return enemies;
        }

        public List<string> Status()
        {
            List<string> statusLines = new List<string>
            {
                String.Format("Нападение: {0}", Character.Protagonist.Attack),
                String.Format("Защита: {0}", Character.Protagonist.Defence),
                String.Format("Жизнь: {0}", Character.Protagonist.Endurance),
                String.Format("Инициатива: {0}", Character.Protagonist.Initiative)
            };

            return statusLines;
        }

        public List<string> AdditionalStatus() => null;

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
            bool disabledSpecialTechniqueButton = (SpecialTechnique != Character.SpecialTechniques.Nope) &&
                (Character.Protagonist.SpecialTechnique != Character.SpecialTechniques.Nope);

            bool disabledStatBonuses = (!String.IsNullOrEmpty(Stat)) && (Character.Protagonist.Bonuses <= 0);

            return !(disabledSpecialTechniqueButton || disabledStatBonuses);
        }

        public static bool CheckOnlyIf(string option) => true;

        public List<string> Get()
        {
            if ((SpecialTechnique != Character.SpecialTechniques.Nope) && (Character.Protagonist.SpecialTechnique == Character.SpecialTechniques.Nope))
                Character.Protagonist.SpecialTechnique = SpecialTechnique ?? Character.SpecialTechniques.Nope;

            else if ((StatStep > 0) && (Character.Protagonist.Bonuses >= 0))
            {
                int currentStat = (int)Character.Protagonist.GetType().GetProperty(Stat).GetValue(Character.Protagonist, null);

                currentStat += StatStep;

                Character.Protagonist.GetType().GetProperty("Max" + Stat).SetValue(Character.Protagonist, currentStat);
                Character.Protagonist.GetType().GetProperty(Stat).SetValue(Character.Protagonist, currentStat);

                Character.Protagonist.Bonuses -= 1;
            }

            return new List<string> { "RELOAD" };
        }

        private bool IsHero(string name) => name == "ГЛАВГЕРОЙ";

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1;
            int enemyWounds = 0;

            List<Character> FightAllies = new List<Character>();
            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            if (Allies == null)
                FightAllies.Add(Character.Protagonist);
            else
                foreach (Character ally in Allies)
                    if (ally.Name == Character.Protagonist.Name)
                        FightAllies.Add(Character.Protagonist);
                    else
                        FightAllies.Add(ally.Clone());

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                foreach (Character ally in FightAllies)
                {
                    if (ally.Endurance <= 0)
                        continue;

                    if (GroupFight)
                        fight.Add(String.Format("{0} (жизнь {1})", (IsHero(ally.Name) ? "Вы" : ally.Name), ally.Endurance));

                    bool attackAlready = false;
                    int allyHitStrength = 0;
                    int firstAllyRoll = 0;
                    int secondAllyRoll = 0;

                    foreach (Character enemy in FightEnemies)
                    {
                        if (enemy.Endurance <= 0)
                            continue;

                        fight.Add(String.Format("{0} (жизнь {1})", enemy.Name, enemy.Endurance));

                        if (!attackAlready)
                        {
                            
                        }

                        int firstEnemyRoll = Game.Dice.Roll();
                        int secondEnemyRoll = Game.Dice.Roll();
                        int enemyHitStrength = firstEnemyRoll + secondEnemyRoll + enemy.Attack;

                        fight.Add(
                            String.Format(
                                "{0} мощность удара: {1} + {2} + {3} = {4}",
                                (GroupFight ? String.Format("{0} -", enemy.Name) : "Его"),
                                Game.Dice.Symbol(firstEnemyRoll), Game.Dice.Symbol(secondEnemyRoll), enemy.Attack, enemyHitStrength
                            )
                        );

                        if ((allyHitStrength > enemyHitStrength) && !attackAlready)
                        {
                            fight.Add(String.Format("GOOD|{0} ранен", (GroupFight ? enemy.Name : "Он")));
                            enemy.Endurance -= 2 + ally.ExtendedDamage;

                            if (enemy.Endurance <= 0)
                                enemy.Endurance = 0;

                            enemyWounds += 1;

                            bool enemyLost = true;

                            foreach (Character e in FightEnemies)
                                if (e.Endurance > 0)
                                    enemyLost = false;

                            if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                            {
                                fight.Add(String.Empty);
                                fight.Add(String.Format("BIG|GOOD|{0} :)", (GroupFight && !IsHero(ally.Name) ? ally.Name + " ПОБЕДИЛ" : "ВЫ ПОБЕДИЛИ")));
                                return fight;
                            }
                        }
                        else if (allyHitStrength > enemyHitStrength)
                        {
                            fight.Add(String.Format("BOLD|{0} не смог ранить", enemy.Name));
                        }
                        else if (allyHitStrength < enemyHitStrength)
                        {
                            fight.Add(GroupFight && !IsHero(ally.Name) ? String.Format("BAD|{0} ранен", ally.Name) : "BAD|Вы ранены");
                            ally.Endurance -= 2 + enemy.ExtendedDamage;

                            if (ally.Endurance < 0)
                                ally.Endurance = 0;

                            bool allyLost = true;

                            foreach (Character a in FightAllies)
                                if (a.Endurance > 0)
                                    allyLost = false;

                            if (allyLost)
                            {
                                fight.Add(String.Empty);
                                fight.Add(String.Format("BIG|BAD|{0} :(", (IsHero(ally.Name) ? "ВЫ ПРОИГРАЛИ" : String.Format("{0} ПРОИГРАЛ", ally.Name))));
                                return fight;
                            }
                        }
                        else
                            fight.Add(String.Format("BOLD|Ничья в раунде"));

                        attackAlready = true;

                        if ((RoundsToWin > 0) && (RoundsToWin <= round))
                        {
                            fight.Add(String.Empty);
                            fight.Add(String.Format("BAD|Отведённые на победу раунды истекли.", RoundsToWin));
                            fight.Add(String.Format("BIG|BAD|{0} :(", (IsHero(ally.Name) ? "ВЫ ПРОИГРАЛИ" : String.Format("{0} ПРОИГРАЛ", ally.Name))));
                            return fight;
                        }

                        fight.Add(String.Empty);
                    }
                }

                round += 1;
            }
        }
    }
}
