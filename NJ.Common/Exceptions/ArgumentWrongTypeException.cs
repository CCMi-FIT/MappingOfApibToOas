using System;
using System.Runtime.Serialization;

namespace NJ.Common.Exceptions
{
  public class ArgumentWrongTypeException : ArgumentException
  {
    private const string Format = "Argument {0} has wrong type.";

    public ArgumentWrongTypeException()
    {
    }

    public ArgumentWrongTypeException(string paramName) : base(string.Format(Format, paramName), paramName)
    {
    }

    public ArgumentWrongTypeException(string paramName, Exception innerException) : base(string.Format(Format, paramName), innerException)
    {
    }


    protected ArgumentWrongTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}
