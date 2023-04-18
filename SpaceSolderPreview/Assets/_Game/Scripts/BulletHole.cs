using UnityEngine;

public class BulletHole : MonoBehaviour
{
    [SerializeField] private float _maxBulletHoleLifeTime = 4;

    public BulletHolesPool pool;

    private void Start()
    {
        SetTimer();
    }

    private void Hide()
    {
        pool.Release(gameObject);
    }

    public void SetTimer()
    {
        Invoke("Hide", _maxBulletHoleLifeTime);
    }
}
