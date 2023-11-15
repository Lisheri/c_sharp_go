using System.Numerics;
using System.Runtime.CompilerServices;

namespace UseArray;

public class Main {

  public static async Task<int> Usage() {
    Console.WriteLine("C#数组");
    // 数组成员类型必须统一
    // 数组是引用类型, 声明数组只是为引用数组实例预留空间
    // ? new初始化长度以及所有成员, 数值类型默认值为0, 引用类型默认值为null
    int[] a = new int[10]; // 长度10
    for (int i = 0; i < a.Length; i++) {
      a[i] = i * i;
    }

    for (int i = 0; i < a.Length; i++) {
      Console.WriteLine($"a[{i}] = {a[i]}");
    }
    multipartArray();
    autoSendLen();
    delegation();
    Solution.ThreeSumClosest([-1,2,1,-4], 1);
    return await RetrieveDocsHomePage();
  }

  public static void multipartArray() {
    // 多维数组
    BigInteger[][] a = new BigInteger[3][];
    a[0] = new BigInteger[10];
    a[1] = new BigInteger[5];
    a[2] = new BigInteger[20];
    for (int i = 0; i < a.Length; i++) {
      for (int j = 0; j < a[i].Length; j++) {
        if (j == 0) {
          a[i][j] = 1;
        } else {
          a[i][j] = j * j * (j > 0 ? a[i][j - 1] : 1);
        }
        Console.WriteLine($"a[{i}][{j}] = {a[i][j]}");
      }
    }
  }

  public static void autoSendLen() {
    // 自动分配长度, 并且初始化为 [1, 2, 3];
    int[] a = { 1, 2, 3 };
    // 上述代码等同于 int[] a = new int[] { 1, 2, 3 };
    // foreach语句实现了IEnumerable<T>接口, 因此适用于任何集合
    foreach (int item in a) {
      Console.WriteLine($"[{item}]");
    }
  }

  // 委托和Lambda表达式
  public static void delegation() {
    DelegateExample.MainFunc();
  }

  public static async Task<int> RetrieveDocsHomePage() {
    var client = new HttpClient();
    Console.WriteLine($"{nameof(RetrieveDocsHomePage)}: start downloading.");
    byte[] content = await client.GetByteArrayAsync("https://learn.microsoft.com/");

    Console.WriteLine($"{nameof(RetrieveDocsHomePage)}: Finished downloading.");
    return content.Length;
  }
}

// 声明委托函数(类似闭包)
delegate double Function(double x);

class Multiplier {
  double _factor;
  public Multiplier(double factor) => _factor = factor;
  public double Multiply(double x) => x * _factor;

  static void Main() {
    Console.WriteLine("避免Main冲突");
  }
}

class DelegateExample {
  static double[] Apply(double[] a, Function f) {
    var result = new double[a.Length];
    for (int i = 0; i < a.Length; i++) result[i] = f(a[i]);
    return result;
  }

  public static void MainFunc() {
    double[] a = { 0.0, 0.5, 1.0 };
    double[] squares = Apply(a, (x) => x * x);
    foreach (double item in squares) {
      Console.WriteLine($"{item}");
    }
    double[] sines = Apply(a, Math.Sin);
    foreach (double item in sines) {
      Console.WriteLine($"{item}");
    }
    Multiplier m = new(2.0);
    double[] doubles = Apply(a, m.Multiply);
    foreach (double item in doubles) {
      Console.WriteLine($"{item}");
    }
  }
}

public class Solution {
    public int ThreeSumClosest(int[] nums, int target) {

    }
}
