using System;

namespace Models
{
  public class TodoItem
  {
    public long Id { get; private set; }
    public DateTime TimeStamp { get; private set; }
    public string ItemText { get; private set; }

    public TodoItem(long id, string text)
    {
      Id = id;
      ItemText = text;
      TimeStamp = DateTime.Now;
    }

    public TodoItem(long id, string text, DateTime time)
    {
      Id = id;
      ItemText = text;
      TimeStamp = time;
    }

    public void Update(string txt)
    {
      ItemText = txt;
    }
  }
}