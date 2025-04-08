using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;

public class FruitParticleManager: MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Color _color;

    private void OnEnable()
    {
        _particleSystem = GameObject.FindGameObjectWithTag("MainFruitParticleSystem").GetComponent<ParticleSystem>();
    }

    public void CreateParticles(Vector3 position)
    {
        var mainModule = _particleSystem.main;
        mainModule.startColor = _color;

        var shapeModule = _particleSystem.shape;
        shapeModule.position = position;

        _particleSystem.Play();
    }
}
