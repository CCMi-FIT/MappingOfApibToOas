﻿namespace NJ.OasModel;

public class DiscriminatorObject
{
  public string PropertyName { get; init; }
  public IReadOnlyDictionary<string, string> Mapping { get; init; }
}