using UnityEngine;
using System.Collections;

public class Flash : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(Flasher());
    }

    IEnumerator Flasher()
    {
        while(true)
        {
            GetComponent<Light>().intensity = 2;
            yield return new WaitForSeconds(0.2f);
            GetComponent<Light>().intensity = 0;
            yield return new WaitForSeconds(0.25f);
        }
    }

}
