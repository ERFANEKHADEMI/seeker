﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Gamebook
{
    class Paragraph
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<Option> Options { get; set; }
    }
}
