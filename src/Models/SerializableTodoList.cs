using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Models
{
  public static class SerializableTodoListExtension
  {
    public static XElement ToXElement(this TodoList todoList)
    {
      XElement todoListXElement = new XElement("TodoList");

      todoListXElement.SetAttributeValue("IdCount", todoList.IdCount);

      foreach (TodoItem item in todoList.Items)
      {
        todoListXElement.Add(item.ToXElement());
      }

      return todoListXElement;
    }
  }

  public class SerializableTodoList
  {
    public static TodoList FromXElement(XElement xElement)
    {
      string strIdCount = xElement.Attribute("IdCount").Value;
      long idCount = Int64.Parse(strIdCount); // handle

      List<TodoItem> lst = new List<TodoItem>();

      foreach (XElement xel in xElement.Elements())
      {
        lst.Add(TodoItem.FromXElement(xel));
      }

      return TodoList.FromListOfTodoItems(lst, idCount);
    }
  }
}