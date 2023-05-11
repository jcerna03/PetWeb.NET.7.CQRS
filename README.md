# PetWeb.NET.7.CQRS
- RESTful API with implementation of read and write operations to a local sql database applying CQRS concepts with MediatR and Dapper

## Clean Architecture - Hexagonal Architecture 
## NET 7 C# 11 - CQRS Pattern with MediatR 12 and Dapper 2 

### Read operations with Pagination:

#### `GET /api/pet?Page=1&Size=10`

Expected Result: 
```json
{
    "currentPage": 1,
    "totalResults": 7,
    "size": 10,
    "results": [
        {
            "petId": 1,
            "name": "Mascota 1",
            "type": "1"
        },
        {
            "petId": 2,
            "name": "Mascota 2",
            "type": "1"
        },
        {
            "petId": 3,
            "name": "Mascota 3",
            "type": "1"
        },
        {
            "petId": 4,
            "name": "Mascota 4",
            "type": "1"
        },
        {
            "petId": 5,
            "name": "Mascota 5",
            "type": "1"
        }
    ]
}
```

### Write operations 

#### `POST /api/pet`

Body parameters:
```json
{
    "name": "Mascota 6",
    "type": "1"
}
```

Expected Result: 
```json
{
    "petId": 6
}
```

#### `PUT /api/pet/6`

Body parameters:
```json
{
    "name": "Mascota 6",
    "type": "2"
}
```
Expected Result: 
```json
{
    "success": true
}
```

#### `DELETE /api/pet/6`

Expected Result: 
```json
{
    "success": true
}
```

### You can find the SQL scripts in the solution root folder, run these scripts in SQL Management Studio to create the database, tables, and stored procedures to be used by the application.

File: dbScripts.sql

### You can find the connection string in the Secret Manager json file

```json
{
  "Database:CommandTimeOut": 30,
  "Database:ConnectionString": "Server=DESKTOP\\SQLEXPRESS;Initial Catalog=dbPetWeb;Trusted_Connection=True;Connection Timeout=30;"
}
```