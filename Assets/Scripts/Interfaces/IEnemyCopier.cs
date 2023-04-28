using UnityEngine;

public interface IEnemyCopier<T>
{
    public T CopyObject(T obj);
}
