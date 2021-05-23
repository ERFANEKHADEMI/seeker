﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook.BloodfeudOfAltheus
{
    class Modification : Prototypes.BasicsModification, Abstract.IModification
    {
        public bool IntuitiveSolution { get; set; }

        public override void Do()
        {
            Character hero = Character.Protagonist;

            if (!String.IsNullOrEmpty(ValueString) && (Name == "Patron"))
                hero.Patron = ValueString;

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "Weapon"))
                hero.AddWeapons(ValueString);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "Armour"))
                hero.AddArmour(ValueString);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "FellIntoFavor"))
                hero.FellIntoFavor(ValueString);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "FellOutOfFavor"))
                hero.FellIntoFavor(ValueString, fellOut: true);

            else if (!String.IsNullOrEmpty(ValueString) && (Name == "Indifferent"))
                hero.FellIntoFavor(ValueString, indifferent: true);

            else if (Name == "DiceShame")
                hero.Shame += Game.Dice.Roll();

            else if (Name == "AresFavor")
            {
                if (hero.Patron != "Арес")
                {
                    hero.FellIntoFavor("Арес");
                    hero.Glory += 3;
                }
                else
                    hero.Glory += 5;
            }

            else if (Name == "ApolloDisFavor")
            {
                if (hero.Patron != "Аполлон")
                    hero.FellIntoFavor("Аполлон", fellOut: true);
                else
                    hero.Glory -= 2;
            }

            else if (Name == "PoseidonDisFavor")
            {
                if (hero.Patron != "Посейдон")
                {
                    hero.FellIntoFavor("Посейдон", fellOut: true);
                    hero.Shame += 4;
                }
                else
                    hero.Glory -= 6;
            }

            else if (Name == "PoseidonDisFavor2")
            {
                if (hero.Patron != "Посейдон")
                    hero.FellIntoFavor("Посейдон", fellOut: true);
                else
                    hero.Shame += 1;
            }

            else if (Name == "Resurrection")
            {
                if (hero.Resurrection > 0)
                    hero.Resurrection += Value;

                else if (hero.BroochResurrection > 0)
                    hero.BroochResurrection += Value;

                hero.Glory = 1;
                hero.Shame = 0;
            }

            else if (!(IntuitiveSolution && (hero.NoIntuitiveSolutionPenalty > 0)))
            {
                int currentValue = (int)hero.GetType().GetProperty(Name).GetValue(hero, null);
                
                if (!((Name == "Glory") && (hero.Glory <= 0)))
                    currentValue += Value;

                hero.GetType().GetProperty(Name).SetValue(hero, currentValue);
            }
        }
    }
}
