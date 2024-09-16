namespace NJ.OasModel.AdditionalDomainObjects
{
  // https://swagger.io/specification/#style-values
  public enum StyleValue
  {
    Simple, // https://datatracker.ietf.org/doc/html/rfc6570#section-3.2.2
    Matrix, // https://datatracker.ietf.org/doc/html/rfc6570#section-3.2.7
    Label, // https://datatracker.ietf.org/doc/html/rfc6570#section-3.2.5
    Form, // https://datatracker.ietf.org/doc/html/rfc6570#section-3.2.8
    SpaceDelimited,
    PipeDelimited,
    DeepObject
  }
}
