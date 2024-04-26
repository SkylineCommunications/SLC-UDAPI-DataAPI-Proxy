# UDAPI-DataAPI-Forwarding


Ever heard about [DataMiner Data API](https://docs.dataminer.services/user-guide/Advanced_Modules/Data_Sources/Data_API.html)?
This is clever module allows the automatic generation of elements based on auto-generated connectors, all by simply pushing JSON formatted data towards this API.

Currently this feature is only available from within the local instance.  **That's why this script solution can come in handy!**

This automationscript solution will leverage the [User Defined APIs](https://docs.dataminer.services/user-guide/Advanced_Modules/User-Defined_APIs/UD_APIs.html) to pass data over to the local Data-API.
!! that means that you can already leverage the data API feature from outside, by leveraging this script in a secure way (secured by the [User Defined API tokens](https://docs.dataminer.services/user-guide/Advanced_Modules/User-Defined_APIs/UD_APIs_Viewing_in_Cube.html))


Follow along, and see how also you can set this up yourself. 🏆

## Step 1
Deploy this script onto your (cloud enabled) DataMiner system.

## Goal: Data API result
Here you can see already how the end result will look like.
The data we use in this example (sent via Postman) looks like the following:

```
{
  "Customer.Name": "Ziine",
  "Customer.Contact": "[EXTERNAL]Thijs V.",
  "KPI.1" : "99%",
  "KPI.2": 10024,
  "KPI.3": "LIVE",
  "KPI.4": "STABLE"
}
```

The result is visualised as an element in DataMiner.  🤖🤖

![Data API result](/Documentation/1_5_DataAPI%20Test%20Element%201.png "Data API Element result")

## Configure User Defined API
![Configure User Defined API](/Documentation/2_configure_API.png "Configure User Defined API")

## Add URL encoded parameters
![Add URL encoded parameters](/Documentation/3_URL_encoded_parameters.png "Add URL encoded parameters")

## Send DataAPI PUT command
![Send DataAPI PUT command](/Documentation/4_Send_DataAPI_PUT_command.png "Send DataAPI PUT command")

## Goal: See the Data API result as an element
![Data API result](/Documentation/1_5_DataAPI%20Test%20Element%201.png "Data API Element result")

## Automatic protocol and element
Here you can start configuring alarm thresholds
![Automatic protocol and element](/Documentation/6_Automatic%20protocol%20and%20element.png "Automatic protocol and element")
