﻿using System;

namespace Seeker.Gamebook.Moria
{
    class Paragraphs : Prototypes.Paragraphs, Abstract.IParagraphs
    {
        public static Paragraphs StaticInstance = new Paragraphs();
    }
}
