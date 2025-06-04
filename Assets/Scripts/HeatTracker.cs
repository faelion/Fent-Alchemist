using UnityEngine;

public class HeatTracker : MonoBehaviour
{
    private float elapsed;
    private float requiredTime;
    private GameObject resultPrefab;

    private AudioSource roastSource;
    private AudioClip roastLoop;
    private AudioClip transformSFX;

    private bool initialized = false;

    public void Initialize(float timeToHeat, GameObject prefab, AudioClip roastClip, AudioClip transformClip)
    {
        requiredTime = timeToHeat;
        resultPrefab = prefab;
        roastLoop = roastClip;
        transformSFX = transformClip;
        elapsed = 0f;

        roastSource = GetComponent<AudioSource>();
        if (roastSource == null)
            roastSource = gameObject.AddComponent<AudioSource>();

        roastSource.clip = roastLoop;
        roastSource.loop = true;
        roastSource.spatialBlend = 1f;

        if (roastLoop != null)
            roastSource.Play();

        initialized = true;
    }

    void Update()
    {
        if (!initialized) return;

        elapsed += Time.deltaTime;
        if (elapsed >= requiredTime)
            CompleteHeating();
    }

    private void CompleteHeating()
    {
        if (roastSource != null && roastSource.isPlaying)
            roastSource.Stop();

        if (transformSFX != null)
            AudioSource.PlayClipAtPoint(transformSFX, transform.position);

        Instantiate(resultPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (roastSource != null && roastSource.isPlaying)
            roastSource.Stop();
    }
}
