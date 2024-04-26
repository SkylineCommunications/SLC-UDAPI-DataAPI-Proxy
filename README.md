# UDAPI-DataAPI-Forwarding

Ever heard about [DataMiner Data API](https://docs.dataminer.services/user-guide/Advanced_Modules/Data_Sources/Data_API.html)?
This is clever module allows the automatic generation of elements based on auto-generated connectors, all by simply pushing JSON formatted data towards this API.

Currently this feature is only available from within the local instance.  **That's why this script solution can come in handy!**

This automationscript solution acts as a PROXY that consumes the [User Defined APIs](https://docs.dataminer.services/user-guide/Advanced_Modules/User-Defined_APIs/UD_APIs.html) to pass data over to the local Data-API.
!! this means that you can already leverage the data API feature from outside in a secure manner. 
Simply by leveraging this proxy automationscript configured as User Defined API in a secure way. (security: see [User Defined API tokens](https://docs.dataminer.services/user-guide/Advanced_Modules/User-Defined_APIs/UD_APIs_Viewing_in_Cube.html))


Follow along, and see how also you can set up this proxy yourself and learn how to use the Data API from outside. 🏆

## Goal: Data API result
Below you can already see how the end result will look like visualised as an element in DataMiner. 🔎

The data we are using in this example (sent via Postman) is as follows:

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

Feel free to use and test out for yourself!  🤖🤖

![Data API result](/Documentation/1_5_DataAPI%20Test%20Element%201.png "Data API Element result")

## Let's get started!
Deploy this automationscript onto your (cloud enabled) DataMiner system. 

## Configure User Defined API
By configuring the User Defined API on top of this script, you can expose this as an external API interface. 
The endpoint is custom to configure.  In this example I used a similar endpoint as the  Data API uses.

```
api/data/parameters
```

You are free to configure this as you like, as long as you of course also use your configured endpoint inside your client when you push data over.

Method of execution can be configured to use **Raw body**. This will allow the content to be picked up by this PROXY script.
in order to make this User Defined API secured, please go ahead and configure an existing (or new) API token.
This token will also be needed to grant access via the use of a **Bearer Token**.

![Configure User Defined API](/Documentation/2_configure_API.png "Configure User Defined API")


> [!NOTE]
> **That's it... you're all set!** 💡🟢
> Let's now look into how you can leverage this Data API proxy with performing a simple test!

## Add URL encoded parameters

![Add URL encoded parameters](/Documentation/3_URL_encoded_parameters.png "Add URL encoded parameters")

## Send DataAPI PUT command
![Send DataAPI PUT command](/Documentation/4_Send_DataAPI_PUT_command.png "Send DataAPI PUT command")

## Goal: See the Data API result as an element
![Data API result](/Documentation/1_5_DataAPI%20Test%20Element%201.png "Data API Element result")

## Automatic protocol and element
Here you can start configuring alarm thresholds
![Automatic protocol and element](/Documentation/6_Automatic%20protocol%20and%20element.png "Automatic protocol and element")
