using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferZone : MonoBehaviour
{
    public bool boost;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        TreddyObject treddy_obj = other.GetComponent<TreddyObject>();
        if (treddy_obj != null && !treddy_obj.needs_death) {
            if (boost) {
                treddy_obj.ApplyBoost();
            } else {
                treddy_obj.ApplyDrag();
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        TreddyObject treddy_obj = other.GetComponent<TreddyObject>();
        if (treddy_obj != null && !treddy_obj.needs_death) {
            treddy_obj.SettleDown();
        }
    }
}
