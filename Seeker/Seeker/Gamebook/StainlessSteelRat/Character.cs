﻿using System;

namespace Seeker.Gamebook.StainlessSteelRat
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();
    }
}
