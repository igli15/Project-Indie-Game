
using UnityEngine;

public interface ICompanion
{   
    void Throw(Vector3 dir);
    void Activate();
    void CheckIfOutOfRange();
    void Reset();
    void Spawn();
}
