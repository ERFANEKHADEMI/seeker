﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class Description
    {
        public delegate void ProtagonistInit();

        public delegate bool CheckOnlyIfFunc(string option);

        public string XmlBook;

        public string BookColor;

        public ProtagonistInit Protagonist;

        public CheckOnlyIfFunc CheckOnlyIf;

        public Interfaces.IParagraphs Paragraphs;

        public Interfaces.IActions Actions;

        public Interfaces.IConstants Constants;

        public string Disclaimer;
    }
}
