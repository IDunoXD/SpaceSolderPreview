using UnityEngine;

public class Shooter : MonoBehaviour
{
    private const float MAX_BULLET_HOLE_LIFE_TIME = 4;

    [SerializeField] private GameObject bulletHole;
    [SerializeField] private MeshRenderer flashMesh;
    [SerializeField] private float cooldown;
    [SerializeField] private float fleshTime;

    private float _startShootTime = 0;
    private bool _fireing;
	private RaycastHit _hit;
    private bool CanShoot => _fireing && _startShootTime + cooldown < Time.time;
    private bool HideFlashEffect => _startShootTime + fleshTime < Time.time;

    private void Update()
    {
        if(CanShoot)
        {
            Shoot();
        }
        if(HideFlashEffect)
        {
            flashMesh.enabled = false;
        }
    }

    private void Shoot()
    {
        _startShootTime = Time.time;
        flashMesh.enabled = true;
        if(Physics.Raycast(transform.position, transform.up, out _hit, Mathf.Infinity))
        {
            var hole = Instantiate(bulletHole, _hit.point, Quaternion.LookRotation(_hit.normal));
            Destroy(hole, MAX_BULLET_HOLE_LIFE_TIME);
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
