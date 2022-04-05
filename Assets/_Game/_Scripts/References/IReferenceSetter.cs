using UnityEngine;

public interface IReferenceSetter<T>
{
    void SetInstance(T newInstance);
}
