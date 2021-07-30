﻿using System;

namespace Seeker.Gamebook.InvisibleFront
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public int Dissatisfaction { get; set; }
        public int Recruitment { get; set; }


        public override void Init()
        {
            Dissatisfaction = 0;
            Recruitment = 0;
        }

        public Character Clone() => new Character()
        {
            Dissatisfaction = this.Dissatisfaction,
            Recruitment = this.Recruitment,
        };

        public override string Save() => String.Join("|", Dissatisfaction, Recruitment);

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Dissatisfaction = int.Parse(save[0]);
            Recruitment = int.Parse(save[1]);
        }
    }
}
