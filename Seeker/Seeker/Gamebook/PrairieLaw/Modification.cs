﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.PrairieLaw
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "StrengthByPoison")
            {
                if (!Game.Data.Triggers.Contains("Противоядие"))
                    Character.Protagonist.Strength -= 10;
            }
            else if (Name == "Skin")
            {
                if (Game.Data.Triggers.Contains("Нож"))
                    Character.Protagonist.AnimalSkins.Add(ValueString);
            }
            else
                InnerDo(Character.Protagonist);
        }
    }
}
