using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectSelectionManager;

public class PlayerSelectResults : MonoBehaviour
{

    public ObjectSelection player1;
    public ObjectSelection player2;

    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
