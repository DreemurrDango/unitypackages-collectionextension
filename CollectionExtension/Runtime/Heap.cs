using System;
using System.Collections;
using System.Collections.Generic;

namespace DreemurrStudio.CollectionExtension
{
    /// <summary>
    /// 可堆排序的元素接口
    /// </summary>
    /// <typeparam name="T">堆排序的排序值类型</typeparam>
    public interface IHeapItem<T> : IComparable<T>
    {
        /// <summary>
        /// 获取或设置元素在堆中的索引
        /// </summary>
        public int HeapIndex
        {
            get;
            set;
        }
    }

    /// <summary>
    /// 用于堆排序的二叉堆抽象数据结构
    /// </summary>
    /// <typeparam name="T">要排序的可比较元素类</typeparam>
    public abstract class Heap<T> : IEnumerable<T> where T : IHeapItem<T>
    {
        /// <summary>
        /// 堆排序元素数组
        /// </summary>
        protected T[] items;
        /// <summary>
        /// 当前堆中元素的数量
        /// </summary>
        protected int currentItemCount;

        /// <summary>
        /// 获取堆中元素的数量
        /// </summary>
        public int Count => currentItemCount;

        public Heap(int maxHeapSize)
        {
            items = new T[maxHeapSize];
        }

        public T this[int index] => items[index];

        /// <summary>
        /// 返回是否包含指定的元素
        /// </summary>
        /// <param name="item">要查找的元素</param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            if (item.HeapIndex < 0 || item.HeapIndex >= currentItemCount) return false;
            return Equals(items[item.HeapIndex], item);
        }

        /// <summary>
        /// 向堆中添加元素
        /// </summary>
        /// <param name="item">要添加的元素</param>
        public void Push(T item)
        {
            item.HeapIndex = currentItemCount;
            items[currentItemCount] = item;
            ShiftUp(item);
            currentItemCount++;
        }

        /// <summary>
        /// 取出堆顶元素
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            T firstItem = items[0];
            currentItemCount--;
            items[0] = items[currentItemCount];
            items[0].HeapIndex = 0;
            ShiftDown(items[0]);
            return firstItem;
        }

        /// <summary>
        /// 更新堆中元素的位置
        /// </summary>
        /// <param name="item">要更新的元素</param>
        public void UpdateItem(T item) => ShiftUp(item);

        /// <summary>
        /// 向上调整堆中元素的位置
        /// </summary>
        /// <param name="item"></param>
        protected abstract void ShiftUp(T item);

        /// <summary>
        /// 向下调整堆中元素的位置
        /// </summary>
        /// <param name="item"></param>
        protected abstract void ShiftDown(T item);

        /// <summary>
        /// 交换堆中两个元素的位置
        /// </summary>
        protected void Swap(T itemA, T itemB)
        {
            items[itemA.HeapIndex] = itemB;
            items[itemB.HeapIndex] = itemA;
            int itemAIndex = itemA.HeapIndex;
            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAIndex;
        }

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator() as IEnumerator<T>;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    /// <summary>
    /// 最小二叉堆：堆顶元素为最小值
    /// 常用于优先队列实现，可比较值越小优先级越高
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MinHeap<T> : Heap<T> where T : IHeapItem<T>
    {
        public MinHeap(int maxHeapSize) : base(maxHeapSize) { }

        protected override void ShiftUp(T item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;
            while (true)
            {
                T parentItem = items[parentIndex];
                if (item.CompareTo(parentItem) < 0) Swap(item, parentItem);
                else break;
                parentIndex = (item.HeapIndex - 1) / 2;
            }
        }
        protected override void ShiftDown(T item)
        {
            while (true)
            {
                int leftChildIndex = item.HeapIndex * 2 + 1;
                int rightChildIndex = item.HeapIndex * 2 + 2;
                int swapIndex = 0;
                if (leftChildIndex < Count)
                {
                    swapIndex = leftChildIndex;
                    if (rightChildIndex < Count)
                    {
                        if (items[rightChildIndex].CompareTo(items[leftChildIndex]) < 0)
                            swapIndex = rightChildIndex;
                    }
                    if (items[swapIndex].CompareTo(item) < 0)
                        Swap(item, items[swapIndex]);
                    else return;
                }
                else return;
            }
        }
    }

    /// <summary>
    /// 最大二叉堆：堆顶元素为最大值，可比较值越大优先级越高
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MaxHeap<T> : Heap<T> where T : IHeapItem<T>
    {
        public MaxHeap(int maxHeapSize) : base(maxHeapSize) { }
        protected override void ShiftUp(T item)
        {
            int parentIndex = (item.HeapIndex - 1) / 2;
            while (true)
            {
                T parentItem = items[parentIndex];
                if (item.CompareTo(parentItem) > 0) Swap(item, parentItem);
                else break;
                parentIndex = (item.HeapIndex - 1) / 2;
            }
        }
        protected override void ShiftDown(T item)
        {
            while (true)
            {
                int leftChildIndex = item.HeapIndex * 2 + 1;
                int rightChildIndex = item.HeapIndex * 2 + 2;
                int swapIndex = 0;
                if (leftChildIndex < Count)
                {
                    swapIndex = leftChildIndex;
                    if (rightChildIndex < Count)
                    {
                        if (items[rightChildIndex].CompareTo(items[leftChildIndex]) > 0)
                            swapIndex = rightChildIndex;
                    }
                    if (items[swapIndex].CompareTo(item) > 0)
                        Swap(item, items[swapIndex]);
                    else return;
                }
                else return;
            }
        }
    }
}