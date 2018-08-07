using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chess960 {
    private string[] piece_names = { "Tower", "Horse", "Bishop", "Queen", "King" };
    private enum TeamColor
    {
        WHITE = -1,
        BLACK = 1
    }

    private Dictionary<string, Dictionary<int, List<Square>>> startStates;
    private List<Square> startSquares;
    private int whiteRow = 7;
    private int blackRow = 0;

    public List<Square> getStartForPiece(string piece_name, int team)
    {
        return startStates[piece_name][team];
    }

    public string[] GetPieceNames()
    {
        return piece_names;
    }

    public Chess960()
    {
        startStates = new Dictionary<string, Dictionary<int, List<Square>>>();
    }

    public void GenerateSetUp(List<Square> squares)
    {
        for (int k = 0; k < piece_names.Length; k++)
        {
            startStates.Add(piece_names[k], new Dictionary<int, List<Square>>());
        }
        foreach (var piecename in startStates)
        {
            piecename.Value.Add((int)TeamColor.WHITE, new List<Square>());
            piecename.Value.Add((int)TeamColor.BLACK, new List<Square>());
        }
        //TODO: Make it so that a 960 board is generated before called
        startSquares = squares.FindAll(square => square.coor.y == whiteRow || square.coor.y == blackRow);
        List<int> possiblePositions = new List<int>();
        for (int k = 0; k < 8; k++)
        {
            possiblePositions.Add(k);
        }
        int kingCoor = AddKing();
        possiblePositions.Remove(kingCoor);
        possiblePositions.Remove(AddTower(kingCoor, true));
        possiblePositions.Remove(AddTower(kingCoor, false));
        possiblePositions.Remove(AddBishop(possiblePositions, true));
        possiblePositions.Remove(AddBishop(possiblePositions, false));
        possiblePositions.Remove(AddRandom("Horse", possiblePositions));
        possiblePositions.Remove(AddRandom("Horse", possiblePositions));
        possiblePositions.Remove(AddRandom("Queen", possiblePositions));
    }

    private void AddStartPosition(string piecename, int position)
    {
        startStates[piecename][(int)TeamColor.WHITE].Add(startSquares.Find(square => square.coor.x == position && square.coor.y == whiteRow));
        startStates[piecename][(int)TeamColor.BLACK].Add(startSquares.Find(square => square.coor.x == position && square.coor.y == blackRow));
    }

    private int AddKing()
    {
        int kingCoor = Random.Range(1, 7);
        AddStartPosition("King", kingCoor);
        return kingCoor;
    }

    private int AddTower(int kingCoor, bool left)
    {
        int towerCoor = left ? Random.Range(kingCoor + 1, 8) : Random.Range(0, kingCoor);
        AddStartPosition("Tower", towerCoor);
        return towerCoor;
    }

    private int AddBishop(List<int> possiblePositions, bool first)
    {
        int bishop = possiblePositions[Random.Range(0, possiblePositions.Count)];
        if (!first)
        {
            List<Square> secondBishopSquares = startSquares.FindAll(
                square => startStates["Bishop"][(int)TeamColor.WHITE][0].team != square.team
                       && possiblePositions.Contains(square.coor.x)
                       && square.coor.y == whiteRow
            );
            bishop = secondBishopSquares[Random.Range(0, secondBishopSquares.Count)].coor.x;
        }
        AddStartPosition("Bishop", bishop);
        return bishop;
    }

    private int AddRandom(string piece_name, List<int> possiblePositions)
    {
        int piece = possiblePositions[Random.Range(0, possiblePositions.Count)];
        AddStartPosition(piece_name, piece);
        return piece;
    }

    public Square GetStartPosition(Piece piece)
    {
        Square startSquare = null;
        if(startStates[piece.piece_name][piece.team].Count != 0)
        {
            startSquare = startStates[piece.piece_name][piece.team][0];
            startStates[piece.piece_name][piece.team].RemoveAt(0);
        }
        return startSquare;
    }
}
