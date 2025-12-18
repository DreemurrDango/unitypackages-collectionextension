using System.Collections;
using System.Collections.Generic;
using System;

public static class ListExtension
{
    /// <summary>
    /// 对 IList 列表进行原地洗牌乱序。
    /// 使用 Fisher-Yates 算法。
    /// </summary>
    /// <typeparam name="T">列表元素的类型。</typeparam>
    /// <param name="list">要进行洗牌的列表。</param>
    /// <param name="seed">可选的随机数生成器种子。如果提供，将使用该种子初始化随机数生成器，以确保可重复的洗牌结果</param>
    public static void Shuffle<T>(this IList<T> list,int? seed = null)
    {
        int n = list.Count;
        var rng = seed.HasValue? new Random(seed.Value) : new Random();
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            // 交换 list[k] 和 list[n]
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
