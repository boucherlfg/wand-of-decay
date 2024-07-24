using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float duration;
    private float counter;

    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > duration)
        {
            Destroy(gameObject);
        }
    }
}