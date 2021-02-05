﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Modification : Abstract.IModification
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public string ValueString { get; set; }
        public bool IntuitiveSolution { get; set; }

        public void Do()
        {
            if (!String.IsNullOrEmpty(ValueString) && (Name == "Patron"))
                Character.Protagonist.Patron = ValueString;

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "Weapon"))
                Character.Protagonist.AddWeapons(ValueString);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "Armour"))
                Character.Protagonist.AddArmour(ValueString);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "FellIntoFavor"))
                Character.Protagonist.FellIntoFavor(ValueString);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "FellOutOfFavor"))
                Character.Protagonist.FellIntoFavor(ValueString, fellOut: true);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "Indifferent"))
                Character.Protagonist.FellIntoFavor(ValueString, indifferent: true);

            else if (Name == "Resurrection")
            {
                Character.Protagonist.Resurrection += Value;
                Character.Protagonist.Glory = 1;
                Character.Protagonist.Shame = 0;
            }

            else if (!(IntuitiveSolution && (Character.Protagonist.NoIntuitiveSolutionPenalty > 0)))
            {
                int currentValue = (int)Character.Protagonist.GetType().GetProperty(Name).GetValue(Character.Protagonist, null);

                currentValue += Value;

                Character.Protagonist.GetType().GetProperty(Name).SetValue(Character.Protagonist, currentValue);
            }
        }
    }
}
