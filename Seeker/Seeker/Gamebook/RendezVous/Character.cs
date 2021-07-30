﻿using System;

namespace Seeker.Gamebook.RendezVous
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        public int Awareness { get; set; }

        public override void Init()
        {
            Name = String.Empty;
            Awareness = 0;
        }

        public Character Clone() => new Character()
        {
            Name = this.Name,
            Awareness = this.Awareness,
        };

        public override string Save() => Awareness.ToString();

        public override void Load(string saveLine) => Awareness = int.Parse(saveLine);
    }
}
