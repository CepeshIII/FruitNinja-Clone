using Unity.VisualScripting;
using UnityEngine;

public class FruitParticleManager: MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Color _color;
    [SerializeField] private float zPosition = 0.5f;

    private void OnEnable()
    {
        _particleSystem = GameObject.FindGameObjectWithTag("MainFruitParticleSystem").GetComponent<ParticleSystem>();
    }

    public void CreateParticles(Vector3 position)
    {
        position.z = zPosition;
        var mainModule = _particleSystem.main;
        mainModule.startColor = _color;

        var shapeModule = _particleSystem.shape;
        shapeModule.position = position;

        _particleSystem.Play();
    }
}
