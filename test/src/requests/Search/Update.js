postman[Symbol.for("define")]({
  name: "Update",
  id: "783ff6ab-35a6-4d24-b852-795b945e14fe",
  method: "PUT",
  address: "https://localhost:7498/Person/update?id=104",
  data:
    ' {\r\n    "id": "104",\r\n    "firstName": "UPDATED {{$randomFirstName}}",\r\n    "lastName": "{{$randomLastName}}",\r\n    "email": "{{$randomEmail}}",\r\n    "emailText": "email user gmail com",\r\n    "country": "{{$randomCountry}}",\r\n    "dob": "2000-01-01",\r\n    "bio": "{{$randomCatchPhrase}}"\r\n}'
});
