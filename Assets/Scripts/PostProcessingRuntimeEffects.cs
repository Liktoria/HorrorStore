using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PostProcessingRuntimeEffects : MonoBehaviour
{
    public Volume vol;

    [SerializeField] private float bloomIntensity;
    [SerializeField] private float vignetteIntensity;

    private Bloom bloom;
    private FilmGrain filmGrain;
    private Vignette vignette;

    private bool lightIsOn;
    private GameManager gameManager;


    void Start()
    {
        vol.profile.TryGet<Bloom>(out bloom);
        vol.profile.TryGet<FilmGrain>(out filmGrain);
        vol.profile.TryGet<Vignette>(out vignette);
        gameManager = GameManager.gameManager;
        gameManager.LightSwitched += ToggleLight;
    }

    void Update()
    {

    }

    private void ToggleLight(bool lightOn, Room currentRoom)
    {
        lightIsOn = lightOn;

        if(lightOn)
        {
            bloom.intensity.value = bloomIntensity;
            filmGrain.intensity.value = 0f;
            vignette.intensity.value = 0.2f;
        }else if(!lightOn)
        {
            bloom.intensity.value = 0f;
            filmGrain.intensity.value = 1f;
            vignette.intensity.value = vignetteIntensity;
        }
    }
}
