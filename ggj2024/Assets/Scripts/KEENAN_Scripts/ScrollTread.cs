using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTread : MonoBehaviour
{
    // Start is called before the first frame update
    private Renderer myRenderer;
    public Vector2 scrollAmount;
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        myRenderer.material.mainTextureOffset = scrollAmount * Time.time;
    }
}
