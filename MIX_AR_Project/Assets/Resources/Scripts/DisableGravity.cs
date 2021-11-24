using UnityEngine;
using Vuforia;

public class DisableGravity : DefaultObserverEventHandler
{
    public Rigidbody car;

    override protected void OnTrackingLost()
    {
        car.useGravity = false;
    }
    override protected void OnTrackingFound()
    {
        car.useGravity = true;
    }
}