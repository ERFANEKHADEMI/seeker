﻿using System;
using System.Collections.Generic;
using System.Linq;
using static Seeker.Gamebook.YounglingTournament.Character;

namespace Seeker.Gamebook.YounglingTournament
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public string Enemy { get; set; }
        public int HeroHitpointsLimith { get; set; }
        public int EnemyHitpointsLimith { get; set; }
        public int HeroRoundWin { get; set; }
        public int EnemyRoundWin { get; set; }
        public bool SpeedActivate { get; set; }
        public bool WithoutTechnique { get; set; }
        public bool NoStrikeBack { get; set; }
        public int EnemyHitpointsPenalty { get; set; }
        public string BonusTechnique { get; set; }

        public int AccuracyBonus { get; set; }
        public int Level { get; set; }
        

        public override List<string> Status() => new List<string>
        {
            $"Cветлая сторона: {protagonist.LightSide}",
            $"Тёмная сторона: {protagonist.DarkSide}",
        };

        public override List<string> AdditionalStatus()
        {
            List<string> newStatuses = new List<string>();

            newStatuses.Add(String.Format("Выносливость: {0}/{1}", protagonist.Hitpoints, protagonist.MaxHitpoints));

            if (protagonist.SecondPart == 0)
            {
                newStatuses.Add(String.Format("Взлом: {0}", protagonist.Hacking));
                newStatuses.Add(String.Format("Пилот: {0}", protagonist.Pilot));
                newStatuses.Add(String.Format("Меткость: {0}", protagonist.Accuracy));
            }
            else
            {
                if ((protagonist.Thrust > 0) || (protagonist.EnemyThrust > 0))
                    newStatuses.Add(String.Format("Уколов: {0} vs {1}", protagonist.Thrust, protagonist.EnemyThrust));

                newStatuses.Add(String.Format("Понимание Силы: {0}", protagonist.ForceTechniques.Values.Sum()));
                newStatuses.Add(String.Format("Форма {0}", Services.GetSwordSkillName(Services.GetSwordType())));
            }

            return newStatuses;
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
                    bool thisIsTechnique = Enum.TryParse(oneOption, out Character.ForcesTypes techniqueType);

                    if (thisIsTechnique && (protagonist.ForceTechniques[techniqueType] == 0))
                        return false;

                    else if (oneOption.Contains(">") || oneOption.Contains("<"))
                    {
                        int level = Game.Services.LevelParse(option);

                        if (oneOption.Contains("ПИЛОТ >") && (level >= protagonist.Pilot))
                            return false;

                        if (oneOption.Contains("БИБЛИОТЕКА <=") && (level < protagonist.Reading))
                            return false;

                        if (oneOption.Contains("УКОЛОВ >") && (level >= protagonist.Thrust))
                            return false;

                        if (oneOption.Contains("УКОЛОВ У ВРАГА >") && (level >= protagonist.EnemyThrust))
                            return false;
                    }

                    else if (oneOption.Contains("!"))
                    {
                        if (Game.Option.IsTriggered(oneOption.Replace("!", String.Empty).Trim()))
                            return false;
                    }
                    else if (!Game.Option.IsTriggered(oneOption.Trim()))
                        return false;
                }

                return true;
            }
        }

        public override List<string> Representer()
        {
            if (Level > 0)
                return new List<string> { String.Format("Пройдите проверку Понимания Силы, сложностью {0}", Level) };

            List<string> enemies = new List<string>();

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
            {
                string accuracy = (enemy.Accuracy > 0 ? String.Format("  меткость {0}  ", enemy.Accuracy) : String.Empty);
                string firepower = (enemy.Firepower > 5 ? String.Format("  сила выстрела {0}", enemy.Firepower) : String.Empty);
                string shield = (enemy.Shield > 0 ? String.Format("  энергощит {0}", enemy.Shield) : String.Empty);
                string skill = (enemy.Skill > 0 ? String.Format("  ловкость {0}", enemy.Skill) : String.Empty);
                string technique = String.Empty, noStrikeBack = String.Empty;

                if (enemy.Rang > 0)
                {
                    bool anotherTechnique = Enum.TryParse(enemy.SwordTechnique, out SwordTypes currectSwordTechniques);

                    if (!anotherTechnique)
                        currectSwordTechniques = SwordTypes.Rivalry;

                    technique = String.Format("\nиспользует Форму {0}",
                        Services.GetSwordSkillName(currectSwordTechniques, rang: enemy.Rang));
                }

                if (NoStrikeBack)
                    noStrikeBack = "\nзнает защиту от Встречного удара";

                enemies.Add(String.Format("{0}\n{1}выносливость {2}{3}{4}{5}{6}{7}",
                    enemy.Name, accuracy, enemy.GetHitpoints(EnemyHitpointsPenalty),
                    firepower, shield, skill, technique, noStrikeBack));
            }

            return enemies;
        }

        public List<string> DiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            diceCheck.Add(String.Format("На кубике выпало: {0}", Game.Dice.Symbol(dice)));

            protagonist.Hitpoints -= dice;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dice));

            return diceCheck;
        }

        public List<string> EnemyDiceWounds()
        {
            List<string> diceCheck = new List<string> { };

            int dice = Game.Dice.Roll();

            int bonus = 0;
            string bonusLine = String.Empty;
            string[] enemy = Enemy.Split(',');

            bool withBonus = Enum.TryParse(BonusTechnique, out Character.ForcesTypes techniqueType);
            
            if (withBonus)
            {
                bonus = protagonist.ForceTechniques[techniqueType];
                bonusLine = String.Format(" + {0} за ранг", bonus);
            }

            diceCheck.Add(String.Format("На кубике выпало: {0}{1}", Game.Dice.Symbol(dice), bonusLine));

            dice += bonus;

            Character.SetHitpoints(enemy[0], dice, int.Parse(enemy[1]));

            diceCheck.Add(String.Format("BIG|GOOD|{0} потерял жизней: {1}", enemy[0], dice));

            return diceCheck;
        }

        public List<string> ForceTest()
        {
            List<string> test = new List<string>();

            int testDice = Game.Dice.Roll();
            int forceLevel = protagonist.ForceTechniques.Values.Sum();
            bool testPassed = testDice + forceLevel >= Level;

            test.Add(String.Format("Проверка Понимания: {0} + {1} {2} {3}",
                Game.Dice.Symbol(testDice), forceLevel, (testPassed ? ">=" : "<"), Level));

            test.Add(Result(testPassed, "ПРОВЕРКА ПРОЙДЕНА|ПРОВЕРКА ПРОВАЛЕНА"));

            return test;
        }

        public List<string> MixedFightAttack()
        {
            List<string> attackCheck = new List<string> { };

            int deflecting = 4 + protagonist.SwordTechniques[SwordTypes.Rivalry];

            attackCheck.Add("Выстрел: 10 (сила выстрела) x 9 (меткость) = 90");

            attackCheck.Add(String.Format("Отражение: 4 + {0} ранг = {1}",
                protagonist.SwordTechniques[SwordTypes.Rivalry], deflecting));

            int result = 90 / deflecting;

            attackCheck.Add(String.Format("Результат: 90 выстрел / {0} отражение = {1}", deflecting, result));

            if (result > 0)
            {
                protagonist.Hitpoints -= result;
                attackCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", result));
            }
            else
                attackCheck.Add("BIG|GOOD|Вам удалось отразить выстрел противника в него самого!");

            return attackCheck;
        }

        public List<string> MixedFightDefence()
        {
            List<string> defenseCheck = new List<string> { };

            int deflecting = 4 + protagonist.SwordTechniques[SwordTypes.Rivalry];

            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);
            int shoot = firstDice + secondDice + 19;

            defenseCheck.Add(String.Format("Выстрел: {0} + {1} + 10 (сила выстрела) + 9 (меткость) = {2}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), shoot));

            defenseCheck.Add(String.Format("Отражение: 4 + {0} ранг = {1}",
                protagonist.SwordTechniques[SwordTypes.Rivalry], deflecting));

            int result = shoot / deflecting;

            defenseCheck.Add(String.Format("Результат: {0} выстрел / {1} отражение = {2}",
                shoot, deflecting, result));

            protagonist.Hitpoints -= result;

            defenseCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", result));

            return defenseCheck;
        }

        public List<string> FireFight()
        {
            List<string> fight = new List<string>();

            Dictionary<Character, int> FightEnemies = new Dictionary<Character, int>();
            List<Character> EnemiesList = new List<Character>();

            foreach (Character enemy in Enemies)
            {
                Character newEnemy = enemy.Clone();
                FightEnemies.Add(newEnemy, 0);
                EnemiesList.Add(newEnemy);
            }
                
            int round = 1;

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                Game.Dice.DoubleRoll(out int protagonistFirstDice, out int protagonistSecondDice);
                int shotAccuracy = protagonist.Accuracy + protagonistFirstDice + protagonistSecondDice + AccuracyBonus;

                string bonus = (AccuracyBonus > 0 ? String.Format(" + {0} бонус", AccuracyBonus) : String.Empty);

                fight.Add(String.Format("Ваш выстрел: {0} меткость{1} + {2} + {3} = {4}",
                    protagonist.Accuracy, bonus, Game.Dice.Symbol(protagonistFirstDice),
                    Game.Dice.Symbol(protagonistSecondDice), shotAccuracy));

                foreach (Character enemy in EnemiesList)
                {
                    if (enemy.Hitpoints <= 0)
                        FightEnemies[enemy] = -1;

                    else
                    {
                        Game.Dice.DoubleRoll(out int enemyFirstDice, out int enemySecondDice);
                        FightEnemies[enemy] = enemy.Accuracy + enemyFirstDice + enemySecondDice;

                        fight.Add(String.Format("{0} стреляет: {1} + {2} + {3} = {4}",
                            enemy.Name, enemy.Accuracy, Game.Dice.Symbol(enemyFirstDice),
                            Game.Dice.Symbol(enemySecondDice), FightEnemies[enemy]));
                    }
                }

                bool protaganistMakeShoot = false;

                foreach (KeyValuePair<Character, int> shooter in FightEnemies.OrderBy(x => x.Value))
                {
                    if (shooter.Value <= 0)
                        continue;

                    else if ((shooter.Value < shotAccuracy) && !protaganistMakeShoot)
                    {
                        protaganistMakeShoot = true;

                        if (shooter.Key.Shield > 0)
                        {
                            int damage = (protagonist.Firepower - shooter.Key.Shield);

                            if (damage <= 0)
                            {
                                fight.Add(String.Format("GOOD|Вы подстрелили {0}, но его энергощит полностью поглотил урон", shooter.Key.Name));

                                shooter.Key.Shield -= protagonist.Firepower;
                            }
                            else
                            {
                                fight.Add(String.Format("GOOD|Вы подстрелили {0}, его энергощит поглотил {1} ед.урона, " +
                                    "в результате он потерял {2} ед.выносливости", shooter.Key.Name, shooter.Key.Shield, damage));

                                shooter.Key.Hitpoints -= damage;
                                shooter.Key.Shield = 0;
                            }
                        }
                        else
                        {
                            shooter.Key.Hitpoints -= protagonist.Firepower;
                            fight.Add(String.Format("GOOD|Вы подстрелили {0}, он потерял {1} ед.выносливости",
                                shooter.Key.Name, protagonist.Firepower));
                        }
                    }

                    else if (shooter.Value > shotAccuracy)
                    {
                        protagonist.Hitpoints -= shooter.Key.Firepower;
                        fight.Add(String.Format("BAD|{0} подстрелил вас, вы потерял {1} ед.выносливости (осталось {2})",
                            shooter.Key.Name, shooter.Key.Firepower, protagonist.Hitpoints));
                    }
                }

                fight.Add(String.Empty);

                if (protagonist.Hitpoints <= 0)
                {
                    fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                    return fight;
                }

                if (FightEnemies.Keys.Where(x => x.Hitpoints > 0).Count() == 0)
                {
                    fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                    return fight;
                }

                round += 1;
            }
        }

        public List<string> SwordFight()
        {
            List<string> fight = new List<string>();

            Dictionary<Character, int> FightEnemies = new Dictionary<Character, int>();
            List<Character> EnemiesList = new List<Character>();

            foreach (Character enemy in Enemies)
            {
                Character newEnemy = enemy.Clone().SetHitpoints(EnemyHitpointsPenalty);
                FightEnemies.Add(newEnemy, 0);
                EnemiesList.Add(newEnemy);
            }

            SwordTypes currectSwordTechniques = Services.GetSwordType();
            fight.Add(String.Format("Вы выбрали для боя Форму {0}", Services.GetSwordSkillName(currectSwordTechniques)));

            int skill = Services.SwordSkills(currectSwordTechniques, out string detail);
            fight.Add(String.Format("Ваша Ловкость в этом бою: {0} (по формуле: {1})", skill, detail));

            fight.Add(String.Empty);

            int round = 1, heroRoundWin = 0, enemyRoundWin = 0;
            bool speedActivate = false, irresistibleAttack = false, rapidAttack = false;
            bool strikeBack = NoStrikeBack;

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

                if (Services.UseForcesInFight(ref fight, ref speedActivate, EnemiesList, SpeedActivate, WithoutTechnique))
                {
                    int enemyLimit = (EnemyHitpointsLimith > 0 ? EnemyHitpointsLimith : 0);

                    if ((FightEnemies.Keys.Where(x => x.Hitpoints > enemyLimit).Count() == 0))
                    {
                        fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");
                        return fight;
                    }
                }

                if (Game.Option.IsTriggered("Скоростная атака") && (round > 3) && !rapidAttack)
                {
                    Character target = EnemiesList.Where(x => x.Hitpoints > 0).FirstOrDefault();

                    irresistibleAttack = Services.AdditionalAttack(ref fight, target,
                        "Вы проводите Скоростную атаку!", "Урон от атаки");
                }                  

                int protagonistFirstDice = Game.Dice.Roll();
                int protagonistSecondDice = Game.Dice.Roll();
                int hitSkill = skill + protagonist.SwordTechniques[currectSwordTechniques] + protagonistFirstDice + protagonistSecondDice;             

                fight.Add(String.Format("Ваша скорость удара: {0} ловкость + {1} ранг + {2} + {3} = {4}",
                    skill, protagonist.SwordTechniques[currectSwordTechniques], Game.Dice.Symbol(protagonistFirstDice),
                    Game.Dice.Symbol(protagonistSecondDice), hitSkill));

                foreach (Character enemy in EnemiesList)
                {
                    if (enemy.Hitpoints <= 0)
                        FightEnemies[enemy] = -1;

                    else
                    {
                        Game.Dice.DoubleRoll(out int enemyFirstDice, out int enemySecondDice);
                        FightEnemies[enemy] = enemy.Skill + enemy.Rang + enemyFirstDice + enemySecondDice;

                        fight.Add(String.Format("Скорость удара {0}: {1} ловкость + {2} ранг + {3} + {4} = {5}",
                            enemy.Name, enemy.Skill, enemy.Rang, Game.Dice.Symbol(enemyFirstDice),
                            Game.Dice.Symbol(enemySecondDice), FightEnemies[enemy]));
                    }
                }

                bool protaganistMakeHit = false;

                foreach (KeyValuePair<Character, int> enemy in FightEnemies.OrderBy(x => x.Value))
                {
                    if (enemy.Value <= 0)
                        continue;

                    else if ((enemy.Value < hitSkill) && !protaganistMakeHit)
                    {
                        protaganistMakeHit = true;

                        enemy.Key.Hitpoints -= 3;
                        heroRoundWin += 1;

                        fight.Add(String.Format("GOOD|Вы ранили {0}, он потерял 3 ед.выносливости (осталось {1})",
                            enemy.Key.Name, enemy.Key.Hitpoints));

                        if ((heroRoundWin >= 3) && !irresistibleAttack && Game.Option.IsTriggered("Неотразимая атака"))
                        {
                            irresistibleAttack = Services.AdditionalAttack(ref fight, enemy.Key,
                                "Вы проводите Неотразимую атаку!", "Урон от атаки");
                        }
                    }

                    else if (enemy.Value > hitSkill)
                    {
                        protagonist.Hitpoints -= 3;
                        enemyRoundWin += 1;

                        fight.Add(String.Format("BAD|{0} ранил вас, вы потеряли 3 ед.выносливости (осталось {1})",
                            enemy.Key.Name, protagonist.Hitpoints));

                        if ((enemyRoundWin >= 3) && !strikeBack && Game.Option.IsTriggered("Встречный удар"))
                        {
                            strikeBack = Services.AdditionalAttack(ref fight, enemy.Key,
                                "Вы проводите Встречный удар!", "Урон от удара");
                        }
                    }
                    else
                        fight.Add("BOLD|Вы парировали удары друг друга");
                }

                if (speedActivate)
                {
                    Services.SpeedFightHitpointsLoss(ref fight, protagonist);

                    foreach (Character enemy in EnemiesList.Where(x => x.Hitpoints > 0))
                        Services.SpeedFightHitpointsLoss(ref fight, enemy);
                }

                fight.Add(String.Empty);

                bool enemyRound = (EnemyRoundWin > 0) && (enemyRoundWin >= EnemyRoundWin);
                int hitpointsLimit = (HeroHitpointsLimith > 0 ? HeroHitpointsLimith : 0);

                if ((protagonist.Hitpoints <= hitpointsLimit) || enemyRound)
                {
                    if (enemyRound)
                        fight.Add(String.Format("BIG|BAD|Вы проиграли {0} раунда :(", enemyRoundWin));
                    else
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");

                    return fight;
                }

                bool heroRound = (HeroRoundWin > 0) && (heroRoundWin >= HeroRoundWin);
                hitpointsLimit = (EnemyHitpointsLimith > 0 ? EnemyHitpointsLimith : 0);

                if ((FightEnemies.Keys.Where(x => x.Hitpoints > hitpointsLimit).Count() == 0) || heroRound)
                {
                    if (heroRound)
                        fight.Add(String.Format("BIG|GOOD|Вы выиграли {0} раундов :)", heroRoundWin));
                    else
                        fight.Add("BIG|GOOD|Вы ПОБЕДИЛИ :)");

                    return fight;
                }

                round += 1;
            }
        }
    }
}
