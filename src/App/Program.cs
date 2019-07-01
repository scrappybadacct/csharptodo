using System;

using Models;

namespace App
{
  class Program
  {
    static void Main(string[] args)
    {
      TodoList lst = new TodoList();
      lst.Add("Whatever");
      lst.Add("cool");
      foreach (TodoItem it in lst.Items)
      {
        Console.WriteLine(it.ItemText);
        Console.WriteLine(it.Id);
        Console.WriteLine(it.TimeStamp);
      }
    }
  }
}