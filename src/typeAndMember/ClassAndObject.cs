using System.Diagnostics.Contracts;

namespace TypeAndMember.ClassAndObject;

// 要用方法重写父类中的虚方法，必须使用 override 关键字，以免发生意外重定义
// 在 C# 中，结构就像是轻量级类，是可以实现接口但不支持继承的堆栈分配类型。
//  C# 提供了 record class 和 record struct 类型，这些类型的目的主要是存储数据值。

// + 所有类型都通过 `构造函数` 进行初始化。两个构造函数声明具有唯一的行为
//    - 无参构造函数, 该构造函数将所有字段初始化为其默认值
//    - 主构造函数, 该构造函数声明该类型的实例的必须参数

// + 类和对象
// 类是最基本的C#类型。为实例对象提供定义

// 简单类Point声明
public class Point {
  public int X { get; }
  public int Y { get; }

  // 构造函数
  public Point(int x, int y) => (X, Y) = (x, y);
}

public class ClassAndObject {
  // Point工厂函数, 批量创建point
  class PointFactory(int numberOfPoints) {
    public IEnumerable<Point> CreatePoints() {
      // 创建随机数
      var generator = new Random();
      for (int i = 0; i < numberOfPoints; i++) {
        // ? int generator.Next(min, max?), 不传max则min为max
        yield return new Point(generator.Next(10), generator.Next(200));
      }
    }
  }

  static void usePoint() {
    var factory = new PointFactory(10);
    foreach (var point in factory.CreatePoints()) {
      Console.WriteLine($"({point.X}, {point.Y})");
    }
  }

  // 泛型使用
  static void usePair() {
    // 第一个是int, 第二个是string
    Pair<int, string> pair = new Pair<int, string>(1, "two");
    int i = pair.First;
    string s = pair.Second;
    Console.WriteLine($"{i}, {s}");
  }

  // * 可以将类类型隐式转换成其任意基类类型
  static void usePoint3D() {
    // 调用Point构造函数
    Point a = new(10, 20);
    // ? 类类型的变量可以引用相应类的实例或任意派生类的实例
    // ? 这里就是 Point类型引用了 Point3D的实例, 这里的 b 实际上是派生类 Point3D 的实例, 而非 Point的实例, 调用的也是 Point3D的构造函数
    // ? 但是 b 依然是Point类型, 不能使用Point3D的Z变量
    Point b = new Point3D(10, 20, 30);

    Console.WriteLine($"Point a: {a.X}, {a.Y}\nPoint b: {b.X}, {b.Y} {b}");
  }

  // 当前模块主要入口
  public static void main() {
    // 调用批量创建函数
    // ? 类的静态成员调用静态方法可以省略前缀 [namespace].[className]
    // usePoint();
    usePair();
    usePoint3D();
  }
}

// 泛型(类型参数)
public class Pair<T, D> {
  public T First { get; }
  public D Second { get; }

  // 构造函数
  public Pair(T first, D second) => (First, Second) = (first, second);
}

// 基类
// * 类声明可以指定基类。 在类名和类型参数后面加上冒号和基类的名称。
public class Point3D : Point {
  public int Z { get; set; }

  // * 这个base相当于调用父类构造函数
  // * 继承意味着一个类隐式包含其基类的几乎所有成员
  // * 类不继承实例、静态构造函数(static)以及终结器
  public Point3D(int x, int y, int z) : base(x, y) {
    Z = z;
  }
}

// 结构体, 不能声明基类型进行派生。
// * 所有结构体从 System.ValueType 隐式派生
public struct PointStruct {
  public double X { get; }
  public double Y { get; }

  public PointStruct(double x, double y) => (X, Y) = (x, y);
}

// Interface

