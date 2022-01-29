using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PostProcessingRuntimeEffects : MonoBehaviour
{
    public Volume vol;

    Bloom bloom;
    FilmGrain filmGrain;


    // Start is called before the first frame update
    void Start()
    {
        vol.profile.TryGet<Bloom>(out bloom);
        vol.profile.TryGet<FilmGrain>(out filmGrain);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
