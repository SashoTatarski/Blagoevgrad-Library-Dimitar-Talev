# Regional Library in Blagoevgrad, Bulgaria

## Team project of **South-West**
### **TeamLead:*8 Aleksandar Tatarski
### **Team Members:** Hristina Stanoeva and Aleksandar Tatarski
---
### Link to Trello Board: https://trello.com/b/7QyCeeWB
        The Regional Library - Blagoevgrad was established in 1953. It is the main library for southwestern Bulgaria.
        It provides the following activities: 
         - Library Information Services;
         - Local automated network and providing access to databases in the country and abroad; 
         - Maintaining an archive of local press and local history literature; 
         - Interlibrary loan;
         - Supporting and coordinating the activities of libraries in the region.

        Regional Library - Blagoevgrad is the largest non-metropolitan library in Western Bulgaria by quantity and quality
        of library stock, by structure, by activity and by the level of automation achieved.

### **The following project is one step further in developing the great automation system of the library.**

The Application is a **Library management system** which helps the library:
- keep track of the books and their checkouts and reservation
- manage users’ subscriptions and profiles
- maintaining the database for entering new books and removing old and damaged ones
- recording books that have been borrowed with their respective due dates

The Application provides variaty of commands:
- **LogIn** → logs in an account
- **LogOut** → logs out an account
- **Register User** → registers new user
- **Register Librarian** → registers new librarian
- **Remove User** → sets the status of a user to Inactive
- **Add Book** → adds new book
- **Edit Book** → by given book and parameter, modifies this parameter of the book
- **Remove Book** → removes a book (if it is demaged)
- **CheckOut Book** → lends book to the user who invokes the command
- **Renew Book** → extends the return due date of lended book by 5 days
- **Reserve Book** → reserves a book for the user
- **Return Book** → returns checkedout book by a user
- **Search** → searches in all books in the library by given parameter
- **View Account** → gets the information for the current(logged in) account
- **Travel In Time** → virtually goes given number of days ahead

---
The Library has existing database, which data you can find in:
- catalog.json
- users.json
- librarians.json
---

## **How to operate with the application:**

## 1. In order to access the system, you have to login first.

- use some of the given credentials.
 
    Depending on the type of account you are logged in (User or Librarian) you can perform different commands:

### Common commands:
- LogIn
- LogOut
- Search
- Travel In Time

 ### Librarian:
 - Register User
 - Register Librarian
 - Add Book
 - Remove Book
 - Edit Book

 ### User:
 - CheckOut Book
 - Renew Book
 - Return Book
 - Reserve Book

---
Some constraints about the parametres:

---

**AddbookCommand:** (adds a new book with book items)
1. Title (between 1 and 30 symbols)
3. Author (between 1 and 50 symbols)
4. Publisher (between 1 and 50 symbols)
6. Genre (between 1 and 50 symbols)
7. Location in Library (Rack)

**EditBookCommand:** (modifies the parameters)
1. Enter ID Code (unique)
2. Enter parameter to modify (Title, Author, Genre or Publisher)
3. Enter new Title/Author/Genre/Publisher

**RemoveBookCommand:** (removes book only if its not taken or reserved)
1. Enter ID Code

**SearchCommand** (Searches through catalogue and prints out book details)
1. Enter parameter to search by (ID, Title, Author, Genre or Publisher)
2. Enter parameter details

**RegisterUserCommand** (Registers a new user in the system)
1. Enter new user's username
2. Enter new user's password

**RemoveUserCommand** (Cancels the registration of a user in the system)
1. Enter user's username

**ViewAccountCommand** (displays account details of the logged user)

**Travel In Time Command** (skips days ahead from today)
1. Enter days you want to skip
