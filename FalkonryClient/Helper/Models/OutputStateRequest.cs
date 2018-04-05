using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace FalkonryClient.Helper.Models
{
  public class OutputStateRequest
  {
    public string Datastream
    {
      get;
      set;
    }

    public List<string> Assessment
    {
      get;
      set;
    }
  }
}
