## FastDev.WebApi

Se utiliza da estrutura dos pacotes FastDev.Service, FastDev.Notifications e FastDev.Infra.Data para criar controllers no padrão REST com todos os metodos http básicos (Get, Post, Put, Delete e Patch), filtrar dados de retorno alem tratar retornos em forma de notificações facilitando o trabalho do desenvolvimento front-end.

## Padrão de rotas e retornos

Tomaremos como exemplo a seguinte model:

````csharp
public class User : DataModel<Guid>
{
    public string Email { get; set; }
    public string Name { get; set; }
}
````

### GET

##### request
````
baseurl/users
````

##### responses

````json
Http status code 204 - No Content
````

````json
Http status code 200 - Success
[
    {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "email": "someemail",
        "name": "somename"
    }
]
````

### GET with fields

##### request
````
baseurl/users?getFields=Id,Email
````
##### responses
````json
Http status code 204 - No Content
````

````json
Http status code 200 - Success
[
    {
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
        "email": "someemail"
    }
]
````


### GET/{id}

##### request
````
baseurl/users
````
##### responses
````json
Http status code 204 - No Content
````

````json
Http status code 200 - Success
{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "someemail",
    "name": "somename"
}
````

### GET/{id} with fields

##### request
````
baseurl/users?getFields=Id,Email
````
##### responses
````json
Http status code 204 - No Content
````

````json
Http status code 200 - Success
{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "someemail"
}
````

### POST

##### Request
````
baseurl/users
````
body
````json
{
    "email": "someemail",
    "name": "somename"
}
````

##### responses

````json
Http status code 400 - BadRequest
````
````json
Http status code 400 - BadRequest
[
    {
        "property": "string",
        "message": "string",
        "level": 0
    }
]
````

````json
Http status code 201 - Created
{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "someemail",
    "name": "somename"
}
````

### POST with fields

##### Request
````
baseurl/users?getFields=Id
````
body
````json
{
    "email": "someemail",
    "name": "somename"
}
````

##### responses

````json
Http status code 400 - BadRequest
````
````json
Http status code 400 - BadRequest
[
    {
        "property": "string",
        "message": "string",
        "level": 0
    }
]
````

````json
Http status code 201 - Created
{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
````

### PUT

##### Request
````
baseurl/users/{id}
````
body
````json
{
    "email": "someemail",
    "name": "somename"
}
````

##### responses

````json
Http status code 404 - NotFound
{
    "property": null,
    "message": "user not found",
    "level": 2
}
````

````json
Http status code 400 - BadRequest
````
````json
Http status code 400 - BadRequest
[
    {
        "property": "string",
        "message": "string",
        "level": 1
    }
]
````

````json
Http status code 200 - Success
{
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "email": "someemail",
    "name": "somename"
}
````



### DELETE

##### Request
````
baseurl/users/{id}
````


##### responses


````json
Http status code 404 - NotFound
{
    "property": null,
    "message": "user not found",
    "level": 2
}
````

````json
Http status code 400 - BadRequest
````
````json
Http status code 400 - BadRequest
[
    {
        "property": "string",
        "message": "string",
        "level": 1
    }
]
````

````json
Http status code 200 - Success
````




### PATCH

##### Request
````
baseurl/users/{id}
````
body
````json
{
    email: "some-new-email"
}
````

##### responses


````json
Http status code 404 - NotFound
{
    "property": null,
    "message": "user not found",
    "level": 2
}
````

````json
Http status code 400 - BadRequest
````
````json
Http status code 400 - BadRequest
[
    {
        "property": "PropX",
        "message": "PropX not existis",
        "level": 0
    }
]
````

````json
Http status code 200 - Success
````
