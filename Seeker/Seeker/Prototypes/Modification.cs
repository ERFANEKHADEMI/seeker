﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Prototypes
{
    class Modification : BasicsModification
    {
        public bool Empty { get; set; }
        public bool Restore { get; set; }

        public void InnerDo(Abstract.ICharacter Character)
        {
            if (Name == "Trigger")
                Game.Option.Trigger(ValueString);

            else if (Name == "Healing")
                Game.Healing.Add(ValueString);

            else
            {
                int currentValue = (int)Character.GetType().GetProperty(Name).GetValue(Character, null);

                if (Restore)
                    currentValue = (int)Character.GetType().GetProperty("Max" + Name).GetValue(Character, null);

                else if (Empty)
                    currentValue = 0;

                if (Name.StartsWith("Max"))
                {
                    string normalParam = Name.Remove(0, 3);

                    int normalValue = (int)Character.GetType().GetProperty(normalParam).GetValue(Character, null);

                    if ((normalValue + Value) > currentValue)
                        Character.GetType().GetProperty(Name).SetValue(Character, currentValue + Value);

                    Character.GetType().GetProperty(normalParam).SetValue(Character, currentValue + Value);
                }
                else
                {
                    currentValue += Value;

                    Character.GetType().GetProperty(Name).SetValue(Character, currentValue);
                }
            }
        }
    }
}
