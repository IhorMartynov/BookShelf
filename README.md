# Library’s book inventory

## Task
Develop a simple web application for managing a library's book inventory targeting the .NET Framework 4.8. The application should allow users to view, add, edit, and delete books.

## Requirements
1. Create a database schema using Entity Framework Code First approach with the following entities:
1.1. Book: Contains fields for Title, Author, ISBN, Publication Year, and Quantity.
1.2. Category: Contains fields for Category Name and Description.
1.3. The Book entity should have a foreign key relationship with the Category entity.

2. Implement a data access layer using Entity Framework Code First to perform CRUD (Create, Read, Update, Delete) operations on the Book and Category entities. To get all books, create a stored procedure supporting pagination, full-text search, and sorting (asc, desc) by any existing column.

3. Create an ASP.NET WebForms application with the following pages:
3.1. Home Page: Display a list of books with their details (Title, Author, ISBN, Publication Year, Quantity) in a tabular format. NOTE: There is no need to implement pagination here, just display all the books.
3.2. Add Book Page: Form to add a new book to the database. Include fields for Title, Author, ISBN, Publication Year, Quantity, and a dropdown to select the book's category.
3.3. Edit Book Page: Form to edit an existing book's details.
3.4. Delete Book Page: Confirmation page to delete a book from the database.

4. Implement an ASP.NET Web API with the following endpoints:
4.1. GET api/books: Returns a JSON response containing a list of all books. NOTE: use pagination, full-text search, and sorting.
4.2. GET api/books/{id}: Returns a JSON response containing the details of a specific book.
4.3. POST api/books: Accepts JSON data to create a new book.
4.4. PUT api/books/{id}: Accepts JSON data to update the details of a specific book.
4.5. DELETE api/books/{id}: Deletes a specific book.

5. Write unit tests using MSTest to test the functionality of the data access layer, API endpoints, and any other critical components.

6. Use MS SQL Server as the database.

## Run solution

The solution can be run from Visual Studio 2022 by selecting "Multiple Startup Projects".
Please note that it uses Docker to run WebApi project and an instance of the MS SQL Server, so make sure you have installed [Docker Desktop](https://www.docker.com/ "Docker Desktop").
