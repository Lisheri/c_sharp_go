// See https://aka.ms/new-console-template for more information
using System;
class Hello {
  static void Main() {
    var s = new Acme.Collections.Stack<int>();
    s.Push(1);
    s.Push(10);
    s.Push(100);
    Console.WriteLine(s.Pop()); // stack contains 1, 10
    Console.WriteLine(s.Pop()); // stack contains 1
    Console.WriteLine(s.Pop()); // stack is empty
    Console.WriteLine("Hello World");
    TypeAndMember.ClassAndObject.ClassAndObject.main();
    TypeAndMember.InterfaceAndEnum.InterfaceAndEnum.main();
    ProgramBlock.ProgramBlock.main();
    MainLanguageArea.MainLanguageArea.Usage();
  }
}
