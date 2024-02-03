postman[Symbol.for("define")]({
  name: "Create",
  id: "66d5dbc5-436a-4b90-bc7b-fe299f27c8b2",
  method: "POST",
  address: "https://localhost:7298/persons",
  data:
    '{\r\n  "firstName": "{{$randomFirstName}}",\r\n  "lastName": "{{$randomLastName}}",\r\n  "email": "{{$randomEmail}}",\r\n  "country": "{{$randomCountry}}",\r\n  "dob": "2001-01-01",\r\n  "bio": "{{$randomCatchPhrase}}",\r\n  "profileUrl": "{{$randomImageUrl}}"\r\n}'
});
