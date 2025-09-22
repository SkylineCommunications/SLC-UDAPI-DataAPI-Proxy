# User-Defined API DataAPI Proxy

## About

This package contains a [user-defined API](https://aka.dataminer.services/UDA) that functions as a secure reverse proxy to enable external interaction with Data API.

The [DataMiner Data API](https://aka.dataminer.services/data_api), which is currently still in [soft-launch](https://community.dataminer.services/scripted-connectors-are-here/), offers flexible access to data from any source imaginable, across hardware, software, and cloud services. This data is directly reflected in the element, and can be easily updated using the same API, eliminating the need to develop a connector.

Currently, the Data API is limited to only accept local requests, most commonly through [scripted connectors](https://aka.dataminer.services/scripted_connectors).

## Setting up the proxy script

### Deploying the user-defined API

Deploy the script onto your cloud-enabled DataMiner System.

See also: [Managing APIs and tokens in DataMiner Cube](https://aka.dataminer.services/managing_apis_and_tokens_in_cube)

### Configuring the user-defined API

1. Configure the user-defined API to expose the Data API as an external API interface with the endpoints `data/parameters` and `data/config`.

1. Set *Method to be executed* to "**Raw body**".

   ![Data API User Defined API Configure](./images/2_UD_API.png)

1. Secure the API with an API token.

   ![Data API User Defined API Overview](./images/1_UD_API.png)

### Using the Data API via the proxy

In this example, the endpoint `/api/custom/data/parameters` will be used with the HTTP verb **PUT** to send data to DataMiner and automatically create an element.

You can use any HTTP client to send data to the Data API via the proxy (e.g. [Postman](https://www.postman.com/)).

1. **Add URL-encoded parameters.**

   When pushing data to DataMiner, you need to provide two URL parameters: `identifier` and `type`.

   - `identifier`: A unique identifier (e.g. `DataAPI Test Element 1`). This will be used as the name of the new element.
   - `type`: The type of the auto-generated connector (e.g. `Skyline DataAPI Test Protocol`).

   > [!TIP]
   > Each element requires a unique `identifier`, but elements can share a `type`. Elements that share a `type` will share the same parameters and layout, but can still have distinct data.

   > [!IMPORTANT]
   > When interacting with the Data API directly, `identifier` and `type` should be provided as HTTP headers, not as URL parameters.

1. **Configure JSON in the body.**

   In this example, use the following JSON code:

   ```json
   {
     "Server Name": "WebServer001",
     "CPU Utilization": 78.5
   }
   ```

1. **Configure the bearer token**

   In your HTTP client, configure the **bearer token**. In Postman, this token is located on the **Authorization tab**.

   See also [Configuring the user-defined API](#configuring-the-user-defined-api) above.

1. **Send the request**  

   Send the PUT request. The result will indicate whether the operation was successful (*200 OK*) or not. In the latter case, feedback will be provided.

### Result

After executing the PUT request, a new element should appear in your DataMiner System. Also, every update you send afterward will instantly be reflected in your new element.

The element will be associated with an automatically generated *connector*, which will be present in the **Protocols and Templates** module. There, you can configure alarm thresholds and trending to manage the received data.

![Result](./images/2_Result.png)

## Prerequisites

- DataMiner 10.4.2 or higher
- A cloud-connected DataMiner Agent on which this user-defined API is deployed.

## Documentation

For more information, see:

- [Data API](https://aka.dataminer.services/data_api)
- [Scripted connectors](https://aka.dataminer.services/scripted_connectors)
- [User-Defined APIs](https://aka.dataminer.services/UDA)
