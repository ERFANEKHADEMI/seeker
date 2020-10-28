﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.LegendsAlwaysLie
{
    class Character : Interfaces.ICharacter
    {
        public static Character Protagonist = new Gamebook.LegendsAlwaysLie.Character();

        public string Name { get; set; }

        public void Init()
        {
            Name = String.Empty;
        }

        public Character Clone()
        {
            return new Character() {
                Name = this.Name,
            };
        }
    }
}
