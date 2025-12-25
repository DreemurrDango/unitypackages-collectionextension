# 更新日志

> 此文件记录了该软件包所有重要的变更
> 文件格式基于 [Keep a Changelog](http://keepachangelog.com/en/1.0.0/) 更新日志规范，且此项目版本号遵循 [语义化版本](http://semver.org/spec/v2.0.0.html) 规范

## [1.0.2] - 2025-12-25
### 新增
- **初始版本发布**: 提供了对 C# 集合类的扩展和补充，包含列表扩展、堆数据结构和双向字典
- **完备的说明文件**： 提供了详细的使用说明和示例代码，帮助快速理解
- **列表扩展 (`ListExtension`)**:
    - 添加了 `Shuffle<T>` 扩展方法，可使用 Fisher-Yates 算法对任何 `IList<T>` 集合进行原地洗牌
    - 支持传入可选的随机种子以实现可复现的乱序结果
- **堆数据结构 (`Heap`)**:
    - 提供了 `MinHeap<T>` (最小堆) 和 `MaxHeap<T>` (最大堆) 两种实现，常用于实现优先队列
    - 需要集合中的元素实现 `IHeapItem<T>` 接口，以支持堆内排序
- **双向字典 (`BiDictionary`)**:
    - 提供了 `BiDictionary<T1, T2>` 类，允许通过键获取值，或通过值获取键
    - 确保键和值都是唯一的，并提供了与标准字典类似的 `Add`, `Remove`, `Contains`, `TryGetValue` 等方法