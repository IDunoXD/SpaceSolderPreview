using UnityEngine;
using UnityEngine.Pool;

public class BulletHolesPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int defaultPoolSize = 10;
    [SerializeField] private int maxPoolSize = 20;
    [SerializeField] private bool collectionChecks = false;

    private ObjectPool<GameObject> _pool;

    void Start()
    {
        _pool = new ObjectPool<GameObject>(() => {
            var hole = Instantiate(prefab);
            hole.GetComponent<BulletHole>().pool = this;
            return hole;
        },  gameObject => {
            gameObject.SetActive(true);
        },  gameObject => {
            gameObject.SetActive(false);
        },  gameObject => {
            Destroy(gameObject);
        }, collectionChecks, defaultPoolSize, maxPoolSize);
    }

    public GameObject Get()
    {
        return _pool.Get();
    }

    public void Release(GameObject obj)
    {
        _pool.Release(obj);
    }
}
