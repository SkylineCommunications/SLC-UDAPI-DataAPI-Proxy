# UDAPI-DataAPI-Proxy

## A Proxy for external interaction with Data API

This document explains how to use a proxy script to leverage Data API features from outside the local DataMiner instance.

### Prerequisites

- DataMiner 10.4.2 or higher
- Cloud connected agent with this User Defined API deployed to

### Overview

Currently Data API can only be used within the local DataMiner instance. This proxy script allows external access by consuming the User Defined APIs to pass data to the local Data API.

### Important Notes

This User Defined API is a proof of concept for securely exposing Data API externally.
Use with caution, as each data push triggers a script run on the proxy.

### Example Data

The following JSON data is used in this example:

```json
{
  "Server Name": "WebServer001",
  "CPU Utilization": 78.5
}
```

### Steps to Get Started

1. **Deploy the User Defined API**: Deploy from the catalog the script onto your (cloud-enabled) DataMiner system. For full details see: [User-Defined APIs - Managing APIs and tokens in DataMiner Cube | DataMiner Docs](https://docs.dataminer.services/user-guide/Advanced_Modules/User-Defined_APIs/UD_APIs_Viewing_in_Cube.html)
1. **Configure User Defined API**:
   - Configure the User Defined API to expose Data API as an external API interface with endpoints: `data/parameters` and `data/config`
   - Use **Raw body** for method execution.
   - Secure the API with an API token.
  
   ![Data API User Defined API](/Documentation/1_UD_API.png)
1. **Add URL Encoded Parameters**:
   - Use a client like Postman to send commands.
   - Configure URL encoded parameters `identifier` and `type`.
1. **Send DataAPI PUT Command**:
   - Set the request verb to **PUT**.
   - Point the URL to your configured User Defined API endpoint.
   - Add a **Body** with the **JSON** data.

### Result

After sending the data, an element will appear on your system, reflecting the parameter updates.

![Result](/Documentation/2_Result.png)

The element will be associated with an auto-generated *connector* which will be present in the **Protocols and Templates** module. In there you can configure alarm thresholds and trending to manage the received data.
