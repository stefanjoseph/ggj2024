using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroGameBehavior : MonoBehaviour
{
    //[SerializeField] private Animation _readyAnim;
    //[SerializeField] private Animation _startAnim;

    [SerializeField] private GameObject _readyTitle;
    [SerializeField] private GameObject _startTitle;
    [SerializeField] private GameObject _hud;
    // Start is called before the first frame update
    void Start()
    {
        //_readyAnim = GetComponent<Animation>();
        //_startAnim = GetComponent<Animation>();

        StartCoroutine(BeginGameRoutine());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator BeginGameRoutine()
    {
        _readyTitle.SetActive(true);

        yield return new WaitForSeconds(3f);
        _readyTitle.SetActive(false);
        _startTitle.SetActive(true);

        yield return new WaitForSeconds(3f);
        _startTitle.SetActive(false);
        _hud.SetActive(true);

        //Set bool to start the game


    }
}
