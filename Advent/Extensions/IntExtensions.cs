﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent.Extensions
{
    public static class IntExtensions
    {
        public static int Mod(this int a, int n)
        {
            if (n == 0)
                throw new ArgumentOutOfRangeException("n", "(a mod 0) is undefined.");

            //puts a in the [-n+1, n-1] range using the remainder operator
            int remainder = a % n;

            //if the remainder is less than zero, add n to put it in the [0, n-1] range if n is positive
            //if the remainder is greater than zero, add n to put it in the [n-1, 0] range if n is negative
            if ((n > 0 && remainder < 0) ||
                (n < 0 && remainder > 0))
                return remainder + n;
            return remainder;
        }
    }
}
