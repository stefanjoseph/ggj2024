using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        TreddyObject treddy_obj = other.GetComponent<TreddyObject>();
        if (treddy_obj != null && !treddy_obj.needs_death) {
            treddy_obj.Fly();
        }
    }
}
