using UnityEngine;

public class HeatTracker : MonoBehaviour
{
    private float elapsed;
    private float requiredTime;
    private GameObject resultPrefab;

    public void Initialize(float timeToHeat, GameObject prefab)
    {
        requiredTime = timeToHeat;
        resultPrefab = prefab;
        elapsed = 0f;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= requiredTime)
            CompleteHeating();
    }

    private void CompleteHeating()
    {
        Instantiate(resultPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
