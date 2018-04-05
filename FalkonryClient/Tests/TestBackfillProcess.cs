using NUnit.Framework;
using FalkonryClient.Helper.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace FalkonryClient.Tests
{
  [TestFixture()]
  public class TestBackfillProcess
  {
    static string host = System.Environment.GetEnvironmentVariable("FALKONRY_HOST_URL");
    static string token = System.Environment.GetEnvironmentVariable("FALKONRY_TOKEN");
    static string datastreamId = System.Environment.GetEnvironmentVariable("FALKONRY_DATASTREAM_ID");
    static string assessmentId = System.Environment.GetEnvironmentVariable("FALKONRY_ASSESSMENT_ID");

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

    EventSource eventSource;

    //Handles live streaming output
    private void EventSource_Message(object sender, EventSource.ServerSentEventArgs e)
    {
      try
      { var falkonryEvent = JsonConvert.DeserializeObject<FalkonryEvent>(e.Data); }
      catch (System.Exception exception)
      {
        // exception in parsing the event
        Assert.AreEqual(exception.Message, null, "Error listening for backfill data");
      }
    }

    //Handles any error while fetching the live streaming output
    private void EventSource_Error(object sender, EventSource.ServerSentErrorEventArgs e)
    {
      // error connecting to Falkonry service for output streaming
      Assert.AreEqual(e.Exception.Message, null, "Error listening for backfill data");
    }

    [Test()]
    //[Ignore("To be executed with model is successfully learned")]
    public void TestBackfillProcessOutput()
    {
      try
      {
        // Start the backfill process
        OutputStateRequest outputStateRequest = new OutputStateRequest();
        outputStateRequest.Datastream = datastreamId;
        List<string> assessmentList = new List<string>();
        assessmentList.Add(assessmentId);
        outputStateRequest.Assessment = assessmentList;
        OutputStateResponse outputStateResponse = _falkonry.StartBackfillProcess(outputStateRequest);
        Assert.AreNotEqual(null, outputStateResponse.OutputStateId);
        Assert.AreEqual(outputStateResponse.OutputUrl.Count > 0, true);


        // Start listening on output
        eventSource = _falkonry.GetOutputDataFromBackfillProcess(outputStateResponse.OutputStateId, assessmentId);

        //On successfull live streaming output EventSource_Message will be triggered
        eventSource.Message += EventSource_Message;

        //On any error while getting live streaming output, EventSource_Error will be triggered
        eventSource.Error += EventSource_Error;

        //Keep stream open for 60sec
        //System.Threading.Thread.Sleep(20000);


        // Start sending streaming data to input url from outputStateResponse
        // Input data should match the datastreamId's data format otherwise it will return error
        var data = "time,unit,signal1,signal2,batch \n" + "1522774573095, UNIT-1, 12.4, 45.30, 12 \n 1522774578122, UNIT-1, 12.4, 45.30, 12";
        var inputstatus = _falkonry.AddInputDataToBackfillProcess(outputStateResponse.OutputStateId, datastreamId, data, null);

        //check data status
        //CheckStatus(inputstatus.Id);
        Assert.AreEqual(inputstatus.Message, "Data submitted successfully");

        eventSource.Dispose();

        //System.Threading.Thread.Sleep(20000);

        // stop the backfill process

        _falkonry.StopBackfillProcess(outputStateResponse.OutputStateId);

      }
      catch (System.Exception exception)
      {
        Assert.AreEqual(exception.Message, null, "Error listening for live output");
      }
    }
  }
}