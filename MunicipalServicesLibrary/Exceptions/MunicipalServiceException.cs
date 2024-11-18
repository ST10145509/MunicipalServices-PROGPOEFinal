using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Custom exception class for handling Municipal Service specific errors
/// Extends the base Exception class with additional ErrorCode property
/// </summary>
public class MunicipalServiceException : Exception
{
    /// <summary>
    /// Gets the error code associated with this exception
    /// </summary>
    public string ErrorCode { get; }

    /// <summary>
    /// Constructor for creating a new MunicipalServiceException with a message and error code
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="errorCode">The specific error code for this exception</param>
    public MunicipalServiceException(string message, string errorCode)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Constructor for creating a new MunicipalServiceException with a message, error code, and inner exception
    /// </summary>
    /// <param name="message">The error message</param>
    /// <param name="errorCode">The specific error code for this exception</param>
    /// <param name="innerException">The inner exception that caused this exception</param>
    public MunicipalServiceException(string message, string errorCode, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
}
