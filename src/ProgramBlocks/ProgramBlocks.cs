using System.Diagnostics;

namespace ProgramBlock;

public class ProgramBlock {
  public static void main() {
    Console.WriteLine("------------------- 程序构建基块 -------------------");
    Color color = new Color(10, 20, 30);
    Console.WriteLine($"({color.R}, {color.G}, {color.B}), red: ({Color.Red.R}, {Color.Red.G}, {Color.Red.B})");
    FuncObject obj = new FuncObject();
    Console.WriteLine($"{obj.ToString()}");
    Params.main();
    // 参数数组
    int x, y, z;
    x = 3;
    y = 4;
    z = 5;
    // WriteLine的函数声明为: void WriteLine(string fmt, params object[] args) {};
    Console.WriteLine("x={0} y={2} z={1}", x, y, z);
    UseExpression.main();
    OtherFunctionMember.Entry.main();
    // OverloadingExample.useOverload();
    Console.WriteLine("------------------- 程序构建基块 -------------------");
  }
}

public class Color {
  public static readonly Color Black = new(0, 0, 0);
  public static readonly Color White = new(255, 255, 255);
  public static readonly Color Red = new(255, 0, 0);
  public static readonly Color Green = new(0, 255, 0);
  public static readonly Color Blue = new(0, 0, 255);

  public byte R;
  public byte G;
  public byte B;

  public Color(byte r, byte g, byte b) {
    R = r;
    G = g;
    B = b;
  }
}

struct FuncObject {
  public override string ToString() => "This is an object";
}


// 参数
// - 分为四类: 值参数, 引用参数, 输出参数和参数数组
// - 和js一样, 有可选参数和默认值
// - 引用参数传递的自变量必须是一个带有明确值的变量, 在方法执行期间, 引用参数指向的地址和自变量本身一样, 使用ref进行修饰
class Params {
  public static void main() {
    SwapExample();
    OutUsage();
  }

  static void Swap(ref int x, ref int y) {
    int temp = x;
    // x指针指向y的地址
    x = y;
    // y指针指向原有x的地址
    y = temp;
  }

  static void SwapExample() {
    int i = 1, j = 2;
    Swap(ref i, ref j);
    Console.WriteLine($"{i} - {j}"); // 经过Swap后引用地址发生变化
  }

  // 输出参数用于按引用传递自变量。与引用参数类似, 不同之处在于, 不要求向调用方提供自变量显示赋值。
  // ? 输出参数使用 out 修饰符进行声明
  static void Divide(int x, int y, out int quotient, out int remainder) {
    quotient = x / y;
    remainder = x % y;
  }

  static void OutUsage() {
    // 经过调用后产生两个输出参数, 一个quo, 一个rem, 而不需要return
    // ? 个人感觉不如return
    Divide(10, 3, out int quo, out int rem);
    Console.WriteLine($"quo: {quo}, rem: {rem}");
  }
}

class Entity {
  static int s_nextSerialNo;
  int _serialNo;

  // 构造函数
  public Entity() {
    // 实例成员可以访问静态成员, 但静态成员不能访问实例成员(本质上类似自动加了Entity.staticProp)
    _serialNo = s_nextSerialNo;
  }

  public int GetSerialNo() {
    // 可以简化this
    return _serialNo;
  }

  // 静态成员不能反向访问实例成员, 除非内部实例化
  public static int getNextSerialNo() {
    var x = new Entity();
    // 可以通过实例访问实例属性
    Console.WriteLine($"{x._serialNo}");
    return s_nextSerialNo;
  }

  public static void SetNextSerialNo(int newVal) {
    // 设置静态成员
    s_nextSerialNo = newVal;
  }
}

// 抽象类
public abstract class Expression {
  public abstract double Evaluate(Dictionary<string, object> vars);
}

// 派生
public class Constant : Expression {
  double _value;
  // 构造器
  public Constant(double value) {
    _value = value;
  }
  public override double Evaluate(Dictionary<string, object> vars) {
    return _value;
  }
}

public class VariableReference : Expression {
  string _name;
  public VariableReference(string name) {
    _name = name;
  }

  public override double Evaluate(Dictionary<string, object> vars) {
    object value = vars[_name] ?? throw new Exception($"Unknown variable: {_name}");
    return Convert.ToDouble(value);
  }
}

public class Operation : Expression {
  Expression _left;
  char _op;
  Expression _right;

  public Operation(Expression left, char op, Expression right) {
    _left = left;
    _op = op;
    _right = right;
  }

  public override double Evaluate(Dictionary<string, object> vars) {
    double x = _left.Evaluate(vars);
    double y = _right.Evaluate(vars);
    switch (_op) {
      case '+': return x + y;
      case '-': return x - y;
      case '*': return x * y;
      case '/': return x / y;
      default: throw new Exception("Unknown operation");
    }
  }
}

class UseExpression() {
  public static void main() {
    Expression e = new Operation(
    new VariableReference("x"),
    '*',
    new Operation(
        new VariableReference("y"),
        '+',
        new Constant(2)
    )
);
    Dictionary<string, object> vars = new();
    vars["x"] = 3;
    vars["y"] = 5;
    Console.WriteLine(e.Evaluate(vars)); // "21"
    vars["x"] = 1.5;
    vars["y"] = 9;
    Console.WriteLine(e.Evaluate(vars)); // "16.5"
  }
}

// 函数支持重载, 根据函数签名分发具体函数
class OverloadingExample {
  static void F() => Console.WriteLine("F()");
  static void F(object x) => Console.WriteLine("F(object)"); // 重载
  static void F(int x) => Console.WriteLine("F(int)");
  static void F(double x) => Console.WriteLine("F(double)");
  static void F<T>(T x) => Console.WriteLine($"F<T>(T), T is {typeof(T)}");
  static void F(double x, double y) => Console.WriteLine($"F(double{x}, double{y})");

  public static void useOverload() {
    F();
    F(1);
    F(1.0);
    F("abc");
    F((double)1);
    F((object)1);
    F<int>(1);
    F(1, 2);
  }
}
