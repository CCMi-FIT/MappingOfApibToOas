using System.Collections.Generic;
using System.Linq;

namespace NJ.Common.EqComparers
{
  public class DerivedTypeEqualityComparer<TDerivedType, TSuperType> : IDerivedTypeEqualityComparer<TSuperType> where TDerivedType : TSuperType
  {
    private readonly IEqualityComparer<TDerivedType> _innerComparer;

    public DerivedTypeEqualityComparer(IEqualityComparer<TDerivedType> innerComparer)
    {
      this._innerComparer = innerComparer;
    }

    public bool CastToDerivedTypeAndCheckEquality(TSuperType x, TSuperType y)
    {
      var typedX = (TDerivedType)x!;
      var typedY = (TDerivedType)y!;
      if (ReferenceEquals(typedX, typedY))
        return true;
      var result = _innerComparer.Equals(typedX, typedY);
      return result;
    }

    public int CastToDerivedTypeAndGetHashCode(TSuperType obj)
    {
      if (obj is null)
        return 0;
      var typedObj = (TDerivedType)obj;
      var result = _innerComparer.GetHashCode(typedObj);
      return result;
    }

    public bool CanCastToDerivedType(params TSuperType[] @params)
    {
      var result = @params.All(CanCastParameterToDerivedType);
      return result;
    }

    private bool CanCastParameterToDerivedType(TSuperType parameter)
    {
      if (parameter is null && default(TDerivedType) is null)
        return true;
      var result = parameter is TDerivedType;
      return result;
    }
  }
}
