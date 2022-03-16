﻿using System;
using System.Collections.Generic;

namespace Seeker.Gamebook.Cyberpunk
{
    class Actions : Prototypes.Actions, Abstract.IActions
    {
        public static Actions StaticInstance = new Actions();
        private static Character protagonist = Character.Protagonist;
        private static Random rand = new Random();

        public string Stat { get; set; }
        public bool MultipliedLuck { get; set; }
        public bool DividedLuck { get; set; }

        public override List<string> Status() => new List<string>
        {
            String.Format("Планирование: {0}", protagonist.Planning),
            String.Format("Подготовка: {0}", protagonist.Preparation),
            String.Format("Везение: {0}", protagonist.Luck),
        };

        public override List<string> Representer()
        {
            if (String.IsNullOrEmpty(Text))
            {
                string line = "Проверка: ";

                foreach (string stat in Stat.Split(','))
                    line += String.Format("{0} + ", Constants.CharactersParams()[stat.Trim()]);

                return new List<string> { line.TrimEnd(' ', '+') };
            }
            else
                return new List<string> { Text };
        }

        public override string ButtonText() => "Пройти проверку";

        public override bool CheckOnlyIf(string option)
        {
            if (String.IsNullOrEmpty(option))
            {
                return true;
            }
            else if (option.Contains(">") || option.Contains("<"))
            {
                int level = Game.Services.LevelParse(option);

                if (option.Contains("ПЛАНИРОВАНИЕ <=") && (level < protagonist.Planning))
                    return false;

                else if (option.Contains("ПОДГОТОВКА <=") && (level < protagonist.Preparation))
                    return false;

                else if (option.Contains("ВЕЗЕНИЕ <=") && (level < protagonist.Luck))
                    return false;

                return true;
            }
            else
            {
                return CheckOnlyIfTrigger(option);
            }
        }

        private int PercentageDice() => rand.Next(100) + 1;

        public List<string> Test()
        {
            List<string> test = new List<string>();

            int paramsLevel = 0;
            string paramsLine = "Параметры: ";

            if (MultipliedLuck)
            {
                paramsLevel = protagonist.Luck * 2;
                paramsLine += String.Format("{0} (везение) x 2", protagonist.Luck);
            }
            else if (DividedLuck)
            {
                paramsLevel = protagonist.Luck / 2;
                paramsLine += String.Format("{0} (везение) / 2", protagonist.Luck);
            }
            else
            {
                foreach (string stat in Stat.Split(','))
                {
                    int param = GetProperty(protagonist, stat.Trim());
                    paramsLevel += param;
                    paramsLine += String.Format("{0} ({1}) + ",
                        param, Constants.CharactersParams()[stat.Trim()].ToLower());
                }
            }

            test.Add(String.Format("{0} = {1}", paramsLine.TrimEnd(' ', '+'), paramsLevel));

            int dice = PercentageDice();

            test.Add(String.Format("BOLD|BIG|Бросок кубика: {0} - {1}!", dice, Game.Services.Сomparison(dice, paramsLevel)));
            test.Add(Result((dice <= paramsLevel), "УСПЕХ|НЕУДАЧА"));

            return test;
        }
    }
}
