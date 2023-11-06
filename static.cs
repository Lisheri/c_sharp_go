namespace Acme.Collections;
public class Stack<T> {
  Entry _top;

  public void Push(T data) {
    _top = new Entry(_top, data);
  }

  public T Pop() {
    if (_top == null) {
      throw new InvalidOperationException();
    }
    T result = _top.Data;
    _top = _top.Next;
    return result;
  }

  class Entry {
    public Entry Next { get; set; }
    public T Data { get; set; }

    // 构造函数
    public Entry(Entry next, T data) {
      Next = next;
      Data = data;
    }
  }
}

// C#程序可以存储在多个源文件中。在编译C#程序时, 将同时处理所有源文件, 并且源文件可以自由地相互引用。从概念上讲, 就好像所有源文件在被处理之前都链接到一个大文件中。
// 在 C# 中永远都不需要使用前向声明, 因为声明顺序无关紧要(极少数例外情况除外)。
// C# 不限制源文件只能声明一种公共类型, 也不要求源文件的文件名必须与其中声明的类型相匹配
