using System.Collections.Generic;

public class DictGrid<T> where T : class
{
    private readonly Dictionary<int, Dictionary<int, T>> dict = new();
    public T this[int x, int y]
    {
        get
        {
            if (!dict.ContainsKey(x)) return null;
            if (!dict[x].ContainsKey(y)) return null;
            return dict[x][y];
        }
        set
        {
            if (!dict.ContainsKey(x)) dict[x] = new();
            dict[x][y] = value;
        }
    }
}