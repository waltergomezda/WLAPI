# Api Demo
## A dotnet core based API
Simple Api Demo to query Configurations from a Database
Project Structure:
- Api
- Application Service
- Domain
- Infrastructure
- Unit Tests

## Framework and Libraries

- DotNet Core 3.1
- Swashbuckle.AspNetCore
- XUnit
- NSubstitute
- Dapper
- AutoMapper

## Endpoints

The solution includes the following Endpoints.

| Verb | URL | Description |
| ------ | ------ | ------ |
| GET | [/api/greetings][PlDb] |Get a Hello World Message.
| GET | [/api/config][PlGh] |Get a list of registered configs.
| GET | [/api/config/{systemCode}/{key}][PlGd] |Gets a config item filter by key and systemCode.


## Database script

Recreate the database table using the following script.

```sh
CREATE TABLE [dbo].[ConfigData](
	[key] [varchar](100) NOT NULL,
	[value] [varchar](100) NOT NULL,
	[systemcode] [varchar](100) NOT NULL,
 CONSTRAINT [PK_ConfigData] PRIMARY KEY CLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

