using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class Piece : MonoBehaviour {

    [SerializeField]
    private string name;

    [SerializeField]
    private int direction;

    private List<Coordinate> allowed_moves = new List<Coordinate>();

    void Start() {
        // Initialize valid moves
        switch (name) {
            case "Pawn":
                addPawnAllowedMoves();
                break;
            case "Tower":
                addTowerAllowedMoves();
                break;
            case "Horse":
                addHorseAllowedMoves();
                break;
            case "Bishop":
                addBishopAllowedMoves();
                break;
            case "King":
                addKingAllowedMoves();
                break;
        }
    }

    public void updatePosition() {

    }

    public bool checkValidMove(int coor_x, int coor_y) {
        for (int i = 0; i < allowed_moves.Count ; i++) {
            if (coor_x == allowed_moves[i].x && coor_y == allowed_moves[i].y) return true;
        }
        return false;
    }

    private void addAllowedMove(int coor_x, int coor_y) {
        Coordinate new_move = new Coordinate(coor_x, coor_y);
        allowed_moves.Add(new_move);
        // Debug.Log(name + ": " + coor_x + ',' + coor_y);
    }

    private void addPawnAllowedMoves() {
        addAllowedMove(0, 1);
        addAllowedMove(0, 2);
        addAllowedMove(1, 1);
        addAllowedMove(1, -1);
    }

    private void addTowerAllowedMoves() {
        for (int coor_x = 1; coor_x < 8; coor_x++) {
            addAllowedMove(coor_x, 0);
            addAllowedMove(0, coor_x);
        }
    }

    private void addBishopAllowedMoves() {
        for (int coor_x = 1; coor_x < 8; coor_x++) {
            addAllowedMove(coor_x, -coor_x);
        }
    }

    private void addHorseAllowedMoves() {
        for (int coor_x = 1; coor_x < 3; coor_x++) {
            for (int coor_y = 1; coor_y < 3; coor_y++) {
                if (coor_y != coor_x) {
                    addAllowedMove(coor_x, coor_y);
                    addAllowedMove(-coor_x, -coor_y);
                    addAllowedMove(coor_x, -coor_y);
                    addAllowedMove(-coor_x, coor_y);
                }
            }
        }
    }

    // @FIXME: King allowed positions algorithm
    // [0, 1], [1, 1], [1, 0], [1, -1], [0, -1], [-1, -1], [-1, 0], [-1, 1]
    private void addKingAllowedMoves() {
        // for (int coor_x = 0; coor_x < 2; coor_x++) {
        //     for (int coor_y = 0; coor_y < 2; coor_y++) {
        //         addAllowedMove(coor_x, coor_y);
        //         addAllowedMove(-coor_x, -coor_y);
        //         addAllowedMove(coor_x, -coor_y);
        //         addAllowedMove(-coor_x, coor_y);
        //     }
        // }
    }
}