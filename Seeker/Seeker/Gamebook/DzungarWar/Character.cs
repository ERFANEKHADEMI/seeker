﻿using System;

namespace Seeker.Gamebook.DzungarWar
{
    class Character : Prototypes.Character, Abstract.ICharacter
    {
        public static Character Protagonist = new Character();

        private int _strength;
        public int Strength
        {
            get => _strength;
            set => _strength = Game.Param.Setter(value, max: 12);
        }

        private int _skill;
        public int Skill
        {
            get => _skill;
            set => _skill = Game.Param.Setter(value, max: 12);
        }

        private int _wisdom;
        public int Wisdom
        {
            get => _wisdom;
            set => _wisdom = Game.Param.Setter(value, max: 12);
        }

        private int _cunning;
        public int Cunning
        {
            get => _cunning;
            set => _cunning = Game.Param.Setter(value, max: 12);
        }

        private int _oratory;
        public int Oratory
        {
            get => _oratory;
            set => _oratory = Game.Param.Setter(value, max: 12);
        }

        private int? _danger;
        public int? Danger
        {
            get => _danger;
            set => _danger = Game.Param.Setter(value, max: 12);
        }

        public int Tanga { get; set; }
        public int? Favour { get; set; }
        public int Tincture { get; set; }
        public int Ginseng { get; set; }
        public int Airag { get; set; }

        public int StatBonuses { get; set; }
        public int MaxBonus { get; set; }
        public int Brother { get; set; }

        public override void Init()
        {
            Strength = 1;
            Skill = 1;
            Wisdom = 1;
            Cunning = 1;
            Oratory = 1;
            Danger = 0;
            Tanga = 150;
            Favour = null;
            Tincture = 3;
            Ginseng = 0;
            Airag = 0;
            StatBonuses = 4;
            MaxBonus = 1;
            Brother = 0;
        }

        public Character Clone() => new Character()
        {
            Strength = this.Strength,
            Skill = this.Skill,
            Wisdom = this.Wisdom,
            Cunning = this.Cunning,
            Oratory = this.Oratory,
            Danger = this.Danger,
            Favour = this.Favour,
            Tincture = this.Tincture,
            Ginseng = this.Ginseng,
            Airag = this.Airag,
            Tanga = this.Tanga,
            StatBonuses = this.StatBonuses,
            MaxBonus = this.MaxBonus,
            Brother = this.Brother,
        };

        public override string Save() => String.Join("|",
            Strength, Skill, Wisdom, Cunning, Oratory, Danger, Favour, Tanga,
            StatBonuses, MaxBonus, Brother, Tincture, Ginseng, Airag
        );

        public override void Load(string saveLine)
        {
            string[] save = saveLine.Split('|');

            Strength = int.Parse(save[0]);
            Skill = int.Parse(save[1]);
            Wisdom = int.Parse(save[2]);
            Cunning = int.Parse(save[3]);
            Oratory = int.Parse(save[4]);
            Danger = Game.Continue.IntNullableParse(save[5]);
            Favour = Game.Continue.IntNullableParse(save[6]);
            Tanga = int.Parse(save[7]);
            StatBonuses = int.Parse(save[8]);
            MaxBonus = int.Parse(save[9]);
            Brother = int.Parse(save[10]);
            Tincture = int.Parse(save[11]);
            Ginseng = int.Parse(save[12]);
            Airag = int.Parse(save[13]);
        }

        public override string Debug() => String.Format(
            "Сила: {0}/12\nЛовкость: {1}/12\nМудрость: {2}/12\nХитрость: {3}/12\n" +
            "Красноречие: {4}/12\nОпасность: {5}\nБлагосклонность: {6}/1\nТаньга: {7}\n" +
            "Бонусы параметров: {8}\nMax.параметров: {9}\nБрат: {10}\nНастойка: {11}\n" +
            "Женьшень: {12}",
            Strength, Skill, Wisdom, Cunning, Oratory, Danger, Favour, Tanga, StatBonuses,
            MaxBonus, Brother, Tincture, Ginseng
        );
    }
}
