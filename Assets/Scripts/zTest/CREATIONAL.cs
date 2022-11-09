using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CREATIONAL {
    public static void CREATE_PLACEMENT(Board board) {
        board.AddingPlacement(new Placement(new Coordinate(2, 2)));
        board.AddingPlacement(new Placement(new Coordinate(6, 1)));
        board.AddingPlacement(new Placement(new Coordinate(4, 2)));
        board.AddingPlacement(new Placement(new Coordinate(8, 8)));
        board.AddingPlacement(new Placement(new Coordinate(9, 9)));
    }
}
