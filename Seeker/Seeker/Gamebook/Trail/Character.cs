﻿using System;

namespace Seeker.Gamebook.Trail
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
