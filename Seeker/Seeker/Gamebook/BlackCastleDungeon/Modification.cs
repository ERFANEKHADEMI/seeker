﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Modification : Prototypes.Modification, Abstract.IModification
    {
        public override void Do()
        {
            if (Name == "Healing")
                Game.Healing.Add(ValueString);

            else if (Name == "RemoveSpell")
                Character.Protagonist.Spells.RemoveAt(Character.Protagonist.Spells.IndexOf(ValueString));

            else
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                if (Name.StartsWith("Max"))
                {
                    string normalParam = Name.Remove(0, 3);

                    int normalValue = (int)Character.Protagonist.GetType().GetProperty(normalParam).GetValue(Character.Protagonist, null);

                    if ((normalValue + Value) > currentValue)
                        Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue + Value);

                    Character.Protagonist.GetType().GetProperty(normalParam).SetValue(Character.Protagonist, currentValue + Value);
                }
                else
                {
                    currentValue += Value;

                    Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
                }
            }
        }
    }
}
