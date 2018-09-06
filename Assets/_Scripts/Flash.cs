using UnityEngine;
using System.Collections;
 
public class Flash : MonoBehaviour
{
    public Light myLight;
    void Start()
    {
        StartCoroutine(Flasher());
    }
    
    IEnumerator Flasher()
    {
        while(true)
        {
            myLight.intensity = 2;
            yield return new WaitForSeconds(0.2f);
            myLight.intensity = 0;
            yield return new WaitForSeconds(0.25f);
        }
    }
    
}