namespace MyTestedAspNetCoreApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IShortStringService
    {
        string GetShort(string str, int maxLength);
    }
}
