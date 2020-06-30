﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Game
{
    class Character
    {
        public string Name { get; set; }

        public int Mastery { get; set; }
        public int Endurance { get; set; }
        public int Luck { get; set; }

        public void Init()
        {
            Mastery = Dice.Roll() + 6;
            Endurance = Dice.Roll(dices: 2) + 12;
            Luck = Dice.Roll() + 6;
        }
    }
}
