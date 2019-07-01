using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Models
{
  public class TodoList
  {
    public static readonly string DEFAULT_XML_FILE_PATH = Path.Combine(Environment.CurrentDirectory, "list.xml");

    private long IdCount;
    private readonly List<TodoItem> ItemList;
    public IReadOnlyList<TodoItem> Items { get; }

    public TodoList()
    {
      IdCount = 0;
      ItemList = new List<TodoItem>();
      Items = ItemList.AsReadOnly();
    }

    public IReadOnlyList<TodoItem> Add(string text)
    {
      TodoItem newItem = new TodoItem(++IdCount, text);
      ItemList.Add(newItem);

      return Items;
    }

    public IReadOnlyList<TodoItem> Delete(long id)
    {
      if (ItemList.RemoveAll(x => x.Id == id) == 0) throw new ArgumentOutOfRangeException("id");

      return Items;
    }

    public IReadOnlyList<TodoItem> Update(long id, string newText)
    {
      TodoItem it = ItemList.Find(x => x.Id == id);
      if (it == null) throw new ArgumentOutOfRangeException("Id");

      it.ItemText = newText;

      return Items;
    }

    // is this really the best way to do this?
    // replace with foreach for one.
    // but its the casts and parsing I'm worried about.
    static void LoadFromFile()
    {
      XDocument doc = XDocument.Load(DEFAULT_XML_FILE_PATH);
      doc.Elements().Aggregate<XElement, List<TodoItem>>(new List<TodoItem>(), (List<TodoItem> lst, XElement xel) =>
      {
        string id = xel.Element("Id").Value;
        string itemText = xel.Element("ItemText").Value;
        string timeStamp = xel.Element("TimeStamp").Value;

        try
        {
          TodoItem it = new TodoItem(Int64.Parse(id), itemText, new DateTime(Int64.Parse(timeStamp)));
          lst.Add(it);
          return lst;
        }
        catch (FormatException)
        {
          throw new Exception("Id of TodoItem could not be parsed from xml!");
        }
      });
    }

    static void SaveToFile() { }
  }
}