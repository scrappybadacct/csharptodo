using System;

namespace Models
{
  public class TodoItem
  {
    public long Id;
    public DateTime? TimeStamp;
    public string ItemText;

    public TodoItem(long id, string text, DateTime? time = null)
    {
      Id = id;
      ItemText = text;
      TimeStamp = time != null ? time : DateTime.Now;
    }
  }
}