namespace NJ.Common.EqComparers
{
  public interface IDerivedTypeEqualityComparer<in TSuperType>
  {
    bool CastToDerivedTypeAndCheckEquality(TSuperType x, TSuperType y);

    int CastToDerivedTypeAndGetHashCode(TSuperType obj);

    bool CanCastToDerivedType(params TSuperType[] obj);
  }
}
