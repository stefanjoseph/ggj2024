using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAssigner : MonoBehaviour
{
    [SerializeField] private GameObject _player1Icon;
    private void Start()
    {
        AssignPlayer1Icon();
    }

    public void AssignPlayer1Icon()
    {
        var iconAssignment = GameObject.FindGameObjectWithTag("Player1Object");

        _player1Icon.transform.parent = iconAssignment.transform;
    }

}
