using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float lifetime;
    public float Lifetime { get => lifetime; set => lifetime = value; }

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(BulletExpire());
    }

    private IEnumerator BulletExpire()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
}
