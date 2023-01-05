﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.StrikeBack
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Allies { get; set; }
        public List<Character> Enemies { get; set; }
        public List<Character.SpecialTechniques> SpecialTechniques { get; set; }

        public bool GroupFight { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public bool NotToDeath { get; set; }
        public int Count { get; set; }
        public bool WoundsByDices { get; set; }
        public int WoundsMultiple { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Атака: {0}", protagonist.Attack),
            String.Format("Защита: {0}", protagonist.Defence),
            String.Format("Выносливость: {0}/{1}", protagonist.Endurance, protagonist.MaxEndurance),
        };

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if ((Type != "Fight") && String.IsNullOrEmpty(Text))
            {
                return enemies;
            }
            else if (Type != "Fight")
            {
                return new List<string> { Text };
            }

            if ((Allies != null) && GroupFight)
            {
                if (!SpecialTechniques.Contains(Character.SpecialTechniques.WithoutProtagonist))
                {
                    enemies.Add(String.Format("Вы\nнападение {0}  защита {1}  жизнь {2}{3}",
                        protagonist.Attack, protagonist.Defence, protagonist.Endurance,
                        protagonist.GetSpecialTechniques()));
                }

                foreach (Character ally in Allies)
                {
                    enemies.Add(String.Format("{0}\nнападение {1}  защита {2}  жизнь {3}{4}",
                        ally.Name, ally.Attack, ally.Defence, ally.GetEndurance(),
                        ally.GetSpecialTechniques()));
                }

                enemies.Add("SPLITTER|против");
            }

            foreach (Character enemy in Enemies)
            {
                enemies.Add(String.Format("{0}\nнападение {1}  защита {2}  жизнь {3}{4}",
                    enemy.Name, enemy.Attack, enemy.Defence, enemy.GetEndurance(),
                    enemy.GetSpecialTechniques()));
            }

            return enemies;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText)
        {
            if (SpecialTechniques?.Contains(Character.SpecialTechniques.WithoutGameover) ?? false)
                return GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);
            else
                return base.GameOver(out toEndParagraph, out toEndText);
        }
            

        public List<string> InlineWoundsByDices()
        {
            WoundsByDices = true;
            return Dices();
        }

        public List<string> Dices()
        {
            List<string> diceCheck = new List<string> { };

            int dices = Count == 0 ? 1 : Count;
            int result = 0;
            string lineFormat = ((dices > 1) || WoundsByDices ? String.Empty : "BIG|") +
                "На{0} кубике выпало: {1}";

            for (int i = 1; i <= dices; i++)
            {
                int dice = Game.Dice.Roll();
                result += dice;
                string diceNum = dices > 1 ? String.Format(" {0}", i) : String.Empty;
                diceCheck.Add(String.Format(lineFormat, diceNum, Game.Dice.Symbol(dice)));
            }

            if (WoundsByDices)
            {
                if (WoundsMultiple > 0)
                {
                    diceCheck.Add(String.Format("Сумма ({0}) умножается на {1}", result, WoundsMultiple));
                    result *= WoundsMultiple;
                }

                protagonist.Endurance -= result;
                diceCheck.Add(String.Format("BIG|BAD|Ты потерял выносливостей: {0}", result));
            }
            else if (dices > 1)
            {
                diceCheck.Add(String.Format("BIG|BOLD|Выпало всего: {0}", result));
            }

            return diceCheck;
        }

        public List<string> FindTheWay()
        {
            List<string> way = new List<string> { "BIG|Ищем путь:" };

            double wayPoint = 202;

            while (true)
            {
                int direction = Game.Dice.Roll(size: 2);

                way.Add("GRAY|Подбрасываем монетку: " + (direction == 1 ? "орёл": "решка"));

                if ((wayPoint < 12) && (direction == 2))
                {
                    direction = 1;
                    way.Add("GRAY|Так в стенку упрёмся, лучше пойдём направо");
                }

                if ((wayPoint >= 1250) && (direction == 1))
                {
                    direction = 2;
                    way.Add("GRAY|Так в стенку упрёмся, лучше пойдём налево");
                }
                    
                if (direction == 1)
                {
                    way.Add("BOLD|Поворачиваем направо...");
                    double newWayPoint = wayPoint * 4;
                    way.Add(String.Format("{0} * 4 = {1}", wayPoint, newWayPoint));
                    wayPoint = newWayPoint;
                }
                else
                {
                    way.Add("BOLD|Поворачиваем налево...");
                    double newWayPoint = (wayPoint - 1) / 3;
                    way.Add(String.Format("({0} - 1) / 3 = {1}", wayPoint, newWayPoint));
                    wayPoint = newWayPoint;
                }

                int cleanWayPoint = Convert.ToInt32(wayPoint);

                if ((cleanWayPoint != wayPoint) || (wayPoint > 5000))
                {
                    way.Add(String.Empty);
                    way.Add("BIG|BAD|Всё, тупик...");
                    return way;
                }
                else if ((wayPoint >= 301) && (wayPoint <= 450))
                {
                    Game.Option.OpenButtonByDestination(cleanWayPoint);

                    way.Add(String.Empty);
                    way.Add(String.Format("BIG|GOOD|Кхм, число {0} вроде бы подходит...", wayPoint));
                    return way;
                }
            }
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains('|'))
            {
                foreach (string optionsPart in option.Split('|'))
                    if (protagonist.Creature == optionsPart)
                        return true;

                return false;
            }
            else if (option.Contains(','))
            {
                List<string> singlOption = option
                    .Split(',')
                    .Select(x => x.Trim())
                    .Select(y => y.Replace("!", String.Empty))
                    .ToList();

                foreach (string optionsPart in singlOption)
                    if (protagonist.Creature == optionsPart)
                        return false;

                return true;
            }
            else if (protagonist.Creature == option)
            {
                return true;
            }
            else if (protagonist.Creature == option.Replace("!", String.Empty))
            {
                return false;
            }
            else
            {
                return AvailabilityTrigger(option.Trim());
            }
        }

        private static Character FindEnemyIn(List<Character> Enemies) =>
            Enemies.Where(x => x.Endurance > 0).OrderBy(y => Game.Dice.Roll(size: 100)).FirstOrDefault();

        private static Character FindEnemy(Character fighter, List<Character> Allies, List<Character> Enemies)
        {
            if (Allies.Contains(fighter))
                return FindEnemyIn(Enemies);

            else if (Enemies.Contains(fighter))
                return FindEnemyIn(Allies);

            else
                return null;
        }

        private static bool IsProtagonist(string name) =>
            name == Character.Protagonist.Name;

        public List<string> HobgoblinGame()
        {
            List<string> game = new List<string> { "BIG|Играем:" };

            for (int round = 1; round < 5; round++)
            {
                if (protagonist.Endurance <= 0)
                    continue;

                game.Add(String.Empty);

                Game.Dice.DoubleRoll(out int firstRoll, out int secondRoll);
                int hitStrength = firstRoll + secondRoll + 1;

                game.Add(String.Format(
                    "Cила атаки кинжала: {0} + {1} + 1 = {2}",
                    Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(secondRoll), hitStrength));

                int hitDiff = hitStrength - protagonist.Defence;
                bool success = hitDiff > 0;

                game.Add(String.Format(
                    "Твоя защита: {0}, это {1} силы атаки",
                    protagonist.Defence, Game.Services.Сomparison(protagonist.Defence, hitStrength)));

                if (success)
                {
                    protagonist.Endurance -= hitDiff;

                    game.Add("BAD|BOLD|Ты ранен");

                    game.Add(String.Format("Ты потерял вносливости: {0} (осталось: {1})",
                       hitDiff, protagonist.Endurance));
                }
                else
                {
                    game.Add("GOOD|BOLD|Ты увернулся");
                }
            }

            game.Add(String.Empty);
            game.Add(protagonist.Endurance > 0 ? "BIG|GOOD|ТЫ ПОБЕДИЛ :)" : "BIG|BAD|ТЫ ПРОИГРАЛ :(");

            return game;
        }


        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1, enemyWounds = 0, enemyDeath = 0, allyDeath = 0;

            List<Character> FightAllies = new List<Character>();
            List<Character> FightEnemies = new List<Character>();
            List<Character> FightOrder = new List<Character>();
            Dictionary<string, int> WoundsCount = new Dictionary<string, int>();
            Dictionary<string, List<int>> AttackStory = new Dictionary<string, List<int>>();

            bool withoutProtagonist = SpecialTechniques.Contains(Character.SpecialTechniques.WithoutProtagonist);
            bool toFirstDeath = SpecialTechniques.Contains(Character.SpecialTechniques.ToFirstDeathOnly);
            bool werewolf = SpecialTechniques.Contains(Character.SpecialTechniques.Werewolf) &&
                (protagonist.Creature == "ОБОРОТЕНЬ");

            if (!withoutProtagonist)
                FightAllies.Add(protagonist);

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone().SetEndurance());

            if (Allies != null)
                foreach (Character ally in Allies)
                    FightAllies.Add(ally.Clone().SetEndurance());

            if ((FightEnemies.Count > 1) && (FightAllies.Count == 1) && !withoutProtagonist)
            {
                fight.Add("Противников много, а ты один, поэтому атакуют они первые :(");
                fight.Add(String.Empty);

                FightOrder.AddRange(FightEnemies);
                FightOrder.AddRange(FightAllies);
            }
            else
            {
                FightOrder.AddRange(FightAllies);
                FightOrder.AddRange(FightEnemies);
            }

            while (true)
            {
                fight.Add(String.Format("HEAD|BOLD|Раунд: {0}", round));

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

                    Game.Dice.DoubleRoll(out int firstRoll, out int secondRoll);
                    int hitStrength = firstRoll + secondRoll + fighter.Attack;

                    fight.Add(String.Format(
                        "{0} cила атаки: {1} + {2} + {3} = {4}",
                        (IsProtagonist(fighter.Name) ? "Ваша" : String.Format("{0} -", fighter.Name)),
                        Game.Dice.Symbol(firstRoll), Game.Dice.Symbol(secondRoll), fighter.Attack, hitStrength));

                    int hitDiff = hitStrength - enemy.Defence;
                    bool success = hitDiff > 0;

                    fight.Add(String.Format(
                        "{0}: {1}, это {2} силы атаки",
                        (IsProtagonist(enemy.Name) ? "Ваша защита" : "Защита противника"),
                        enemy.Defence, Game.Services.Сomparison(enemy.Defence, hitStrength)));

                    if (IsProtagonist(enemy.Name))
                    {
                        if (werewolf && success && !fighter.SpecialTechnique.Contains(Character.SpecialTechniques.RumbleKnife))
                        {
                            fight.Add("GOOD|Атака противника не может навредить оборотню!");
                            success = false;
                        }
                        else
                        {
                            fight.Add(success ? "BAD|BOLD|Ты ранены" : "GOOD|Атака отбита");
                        }
                    }
                    else  if (FightAllies.Contains(enemy))
                    {
                        fight.Add(success ? String.Format("BAD|{0} ранен", enemy.Name) : "GOOD|Атака отбита");
                    }
                    else
                    {
                        fight.Add(success ? String.Format("GOOD|BOLD|{0} ранен", enemy.Name) : "BAD|Атака отбита");
                    }

                    if (success)
                    {
                        enemy.Endurance -= hitDiff;
                        fight.Add(String.Format("{0} потерял вносливости: {1} (осталось: {2})",
                            enemy.Name, hitDiff, enemy.Endurance));

                        if (toFirstDeath && (enemy.Endurance <= 0) && !IsProtagonist(enemy.Name))
                        {
                            fight.Add("BIG|BOLD|В бою случилась первая смерть!");
                            return fight;
                        }
                    }
                }

                bool enemyLost = FightEnemies.Where(x => (x.Endurance > 0) || (x.MaxEndurance == 0)).Count() == 0;

                fight.Add(String.Empty);

                if (enemyLost || ((WoundsToWin > 0) && (WoundsToWin <= enemyWounds)))
                {
                    fight.Add("BIG|GOOD|ТЫ ПОБЕДИЛ :)");
                    return fight;
                }

                bool allyLost = FightAllies.Where(x => x.Endurance > 0).Count() == 0;

                if (allyLost)
                {
                    fight.Add("BIG|BAD|ТЫ ПРОИГРАЛ :(");

                    if (NotToDeath)
                        protagonist.Endurance += 1;

                    return fight;
                }

                if ((RoundsToWin > 0) && (RoundsToWin <= round))
                {
                    fight.Add("BAD|Отведённые на победу раунды истекли.");
                    fight.Add("BIG|BAD|ТЫ ПРОИГРАЛ :(");
                    return fight;
                }

                round += 1;
            }
        }

        public override bool IsHealingEnabled() =>
            protagonist.Endurance < protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel) =>
            protagonist.Endurance += healingLevel;
    }
}
