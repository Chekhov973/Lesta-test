using System.Collections;
using UnityEngine;

public class DisapearPlatform : MonoBehaviour
{
    [SerializeField] private float _coolDown;
    [SerializeField] private Color _colorCharged;
    [SerializeField] private Color _colorPrepare;
    [SerializeField] private Color _colorCharging;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private BoxCollider _isTriggerCollider;
    private bool _isCharged;

    private void OnTriggerEnter(Collider other)
    {
        if (_isCharged)
        {
            if (other.TryGetComponent(out Character target))
                StartCoroutine(Prepare());
        }
    }

    private IEnumerator Prepare()
    {
        _isCharged = false;
        SetColor(_colorPrepare);

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(Disapear());
    }

    private IEnumerator Disapear()
    {
        _renderer.enabled = false;
        _boxCollider.enabled = false;
        _isTriggerCollider.enabled = false;

        yield return new WaitForSeconds(2f);

        _renderer.enabled = true;   
        _boxCollider.enabled=true;
        _isTriggerCollider.enabled=true;

        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {       
        SetColor(_colorCharging);

        yield return new WaitForSeconds(_coolDown);

        SetColor(_colorCharged);
        _isCharged = true;
    }

    private void SetColor(Color color)
    {
        _renderer.material.color = color;
    }

    private void Start()
    {
        _isCharged= true;
    }
}
