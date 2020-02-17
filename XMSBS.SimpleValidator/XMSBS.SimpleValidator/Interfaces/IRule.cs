using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XMSBS.SimpleValidator.Results;

namespace XMSBS.SimpleValidator.Interfaces
{
    public interface IRule
    {
        Task<RuleResult> ExecuteAsync();
    }
}
