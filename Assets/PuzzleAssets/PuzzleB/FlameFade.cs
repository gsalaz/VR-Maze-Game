using UnityEngine;

public class FlameFade : MonoBehaviour
{
    // Reference to ParticleSystem component that controls fire emission
    [SerializeField] private ParticleSystem flame = null;

    // Reference to Light component that controls light emission (should be a Point Light)
    [SerializeField] private Light glow = null;

    // Light strength decrease per frame
    [SerializeField] private float fadeAmount = 0.0005f;

    // Modifiable reference to ParticleSystem main module
    private ParticleSystem.MainModule flameMain;

    // Initial particle start size
    private float initStartSize;

    // Initial Point Light range
    private float initRange;

    // Flag for indicating whether flame diminishing is in progress
    private bool fading;
    

    private void Start()
    {
        flameMain = flame.main;
        initStartSize = flameMain.startSize.constant;
        initRange = glow.range;
        fading = false;
    }


    private void Update()
    {
        if (fading)
        {
            flameMain.startSize = new ParticleSystem.MinMaxCurve(Mathf.Clamp(flameMain.startSize.constant - fadeAmount, 0f, initStartSize));
            glow.range = (flameMain.startSize.constant / initStartSize) * initRange;
            if (flameMain.startSize.constant <= 0)
                GetComponent<TorchController>().DestroySelf();
        }
    }


    public void BeginFade()
    {
        fading = true;
    }
}
