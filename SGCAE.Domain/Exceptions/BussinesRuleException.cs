using System;
using System.Collections.Generic;
using System.Text;

namespace SGCAE.Domain.Exceptions;

public class BussinesRuleException : Exception
{
    public BussinesRuleException(string message)
        : base(message)
    {
    }
}

