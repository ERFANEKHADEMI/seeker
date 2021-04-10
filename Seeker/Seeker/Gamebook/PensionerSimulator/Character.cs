﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.PensionerSimulator
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.PensionerSimulator.Character();

        public string Name { get; set; }

        public void Init()
        {
            Name = String.Empty;
        }

        public string Save() => String.Empty;

        public void Load(string saveLine) { }
    }
}
