namespace book_marks
{
  public class Book
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public uint Pages { get; set; }
    public uint CurrentPage { get; set; }

    public Book(string title, string description, uint pages)
    {
      Title = title;
      Description = description;
      Pages = pages;
      CurrentPage = 0;
    }
  }
}