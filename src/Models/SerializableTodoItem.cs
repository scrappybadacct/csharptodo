using System;
using System.Xml.Linq;

namespace Models
{
  public static class SerializableTodoItemExtensionMethod
  {
    public static XElement ToXElement(this TodoItem item)
    {
      return new XElement("TodoItem",
        new XElement("Id", item.Id),
        new XElement("TimeStamp", item.TimeStamp),
        new XElement("ItemText", item.ItemText),
        new XElement("IsCompleted", item.IsCompleted)
      );
    }
  }

  public class SerializableTodoItem
  {
    public static TodoItem FromXElement(XElement xElement)
    {
      long id;
      DateTime timeStamp;
      bool isCompleted;

      string strId = xElement.Element("Id").Value;
      string strTimeStamp = xElement.Element("TimeStamp").Value;
      string strIsCompleted = xElement.Element("IsCompleted").Value;

      if (Int64.TryParse(strId, out id)
          && DateTime.TryParse(strTimeStamp, out timeStamp)
          && Boolean.TryParse(strIsCompleted, out isCompleted))
      {
        string itemText = xElement.Element("Itemtext").Value;

        return TodoItem.LoadFromDetails(id, itemText, timeStamp, isCompleted);
      }
      else
      {
        throw new Exception("Todo Item details could not be parsed from xml!");
      }
    }
  }
}