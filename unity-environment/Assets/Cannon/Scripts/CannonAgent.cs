using UnityEngine;
using MLAgents;

public class CannonAgent : Agent
{
    [SerializeField] private Cannon cannon;
    [SerializeField] private CannonBall cannonBall;
    [SerializeField] private Target target;

    [HideInInspector] public int shots;
    [HideInInspector] public int hits;

    private Texture2D testSignal;
    private Color testCol1;
    private Color testCol2;

    public override void InitializeAgent()
    {
        cannonBall.RegisterCallback(OnHitTarget);

        // Custom texture demo.
        InitTestSignal();
    }

    public override void CollectObservations()
    {
        // Custom texture demo.
        UpdateTestSignal();
    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        switch (Mathf.FloorToInt(vectorAction[0]))
        {
            case 0:
                if (cannon.Shoot())
                {
                    AddReward(0.02f);
                    shots++;
                }
                else
                {
                    AddReward(-0.01f);
                }
                break;
            case 1:
                cannon.Move(-cannon.Speed);
                break;
            case 2:
                cannon.Move(cannon.Speed);
                break;
            case 3:
                // idle
                break;
            default:
                break;
        }
    }

    public override void AgentReset()
    {
        cannon.ResetPosition();
        target.SetActive(true);
        shots = 0;
        hits = 0;
    }

    public override void AgentOnDone()
    {
        target.SetActive(false);
    }

    private void OnHitTarget()
    {
        AddReward(1f);
        hits++;
    }

    private void InitTestSignal()
    {
        testSignal = TextureHelper.CreateTexture(96, 54);
        visualObservations.SetCustomTexture(this, "test", testSignal);
        testCol1 = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
        testCol2 = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f, 1f, 1f);
    }

    private void UpdateTestSignal()
    {
        testSignal.Erase();
        int h = testSignal.height / 2;
        for (int x = 0; x < testSignal.width; x++)
        {
            testSignal.SetPixel(x, h, testCol1);
            testSignal.SetPixel(x, h + (int)(Mathf.Sin(Time.time * 10f + x / 10f) * (h - 1)), testCol2);
        }
    }
}
