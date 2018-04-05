using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace FalkonryClient.Helper.Models
{
  public class OutputStateResponse
  {
    public string InputUrl
    {
      get;
      set;
    }

    public string OutputStateId
    {
      get;
      set;
    }

    public string StopUrl
    {
      get;
      set;
    }

    public List<OutputUrl> OutputUrl
    {
      get;
      set;
    }
    public string ToJson()
    {
      return new JavaScriptSerializer().Serialize(this);
    }

  }
}
