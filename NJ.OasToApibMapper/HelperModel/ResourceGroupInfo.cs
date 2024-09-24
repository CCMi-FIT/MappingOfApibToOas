namespace NJ.OasToApibMapper.HelperModel
{
  public class ResourceGroupInfo
  {
    public string Name { get; set; }
    public string Description { get; set; }

    public ResourceGroupInfo(string name, string description)
    {
      Name = name;
      Description = description;
    }
  }
}
