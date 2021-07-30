﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.FaithfulSwordOfTheKing
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();

        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public int SkillPenalty { get; set; }
        public bool WithoutShooting { get; set; }

        public Character.MeritalArts? MeritalArt { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Ловкость: {0}", Character.Protagonist.Skill),
            String.Format("Сила: {0}", Character.Protagonist.Strength),
            String.Format("Честь: {0}", Character.Protagonist.Honor),
            String.Format("День: {0}", Character.Protagonist.Day),
            String.Format("Экю: {0}", ToEcu(Character.Protagonist.Ecu))
        };

        private string ToEcu(int ecu) => String.Format("{0:f2}", (double)ecu / 100).TrimEnd('0').TrimEnd(',').Replace(',', '.');
            
        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            toEndParagraph = 0;
            toEndText = String.Empty;

            if (Character.Protagonist.Strength <= 0)
                toEndText = "Начать сначала";

            else if (Character.Protagonist.Honor <= 0)
            {
                toEndParagraph = 150;
                toEndText = "Задуматься о чести";

                Character.Protagonist.Strength = 0;
            }
            else
                return false;

            return true;
        }
        public override bool IsButtonEnabled()
        {
            bool disabledMeritalArtButton =
                (MeritalArt != Character.MeritalArts.Nope) && (Character.Protagonist.MeritalArt != Character.MeritalArts.Nope);

            bool disabledGetOptions = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (Character.Protagonist.Ecu < Price);

            return !(disabledMeritalArtButton || disabledGetOptions || disabledByPrice);
        }

        public override bool CheckOnlyIf(string option)
        {
            Character hero = Character.Protagonist;

            if (option.Contains("="))
            {
                int value = int.Parse(option.Split('=')[1]);

                if (option.Contains("ДЕНЬ >=") && (value > hero.Day))
                    return false;

                else if (option.Contains("ДЕНЬ =") && (value != hero.Day))
                    return false;

                else if (option.Contains("ДЕНЬ <=") && (value < hero.Day))
                    return false;

                else if (option.Contains("ЭКЮ >=") && (value > hero.Ecu))
                    return false;

                return true;
            }
            else if (Enum.TryParse(option, out Character.MeritalArts value))
                return hero.MeritalArt == value;
            else
                return CheckOnlyIfTrigger(option);
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
                return new List<string> { String.Format("{0}, {1} экю", Text, ToEcu(Price)) };

            if (Name == "Get")
                return new List<string> { Text };

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nловкость {1}  сила {2}", enemy.Name, enemy.Skill, enemy.Strength));

            return enemies;
        }

        public List<string> Luck()
        {
            int luckDice = Game.Dice.Roll();

            bool goodLuck = luckDice % 2 == 0;

            List<string> luckCheck = new List<string> { String.Format("Проверка удачи: {0} - {1}",
                Game.Dice.Symbol(luckDice), (goodLuck ? "чётное" : "нечётное")) };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            return luckCheck;
        }

        public List<string> Skill()
        {
            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            bool goodSkill = (firstDice + secondDice) <= Character.Protagonist.Skill;

            List<string> skillCheck = new List<string> { String.Format("Проверка ловкости: {0} + {1} {2} {3} ловкость",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodSkill ? "<=" : ">"), Character.Protagonist.Skill) };

            skillCheck.Add(goodSkill ? "BIG|GOOD|ЛОВКОСТИ ХВАТИЛО :)" : "BIG|BAD|ЛОВКОСТИ НЕ ХВАТИЛО :(");

            return skillCheck;
        }

        public List<string> DicesDoubles()
        {
            List<string> doubleCheck = new List<string>();

            bool doubleFail = false;

            for (int i = 0; i < 2; i++)
            {
                int firstDice = Game.Dice.Roll();
                int secondDice = Game.Dice.Roll();

                bool fail = firstDice == secondDice;

                doubleCheck.Add(String.Format("Бросок: {0} и {1} - {2}дубль",
                    Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (fail ? String.Empty : "НЕ ")));

                if (fail)
                    doubleFail = true;
            }

            doubleCheck.Add(doubleFail ? "BIG|BAD|ВЫПАЛИ :(" : "BIG|GOOD|НЕ ВЫПАЛИ :)");

            return doubleCheck;
        }

        public List<string> DiceWound()
        {
            List<string> diceWound = new List<string>();

            int wounds = Game.Dice.Roll();

            diceWound.Add(String.Format("Бросок: {0}", Game.Dice.Symbol(wounds)));

            if (wounds < 6)
            {
                Character.Protagonist.Strength -= wounds;
                diceWound.Add(String.Format("BIG|BAD|Вы потеряли сил: {0}", wounds));
            }
            else
                diceWound.Add("BIG|BAD|Выпала шестёрка :(");
 
            return diceWound;
        }

        public List<string> DiceDoubleWound()
        {
            List<string> diceCheck = new List<string> { };

            int dices = 0;

            for (int i = 1; i <= 2; i++)
            {
                int dice = Game.Dice.Roll();
                dices += dice;
                diceCheck.Add(String.Format("На {0} выпало: {1}", i, Game.Dice.Symbol(dice)));
            }

            Character.Protagonist.Strength -= dices;

            diceCheck.Add(String.Format("BIG|BAD|Вы потеряли жизней: {0}", dices));

            return diceCheck;
        }

        public List<string> FortunetellersAmbush()
        {
            List<string> ambush = new List<string>();

            int firstDice = Game.Dice.Roll();
            int secondDice = Game.Dice.Roll();

            int result = firstDice + secondDice;
            bool evenNumber = result % 2 == 0;

            ambush.Add(String.Format("Кубики: {0} + {1} = {2}", Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), result));

            if (firstDice == secondDice)
                ambush.Add("BIG|BAD|Выпали одинаковые числа :(");
            
            else if (evenNumber)
                ambush.Add("BIG|GOOD|Получилось четное число :)");

            else
                ambush.Add("BIG|BAD|Получилось нечетное число :(");

            return ambush;
        }

        public List<string> Pursuit()
        {
            List<string> pursuit = new List<string>();

            int threeTimesInRow = 0, theyWin = 0;
            
            for (int i = 1; i < 12; i++)
            {
                int heroSpeed = Game.Dice.Roll();
                int enemiesSpeed = Game.Dice.Roll();

                pursuit.Add(String.Format("Ваша скорость: {0}  <-->  Их скорость: {1}",
                    Game.Dice.Symbol(heroSpeed), Game.Dice.Symbol(enemiesSpeed)));

                if (heroSpeed > enemiesSpeed)
                {
                    pursuit.Add("GOOD|Вы быстрее");
                    threeTimesInRow = 0;
                }
                else
                {
                    pursuit.Add("BAD|Они быстрее");
                    threeTimesInRow += 1;
                    theyWin += 1;

                    if (threeTimesInRow >= 3)
                    {
                        pursuit.Add(String.Empty);
                        pursuit.Add("BIG|BAD|Они без труда догнали вас :(");

                        return pursuit;
                    }
                }
            }

            pursuit.Add(String.Empty);
            pursuit.Add(theyWin > 6 ? "BIG|BAD|Они догнали вас :(" : "BIG|GOOD|Они не догнали вас :)");

            return pursuit;
        }

        public List<string> Get()
        {
            Character hero = Character.Protagonist;

            if ((MeritalArt != Character.MeritalArts.Nope) && (hero.MeritalArt == Character.MeritalArts.Nope))
                hero.MeritalArt = MeritalArt ?? Character.MeritalArts.Nope;

            else if ((Price > 0) && (hero.Ecu >= Price))
            {
                hero.Ecu -= Price;

                if (!Multiple)
                    Used = true;

                if (BenefitList != null)
                {
                    foreach (Modification modification in BenefitList)
                        modification.Do();
                }
            }

            return new List<string> { "RELOAD" };
        }

        private bool NoMoreEnemies(List<Character> enemies) => enemies.Where(x => x.Strength > 0).Count() == 0;

        private bool LuckyHit(int? roll = null) => (roll ?? Game.Dice.Roll()) % 2 == 0;

        private bool EnemyWound(Character hero, ref Character enemy, List<Character> FightEnemies,
            int roll, int WoundsToWin, ref int enemyWounds, ref List<string> fight, bool dagger = false)
        {
            enemy.Strength -= ((hero.MeritalArt == Character.MeritalArts.SwordAndDagger) && LuckyHit(roll) && !dagger ? 3 : 2);

            enemyWounds += 1;

            bool enemyLost = NoMoreEnemies(FightEnemies);

            if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
            {
                fight.Add(String.Empty);
                fight.Add(String.Format("BIG|GOOD|Вы ПОБЕДИЛИ :)"));
                return true;
            }
            else
                return false;
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            int round = 1, enemyWounds = 0, shoots = 0;

            Character hero = Character.Protagonist;

            if (WithoutShooting)
                shoots = 0;
            else if ((hero.MeritalArt == Character.MeritalArts.TwoPistols) && (hero.Pistols > 1) && (hero.BulletsAndGubpowder > 1))
                shoots = 2;
            else if ((hero.Pistols > 0) && (hero.BulletsAndGubpowder > 0))
                shoots = 1;

            for (int pistol = 1; pistol <= shoots; pistol++)
            {
                if (NoMoreEnemies(FightEnemies))
                    continue;

                bool hit = LuckyHit();

                hero.BulletsAndGubpowder -= 1;

                fight.Add(String.Format("Выстрел из {0}пистолета: {1}", (shoots > 1 ? String.Format("{0} ", pistol) : ""), (hit ? "попал" : "промах")));

                if (hit)
                {
                    foreach (Character enemy in FightEnemies.Where(x => x.Strength > 0))
                    {
                        fight.Add(String.Format("GOOD|{0} убит", enemy.Name));
                        enemy.Strength = 0;
                        break;
                    }
                }
            }

            if (NoMoreEnemies(FightEnemies))
                return fight;

            if (shoots > 0)
                fight.Add(String.Empty);

            while (true)
            {
                fight.Add(String.Format("HEAD|Раунд: {0}", round));

                if ((hero.MeritalArt == Character.MeritalArts.SecretBlow) && (round == 1))
                {
                    Character enemy = FightEnemies.Where(x => x.Strength > 0).FirstOrDefault();

                    enemy.Strength -= 4;
                    fight.Add(String.Format("Тайный удар шпагой: {0} теряет 4 силы, у него осталось {1}", enemy.Name, enemy.Strength));
                    fight.Add(String.Empty);
                }

                bool attackAlready = false;
                int protagonistHitStrength = 0, protagonistRoll = 0;

                foreach (Character enemy in FightEnemies)
                {
                    if (enemy.Strength <= 0)
                        continue;

                    Character enemyInFight = enemy;
                    fight.Add(String.Format("{0} (сила {1})", enemy.Name, enemy.Strength));

                    if (!attackAlready)
                    {
                        protagonistRoll = Game.Dice.Roll();
                        int protagonistSkill = hero.Skill - SkillPenalty - (hero.MeritalArt == Character.MeritalArts.LefthandFencing ? 0 : enemy.LeftHandPenalty);
                        protagonistHitStrength = (protagonistRoll * 2) + protagonistSkill;

                        fight.Add(String.Format("Мощность вашего удара: {0} x 2 + {1} = {2}",
                            Game.Dice.Symbol(protagonistRoll), protagonistSkill, protagonistHitStrength
                        ));
                    }

                    int enemyRoll = Game.Dice.Roll();
                    int enemyHitStrength = (enemyRoll * 2) + enemy.Skill;

                    fight.Add(String.Format("Мощность его удара: {0} x 2 + {1} = {2}",
                        Game.Dice.Symbol(enemyRoll), enemy.Skill, enemyHitStrength
                    ));

                    if ((protagonistHitStrength > enemyHitStrength) && !attackAlready)
                    {
                        if ((enemy.Chainmail > 0) && (protagonistRoll == 3))
                            fight.Add(String.Format("BOLD|Кольчуга отразила удар!"));
                        else
                        {
                            fight.Add(String.Format("GOOD|{0} ранен", enemy.Name));

                            if (EnemyWound(hero, ref enemyInFight, FightEnemies, protagonistRoll, WoundsToWin, ref enemyWounds, ref fight))
                                return fight;
                        }

                    }
                    else if (protagonistHitStrength > enemyHitStrength)
                    {
                        fight.Add(String.Format("BOLD|{0} не смог вас ранить", enemy.Name));
                    }
                    else if (protagonistHitStrength < enemyHitStrength)
                    {
                        if ((hero.Chainmail > 0) && (enemyRoll == 6))
                            fight.Add(String.Format("BOLD|Кольчуга отразила удар!"));
                        else
                        {
                            fight.Add(String.Format("BAD|{0} ранил вас", enemy.Name));
                            hero.Strength -= 2;

                            if (hero.Strength <= 0)
                            {
                                fight.Add(String.Empty);
                                fight.Add(String.Format("BIG|BAD|Вы ПРОИГРАЛИ :("));
                                return fight;
                            }

                            if ((hero.MeritalArt == Character.MeritalArts.SwordAndDagger) && LuckyHit(protagonistRoll))
                            {
                                fight.Add(String.Format("GOOD|{0} ранен вашим кинжалом", enemy.Name));

                                if (EnemyWound(hero, ref enemyInFight, FightEnemies, protagonistRoll, WoundsToWin, ref enemyWounds, ref fight, dagger: true))
                                    return fight;
                            }
                        }
                    }
                    else
                        fight.Add(String.Format("BOLD|Ничья в раунде"));

                    attackAlready = true;

                    if ((RoundsToWin > 0) && (RoundsToWin <= round))
                    {
                        fight.Add(String.Empty);
                        fight.Add("BAD|Отведённые на победу раунды истекли.");
                        fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                        return fight;
                    }

                    fight.Add(String.Empty);
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() => Character.Protagonist.Strength < Character.Protagonist.MaxStrength;

        public override void UseHealing(int healingLevel)
        {
            Character.Protagonist.Strength += healingLevel;
            Character.Protagonist.HadFoodToday += 1;
        }
    }
}
