using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MyTestedAspNetCoreApp.Models
{
    using System;

    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    }
}
