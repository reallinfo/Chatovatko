﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Server.Database
{
    internal static class IDGenerator
    {
        private static ulong theLastOne = 0;
        public static ulong getNext()
        {
            return theLastOne++;
        }
    }
}
