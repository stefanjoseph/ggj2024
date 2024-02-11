using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAssigner : MonoBehaviour
{
    [SerializeField] private GameObject _player1Icon;
    private void Start()
    {
        var iconAssignment = GameObject.FindGameObjectWithTag("Player1Object");

        _player1Icon.transform.parent = iconAssignment.transform;
    }

}
