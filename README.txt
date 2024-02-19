CDNCRUD Project Documentation
Project Overview
CDNCRUD is a CRUD (Create, Read, Update, and Delete) application developed by CDN - Complete Developer Network. It provides a RESTful API for managing Freelancer registrations, updates, deletions, and retrievals, along with a simple interface for performing these operations. The project aims to build a comprehensive directory of freelancers, allowing users to easily find and contact individuals for their job requirements.
Technologies Used
* Programming Language: C#
* Web Framework: ASP.NET Core
* Database: SQL Server
* ORM: Entity Framework Core
Folder Structure
CDNCRUD
-Controllers/
      -UserController.cs
-Models/
	-User.cs
-Views/
      -Shared/
	-_Layout.cshtml
      -Users/
	-Create.cshtml
	-Delete.cshtml
	-Details.cshtml
	Index.cshtml
-appsettings.json
-Program.cs
-Startup.cs



Database Schema
The project uses the following database schema:
Users Table
Column NameData TypeDescriptionIDINTPrimary KeyUsernameVARCHARUser's usernameMailVARCHARUser's emailPhoneNumberINTUser's phone numberSkillsetsVARCHARUser's skill setsHobbyVARCHARUser's hobbyAPI Documentation
Endpoints
* GET /users: Retrieve all users
* POST /users: Register a new user
* PUT /users: Update a user 
* DELETE /users: Delete a user 
For detailed documentation, refer to API Documentation.
Code Documentation
* Inline comments in code files explain important sections of the code, including logic, algorithms, and behaviour.

Contact Information
For questions or feedback, please contact:
* Email: rockjoe2121@gmail.com

