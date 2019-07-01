using System;

// start here!
namespace Models
{
  public class TodoItem
  {
    readonly public long Id;
    readonly public DateTime TimeStamp;
    public string ItemText;

    public void TodoItem(long id, string text)
    {
      Id = id;
      ItemText = text;
      TimeStamp = DateTime.Now();
    }
  }
}