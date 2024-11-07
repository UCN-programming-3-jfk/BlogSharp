# BlogSharp 

Mini project fordisplaying a cohesive solution using
 - Web Front-end (using ASP.NET MVC)
 - RestClient (using RestSharp)
 - RESTful service API (using ASP.NET Web API)
 - Data Access Layer (using Dapper)
 - Database - using SQL Server

![image](https://github.com/user-attachments/assets/95c96bd0-677f-43b5-9ab2-287a5f0b9f92)

### Note on the REST API
There is no benefit to adding a REST API in this solution. It is done, to showcase how to add this extra tier.

## Functionalty - an ASP.NET MVC web page
- front end with possibility of
   - viewing 10 latest blog posts
   - viewing all blog posts from a single author
   - creating a user
   - logging in/out - using cookie based authentication
       - see [Cookiebased authentication, w. GitHub link in footer](https://cookieauthentication.codesamples.dk/) for sample code
   - creating blog posts

## REST API client
- for communication with the REST API

## REST API
- AuthorsController
- BlogPostsController
- AccountController

## Data Access Layer and database
- Data access layer (DAL) project with
    - Author and BlogPost tables in MS SQL server
    - Dapper for ORM [Tutorial](https://dappertutorial.net/)
    - BCrypt for safe password storage
    - SQL scripts for creating database and sample data
