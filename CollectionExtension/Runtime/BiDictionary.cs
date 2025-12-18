using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 双向字典，允许通过键获取值，也允许通过值获取键，其中键和值均为唯一
/// 注意：双向字典的两个类型不应该是相同的类型，以避免歧义
/// </summary>
public class BiDictionary<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>
{
    /// <summary>
    /// 正向字典
    /// </summary>
    private readonly Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
    /// <summary>
    /// 反向字典
    /// </summary>
    private readonly Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

    /// <summary>
    /// 获取双向字典中的键值对数量
    /// </summary>
    public int Count => _forward.Count;

    /// <summary>
    /// 正向获取值
    /// </summary>
    /// <param name="key">正向键值</param>
    /// <returns>键对应的值</returns>
    public T2 this[T1 key]
    {
        get => _forward[key];
        set
        {
            if (_forward.TryGetValue(key,out T2 oldValue))
                _reverse.Remove(oldValue);
            _forward[key] = value;
            _reverse[value] = key;
        }
    }

    /// <summary>
    /// 反向获取键
    /// </summary>
    /// <param name="key2"></param>
    /// <returns></returns>
    public T1 this[T2 key2]
    {
        get => _reverse[key2];
        set
        {
            if (_reverse.TryGetValue(key2, out T1 oldKey))
                _forward.Remove(oldKey);
            _reverse[key2] = value;
            _forward[value] = key2;
        }
    }

    /// <summary>
    /// 添加新的键值对（键与值均应当唯一）
    /// </summary>
    /// <param name="key">要添加的键</param>
    /// <param name="value">要添加的值</param>
    /// <exception cref="System.ArgumentException"></exception>
    public void Add(T1 key, T2 value)
    {
        if(_forward.ContainsKey(key) || _reverse.ContainsKey(value))
            throw new System.ArgumentException($"双向键值 {key}:{value} 已存在于双向字典中");
        _forward.Add(key, value);
        _reverse.Add(value, key);
    }

    /// <summary>
    /// 移除指定键对应的键值对
    /// </summary>
    /// <param name="key"></param>
    public void Remove(T1 key)
    {
        if (_forward.TryGetValue(key, out T2 value))
        {
            _forward.Remove(key);
            _reverse.Remove(value);
        }
    }

    /// <summary>
    /// 移除指定值对应的键值对
    /// </summary>
    /// <param name="value"></param>
    public void Remove(T2 value)
    {
        if (_reverse.TryGetValue(value, out T1 key))
        {
            _reverse.Remove(value);
            _forward.Remove(key);
        }
    }

    public bool Contains(T1 key) => _forward.ContainsKey(key);
    public bool Contains(T2 value) => _reverse.ContainsKey(value);
    public bool TryGetValue(T1 key, out T2 value) => _forward.TryGetValue(key, out value);
    public bool TryGetValue(T2 value, out T1 key) => _reverse.TryGetValue(value, out key);

    /// <summary>
    /// 移除双向字典中的所有键值对
    /// </summary>
    public void Clear()
    {
        _forward.Clear();
        _reverse.Clear();
    }

    /// <summary>
    /// 返回一个枚举器，按添加顺序遍历双向字典中的键值对
    /// </summary>
    /// <returns></returns>
    public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator() => _forward.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
