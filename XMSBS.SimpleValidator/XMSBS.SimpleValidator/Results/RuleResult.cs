using System;
using System.Collections.Generic;
using System.Text;
using XMSBS.SimpleValidator.Exceptions;

namespace XMSBS.SimpleValidator.Results
{
    public class RuleResult
    {
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }

        public RuleResult()
        {
            ErrorMessages = new List<string>();
        }
    }
}
