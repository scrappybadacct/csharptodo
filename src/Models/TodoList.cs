using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Models
{
  public class TodoList : FsSavableTodoList
  {
    public static readonly string DEFAULT_XML_FILE_PATH = Path.Combine(Environment.CurrentDirectory, "list.xml");

    public long IdCount { get; private set; }
    private readonly List<TodoItem> ItemList;
    public IReadOnlyList<TodoItem> Items { get; }

    public TodoList()
    {
      IdCount = 0;
      ItemList = new List<TodoItem>();
      Items = ItemList.AsReadOnly();
    }

    private TodoList(List<TodoItem> lst, long idCount)
    {
      IdCount = idCount;
      ItemList = lst;
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

    public static TodoList FromListOfTodoItems(List<TodoItem> lst, long idCount)
    {
      return new TodoList(lst, idCount);
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

    static TodoList LoadFromFile()
    {
      XDocument doc = XDocument.Load(DEFAULT_XML_FILE_PATH);
      XElement todoListXElement = doc.Element("TodoList");

      return TodoList.FromXElement(todoListXElement);
    }

    // Error handling? return bool?
    static void SaveToFile(List<TodoItem> lst, long cnt)
    {
      XElement newRoot = new XElement("root");
      newRoot.SetAttributeValue("IdCount", cnt);

      XDocument newDoc = new XDocument(newRoot);

      foreach (TodoItem it in lst)
      {
        XElement xel = it.ToXElement();

        newDoc.Element("root").Add(xel);
      }

      newDoc.Save(DEFAULT_XML_FILE_PATH);
    }
  }
}