using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelectionManager : MonoBehaviour
{
    public enum ObjectSelection
    {
        UNDEFINED,
        GMO_ORANGE,
        OG_ORANGE,
        GO_KART,
        TAPE
    }
    public PlayerSelectResults results;
    public bool shouldSetPlayer1 = false;
    public Vector3 player1Offset;
    public Vector3 player2Offset;
    public Button startButton;
    public GameObject gmoOrangeIcon;
    public GameObject ogOrangeIcon;
    public GameObject tapeIcon;
    public GameObject goKartIcon;
    public GameObject player1Selector;
    public GameObject player2Selector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        startButton.interactable = results.player1 != ObjectSelection.UNDEFINED && results.player2 != ObjectSelection.UNDEFINED;
    }

    public void SelectOGOrange()
    {
        if (ShouldUpdatePlayer1())
        {
            results.player1 = ObjectSelection.OG_ORANGE;
            player1Selector.gameObject.transform.position = ogOrangeIcon.gameObject.transform.position + player1Offset;
        }
        else
        {
            results.player2 = ObjectSelection.OG_ORANGE;
            player2Selector.gameObject.transform.position = ogOrangeIcon.gameObject.transform.position + player2Offset;
        }
    }

    public void SelectGMOOrange()
    {
        if (ShouldUpdatePlayer1())
        {
            results.player1 = ObjectSelection.GMO_ORANGE;
            player1Selector.gameObject.transform.position = gmoOrangeIcon.gameObject.transform.position + player1Offset;
        }
        else
        {
            results.player2 = ObjectSelection.GMO_ORANGE;
            player2Selector.gameObject.transform.position = gmoOrangeIcon.gameObject.transform.position + player2Offset;
        }
    }

    public void SelectKart()
    {
        if (ShouldUpdatePlayer1())
        {
            results.player1 = ObjectSelection.GO_KART;
            player1Selector.gameObject.transform.position = goKartIcon.gameObject.transform.position + player1Offset;
        }
        else
        {
            results.player2 = ObjectSelection.GO_KART;
            player2Selector.gameObject.transform.position = goKartIcon.gameObject.transform.position + player2Offset;
        }
    }

    public void SelectTape()
    {
        if (ShouldUpdatePlayer1())
        {
            results.player1 = ObjectSelection.TAPE;
            player1Selector.gameObject.transform.position = tapeIcon.gameObject.transform.position + player1Offset;
        }
        else
        {
            results.player2 = ObjectSelection.TAPE;
            player2Selector.gameObject.transform.position = tapeIcon.gameObject.transform.position + player2Offset;
        }
    }

    public bool ShouldUpdatePlayer1()
    {
        shouldSetPlayer1 = !shouldSetPlayer1;
        return shouldSetPlayer1;
    }
}
