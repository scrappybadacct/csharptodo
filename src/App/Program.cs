using System;

using Models;

namespace App
{
  class Program
  {
    static void Main(string[] args)
    {
      switch (args[0])
      {
        case "add":
          bool flag = AddItem(args[1]);
          if (flag)
          {
            System.Console.WriteLine("item added.");
          }
          else
          {
            System.Console.WriteLine("error");
          }
          break;

        case "list-all":
          ListAll();
          break;

        case "remove":
          bool flag1 = RemoveItem(Int64.Parse(args[1]));

          if (flag1)
          {
            System.Console.WriteLine("removed");
          }
          else
          {
            System.Console.WriteLine("error");
          }
          break;

        case "edit":
          bool flag2 = UpdateItem(Int64.Parse(args[1]), args[2]);

          if (flag2)
          {
            System.Console.WriteLine("updated");
          }
          else
          {
            System.Console.WriteLine("error");
          }
          break;

        default:
          Console.WriteLine("!!!");
          break;
      }
    }

    static bool AddItem(string todotext)
    {
      TodoList lst = new TodoList();
      lst.Add(todotext);
      return lst.Save();
    }

    static void ListAll()
    {
      TodoList lst = new TodoList();

      foreach (TodoItem it in lst.Items)
      {
        string rep = $"|{it.Id}|\n\t{it.ItemText}\nDone: {(it.IsCompleted ? "True" : "False")}";
        System.Console.WriteLine(rep);
      }
    }

    static bool RemoveItem(long id)
    {
      TodoList lst = new TodoList();
      lst.Delete(id);

      return lst.Save();
    }

    static bool UpdateItem(long id, string newText)
    {
      TodoList lst = new TodoList();
      lst.Update(id, newText);

      return lst.Save();
    }
  }
}