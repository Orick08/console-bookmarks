namespace book_marks
{
  public class InvalidBookException : Exception
  {
    public InvalidBookException(string message) : base(message)
    {
    }
  }
}