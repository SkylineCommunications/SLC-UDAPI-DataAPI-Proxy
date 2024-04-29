# UDAPI-DataAPI-Forwarding

## 🥁🥁 A PROXY to leverage Data API features from outside 🥁🥁

Ever heard about [DataMiner Data API](https://docs.dataminer.services/user-guide/Advanced_Modules/Data_Sources/Data_API.html) ? 
This clever DataMiner module allows the automatic generation of elements and auto-generated connectors, simply by pushing JSON formatted data towards this data API.

Also have a read here: [Scripted Connectors are here!](https://community.dataminer.services/scripted-connectors-are-here/)
This will guide you on how to enable the soft-launch option to get started with this feature.
look for _**"A quick guide to activating the DataAPI soft-launch option"**_


Currently this feature is only available from within the local DataMiner instance.  **That's why this PROXY script can come in handy!**

This automation script acts as a PROXY that consumes the [User Defined APIs](https://docs.dataminer.services/user-guide/Advanced_Modules/User-Defined_APIs/UD_APIs.html) to pass data over to the local Data-API.
!! this means that you can already leverage the data API feature from outside, simply by leveraging this proxy, configured as User Defined API in a secure way. (security: see [User Defined API tokens](https://docs.dataminer.services/user-guide/Advanced_Modules/User-Defined_APIs/UD_APIs_Viewing_in_Cube.html))

> [!WARNING]
> This proxy was made as a proof of concept to expose the Data API externally in a secure manner via the User Defined APIs
> Use it with caution, as every data push will result in a script-run on this proxy to pass data to the Data API.

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

Any Key-value pair you supply here, will end up as parameter KPIs with respective data in DataMiner.
if you want to use more advanced features, please check the docs: [DataMiner Data API](https://docs.dataminer.services/user-guide/Advanced_Modules/Data_Sources/Data_API.html)
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
To demonstrate this PROXY script, let's get started and sent some commands via a client.
In this example I used [Postman](https://www.postman.com/) to test around.

The [DataMiner Data API](https://docs.dataminer.services/user-guide/Advanced_Modules/Data_Sources/Data_API.html) requires two important fields:
- The **identifier**, stored as the General Parameter "Data API Identifier", must be unique within the DMS cluster. The identifier serves as the initial name of the element, which can be renamed later at any time as the Data API uses the Data API Identifier.
- The **type** denotes the name of the auto-generated connector.

Since the PROXY script needs to pass over this information, you can simply configure the URL encoded parameters **indentifier** and **type** with your appropriate values.

```
identifier: DataAPI Test Element 1
type: Skyline DataAPI Test Protocol
```

As mentioned before: on the **authorization tab**, you can now fill in the **bearer token** with the **User Defined API Token** that you configured.

![Add URL encoded parameters](/Documentation/3_URL_encoded_parameters.png "Add URL encoded parameters")

## Send DataAPI PUT command
All that is remaining is:
- Configure the **request verb** to use "**PUT**"
- Make sure your URL points to your DataMiner User Defined API endpoint as you configured it
- Add a **Body** of type **RAW** --> **JSON**
   
And simply fire away!  ✈️📤

The result is expected to show if it was successful (_200 OK_, or in case it failed, you will also see feedback on why it failed.)

![Send DataAPI PUT command](/Documentation/4_Send_DataAPI_PUT_command.png "Send DataAPI PUT command")

## Goal: See the Data API result as an element
After trying this out, you will notice an element appeared on your system, every update you now push, will reflect the parameter updates instantly!

> [!CAUTION]
> note that everytime you query this PROXY, a script-run will occur on your DataMiner to process and pass over this data to the Data API.
> Keep this in mind to only use this under specific cases to avoid impact on your system.
> In the future there might be updates on DataMiner that allow Data API to be used directly with security in order to avoid this PROXY workaround.

![Data API result](/Documentation/1_5_DataAPI%20Test%20Element%201.png "Data API Element result")

## Automatic protocol and element
Via the **Protocols and Templates** module you will find this newly automatic created connector to be present on your DataMiner.
You can now start configuring alarm thresholds to further refine how you want to operate this newly received data! 💡

![Automatic protocol and element](/Documentation/6_Automatic%20protocol%20and%20element.png "Automatic protocol and element")
