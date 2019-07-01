using System;

namespace Models
{
  public class TodoItem
  {
    public long Id { get; private set; }
    public DateTime? TimeStamp { get; private set; }
    public string ItemText { get; private set; }

    public TodoItem(long id, string text, DateTime? time = null)
    {
      Id = id;
      ItemText = text;
      TimeStamp = time != null ? time : DateTime.Now;
    }

    public void Update(string txt)
    {
      ItemText = txt;
    }
  }
}