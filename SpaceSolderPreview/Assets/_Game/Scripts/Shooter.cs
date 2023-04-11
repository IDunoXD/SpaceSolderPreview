using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletHole;
    [SerializeField] private Button shootButton;
    [SerializeField] private float cooldown;
    [SerializeField] private float fleshTime;

    private MeshRenderer _meshRenderer;
    private float _startShootTime;
    private bool _fireing;
	private RaycastHit _hit;

	private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if(_fireing)
        {
            Shoot();
        }
        if(_startShootTime + fleshTime < Time.time)
        {
            _meshRenderer.enabled = false;
        }
    }

    private void Shoot()
    {
        if(_startShootTime + cooldown < Time.time)
        {
            _startShootTime = Time.time;
            _meshRenderer.enabled = true;
            if(Physics.Raycast(transform.position, transform.forward, out _hit, Mathf.Infinity))
            {
                Instantiate(bulletHole, _hit.point, Quaternion.LookRotation(_hit.normal));
            }
        }
    }

    public void Press()
    {
        _fireing = true;
    }

    public void Release()
    {
        _fireing = false;
    }
}
