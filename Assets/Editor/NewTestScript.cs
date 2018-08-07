using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

public class NewTestScript {

    [Test]
    public void TestGenerate()
    {
        Chess960 chess = new Chess960();
        List<Square> squares = getBoard();
        chess.GenerateSetUp(squares);
        foreach(var item in chess.GetPieceNames())
        {
            Assert.IsNotEmpty(chess.getStartForPiece(item, 1));
            Assert.IsNotEmpty(chess.getStartForPiece(item, -1));
        }
    }

    [Test]
    public void TestBishopsOnOppositeSquares()
    {
        Chess960 chess = new Chess960();
        List<Square> squares = getBoard();
        Piece piece = new Piece();
        piece.piece_name = "Bishop";
        piece.team = 1;
        chess.GenerateSetUp(squares);
        Assert.AreNotEqual(chess.GetStartPosition(piece).team, chess.GetStartPosition(piece).team);
    }

    [Test]
    public void TestDuplicatePieceReturnsDifferentSquares()
    {
        Chess960 chess = new Chess960();
        List<Square> squares = getBoard();
        Piece piece = new Piece();
        piece.piece_name = "Tower";
        piece.team = 1;
        chess.GenerateSetUp(squares);
        Assert.AreNotEqual(chess.GetStartPosition(piece), chess.GetStartPosition(piece));
    }

    [Test]
    public void TestKingBetweenBishops()
    {
        Chess960 chess = new Chess960();
        List<Square> squares = getBoard();
        Piece piece = new Piece();
        piece.piece_name = "Tower";
        piece.team = 1;
        Piece kingpiece = new Piece();
        kingpiece.piece_name = "King";
        kingpiece.team = 1;
        chess.GenerateSetUp(squares);
        int fTower = chess.GetStartPosition(piece).coor.x;
        int sTower = chess.GetStartPosition(piece).coor.x;
        int king = chess.GetStartPosition(kingpiece).coor.x;
        Assert.IsTrue((king < fTower && king > sTower) || (king > fTower && king < sTower));
    }

    [Test]
    public void TestRemoveOnGet()
    {
        Chess960 chess = new Chess960();
        List<Square> squares = getBoard();
        Piece piece = new Piece();
        piece.piece_name = "Tower";
        piece.team = 1;
        chess.GenerateSetUp(squares);
        Assert.IsFalse(chess.getStartForPiece("Tower", 1).Contains(chess.GetStartPosition(piece)));
    }

    private List<Square> getBoard()
    {
        List<Square> squares = new List<Square>();
        int coor_x = 0;
        int coor_y = 0;
        for (int k = 0; k < 64; k++)
        {
            GameObject square = new GameObject();
            square.AddComponent<MeshRenderer>();
            squares.Add(square.AddComponent<Square>());
            square.GetComponent<Square>().team = coor_x % 2 == 0 ? 1 : -1;
            square.GetComponent<Square>().coor = squares[k].coor = new Coordinate(coor_x, coor_y);
            squares[k].coor.pos = new Vector3(squares[k].transform.position.x - 0.5f, squares[k].transform.position.y, squares[k].transform.position.z - 0.5f);
            if (coor_y > 0 && coor_y % 7 == 0)
            {
                coor_x++;
                coor_y = 0;
            }
            else
            {
                coor_y++;
            }
        }
        return squares;
    }
}
