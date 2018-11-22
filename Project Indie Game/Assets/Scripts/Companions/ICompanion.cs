
using UnityEngine;

public interface ICompanion
{   
    void Throw(Vector3 dir);
    void Activate(GameObject other = null);  //some time the activation requires other objects that companion collides with.
    void CheckIfOutOfRange();
    void Reset();
    void Spawn();
}
