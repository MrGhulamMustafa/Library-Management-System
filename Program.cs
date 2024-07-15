using System;
using System.Collections.Generic;
using System.Net;

namespace MyApp
{ // Abstract base class Book
   internal abstract class Book { public string Title { get; set; } public string Author { get; set; } public string BookId { get; set; }

    // Constructor
    public Book(string title, string author, string bookId)
    {
        Title = title;
        Author = author;
        BookId = bookId;
    }

    // Abstract method to be implemented in derived classes
    public abstract void DisplayInfo();
}

// Derived class Fiction
internal class Fiction : Book
{
    // Constructor
    public Fiction(string title, string author, string bookId) : base(title, author, bookId)
    {
    }

    // Implementing abstract method for Fiction class
    public override void DisplayInfo()
    {
        Console.WriteLine($"Fiction Book - Title: {Title}, Author: {Author}, Book ID: {BookId}");
        Console.WriteLine("This is a thrilling story for fiction lovers.");
    }
}

// Derived class NonFiction
internal class NonFiction : Book
{
    // Constructor
    public NonFiction(string title, string author, string bookId) : base(title, author, bookId)
    {
    }

    // Implementing abstract method for NonFiction class
    public override void DisplayInfo()
    {
        Console.WriteLine($"Non-Fiction Book - Title: {Title}, Author: {Author}, Book ID: {BookId}");
        Console.WriteLine("This is a factual book for those seeking knowledge.");
    }
}

// Person class
internal class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string PersonId { get; set; }

    // Constructor
    public Person(string name, int age, string personId)
    {
        Name = name;
        Age = age;
        PersonId = personId;
    }
}

// Librarian class inheriting from Person
internal class Librarian : Person
{
    public string EmpId { get; set; }
    public List<Book> IssuedBooks { get; set; }

    // Constructor
    public Librarian(string name, int age, string personId, string empId) : base(name, age, personId)
    {
        EmpId = empId;
        IssuedBooks = new List<Book>();
    }

    // Method to issue a book to a user
    public void IssueBook(Book book, Person user)
    {
        if (book != null)
        {
            IssuedBooks.Add(book);
            Console.WriteLine($"The book '{book.Title}' is issued to {user.Name}");
            Console.WriteLine($"Transaction logged: {user.Name} issued '{book.Title}'");
        }
        else
        {
            Console.WriteLine($"The book is not present");
        }
    }

    // Method to return a book from a user
    public void ReturnBook(Book book, Person user)
    {
        if (book != null)
        {
            IssuedBooks.Remove(book);
            Console.WriteLine($"The book '{book.Title}' is returned by {user.Name}");
            Console.WriteLine($"Transaction logged: '{book.Title}' returned by {user.Name}");
        }
        else
        {
            Console.WriteLine($"{user.Name} did not return the book");
        }
    }
}

// Library class
internal class Library
{
    public string Name { get; set; }
    public string ID { get; set; }
    private List<Book> books;
    private Librarian librarian;
    private List<string> transactionHistory;

    // Constructor
    public Library(string name, string id, Librarian librarian)
    {
        Name = name;
        ID = id;
        books = new List<Book>();
        this.librarian = librarian;
        transactionHistory = new List<string>();
    }

    // Method to add a book to the library
    public void AddBook(Book book)
    {
        if (!books.Contains(book))
        {
            books.Add(book);
            Console.WriteLine($"Book '{book.Title}' added to the library.");
            transactionHistory.Add($"Book '{book.Title}' added to the library.");
        }
        else
        {
            Console.WriteLine($"The book '{book.Title}' already exists.");
        }
    }

    // Method to remove a book from the library
    public void RemoveBook(string bookId)
    {
        Book bookToRemove = books.Find(b => b.BookId == bookId);
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine($"{bookToRemove.Title} is removed from the library.");
            transactionHistory.Add($"{bookToRemove.Title} is removed from the library.");
        }
        else
        {
            Console.WriteLine($"Either the book with ID '{bookId}' is not present in library or already removed.");
        }
    }

    // Method to view all books in the library
    public void ViewBooks()
    {
        Console.WriteLine("Books in the library:");
        foreach (Book book in books)
        {
            book.DisplayInfo();
        }
    }

    // Method to search for a book by title
    public void SearchBook(string title)
    {
        bool found = false;
        foreach (Book book in books)
        {
            if (book.Title == title)
            {
                book.DisplayInfo();
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine($"There is no book with the title '{title}' in the {Name} library.");
        }
    }

    // Method to list all issued books
    public void ListIssuedBooks()
    {
        Console.WriteLine("Issued books:");
        foreach (Book book in librarian.IssuedBooks)
        {
            book.DisplayInfo();
            Console.WriteLine("--------------------------------------------------");
        }
    }

    // Method to display transaction history
    public void DisplayTransactionHistory()
    {
        Console.WriteLine("\nTransaction History:");
        foreach (string transaction in transactionHistory)
        {
            Console.WriteLine(transaction);
        }
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        // Create instances of different types of books (Fiction and NonFiction)
        Fiction fictionBook = new Fiction("The Lord of the Rings", "J.R.R. Tolkien", "FIC-001");
        NonFiction nonFictionBook = new NonFiction("Sapiens: A Brief History of Humankind", "Yuval Noah Harari", "NFIC-001");

        // Display book information using polymorphism
        fictionBook.DisplayInfo();
        Console.WriteLine();

        nonFictionBook.DisplayInfo();
        Console.WriteLine();

        // Create librarian
        Librarian librarian = new Librarian("Ghulam Mustafa", 24, "5599", "5599-Ghulam Mustafa");

        // Create library
        Library library = new Library("National Library", "1", librarian);

        // Add books to the library
        library.AddBook(fictionBook);
        library.AddBook(nonFictionBook);

        // Issue a book
        Person user = new Person("Khurram Aziz", 23, "5577");
        librarian.IssueBook(fictionBook, user);

        // List issued books
        library.ListIssuedBooks();

        // Return a book
        librarian.ReturnBook(fictionBook, user);

        // List issued books again
        library.ListIssuedBooks();

        // Search for a book by title
        library.SearchBook("Sapiens: A Brief History of Humankind");

        // Remove a book from the library
        library.RemoveBook("NFIC-001");

        // View books in the library again
        library.ViewBooks();

        // Display transaction history
        library.DisplayTransactionHistory();
    }
}
}