﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.BlackCastleDungeon
{
    class Paragraphs : Interfaces.IParagraphs
    {
        public Game.Paragraph Get(int id)
        {
            Paragraph source = Paragraph[id];

            Game.Paragraph paragraph = new Game.Paragraph();

            if (source.Options != null)
                paragraph.Options = new List<Option>(source.Options);

            if (source.Actions != null)
                paragraph.Actions = new List<Interfaces.IActions>(source.Actions);

            if (source.Modification != null)
                paragraph.Modification = new List<Interfaces.IModification>(source.Modification);

            paragraph.OpenOption = source.OpenOption;

            return paragraph;
        }

        private static Dictionary<int, Paragraph> Paragraph = new Dictionary<int, Paragraph>
        {
            [0] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 620, Text = "В путь!" },
                    new Option { Destination = 619, Text = "Правила и инструкции" },
                }
            },
            [1] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "По правой" },
                    new Option { Destination = 110, Text = "По левой" },
                }
            },
            [2] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "Поднимитесь на холм" },
                    new Option { Destination = 97, Text = "Пойдете дальше по дороге" },
            }
            },
            [3] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 211, Text = "Далее" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Вернуться обратно и направиться к зданию в центре двора" },
                    new Option { Destination = 372, Text = "Заклятие Плавания", OnlyIf = "ЗАКЛЯТИЕ ПЛАВАНИЯ" },
                    new Option { Destination = 103, Text = "Заклятие Левитации", OnlyIf = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ" },
                    new Option { Destination = 311, Text = "Заклятием Плавания и поплыть по течению реки" , OnlyIf = "ЗАКЛЯТИЕ ПЛАВАНИЯ"},
                }
            },
            [5] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ПЛАВАНИЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 216, Text = "К острову" },
                    new Option { Destination = 517, Text = "На другой берег" },
                }
            },
            [6] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ДРОВОСЕК",
                                Mastery = 5,
                                Endurance = 4,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ДРОВОСЕК",
                                Mastery = 6,
                                Endurance = 7,
                            }
                        },

                        Aftertext = "Если вы победили их, то наградой вам будет всего 1 золотой в кармане Первого дровосека. Дровосеки люди небогатые, и вряд ли стоило убивать их. Потеряйте 1 УДАЧУ. Теперь отправляйтесь дальше.",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Luck",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 420, Text = "Отправиться дальше" },
                }
            },
            [7] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ КОПИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 36, Text = "Заклятия Силы", OnlyIf = "ЗАКЛЯТИЕ СИЛЫ" },
                    new Option { Destination = 314, Text = "Заклятия Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 112, Text = "Заклятия Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 183, Text = "Драться" },
                }
            },
            [8] = new Paragraph
            {
                OpenOption = "BronzeWhistle",

                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "К мосту" },
                }
            },
            [9] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 222, Text = "Далее" },
                }
            },
            [10] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 399, Text = "Войти" },
                    new Option { Destination = 325, Text = "По коридору" },
                }
            },
            [11] = new Paragraph
            {
                OpenOption = "PirateTreasure",

                Options = new List<Option>
                {
                    new Option { Destination = 234, Text = "Далее" },
                }
            },
            [12] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 312, Text = "Построите плот" },
                    new Option { Destination = 201, Text = "Воспользуетесь заклятием Плавания", OnlyIf = "ЗАКЛЯТИЕ ПЛАВАНИЯ" },
                    new Option { Destination = 425, Text = "Воспользуетесь заклятием Левитации", OnlyIf = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ" },
                }
            },
            [13] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 360, Text = "Пересечь дорогу и идти дальше в обход деревни" },
                    new Option { Destination = 184, Text = "Выйти на дорогу и пойти к деревне" },
                    new Option { Destination = 235, Text = "Выйти на дорогу и пойти от деревни" },
                }
            },
            [14] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Вы удачливы" },
                    new Option { Destination = 404, Text = "Если нет, то ваше решение принято слишком поздно" },
                }
            },
            [15] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 417, Text = "Заклятие Силы", OnlyIf = "ЗАКЛЯТИЕ СИЛЫ" },
                    new Option { Destination = 228, Text = "Заклятие Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 521, Text = "Заклятие Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 326, Text = "Драться без помощи магии" },
                }
            },
            [16] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВАЯ ЛЕТУЧАЯ МЫШЬ",
                                Mastery = 6,
                                Endurance = 8,
                            },
                            new Character
                            {
                                Name = "ВТОРАЯ ЛЕТУЧАЯ МЫШЬ",
                                Mastery = 5,
                                Endurance = 7,
                            },
                            new Character
                            {
                                Name = "ТРЕТЬЯ ЛЕТУЧАЯ МЫШЬ",
                                Mastery = 5,
                                Endurance = 6,
                            }
                        },

                        Aftertext = "Если вы победите их, то можете откинуть ставни и осмотреть домик или же уйти и направиться к центральному строению.",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 557, Text = "Воспользоваться заклятием Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 120, Text = "Если вы победите их, осмотреть домик" },
                    new Option { Destination = 416, Text = "Уйти и направиться к центральному строению" },
                }
            },
            [17] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 426, Text = "Отдадите им все золото" },
                    new Option { Destination = 109, Text = "Вы идете сражаться с волшебником" },
                    new Option { Destination = 339, Text = "Вы встречали по дороге кого-то из их знакомых" },
                }
            },
            [18] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Заплатите ему" },
                    new Option { Destination = 217, Text = "Поищите в заплечном мешке подарок" },
                    new Option { Destination = 106, Text = "Вежливо поблагодарите и уйдете" },
                    new Option { Destination = 518, Text = "Лучше убить его и обыскать лавку" },
                }
            },
            [19] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 185, Text = "Пойдете дальше" },
                    new Option { Destination = 176, Text = "Зайдете в дом" },
                }
            },
            [20] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 340, Text = "Поискать убежища на ночь" },
                    new Option { Destination = 442, Text = "Пойти дальше" },
                }
            },
            [21] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 186, Text = "Воспользуетесь предложением и скажете, что ищете Черный замок" },
                    new Option { Destination = 202, Text = "Попросите проводить вас до ближайшего жилья" },
                    new Option { Destination = 229, Text = "Откажетесь и уйдете" },
                }
            },
            [22] = new Paragraph
            {
                OpenOption = "WhiteArrow",

                Options = new List<Option>
                {
                    new Option { Destination = 427, Text = "Через дверь в той же стене, где вход" },
                    new Option { Destination = 398, Text = "Через правую дверь" },
                    new Option { Destination = 206, Text = "Через левую дверь" },
                    new Option { Destination = 350, Text = "Через среднюю дверь" },
                }
            },
            [23] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Заплатить ему" },
                    new Option { Destination = 217, Text = "Поищите в заплечном мешке подарок" },
                    new Option { Destination = 106, Text = "Вежливо поблагодарите и уйдете" },
                    new Option { Destination = 518, Text = "Лучше убить его" },
                }
            },
            [24] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 238, Text = "Далее" },
                }
            },
            [25] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 427, Text = "Через дверь в той же стене, где вход" },
                    new Option { Destination = 398, Text = "Через правую дверь" },
                    new Option { Destination = 206, Text = "Через левую дверь" },
                    new Option { Destination = 350, Text = "Через среднюю дверь" },
                }
            },
            [26] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 341, Text = "Далее" },
                }
            },
            [27] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "Дадите ей денег" },
                    new Option { Destination = 522, Text = "Будете драться" },
                }
            },
            [28] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Он вполне доволен" },
                }
            },
            [29] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "Пойти по дороге" },
                    new Option { Destination = 184, Text = "Войти в деревню" },
                }
            },
            [30] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Покажите шкуру лисы", OnlyIf = "FoxSkin" },
                    new Option { Destination = 428, Text = "Придется драться" },
                }
            },
            [32] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Mastery",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [33] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться c орком",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ОРК",
                                Mastery = 6,
                                Endurance = 8,
                            },
                        },

                        Aftertext = "Если вы убили его, то сделали это как раз вовремя: из погреба с бутылкой вина поднимается Гоблин. Увидев вас, он бросает бутылку и хватается за боевой топор.",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться c гоблином",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГОБЛИН",
                                Mastery = 7,
                                Endurance = 5,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 143, Text = "Если хотите, можете попробовать убежать" },
                    new Option { Destination = 239, Text = "Гоблин убит" },
                }
            },
            [34] = new Paragraph
            {
                OpenOption = "Mirror, GoldenWhistle",

                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 204, Text = "Отдохнуть" },
                    new Option { Destination = 342, Text = "Пойдете дальше" },
                }
            },
            [36] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СИЛЫ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее" },
                }
            },
            [37] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ДРАКОН",
                                Mastery = 9,
                                Endurance = 4,
                            },
                        },
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 451, Text = "Заклятие Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 454, Text = "Дракон убит" },
                }
            },
            [38] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 221, Text = "Войти внутрь" },
                    new Option { Destination = 55, Text = "Пройти мимо" },
                }
            },
            [39] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 138, Text = "В дверь" },
                    new Option { Destination = 523, Text = "По лестнице вверх" },
                    new Option { Destination = 452, Text = "По лестнице вниз" },
                    new Option { Destination = 70, Text = "Подойдете к окну" },
                    new Option { Destination = 205, Text = "К картине" },
                }
            },
            [40] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ГОБЛИН",
                                Mastery = 6,
                                Endurance = 9,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ГОБЛИН",
                                Mastery = 7,
                                Endurance = 5,
                            },
                        },

                        Aftertext = "Если вам удалось убить Гоблинов и остаться в живых, то можете забрать у них бронзовый свисток и медный ключик, которые им больше не понадобятся, и перейти реку.",
                    },
                },

                OpenOption = "BronzeWhistle",

                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [41] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ДРАКОН",
                                Mastery = 9,
                                Endurance = 4,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [42] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Далее" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 431, Text = "Поверите ему" },
                    new Option { Destination = 240, Text = "Осторожность никогда не помешает" },
                }
            },
            [45] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 345, Text = "Попросить книгу о самом волшебнике" },
                    new Option { Destination = 208, Text = "Попросить книгу о Принцессе" },
                    new Option { Destination = 139, Text = "Покинуть библиотеку" },
                }
            },
            [46] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 445, Text = "Направо" },
                    new Option { Destination = 524, Text = "Налево" },
                }
            },
            [47] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 101, Text = "К деревне" },
                    new Option { Destination = 262, Text = "К лесу" },
                    new Option { Destination = 187, Text = "Пойти поискать клад", OnlyIf = "PirateTreasure" },
                }
            },
            [48] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "К строению в центре двора " },
                    new Option { Destination = 218, Text = "К сторожке" },
                    new Option { Destination = 116, Text = "К маленькому домику слева" },
                }
            },
            [49] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 14, Text = "Быстро вернетесь на перекресток и пойдете в другую сторону" },
                    new Option { Destination = 404, Text = "Останетесь на дороге" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 310, Text = "Скажете, что вы идете в Черный замок сражаться с волшебником" },
                    new Option { Destination = 418, Text = "Попытаетесь осторожно выведать у него что-нибудь еще, не называя себя" },
                    new Option { Destination = 179, Text = "Покинете деревню" },
                }
            },
            [51] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Свернете к нему" },
                    new Option { Destination = 400, Text = "Поторопитесь к цели" },
                }
            },
            [52] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 373, Text = "Пойдете по тропинке" },
                    new Option { Destination = 121, Text = "Дальше по дороге" },
                }
            },
            [53] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 519, Text = "Попробуете с ними поговорить" },
                    new Option { Destination = 328, Text = "Будете драться" },
                }
            },
            [54] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",

                        Aftertext = "Если вы не удачливы, то паук затягивает вас на дерево, и вам приходится драться с ним. Во время боя уменьшайте вашу СИЛУ УДАРА на 1. Ведь вы не слишком-то привыкли драться на деревьях. Пользоваться заклятием Огня на дереве неразумно. Копии же просто негде будет поместиться. Однако вы можете воспользоваться заклятиями либо Силы, либо Слабости.",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГИГАНТСКИЙ ПАУК",
                                Mastery = 8,
                                Endurance = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 558, Text = "Если вы удачливы" },
                    new Option { Destination = 410, Text = "Заклятие Силы", OnlyIf = "ЗАКЛЯТИЕ СИЛЫ" },
                    new Option { Destination = 219, Text = "Заклятие Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 189, Text = "Если вы победили" },
                }
            },
            [55] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 121, Text = "Направо" },
                    new Option { Destination = 188, Text = "Налево" },
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [57] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [58] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СИЛЫ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Далее" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 240, Text = "Идти дальше вперед" },
                    new Option { Destination = 528, Text = "Свернуть с дороги" },
                }
            },
            [60] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 267, Text = "Достаньте перо павлина", OnlyIf = "PeacockFeather" },
                    new Option { Destination = 147, Text = "Кто сидит за следующей решеткой" },
                }
            },
            [62] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 457, Text = "Амулет" },
                    new Option { Destination = 156, Text = "Пояс" },
                    new Option { Destination = 367, Text = "Шкуру" },
                    new Option { Destination = 44, Text = "Отказаться" },
                }
            },
            [63] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Далее" },
                }
            },
            [64] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 340, Text = "Поискать убежища на ночь" },
                    new Option { Destination = 442, Text = "Пойти вперед" },
                }
            },
            [65] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "НАЧАЛЬНИК СТРАЖИ",
                                Mastery = 9,
                                Endurance = 5,
                            },
                        },

                        RoundsToWin = 3,
                        Aftertext = "Если вы победили Начальника стражи за три раунда, то всё хорошо. Если же нет, то Орки услышали подозрительный шум и не входили только потому, что ждали подмогу. Подоспевший отряд Гоблинов врывается раньше, чем вы успеваете добить вашего врага. И даже в самых кошмарных снах вы не видели того, что они сделали с вами…",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 468, Text = "Далее" },
                }
            },
            [66] = new Paragraph
            {
                OpenOption = "StorkFeather",

                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Далее" },
                }
            },
            [67] = new Paragraph
            {
                OpenOption = "SecondClue, DragonClaw",

                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Далее" },
                    new Option { Destination = 95, Text = "Это 'Пароль'", OnlyIf = "SecondClue" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 160, Text = "Далее" },
                }
            },
            [69] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [70] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Отойти от окна" },
                    new Option { Destination = 273, Text = "Воспользоваться заклятием Левитации", OnlyIf = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ" },
                }
            },
            [71] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 368, Text = "Правый" },
                    new Option { Destination = 482, Text = "Cредний" },
                    new Option { Destination = 536, Text = "Левый" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 340, Text = "Поискать убежище на ночь" },
                    new Option { Destination = 442, Text = "Пойти дальше" },
                }
            },
            [73] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 286, Text = "Налево" },
                    new Option { Destination = 470, Text = "Дальше" },
                }
            },
            [74] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [75] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 247, Text = "Драться" },
                }
            },
            [76] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Выйти" },
                    new Option { Destination = 486, Text = "Поговорить" },
                }
            },
            [77] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 378, Text = "Уйти" },
                }
            },
            [78] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 497, Text = "Далее" },
                }
            },
            [79] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 325, Text = "Выбежать в следующую дверь" },
                }
            },
            [80] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Открыть" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Вернуться" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 280, Text = "Сражаться с волшебником" },
                    new Option { Destination = 384, Text = "Освобождать Принцессу" },
                    new Option { Destination = 492, Text = "Забрели в лес случайно" },
                    new Option { Destination = 563, Text = "Обнажить меч" },
                }
            },
            [83] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 247, Text = "Вернуться" },
                }
            },
            [84] = new Paragraph
            {
                OpenOption = "BadgeWithAnEagle",

                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "Правую" },
                    new Option { Destination = 501, Text = "Левую" },
                }
            },
            [85] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СИЛЫ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Сражайтесь" },
                }
            },
            [86] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 263, Text = "Налево" },
                    new Option { Destination = 403, Text = "Направо" },
                }
            },
            [87] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Дальше в обход" },
                }
            },
            [88] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 177, Text = "Через кустарник" },
                    new Option { Destination = 212, Text = "К озеру" },
                }
            },
            [89] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 419, Text = "Поговорить с ними" },
                    new Option { Destination = 104, Text = "Предложить еду" },
                    new Option { Destination = 374, Text = "Драться" },
                }
            },
            [90] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Вернуться" },
                }
            },
            [91] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Уйти" },
                }
            },
            [92] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "Говорить с жёнами" },
                    new Option { Destination = 255, Text = "Принять другое решение" },
                }
            },
            [93] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЖЕНЩИНА-ВАМПИР",
                                Mastery = 11,
                                Endurance = 14,
                            },
                        },

                        Aftertext = "Если вы победили ее, то поторопитесь выйти в дверь, скрытую за портьерой, висящей слева от вас.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Выйти в дверь" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 264, Text = "Войти" },
                }
            },
            [95] = new Paragraph
            {
                OpenOption = "ThirdClue, Password",

                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Уйти" },
                    new Option { Destination = 163, Text = "Это 'Совесть'", OnlyIf = "ThirdClue" },
                }
            },
            [96] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Дальше по коридору" },
                }
            },
            [97] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 306, Text = "Удачлив" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 412, Text = "Далее" },
                }
            },
            [99] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [100] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 375, Text = "К реке" },
                    new Option { Destination = 424, Text = "К палаткам" },
                }
            },
            [101] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 405, Text = "Попить" },
                    new Option { Destination = 307, Text = "Наполнить флягу" },
                    new Option { Destination = 261, Text = "Пойти дальше" },
                }
            },
            [102] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГОБЛИН",
                                Mastery = 8,
                                Endurance = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [103] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Далее" },
                }
            },
            [104] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 520, Text = "Далее" },
                }
            },
            [105] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 413, Text = "К острову" },
                    new Option { Destination = 425, Text = "На другой берег" },
                }
            },
            [106] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 329, Text = "Направо" },
                    new Option { Destination = 432, Text = "Налево" },
                    new Option { Destination = 20, Text = "Прямо" },
                }
            },
            [107] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Поблагодарить и уйти" },
                    new Option { Destination = 209, Text = "Купить у него еду" },
                    new Option { Destination = 376, Text = "Попить воды" },
                    new Option { Destination = 15, Text = "Напасть на него" },
                }
            },
            [108] = new Paragraph
            {
                OpenOption = "GoldAmulet",

                Options = new List<Option>
                {
                    new Option { Destination = 407, Text = "Далее" },
                }
            },
            [109] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 453, Text = "Удачлив" },
                    new Option { Destination = 242, Text = "Нет" },
                }
            },
            [110] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 302, Text = "Подойти и выяснить" },
                    new Option { Destination = 234, Text = "Оставите умирать" },
                }
            },
            [111] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ КОПИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 309, Text = "Вернуться" },
                }
            },
            [112] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",

                        Aftertext = "Если вы удачливы, то вам удается оседлать коня Первого рыцаря, и ваша СИЛА УДАРА не изменяется, если же нет — драться все же придется, стоя на земле.",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЗЕЛЕНЫЙ РЫЦАРЬ",
                                Mastery = 10,
                                Endurance = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 414, Text = "Далее" },
                }
            },
            [113] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 446, Text = "В правую" },
                    new Option { Destination = 330, Text = "В левую" },
                    new Option { Destination = 397, Text = "В противоположной стене" },
                }
            },
            [114] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [115] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
               {
                    new Option { Destination = 424, Text = "Далее" },
               }
            },
            [116] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 16, Text = "Войдете" },
                    new Option { Destination = 416, Text = "К центральному строению" },
                    new Option { Destination = 4, Text = "К реке" },
                }
            },
            [117] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Войдете" },
                    new Option { Destination = 180, Text = "Направо" },
                    new Option { Destination = 334, Text = "Налево" },
                }
            },
            [118] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 190, Text = "Прямо к воротам" },
                    new Option { Destination = 236, Text = "Пойти налево в обход замка" },
                    new Option { Destination = 167, Text = "Есть волшебный пояс", OnlyIf = "MoleBelt" },
                }
            },
            [119] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 529, Text = "Защищаться" },
                    new Option { Destination = 447, Text = "Поговорить с ней" },
                }
            },
            [120] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 433, Text = "Заглянете в сундук" },
                    new Option { Destination = 227, Text = "Исследуете люк" },
                    new Option { Destination = 416, Text = "Уйти" },
                }
            },
            [121] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 525, Text = "К дереву" },
                    new Option { Destination = 210, Text = "Дальше по дороге" },
                }
            },
            [122] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Далее" },
                }
            },
            [123] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ РАЗБОЙНИК",
                                Mastery = 6,
                                Endurance = 4,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ РАЗБОЙНИК",
                                Mastery = 7,
                                Endurance = 8,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ РАЗБОЙНИК",
                                Mastery = 5,
                                Endurance = 5,
                            },
                        },

                        Aftertext = "Если вы победили их, то можете либо поскорее уйти, опасаясь, что поблизости могут быть их друзья, либо обшарить карманы убитых.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Уйти" },
                    new Option { Destination = 335, Text = "Обшарить карманы убитых" },
                }
            },
            [124] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Заплатите ему" },
                    new Option { Destination = 217, Text = "Поищите подарок" },
                    new Option { Destination = 106, Text = "Поблагодарите и уйдете" },
                    new Option { Destination = 518, Text = "Убить и осмотреть лавку" },
                }
            },
            [125] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 21, Text = "Войдете" },
                    new Option { Destination = 180, Text = "На тропинку направо, уходящую в лес" },
                    new Option { Destination = 334, Text = "Налево через сад" },
                }
            },
            [126] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Mastery",
                        Value = 1,
                    },
                },

                OpenOption = "GreenSword",

                Options = new List<Option>
                {
                    new Option { Destination = 434, Text = "Далее" },
                }
            },
            [127] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 225, Text = "Далее" },
                }
            },
            [128] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [129] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Идти в деревню" },
                    new Option { Destination = 223, Text = "Двинуться по лесу" },
                }
            },
            [130] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Осмотреться" },
                }
            },
            [131] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 305, Text = "Далее" },
                }
            },
            [132] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 336, Text = "Сорвать его" },
                    new Option { Destination = 24, Text = "Выбираться с острова" },
                }
            },
            [133] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 225, Text = "Далее" },
                }
            },
            [134] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [135] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 422, Text = "Прямо" },
                    new Option { Destination = 435, Text = "Налево" },
                }
            },
            [136] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 337, Text = "Далее" },
                }
            },
            [137] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 458, Text = "Три" },
                    new Option { Destination = 369, Text = "Шесть" },
                    new Option { Destination = 274, Text = "Восемь" },
                    new Option { Destination = 522, Text = "Отгонять ее мечом" },
                }
            },
            [138] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 243, Text = "Войдете в комнату" },
                    new Option { Destination = 39, Text = "Другой выбор" },
                }
            },
            [139] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 530, Text = "Понюхать цветы" },
                    new Option { Destination = 471, Text = "Сорвать лук" },
                    new Option { Destination = 268, Text = "Сорвать огурец" },
                    new Option { Destination = 80, Text = "Пройти через комнату" },
                }
            },
            [140] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Далее" },
                }
            },
            [141] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [142] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Согласиться с предложением" },
                    new Option { Destination = 106, Text = "Отказаться и уйти" },
                }
            },
            [143] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 531, Text = "Далее" },
                }
            },
            [144] = new Paragraph
            {
                OpenOption = "HiddenLadder",

                Options = new List<Option>
                {
                    new Option { Destination = 465, Text = "Далее" },
                }
            },
            [145] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 525, Text = "Свернете к дереву" },
                    new Option { Destination = 188, Text = "Пойдете дальше" },
                }
            },
            [146] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Драться" },
                }
            },
            [147] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 460, Text = "Показать серебряный свисток", OnlyIf = "SilverWhistle" },
                    new Option { Destination = 348, Text = "Возвратиться и пойти налево" },
                    new Option { Destination = 537, Text = "Возвратиться и пойти обратно" },
                }
            },
            [148] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 464, Text = "Далее" },
                }
            },
            [149] = new Paragraph
            {
                OpenOption = "FirstClue",

                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Далее" },
                    new Option { Destination = 67, Text = "Это 'Дракон'", OnlyIf = "FirstClue" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 269, Text = "Пройти до конца" },
                    new Option { Destination = 416, Text = "Вылезти назад и уйти" },
                }
            },
            [151] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [152] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "С правой" },
                    new Option { Destination = 275, Text = "С левой" },
                }
            },
            [153] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 538, Text = "Заклятие Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 370, Text = "Заклятие Иллюзии", OnlyIf = "ЗАКЛЯТИЕ ИЛЛЮЗИИ" },
                }
            },
            [154] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "Направо" },
                    new Option { Destination = 181, Text = "Налево" },
                    new Option { Destination = 445, Text = "Прямо" },
                }
            },
            [155] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 461, Text = "Прямо" },
                    new Option { Destination = 250, Text = "Направо" },
                }
            },
            [156] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Luck",
                        Value = 1,
                    },
                },

                OpenOption = "MoleBelt",

                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [157] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [158] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 484, Text = "Далее" },
                }
            },
            [159] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                    new Modification
                    {
                        Name = "Mastery",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 540, Text = "Средний сундук" },
                    new Option { Destination = 380, Text = "Маленький сундук" },
                    new Option { Destination = 39, Text = "Вернетесь обратно" },
                }
            },
            [160] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 371, Text = "Напасть на него" },
                    new Option { Destination = 276, Text = "Пойдете дальше" },
                    new Option { Destination = 135, Text = "Трое из Авенло", OnlyIf = "ThreeFromAvenlo" },
                }
            },
            [161] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Далее" },
                }
            },
            [162] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 611, Text = "Позвоните" },
                    new Option { Destination = 76, Text = "В другую дверь" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Далее" },
                }
            },
            [164] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СИЛЫ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 487, Text = "Драться" },
                }
            },
            [165] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 560, Text = "Осмотреть шкаф" },
                    new Option { Destination = 288, Text = "Осмотреть карты на столе" },
                    new Option { Destination = 493, Text = "Сделано и то, и другое" },
                    new Option { Destination = 152, Text = "Открыть тайный проход на лестницу", OnlyIf = "HiddenLadder" },
                }
            },
            [166] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 350, Text = "Далее" },
                }
            },
            [167] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [168] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [169] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 617, Text = "Принцесса уже разбужена" },
                    new Option { Destination = 612, Text = "Обратить внимание на побежденного врага" },
                    new Option { Destination = 560, Text = "Подойти к шкафу" },
                    new Option { Destination = 288, Text = "Посмотреть карты на столе" },
                    new Option { Destination = 165, Text = "Подойти к зеркалу" },
                }
            },
            [170] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Правую" },
                    new Option { Destination = 599, Text = "Левую" },
                }
            },
            [171] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Выскользнуть" },
                }
            },
            [172] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 347, Text = "Далее" },
                }
            },
            [173] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Далее" },
                }
            },
            [174] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [175] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее" },
                }
            },
            [176] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 5,
                    },
                },

                OpenOption = "Banana",

                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Уходить" },
                    new Option { Destination = 213, Text = "Поговорить с ним еще" },
                }
            },
            [177] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [178] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Далее" },
                }
            },
            [179] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "По ней" },
                    new Option { Destination = 377, Text = "По дороге" },
                }
            },
            [180] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 400, Text = "Далее" },
                }
            },
            [181] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Напасть первым" },
                    new Option { Destination = 17, Text = "Поговорить с ними" },
                }
            },
            [182] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 25, Text = "Осмотрите буфет" },
                    new Option { Destination = 224, Text = "Осмотрите люк" },
                    new Option { Destination = 22, Text = "Осмотрите трон" },
                    new Option { Destination = 427, Text = "Через дверь в той же стене, в которой был вход" },
                    new Option { Destination = 398, Text = "Через правую дверь" },
                    new Option { Destination = 206, Text = "Через левую дверь" },
                    new Option { Destination = 350, Text = "Через среднюю дверь" },
                }
            },
            [183] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ЗЕЛЕНЫЙ РЫЦАРЬ",
                                Mastery = 10,
                                Endurance = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ЗЕЛЕНЫЙ РЫЦАРЬ",
                                Mastery = 10,
                                Endurance = 10,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 436, Text = "Далее" },
                }
            },
            [184] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "Уйти сразу" },
                    new Option { Destination = 351, Text = "Как лучше попасть в Черный замок" },
                    new Option { Destination = 448, Text = "Где можно купить еду" },
                    new Option { Destination = 526, Text = "Что находится поблизости от деревни" },
                }
            },
            [185] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 329, Text = "Направо" },
                    new Option { Destination = 432, Text = "Налево" },
                    new Option { Destination = 20, Text = "Прямо" },
                }
            },
            [186] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 456, Text = "Заплатите" },
                    new Option { Destination = 229, Text = "Откажетесь и уйдете" },
                }
            },
            [187] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Luck",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 47, Text = "Далее" },
                }
            },
            [188] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Налево" },
                    new Option { Destination = 235, Text = "Направо" },
                }
            },
            [189] = new Paragraph
            {
                OpenOption = "GoldenWhistle, Diamond",

                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Далее" },
                }
            },
            [190] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 245, Text = "Бродячий торговец" },
                    new Option { Destination = 449, Text = "Азартный игрок" },
                    new Option { Destination = 26, Text = "Собираетесь наняться служить в его армии" },
                    new Option { Destination = 341, Text = "Сражаться" },
                    new Option { Destination = 618, Text = "Сказать пароль домика", OnlyIf = "Password" },
                }
            },
            [191] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 532, Text = "Далее" },
                }
            },
            [192] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГОБЛИН",
                                Mastery = 7,
                                Endurance = 9,
                            },
                        },

                        Aftertext = "Если вы убили его, то можете взять бронзовый свисток из его кармана и отправляться дальше по тропинке в лес.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 437, Text = "Далее" },
                }
            },
            [193] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 230, Text = "За правую" },
                    new Option { Destination = 352, Text = "За левую" },
                }
            },
            [194] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 27, Text = "Перо павлина", OnlyIf = "PeacockFeather" },
                    new Option { Destination = 270, Text = "Серебряный браслет", OnlyIf = "SilverBracelet" },
                    new Option { Destination = 533, Text = "Белую стрелу", OnlyIf = "WhiteArrow" },
                    new Option { Destination = 137, Text = "Дать денег" },
                    new Option { Destination = 522, Text = "Сразиться" },
                }
            },
            [195] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [196] = new Paragraph
            {
                OpenOption = "RingWithRuby",

                Options = new List<Option>
                {
                    new Option { Destination = 560, Text = "Шкаф" },
                    new Option { Destination = 288, Text = "Стол" },
                    new Option { Destination = 165, Text = "Зеркало" },
                }
            },
            [197] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 434, Text = "Далее" },
                }
            },
            [198] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 346, Text = "Далее" },
                }
            },
            [199] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 107, Text = "Войдете" },
                    new Option { Destination = 304, Text = "По тропинке направо" },
                }
            },
            [200] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ИЛЛЮЗИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 318, Text = "Осмотреть ларь в углу" },
                    new Option { Destination = 193, Text = "Сразу уйти" },
                }
            },
            [201] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ПЛАВАНИЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [202] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 186, Text = "Попросите проводить" },
                    new Option { Destination = 229, Text = "Поблагодарите за обед и уйдете" },
                }
            },
            [203] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 440, Text = "Свернете" },
                    new Option { Destination = 140, Text = "Пойдете прямо" },
                }
            },
            [204] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 353, Text = "Далее" },
                }
            },
            [205] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -3,
                    },
                    new Modification
                    {
                        Name = "Mastery",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Далее" },
                }
            },
            [206] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [207] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [208] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
                }
            },
            [209] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Далее" },
                }
            },
            [210] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 462, Text = "Далее" },
                }
            },
            [211] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "Заклятием Левитации", OnlyIf = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ" },
                    new Option { Destination = 313, Text = "Заклятием Плавания", OnlyIf = "ЗАКЛЯТИЕ ПЛАВАНИЯ" },
                    new Option { Destination = 423, Text = "Пойти по тропинке" },
                }
            },
            [212] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "Заклятием Плавания", OnlyIf = "ЗАКЛЯТИЕ ПЛАВАНИЯ" },
                    new Option { Destination = 105, Text = "Заклятием Левитации", OnlyIf = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ" },
                    new Option { Destination = 177, Text = "Вернуться и попробовать пройти через кустарник" },
                }
            },
            [213] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 18, Text = "Ищите Черный замок" },
                    new Option { Destination = 124, Text = "Идете сражаться с волшебником" },
                    new Option { Destination = 23, Text = "Будить Принцессу" },
                    new Option { Destination = 438, Text = "Гуляете по лесу" },
                }
            },
            [214] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 401, Text = "Сесть на коня" },
                    new Option { Destination = 378, Text = "Двинуться дальше" },
                }
            },
            [215] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ОБЕЗЬЯНА",
                                Mastery = 9,
                                Endurance = 14,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 408, Text = "Далее" },
                }
            },
            [216] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Далее" },
                }
            },
            [217] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 354, Text = "Белую стрелу", OnlyIf = "WhiteArrow" },
                    new Option { Destination = 28, Text = "Бриллиант", OnlyIf = "Diamond" },
                    new Option { Destination = 142, Text = "Серебряный свисток", OnlyIf = "SilverWhistle" },
                    new Option { Destination = 106, Text = "Уйти" },
                }
            },
            [218] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 463, Text = "Удачлив" },
                    new Option { Destination = 539, Text = "Нет" },
                }
            },
            [219] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГИГАНТСКИЙ ПАУК",
                                Mastery = 5,
                                Endurance = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Далее" },
                }
            },
            [220] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 548, Text = "Войдете в конюшню" },
                    new Option { Destination = 416, Text = "Пойдете к центральному зданию" },
                }
            },
            [221] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 535, Text = "Идете в Черный замок служить" },
                    new Option { Destination = 148, Text = "Сражаться с волшебником" },
                    new Option { Destination = 464, Text = "Освобождать Принцессу" },
                    new Option { Destination = 55, Text = "Зашли случайно" },
                }
            },
            [222] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 167, Text = "Есть волшебный пояс", OnlyIf = "MoleBelt" },
                    new Option { Destination = 190, Text = "К стене впереди" },
                    new Option { Destination = 236, Text = "Направиться влево, в обход замка" },
                }
            },
            [223] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Отправиться по дороге" },
                    new Option { Destination = 184, Text = "Войти в деревню" },
                    new Option { Destination = 29, Text = "Пересечь дорогу и продолжать идти" },
                }
            },
            [224] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 491, Text = "Воспользоваться подъёмником" },
                    new Option { Destination = 427, Text = "Уйти через дверь в той же стене" },
                    new Option { Destination = 398, Text = "Уйти через правую дверь" },
                    new Option { Destination = 206, Text = "Уйти через левую дверь" },
                    new Option { Destination = 350, Text = "Уйти через среднюю дверь" },
                }
            },
            [225] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 144, Text = "Есть шкура оленя", OnlyIf = "DeerSkin" },
                    new Option { Destination = 465, Text = "Идти дальше" },
                }
            },
            [226] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [227] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Медный ключик", OnlyIf = "CopperKey" },
                    new Option { Destination = 150, Text = "Кусок металла", OnlyIf = "PieceOfMetal" },
                    new Option { Destination = 473, Text = "Фигурный ключ", OnlyIf = "CurlyKey" },
                    new Option { Destination = 416, Text = "Покинуть домик" },
                }
            },
            [228] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Уйти по дороге" },
                    new Option { Destination = 561, Text = "Переплыть озеро" },
                }
            },
            [229] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Направо от входа в дом" },
                    new Option { Destination = 334, Text = "Налево, проходя через сад" },
                }
            },
            [230] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 549, Text = "Пойдете направо" },
                    new Option { Destination = 466, Text = "Пройти прямо, ни за что не держась" },
                }
            },
            [231] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 406, Text = "Далее" },
                }
            },
            [232] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Далее" },
                }
            },
            [233] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [234] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 47, Text = "По тропинке" },
                    new Option { Destination = 82, Text = "По дороге" },
                }
            },
            [235] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "Далее" },
                }
            },
            [236] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 331, Text = "Пойти дальше" },
                    new Option { Destination = 87, Text = "Использовать заклятие Левитации", OnlyIf = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ" },
                }
            },
            [237] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 108, Text = "Подойдете посмотреть" },
                    new Option { Destination = 407, Text = "Пойдете дальше" },
                }
            },
            [238] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Направо" },
                    new Option { Destination = 531, Text = "Налево" },
                }
            },
            [239] = new Paragraph
            {
                OpenOption = "SilverWhistle",

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 5,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 531, Text = "Свернуть на нее" },
                    new Option { Destination = 64, Text = "Пойти дальше" },
                }
            },
            [240] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 462, Text = "Пойти прямо" },
                    new Option { Destination = 145, Text = "Пойти налево" },
                }
            },
            [241] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 485, Text = "Далее" },
                }
            },
            [242] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Далее" },
                }
            },
            [243] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПРИЗРАК",
                                Mastery = 10,
                                Endurance = 9,
                            },
                        },

                        Aftertext = "Если вы победили Призрака, то он исчезает, а вы можете либо уйти от греха подальше (вернувшись и выбрав что-нибудь более приятное), либо осмотреть сундуки у противоположной стены. С какого вы начнете: с большого, среднего или маленького?",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Уйти от греха подальше" },
                    new Option { Destination = 474, Text = "Осмотреть большой сундук" },
                    new Option { Destination = 540, Text = "Осмотреть средний сундук" },
                    new Option { Destination = 380, Text = "Осмотреть маленький сундук" },
                }
            },
            [244] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться с пауком",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПАУК",
                                Mastery = 8,
                                Endurance = 8,
                            },
                        },

                        Aftertext = "Если вы победили паука, придите в себя, переведите дух и идите по дороге дальше.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Далее" },
                }
            },
            [245] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Есть шкура лисы" },
                    new Option { Destination = 341, Text = "Сражаться" },
                }
            },
            [246] = new Paragraph
            {
                OpenOption = "Pass",

                Options = new List<Option>
                {
                    new Option { Destination = 550, Text = "Скажете ему, что вам надо на 2-й этаж" },
                    new Option { Destination = 39, Text = "Уйти через противоположную дверь" },
                }
            },
            [247] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ДУХ МЕРТВЫХ",
                                Mastery = 10,
                                Endurance = 12,
                            },
                        },

                        Aftertext = "Если вам удалось победить, то можете оглядеться.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 83, Text = "Попытаться убежать" },
                    new Option { Destination = 381, Text = "Если вам удалось победить" },
                }
            },
            [248] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [249] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 562, Text = "Пригласили в замок и вы просто ошиблись дверью" },
                    new Option { Destination = 315, Text = "Вам поручили что-то передать ему" },
                }
            },
            [250] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Далее" },
                }
            },
            [251] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "Далее" },
                }
            },
            [252] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 564, Text = "Заклятие Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 541, Text = "Заклятие Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 494, Text = "Сразитесь с ними" },
                    new Option { Destination = 364, Text = "Если есть меч Зеленого рыцаря" },
                    new Option { Destination = 272, Text = "Предъявить пропуск", OnlyIf = "Pass" },
                }
            },
            [253] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [254] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ПЛАВАНИЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 366, Text = "Далее" },
                }
            },
            [255] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 551, Text = "Подарить ей какой-нибудь подарок" },
                    new Option { Destination = 79, Text = "1 золотой" },
                    new Option { Destination = 382, Text = "4 золотых" },
                    new Option { Destination = 495, Text = "6 золотых" },
                    new Option { Destination = 198, Text = "Извиниться за вторжение" },
                }
            },
            [256] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Далее" },
                }
            },
            [257] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЛЕВ",
                                Mastery = 9,
                                Endurance = 15,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 565, Text = "Знаете пароль" },
                    new Option { Destination = 264, Text = "Если лев мёртв" },
                }
            },
            [258] = new Paragraph
            {
                OpenOption = "RingWithEmerald",

                Options = new List<Option>
                {
                    new Option { Destination = 560, Text = "Шкаф" },
                    new Option { Destination = 165, Text = "Зеркало" },
                    new Option { Destination = 288, Text = "Стол" },
                }
            },
            [259] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Далее" },
                }
            },
            [260] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 116, Text = "К нему" },
                    new Option { Destination = 4, Text = "К реке" },
                    new Option { Destination = 416, Text = "К зданию в центре двора" },
                }
            },
            [261] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 50, Text = "Поговорить" },
                    new Option { Destination = 179, Text = "Пойти дальше" },
                }
            },
            [262] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Luck",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 117, Text = "Направо" },
                    new Option { Destination = 303, Text = "Налево" },
                }
            },
            [263] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Взобраться" },
                    new Option { Destination = 19, Text = "Пойти дальше" },
                }
            },
            [264] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 191, Text = "По лестнице вверх" },
                    new Option { Destination = 30, Text = "По лестнице вниз" },
                    new Option { Destination = 53, Text = "В дверь направо" },
                    new Option { Destination = 467, Text = "В дверь налево" },
                }
            },
            [265] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГИЕНА",
                                Mastery = 6,
                                Endurance = 6,
                            },
                        },

                        Aftertext = "Если вы убили ее, можете двигаться дальше.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "Далее" },
                }
            },
            [266] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 553, Text = "Далее" },
                }
            },
            [267] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 147, Text = "Далее" },
                }
            },
            [268] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [269] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 264, Text = "Далее" },
                }
            },
            [270] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 542, Text = "В дверь справа" },
                    new Option { Destination = 252, Text = "В дверь в противоположной от входа стене" },
                }
            },
            [271] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 341, Text = "Далее" },
                }
            },
            [272] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 480, Text = "Далее" },
                }
            },
            [273] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Вернетесь обратно" },
                    new Option { Destination = 483, Text = "Влететь в то окно, которое выше" },
                    new Option { Destination = 566, Text = "Влететь в то окно, которое под ним" },
                }
            },
            [274] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 542, Text = "Повинуетесь и войдете" },
                    new Option { Destination = 522, Text = "Попробуете отогнать старуху мечом" },
                }
            },
            [275] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 279, Text = "Подойти к зеркалу" },
                    new Option { Destination = 385, Text = "Подойти к столикам" },
                    new Option { Destination = 475, Text = "Воспользоваться золотым апельсином", OnlyIf = "GoldenOrange" },
                    new Option { Destination = 324, Text = "Воспользоваться кольцом с рубином", OnlyIf = "RingWithRuby" },
                    new Option { Destination = 444, Text = "Воспользоваться кольцом с изумрудом", OnlyIf = "RingWithEmerald" },
                }
            },
            [276] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [277] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 554, Text = "Подсвечник", OnlyIf = "Candlestick" },
                    new Option { Destination = 78, Text = "Перо павлина", OnlyIf = "PeacockFeather" },
                    new Option { Destination = 386, Text = "Серебряный сосуд", OnlyIf = "SilverVessel" },
                    new Option { Destination = 429, Text = "Золотое ожерелье", OnlyIf = "GoldNecklace" },
                    new Option { Destination = 287, Text = "Зажечь свечу", OnlyIf = "CandleAndFlint" },
                    new Option { Destination = 572, Text = "Нет ни одного из этих предметов" },
                }
            },
            [278] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [279] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Далее" },
                    new Option { Destination = 266, Text = "Открыть тайный проход на лестницу", OnlyIf = "HiddenLadder" },
                }
            },
            [280] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 555, Text = "Далее" },
                }
            },
            [281] = new Paragraph
            {
                OpenOption = "Beads",

                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "Правую" },
                    new Option { Destination = 501, Text = "Левую" },
                }
            },
            [282] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 368, Text = "Правый" },
                    new Option { Destination = 536, Text = "Левый" },
                }
            },
            [283] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 387, Text = "Зеркальце", OnlyIf = "Mirror" },
                    new Option { Destination = 69, Text = "Бронзовый свисток", OnlyIf = "BronzeWhistle" },
                    new Option { Destination = 233, Text = "Золотое кольцо", OnlyIf = "GoldRing" },
                    new Option { Destination = 567, Text = "Сражаться" },
                }
            },
            [284] = new Paragraph
            {
                OpenOption = "GoldNecklace",

                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "Открыть большой сундук" },
                    new Option { Destination = 380, Text = "Открыть маленький сундук" },
                    new Option { Destination = 39, Text = "Уйти от греха подальше" },
                }
            },
            [285] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 321, Text = "Далее" },
                }
            },
            [286] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 129, Text = "Далее" },
                }
            },
            [287] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 497, Text = "Далее" },
                }
            },
            [288] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 165, Text = "Подойти к зеркалу" },
                    new Option { Destination = 560, Text = "Осмотреть шкаф" },
                    new Option { Destination = 493, Text = "Если вы уже сделано и то, и другое" },
                }
            },
            [289] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 487, Text = "Далее" },
                }
            },
            [290] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГОБЛИН",
                                Mastery = 6,
                                Endurance = 9,
                            },
                        },

                        Aftertext = "Если победитель вы, то можете забрать у ваших противников бронзовый свисток и медный ключик, которые им больше не понадобятся, и перейти реку.",
                    },
                },

                OpenOption = "BronzeWhistle, CopperKey",

                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [291] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 568, Text = "Меч" },
                    new Option { Destination = 498, Text = "Щит" },
                    new Option { Destination = 389, Text = "Выпьете жидкость из бутылочки" },
                    new Option { Destination = 300, Text = "Закрыть дверь и снова запереть ее" },
                }
            },
            [292] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 503, Text = "Выяснить, что ей нужно" },
                    new Option { Destination = 265, Text = "Сразиться с ней" },
                }
            },
            [293] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "БАРЛАД ДЭРТ",
                                Mastery = 14,
                                Endurance = 12,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Далее" },
                }
            },
            [294] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [295] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 379, Text = "Далее" },
                }
            },
            [296] = new Paragraph
            {
                OpenOption = "PerfumeBottle",

                Options = new List<Option>
                {
                    new Option { Destination = 255, Text = "Поговорить с женами мага" },
                    new Option { Destination = 325, Text = "Выйти из гарема" },
                }
            },
            [297] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Далее" },
                }
            },
            [298] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [299] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 567, Text = "Далее" },
                }
            },
            [300] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 337, Text = "В ту, что перед вами" },
                    new Option { Destination = 595, Text = "в ту, что слева" },
                }
            },
            [301] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться c двумя рыцарями",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ РЫЦАРЬ",
                                Mastery = 10,
                                Endurance = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ РЫЦАРЬ",
                                Mastery = 10,
                                Endurance = 10,
                            },
                        },

                        Aftertext = "А через 6 раундов атаки к ним присоединится еще один рыцарь.",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться c третьим рыцарем",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ТРЕТИЙ РЫЦАРЬ",
                                Mastery = 10,
                                Endurance = 10,
                            },
                        },

                        Aftertext = "Когда вы победите двух рыцарей, в дело вступит Капитан.",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться c капитаном",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "КАПИТАН РЫЦАРЕЙ",
                                Mastery = 12,
                                Endurance = 12,
                            },
                        },

                        Aftertext = "Если же произойдет чудо и вы сможете перебить всю охрану, то поднимитесь по лестнице на небольшой балкончик, куда выходят две двери. Вы пойдете в правую дверь или в левую?",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 170, Text = "Есть Оберег", OnlyIf = "Amulet" },
                    new Option { Destination = 606, Text = "Есть серебряный сосуд", OnlyIf = "SilverVessel" },
                    new Option { Destination = 594, Text = "В правую дверь" },
                    new Option { Destination = 599, Text = "В левую дверь" },
                }
            },
            [302] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 11, Text = "Дадите напиться" },
                    new Option { Destination = 234, Text = "Откажете" },
                }
            },
            [303] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Идти дальше" },
                    new Option { Destination = 117, Text = "Вернуться" },
                }
            },
            [304] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "Пойдете дальше" },
                    new Option { Destination = 12, Text = "Cвернете к озеру" },
                }
            },
            [305] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 412, Text = "Далее" },
                }
            },
            [306] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 52, Text = "Далее" },
                }
            },
            [307] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 261, Text = "Далее" },
                }
            },
            [308] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "К центральному строению" },
                    new Option { Destination = 220, Text = "К низкому зданию" },
                    new Option { Destination = 4, Text = "К реке" },
                }
            },
            [309] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЗЕЛЕНЫЙ РЫЦАРЬ",
                                Mastery = 10,
                                Endurance = 10,
                            },
                        },
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 111, Text = "Наложить заклятие Копии", OnlyIf = "ЗАКЛЯТИЕ КОПИИ" },
                    new Option { Destination = 126, Text = "Если победили" },
                }
            },
            [310] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Далее" },
                }
            },
            [311] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -1,
                    },
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ПЛАВАНИЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "Да, догадались" },
                }
            },
            [312] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [313] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ПЛАВАНИЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 222, Text = "Далее" },
                }
            },
            [314] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Далее" },
                }
            },
            [315] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [316] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ДРАКОН",
                                Mastery = 12,
                                Endurance = 8,
                            },
                        },

                        WoundsToWin = 2,
                        Aftertext = "Удастся ли вам дважды ранить Дракона?",
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -4,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 513, Text = "Удалось" },
                }
            },
            [317] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 127, Text = "Сделаете это" },
                    new Option { Destination = 225, Text = "Откажетесь и посмотрите" },
                }
            },
            [318] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 193, Text = "Далее" },
                }
            },
            [319] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Бродячий торговец" },
                    new Option { Destination = 439, Text = "Азартный игрок" },
                    new Option { Destination = 146, Text = "Собираетесь наняться в армию чародея" },
                    new Option { Destination = 122, Text = "Сказать пароль домика", OnlyIf = "Password" },
                }
            },
            [320] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 355, Text = "Рискнете предложить что-то в виде пропуска" },
                    new Option { Destination = 543, Text = "Драться" },
                }
            },
            [321] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "Попробуете наложить заклятие" },
                    new Option { Destination = 247, Text = "Сражаться" },
                }
            },
            [322] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 277, Text = "Прямо" },
                    new Option { Destination = 556, Text = "Направо" },
                }
            },
            [323] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 522, Text = "Замахнетесь" },
                    new Option { Destination = 137, Text = "Дадите денег" },
                    new Option { Destination = 194, Text = "Предложите подарок" },
                    new Option { Destination = 343, Text = "Предъявить пропуск", OnlyIf = "Pass" },
                }
            },
            [324] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 589, Text = "Если волшебник жив" },
                    new Option { Destination = 617, Text = "Если же он мертв" },
                }
            },
            [325] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 162, Text = "Направо" },
                    new Option { Destination = 76, Text = "Налево" },
                }
            },
            [326] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВОДЯНОЙ",
                                Mastery = 7,
                                Endurance = 7,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 573, Text = "Если вы победили" },
                    new Option { Destination = 504, Text = "Покинуть таверну и бежать" },
                }
            },
            [327] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 443, Text = "Далее" },
                }
            },
            [328] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ОРК",
                                Mastery = 8,
                                Endurance = 5,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ОРК",
                                Mastery = 7,
                                Endurance = 7,
                            },
                        },

                        Aftertext = "Убиты ли оба врага за 8 раундов?",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 505, Text = "Если убили обоих врагов" },
                    new Option { Destination = 248, Text = "Если они живы" },
                }
            },
            [329] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 33, Text = "Далее" },
                }
            },
            [330] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 532, Text = "Далее" },
                }
            },
            [331] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 305, Text = "Углубиться в лес" },
                    new Option { Destination = 195, Text = "Продолжать идти прямо" },
                }
            },
            [332] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 250, Text = "Прямо" },
                    new Option { Destination = 461, Text = "Налево" },
                }
            },
            [333] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 149, Text = "Поговорить с ним" },
                    new Option { Destination = 98, Text = "Пойти дальше по дорожке" },
                }
            },
            [334] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [335] = new Paragraph
            {
                OpenOption = "StorkFeather",

                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Далее" },
                }
            },
            [336] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Luck",
                        Value = 1,
                    },
                },

                OpenOption = "GoldenOrange",

                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Далее" },
                }
            },
            [337] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 613, Text = "Подкупить его" },
                    new Option { Destination = 249, Text = "Поговорить с ним" },
                    new Option { Destination = 65, Text = "Драться" },
                }
            },
            [338] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Направо" },
                    new Option { Destination = 231, Text = "Прямо" },
                }
            },
            [339] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Далее" },
                }
            },
            [340] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 509, Text = "Далее" },
                }
            },
            [341] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 164, Text = "Заклятие Силы", OnlyIf = "ЗАКЛЯТИЕ СИЛЫ" },
                    new Option { Destination = 289, Text = "Заклятие Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 506, Text = "Заклятие Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 487, Text = "Драться" },
                }
            },
            [342] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [343] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 252, Text = "Прямо" },
                    new Option { Destination = 542, Text = "Направо" },
                }
            },
            [344] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Прямо к воротам замка" },
                    new Option { Destination = 232, Text = "В обход" },
                }
            },
            [345] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
                }
            },
            [346] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 153, Text = "Воспользоваться заклятием" },
                    new Option { Destination = 68, Text = "Посмотреть, что будет дальше" },
                }
            },
            [347] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [348] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [349] = new Paragraph
            {
                OpenOption = "Comb",

                Options = new List<Option>
                {
                    new Option { Destination = 615, Text = "Пойдете за ней" },
                    new Option { Destination = 305, Text = "Пойдете по тропинке" },
                    new Option { Destination = 210, Text = "Вернетесь на дорогу" },
                }
            },
            [350] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 545, Text = "Взять серебряный сосуд" },
                    new Option { Destination = 253, Text = "Взять стеклянный сосуд" },
                    new Option { Destination = 39, Text = "Выйти в противоположную дверь " },
                }
            },
            [351] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 574, Text = "Принять предложение" },
                    new Option { Destination = 235, Text = "Отказаться и покинуть деревню на юг" },
                    new Option { Destination = 2, Text = "Отказаться и покинуть деревню на запад" },
                }
            },
            [352] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 322, Text = "Далее" },
                }
            },
            [353] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 154, Text = "По тропинке направо" },
                    new Option { Destination = 237, Text = "По тропинке налево" },
                    new Option { Destination = 181, Text = "По дороге прямо" },
                }
            },
            [354] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 327, Text = "Заплатить деньги" },
                    new Option { Destination = 518, Text = "Драться" },
                    new Option { Destination = 106, Text = "Уйти" },
                }
            },
            [355] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 233, Text = "Золотое кольцо", OnlyIf = "GoldRing" },
                    new Option { Destination = 69, Text = "Бронзовый свисток", OnlyIf = "BronzeWhistle" },
                    new Option { Destination = 168, Text = "Четки", OnlyIf = "Beads" },
                    new Option { Destination = 543, Text = "Драться" },
                }
            },
            [356] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ТРОЛЛЬ",
                                Mastery = 9,
                                Endurance = 14,
                            },
                        },

                        Aftertext = "Если вы победили Тролля, можете взять из его кармана Золотое кольцо. Добавьте себе 1 УДАЧУ. Теперь уходите.",
                    },
                },

                OpenOption = "GoldRing",

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Luck",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 207, Text = "Далее" },
                }
            },
            [357] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СИЛЫ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Далее" },
                }
            },
            [358] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 510, Text = "Направо" },
                    new Option { Destination = 214, Text = "Налево" },
                    new Option { Destination = 38, Text = "Прямо" },
                }
            },
            [359] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [360] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Пойти по другой дороге" },
                    new Option { Destination = 184, Text = "Войти в деревню" },
                }
            },
            [361] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [362] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [363] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [364] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 480, Text = "Далее" },
                }
            },
            [365] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 543, Text = "Далее" },
                }
            },
            [366] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Обойти ее вокруг и поискать вход" },
                    new Option { Destination = 116, Text = "К заброшенному домику" },
                }
            },
            [367] = new Paragraph
            {
                OpenOption = "FoxSkin",
                
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [368] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [369] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 542, Text = "В эту дверь" },
                    new Option { Destination = 252, Text = "В ту, что прямо" },
                }
            },
            [370] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ИЛЛЮЗИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 538, Text = "Заклятие Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 68, Text = "Ждать" },
                }
            },
            [371] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 276, Text = "Далее" },
                }
            },
            [372] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ПЛАВАНИЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Далее" },
                }
            },
            [373] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 221, Text = "Войти в хижину" },
                    new Option { Destination = 121, Text = "Вернуться на дорогу" },
                }
            },
            [374] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ЧЕЛОВЕК",
                                Mastery = 8,
                                Endurance = 5,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ЧЕЛОВЕК",
                                Mastery = 6,
                                Endurance = 9,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 34, Text = "Далее" },
                }
            },
            [375] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 254, Text = "Заклятье Плавания", OnlyIf = "ЗАКЛЯТИЕ ПЛАВАНИЯ" },
                    new Option { Destination = 508, Text = "Заклятье Левитации", OnlyIf = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ" },
                    new Option { Destination = 311, Text = "Попробовать проплыть по течению под замок" },
                    new Option { Destination = 424, Text = "К палаткам" },
                }
            },
            [376] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Далее" },
                }
            },
            [377] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 197, Text = "Спрятаться" },
                    new Option { Destination = 309, Text = "Поговорить со всадником" },
                }
            },
            [378] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 415, Text = "Спрыгнуть" },
                    new Option { Destination = 129, Text = "Идти дальше" },
                }
            },
            [379] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [380] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 474, Text = "Посмотреть большой сундук" },
                    new Option { Destination = 540, Text = "Посмотреть средний сундук" },
                    new Option { Destination = 39, Text = "Вернуться в большую залу" },
                }
            },
            [381] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 84, Text = "К саркофагу со статуей" },
                    new Option { Destination = 281, Text = "К саркофагу с крестом" },
                    new Option { Destination = 496, Text = "К могиле" },
                    new Option { Destination = 547, Text = "Уйти через правую дверь" },
                    new Option { Destination = 501, Text = "Уйти через левую дверь" },
                }
            },
            [382] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 575, Text = "Хотите пойти наверх" },
                    new Option { Destination = 511, Text = "Хотите пойти вниз" },
                    new Option { Destination = 325, Text = "Выйти из гарема" },
                }
            },
            [383] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 576, Text = "Пойти за ним" },
                    new Option { Destination = 353, Text = "Вернуться и выбрать" },
                }
            },
            [384] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 555, Text = "Далее" },
                }
            },
            [385] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Далее" },
                }
            },
            [386] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "Далее" },
                }
            },
            [387] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [388] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 577, Text = "Выхватить меч" },
                    new Option { Destination = 293, Text = "Стоять и смотреть" },
                    new Option { Destination = 605, Text = "Есть золотое кольцо", OnlyIf = "GoldRing" },
                    new Option { Destination = 605, Text = "Есть золотой амулет", OnlyIf = "GoldAmulet" },
                }
            },
            [389] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Далее" },
                }
            },
            [390] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 569, Text = "Далее" },
                }
            },
            [391] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Далее" },
                }
            },
            [392] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [393] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 291, Text = "Есть медный ключик", OnlyIf = "CopperKey" },
                    new Option { Destination = 337, Text = "Попробуете открыть дверь за вами" },
                    new Option { Destination = 595, Text = "Попробуете открыть дверь слева" },
                }
            },
            [394] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 294, Text = "Бронзовый свисток", OnlyIf = "BronzeWhistle" },
                    new Option { Destination = 596, Text = "Зеркальце", OnlyIf = "Mirror" },
                    new Option { Destination = 171, Text = "Флакончик духов", OnlyIf = "PerfumeBottle" },
                    new Option { Destination = 93, Text = "Сражаться" },
                }
            },
            [395] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СИЛЫ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 567, Text = "Далее" },
                }
            },
            [396] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [397] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "В переднюю дверь" },
                    new Option { Destination = 512, Text = "В правую дверь" },
                    new Option { Destination = 315, Text = "Поговорить" },
                }
            },
            [398] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Далее" },
                }
            },
            [399] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Пройдете, ничего не сказав" },
                    new Option { Destination = 255, Text = "Попытаетесь поговорить" },
                }
            },
            [400] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 514, Text = "Пойдете, куда указывает стрелка" },
                    new Option { Destination = 35, Text = "Не будете сворачивать" },
                }
            },
            [401] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 546, Text = "Удачлив" },
                    new Option { Destination = 77, Text = "Нет " },
                }
            },
            [402] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 547, Text = "В правую" },
                    new Option { Destination = 501, Text = "В левую" },
                }
            },
            [403] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 199, Text = "Пойдете к таверне" },
                    new Option { Destination = 244, Text = "Свернете налево" },
                }
            },
            [404] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Выхватить меч" },
                    new Option { Destination = 36, Text = "Заклятие Силы", OnlyIf = "ЗАКЛЯТИЕ СИЛЫ" },
                    new Option { Destination = 334, Text = "Заклятие Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 7, Text = "Заклятие Копии", OnlyIf = "ЗАКЛЯТИЕ КОПИИ" },
                    new Option { Destination = 112, Text = "Заклятие Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                }
            },
            [405] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 307, Text = "Наполнить флягу" },
                    new Option { Destination = 261, Text = "Отправиться дальше" },
                }
            },
            [406] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Прямо" },
                    new Option { Destination = 515, Text = "К ульям" },
                }
            },
            [407] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [408] = new Paragraph
            {
                OpenOption = "Comb",

                Options = new List<Option>
                {
                    new Option { Destination = 131, Text = "Выпьете" },
                    new Option { Destination = 305, Text = "Уйдете по тропинке" },
                    new Option { Destination = 210, Text = "Вернетесь на дорогу" },
                }
            },
            [409] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 200, Text = "Заклятие Иллюзии", OnlyIf = "ЗАКЛЯТИЕ ИЛЛЮЗИИ" },
                    new Option { Destination = 114, Text = "Заклятие Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 56, Text = "Нет ни того, ни другого" },
                }
            },
            [410] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СИЛЫ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 54, Text = "Далее" },
                }
            },
            [411] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 578, Text = "Попробовать овощи" },
                    new Option { Destination = 80, Text = "Покинете комнату" },
                }
            },
            [412] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 316, Text = "Биться" },
                    new Option { Destination = 37, Text = "Заклятье Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 357, Text = "Заклятье Силы", OnlyIf = "ЗАКЛЯТИЕ СИЛЫ" },
                    new Option { Destination = 256, Text = "Заклятье Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 516, Text = "Предложить подарок" },
                }
            },
            [413] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Далее" },
                }
            },
            [414] = new Paragraph
            {
                OpenOption = "PeacockFeather",

                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Далее" },
                }
            },
            [415] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Удачлив" },
                    new Option { Destination = 579, Text = "Нет" },
                }
            },
            [416] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 424, Text = "Попытаться разузнать что-нибудь о замке" },
                    new Option { Destination = 257, Text = "Попробовать войти" },
                }
            },
            [417] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СИЛЫ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Далее" },
                }
            },
            [418] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "Далее" },
                }
            },
            [419] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 104, Text = "Дадите им поесть" },
                    new Option { Destination = 374, Text = "Откажете" },
                }
            },
            [420] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Направо" },
                    new Option { Destination = 214, Text = "Прямо" },
                }
            },
            [421] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [422] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 317, Text = "У вас есть кольцо" },
                    new Option { Destination = 133, Text = "У вас нет кольца" },
                }
            },
            [423] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Заклятье Силы", OnlyIf = "ЗАКЛЯТИЕ СИЛЫ" },
                    new Option { Destination = 580, Text = "Заклятье Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 290, Text = "Заклятье Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 40, Text = "Драться с ними" },
                }
            },
            [424] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 582, Text = "Поговорить с Орками" },
                    new Option { Destination = 74, Text = "Попытаться проникнуть в сердце замка" },
                }
            },
            [425] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 614, Text = "Наложить еще одно заклятие Левитации или Плавания" },
                    new Option { Destination = 581, Text = "Упасть в воду" },
                }
            },
            [426] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 123, Text = "Далее" },
                }
            },
            [427] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 583, Text = "Золотой свисток", OnlyIf = "GoldenWhistle" },
                    new Option { Destination = 597, Text = "Выломать ее" },
                    new Option { Destination = 398, Text = "Выйти в правую дверь" },
                    new Option { Destination = 206, Text = "Выйти в левую дверь" },
                    new Option { Destination = 350, Text = "Выйти в среднюю дверь" },
                }
            },
            [428] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 499, Text = "Заклятье Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 85, Text = "Заклятье Силы", OnlyIf = "ЗАКЛЯТИЕ СИЛЫ" },
                    new Option { Destination = 390, Text = "Заклятье Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 569, Text = "Сражаться мечом" },
                }
            },
            [429] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Далее" },
                }
            },
            [430] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 400, Text = "Далее" },
                }
            },
            [431] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",

                        Aftertext = "Если вы не удачливы, то дерево падает прямо на вас раньше, чем вы успеваете понять, что происходит. Доверчивость сослужила вам плохую службу. Вы забыли, что лес наводнен шпионами злого волшебника и попались на удочку одного из них. Ваше путешествие закончено.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 59, Text = "Удачлив" },
                    new Option { Destination = 0, Text = "Нет" },
                }
            },
            [432] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 421, Text = "Cвернуть" },
                    new Option { Destination = 73, Text = "Идти прямо" },
                }
            },
            [433] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 500, Text = "Выпить ее" },
                    new Option { Destination = 416, Text = "Уйти" },
                }
            },
            [434] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 584, Text = "Налево" },
                    new Option { Destination = 60, Text = "Направо" },
                }
            },
            [435] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [436] = new Paragraph
            {
                OpenOption = "PeacockFeather",

                Options = new List<Option>
                {
                    new Option { Destination = 338, Text = "Направо" },
                }
            },
            [437] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 531, Text = "Направо" },
                    new Option { Destination = 72, Text = "Налево" },
                }
            },
            [438] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Далее" },
                }
            },
            [439] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 259, Text = "В карты" },
                    new Option { Destination = 585, Text = "В кости" },
                }
            },
            [440] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 348, Text = "Направо" },
                    new Option { Destination = 61, Text = "Налево" },
                }
            },
            [441] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 378, Text = "Далее" },
                }
            },
            [442] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Далее" },
                }
            },
            [443] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Далее" },
                }
            },
            [444] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 502, Text = "Далее" },
                }
            },
            [445] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 102, Text = "Далее" },
                }
            },
            [446] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 328, Text = "Далее" },
                }
            },
            [447] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Исцелить медвежонка", OnlyIf = "ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ" },
                    new Option { Destination = 529, Text = "Драться с медведицей" },
                }
            },
            [448] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = 5,
                    },
                },

                OpenOption = "CurlyKey, Banana, PieceOfMetal, SilverBracelet",

                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "На юг" },
                    new Option { Destination = 2, Text = "На запад" },
                }
            },
            [449] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 396, Text = "В карты" },
                    new Option { Destination = 271, Text = "В кости" },
                }
            },
            [450] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 292, Text = "Налево" },
                    new Option { Destination = 51, Text = "Прямо" },
                    new Option { Destination = 586, Text = "Направо" },
                }
            },
            [451] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [452] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Далее" },
                }
            },
            [453] = new Paragraph
            {
                OpenOption = "StorkFeather",

                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Далее" },
                }
            },
            [454] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 319, Text = "Прямо к воротам" },
                    new Option { Destination = 232, Text = "В обход замка" },
                }
            },
            [455] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 141, Text = "Далее" },
                }
            },
            [456] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [457] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [458] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 252, Text = "В противоположную от входа" },
                    new Option { Destination = 542, Text = "В ту, что справа" },
                }
            },
            [459] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 399, Text = "Вернитесь" },
                    new Option { Destination = 325, Text = "Дальше по коридору" },
                    new Option { Destination = 255, Text = "Поговорите с женщинами" },
                }
            },
            [460] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 587, Text = "Сделаете это" },
                    new Option { Destination = 537, Text = "Дальше направо" },
                    new Option { Destination = 348, Text = "Дальше налево" },
                }
            },
            [461] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 340, Text = "Искать убежище на ночь" },
                    new Option { Destination = 442, Text = "Продолжите путь" },
                }
            },
            [462] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 178, Text = "Попытаться поймать" },
                    new Option { Destination = 333, Text = "Продолжить путь" },
                }
            },
            [463] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 158, Text = "Войти в сторожку" },
                    new Option { Destination = 308, Text = "К высокому зданию в центре двора" },
                }
            },
            [464] = new Paragraph
            {
                OpenOption = "DeerSkin",

                Options = new List<Option>
                {
                    new Option { Destination = 600, Text = "Согласны" },
                    new Option { Destination = 55, Text = "Уйти" },
                }
            },
            [465] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Далее" },
                }
            },
            [466] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 352, Text = "Далее" },
                }
            },
            [467] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Далее" },
                }
            },
            [468] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Далее" },
                }
            },
            [469] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 257, Text = "Далее" },
                }
            },
            [470] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 221, Text = "Войте внутрь" },
                    new Option { Destination = 55, Text = "Пройти мимо" },
                }
            },
            [471] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "Далее" },
                }
            },
            [472] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 588, Text = "Удачлив" },
                    new Option { Destination = 391, Text = "Нет" },
                }
            },
            [473] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Далее" },
                }
            },
            [474] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                    new Modification
                    {
                        Name = "Mastery",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 159, Text = "Еще раз попробовать" },
                    new Option { Destination = 540, Text = "Заняться средним сундуком" },
                    new Option { Destination = 380, Text = "Заняться маленьким сундуком" },
                    new Option { Destination = 39, Text = "Лучше уйти" },
                }
            },
            [475] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 617, Text = "Далее" },
                }
            },
            [476] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СИЛЫ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 347, Text = "Далее" },
                }
            },
            [477] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Luck",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 106, Text = "Далее" },
                }
            },
            [478] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 320, Text = "Пойдете по нему" },
                    new Option { Destination = 534, Text = "Вернетесь" },
                }
            },
            [479] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 590, Text = "Откроете крышку" },
                    new Option { Destination = 392, Text = "Идти дальше" },
                }
            },
            [480] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "О Зеленых рыцарях" },
                    new Option { Destination = 345, Text = "О самом волшебнике" },
                    new Option { Destination = 208, Text = "О Принцессе" },
                    new Option { Destination = 591, Text = "Уйти из библиотеки" },
                    new Option { Destination = 455, Text = "Трое из Авенло", OnlyIf = "ThreeFromAvenlo" },
                }
            },
            [481] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 60, Text = "Далее" },
                }
            },
            [482] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [483] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Вернуться обратно" },
                    new Option { Destination = 566, Text = "В окно немного ниже" },
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [484] = new Paragraph
            {
                OpenOption = "BronzeWhistle",

                Options = new List<Option>
                {
                    new Option { Destination = 308, Text = "Далее" },
                }
            },
            [485] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 472, Text = "Справа" },
                    new Option { Destination = 275, Text = "Слева" },
                }
            },
            [486] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 570, Text = "Спросить про сундук" },
                    new Option { Destination = 91, Text = "Попросить накормить вас" },
                    new Option { Destination = 295, Text = "Расспросить о замке" },
                }
            },
            [487] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться c первым орком",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ОРК",
                                Mastery = 10,
                                Endurance = 6,
                            },
                        },

                        Aftertext = "Если убили его за три раунда атаки, то сражайтесь с двумя остальными. Если же нет, то через три раунда оставшиеся два Орка приходят на помощь своему предводителю, и вам придется биться с тремя сразу.",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться с двумя другими",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ОРК",
                                Mastery = 7,
                                Endurance = 7,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ОРК",
                                Mastery = 7,
                                Endurance = 7,
                            },
                        },

                        Aftertext = "Если после всего этого вы еще остались живы, то можете переступить через тела своих поверженных противников и войти в ворота.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [488] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 454, Text = "Далее" },
                }
            },
            [489] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Правую" },
                    new Option { Destination = 547, Text = "Левую" },
                }
            },
            [490] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 255, Text = "Далее" },
                }
            },
            [491] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 571, Text = "Удачлив" },
                    new Option { Destination = 593, Text = "Нет" },
                }
            },
            [492] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Далее" },
                }
            },
            [493] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [494] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [495] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Luck",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 325, Text = "Далее" },
                }
            },
            [496] = new Paragraph
            {
                OpenOption = "Candlestick",

                Options = new List<Option>
                {
                    new Option { Destination = 402, Text = "Взять копье" },
                    new Option { Destination = 547, Text = "Выйти через правую" },
                    new Option { Destination = 501, Text = "Выйти через левую" },
                }
            },
            [497] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 572, Text = "Далее" },
                }
            },
            [498] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Далее" },
                }
            },
            [499] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ОРК",
                                Mastery = 7,
                                Endurance = 7,
                            },
                        },

                        Aftertext = "Если вы победили стража, можете войти в ворота.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Далее" },
                }
            },
            [500] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 416, Text = "Далее" },
                }
            },
            [501] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 534, Text = "Далее" },
                }
            },
            [502] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [503] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "Далее" },
                }
            },
            [504] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 332, Text = "Далее" },
                }
            },
            [505] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 337, Text = "Ту, что перед вами" },
                    new Option { Destination = 393, Text = "Ту, что справа от вас" },
                    new Option { Destination = 595, Text = "Ту, что слева" },
                }
            },
            [506] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [507] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 598, Text = "Пойти по ней" },
                    new Option { Destination = 501, Text = "Заглянуть за левую дверь" },
                }
            },
            [508] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 366, Text = "Далее" },
                }
            },
            [509] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 119, Text = "Далее" },
                }
            },
            [510] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Нправо" },
                    new Option { Destination = 214, Text = "Прямо" },
                }
            },
            [511] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 544, Text = "Далее" },
                }
            },
            [512] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [513] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 41, Text = "Заклятье Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 476, Text = "Заклятье Силы", OnlyIf = "ЗАКЛЯТИЕ СИЛЫ" },
                    new Option { Destination = 172, Text = "Заклятье Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 347, Text = "Продолжать драться" },
                    new Option { Destination = 601, Text = "Попробовать убежать" },
                }
            },
            [514] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 358, Text = "Отдохнуть" },
                    new Option { Destination = 441, Text = "Пойти дальше" },
                }
            },
            [515] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "Далее" },
                }
            },
            [516] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 134, Text = "Коготь дракона", OnlyIf = "DragonClaw" },
                    new Option { Destination = 42, Text = "Бриллиант", OnlyIf = "Diamond" },
                    new Option { Destination = 161, Text = "Гребень", OnlyIf = "Comb" },
                    new Option { Destination = 488, Text = "Перо аиста", OnlyIf = "StorkFeather" },
                    new Option { Destination = 316, Text = "Драться" },
                }
            },
            [517] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 359, Text = "Подождать" },
                    new Option { Destination = 105, Text = "Заклятье Левитации", OnlyIf = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ" },
                }
            },
            [518] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ТОРГОВЕЦ",
                                Mastery = 6,
                                Endurance = 12,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 477, Text = "Далее" },
                }
            },
            [519] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 278, Text = "Как пройти к Принцессе" },
                    new Option { Destination = 43, Text = "Как пройти к Барладу Дэрту" },
                    new Option { Destination = 136, Text = "Как пройти к Начальнику стражи" },
                }
            },
            [520] = new Paragraph
            {
                OpenOption = "Mirror, GoldenWhistle, ThreeFromAvenlo",

                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Далее" },
                }
            },
            [521] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 326, Text = "Далее" },
                }
            },
            [522] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 252, Text = "В дверь, которая перед вами" },
                    new Option { Destination = 542, Text = "В ту дверь, что справа" },
                }
            },
            [523] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 39, Text = "Далее" },
                }
            },
            [524] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 89, Text = "Тропинка прямо" },
                    new Option { Destination = 616, Text = "Дорога направо" },
                }
            },
            [525] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 349, Text = "Есть банан", OnlyIf = "Banana" },
                    new Option { Destination = 215, Text = "Драться с ней" },
                }
            },
            [526] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 235, Text = "На юг" },
                    new Option { Destination = 2, Text = "На запад" },
                }
            },
            [527] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 182, Text = "Далее" },
                }
            },
            [528] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ОБОРОТЕНЬ",
                                Mastery = 10,
                                Endurance = 10,
                            },
                        },

                        Aftertext = "Если вы убили Оборотня, то можете снять с его шеи Оберег и надеть на себя. Потом отправляйтесь в путь, ведь и так потеряно слишком много времени.",
                    },
                },

                OpenOption = "Amulet",

                Options = new List<Option>
                {
                    new Option { Destination = 240, Text = "Далее" },
                }
            },
            [529] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "МЕДВЕДИЦА",
                                Mastery = 8,
                                Endurance = 10,
                            },
                        },

                        Aftertext = "Если вы победили медведицу, то выбираетесь из берлоги и идете дальше по дороге, которая уходит в глубь леса.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "Далее" },
                }
            },
            [530] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [531] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 49, Text = "Прямо" },
                    new Option { Destination = 338, Text = "Налево" },
                }
            },
            [532] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ГОБЛИН",
                                Mastery = 8,
                                Endurance = 10,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ГОБЛИН",
                                Mastery = 6,
                                Endurance = 8,
                            },
                        },

                        Aftertext = "Если вы справились с охраной, то можете открыть дверь, которую они охраняли.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                    new Option { Destination = 552, Text = "Предъявить пропуск" },
                }
            },
            [533] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 137, Text = "Попробовать откупиться" },
                    new Option { Destination = 522, Text = "Драться" },
                }
            },
            [534] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Есть белая стрела", OnlyIf = "WhiteArrow" },
                    new Option { Destination = 362, Text = "Есть бляха с золотым орлом", OnlyIf = "BadgeWithAnEagle" },
                    new Option { Destination = 478, Text = "Войти в правую дверь" },
                    new Option { Destination = 603, Text = "Войти левую дверь" },
                    new Option { Destination = 282, Text = "Войти в среднюю дверь" },
                }
            },
            [535] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Далее" },
                }
            },
            [536] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 363, Text = "Есть Оберег", OnlyIf = "Amulet" },
                    new Option { Destination = 479, Text = "Есть серебряный сосуд", OnlyIf = "SilverVessel" },
                    new Option { Destination = 283, Text = "Подумать" },
                    new Option { Destination = 567, Text = "Атаковать" },
                }
            },
            [537] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Далее" },
                }
            },
            [538] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 370, Text = "Заклятье Иллюзии", OnlyIf = "ЗАКЛЯТИЕ ИЛЛЮЗИИ" },
                    new Option { Destination = 68, Text = "Спокойно ждать" },
                }
            },
            [539] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться c гоблином",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГОБЛИН",
                                Mastery = 4,
                                Endurance = 7,
                            },
                        },

                        Aftertext = "Как только вы убиваете Гоблина, из дома появляется еще один. Он менее пьян и будет не столь легким противником.",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться со вторым",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ГОБЛИН",
                                Mastery = 8,
                                Endurance = 9,
                            },
                        },

                        Aftertext = "Если же вы убиваете и второго Гоблина, то остальные решают не искушать судьбу и убегают, выпрыгнув в окно. Вы же можете зайти в сторожку и посмотреть, нет ли там чего-нибудь для вас полезного, а можете решить, что лучше сразу пойти к зданию в центре двора.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 484, Text = "Зайти в сторожку" },
                    new Option { Destination = 308, Text = "К зданию в центре двора" },
                }
            },
            [540] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 284, Text = "Есть фигурный ключ", OnlyIf = "CurlyKey" },
                    new Option { Destination = 474, Text = "Заняться большим сундуком" },
                    new Option { Destination = 380, Text = "Заняться маленьким сундуком" },
                    new Option { Destination = 39, Text = "Вернуться в залу" },
                }
            },
            [541] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 364, Text = "Есть меч Зеленого рыцаря", OnlyIf = "GreenSword" },
                    new Option { Destination = 494, Text = "Нет меча" },
                }
            },
            [542] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 411, Text = "Мясо" },
                    new Option { Destination = 578, Text = "Овощи" },
                    new Option { Destination = 80, Text = "Выходите через дверь" },
                }
            },
            [543] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЗЕЛЕНЫЙ РЫЦАРЬ",
                                Mastery = 11,
                                Endurance = 14,
                            },
                        },

                        Aftertext = "Если вам удалось победить рыцаря, то идите по коридору дальше, пока не дойдете до двери.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 365, Text = "Использовать заклятие" },
                    new Option { Destination = 241, Text = "По коридору дальше" },
                }
            },
            [544] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 285, Text = "Правую" },
                    new Option { Destination = 489, Text = "Левую" },
                }
            },
            [545] = new Paragraph
            {
                OpenOption = "SilverVessel",

                Options = new List<Option>
                {
                    new Option { Destination = 253, Text = "Взять стеклянный сосуд" },
                    new Option { Destination = 39, Text = "Выйти из комнаты" },
                }
            },
            [546] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "Далее" },
                }
            },
            [547] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Далее" },
                }
            },
            [548] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 297, Text = "Погладить" },
                    new Option { Destination = 416, Text = "Уйти" },
                }
            },
            [549] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 285, Text = "Правую" },
                    new Option { Destination = 489, Text = "Левую" },
                }
            },
            [550] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "Далее" },
                }
            },
            [551] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "Зеркальце", OnlyIf = "Mirror" },
                    new Option { Destination = 296, Text = "Гребень", OnlyIf = "Comb" },
                    new Option { Destination = 490, Text = "Оберег", OnlyIf = "Amulet" },
                    new Option { Destination = 255, Text = "Придумать что-то другое" },
                }
            },
            [552] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [553] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГАРПИЯ",
                                Mastery = 10,
                                Endurance = 12,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 388, Text = "Далее" },
                }
            },
            [554] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 301, Text = "Далее" },
                }
            },
            [555] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 450, Text = "Далее" },
                }
            },
            [556] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 394, Text = "Попробовать откупиться" },
                    new Option { Destination = 93, Text = "Достать меч" },
                }
            },
            [557] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ ОГНЯ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Осмотреть домик" },
                    new Option { Destination = 416, Text = "Уйти" },
                }
            },
            [558] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ГИГАНТСКИЙ ПАУК",
                                Mastery = 8,
                                Endurance = 8,
                            },
                        },
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 189, Text = "Далее" },
                }
            },
            [559] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 184, Text = "Идти в деревню" },
                    new Option { Destination = 13, Text = "Пойти по тропинке" },
                }
            },
            [560] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 165, Text = "Зеркало" },
                    new Option { Destination = 288, Text = "Карты на столе" },
                    new Option { Destination = 493, Text = "Если вы уже осмотрели и то, и другое" },
                }
            },
            [561] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [562] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 613, Text = "Предложить ему деньги" },
                    new Option { Destination = 65, Text = "Напасть" },
                }
            },
            [563] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЛЕСОВИЧОК",
                                Mastery = 6,
                                Endurance = 8,
                            },
                        },

                        Aftertext = "Победить его не так уж сложно. После этого вы можете взять у него из кармана серебряный свисток и отправиться по дороге дальше.",
                    },
                },

                OpenOption = "SilverWhistle",

                Options = new List<Option>
                {
                    new Option { Destination = 450, Text = "Далее" },
                }
            },
            [564] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 480, Text = "Далее" },
                }
            },
            [565] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Щит тьмы" },
                    new Option { Destination = 469, Text = "Демоны и сила" },
                    new Option { Destination = 298, Text = "Меч и верность" },
                }
            },
            [566] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 556, Text = "В правую" },
                    new Option { Destination = 277, Text = "В ту, что напротив" },
                    new Option { Destination = 483, Text = "Заклятье Левитации", OnlyIf = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ" },
                    new Option { Destination = 39, Text = "Вернуться в залу" },
                }
            },
            [567] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ЗЕЛЕНЫЙ РЫЦАРЬ",
                                Mastery = 10,
                                Endurance = 10,
                            },
                        },

                        Aftertext = "Если вы победили врага, то путь вперед свободен, и вы бежите дальше по коридору, пока дорогу не преграждает дверь.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 395, Text = "Заклятье Силы", OnlyIf = "ЗАКЛЯТИЕ СИЛЫ" },
                    new Option { Destination = 299, Text = "Заклятье Слабости", OnlyIf = "ЗАКЛЯТИЕ СЛАБОСТИ" },
                    new Option { Destination = 96, Text = "Заклятье Огня", OnlyIf = "ЗАКЛЯТИЕ ОГНЯ" },
                    new Option { Destination = 241, Text = "Если вы победили врага" },
                }
            },
            [568] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 300, Text = "Далее" },
                }
            },
            [569] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться c орком",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ОРК",
                                Mastery = 10,
                                Endurance = 6,
                            },
                        },

                        Aftertext = "Если вы убили его за три раунда атаки, то сражайтесь с двумя остальными. Если же нет, то через три раунда они приходят на помощь своему предводителю, и вам придется биться с тремя сразу. После того как первый Орк все же будет повержен, если хотите, ПРОВЕРЬТЕ СВОЮ УДАЧУ.",
                    },

                    new Actions
                    {
                        ActionName = "Luck",
                        ButtonName = "Проверить удачу",

                        Aftertext = "Если вы удачливы, то остальные Орки не захотят умирать, защищая ворота, и убегут в лес. Если же нет, то придется драться со всеми.",
                    },

                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться с остальными",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ВТОРОЙ ОРК",
                                Mastery = 7,
                                Endurance = 7,
                            },
                            new Character
                            {
                                Name = "ТРЕТИЙ ОРК",
                                Mastery = 7,
                                Endurance = 7,
                            },
                        },

                        Aftertext = "Если вы еще остались живы после битвы, то можете проскользнуть в ворота, оставив стражу лежать на дороге.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "Далее" },
                }
            },
            [570] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 604, Text = "Далее" },
                }
            },
            [571] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 76, Text = "Далее" },
                }
            },
            [572] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [573] = new Paragraph
            {
                OpenOption = "WhiteArrow, CandleAndFlint",

                Options = new List<Option>
                {
                    new Option { Destination = 561, Text = "Попробовать переплыть" },
                    new Option { Destination = 332, Text = "Пойдете по дороге" },
                }
            },
            [574] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [575] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [576] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 559, Text = "Направо" },
                    new Option { Destination = 181, Text = "Налево" },
                    new Option { Destination = 445, Text = "Прямо" },
                }
            },
            [577] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Mastery",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 293, Text = "Далее" },
                }
            },
            [578] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 411, Text = "Попробовать мясо" },
                    new Option { Destination = 80, Text = "Уйти" },
                }
            },
            [579] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [580] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "RemoveSpell",
                        ValueString = "ЗАКЛЯТИЕ СЛАБОСТИ",
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПЕРВЫЙ ГОБЛИН",
                                Mastery = 4,
                                Endurance = 9,
                            },
                            new Character
                            {
                                Name = "ВТОРОЙ ГОБЛИН",
                                Mastery = 7,
                                Endurance = 5,
                            },
                        },

                        Aftertext = "Когда вы добьете своих врагов, можете забрать у них бронзовый свисток и медный ключик, которые им больше не понадобятся, и перейти реку.",
                    },
                },

                OpenOption = "BronzeWhistle, CopperKey",

                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Далее" },
                }
            },
            [581] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [582] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Ко входу в центр цитадели" },
                    new Option { Destination = 174, Text = "Поговорите с ними еще" },
                }
            },
            [583] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "По лестнице вверх" },
                    new Option { Destination = 398, Text = "В правую дверь" },
                    new Option { Destination = 206, Text = "В левую дверь" },
                    new Option { Destination = 350, Text = "В среднюю дверь" },
                }
            },
            [584] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 616, Text = "Прямо" },
                    new Option { Destination = 89, Text = "Налево" },
                }
            },
            [585] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 428, Text = "Далее" },
                }
            },
            [586] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 608, Text = "Поговорите с ними" },
                    new Option { Destination = 6, Text = "Расчистить путь мечом" },
                }
            },
            [587] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 348, Text = "Свернуть налево" },
                    new Option { Destination = 537, Text = "Вернуться направо" },
                }
            },
            [588] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 275, Text = "Далее" },
                }
            },
            [589] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 266, Text = "Далее" },
                }
            },
            [590] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 241, Text = "Далее" },
                }
            },
            [591] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 139, Text = "Далее" },
                }
            },
            [592] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [593] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 409, Text = "Далее" },
                }
            },
            [594] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 553, Text = "Далее" },
                }
            },
            [595] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 610, Text = "Дверь сломана" },
                    new Option { Destination = 393, Text = "Попробовать открыть дверь за вашей спиной" },
                    new Option { Destination = 337, Text = "Попробовать открыть дверь справа" },
                }
            },
            [596] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [597] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 350, Text = "Прямо" },
                    new Option { Destination = 398, Text = "Справа" },
                    new Option { Destination = 206, Text = "Слева" },
                }
            },
            [598] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 501, Text = "Далее" },
                }
            },
            [599] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [600] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 55, Text = "Далее" },
                }
            },
            [601] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Endurance",
                        Value = -6,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "Далее" },
                }
            },
            [602] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Далее" },
                }
            },
            [603] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [604] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Fight",
                        ButtonName = "Сражаться",

                        Enemies = new List<Character>
                        {
                            new Character
                            {
                                Name = "ПОВАР",
                                Mastery = 8,
                                Endurance = 10,
                            },
                        },

                        Aftertext = "Его несложно убить, однако пока вы сражались, второй поваренок успел куда-то спрятаться. У вас нет времени его искать, и остается только надеяться, что он вылезет, когда вы будете уже далеко. Теперь можете или заглянуть в сундук или побыстрее уйти из кухни.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 173, Text = "Заглянуть в сундук" },
                    new Option { Destination = 379, Text = "Уйти" },
                }
            },
            [605] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 169, Text = "Далее" },
                }
            },
            [606] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 594, Text = "Правую" },
                    new Option { Destination = 599, Text = "Левую" },
                }
            },
            [607] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 323, Text = "Далее" },
                }
            },
            [608] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 214, Text = "Прямо" },
                    new Option { Destination = 38, Text = "Направо" },
                }
            },
            [609] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [610] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 607, Text = "Подняться по ней" },
                    new Option { Destination = 393, Text = "Попробуете открыть дверь за вашей спиной" },
                    new Option { Destination = 337, Text = "Попробуете открыть дверь справа от вас" },
                }
            },
            [611] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 128, Text = "Красного вина" },
                    new Option { Destination = 32, Text = "Белого вина" },
                    new Option { Destination = 226, Text = "Эля" },
                    new Option { Destination = 356, Text = "Драться" },
                }
            },
            [612] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 196, Text = "С рубином" },
                    new Option { Destination = 258, Text = "И изумрудом" },
                    new Option { Destination = 169, Text = "Осмотреть кабинет" },
                }
            },
            [613] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 246, Text = "Согласиться" },
                    new Option { Destination = 65, Text = "Драться" },
                }
            },
            [614] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 192, Text = "Далее" },
                }
            },
            [615] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 232, Text = "Далее" },
                }
            },
            [616] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [617] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [618] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "Далее" },
                }
            },
            [619] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Далее" },
                }
            },
            [620] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать левитацию",
                        Text = "ЗАКЛЯТИЕ ЛЕВИТАЦИИ",
                        ThisIsSpell = true,
                        Aftertext = "С его помощью вы сможете подняться в воздух и перелететь то препятствие, которое вам встретится. Но будьте осторожны: заклятие действует не слишком долго, и если вы не рассчитаете свои силы, то можете опуститься на землю раньше, чем препятствие или опасность будут позади.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать огонь",
                        Text = "ЗАКЛЯТИЕ ОГНЯ",
                        ThisIsSpell = true,
                        Aftertext = "Поможет вам в нужный момент создать в воздухе огненный шар и направить его на врагов. Но в закрытых помещениях им надо пользоваться осмотрительно, чтобы не устроить пожар.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать иллюзию",
                        Text = "ЗАКЛЯТИЕ ИЛЛЮЗИИ",
                        ThisIsSpell = true,
                        Aftertext = "Вы создадите у вашего врага необходимую иллюзию и сможете спастись в тех ситуациях, из которых другого выхода не будет. Но заклятие иллюзии — опасное колдовство: ведь иллюзия рассеивается, и враг понимает, что его одурачили.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать силу",
                        Text = "ЗАКЛЯТИЕ СИЛЫ",
                        ThisIsSpell = true,
                        Aftertext = "Прибавит вам силу и увеличит вашу СИЛУ УДАРА.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать слабость",
                        Text = "ЗАКЛЯТИЕ СЛАБОСТИ",
                        ThisIsSpell = true,
                        Aftertext = "Сделает вашего врага неуклюжим и неповоротливым, ослабит СИЛУ его УДАРА.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать копию",
                        Text = "ЗАКЛЯТИЕ КОПИИ",
                        ThisIsSpell = true,
                        Aftertext = "С его помощью вы сможете при случае создать точную Копию вашего противника, которую вы будете контролировать. Тогда прежде чем добраться до вас, ему придется драться с собственной Копией, МАСТЕРСТВО и ВЫНОСЛИВОСТЬ которой будут равны его МАСТЕРСТВУ и ВЫНОСЛИВОСТИ. Если ваш враг победит свою Копию, то с ним придется драться вам самим. Если же Копия сразит противника, то заклятие теряет силу и Копия исчезает, а вы продолжаете свой путь. Но если противников было несколько, а Копию вы смогли или захотели создать только одну, то придется драться и с остальными.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать исцеление",
                        Text = "ЗАКЛЯТИЕ ИСЦЕЛЕНИЯ",
                        ThisIsSpell = true,
                        Aftertext = "В любой момент (но не во время сражения) добавит вам 8 ВЫНОСЛИВОСТЕЙ",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Выбрать плавание",
                        Text = "ЗАКЛЯТИЕ ПЛАВАНИЯ",
                        ThisIsSpell = true,
                         Aftertext = "Вы никогда не видели ни реки, ни озера, а в дороге может случиться всякое. У вас уже нет времени учиться плавать. Но с помощью этого заклятия вы сможете переплыть любую водную преграду, которая вам встретится.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 1, Text = "Далее" },
                }
            },
        };
    }
}
