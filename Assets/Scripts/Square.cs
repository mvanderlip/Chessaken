﻿using UnityEngine;
using System.Collections;

public class Square : MonoBehaviour {
    private Coordinate position;

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Piece") {
            Debug.Log("object message");
            col.GetComponent<Piece>().updatePosition();
        }
    }
}
