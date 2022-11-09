using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardController : MonoBehaviour {
    private Board m_board;
    private BoardView m_boardView;

    public BoardController CreateBoard(int width, int height) {
        this.m_board = new Board(width, height);
        return this;
    }

    public void Init() {
        m_boardView = GameObject.Find("BoardView").GetComponent<BoardView>();
    }

    public Board GetBoard() {
        return m_board;
    }

    public bool PositionInBoard(Coordinate coor) {
        return m_board.IsInBoard(coor.x, coor.y);
    }

    public void ShootingBullet(Coordinate bulletPosition, Coordinate.Direction bulletDirection, Placement hitPlacement) {
        m_boardView.shootingBullet(bulletPosition, bulletDirection, hitPlacement);
    }

    #region Controlling View
    private string CurrentSelectedPlacementKey;
    private InputController.Action CurrentSelectedAction;

    public void BoardDisplay() {
        m_boardView.Display(m_board);
    }
    public void ShowSelectablePlacement() { //player ဆီမှာရှိတဲ့ Placement တွေအကုန်လင်းမယ် 
        m_boardView.ShowSelectablePlacementHighlight();
    }

    public void SelectPlacementKey(Transform objectHit) { //input က select လိုက်တဲ့ Placement ကိုခဏသိမ်းထားမယ် 
        if (objectHit.HasComponent<NodePref>()) {
            m_boardView.ShowExtraHighlight(objectHit);
            CurrentSelectedPlacementKey = objectHit.GetComponent<NodePref>().key;
        }
           
    }
    public void ShowDoActionPlacement(InputController.Action Action) { //သတ်မှတ်ထားတဲ့ Player သွားလို့ရတဲ့ Block တွေအကုန်လင်းမယ် 
        CurrentSelectedAction = Action;
        m_boardView.ShowSelectablePlacementHighlight();
        Dictionary<string, BorderNode> freePosition = MovablePosition(m_board.PlacementList[CurrentSelectedPlacementKey].position);
        m_boardView.ShowMovablePositionHighlight(freePosition.Keys.ToList<string>());
    }
    #endregion

    #region private find positions
    private Dictionary<string, BorderNode> MovablePosition(Coordinate position) {
        Dictionary<string, BorderNode> closestFreePositionList = new Dictionary<string, BorderNode>();
        closestFreePositionList.Merge(BorderInfo(position));
        foreach (KeyValuePair<string, BorderNode> ele in closestFreePositionList) {
            if (ele.Value.borderInfo != BorderNode.BorderInfo.FreeSpace) {
                closestFreePositionList.Remove(ele.Key);
            }
        }
        return closestFreePositionList;
    }

    private Dictionary<string, BorderNode> Exploration(Coordinate position, int layer) {
        int layerCounter = layer;
        Dictionary<string, BorderNode> boundary = BorderInfo(position);
        layerCounter--;
        while (layerCounter > 0) {
            Dictionary<string, BorderNode> anotherBoundary = new Dictionary<string, BorderNode>();
            foreach (KeyValuePair<string, BorderNode> ele in boundary) {
                anotherBoundary.Merge(BorderInfo(ele.Value.position));
            }
            boundary.Merge(anotherBoundary);
            layerCounter--;
        }
        string key = Coordinate.KeyGenerator(position);
        boundary.Remove(key);
        return boundary;
    }

    private Dictionary<string, BorderNode> BorderInfo(Coordinate position) {
        Dictionary<string, BorderNode> boundary = new Dictionary<string, BorderNode>();
        for (int i = 0; i < Coordinate.DIRECTION_LIST.Length; i++) {
            Coordinate newPosition = Coordinate.moveDirection(position, Coordinate.DIRECTION_LIST[i]);
            string key = Coordinate.KeyGenerator(newPosition);
            if (!PositionInBoard(newPosition)) {
                boundary.Add(key, new BorderNode(newPosition, Coordinate.DIRECTION_LIST[i], BorderNode.BorderInfo.OutOfBoundary, null));
            } else {
                Placement positionPlace = m_board.FindPlacementByPosition(newPosition);
                if (positionPlace != null) {
                    boundary.Add(key, new BorderNode(newPosition, Coordinate.DIRECTION_LIST[i], BorderNode.BorderInfo.Placement, positionPlace));
                } else {
                    boundary.Add(key, new BorderNode(newPosition, Coordinate.DIRECTION_LIST[i], BorderNode.BorderInfo.FreeSpace, null));
                }
            }
        }
        return boundary;
    }
    #endregion
}
