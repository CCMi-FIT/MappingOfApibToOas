﻿namespace NJ.ApibModel;

public class BodySection : AssetSection
{
  public override string Keyword { get; set; } = "";

  public BodySection(string content = null) : base(content)
  {
    
  }
}