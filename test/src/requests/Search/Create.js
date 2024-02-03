postman[Symbol.for("define")]({
  name: "Create",
  id: "760bd393-f3aa-497d-951a-3d5572437798",
  method: "POST",
  address: "https://localhost:7498/Person/create",
  data:
    ' [\r\n {\r\n    "id": "3",\r\n    "firstName": "{{$randomFirstName}}",\r\n    "lastName": "{{$randomLastName}}",\r\n    "email": "{{$randomEmail}}",\r\n    "emailText": "email user mail com",\r\n    "country": "{{$randomCountry}}",\r\n    "dob": "2000-01-01",\r\n    "bio": "{{$randomCatchPhrase}}. Android"\r\n}\r\n]'
});
