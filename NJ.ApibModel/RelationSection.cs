﻿namespace NJ.ApibModel;

public class RelationSection
{
  public string Identifier { get; set; }

  public RelationSection(string identifier = null)
  {
    Identifier = identifier;
  }
}