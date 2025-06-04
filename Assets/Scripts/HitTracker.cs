using UnityEngine;

public class HitTracker : MonoBehaviour
{
    private int hitsSoFar;
    private int hitsNeeded;
    private GameObject resultPrefab;

    private AudioClip hitSFX;
    private AudioClip transformSFX;
    private AudioSource audioSource;

    public void Initialize(int requiredHits, GameObject prefab, AudioClip hitClip, AudioClip transformClip)
    {
        hitsNeeded = requiredHits;
        resultPrefab = prefab;
        hitSFX = hitClip;
        transformSFX = transformClip;

        hitsSoFar = 0;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
    }

    public void RegisterHit()
    {
        hitsSoFar++;

        if (hitSFX != null)
            audioSource.PlayOneShot(hitSFX);

        if (hitsSoFar >= hitsNeeded)
            TransformIntoResult();
    }

    private void TransformIntoResult()
    {
        if (transformSFX != null)
            AudioSource.PlayClipAtPoint(transformSFX, transform.position);

        Instantiate(resultPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
