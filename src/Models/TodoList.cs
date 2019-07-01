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
      ItemList = File.Exists(DEFAULT_XML_FILE_PATH) ? LoadFromFile() : new List<TodoItem>();
      IdCount = ItemList.Count;
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

      it.Update(newText);

      return Items;
    }

    public void Save()
    {
      SaveToFile(ItemList);
    }

    static List<TodoItem> LoadFromFile()
    {
      XDocument doc = XDocument.Load(DEFAULT_XML_FILE_PATH);
      return doc.Element("root").Elements().Aggregate<XElement, List<TodoItem>>(new List<TodoItem>(), (List<TodoItem> lst, XElement xel) =>
      {
        string id = xel.Element("Id").Value;
        string itemText = xel.Element("ItemText").Value;
        string timeStamp = xel.Element("TimeStamp").Value;

        try
        {
          TodoItem it = new TodoItem(Int64.Parse(id), itemText, DateTime.Parse(timeStamp));
          lst.Add(it);
          return lst;
        }
        catch (FormatException)
        {
          throw new Exception("Id of TodoItem could not be parsed from xml!");
        }
      });
    }

    static void SaveToFile(List<TodoItem> lst)
    {
      XDocument newDoc = new XDocument(new XElement("root"));
      foreach (TodoItem it in lst)
      {
        XElement xel = new XElement("TodoItem",
        new XElement("Id", it.Id),
        new XElement("ItemText", it.ItemText),
        new XElement("TimeStamp", it.TimeStamp)
        );

        newDoc.Element("root").Add(xel);
      }

      newDoc.Save(DEFAULT_XML_FILE_PATH);
    }
  }
}