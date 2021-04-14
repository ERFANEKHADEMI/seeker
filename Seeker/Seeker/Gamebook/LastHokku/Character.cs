﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Seeker.Gamebook.LastHokku
{
    class Character : Abstract.ICharacter
    {
        public static Character Protagonist = new Gamebook.LastHokku.Character();

        public List<string> Hokku { get; set; }

        public void Init()
        {
            Hokku = new List<string>();
        }

        public string Save() => String.Join("|", Hokku);

        public void Load(string saveLine)
        {
            Hokku = saveLine.Split('|').ToList();
        }
    }
}
