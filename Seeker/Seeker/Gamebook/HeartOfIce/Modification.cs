﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.HeartOfIce
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

        public void Do()
        {
            if (Name == "Skill")
                Character.Protagonist.Skills.Add(ValueString);

            else if (Name == "RemoveTrigger")
                Game.Option.Trigger(ValueString, remove: true);

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
