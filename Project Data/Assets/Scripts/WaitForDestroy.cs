using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyFog());
    }

    IEnumerator DestroyFog()
    {
        yield return new WaitForSeconds(15f);
        Destroy(gameObject);
    }
}
