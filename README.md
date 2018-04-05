[![Falkonry Logo](https://app.falkonry.ai/img/logo.png)](http://falkonry.com/)

[![NuGet version (FalkonryClient)](https://img.shields.io/nuget/v/FalkonryClient.svg?style=flat-square)](https://www.nuget.org/packages/FalkonryClient/)
[![Run Status](https://api.shippable.com/projects/5734c7032a8192902e1fde1f/badge?branch=master)](https://app.shippable.com/github/Falkonry/falkonry-csharp-client)

Falkonry C# Client to access [Falkonry Condition Prediction](falkonry.com) APIs

[Nuget Package Manager](https://www.nuget.org/packages/FalkonryClient) | [Releases](https://github.com/Falkonry/falkonry-csharp-client/releases)

## Features

  * Create Datastream for narrow/historian style data from a single entity
  * Create Batch Datastream for narrow/historian style data from a single entity
  * Create Datastream for narrow/historian style data from a multiple entities
  * Create Datastream for wide style data from a single entity
  * Create Datastream for wide style data from a multiple entities
  * Create Datastream with microseconds precision
  * Retrieve Datastreams
  * Retrieve Datastream by id
  * Delete Datastream by id
  * Add EntityMeta to a Datastream
  * Get EntityMeta of a Datastream
  * Create Assessment
  * Retrieve Assessments
  * Retrieve Assessment by Id
  * Delete Assessment
  * Get Condition List Of Assessment
  * Add historical narrow input data (json format) to multi entity Datastream (Used for model revision)
  * Add historical narrow input data (csv format) single entity to Datastream (Used for model revision)
  * Add historical wide input data (json format) to single entity Datastream (Used for model revision)
  * Add historical wide input data (csv format) to multi entity Datastream (Used for model revision)
  * Add historical narrow input data (json format) to single entity Batch Datastream (Used for model revision)
  * Add historical narrow input data (csv format) multi entity to Batch Datastream (Used for model revision)
  * Add historical wide input data (json format) to multi entity Batch Datastream (Used for model revision)
  * Add historical wide input data (csv format) to single entity Batch Datastream (Used for model revision)
  * Add live input data (json format) to Datastream (Used for live monitoring) 
  * Add live input data (csv format) to Datastream (Used for live monitoring) 
  * Add live input data (json format) from a stream to Datastream (Used for live monitoring) 
  * Add live input data (csv format) from a stream to Datastream (Used for live monitoring) 
  * Add facts data (json format) to Assessment of single entity datastream
  * Add facts data (json format) to Assessment of single entity Batch datastream
  * Add facts data (json format) with additional keyword to Assessment of multi entity datastream
  * Add facts data (csv format) to Assessment of single entity datastream
  * Add facts data (csv format) with keywords to Assessment of multi entity datastream
  * Add facts data (json format) from a stream to Assessment of multi entity datastream
  * Add facts data (csv format) from a stream to Assessment of multi entity datastream
  * Get facts data
  * Get Datastream Input data
  * Get Historian Output from Assessment
  * Get Streaming Output
  * Datastream On (Start live monitoring of datastream)
  * Datastream Off (Stop live monitoring of datastream)
  * Run Backfill Process
    
## Quick Start
  * Get auth token from the Falkonry Service UI.
  * Read below examples for integration with various data formats.

## Examples    

#### Create Datastream for narrow/historian style data from a single entity
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;
    
  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
    
  var time = new Time();
  time.Zone = "GMT";
  time.Identifier = "time";
  time.Format = "iso_8601";

  var datasource = new Datasource();
  datasource.Type = "PI";
  var ds = new DatastreamRequest();
  var Field = new Field();
  var Signal = new Signal();
  Signal.ValueIdentifier = "value";
  Signal.SignalIdentifier = "signal";
  Field.Signal = Signal;
  Field.Time = time;
  ds.Field = Field;
  ds.DataSource = datasource;
  var rnd = new System.Random();
  var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
  ds.Name = "TestDS" + randomNumber;
  ds.Field.Time = time;
  ds.DataSource = datasource;
  var datastream = _falkonry.CreateDatastream(ds);
```

#### Create Batch Datastream for narrow/historian style data from a single entity
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;
    
  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
    
  var time = new Time();
  time.Zone = "GMT";
  time.Identifier = "time";
  time.Format = "iso_8601";

  var datasource = new Datasource();
  datasource.Type = "PI";
  var ds = new DatastreamRequest();
  var Field = new Field();
  var Signal = new Signal();
  Signal.ValueIdentifier = "value";
  Field.BatchIdentifier = "Batch";
  Field.Signal = Signal;
  Field.Time = time;
  ds.Field = Field;
  ds.DataSource = datasource;
  var rnd = new System.Random();
  var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
  ds.Name = "TestDS" + randomNumber;
  ds.Field.Time = time;
  ds.DataSource = datasource;
  var datastream = _falkonry.CreateDatastream(ds);
```
#### Create Datastream for narrow/historian style data from a multiple entities
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;
  
  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
    
  var time = new Time();
  time.Zone = "GMT";
  time.Identifier = "time";
  time.Format = "iso_8601";

  var datasource = new Datasource();
  datasource.Type = "PI";
  var ds = new DatastreamRequest();
  var Field = new Field();
  var Signal = new Signal();
  Signal.ValueIdentifier = "value";
  Signal.SignalIdentifier = "signal";
  Field.Signal = Signal;
  Field.Time = time;
  ds.Field = Field;
  ds.DataSource = datasource;
  var rnd = new System.Random();
  var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
  ds.Name = "TestDS" + randomNumber;
  ds.Field.Time = time;
  ds.DataSource = datasource;
  var datastream = _falkonry.CreateDatastream(ds);
```
#### Create Datastream for wide style data from a single entity
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;
  
  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
    
  var time = new Time();
  time.Zone = "GMT";
  time.Identifier = "time";
  time.Format = "iso_8601";

  var datasource = new Datasource();
  datasource.Type = "PI";
  var ds = new DatastreamRequest();
  var Field = new Field();
  var Signal = new Signal();
  
  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
    
  var time = new Time();
  time.Zone = "GMT";
  time.Identifier = "time";
  time.Format = "iso_8601";

  var datasource = new Datasource();
  datasource.Type = "PI";
  var ds = new DatastreamRequest();

  // Input List
  var inputList = new List<Input>();
  var currents = new Input();
  currents.Name = "current";
  currents.ValueType = new ValueType();
  currents.EventType = new EventType();
  currents.ValueType.Type = "Numeric";
  currents.EventType.Type = "Samples";
  inputList.Add(currents);

  var vibration = new Input();
  vibration.Name = "vibration";
  vibration.ValueType = new ValueType();
  vibration.EventType = new EventType();
  vibration.ValueType.Type = "Numeric";
  vibration.EventType.Type = "Samples";
  inputList.Add(vibration);

  var state = new Input();
  state.Name = "state";
  state.ValueType = new ValueType();
  state.EventType = new EventType();
  state.ValueType.Type = "Categorical";
  state.EventType.Type = "Samples";
  inputList.Add(state);

  ds.InputList = inputList;
  var Field = new Field();
  var Signal = new Signal();
  Field.Signal = Signal;
  Field.Time = time;
  ds.Field = Field;
  ds.DataSource = datasource;
  var rnd = new System.Random();
  var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
  ds.Name = "TestDS" + randomNumber;
  ds.Field.Time = time;
  ds.DataSource = datasource;
  var datastream = _falkonry.CreateDatastream(ds);
```
#### Create Datastream for wide style data from a multiple entities
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;
  
  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
    
  var time = new Time();
  time.Zone = "GMT";
  time.Identifier = "time";
  time.Format = "iso_8601";

  var datasource = new Datasource();
  datasource.Type = "PI";
  var ds = new DatastreamRequest();
  var Field = new Field();
  var Signal = new Signal();
  using FalkonryClient;
  using FalkonryClient.Helper.Models;
  
  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
    
  var time = new Time();
  time.Zone = "GMT";
  time.Identifier = "time";
  time.Format = "iso_8601";

  var datasource = new Datasource();
  datasource.Type = "PI";
  var ds = new DatastreamRequest();
  // Input List
  var inputList = new List<Input>();
  var currents = new Input();
  currents.Name = "current";
  currents.ValueType = new ValueType();
  currents.EventType = new EventType();
  currents.ValueType.Type = "Numeric";
  currents.EventType.Type = "Samples";
  inputList.Add(currents);

  var vibration = new Input();
  vibration.Name = "vibration";
  vibration.ValueType = new ValueType();
  vibration.EventType = new EventType();
  vibration.ValueType.Type = "Numeric";
  vibration.EventType.Type = "Samples";
  inputList.Add(vibration);

  var state = new Input();
  state.Name = "state";
  state.ValueType = new ValueType();
  state.EventType = new EventType();
  state.ValueType.Type = "Categorical";
  state.EventType.Type = "Samples";
  inputList.Add(state);

  ds.InputList = inputList;
  var Field = new Field();
  Field.EntityIdentifier = "Unit";
  var Signal = new Signal();
  Field.Signal = Signal;
  Field.Time = time;
  ds.Field = Field;
  ds.DataSource = datasource;
  var rnd = new System.Random();
  var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
  ds.Name = "TestDS" + randomNumber;
  ds.Field.Time = time;
  ds.DataSource = datasource;
  var datastream = _falkonry.CreateDatastream(ds);

```

#### Create Datastream with microseconds precision
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;
  
  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  
  var time = new Time();
  time.Zone = "GMT";
  time.Identifier = "time";
  time.Format = "iso_8601";

  var datasource = new Datasource();
  datasource.Type = "PI";
  var ds = new DatastreamRequest();
  var Field = new Field();
  var Signal = new Signal();
  Signal.ValueIdentifier = "value";
  Signal.SignalIdentifier = "signal";
  Field.Signal = Signal;
  Field.Time = time;
  ds.Field = Field;
  ds.DataSource = datasource;
  var rnd = new System.Random();
  var randomNumber = System.Convert.ToString(rnd.Next(1, 10000));
  ds.Name = "TestDS" + randomNumber;
  ds.TimePrecision = "micro"; # this is use to store your data in different date time format. If input data precision is in micorseconds then set "micro" else "millis". If not sent then it will be "millis"
  ds.Field.Time = time;
  ds.DataSource = datasource;
  var datastream = _falkonry.CreateDatastream(ds);
```

#### Retrieve Datastreams
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  List<Datastream> datastreams = _falkonry.GetDatastreams();
```

#### Retrieve Datastream by id
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  Datastream datastream = _falkonry.GetDatastream('datastreamId')

```

#### Delete Datastream by id
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  _falkonry.DeleteDatastream('datastreamId')

```

#### Add EntityMeta to a Datastream
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  
  // Create Datastream
  datastream = falkonry.getDatastream(datastream.id);

  // Create EntityMetaRequest
  List<EntityMetaRequest> entityMetaRequestList = new List<EntityMetaRequest>();
  EntityMetaRequest entityMetaRequest1 = new EntityMetaRequest();
  entityMetaRequest1.label = "User readable label";
  entityMetaRequest1.sourceId = "1234-21342134";
  entityMetaRequest1.path = "//root/branch1/";

  EntityMetaRequest entityMetaRequest2 = new EntityMetaRequest();
  entityMetaRequest2.label = "User readable label2";
  entityMetaRequest2.sourceId = "1234-213421rawef";
  entityMetaRequest2.path = "//root/branch2/";

  entityMetaRequestList.Add(entityMetaRequest1);
  entityMetaRequestList.Add(entityMetaRequest2);

  List<EntityMeta> entityMetaResponseList = falkonry.postEntityMeta(entityMetaRequestList, datastream);

```

#### Get Entity Meta of datastream  
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  // Get entitymeta
  entityMetaResponseList = falkonry.getEntityMeta('datastream-id');
```
#### To create Assessment
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  AssessmentRequest asmt = new AssessmentRequest();
  asmt.name = "assessment name here";
  asmt.datastream= "datastream id";
  Assessment assessment = falkonry.createAssessment(asmt);

```
#### Retrieve Assessments
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  List<Assessment> assessmentList = falkonry.getAssessments();
```

#### Retrieve Assessment by Id
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  Assessment assessment = falkonry.getAssessment('assessment-id');
```

#### Delete assessment by id
    
```
    using FalkonryClient;
    using FalkonryClient.Helper.Models;

    string token="Add your token here";   
    Falkonry falkonry = new Falkonry("https://localhost:8080", token);
    falkonry.DeleteAssessment('assessment-id');
```

### Get Condition List Of Assessment
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("startTimeIdentifier", "time");
  options.Add("endTimeIdentifier", "end");
  options.Add("timeFormat", "iso_8601");
  options.Add("timeZone", "GMT");
  options.Add("entityIdentifier", "entities");
  options.Add("valueIdentifier", "Health");

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  Assessment assessment = falkonry.getAssessment('assessment-id');
  String data = "{\"time\" : \"2011-03-26T12:00:00Z\", \"entities\" : \"entity1\", \"end\" : \"2012-06-01T00:00:00Z\", \"Health\" : \"Normal\"}";
  string response = falkonry.addFacts('assessment-id',data, options);
  
  // aprioriConditionList 
  String [] conditionList = assessment.AprioriConditionList
```

#### Add historical narrow input data (json format) to multi entity Datastream (Used for model revision) 
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  var data = "{\"time\" :\"2016-03-01 01:01:01\", \"signal\":\"current\",\"value\" : 12.4,\"car\" : \"car1\"}";
  var options = new SortedDictionary<string, string>();
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
  options.Add("timeIdentifier", "time");
  options.Add("timeZone", "GMT");
  options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
  options.Add("signalIdentifier", "signal");
  options.Add("valueIdentifier", "value");
  var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
```

#### Add historical narrow input data (csv format) single entity to Datastream (Used for model revision) 
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  var data = "time,signal,value\n" + "2016-05-05 12:00:00,current,12.4\n2016-03-01 01:01:01,vibration,20.4";
  var options = new SortedDictionary<string, string>();
              
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
              
  var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
               
```

#### Add historical wide input data (json format) to single entity Datastream (Used for model revision)
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  var data = "{\"time\" :\"2016-03-01 01:01:01\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\"}";
  var options = new SortedDictionary<string, string>();
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
  var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
```

#### Add historical wide input data (csv format) to multi entity Datastream (Used for model revision)
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  var data = "time,Unit,current,vibration,state\n 2016-05-05T12:00:00.000Z,Unit1,12.4,3.4,On";
  var options = new SortedDictionary<string, string>();
  options.Add("timeIdentifier", "time");
  options.Add("timeFormat", "iso_8601");
  options.Add("timeZone", "GMT");
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
  options.Add("entityIdentifier", "Unit");
  var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
            
```

#### Add historical narrow input data (json format) to single entity Batch Datastream (Used for model revision) 
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  var data = "{\"time\" :\"2016-03-01 01:01:01\", \"signal\":\"current\",\"value\" : 12.4,\"batch\" : \"batch1\"}";
  var options = new SortedDictionary<string, string>();
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
  options.Add("timeIdentifier", "time");
  options.Add("timeZone", "GMT");
  options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
  options.Add("signalIdentifier", "signal");
  options.Add("valueIdentifier", "value");
  options.Add("batchIdentifier", "batch");
  var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
```

#### Add historical narrow input data (csv format) multi entity to Batch Datastream (Used for model revision) 
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  var data = "time,signal,value,Unit,Batch\n" + "2016-05-05 12:00:00,current,12.4,uni1,batch1\n2016-03-01 01:01:01,vibration,20.4,unit2,batch2";
  var options = new SortedDictionary<string, string>();
              
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
  options.Add("timeIdentifier", "time");
  options.Add("timeZone", "GMT");
  options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
  options.Add("signalIdentifier", "signal");
  options.Add("valueIdentifier", "value");
  options.Add("entityIdentifier", "Unit");
  options.Add("batchIdentifier", "Batch");
                
  var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
               
```

#### Add historical wide input data (json format) to multi entity Datastream (Used for model revision)
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  var data = "{\"time\" :\"2016-03-01 01:01:01\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\", \"car\" : \"car1\", \"batch\" : \"batch1\"}";
  var options = new SortedDictionary<string, string>();
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
  options.Add("timeIdentifier", "time");
  options.Add("timeZone", "GMT");
  options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
  options.Add("entityIdentifier", "car");
  options.Add("batchIdentifier", "batch");
  var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
```

#### Add historical wide input data (csv format) to single entity Batch Datastream (Used for model revision)
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  var data = "time,Batch,current,vibration,state\n 2016-05-05T12:00:00.000Z,batch1,12.4,3.4,On";
  var options = new SortedDictionary<string, string>();
  options.Add("timeIdentifier", "time");
  options.Add("timeFormat", "iso_8601");
  options.Add("timeZone", "GMT");
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
  options.Add("batchIdentifier", "Batch");
  var inputstatus = _falkonry.AddInput(datastream.Id, data, options);
```

#### Add historical input data (json format) from a stream to Datastream (Used for model revision) 
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  /*This particular example will read data from a AddData.json file in debug folder in bin*/
  
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("timeIdentifier", "time");
  options.Add("timeFormat", "iso_8601");
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
  options.Add("fileFormat", "json");
    
  string folder_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

  string path = folder_path + "/AddData.json";
  //Alternatively, you can directly specify the folder path in the "folder_path" variable

  byte[] bytes = System.IO.File.ReadAllBytes(path);

  InputStatus inputstatus = falkonry.addInputStream('datastream-id', bytes, options);
```

#### Add historical input data (csv format) from a stream to Datastream (Used for model revision) 
    
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  /*This particular example will read data from a AddData.json file in debug folder in bin*/
  
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("timeIdentifier", "time");
  options.Add("timeFormat", "iso_8601");
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
  options.Add("fileFormat", "csv");
    
  string folder_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

  string path = folder_path + "/AddData.csv";
  //Alternatively, you can directly specify the folder path in the "folder_path" variable

  byte[] bytes = System.IO.File.ReadAllBytes(path);

  InputStatus inputstatus = falkonry.addInputStream('datastream-id', bytes, options);
   
```
#### Add live input data (json format) to Datastream (Used for live monitoring)  
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  string data = "{\"time\" :\"2016-03-01 01:01:01\", \"current\" : 12.4, \"vibration\" : 3.4, \"state\" : \"On\"}";
  
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("timeIdentifier", "time");
  options.Add("timeFormat", "YYYY-MM-DD HH:mm:ss");
  options.Add("fileFormat", "json");
  options.Add("streaming", "true");
  options.Add("hasMoreData", "false");
  InputStatus inputstatus = falkonry.addInput('datastream-id', data, options);
```

#### Add live input data (csv format) to Datastream (Used for live monitoring)
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  string data = "time, entities, signal1, signal2, signal3, signal4" + "\n"
        + "1467729675422, entity1, 41.11, 62.34, 77.63, 4.8" + "\n"
        + "1467729675445, entity1, 43.91, 82.64, 73.63, 3.8";
  
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("timeIdentifier", "time");
  options.Add("timeFormat", "millis");
  options.Add("fileFormat", "csv");
  options.Add("streaming", "true");
  options.Add("hasMoreData", "false");
  InputStatus inputstatus = falkonry.addInput('datastream-id', data, options);
```

#### Add live input data (json format) from a stream to Datastream (Used for live monitoring) 
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  /*This particular example will read data from a AddData.json file in debug folder in bin*/
  
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("timeIdentifier", "time");
  options.Add("timeFormat", "iso_8601");
  options.Add("streaming", "true");
  options.Add("hasMoreData", "false");
  options.Add("fileFormat", "json");
    
  string folder_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

  string path = folder_path + "/AddData.json";
  //Alternatively, you can directly specify the folder path in the "folder_path" variable

  byte[] bytes = System.IO.File.ReadAllBytes(path);

  InputStatus inputstatus = falkonry.addInputStream('datastream-id', bytes, options);
```

#### Add live input data (csv format) from a stream to Datastream (Used for live monitoring) 
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  /*This particular example will read data from a AddData.json file in debug folder in bin*/
  
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("timeIdentifier", "time");
  options.Add("timeFormat", "iso_8601");
  options.Add("streaming", "false");
  options.Add("hasMoreData", "false");
  options.Add("fileFormat", "csv");
    
  string folder_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

  string path = folder_path + "/AddData.csv";
  //Alternatively, you can directly specify the folder path in the "folder_path" variable

  byte[] bytes = System.IO.File.ReadAllBytes(path);

  InputStatus inputstatus = falkonry.addInputStream('datastream-id', bytes, options);
```

#### Add facts data (json format) to Assessment of single entity datastream  
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("startTimeIdentifier", "time");
  options.Add("endTimeIdentifier", "end");
  options.Add("timeFormat", "iso_8601");
  options.Add("timeZone", "GMT");
  options.Add("valueIdentifier", "Health");

  string token = "Add your token here";   
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  String data = "{\"time\" : \"2011-03-26T12:00:00Z\", \"end\" : \"2012-06-01T00:00:00Z\", \"Health\" : \"Normal\"}";
  string response = falkonry.addFacts('assessment-id',data, options);

```

#### Add facts data (json format) to Assessment of single entity Batch datastream  
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  SortedDictionary<string, string> options = new SortedDictionary<string, string>();

  string token = "Add your token here";   
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  String data = "{\"Health\" : \"Normal\", \"Batch\" : \"batch1\"}";
  options.Add("valueIdentifier", "Health");
  options.Add("batchIdentifier", "Batch");
  string response = falkonry.addFacts('assessment-id',data, options);
```

#### Add facts data (json format) with additional keyword to Assessment of multi entity datastream
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("startTimeIdentifier", "time");
  options.Add("endTimeIdentifier", "end");
  options.Add("timeFormat", "iso_8601");
  options.Add("timeZone", "GMT");
  options.Add("entityIdentifier", "entities");
  options.Add("valueIdentifier", "Health");
  options.Add("additionalKeyword", "testTag");

  string token = "Add your token here";   
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  String data = "{\"time\" : \"2011-03-26T12:00:00Z\", \"entities\" : \"entity1\", \"end\" : \"2012-06-01T00:00:00Z\", \"Health\" : \"Normal\"}";
  string response = falkonry.addFacts('assessment-id',data, options);

```

#### Add facts data (csv format) to Assessment of single entity datastream
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("startTimeIdentifier", "time");
  options.Add("endTimeIdentifier", "end");
  options.Add("timeFormat", "iso_8601");
  options.Add("timeZone", "GMT");
  options.Add("valueIdentifier", "Health");

  string token = "Add your token here";   
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  string data = "time,end,Health\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,Normal\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,Normal";
  string response = falkonry.addFacts('assessment-id',data, options);

```

#### Add facts data (csv format) with keywords to Assessment of multi entity datastream
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("startTimeIdentifier", "time");
  options.Add("endTimeIdentifier", "end");
  options.Add("timeFormat", "iso_8601");
  options.Add("timeZone", "GMT");
  options.Add("entityIdentifier", "car");
  options.Add("valueIdentifier", "Health");
  options.Add("keywordIdentifier", "tag");

  string token = "Add your token here";   
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  string data = "time,end,car,Health,Tag\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,IL9753,Normal\n2011-03-31T00:00:00Z,2011-04-01T00:00:00Z,HI3821,Normal,testTag1";
  string response = falkonry.addFacts('assessment-id',data, options);

```

#### Add facts data (json format) from a stream to Assessment of multi entity datastream
    
Sample JSONFile:
```
  {"time" : "2011-03-26T12:00:00.000Z", "car" : "HI3821", "end" : "2012-06-01T00:00:00.000Z", "Health" : "Normal"}
  {"time" : "2014-02-10T23:00:00.000Z", "car" : "HI3821", "end" : "2014-03-20T12:00:00.000Z", "Health" : "Spalling"}
```

```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("startTimeIdentifier", "time");
  options.Add("endTimeIdentifier", "end");
  options.Add("timeFormat", "iso_8601");
  options.Add("timeZone", "GMT");
  options.Add("entityIdentifier", "car");
  options.Add("valueIdentifier", "Health");

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  string path = "Insert the path to your file here";
  byte[] bytes = System.IO.File.ReadAllBytes(path);
  string response = falkonry.AddFactsStream('assessment-id',bytes, options);
```

#### Add facts data (csv format) from a stream to Assessment of multi entity datastream

```
    
Sample CSVFile
  time,car,end,Health
  2011-03-26T12:00:00Z,HI3821,2012-06-01T00:00:00Z,Normal
  2014-02-10T23:00:00Z,HI3821,2014-03-20T12:00:00Z,Spalling

```

```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("startTimeIdentifier", "time");
  options.Add("endTimeIdentifier", "end");
  options.Add("timeFormat", "iso_8601");
  options.Add("timeZone", "GMT");
  options.Add("entityIdentifier", "car");
  options.Add("valueIdentifier", "Health");

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  string path = "Insert the path to your file here";
  byte[] bytes = System.IO.File.ReadAllBytes(path);
  string response = falkonry.AddFactsStream('assessment-id',bytes, options);
```

#### Get facts data of Assessment
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  string assessment = "id of the assessment here";
  string response = falkonry.getFacts('assessment',options);

```

#### Get Datastream Input data
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  string datastream = "id of the datastream here";
  string response = falkonry.GetDatastreamData('datastream',options);
```

#### Get Historian Output from Assessment
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  
  // Create Datastream

  // create Assessment

  // Add Data To dataStream

  // From Falkonry UI, run a model revision.

  // Fetch Historical output data for given assessment, startTime , endtime
  SortedDictionary<string, string> options = new SortedDictionary<string, string>();
  options.Add("startTime", "2011-01-01T01:00:00.000Z"); // in the format YYYY-MM-DDTHH:mm:ss.SSSZ
  options.Add("endTime", "2011-06-01T01:00:00.000Z");  // in the format YYYY-MM-DDTHH:mm:ss.SSSZ
  options.Add("responseFormat", "application/json");  // also available options 1. text/csv 2. application/json

  HttpResponse httpResponse = falkonry.getHistoricalOutput(assessment, options);
  // If data is not readily available then, a tracker id will be sent with 202 status code. While falkonry will genrate ouptut data
  // Client should do timely pooling on the using same method, sending tracker id in the query params
  // Once data is available server will response with 200 status code and data in json/csv format.

  if (httpResponse.statusCode == 202)
  {
    Tracker trackerResponse = javascript.Deserialize<Tracker>(httpResponse.response);
    // get id from the tracker
    string Id = trackerResponse.Id;

    // use this tracker for checking the status of the process.
    options = new SortedDictionary<string, string>();
    options.Add("trackerId", Id);
    options.Add("responseFormat", "application/json");

    httpResponse = falkonry.getHistoricalOutput(assessment, options);

    // if status is 202 call the same request again

    // if statsu is 200, output data will be present in httpResponse.response field
  }
  if (httpResponse.statusCode > 400)
  {
    // Some Error has occured. Please httpResponse.response for detail message
  }
```

#### Get Streaming Output
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  string assessment_id ="assessment ID here";

  //On successfull live streaming output EventSource_Message will be triggered
  private void EventSource_Message(object sender, EventSource.ServerSentEventArgs e)
  {
    try { var falkonryEvent = JsonConvert.DeserializeObject<FalkonryEvent>(e.Data); }
    catch(System.Exception exception) 
    { //log error in case of error parsing the output event }
  }

  //On any error while getting live streaming output, EventSource_Error will be triggered
  private void EventSource_Error(object sender, EventSource.ServerSentErrorEventArgs e)
  { // Error handling }

  EventSource eventSource = falkonry.GetOutput(assessment,null,null);
  eventSource.Message += EventSource_Message;
  eventSource.Error += EventSource_Error;

  // NOTE: To stop listening to output
  eventSource.Dispose();

```

#### Datastream On (Start live monitoring of datastream)
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  string datastream_id = "Your datastream id";
  falkonry.onDatastream(datastream_id);
```

#### Datastream Off (Stop live monitoring of datastream)
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);
  string datastream_id = "Your datastream id";
  falkonry.offDatastream(datastream_id);
```

#### Run Backfill Process
```
  using FalkonryClient;
  using FalkonryClient.Helper.Models;

  string token="Add your token here";   
  Falkonry falkonry = new Falkonry("https://localhost:8080", token);

  string datastream_id ="datastream ID here";
  string assessment_id ="assessment ID here";

   EventSource eventSource;

    //Handles live streaming output
    private void EventSource_Message(object sender, EventSource.ServerSentEventArgs e)
    {
      try
      { var falkonryEvent = JsonConvert.DeserializeObject<FalkonryEvent>(e.Data); }
      catch (System.Exception exception)
      {
        // exception in parsing the event
      }
    }

    //Handles any error while fetching the live streaming output
    private void EventSource_Error(object sender, EventSource.ServerSentErrorEventArgs e)
    {
      // error connecting to Falkonry service for output streaming
    }

  OutputStateRequest outputStateRequest = new OutputStateRequest();
  outputStateRequest.Datastream = datastream_id;
  List<string> assessmentList = new List<string>();
  assessmentList.Add(assessment_id);
  outputStateRequest.Assessment = assessmentList;
  OutputStateResponse outputStateResponse = _falkonry.StartBackfillProcess(outputStateRequest);

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
  eventSource.Dispose();

  //System.Threading.Thread.Sleep(20000);
  // stop the backfill process
  _falkonry.StopBackfillProcess(outputStateResponse.OutputStateId);


```
## Docs

    [Falkonry APIs](https://service.falkonry.io/api)
     

## License

  Available under [MIT License](LICENSE)
