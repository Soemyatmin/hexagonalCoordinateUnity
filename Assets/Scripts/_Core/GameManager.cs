
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager> {

    [HideInInspector]
    public BoardController boardController;
    [HideInInspector]
    public InputController input;

    public void NewGame() {
        input = GetComponent<InputController>();
        boardController = GetComponent<BoardController>().CreateBoard(10, 10);
    }

    public void Init() {
        input.Init();// nothing inside it
        boardController.Init();
        boardController.BoardDisplay();
    }


    #region old Java
    void move(string selectedPlacementKey) {
        Coordinate.Direction direction = input.requestDirection();
        Coordinate newPosition = Coordinate.moveDirection(boardController.GetBoard().PlacementList[selectedPlacementKey].position, direction);

        Debug.Log("new Position " + newPosition.x + ", " + newPosition.y);
        if (boardController.GetBoard().PlacementList[Coordinate.KeyGenerator(newPosition)] != null) {
            Debug.Log("Unable to move");
        } else {
            boardController.GetBoard().MovePlacement(selectedPlacementKey, newPosition);
        }
    }

    void shot(string selectedPlacementKey) {
        Coordinate.Direction direction = input.requestDirection();
        Placement placementAttacker = boardController.GetBoard().PlacementList[selectedPlacementKey];
        Coordinate attackerPosition = new Coordinate(placementAttacker.position.x, placementAttacker.position.y);
        if (placementAttacker.attackType == Placement.AttackType.Range) {
            for (int i = 0; i < placementAttacker.attackRange; i++) {
                Coordinate newPosition = Coordinate.moveDirection(attackerPosition, direction);
                Placement possibleHitee = boardController.GetBoard().FindPlacementByPosition(newPosition);
                if (possibleHitee != null) {
                    boardController.ShootingBullet(attackerPosition, direction, possibleHitee);
                    boardController.GetBoard().RemovingPlacement(Coordinate.KeyGenerator(newPosition)); // there may be some calculation for hit hitPoint and die condition
                    return; // if the bullet is keep going, don't break it
                }
                attackerPosition = newPosition;
            }
            boardController.ShootingBullet(attackerPosition, direction, null);
        } else if (placementAttacker.attackType == Placement.AttackType.Melee) { // melee hitee will fight back
            Coordinate newPosition = Coordinate.moveDirection(attackerPosition, direction);
            Placement possibleHitee = boardController.GetBoard().FindPlacementByPosition(newPosition);
            if (possibleHitee != null) {
                boardController.ShootingBullet(attackerPosition, direction, possibleHitee);
                boardController.GetBoard().PlacementList.Remove(Coordinate.KeyGenerator(newPosition));// there may be some calculation for hit hitPoint and die condition
                boardController.GetBoard().PlacementList.Remove(Coordinate.KeyGenerator(attackerPosition));
                return; // if the bullet is keep going, don't break it
            }
        }
    }
    //Hitee meant for a placement hit by the bullet or melee weapon.
    #endregion
}