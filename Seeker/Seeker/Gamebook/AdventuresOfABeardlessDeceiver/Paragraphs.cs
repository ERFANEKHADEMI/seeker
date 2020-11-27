﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Seeker.Game;

namespace Seeker.Gamebook.AdventuresOfABeardlessDeceiver
{
    class Paragraphs : Abstract.IParagraphs
    {
        public Game.Paragraph Get(int id)
        {
            Paragraph source = Paragraph[id];

            Game.Paragraph paragraph = new Game.Paragraph();

            if (source.Options != null)
                paragraph.Options = new List<Option>(source.Options);

            if (source.Actions != null)
                paragraph.Actions = new List<Abstract.IActions>(source.Actions);

            if (source.Modification != null)
                paragraph.Modification = new List<Abstract.IModification>(source.Modification);

            paragraph.Trigger = source.Trigger;
            paragraph.Image = source.Image;

            return paragraph;
        }

        private static Dictionary<int, Paragraph> Paragraph = new Dictionary<int, Paragraph>
        {
            [0] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 205, Text = "В путь!" },
                }
            },
            [1] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 30, Text = "Поднять золотую монету" },
                    new Option { Destination = 60, Text = "Поднять бронзовый знак Тенгри" },
                    new Option { Destination = 90, Text = "Поднять сверток с одеждой" },
                     
                }
            },
            [2] = new Paragraph
            {
                Trigger = "Letter",

                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Далее" },
                }
            },
            [3] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 7,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 92, Text = "В случае успеха" },
                    new Option { Destination = 59, Text = "В случае провала" },
                }
            },
            [4] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 22, Text = "Далее" },
                }
            },
            [5] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 156, Text = "посетить базар и купить там что-нибудь полезное" },
                    new Option { Destination = 164, Text = "или пообщаться с соплеменниками" },
                }
            },
            [6] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 187, Text = "Далее" },
                }
            },
            [7] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 98, Text = "На северо-запад к кипчакам" },
                    new Option { Destination = 162, Text = "На запад к оазису" },
                    new Option { Destination = 38, Text = "На север к Иртышу" },
                }
            },
            [8] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 143, Text = "Далее" },
                }
            },
            [9] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 150,
                    },
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 3,
                    },
                    new Modification
                    {
                        Name = "AkynGlory",
                        Empty = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [10] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 28, Text = "В случае успеха" },
                    new Option { Destination = 50, Text = "В случае провала" },
                }
            },
            [11] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Далее" },
                }
            },
            [12] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Заплатить за комнату",
                        Text = "КОМНАТА НА НОЧЬ, 10 таньга",
                        Price = 10,
                        Aftertext = "Цена очень завышена, учитывая то, что вчера Алдар не выложил ни одной монеты за ночлег. Но в противной случае придётся провести ночь у костра во внутреннем дворе.",
                        Trigger = "Room",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 109, Text = "Далее" },
                }
            },
            [13] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 3,
                    },
                },

                Trigger = "ShameOfTemir",

                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Далее" },
                }
            },
            [14] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 150,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Далее" },
                }
            },
            [15] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 38, Text = "Вдоль левого берега Иртыша к ойратским землям" },
                    new Option { Destination = 68, Text = "Переправиться на безлюдное правобережье" },
                    new Option { Destination = 98, Text = "В степь к кочевью кипчаков" },
                }
            },
            [16] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 187, Text = "Далее" },
                }
            },
            [17] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Trigger = "Note",

                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Далее" },
                }
            },
            [18] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 89, Text = "В случае успеха", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0, !DonsHorse" },
                    new Option { Destination = 129, Text = "В случае провала", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0, !DonsHorse" },
                    new Option { Destination = 32, Text = "У Алдара донской рысак", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0, DonsHorse" },
                    new Option { Destination = 54, Text = "Взять немного левее", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0, !DonsHorse" },
                }
            },
            [19] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 185, Text = "В случае успеха" },
                    new Option { Destination = 67, Text = "В случае провала" },
                    new Option { Destination = 177, Text = "Алдар Косе заручился поддержкой охраны оазиса", OnlyIf = "OasisGuardSupport" },
                    new Option { Destination = 194, Text = "Знает слабое место персидского торговца" },
                    new Option { Destination = 8, Text = "Продолжить путь на северо-запад к караван-сараю" },
                }
            },
            [20] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 80, Text = "В случае успеха" },
                    new Option { Destination = 50, Text = "В случае провала" },
                }
            },
            [21] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [22] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 86, Text = "В случае успеха" },
                    new Option { Destination = 97, Text = "В случае провала" },
                }
            },
            [23] = new Paragraph
            {
                Trigger = "OasisGuardSupport",

                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Далее" },
                }
            },
            [24] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Откликнуться на вызов", OnlyIf = "PartyClothes" },
                    new Option { Destination = 135, Text = "Купить нужное (одежду или скакуна) на базаре неподалёку" },
                    new Option { Destination = 27, Text = "Отказаться от участия в этом соревновании" },
                }
            },
            [25] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Согласиться посетить поселение ойратов и поговорить с их предводителем" },
                    new Option { Destination = 140, Text = "Отказаться и продолжить путь дальше вдоль Иртыша" },
                }
            },
            [26] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [27] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Далее" },
                }
            },
            [28] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [29] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 215,
                    },
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Алдару удалось получить комнату в караван-сарае", OnlyIf = "Room" },
                    new Option { Destination = 172, Text = "Он будет спать во внутреннем дворе у костра" },
                }
            },
            [30] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 45, Text = "В случае успеха" },
                    new Option { Destination = 35, Text = "В случае провала" },
                }
            },
            [31] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 136, Text = "Попроситься на приём к караван-баши", OnlyIf = "PartyClothes" },
                    new Option { Destination = 144, Text = "Принять приглашение погонщиков посидеть у костра" },
                    new Option { Destination = 154, Text = "Послушать, что рассказывает бородач с ослом" },
                }
            },
            [32] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 76, Text = "Далее", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0" },
                }
            },
            [33] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 7,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 100, Text = "В случае успеха" },
                    new Option { Destination = 50, Text = "В случае провала" },
                }
            },
            [34] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 187, Text = "Далее" },
                }
            },
            [35] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Далее" },
                }
            },
            [36] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 11,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 199, Text = "Если обе проверки успешны" },
                    new Option { Destination = 176, Text = "Если хотя бы одна из них провалена" },
                }
            },
            [37] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить одежду",
                        Text = "ПРАЗДНИЧНАЯ ОДЕЖДА, 25 таньга",
                        Price = 25,
                        Trigger = "PartyClothes",
                        Aftertext = "Костюм, несомненно, хороший. Но цена на него безбожно задрана. Алдар понимает, что воины состоят в доле с хитрым торговцем, получая хорошую прибыль с путников, которые хотят попасть в ханскую ставку. Придётся либо платить, либо придумывать другой способ.\n\nЕсли нет ни денег ни перстня, придётся полагаться на свои способности.",
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 112, Text = "В случае успеха" },
                    new Option { Destination = 83, Text = "В случае провала" },
                    new Option { Destination = 24, Text = "Переодеться и зайти", OnlyIf = "PartyClothes" },
                    new Option { Destination = 24, Text = "Есть ханский перстень", OnlyIf = "KhansRing" },
                }
            },
            [38] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 113, Text = "Вперёд к кочевью ойратов" },
                    new Option { Destination = 133, Text = "Избежать встречи, обогнув стоянку за холмами" },
                }
            },
            [39] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 45,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Алдару удалось получить комнату в караван-сарае", OnlyIf = "Room" },
                    new Option { Destination = 172, Text = "Он будет спать во внутреннем дворе у костра" },
                }
            },
            [40] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 11,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 149, Text = "Если обе проверки удачны" },
                    new Option { Destination = 139, Text = "Если только одна из них удачна (неважно какая)" },
                    new Option { Destination = 159, Text = "Если обе проверки провалены" },
                }
            },
            [41] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = -10,
                    },
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Далее" },
                }
            },
            [42] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 180, Text = "Вызвать Темир-батыра на курес, казахскую борьбу" },
                    new Option { Destination = 36, Text = "Бросить вызов на музыкальный поединок", OnlyIf = "Dombra" },
                    new Option { Destination = 192, Text = "Попытаться хитростью опоить бандита", OnlyIf = "Arak" },
                    new Option { Destination = 161, Text = "Направиться к ханской юрте для выполнения задания" },
                }
            },
            [43] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "Далее" },
                }
            },
            [44] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "AkynGlory",
                        Value = 3,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Далее" },
                }
            },
            [45] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 50,
                    },
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Далее" },
                }
            },
            [46] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Заплатить за комнату",
                        Text = "КОМНАТА НА НОЧЬ, 25 таньга",
                        Price = 25,
                        Aftertext = "В случае отказа он с лёгкостью продаст комнату любому торговцу, у которых всегда есть деньги. Но тогда нашему герою придётся провести ночь у костра во внутреннем дворе.",
                        Trigger = "Room",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 109, Text = "Далее" },
                }
            },
            [47] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 88, Text = "В случае успеха" },
                    new Option { Destination = 188, Text = "В случае провала" },
                }
            },
            [48] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 123, Text = "Далее", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0" },
                }
            },
            [49] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 187, Text = "Заплатить требуемую сумму", OnlyIf = "ТАНЬГА >= 100"  },
                    new Option { Destination = 83, Text = "Если же таких денег нет" },
                }
            },
            [50] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [51] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Алдару удалось получить комнату в караван-сарае", OnlyIf = "Room" },
                    new Option { Destination = 172, Text = "Он будет спать во внутреннем дворе у костра" },
                }
            },
            [52] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 60,
                    },
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "AkynGlory",
                        Empty = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [53] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 3, Text = "Река" },
                    new Option { Destination = 26, Text = "Лес" },
                    new Option { Destination = 134, Text = "Горы" },
                }
            },
            [54] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 76, Text = "Далее", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0" },
                }
            },
            [55] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "AkynGlory",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 132, Text = "Далее" }
                }
            },
            [56] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 71, Text = "Алдар Косе сам спас мальчика на реке" },
                    new Option { Destination = 91, Text = "Он прибыл к ойратам в качестве гостя" },
                    new Option { Destination = 111, Text = "Он прибыл как пленник" },
                }
            },
            [57] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [58] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "PopularityByTime",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Далее" },
                }
            },
            [59] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [60] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 75, Text = "В случае успеха" },
                    new Option { Destination = 65, Text = "В случае провала" },
                }
            },
            [61] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Не взирая ни на что, рассказать правду о задании Алдара" },
                    new Option { Destination = 101, Text = "Уклончиво ответить, что Алдар всего лишь бедный кочевник, зарабатывающий на жизнь там, где подвернётся возможность" },
                }
            },
            [62] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 162, Text = "К оазису" },
                    new Option { Destination = 98, Text = "В кипчакское кочевье" },
                }
            },
            [63] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 148, Text = "Далее" },
                }
            },
            [64] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "StatBonuses",
                        Value = 3,
                    },
                },

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить силы",
                        Text = "СИЛА",
                        Stat = "Strength",
                        Aftertext = "Способность персонажа поднимать и переносить тяжести, бороться, а также общая выносливость.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить ловкости",
                        Text = "ЛОВКОСТЬ",
                        Stat = "Skill",
                        Aftertext = "Скорость реакции, умение держать равновесие, уворачиваться, прыгать, а также ловкость рук (точные движения кистей и пальцев, включая игру на музыкальных инструментах).",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить мудрости",
                        Text = "МУДРОСТЬ",
                        Stat = "Wisdom",
                        Aftertext = "Общие знания, жизненный опыт, способность к анализу и логическим рассуждениям, умение замечать детали и делать правильные выводы.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить хитрости",
                        Text = "ХИТРОСТЬ",
                        Stat = "Cunning",
                        Aftertext = "Способность обманывать, жульничать, воровские навыки, а также находчивость и умение быстро находить решения в сложных ситуациях.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить красноречия",
                        Text = "КРАСНОРЕЧИЕ",
                        Stat = "Oratory",
                        Aftertext = "Способность разговорить собеседника, дар убеждения, умение слагать стихи и песни.\n\nСердечно поблагодарив аксакалов, джигит покидает их юрту. В поисках ночлега Алдар спрашивает совета у охранников на входе, которые указывают ему на шатры для гостей праздника.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [65] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Далее" },
                }
            },
            [66] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Алдару удалось получить комнату в караван-сарае", OnlyIf = "Room" },
                    new Option { Destination = 172, Text = "Он будет спать во внутреннем дворе у костра" },
                }
            },
            [67] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [68] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 201, Text = "Двигаться и дальше вдоль реки" },
                    new Option { Destination = 103, Text = "Срезать путь по прямой, углубившись в лес" },
                }
            },
            [69] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 123, Text = "Далее", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0" },
                }
            },
            [70] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 7, Text = "Далее" },
                }
            },
            [71] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 2, Text = "Не взирая ни на что, рассказать правду о задании Алдара" },
                    new Option { Destination = 14, Text = "Уклончиво ответить, что Алдар всего лишь бедный кочевник, зарабатывающий на жизнь там, где подвернется возможность" },
                }
            },
            [72] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [73] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 196, Text = "В случае успеха" },
                    new Option { Destination = 186, Text = "В случае провала" },
                }
            },
            [74] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "AkynGlory",
                        Init = true,
                    },
                },

                Image = "AdventuresOfABeardlessDeceiver_Akyn.jpg",

                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 197, Text = "В случае успеха" },
                    new Option { Destination = 4, Text = "В случае провала" },
                }
            },
            [75] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Trigger = "TengriSymbol",

                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Далее" },
                }
            },
            [76] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 119, Text = "В случае успеха", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0, !ArabianHorse" },
                    new Option { Destination = 141, Text = "В случае провала", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0, !ArabianHorse" },
                    new Option { Destination = 106, Text = "У Алдара арабский скакун", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0, ArabianHorse" },
                }
            },
            [77] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Kumis",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Далее" },
                }
            },
            [78] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить домбру",
                        Text = "ДОМБРА, 10 таньга",
                        Price = 10,
                        Aftertext = "Двухструнный музыкальный инструмент.",
                        Trigger = "Dombra",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить одежду",
                        Text = "ПРАЗДНИЧНАЯ ОДЕЖДА, 15 таньга",
                        Price = 15,
                        Aftertext = "Шелковый халат, шапка из лисьего меха и расшитая нательная рубаха.",
                        Trigger = "PartyClothes",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить пузырёк",
                        Text = "ПУЗЫРЁК НАСТОЙКИ ИЗ ТРАВ, 20 таньга",
                        Price = 20,
                        Aftertext = "К сожалению, у торговца есть только один пузырёк на продажу. Выпив настойку перед проверкой способности, Алдар Косе сможет увеличить уровень своей способности при данной проверке сразу на 4 пункта. В пузырьке две порции, которые можно использовать по отдельности или вместе, увеличив уровень способности сразу на 8 пунктов.\n\nАлдар проводит много времени прицениваясь к товарам. И вот базарный народ начинает сворачивать торговлю, собираясь на обед.\n\nКупить всё, что нужно, и на что хватит денег, и внести соответствующие изменения на лист персонажа.\n\nВ тени пальм уже установили деревянные топчаны для дастархана, покрыв их огромной скатертью, а по краям положив стёганые подушки для гостей. Как принято на караванных стоянках, каждый кладёт свою долю еды на скатерть. Обедают все вместе, без различия общественного положения, будь ты глава каравана или слуга. А хозяева разносят привычный в таких случаях зелёный чай. Ведь каждый знает, что в жаркое время нет ничего лучше горячего чая, чтобы утолить жажду.\n\nНаравне со всеми Алдар выкладывает свою часть провизии из сумки: сухофрукты, вяленое мясо, несколько лепёшек, козий сыр. Перебрасываясь шутками путешественники приступают к обильному обеду, во время которого обмениваются свежими новостями.\n\nКогда обед заканчивается, торговцы выражают желание сыграть в нарды, настольную игру на специальной доске, разделённой на две половины. Цель игры - бросая кости и передвигая шашки в соответствии с выпавшими очками, пройти шашками полный круг по доске. А чтобы было интересней играть, назначают ставку в 10 ТАНЬГА. В нардах важна не столько удача при броске кубиков, сколько умение анализировать ситуацию и делать неожиданные для соперника ходы.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 40, Text = "Если у Алдара есть такие деньги и желание, то можно сделать ставку и сыграть в нарды" },
                    new Option { Destination = 128, Text = "Или можно просто посмотреть, как другие играют" },
                }
            },
            [79] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 66, Text = "Посидеть в чайхане и поговорить с торговцами" },
                    new Option { Destination = 87, Text = "Пойти на приём к местному беку" },
                    new Option { Destination = 126, Text = "Посмотреть на танцовщиц" },
                }
            },
            [80] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [81] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Имя Сауле знакомо Алдару Косе", OnlyIf = "SauleName" },
                    new Option { Destination = 204, Text = "Далее" },
                }
            },
            [82] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 114, Text = "Сделать выбор в пользу медового напитка" },
                    new Option { Destination = 147, Text = "Попросить собеседников налить полугар" },
                }
            },
            [83] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [84] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить одежду",
                        Text = "ПРАЗДНИЧНАЯ ОДЕЖДА, 20 таньга",
                        Price = 20,
                        Aftertext = "Шелковый халат, шапка-малахай из лисьего меха и расшитая нательная рубаха.",
                        Trigger = "PartyClothes",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить домбру",
                        Text = "ДОМБРА, 15 таньга",
                        Price = 15,
                        Aftertext = "Двухструнный музыкальный инструмент.",
                         Trigger = "Dombra",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить доспех",
                        Text = "ЛЁГКИЙ КОЖАНЫЙ ДОСПЕХ, 20 таньга",
                        Price = 20,
                        Aftertext = "Лёгкий кожаный доспех, который одевается на тело под рубаху. Не заметен и не сковывает движений, но смягчает удары и захваты противника (во время рукопашной схватки ЛОВКОСТЬ И СИЛА увеличиваются на 2 пункта).",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить вина",
                        Text = "БУТЫЛЬ ВИНА, 10 таньга",
                        Price = 10,
                        Aftertext = "Содержит 2 порции, которые могут применяться по тем же правилам, что и кумыс. Причём для разрешается применить обе порции для одной проверки способностей, увеличивая свой уровень сразу на 4 пунтка, или даже на 6 и более пунктов, если дополнительно используется порция кумыса и/или другого напитка.",

                        Benefit = new Modification
                        {
                            Name = "Kumis",
                            Value = 2,
                        },
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить скакуна",
                        Text = "СКАКУН АРАБСКОЙ ПОРОДЫ, 120 таньга",
                        Price = 120,
                        Trigger = "ArabianHorse",
                        Aftertext = "Алдару очень нравится этот конь и он сторговывается с кипчаком на 120 ТАНЬГА, если наш герой отдаст своего жеребца торговцу.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 46, Text = "Далее" },
                }
            },
            [85] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Trigger = "DonsHorse",

                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Далее" },
                }
            },
            [86] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "AkynGlory",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "Далее" },
                }
            },
            [87] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 179, Text = "В случае успеха" },
                    new Option { Destination = 167, Text = "В случае провала" },
                    new Option { Destination = 189, Text = "Алдар Косе знает человека по имени Серик", OnlyIf = "Serik" },
                }
            },
            [88] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Trigger = "Note, ArabianHorse",

                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Далее" },
                }
            },
            [89] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 76, Text = "Далее", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0" },
                }
            },
            [90] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 105, Text = "В случае успеха" },
                    new Option { Destination = 95, Text = "В случае провала" },
                }
            },
            [91] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "В случае успеха" },
                    new Option { Destination = 121, Text = "В случае провала" },
                }
            },
            [92] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Trigger = "GreenStone, TipsFromAthanasius",

                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [93] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Далее" },
                }
            },
            [94] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [95] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Далее" },
                }
            },
            [96] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Empty = true,
                    },
                },

                Trigger = "SauleName",

                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Далее" },
                }
            },
            [97] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "AkynGlory",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 115, Text = "Далее" },
                }
            },
            [98] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 158, Text = "Попроситься на приём к Джанибек-батыру" },
                    new Option { Destination = 131, Text = "Поучаствовать в ат-омырауластыру" },
                    new Option { Destination = 47, Text = "Заговорить с бандитами и обвинить их в разбойном нападении" },
                }
            },
            [99] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 165, Text = "Далее" },
                }
            },
            [100] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [101] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 10,
                    },
                    new Modification
                    {
                        Name = "Kumis",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Далее" },
                }
            },
            [102] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 151, Text = "Если имя Сауле знакомо Алдару Косе" },
                    new Option { Destination = 204, Text = "Если оно ни о чём не говорит" },
                }
            },
            [103] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Отказаться от испытания и продолжить путь вдоль реки" },
                    new Option { Destination = 190, Text = "Показать силушку немереную" },
                    new Option { Destination = 203, Text = "Показать удаль молодецкую" },
                    new Option { Destination = 53, Text = "Показать острый ум" },
                }
            },
            [104] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 2,
                    },
                    new Modification
                    {
                        Name = "Kumis",
                        Value = 1,
                    },
                },

                Trigger = "AkhalTeke",

                Options = new List<Option>
                {
                    new Option { Destination = 8, Text = "Далее" },
                }
            },
            [105] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Trigger = "PartyClothes",

                Options = new List<Option>
                {
                    new Option { Destination = 120, Text = "Далее" },
                }
            },
            [106] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Если ЕДИНИЦЫ ВРЕМЕНИ уменьшились до нуля", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 58, Text = "Если счёт всё ещё больше нуля", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0" },
                }
            },
            [107] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 23, Text = "Пообщаться с казахскими воинами" },
                    new Option { Destination = 78, Text = "Посмотреть на товары" },
                    new Option { Destination = 124, Text = "Просто побродить по оазису, наблюдая за его обитателями" },
                }
            },
            [108] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "Далее" },
                }
            },
            [109] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 66, Text = "Посидеть в чайхане и поговорить с торговцами" },
                    new Option { Destination = 87, Text = "Пойти на приём к местному беку" },
                    new Option { Destination = 126, Text = "Посмотреть на танцовщиц" },
                    new Option { Destination = 138, Text = "Поиграть в нарды" },
                }
            },
            [110] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 150,
                    },
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Skill",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [111] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 13,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 61, Text = "В случае успеха" },
                    new Option { Destination = 121, Text = "В случае провала" },
                }
            },
            [112] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Далее" },
                }
            },
            [113] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 10, Text = "Пуститься вплавь на коне" },
                    new Option { Destination = 20, Text = "Пуститься вплавь без коня" },
                    new Option { Destination = 33, Text = "Использовать верёвку, перебросив один конец мальчику" },
                }
            },
            [114] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Kumis",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 145, Text = "Лихарев обещал подарить коня", OnlyIf = "GiftPromise" },
                    new Option { Destination = 175, Text = "Покинуть крепость" },
                }
            },
            [115] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 44, Text = "В случае успеха" },
                    new Option { Destination = 55, Text = "В случае провала" },
                }
            },
            [116] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [117] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "AkynGlory",
                        Empty = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [118] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "Далее" },
                }
            },
            [119] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Если ЕДИНИЦЫ ВРЕМЕНИ уменьшились до нуля", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 58, Text = "Если счёт всё ещё больше нуля", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0" },
                }
            },
            [120] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 5, Text = "На северо-запад в кочевье найманов" },
                    new Option { Destination = 202, Text = "На запад к караванному пути" },
                    new Option { Destination = 160, Text = "На север к реке Иртыш" },
                }
            },
            [121] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Kumis",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "Далее" },
                }
            },
            [122] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 152, Text = "Далее" },
                }
            },
            [123] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 169, Text = "В случае успеха", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0, !AkhalTeke" },
                    new Option { Destination = 153, Text = "В случае провала", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0, !AkhalTeke" },
                    new Option { Destination = 182, Text = "Если у Алдара Косе жеребец-ахалтекинец", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0, AkhalTeke" },
                }
            },
            [124] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 78, Text = "Далее" },
                }
            },
            [125] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 178, Text = "В случае успеха" },
                    new Option { Destination = 41, Text = "В случае провала" },
                }
            },
            [126] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 51, Text = "Бросить в блюдо 5 ТАНЬГА", OnlyIf = "ТАНЬГА >= 5" },
                    new Option { Destination = 195, Text = "Бросить в блюдо 15 ТАНЬГА", OnlyIf = "ТАНЬГА >= 15" },
                    new Option { Destination = 21, Text = "Бросить в блюдо 25 ТАНЬГА", OnlyIf = "ТАНЬГА >= 25" },
                    new Option { Destination = 163, Text = "Алдару удалось получить комнату в караван-сарае", OnlyIf = "Room" },
                    new Option { Destination = 172, Text = "Он будет спать во внутреннем дворе у костра" },
                }
            },
            [127] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 150,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [128] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Далее" },
                }
            },
            [129] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 174, Text = "Далее" },
                }
            },
            [130] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 11,
                        GuessBonus = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 181, Text = "В случае успеха" },
                    new Option { Destination = 108, Text = "В случае провала" },
                    new Option { Destination = 43, Text = "Алдара Косе помнит наставления Афанасия", OnlyIf = "TipsFromAthanasius" },
                    new Option { Destination = 137, Text = "Есть знак Тенгри", OnlyIf = "TengriSymbol" },
                    
                }
            },
            [131] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 125, Text = "Если у Алдара Косе есть 10 ТАНЬГА и желание, принять вызов", OnlyIf = "ТАНЬГА >= 10" },
                    new Option { Destination = 168, Text = "Покинуть кочевье кипчаков" },
                }
            },
            [132] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 9, Text = "СЛАВА АКЫНА равна 6 или больше", OnlyIf = "СЛАВА_АКЫНА >= 6" },
                    new Option { Destination = 52, Text = "СЛАВА АКЫНА от 3 до 5", OnlyIf = "СЛАВА_АКЫНА >= 3"  },
                    new Option { Destination = 117, Text = "СЛАВА АКЫНА равняется 1 или 2", OnlyIf = "СЛАВА_АКЫНА >= 1" },
                    new Option { Destination = 191, Text = "СЛАВА АКЫНА осталась на нуле или даже уменьшилась" },
                }
            },
            [133] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 183, Text = "Сдаться на милость победителя" },
                    new Option { Destination = 193, Text = "Попытаться обхитрить воина" },
                    new Option { Destination = 173, Text = "Вступить с ним в борьбу" },
                }
            },
            [134] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [135] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить одежду",
                        Text = "ПРАЗДНИЧНАЯ ОДЕЖДА, 30 таньга",
                        Price = 30,
                        Aftertext = "Чистопородных коней на базаре нет. А скакун-полукровка обойдётся в 150 ТАНЬГА с учётом того, что Алдар Косе отдаст своего жеребца торговцу.",
                        Trigger = "PartyClothes",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить скакуна",
                        Text = "СКАКУН-ПОЛУКРОВКА, 150 таньга",
                        Price = 150,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 63, Text = "Откликнуться на вызов", OnlyIf = "PartyClothes" },
                    new Option { Destination = 27, Text = "Отказаться от участия в этом соревновании" },
                }
            },
            [136] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 9,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 166, Text = "В случае успеха" },
                    new Option { Destination = 184, Text = "В случае провала" },
                }
            },
            [137] = new Paragraph
            {
                Trigger = "guess",

                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "Вернуться и пройти проверку ещё раз" },
                }
            },
            [138] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 12,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 12,
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = -15,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 29, Text = "Если обе проверки удачны" },
                    new Option { Destination = 39, Text = "Если только одна из них удачна (неважно какая)" },
                    new Option { Destination = 79, Text = "Если обе проверки провалены" },
                }
            },
            [139] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = -25,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Далее" },
                }
            },
            [140] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 165, Text = "Далее" },
                }
            },
            [141] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 58, Text = "Далее" },
                }
            },
            [142] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 15, Text = "Далее" },
                }
            },
            [143] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 84, Text = "Отправиться на местный базар" },
                    new Option { Destination = 12, Text = "Позаботиться о ночлеге" },
                }
            },
            [144] = new Paragraph
            {
                Trigger = "KhansRing",

                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Далее" },
                }
            },
            [145] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 20,
                    },
                },

                Trigger = "DonsHorse",

                Options = new List<Option>
                {
                    new Option { Destination = 175, Text = "Далее" },
                }
            },
            [146] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Согласиться посетить поселение ойратов и поговорить с их предводителем" },
                    new Option { Destination = 140, Text = "Отказаться и продолжить путь дальше вдоль Иртыша" },
                }
            },
            [147] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить арак",
                        Text = "АРАК, 10 таньга",
                        Price = 10,
                        Trigger = "Arak",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 118, Text = "Алдар Косе получал от Лихарева крупную наградуа", OnlyIf = "LargeSum" },
                    new Option { Destination = 145, Text = "Лихарев обещал подарить скакуна", OnlyIf = "GiftPromise" },
                    new Option { Destination = 175, Text = "Покинуть крепость" },
                }
            },
            [148] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить мудрость",
                        Stat = "Wisdom",
                        Level = 11,
                    },
                },

                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Init = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 48, Text = "В случае успеха" },
                    new Option { Destination = 69, Text = "В случае провала" },
                }
            },
            [149] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 160,
                    },
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Далее" },
                }
            },
            [150] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
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
                    new Option { Destination = 15, Text = "Далее" },
                }
            },
            [153] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Value = +1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 18, Text = "Далее", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0" },
                }
            },
            [154] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Далее" },
                }
            },
            [155] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 24, Text = "Есть праздничная одежда", OnlyIf = "PartyClothes" },
                    new Option { Destination = 37, Text = "Праздничной одежды нет" },
                }
            },
            [156] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить домбру",
                        Text = "ДОМБРА, 10 таньга",
                        Price = 10,
                        Aftertext = "Домбра необходима, если Алдар решит принять участие в конкурсе акынов, который сейчас начнётся.",
                        Trigger = "Dombra",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Купить одежду",
                        Text = "ПРАЗДНИЧНАЯ ОДЕЖДА, 15 таньга",
                        Price = 15,
                        Trigger = "PartyClothes",
                        Aftertext = "Праздничная одежда, состоящая из расшитого золотистыми узорами халата, шапки с лисьим мехом и нательной рубашки с вышивкой. Ну а нарядная одежда всегда полезна, чтобы произвести хорошее впечатление на хана и его окружение. Не в лохмотьях же, в конце концов, соваться в ханскую ставку?\n\nКупить всё, что нужно и на что хватит денег, и отметить соответствующие изменения на листе персонажа.",
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 74, Text = "Принять участие в айтысе", OnlyIf = "Dombra" },
                    new Option { Destination = 94, Text = "Отказаться от участия и просто посмотреть на соревнование" },
                }
            },
            [157] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 82, Text = "Далее" },
                }
            },
            [158] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 11,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 17, Text = "В случае успеха" },
                    new Option { Destination = 77, Text = "В случае провала" },
                }
            },
            [159] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -1,
                    },
                    new Modification
                    {
                        Name = "Tanga",
                        Value = -10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 19, Text = "Далее" },
                }
            },
            [160] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 152, Text = "Использовать огонь из костра" },
                    new Option { Destination = 170, Text = "Использовать лук и стрелы" },
                }
            },
            [161] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 6, Text = "Есть ханский перстень", OnlyIf = "KhansRing" },
                    new Option { Destination = 6, Text = "Если письмо Гулинши-багатура", OnlyIf = "Letter" },
                    new Option { Destination = 6, Text = "Если сообщение из бухарского каравана", OnlyIf = "Message" },
                    new Option { Destination = 6, Text = "Если записка Джанибека", OnlyIf = "Note" },
                    new Option { Destination = 16, Text = "Если Темир-батыр был посрамлён", OnlyIf = "ShameOfTemir" },
                    new Option { Destination = 16, Text = "ПОПУЛЯРНОСТЬ Алдара Косе равняется 10 или больше", OnlyIf = "ПОПУЛЯРНОСТЬ > 9" },
                    new Option { Destination = 34, Text = "Кочевник знает человека по имени Серик", OnlyIf = "Serik" },
                    new Option { Destination = 49, Text = "Посулить денег охране" },
                    new Option { Destination = 83, Text = "Попытаться уговорить" },
                }
            },
            [162] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 107, Text = "Далее" },
                }
            },
            [163] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [164] = new Paragraph
            {
                Trigger = "Serik",

                Options = new List<Option>
                {
                    new Option { Destination = 94, Text = "Далее" },
                }
            },
            [165] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 130, Text = "Есть зелёный камень", OnlyIf = "GreenStone" },
                    new Option { Destination = 157, Text = "Далее" },
                }
            },
            [166] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Trigger = "Message",

                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Далее" },
                }
            },
            [167] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Алдару удалось получить комнату в караван-сарае", OnlyIf = "Room" },
                    new Option { Destination = 172, Text = "Он будет спать во внутреннем дворе у костра" },
                }
            },
            [168] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 140, Text = "На север к русской крепости" },
                    new Option { Destination = 8, Text = "На запад к караван-сараю" },
                }
            },
            [169] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 18, Text = "Далее", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0" },
                }
            },
            [170] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 8,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 142, Text = "В случае успеха" },
                    new Option { Destination = 122, Text = "В случае провала" },
                }
            },
            [171] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Далее" },
                }
            },
            [172] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Kumis",
                        Empty = true,
                    },
                    new Modification
                    {
                        Name = "Tanga",
                        Empty = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [173] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 10,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 85, Text = "Если обе проверки успешны" },
                    new Option { Destination = 25, Text = "Если только одна из них успешна (неважно какая)" },
                    new Option { Destination = 183, Text = "Если обе проверки провалились" },
                }
            },
            [174] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Empty = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 42, Text = "ПОПУЛЯРНОСТЬ Алдара Косе равняется 6 или больше", OnlyIf = "ПОПУЛЯРНОСТЬ > 5" },
                    new Option { Destination = 11, Text = "ПОПУЛЯРНОСТЬ Алдара Косе меньше 6", OnlyIf = "ПОПУЛЯРНОСТЬ <= 5" },
                }
            },
            [175] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 155, Text = "Далее" },
                }
            },
            [176] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Далее" },
                }
            },
            [177] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 104, Text = "Далее" },
                }
            },
            [178] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 100,
                    },
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Далее" },
                }
            },
            [179] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Kumis",
                        Value = 1,
                    },
                },

                Trigger = "PartyClothes",

                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [180] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 12,
                    },
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 12,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 13, Text = "Если обе проверки успешны" },
                    new Option { Destination = 171, Text = "Если хотя бы одна из них провалена" },
                }
            },
            [181] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 73, Text = "Далее" },
                }
            },
            [182] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "UnitOfTime",
                        Value = -2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 96, Text = "Догнал", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ <= 0" },
                    new Option { Destination = 18, Text = "Далее", OnlyIf = "ЕДИНИЦЫ_ВРЕМЕНИ > 0" },
                }
            },
            [183] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 56, Text = "Далее" },
                }
            },
            [184] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 62, Text = "Далее" },
                }
            },
            [185] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 104, Text = "Далее" },
                }
            },
            [186] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 150,
                    },
                },

                Trigger = "LargeSum",

                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "Далее" },
                }
            },
            [187] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить красноречие",
                        Stat = "Oratory",
                        Level = 20,
                        GreatKhanSpecialCheck = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 81, Text = "В случае успеха" },
                    new Option { Destination = 116, Text = "В случае провала" },
                    new Option { Destination = 102, Text = "Если письмо Гулинши-багатура", OnlyIf = "Letter" },
                    new Option { Destination = 102, Text = "Если сообщение из бухарского каравана", OnlyIf = "Message" },
                    new Option { Destination = 102, Text = "Если записка Джанибека", OnlyIf = "Note" },
                }
            },
            [188] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 168, Text = "Далее" },
                }
            },
            [189] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Kumis",
                        Value = 1,
                    },
                },

                Trigger = "KhansRing",

                Options = new List<Option>
                {
                    new Option { Destination = 93, Text = "Далее" },
                }
            },
            [190] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить силу",
                        Stat = "Strength",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 127, Text = "В случае успеха" },
                    new Option { Destination = 72, Text = "В случае провала" },
                }
            },
            [191] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = -1,
                    },
                    new Modification
                    {
                        Name = "AkynGlory",
                        Empty = true,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 198, Text = "Далее" },
                }
            },
            [192] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 2,
                    },
                },

                Trigger = "ShameOfTemir",

                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Далее" },
                }
            },
            [193] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить хитрость",
                        Stat = "Cunning",
                        Level = 7,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 146, Text = "В случае успеха" },
                    new Option { Destination = 183, Text = "В случае провала" },
                }
            },
            [194] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Tanga",
                        Value = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 104, Text = "Далее" },
                }
            },
            [195] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 163, Text = "Алдару удалось получить комнату в караван-сарае", OnlyIf = "Room" },
                    new Option { Destination = 172, Text = "Он будет спать во внутреннем дворе у костра" },
                }
            },
            [196] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 1,
                    },
                },

                Trigger = "GiftPromise",

                Options = new List<Option>
                {
                    new Option { Destination = 157, Text = "Далее" },
                }
            },
            [197] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "AkynGlory",
                        Value = 1,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 22, Text = "Далее" },
                }
            },
            [198] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 57, Text = "Старик" },
                    new Option { Destination = 64, Text = "Старуха" },
                    new Option { Destination = 70, Text = "Молодой джигит" },
                }
            },
            [199] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Popularity",
                        Value = 3,
                    },
                },

                Trigger = "ShameOfTemir",

                Options = new List<Option>
                {
                    new Option { Destination = 161, Text = "Далее" },
                }
            },
            [200] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Далее" },
                }
            },
            [201] = new Paragraph
            {
                Modification = new List<Modification>
                {
                    new Modification
                    {
                        Name = "Wisdom",
                        Value = 2,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 99, Text = "Начать сначала" },
                }
            },
            [202] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 31, Text = "Далее" },
                }
            },
            [203] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Test",
                        ButtonName = "Проверить ловкость",
                        Stat = "Skill",
                        Level = 10,
                    },
                },

                Options = new List<Option>
                {
                    new Option { Destination = 110, Text = "В случае успеха" },
                    new Option { Destination = 200, Text = "В случае провала" },
                }
            },
            [204] = new Paragraph
            {
                Options = new List<Option>
                {
                    new Option { Destination = 0, Text = "Начать сначала" },
                }
            },
            [205] = new Paragraph
            {
                Actions = new List<Actions>
                {
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить силы",
                        Text = "СИЛА",
                        Stat = "Strength",
                        Aftertext = "Способность персонажа поднимать и переносить тяжести, бороться, а также общая выносливость.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить ловкости",
                        Text = "ЛОВКОСТЬ",
                        Stat = "Skill",
                        Aftertext = "Скорость реакции, умение держать равновесие, уворачиваться, прыгать, а также ловкость рук (точные движения кистей и пальцев, включая игру на музыкальных инструментах).",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить мудрости",
                        Text = "МУДРОСТЬ",
                        Stat = "Wisdom",
                        Aftertext = "Общие знания, жизненный опыт, способность к анализу и логическим рассуждениям, умение замечать детали и делать правильные выводы.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить хитрости",
                        Text = "ХИТРОСТЬ",
                        Stat = "Cunning",
                        Aftertext = "Способность обманывать, жульничать, воровские навыки, а также находчивость и умение быстро находить решения в сложных ситуациях.",
                    },
                    new Actions
                    {
                        ActionName = "Get",
                        ButtonName = "Добавить красноречия",
                        Text = "КРАСНОРЕЧИЕ",
                        Stat = "Oratory",
                        Aftertext = "Способность разговорить собеседника, дар убеждения, умение слагать стихи и песни.\n\nАвтор советует не распылять эти пункты, а определить те качества, которые вы считаете важными. Ведь каждый человек уникален. Каким вы видите народного героя? Кроме того, книга-игра тем и хороша, что приключение можно повторить, выбрав нового персонажа с другими навыками.",
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
