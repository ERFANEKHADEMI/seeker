﻿using System;

namespace Seeker.Gamebook.GoingToLaughter
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (!ModBySpecificName(Name))
                base.Do(Character.Protagonist);
        }

        private bool ModBySpecificName(string name)
        {
            Character protagonist = Character.Protagonist;
            int lutnaBonus = ((Game.Data.Triggers.Contains("Лютня") && Advantage("Музицирование")) ? 2 : 1);

            if ((name == "CrazyDance") && Advantage("Пение, Танец"))
            {
                protagonist.Heroism += 6;
                protagonist.Inspiration += 6;
            }
            else if (name == "RevoltInspiration")
            {
                protagonist.Heroism += AdvantagesBonus("Красноречие, Артистизм, Верховая езда, Предусмотрительность, Сражение", 1);

                if (Advantage("Популярность"))
                    protagonist.Heroism += 10;

                for(int i = 0; i < protagonist.Buffoonery; i++)
                    protagonist.Heroism += 10;

                for (int i = 0; i < protagonist.Inspiration; i++)
                    protagonist.Heroism += 5;

                for (int i = 0; i < (protagonist.Villainy / 6); i++)
                    protagonist.Heroism -= 10;

                if (Advantage("Жестокосердие"))
                    protagonist.Heroism -= 10;
            }
            else if (name == "SuccessInspiration")
            {
                protagonist.Heroism += AdvantagesBonus("Популярность, Пение, Танец, Красноречие," +
                    "Везение, Смекалка, Чревовещание, Наблюдательность, Интуиция, Эрудиция", 5);
            }
            else if (name == "Musical1Performance")
            {
                protagonist.Heroism += 10 * lutnaBonus;
                protagonist.Inspiration += 2 * lutnaBonus;
                protagonist.Buffoonery += 5 * lutnaBonus;
            }
            else if (name == "Musical2Performance")
            {
                protagonist.Buffoonery += 1 * lutnaBonus;
            }
            else if (name == "Musical3Performance")
            {
                protagonist.Heroism += 6 * lutnaBonus;
                protagonist.Inspiration += 2 * lutnaBonus;
                protagonist.Buffoonery += 6 * lutnaBonus;
            }
            else if (name == "Musical4Performance")
            {
                protagonist.Heroism += 5 * lutnaBonus;
                protagonist.Inspiration += 2 * lutnaBonus;
                protagonist.Buffoonery += 5 * lutnaBonus;
            }
            else if (name == "Musical5Performance")
            {
                protagonist.Heroism += 2 * lutnaBonus;
            }
            else if (name == "Musical6Performance")
            {
                protagonist.Heroism += 3 * lutnaBonus;
            }
            else if (name == "Musical7Performance")
            {
                protagonist.Heroism += 2 * lutnaBonus;
                protagonist.Buffoonery += 2 * lutnaBonus;
            }
            else if (name == "Musical8Performance")
            {
                protagonist.Heroism += 4 * lutnaBonus;
            }
            else if (name == "Musical9Performance")
            {
                protagonist.Heroism += 12 * lutnaBonus;
                protagonist.Inspiration += 2 * lutnaBonus;
            }
            else if (name == "Musical10Performance")
            {
                protagonist.Heroism += 5 * lutnaBonus;
                protagonist.Inspiration += 1 * lutnaBonus;
            }
            else
                return false;

            return true;
        }

        private bool Advantage(string advantages)
        {
            Character protagonist = Character.Protagonist;

            if (advantages.Contains(","))
            {
                string[] advantagesList = advantages.Split(',');

                foreach (string advantage in advantagesList)
                    if (!protagonist.Advantages.Contains(advantage.Trim()))
                        return false;

                return true;
            }
            else if (advantages.Contains("|"))
            {
                string[] advantagesList = advantages.Split('|');

                foreach (string advantage in advantagesList)
                    if (protagonist.Advantages.Contains(advantage.Trim()))
                        return true;

                return false;
            }
            else
                return protagonist.Advantages.Contains(advantages);
        }

        private int AdvantagesBonus(string advantages, int bonus)
        {
            string[] advantagesList = advantages.Split(',');

            int total = 0;

            foreach (string advantage in advantagesList)
                if (Advantage(advantage))
                    total += bonus;

            return total;
        }
    }
}
