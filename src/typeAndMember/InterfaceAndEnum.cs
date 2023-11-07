using System.Reflection;

namespace TypeAndMember.InterfaceAndEnum;

public class InterfaceAndEnum {
  public static void main() {
    Console.WriteLine("------------------- interface and enum -------------------");
    EditBox editBox = new();
    // 当类或结构体实现特定接口时, 此类或结构体的实例可以隐式转换成相应的接口类型
    IControl control = editBox;
    IDataBound dataBound = editBox;
    NullOrNotNull.staticFunc();
    Tuple.staticFunc();
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
// * 枚举定义了一组常量
public enum SomeRootVegetable {
  HorseRadish,
  Radish,
  Turnip
}

// * 枚举的Flags标记, 可以让空位枚举成员多项填充
// ? 下面的枚举Seasons, 3是空位, 会被解析为 1 + 2, 因此3对应的是 summer, Autumn
[Flags]
public enum Seasons {
  None = 0,
  Summer = 1,
  Autumn = 2,
  Winter = 4,
  Spring = 8,
  All = Summer | Autumn | Winter | Spring
}

// + 可为null 以及 不可为null
class NullOrNotNull {
  public static void staticFunc() {
    // 声明 optionalInt 为可为null的int类型值, 默认为null
    int? optionalInt = default;
    // 赋值
    optionalInt = 5;
    string? optionalText = default;
    optionalText = "hello world!";
    Console.WriteLine($"{optionalInt} {optionalText}");
  }
}

// + 元组
class Tuple {
  public static void staticFunc() {
    (double Sum, int Count) t2 = (4.5, 3);
    Console.WriteLine($"Sum of {t2.Count} elements is {t2.Sum}");
  }
}
