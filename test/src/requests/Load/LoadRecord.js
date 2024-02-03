postman[Symbol.for("define")]({
  name: "LoadRecord",
  id: "477fc005-811e-4c84-a50f-19f3555a4ac8",
  method: "POST",
  address: "https://localhost:7298/persons",
  data:
    '{\r\n  "firstName": "{{$randomFirstName}}",\r\n  "lastName": "{{$randomLastName}}",\r\n  "email": "{{$randomEmail}}",\r\n  "country": "{{$randomCountry}}",\r\n  "dob": "2001-01-01",\r\n  "bio": "{{$randomCatchPhrase}}",\r\n  "profileUrl": "{{$randomImageUrl}}"\r\n}',
  headers: {
    "Content-Type": "application/json"
  },
  post(response) {
    pm.test(`[${pm.info.requestName}] Status code is 200`, function() {
      pm.response.to.have.status(200);
    });

    // pm.test(`[${pm.info.requestName}] Log response time`, function() {
    //   let needLog =
    //     pm.variables.get("logResponseTime").toLowerCase() === "true";
    //   if (needLog) {
    //     console.log(
    //       pm.response.responseTime,
    //       `\t response from [${pm.info.requestName}]`
    //     );
    //   }
    // });

    // pm.test(`[${pm.info.requestName}] Log failed status`, function() {
    //   let needLog =
    //     pm.variables.get("logFailedRequest").toLowerCase() === "true";
    //   if (!needLog) return;

    //   const s = pm.response.code;

    //   if (s != 200) {
    //     let logObj = {
    //       name: pm.info.requestName,
    //       responseCode: pm.response.code
    //     };

    //     try {
    //       logObj["url"] = pm.request.url.toString();
    //     } catch (e) {
    //       logObj["url"] = response.request.url;
    //     }
    //     try {
    //       logObj["requestHeaders"] = pm.request.getHeaders();
    //     } catch (e) {
    //       logObj["requestHeaders"] = response.request.headers;
    //     }

    //     try {
    //       if (pm.request.body.raw) {
    //         logObj["requestBody"] = pm.request.body.raw;
    //       }
    //     } catch (e) {
    //       if (response.request.body) {
    //         logObj["requestBody"] = response.request.body;
    //       }
    //     }

    //     try {
    //       const resBody = pm.response.body.raw;
    //       logObj["responseBody"] = resBody;
    //     } catch (e) {
    //       logObj["responseBody"] = "No response body";
    //     }

    //     console.log("Failed status check: ", JSON.stringify(logObj));
    //   }
    // });

    // pm.test(`[${pm.info.requestName}] Get data`, function() {
    //   var jsonData = pm.response.json();
    //   pm.collectionVariables.set("id", jsonData.id);
    // });
  }
});
