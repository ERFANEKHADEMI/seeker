﻿using System;

namespace Seeker.Prototypes
{
    class Modification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }

        public bool Empty { get; set; }
        public bool Restore { get; set; }

        public delegate void ParamMod();

        public virtual void Do() => Game.Other.DoNothing();

        public virtual void Do(Abstract.ICharacter Character)
        {
            if (Name == "Trigger")
            {
                Game.Option.Trigger(ValueString);
            }
            else if (Name == "Healing")
            {
                Game.Healing.Add(ValueString);
            }
            else
            {
                int currentValue = 0;

                if (Restore)
                    currentValue = GetProperty(Character, "Max" + Name);

                else if (!Empty)
                    currentValue = GetProperty(Character, Name);

                SetProperty(Character, Name, currentValue + Value);

                if (Name.StartsWith("Max"))
                {
                    string normalParam = Name.Remove(0, 3);
                    SetProperty(Character, normalParam, GetProperty(Character, normalParam) + Value);
                }
            }
        }

        public int GetProperty(Abstract.ICharacter Character, string property) =>
            (int)Character.GetType().GetProperty(property).GetValue(Character, null);

        public void SetProperty(Abstract.ICharacter Character, string property, int value) =>
            Character.GetType().GetProperty(property).SetValue(Character, value);

        public void DoByTrigger(string trigger, ParamMod doModification, bool not = false)
        {
            if ((!not && Game.Option.IsTriggered(trigger)) || (not && !Game.Option.IsTriggered(trigger)))
                doModification();
        }

        public bool DoByName(string actualName, ParamMod doModification)
        {
            if (Name == actualName)
            {
                doModification();
                return true;
            }
            else
                return false;
        }

        public bool DoByValueString(string actualName, ParamMod doModification) =>
            (String.IsNullOrEmpty(ValueString) ? false : DoByName(actualName, doModification));
    }
}
