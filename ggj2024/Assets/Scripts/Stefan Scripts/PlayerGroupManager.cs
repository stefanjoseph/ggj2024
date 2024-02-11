using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectSelectionManager;

public class PlayerGroupManager : MonoBehaviour
{
    public bool shouldLookAtPlayer1Result;
    public PlayerSelectResults selectResults;
    public GameObject gmoOrange;
    public GameObject ogOrange;
    public GameObject kart;
    public GameObject tape;

    // Start is called before the first frame update
    void Start()
    {
        selectResults = GameObject.FindGameObjectWithTag("PlayerSelectResults").GetComponent<PlayerSelectResults>();

        ObjectSelection selection;

        if (shouldLookAtPlayer1Result)
        {
            selection = selectResults.player1;
        }
        else
        {
            selection = selectResults.player2;
        }

        gmoOrange.SetActive(selection == ObjectSelection.GMO_ORANGE);
        ogOrange.SetActive(selection == ObjectSelection.OG_ORANGE);
        kart.SetActive(selection == ObjectSelection.GO_KART);
        tape.SetActive(selection == ObjectSelection.TAPE);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
