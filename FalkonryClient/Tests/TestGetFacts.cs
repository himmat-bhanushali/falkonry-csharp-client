using NUnit.Framework;
using FalkonryClient.Helper.Models;
using System.Collections.Generic;

namespace FalkonryClient.Tests
{
  [TestFixture()]
  public class TestGetFacts
  {
    static string host = System.Environment.GetEnvironmentVariable("FALKONRY_HOST_URL");
    static string token = System.Environment.GetEnvironmentVariable("FALKONRY_TOKEN");
    readonly Falkonry _falkonry = new Falkonry(host, token);
    List<Datastream> _datastreams = new List<Datastream>();

    private void CheckStatus(System.String trackerId)
    {
      for (int i = 0; i < 12; i++)
      {
        Tracker tracker = _falkonry.GetStatus(trackerId);
        if (tracker.Status.Equals("FAILED") || tracker.Status.Equals("ERROR"))
        {
          throw new System.Exception(tracker.Message);
        }
        else if (tracker.Status.Equals("SUCCESS") || tracker.Status.Equals("COMPLETED"))
        {
          break;
        }
        System.Threading.Thread.Sleep(5000);
      }
    }

    [Test()]
    public void GetFacts()
    {
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var time = new Time
      {
        Zone = "GMT",
        Identifier = "time",
        Format = "iso_8601"
      };
      var datasource = new Datasource
      {
        Type = "PI",
        Host = "https://test.piserver.com/piwebapi",
        ElementTemplateName = "SampleElementTempalte"
      };
      var ds = new DatastreamRequest();

      var Signal = new Signal
      {
        ValueIdentifier = "value",
        SignalIdentifier = "signal"
      };
      var Field = new Field
      {
        Signal = Signal,
        Time = time,
        EntityIdentifier = "Unit"
      };
      ds.Field = Field;
      ds.DataSource = datasource;
      ds.Name = "TestDS" + randomNumber;
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);
        Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
        Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
        Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);

        datastream = _falkonry.GetDatastream(datastream.Id);

        //add data
        var data = "time,Unit,current,vibration,state\n 2016-05-05T12:00:00.000Z,Unit1,12.4,3.4,On";
        var options = new SortedDictionary<string, string>
        {
          { "timeIdentifier", "time" },
          { "timeFormat", "iso_8601" },
          { "timeZone", time.Zone },
          { "streaming", "false" },
          { "hasMoreData", "false" },
          { "entityIdentifier", "Unit" }
        };
        InputStatus inputstatus = _falkonry.AddInput(datastream.Id, data, options);

        //check data status
        CheckStatus(inputstatus.Id);

        // add assessment
        var asmt = new AssessmentRequest();
        var randomNumber1 = System.Convert.ToString(rnd.Next(1, 10000));
        asmt.Name = "TestAssessment" + randomNumber1;
        asmt.Datastream = datastream.Id;
        var assessment = _falkonry.CreateAssessment(asmt);

        var optionsFacts = new SortedDictionary<string, string>
        {
          {"startTimeIdentifier", "time"},
          {"endTimeIdentifier", "end"},
          {"timeFormat", "iso_8601"},
          {"timeZone", time.Zone },
          { "entityIdentifier", datastream.Field.EntityIdentifier},
          { "valueIdentifier" , "Health"}
        };

        var data1 = "time,end," + datastream.Field.EntityIdentifier
      + ",Health\n2011-03-31T00:00:00.000Z,2011-04-01T00:00:00.000Z,Unit1,Normal\n2011-03-31T00:00:00.000Z,2011-04-01T00:00:00.000Z,Unit1,Normal";
        inputstatus = _falkonry.AddFacts(assessment.Id, data1, optionsFacts);

        //check data status
        CheckStatus(inputstatus.Id);

        /// Get Facts
        options = new SortedDictionary<string, string>
        {
          { "startTime", "2011-03-30T00:00:00.000Z" },
          { "endTime", "2011-04-01T00:00:00.000Z" },
          { "responseFormat", "application/json" }
        };
        var factsData = _falkonry.getFacts(assessment.Id, options);
        Assert.AreEqual(factsData.Response.Length > 0, true);
        Assert.AreEqual(factsData.Response.ToLower().Contains(optionsFacts["startTimeIdentifier"].ToLower()), true);
      }
      catch (System.Exception exception)
      {

        Assert.AreEqual(exception.Message, null, "Cannot get facts");
      }
    }

    //Get Facts for batch datastream
    [Test()]
    public void GetFactsForBatchDatastream()
    {
      var rnd = new System.Random();
      var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
      var time = new Time
      {
        Zone = "GMT",
        Identifier = "time",
        Format = "iso_8601"
      };
      var datasource = new Datasource
      {
        Type = "PI",
        Host = "https://test.piserver.com/piwebapi",
        ElementTemplateName = "SampleElementTempalte"
      };
      var Signal = new Signal
      {
        ValueIdentifier = "value",
        SignalIdentifier = "signal"
      };
      var Field = new Field
      {
        Signal = Signal,
        Time = time,
        EntityIdentifier = "Unit",
        BatchIdentifier = "Batch"
      };
      var ds = new DatastreamRequest
      {
        Field = Field,
        DataSource = datasource,
        Name = "TestDS" + randomNumber
      };
      ds.DataSource = datasource;
      try
      {
        var datastream = _falkonry.CreateDatastream(ds);
        _datastreams.Add(datastream);
        Assert.AreEqual(ds.Name, datastream.Name);
        Assert.AreNotEqual(null, datastream.Id);
        Assert.AreEqual(ds.Field.Time.Format, datastream.Field.Time.Format);
        Assert.AreEqual(ds.Field.Time.Identifier, datastream.Field.Time.Identifier);
        Assert.AreEqual(ds.DataSource.Type, datastream.DataSource.Type);

        datastream = _falkonry.GetDatastream(datastream.Id);

        //add data
        var data = "time,Unit,current,vibration,state,batch\n 2016-05-05T12:00:00.000Z,Unit1,12.4,3.4,On,batch1";
        var options = new SortedDictionary<string, string>
                {
                    { "timeIdentifier", "time" },
                    { "timeFormat", "iso_8601" },
                    { "timeZone", time.Zone },
                    { "streaming", "false" },
                    { "hasMoreData", "false" },
                    { "entityIdentifier", "Unit" },
                    { "batchIdentifier", "batch" }
                };
        InputStatus inputstatus = _falkonry.AddInput(datastream.Id, data, options);

        //check data status
        CheckStatus(inputstatus.Id);

        // add assessment
        var asmt = new AssessmentRequest();
        var randomNumber1 = System.Convert.ToString(rnd.Next(1, 10000));
        asmt.Name = "TestAssessment" + randomNumber1;
        asmt.Datastream = datastream.Id;
        var assessment = _falkonry.CreateAssessment(asmt);

        var optionsFacts = new SortedDictionary<string, string>
        {
          { "entityIdentifier", datastream.Field.EntityIdentifier},
          { "valueIdentifier" , "Health"},
          { "batchIdentifier" , "Batch"}
        };

        var data1 = datastream.Field.EntityIdentifier
      + ",Health,Batch\nUnit1,Normal,batch1\nUnit1,Normal,batch2";
        inputstatus = _falkonry.AddFacts(assessment.Id, data1, optionsFacts);

        //check data status
        CheckStatus(inputstatus.Id);

        /// Get Facts
        options = new SortedDictionary<string, string>
        {
          { "responseFormat", "application/json" }
        };

        System.Threading.Thread.Sleep(60000);
        var factsData = _falkonry.getFacts(assessment.Id, options);
        Assert.AreEqual(factsData.Response.Length > 0, true);
        Assert.AreEqual(factsData.Response.ToLower().Contains(optionsFacts["batchIdentifier"].ToLower()), true);
      }
      catch (System.Exception exception)
      {

        Assert.AreEqual(exception.Message, null, "Cannot get facts");
      }
    }
    [TearDown()]
    public void Cleanup()
    {
      if (_datastreams.Count > 0)
      {
        var ds = _datastreams[0];
        _datastreams.Clear();
        _falkonry.DeleteDatastream(ds.Id);
      }
    }
  }
}