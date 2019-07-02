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
        string rep = $"|{it.Id}|\n\t{it.ItemText}";
        System.Console.WriteLine(rep);
      }
    }
  }
}