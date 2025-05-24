using UnityEngine;

public class HitTracker : MonoBehaviour
{
    private int hitsSoFar;
    private int hitsNeeded;
    private GameObject resultPrefab;
    public void Initialize(int requiredHits, GameObject prefab)
    {
        hitsNeeded = requiredHits;
        resultPrefab = prefab;
        hitsSoFar = 0;
    }
    public void RegisterHit()
    {
        hitsSoFar++;
        if (hitsSoFar >= hitsNeeded)
            TransformIntoResult();
    }

    private void TransformIntoResult()
    {
        Instantiate(resultPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
