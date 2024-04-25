thunder-streaming-space
=======================

Project that shows how a movie website works through an API integration with TMDb in a simple system create in .NET/C#, using only GET response as a way of simulate simple queries to this type of website.

----

#### appsettings.json
Important file to define all the parameters that will be used in this integration.

- **pgsqlConnStr**: Connection string of postgreSQL database where the parameter table that will be used is.
- **TMDb**: Website that contains the API integration and other informations about this one.
- **api_key/access_token/token_type**: Security information available only accessing the API integration's website.
```json
{
  "ConnectionStrings": {
    "pgsqlConnStr": "Host=localhost;port=5432;Username=postgres;Password=s$cr3t;Database=dbtest"
  },
  "TMDb": "https://api.themoviedb.org",
  "api_key": "",
  "token": {
    "access_token": "",
    "token_type": ""
  }
}
```
Here, there is three ways to define the parameters in this system:

- *Filling all this three parameters in appsetings.json (api_key/access_token/token_type)*
- *Using the table api_parameters inner the PostgreSQL through the Entity Datatable*
- *Or using the database through this table*
```sql
create table api_parameters (
	id serial primary key,
	api_key varchar(100) null,
	access_token varchar (8000) null,
	token_type varchar (30)
)
```

### Other informations/Links

- [**MovieDb API**](https://publicapis.io/movie-db-api) - Short details about the API used for this project.
- [**TMDb Website**](https://www.themoviedb.org/) - Database opensource about movies and series (local where the API key and API token is *[https://%tmdb%/settings/api]*).
- [**TMDb developer doc.**](https://developer.themoviedb.org/reference/intro/getting-started) - API documentation.
----
### Contributing

We welcome contributions to enhance the content and breadth of this repository. To contribute:

1. Fork the repository.
2. Create a new branch for your feature or bug fix.
3. Make your changes and commit them.
4. Push your changes to your fork.
5. Create a pull request to the `main` branch of the original repository.).

### Feedback

Your feedback is essential! If you have suggestions, find bugs, or want to request specific content, please [open an issue](https://github.com/tioncko/thunder-streaming-space/issues).

Happy coding!