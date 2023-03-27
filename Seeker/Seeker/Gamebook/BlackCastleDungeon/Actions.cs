﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;

        public List<Character> Enemies { get; set; }
        public int RoundsToWin { get; set; }
        public int WoundsToWin { get; set; }
        public int StrengthPenlty { get; set; }
        public int ExtendedDamage { get; set; }

        public bool ThisIsSpell { get; set; }

        static Dictionary<string, bool> SpellActivate = new Dictionary<string, bool>
        {
            ["ЗАКЛЯТИЕ КОПИИ"] = false,
            ["ЗАКЛЯТИЕ СИЛЫ"] = false,
            ["ЗАКЛЯТИЕ СЛАБОСТИ"] = false,
        };

        public override List<string> Status() => new List<string>
        {
            String.Format("Мастерство: {0}", protagonist.Mastery),
            String.Format("Выносливость: {0}/{1}", protagonist.Endurance, protagonist.MaxEndurance),
            String.Format("Удача: {0}", protagonist.Luck),
            String.Format("Золото: {0}", protagonist.Gold)
        };

        public override List<string> AdditionalStatus()
        {
            Dictionary<string, int> currentSpells = new Dictionary<string, int>();

            if (protagonist.Spells == null)
                return null;

            foreach (string spell in protagonist.Spells)
            {
                string shortSpellName = spell.Replace("ЗАКЛЯТИЕ ", String.Empty).ToLower();

                if (currentSpells.ContainsKey(shortSpellName))
                    currentSpells[shortSpellName] += 1;
                else
                    currentSpells.Add(shortSpellName, 1);
            }

            if (currentSpells.Count <= 0)
                return null;

            List<string> statusLines = new List<string>();

            foreach (string spell in currentSpells.Keys.ToList().OrderBy(q => q))
                statusLines.Add(String.Format("{0}: {1}", char.ToUpper(spell[0]) + spell.Substring(1), currentSpells[spell]));

            return statusLines;
        }

        public override List<string> StaticButtons()
        {
            List<string> staticButtons = new List<string> { };

            if (Game.Data.Constants.GetParagraphsWithoutStaticsButtons().Contains(Game.Data.CurrentParagraphID))
                return staticButtons;

            if (protagonist.Spells.Contains("ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ") && (protagonist.Endurance < protagonist.MaxEndurance))
                staticButtons.Add("ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ");

            foreach (string spell in Constants.StaticSpells)
                if (Services.ParagraphWithFight(spell) && protagonist.Spells.Contains(spell) && !SpellActivate[spell])
                    staticButtons.Add(spell);

            return staticButtons;
        }

        public override bool StaticAction(string action)
        {
            protagonist.Spells.Remove(action);

            if (action.Contains("ИСЦЕЛЕНИЯ"))
                protagonist.Endurance += 8;

            if (Constants.StaticSpells.Contains(action))
                SpellActivate[action] = true;

            return true;
        }

        public override bool GameOver(out int toEndParagraph, out string toEndText) =>
            GameOverBy(protagonist.Endurance, out toEndParagraph, out toEndText);

        public override bool IsButtonEnabled(bool secondButton = false)
        {
            bool disabledSpellAdd = ThisIsSpell && (protagonist.SpellSlots <= 0) && !secondButton;
            bool disabledSpellRemove = ThisIsSpell && !protagonist.Spells.Contains(Head) && secondButton;
            bool disabledGetOptions = (Price > 0) && Used;
            bool disabledByPrice = (Price > 0) && (protagonist.Gold < Price);

            return !(disabledSpellAdd || disabledSpellRemove || disabledGetOptions || disabledByPrice);
        }

        public override bool Availability(string option)
        {
            if (String.IsNullOrEmpty(option))
                return true;

            else if (option.Contains("ЗОЛОТО >="))
                return int.Parse(option.Split('=')[1]) <= protagonist.Gold;

            else if (option.Contains("ЗАКЛЯТИЕ"))
                return protagonist.Spells.Contains(option);

            else
                return AvailabilityTrigger(option);
        }

        public override List<string> Representer()
        {
            List<string> enemies = new List<string>();

            if (Price > 0)
            {
                string gold = Game.Services.CoinsNoun(Price, "золотой", "золотых", "золотых");
                return new List<string> { String.Format("{0}, {1} {2}", Head, Price, gold) };
            }
            else if (ThisIsSpell)
            {
                int count = (ThisIsSpell ? protagonist.Spells.Where(x => x == Head).Count() : 0);
                return new List<string> { String.Format("{0}{1}", Head, (count > 0 ? String.Format(" ({0} шт)", count) : String.Empty)) };
            }

            if (Enemies == null)
                return enemies;

            foreach (Character enemy in Enemies)
                enemies.Add(String.Format("{0}\nмастерство {1}  выносливость {2}", enemy.Name, enemy.Mastery, enemy.Endurance));

            return enemies;
        }

        public List<string> Luck()
        {
            Game.Dice.DoubleRoll(out int firstDice, out int secondDice);

            bool goodLuck = (firstDice + secondDice) <= protagonist.Luck;

            List<string> luckCheck = new List<string> { String.Format(
                "Проверка удачи: {0} + {1} {2} {3}",
                Game.Dice.Symbol(firstDice), Game.Dice.Symbol(secondDice), (goodLuck ? "<=" : ">"), protagonist.Luck
            ) };

            luckCheck.Add(goodLuck ? "BIG|GOOD|УСПЕХ :)" : "BIG|BAD|НЕУДАЧА :(");

            if (protagonist.Luck > 2)
            {
                protagonist.Luck -= 1;
                luckCheck.Add("Уровень удачи снижен на единицу");
            }

            return luckCheck;
        }

        public List<string> Get()
        {
            if (ThisIsSpell && (protagonist.SpellSlots >= 1))
            {
                protagonist.Spells.Add(Head);
                protagonist.SpellSlots -= 1;
            }
            else if ((Price > 0) && (protagonist.Gold >= Price))
            {
                protagonist.Gold -= Price;

                if (!Multiple)
                    Used = true;

                if (Benefit != null)
                    Benefit.Do();
            }

            return new List<string> { "RELOAD" };
        }

        public List<string> Decrease()
        {
            protagonist.Spells.Remove(Head);
            protagonist.SpellSlots += 1;

            return new List<string> { "RELOAD" };
        }

        public List<string> Fight()
        {
            List<string> fight = new List<string>();

            int round = 1;
            int enemyWounds = 0;

            List<Character> FightEnemies = new List<Character>();

            foreach (Character enemy in Enemies)
                FightEnemies.Add(enemy.Clone());

            if (SpellActivate["ЗАКЛЯТИЕ СЛАБОСТИ"])
            {
                SpellActivate["ЗАКЛЯТИЕ СЛАБОСТИ"] = false;

                int oldEnemyMastery = FightEnemies[0].Mastery;
                FightEnemies[0].Mastery -= 2;

                fight.Add(String.Format(
                    "BOLD|Заклятье слабости ослабляет вашего противника: {0} теперь имеет ловкость {1} вместо {2}",
                    FightEnemies[0].Name, FightEnemies[0].Mastery, oldEnemyMastery
                ));

                fight.Add(String.Empty);
            }

            if (SpellActivate["ЗАКЛЯТИЕ КОПИИ"])
            {
                SpellActivate["ЗАКЛЯТИЕ КОПИИ"] = false;

                Character enemyCopy = FightEnemies[0].Clone();
                enemyCopy.Name += "-копия";

                bool copyWin = Services.WinInFight(ref fight, ref round, ref enemyCopy, ref FightEnemies, ref enemyWounds,
                    StrengthPenlty, WoundsToWin, RoundsToWin, ExtendedDamage, copyFight: true);

                fight.Add(String.Empty);

                if (copyWin)
                {
                    fight.Add("BIG|GOOD|Копия ПОБЕДИЛА :)");
                    return fight;
                }

                else if ((RoundsToWin > 0) && (RoundsToWin <= round))
                {
                    fight.Add("BIG|BAD|Вы ПРОИГРАЛИ :(");
                    return fight;
                }

                else
                    fight.Add("BOLD|BAD|Копия проиграла, дальше сражаться придётся вам");

                fight.Add(String.Empty);
            }

            int oldMastery = protagonist.Mastery;

            if (SpellActivate["ЗАКЛЯТИЕ СИЛЫ"])
            {
                SpellActivate["ЗАКЛЯТИЕ СИЛЫ"] = false;

                protagonist.Mastery += 2;

                fight.Add(String.Format(
                    "BOLD|Заклятье Силы увеличивает ваше мастерство: на время этого боя она равна {0}",
                    protagonist.Mastery
                ));

                fight.Add(String.Empty);
            }

            bool win = Services.WinInFight(ref fight, ref round, ref protagonist, ref FightEnemies, ref enemyWounds,
                StrengthPenlty, WoundsToWin, RoundsToWin, ExtendedDamage);

            protagonist.Mastery = oldMastery;

            fight.Add(String.Empty);
            fight.Add(Result(win, "Вы ПОБЕДИЛИ|Вы ПРОИГРАЛИ"));

            return fight;
        }

        public override bool IsHealingEnabled() =>
            protagonist.Endurance < protagonist.MaxEndurance;

        public override void UseHealing(int healingLevel) =>
            protagonist.Endurance += healingLevel;
    }
}
