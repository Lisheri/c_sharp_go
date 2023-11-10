namespace OtherFunctionMember;

public class Entry {
  public static void main() {
    MyList<string> list1 = new();
    MyList<string> list2 = new(10);
    EventExample.Usage();
  }
}

// 其他函数成员
class MyList<T> {
  // 泛型类, 用于实现对象的可扩充列表
  const int DefaultCapacity = 4;

  private T[] _items;
  int _count;

  bool _disposed;

  public MyList(int capacity = DefaultCapacity) {
    // 构造函数, 实例化时访问
    _items = new T[capacity];
  }

  public int Count => _count;

  public int Capacity {
    get => _items.Length;
    set {
      if (value < _count) value = _count;
      if (value != _items.Length) {
        T[] newItems = new T[value];
        Array.Copy(_items, 0, newItems, 0, _count);
        _items = newItems;
      }
    }
  }

  // 索引器, 利用索引器查找索引成员, 否则还需要 实例._items[index], 不过_items并非公有成员, 实例对象无法访问私有成员
  public T this[int index] {
    get => _items[index];
    set {
      if (!object.Equals(_items[index], value)) {
        _items[index] = value;
        OnChanged();
      }
    }
  }

  public void Add(T item) {
    if (_count == Capacity) Capacity = _count * 2;
    _items[_count] = item;
    _count++;
    OnChanged();
  }

  protected virtual void OnChanged() => Changed?.Invoke(this, EventArgs.Empty);

  public override bool Equals(object other) => Equals(this, other as MyList<T>);

  static bool Equals(MyList<T> a, MyList<T> b) {
    if (Object.ReferenceEquals(a, null)) return Object.ReferenceEquals(b, null);
    if (Object.ReferenceEquals(b, null) || a._count != b._count) return false;
    for (int i = 0; i < a._count; i++) {
      if (!object.Equals(a._items[i], b._items[i])) {
        return false;
      }
    }
    return true;
  }

  // 事件成员
  // ? 借助事件成员, 类可以提供通知。事件的声明方式与字段类似, 区别是事件声明包含 event 关键字, 且必须是委托类型
  // ? 在声明事件成员的类中，事件的行为与委托类型的字段完全相同（前提是事件不是抽象的，且不声明访问器）。
  // ? 字段存储对委托的引用，委托表示已添加到事件的事件处理程序。 如果没有任何事件处理程序，则字段为 null。
  // * 下面的 Changed事件由 onChanged 虚方法触发
  public event EventHandler Changed;

  public static bool operator ==(MyList<T> a, MyList<T> b) => Equals(a, b);
  public static bool operator !=(MyList<T> a, MyList<T> b) => !Equals(a, b);

  public void Dispose() {
    Console.WriteLine("ok");
    // 触发重写的 Dispose 虚方法
    Dispose(true);
    GC.SuppressFinalize(this);
  }

  protected virtual void Dispose(bool disposing) {
    // 重载 Dispose 方法
    // 指示方法调用是来自 Dispose方法(disposing: true)还是来自终结器(disposing: false)
    // 也就是说确定情况下调用为true, 所谓主动释放
    // + 可以释放如下对象:
    //  - 实现IDisposable托管对象
    //  - 占用大量内存或使用短缺资源的托管对象。将大型托管对象引用分配为null, 使其无法访问, 加快释放速度
    // 不确定的情况下调用为false, 执行被动释放
    Console.WriteLine($"Dispose, {_disposed}");
    if (_disposed) {
      return;
    }

    if (disposing) {
      // TODO: dispose managed state (managed objects).
    }

    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
    // TODO: set large fields to null.

    _disposed = true;
  }

  ~MyList() {
    // 析构函数
    // ? 不能主动触发, 只能被动触发
    Console.WriteLine("析构函数触发");
    Dispose(false);
  }
}

class EventExample {
  static int s_changeCount = 0;

  // 事件回调函数的 sender 其实就是Invoke的第一个参数, 这里的this代表的是 MyList<string> 实例, 第二个参数是 EventArgs.Empty 为空
  static void ListChanged(object sender, EventArgs e) {
    s_changeCount++;
  }

  public static void Usage() {
    MyList<string> names = new MyList<string>();
    // 注册一个 Changed事件
    names.Changed += new EventHandler(ListChanged);
    names.Add("Liz");
    names.Add("Martha");
    // 移除监听
    names.Changed -= new EventHandler(ListChanged);
    names.Add("Beth");
    Console.WriteLine(s_changeCount); // "2"
    // 回收
    names.Dispose();
    names = null;
  }
}
