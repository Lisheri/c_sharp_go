using System.Reflection;

namespace TypeAndMember.InterfaceAndEnum;

public class InterfaceAndEnum {
  public void main() {
    Console.WriteLine("------------------- interface and enum -------------------");
    EditBox editBox = new();
    // 当类或结构体实现特定接口时, 此类或结构体的实例可以隐式转换成相应的接口类型
    IControl control = editBox;
    IDataBound dataBound = editBox;
    Console.WriteLine("------------------- interface and enum -------------------");
  }
}

// interface允许多重继承
interface IControl {
  void Paint();
}

interface ITextBox : IControl {
  void SetText(string text);
}

interface IListBox : IControl {
  void SetList(string[] list);
}

// 同时继承了 ITextBox 以及 IListBox
interface IComboBox : ITextBox, IListBox { }

interface IDataBound {
  void Bind(Binder b);
}

// 类同时实现多个接口
public class EditBox : IControl, IDataBound {
  public void Paint() {
    Console.WriteLine("实现了IControl");
  }

  public void Bind(Binder b) {
    Console.WriteLine($"实现了IDataBound {b}");
  }
}

// 枚举


