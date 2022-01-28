﻿namespace MyTestedAspNetCoreApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ShortStringService : IShortStringService
    {
        public string GetShort(string str, int maxLength)
        {
            if (str == null)
            {
                return str;
            }

            if (str.Length <= maxLength)
            {
                return str;
            }

            return str.Substring(0, maxLength) + "....";
        }
    }
}
