using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MunicipalServiceException : Exception
{
    public string ErrorCode { get; }

    public MunicipalServiceException(string message, string errorCode)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    public MunicipalServiceException(string message, string errorCode, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
