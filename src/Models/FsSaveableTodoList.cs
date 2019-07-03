using System;
using System.Linq;
using System.Xml.Linq;

namespace Models
{
  public static class FsSavableTodoListExtensionMethods
  {
    public static bool SaveToFile(this TodoList todoList, string filePath)
    {
      XElement todoListXElement = todoList.ToXElement();

      // Is this try/catch completely unecessary?
      try
      {
        new XDocument(todoListXElement).Save(filePath);
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }
  }

  public class FsSavableTodoList : SerializableTodoList
  {
    public static TodoList LoadFromFile(string filePath)
    {
      XDocument loadedDocument = XDocument.Load(filePath);

      return TodoList.FromXElement(loadedDocument.Element("TodoList"));
    }
  }
}