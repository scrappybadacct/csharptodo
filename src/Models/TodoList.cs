using System;
using System.Collections.Generic;
/* 
  I'm taking a break here.
  My plan going forward is to create save and load static methods which can serialize the object in XML to an external file.
  The load method will be called by the object in the constructor. 
    - It will check if there is a file(stored at a standard location), and if it deserializes to list<TodoItems> and set that to ItemList.
    - else it will set an empty List.
  The Save method will serialize the object, then save it to the file.
    - It will be exposed through a public method on the instance, which calls the static method with "this".
*/

namespace Models
{
  public class TodoList
  {
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

    static void LoadFromFile() { }
    static void SaveToFile() { }
  }
}
