using System;
using System.Xml.Linq;


namespace Models
{

  public class TodoItem : SerializableTodoItem
  {
    public long Id { get; private set; }
    public DateTime TimeStamp { get; private set; }
    public string ItemText { get; private set; }
    public bool IsCompleted { get; private set; }


    public TodoItem(long id, string text) // constructor for making brand new item.
    {
      Id = id;
      ItemText = text;
      TimeStamp = DateTime.Now;
      IsCompleted = false;
    }

    private TodoItem(long id, string text, DateTime time, bool isCompleted) // constructor for creating new instance after loadin from xml.
    {
      Id = id;
      ItemText = text;
      TimeStamp = time;
      IsCompleted = isCompleted;
    }

    public void Update(string txt)
    {
      ItemText = txt;
    }

    public void MarkComplete()
    {
      IsCompleted = true;
    }

    public void MarkIncomplete()
    {
      IsCompleted = false;
    }

    public static TodoItem LoadFromDetails(long id, string text, DateTime time, bool isCompleted)
    {
      return new TodoItem(id, text, time, isCompleted);
    }

    // public static TodoItem FromXElement(XElement xElement)
    // {
    //   return SerializableTodoItem.FromXElement(xElement);
    // }
  }
}