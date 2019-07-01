using System;

using Models;

namespace App
{
  class Program
  {
    static void Main(string[] args)
    {
      TodoList lst = new TodoList();
      foreach (TodoItem it in lst.Items)
      {
        Console.WriteLine(it.ItemText);
        Console.WriteLine(it.Id);
        Console.WriteLine(it.TimeStamp);
      }
    }
  }
}