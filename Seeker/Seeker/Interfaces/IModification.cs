﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seeker.Interfaces
{
    interface IModification
    {
        string Name { get; set; }
        int Value { get; set; }

        void Do();
    }
}
