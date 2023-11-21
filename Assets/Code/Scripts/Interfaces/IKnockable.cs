using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockable
{
    void Knock(float knockForce, Vector2 knockDirection);
    IEnumerator ResetKnock();
}
