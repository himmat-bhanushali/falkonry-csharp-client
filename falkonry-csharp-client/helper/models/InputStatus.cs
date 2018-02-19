﻿using Newtonsoft.Json;

namespace falkonry_csharp_client.helper.models
{
  public class InputStatus
  {
    public string Status
    {
      get;
      set;
    }
    [JsonProperty("__$id")]
    public string Id
    {
      get;
      set;
    }
    public string Tenant
    {
      get;
      set;
    }
    public long CreateTime
    {
      get;
      set;
    }
    public string Action
    {
      get;
      set;
    }

    public string Message
    {
      get;
      set;
    }
  }
}
