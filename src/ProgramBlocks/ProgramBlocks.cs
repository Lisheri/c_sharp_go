namespace ProgramBlock;

public class ProgramBlock {
  public static void main() {
    Console.WriteLine("------------------- 程序构建基块 -------------------");
    Color color = new Color(10, 20, 30);
    Console.WriteLine($"({color.R}, {color.G}, {color.B}), red: ({Color.Red.R}, {Color.Red.G}, {Color.Red.B})");
    FuncObject obj = new FuncObject();
    Console.WriteLine($"{obj.ToString()}");
    Params.main();
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
class Params {
  public static void main() {

  }
}
