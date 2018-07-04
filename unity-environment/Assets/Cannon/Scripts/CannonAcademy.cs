using UnityEngine;
using MLAgents;

public class CannonAcademy : Academy 
{
    public override void InitializeAcademy()
    {
        Physics.gravity = new Vector3(0f, -4f, 0f);
    }

    public override void AcademyReset()
    {
    }

    public override void AcademyStep()
    {
    }
}
