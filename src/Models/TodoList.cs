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
      if (File.Exists(DEFAULT_XML_FILE_PATH))
      {
        (IdCount, ItemList) = LoadFromFile();
      }
      else
      {
        IdCount = 0;
        ItemList = new List<TodoItem>();
      }

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

    public bool Save()
    {
      try
      {
        SaveToFile(ItemList, IdCount);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    static (long idCount, List<TodoItem> lst) LoadFromFile()
    {
      XDocument doc = XDocument.Load(DEFAULT_XML_FILE_PATH);

      List<TodoItem> listFromXML = doc.Element("root").Elements().Aggregate<XElement, List<TodoItem>>(new List<TodoItem>(), (List<TodoItem> lst, XElement xel) =>
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

      long idCountFromXml = Int64.Parse(doc.Element("root").Attribute("IdCount").Value);

      return (idCount: idCountFromXml, lst: listFromXML);
    }

    static void SaveToFile(List<TodoItem> lst, long cnt)
    {
      XElement newRoot = new XElement("root");
      newRoot.SetAttributeValue("IdCount", cnt);

      XDocument newDoc = new XDocument(newRoot);
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