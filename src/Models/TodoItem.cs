using System;

namespace Models
{
  public class TodoItem
  {
    public long Id;
    public DateTime? TimeStamp; // I didn't want to have to pass this, and wanted this and id to be readonly. ??
    public string ItemText;

    public TodoItem(long id, string text, DateTime? time = null)
    {
      Id = id;
      ItemText = text;
      TimeStamp = time != null ? time : DateTime.Now;
    }
  }
}