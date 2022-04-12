using System;
using System.Collections.Generic;
using book_marks;

namespace Program
{
  class Program
  {
    public static Dictionary<int, Book> db = new Dictionary<int, Book>();
    public static int index = 1;

    public static void Main(string[] args)
    {
      Console.Beep();

      string option = "q";

      do
      {
        Console.Clear();
        Console.WriteLine("WELCOME TO BOOK MARKS\nPlease, select an option:");
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("CMD\t| Action");
        Console.ResetColor();
        Console.WriteLine("new\t| New book");
        Console.WriteLine("show\t| Show saved books");
        Console.WriteLine("read\t| Change the current page of a book");
        Console.WriteLine("q\t| Quit");

        option = Console.ReadLine() ?? "ups";

        try
        {
          Option(option);
        }
        catch (Exception e)
        {
          Console.WriteLine(e.Message);
        }

      } while (option != "q");

      Console.WriteLine("Bye!");
    }

    private static void Option(string o)
    {
      if (o == "q" || o == "ups")
        return;

      switch (o)
      {
        case "new":
          RegisterNewBook();
          return;
        case "show":
          ShowDatabase();
          return;
        case "read":
          ReadBook();
          return;
      }
    }

    private static void RegisterNewBook()
    {
      Console.Clear();
      string title = "";
      string? description;
      uint pages = 0;
      do
      {
        Console.WriteLine("Title? ");
        title = Console.ReadLine() ?? "";
      } while (title == "");

      Console.WriteLine("Description? (Optional) ");
      description = Console.ReadLine() ?? "";

      do
      {
        Console.WriteLine("Pages?");
        string input = Console.ReadLine() ?? "0";
        uint.TryParse(input, out pages);

        //Error message
        if (pages == 0)
          Console.WriteLine("Invalid number, please try again");
      } while (pages == 0);

      try
      {
        Book book = new Book(title, description, pages);
        db.Add(index, book);
        index++;
      }
      catch (Exception exp)
      {
        Console.WriteLine(exp.Message);
      }

      Console.WriteLine("Book saved!");
      Console.ReadLine();
    }

    private static void ShowDatabase()
    {
      Console.Clear();
      Console.BackgroundColor = ConsoleColor.Black;
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("ID\t|Title\t|Description\t|Pages\t|C/P\t|%");
      Console.ResetColor();

      foreach (KeyValuePair<int, Book> kvp in db)
      {
        //ISSUE: Porcentaje is not properly showed
        Console.WriteLine($"{kvp.Key}\t|{kvp.Value.Title}\t|{kvp.Value.Description}\t\t|{kvp.Value.Pages}\t|{kvp.Value.CurrentPage}\t|{kvp.Value.Porcentaje}");
      }

      Console.ReadLine();
    }

    private static void ReadBook()
    {
      Console.Clear();
      //Check if is there at least one book
      if (db.Count <= 0)
      {
        Console.WriteLine("There is no books registered yet...");
        Console.ReadLine();
        return;
      }

      Console.WriteLine("Book id?");
      int id = 0;
      bool validId = false;
      do
      {
        int.TryParse(Console.ReadLine(), out id);
        if (db.ContainsKey(id))
          validId = true;
        else
          Console.WriteLine("Invalid ID, plase try again");
      } while (!validId);

      Console.WriteLine("Current page?");
      uint currentPage = 0;
      do
      {
        uint.TryParse(Console.ReadLine(), out currentPage);
        //TODO: Better validation over here
        if (currentPage == 0 || currentPage > db[id].Pages)
          Console.WriteLine("Invalid current page, plase try again");
      } while (currentPage == 0 || currentPage > db[id].Pages);

      db[id].CurrentPage = currentPage;

      Console.WriteLine("Current page succesfully updated, press enter to continue");
      Console.ReadLine();
    }
  }
}