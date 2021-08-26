﻿using System;

namespace Seeker.Gamebook.SilentSchool
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _life;
        public int Life
        {
            get => _life;
            set => _life = Game.Param.Setter(value);
        }

        public string Weapon { get; set; }
        public int Grail { get; set; }
        public int ChangeDecision { get; set; }
        public int HarmSelfAlready { get; set; }
        
        public override void Init()
        {
            Life = 30;
            Weapon = String.Empty;
            Grail = 0;
            ChangeDecision = 0;
            HarmSelfAlready = 0;
        }

        public Character Clone() => new Character()
        {
            Life = this.Life,
            Weapon = this.Weapon,
            Grail = this.Grail,
            ChangeDecision = this.ChangeDecision,
            HarmSelfAlready = this.HarmSelfAlready,
        };

        public override string Save() => String.Join("|",
            Life, Weapon, Grail, ChangeDecision, HarmSelfAlready
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Life = int.Parse(save[0]);
            Weapon = save[1];
            Grail = int.Parse(save[2]);
            ChangeDecision = int.Parse(save[3]);
            HarmSelfAlready = int.Parse(save[4]);
        }
    }
}
