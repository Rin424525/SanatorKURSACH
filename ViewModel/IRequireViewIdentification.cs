using System;

namespace Sanator
{
    internal interface IRequireViewIdentification
    {
        Guid ViewID { get; }
    }
}