using UnityEngine;

public class UICross : MonoBehaviour
{
    [SerializeField] private GameObject _fullCross;
    [SerializeField] private GameObject _emptyCross;

    [SerializeField] private bool isTrigger = false;

    void OnEnable()
    {
        UpdateCross();
    }

    void UpdateCross()
    {
        if (isTrigger)
        {
            _fullCross.SetActive(true);
            _emptyCross.SetActive(false);
        }
    }

    public void Trigger()
    {
        isTrigger = true;
        UpdateCross();
    }
}
