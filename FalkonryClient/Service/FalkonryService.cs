using System;
using System.Collections.Generic;
using FalkonryClient.Helper.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace FalkonryClient.Service
{
  internal class FalkonryService
  {
    public string Host { get; }
    public string Token { get; }
    private readonly HttpService _http;

    public FalkonryService(string host, string token, SortedDictionary<string, string> _piOptions = null)
    {
      Host = host;
      Token = token;
      _http = new HttpService(host, token, _piOptions);

    }

    // Create Datastream
    public Datastream CreateDatastream(DatastreamRequest datastream)
    {
      try
      {
        var data = JsonConvert.SerializeObject(datastream, Formatting.Indented,
            new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

        var datastreamJson = _http.Post("/datastream", data);


        return JsonConvert.DeserializeObject<Datastream>(datastreamJson);
      }
      catch (Exception)
      {
        throw;
      }
    }

    // List Datastream
    public List<Datastream> GetDatastream()
    {
      try
      {
        var datastreamJson = _http.Get("/datastream");
        return JsonConvert.DeserializeObject<List<Datastream>>(datastreamJson);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Get Datastream by id
    public Datastream GetDatastream(string id)
    {
      var url = "/datastream/" + id;

      try
      {
        var datastreamJson = _http.Get(url);
        return JsonConvert.DeserializeObject<Datastream>(datastreamJson);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Add data to DataStream
    public InputStatus AddInputData(string datastream, string data, SortedDictionary<string, string> options)
    {
      try
      {
        var url = getInputIngestionUrl(datastream, options);
        var status = _http.PostData(url, data);
        return JsonConvert.DeserializeObject<InputStatus>(status);
      }
      catch (Exception)
      {

        throw;
      }
    }

    public InputStatus AddInputFromStream(string datastream, byte[] data, SortedDictionary<string, string> options)
    {
      try
      {
        var url = getInputIngestionUrl(datastream, options);
        var status = _http.Upstream(url, data);
        return JsonConvert.DeserializeObject<InputStatus>(status);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Delete Datastream
    public void DeleteDatastream(string datastream)
    {
      try
      {
        _http.Delete("/datastream/" + datastream);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Create Assessment
    public Assessment CreateAssessment(AssessmentRequest assessment)
    {
      try
      {
        var data = JsonConvert.SerializeObject(assessment, Formatting.Indented,
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });

        var assessmentJson = _http.Post("/assessment", data);

        return JsonConvert.DeserializeObject<Assessment>(assessmentJson);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // List Assessment
    public List<Assessment> GetAssessment()
    {
      try
      {
        var assessmentJson = _http.Get("/assessment");
        return JsonConvert.DeserializeObject<List<Assessment>>(assessmentJson);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // get assessment by id
    public Assessment GetAssessment(string assessment)
    {
      try
      {
        var assessmentJson = _http.Get("/assessment/" + assessment);
        return JsonConvert.DeserializeObject<Assessment>(assessmentJson);
      }
      catch (Exception)
      {

        throw;
      }
    }


    // delete Assessment
    public void DeleteAssessment(string assessment)
    {
      try
      {
        _http.Delete("/assessment/" + assessment);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Add Facts to assessment
    public InputStatus AddFacts(string assessment, string data, SortedDictionary<string, string> options)
    {
      try
      {
        var url = get_add_facts_url(assessment, options);
        var status = _http.PostData(url, data);
        return JsonConvert.DeserializeObject<InputStatus>(status);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Get facts data of assessment
    internal HttpResponse GetFacts(string assessment, SortedDictionary<string, string> options)
    {
      try
      {
        var url = "/assessment/" + assessment + "/facts?";
        string modelIndex;
        string startTime;
        string endTime;
        var firstReqParam = true;

        if (options.TryGetValue("modelIndex", out modelIndex))
        {
          if (firstReqParam)
          {
            firstReqParam = false;
            url += "model=" + Uri.EscapeDataString(modelIndex);
          }
          else
            url += "&model=" + Uri.EscapeDataString(modelIndex);

        }
        if (options.TryGetValue("startTime", out startTime))
        {
          if (firstReqParam)
          {
            firstReqParam = false;
            url += "startTime=" + Uri.EscapeDataString(startTime);
          }
          else
            url += "&startTime=" + Uri.EscapeDataString(startTime);

        }
        if (options.TryGetValue("endTime", out endTime))
        {
          if (firstReqParam)
          {
            url += "endTime=" + Uri.EscapeDataString(endTime);
          }
          else
            url += "&endTime=" + Uri.EscapeDataString(endTime);

        }
        string format;
        var responseFormat = "application/json";
        if (options.TryGetValue("responseFormat", out format))
        {
          if (format.Equals("text/csv"))
          {
            responseFormat = "text/csv";
          }
        }

        var factsData = _http.GetOutput(url, responseFormat);
        return factsData;
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Get Input data of datastream
    internal HttpResponse GetDatastreamData(string datastream, SortedDictionary<string, string> options)
    {
      try
      {
        var url = "/datastream/" + datastream + "/data";

        string format;
        var responseFormat = "application/json";
        if (options.TryGetValue("responseFormat", out format))
        {
          if (format.Equals("text/csv"))
          {
            responseFormat = "text/csv";
          }
        }

        var inputData = _http.GetOutput(url, responseFormat);
        return inputData;
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Add facts as stream
    public InputStatus AddFactsStream(string assessment, byte[] stream, SortedDictionary<string, string> options)
    {
      try
      {
        var url = get_add_facts_url(assessment, options);
        var status = _http.Upstream(url, stream);
        return JsonConvert.DeserializeObject<InputStatus>(status);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Stream Output
    public EventSource GetOutput(string assessment, long? start, long? end)
    {
      try
      {
        var url = "/assessment/" + assessment + "/output";

        var starttemp = start;
        var endtemp = end;

        if (endtemp.HasValue)
        {
          url += "?lastTime=" + end.Value;
          if (starttemp.HasValue) url += "&startTime=" + start.Value;
        }
        else
        {
          if (starttemp.HasValue) url += "?startTime=" + start.Value;
        }

        return _http.Downstream(url);
      }
      catch (Exception)
      {

        throw;
      }
    }

    //Stream historical output
    public HttpResponse GetHistoricalOutput(Assessment assessment, SortedDictionary<string, string> options)
    {
      try
      {
        var url = "/assessment/" + assessment.Id + "/output?";
        string trackerId;
        string modelIndex;
        string startTime;
        string endTime;
        var firstReqParam = true;

        if (options.TryGetValue("trackerId", out trackerId))
        {
          firstReqParam = false;
          url += "trackerId=" + Uri.EscapeDataString(trackerId);
        }
        if (options.TryGetValue("modelIndex", out modelIndex))
        {
          if (firstReqParam)
          {
            firstReqParam = false;
            url += "model=" + Uri.EscapeDataString(modelIndex);
          }
          else
            url += "&model=" + Uri.EscapeDataString(modelIndex);

        }
        if (options.TryGetValue("startTime", out startTime))
        {
          if (firstReqParam)
          {
            firstReqParam = false;
            url += "startTime=" + Uri.EscapeDataString(startTime);
          }
          else
            url += "&startTime=" + Uri.EscapeDataString(startTime);

        }
        if (options.TryGetValue("endTime", out endTime))
        {
          if (firstReqParam)
          {
            url += "endTime=" + Uri.EscapeDataString(endTime);
          }
          else
            url += "&endTime=" + Uri.EscapeDataString(endTime);

        }
        string format;
        var responseFormat = "application/json";
        if (options.TryGetValue("responseFormat", out format))
        {
          if (format.Equals("text/csv"))
          {
            responseFormat = "text/csv";
          }
        }

        var outputData = _http.GetOutput(url, responseFormat);
        return outputData;
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Post EntityMeta
    public List<EntityMeta> PostEntityMeta(List<EntityMetaRequest> entityMetaRequest, Datastream datastream)
    {
      try
      {
        var data = JsonConvert.SerializeObject(entityMetaRequest, Formatting.Indented,
                new JsonSerializerSettings() { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        var response = _http.Post("/datastream/" + datastream.Id + "/entityMeta", data);
        return JsonConvert.DeserializeObject<List<EntityMeta>>(response);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // Get EntityMeta
    public List<EntityMeta> GetEntityMeta(Datastream datastream)
    {
      try
      {
        var response = _http.Get("/datastream/" + datastream.Id + "/entityMeta");
        return JsonConvert.DeserializeObject<List<EntityMeta>>(response);
      }
      catch (Exception)
      {

        throw;
      }
    }

    // On  Datastream
    public void onDatastream(string datastreamId, SortedDictionary<string, string> options = null)
    {
      try
      {
        var url = "/datastream/" + datastreamId + "/on?";
        if (options.TryGetValue("assessment", out assessment))
        {
          url += "assessment=" + Uri.EscapeDataString(assessment);
        }
        _http.Post(url, "");
      }
      catch (Exception)
      {

        throw;
      }
    }

    // OFF  Datastream
    public void offDatastream(string datastreamId, SortedDictionary<string, string> options = null)
    {
      try
      {
        var url = "/datastream/" + datastreamId + "/off?";
        if (options.TryGetValue("assessment", out assessment))
        {
          url += "assessment=" + Uri.EscapeDataString(assessment);
        }
        _http.Post(url, "");
      }
      catch (Exception)
      {

        throw;
      }
    }

    private string get_add_facts_url(string assessment, SortedDictionary<string, string> options)
    {
      var url = "/assessment/" + assessment + "/facts?";
      try
      {
        string startTimeIdentifier;
        string endTimeIdentifier;
        string timeFormat;
        string timeZone;
        string entityIdentifier;
        string valueIdentifier;
        string keywordIdentifier;
        string additionalKeyword;
        string batchIdentifier;

        var firstReqParam = true;
        if (options.TryGetValue("startTimeIdentifier", out startTimeIdentifier))
        {
          if (firstReqParam)
            firstReqParam = false;
          else
            url += "&";
          url += "startTimeIdentifier=" + Uri.EscapeDataString(startTimeIdentifier);
        }
        if (options.TryGetValue("endTimeIdentifier", out endTimeIdentifier))
        {
          if (firstReqParam)
            firstReqParam = false;
          else
            url += "&";

          url += "endTimeIdentifier=" + Uri.EscapeDataString(endTimeIdentifier);
        }
        if (options.TryGetValue("timeFormat", out timeFormat))
        {
          if (firstReqParam)
            firstReqParam = false;
          else
            url += "&";
          url += "&timeFormat=" + Uri.EscapeDataString(timeFormat);
        }
        if (options.TryGetValue("timeZone", out timeZone))
        {
          if (firstReqParam)
            firstReqParam = false;
          else
            url += "&";
          url += "timeZone=" + Uri.EscapeDataString(timeZone);
        }
        if (options.TryGetValue("entityIdentifier", out entityIdentifier))
        {
          if (firstReqParam)
            firstReqParam = false;
          else
            url += "&";
          url += "entityIdentifier=" + Uri.EscapeDataString(entityIdentifier);
        }
        if (options.TryGetValue("valueIdentifier", out valueIdentifier))
        {
          if (firstReqParam)
            firstReqParam = false;
          else
            url += "&";
          url += "valueIdentifier=" + Uri.EscapeDataString(valueIdentifier);
        }
        if (options.TryGetValue("additionalKeyword", out additionalKeyword))
        {
          if (firstReqParam)
            firstReqParam = false;
          else
            url += "&";
          url += "additionalKeyword=" + Uri.EscapeDataString(additionalKeyword);
        }
        if (options.TryGetValue("keywordIdentifier", out keywordIdentifier))
        {
          if (firstReqParam)
            firstReqParam = false;
          else
            url += "&";
          url += "keywordIdentifier=" + Uri.EscapeDataString(keywordIdentifier);
        }
        if (options.TryGetValue("batchIdentifier", out batchIdentifier))
        {
          if (firstReqParam)
            firstReqParam = false;
          else
            url += "&";
          url += "batchIdentifier=" + Uri.EscapeDataString(batchIdentifier);
        }

        return url;
      }
      catch (Exception)
      {
        return url;
      }
    }

    private String getInputIngestionUrl(string datastreamId, SortedDictionary<string, string> options = null)
    {
      string streamingValue;
      string hasMoreDataValue;
      string timeFormatValue;
      string timeZoneValue;
      string timeIdentifierValue;
      string entityIdentifierValue;
      string signalIdentifierValue;
      string valueIdentifierValue;
      string batchIdentifierValue;

      var url = "/datastream/" + datastreamId;

      if (options.TryGetValue("streaming", out streamingValue))
      {
        url += "?streaming=" + Uri.EscapeDataString(streamingValue);
      }
      else
      {
        url += "?streaming=true";
      }
      if (options.TryGetValue("hasMoreData", out hasMoreDataValue))
      {
        url += "&hasMoreData=" + Uri.EscapeDataString(hasMoreDataValue);
      }
      else
      {
        url += "&hasMoreData=true";
      }
      if (options.TryGetValue("timeFormat", out timeFormatValue))
      {
        url += "&timeFormat=" + Uri.EscapeDataString(timeFormatValue);
      }
      if (options.TryGetValue("timeZone", out timeZoneValue))
      {
        url += "&timeZone=" + Uri.EscapeDataString(timeZoneValue);
      }
      if (options.TryGetValue("timeIdentifier", out timeIdentifierValue))
      {
        url += "&timeIdentifier=" + Uri.EscapeDataString(timeIdentifierValue);
      }
      if (options.TryGetValue("entityIdentifier", out entityIdentifierValue))
      {
        url += "&entityIdentifier=" + Uri.EscapeDataString(entityIdentifierValue);
      }
      if (options.TryGetValue("signalIdentifier", out signalIdentifierValue))
      {
        url += "&signalIdentifier=" + Uri.EscapeDataString(signalIdentifierValue);
      }
      if (options.TryGetValue("valueIdentifier", out valueIdentifierValue))
      {
        url += "&valueIdentifier=" + Uri.EscapeDataString(valueIdentifierValue);
      }
      if (options.TryGetValue("batchIdentifier", out batchIdentifierValue))
      {
        url += "&batchIdentifier=" + Uri.EscapeDataString(batchIdentifierValue);
      }
      return url;
    }

    public Tracker GetStatus(String trackerId)
    {
      try
      {
        var response = _http.Get("/app/track/" + trackerId);
        return JsonConvert.DeserializeObject<Tracker>(response);
      }
      catch (Exception)
      {
        throw;
      }
    }
  }
}

