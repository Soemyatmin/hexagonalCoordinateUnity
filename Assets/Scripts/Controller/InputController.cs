
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputController : MonoBehaviour {
    private GameObject mainCanvas;
    public GameObject inputPanelPref;
    public GameObject inputPanelGO;

    private Button btnSelect;
    private Button btnActionAttack;
    private Button btnActionMove;

    private BoardController boardController;
    private GameObject boardView;

    private bool allowRayCast = false;
    private String selectedKey = "";

    public enum SelectionState {
        None,
        SelectBlankPosition,
        SelectPlacement
    }
    public SelectionState currentSelectionState;

    public enum Action {
        Attack,
        Move
    }
    //public Action[] actionArray = { Action.Attack, Action.Move };

    public void Init() {
        mainCanvas = GameObject.Find("MainCanvas");
        // inputPanelGO= GameObject.Instantiate(inputPanelPref);
        //inputPanelGO.transform.SetParent(mainCanvas.transform); // input canvas should be instantiate dynamically 
        boardView = GameObject.Find("BoardView");
        boardController = GetComponent<BoardController>();

        this.btnSelect = inputPanelGO.GetComponent<InputPanel>().btnSelect;
        this.btnActionMove = inputPanelGO.GetComponent<InputPanel>().btnActionMove;
        this.btnActionAttack = inputPanelGO.GetComponent<InputPanel>().btnActionAttack;

        btnSelect.onClick.AddListener(delegate {
            GameManager.Instance().boardController.ShowSelectablePlacement();
            currentSelectionState = SelectionState.SelectPlacement;
        });
        btnActionAttack.onClick.AddListener(delegate {
            OnClickDoAction(Action.Attack);
        });
        btnActionMove.onClick.AddListener(delegate {
            OnClickDoAction(Action.Move);

        });
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Selection_SelectPlacement();
        }
    }

    private void Selection_SelectPlacement() {
        RaycastHit2D[] hit2D = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        foreach (RaycastHit2D ele in hit2D) {
            if (!EventSystem.current.IsPointerOverGameObject()) {
                GameManager.Instance().boardController.SelectPlacementKey(ele.transform);
            }
        }
    }

    public void OnClickPlacementKeyFromView() { // show highlight to select

    }
    public void OnClickDoAction(Action action) {
        currentSelectionState = SelectionState.SelectBlankPosition;
        boardController.ShowDoActionPlacement(action);
    }

    public String SelectPlacementKeyFromView(Transform objectHit) {
        allowRayCast = false;
        NodePref[] nodePrefs = boardView.GetComponentsInChildren<NodePref>();
        for (int i = 0; i < nodePrefs.Length; i++) {
            if (nodePrefs[i].child != null) {
                nodePrefs[i].RemoveHighlight();
            }
        }
        return objectHit.GetComponent<NodePref>().key;
    }


    #region old java
    public string requestPlacementKey(Dictionary<string, Placement> placementList) {
        if (placementList.Count == 0) {
            return "";
        } else {
            try {
                Debug.Log("Enter Placement");
                foreach (KeyValuePair<string, Placement> ele in placementList) {
                    if (ele.Value != null) {
                        Debug.Log(ele.Key + " = " + ele.Value.symbol + ", " + ele.Value.name);
                    }
                }
                string key = "2,2";/*br.readLine(); */
                if (placementList[key] == null) {
                    throw new NullDataException("No data in placement");
                }
                return key;
            } catch (Exception) {
                Debug.Log("Other Error Occur, Unable to continue.");
                return requestPlacementKey(placementList);
            }
        }
    }

    public Coordinate.Direction requestDirection() {
        Debug.Log("Enter Direction");
        string direction = "E";
        //boardView.
        switch (direction) {
            case "E":
                return Coordinate.Direction.East;
            case "NE":
                return Coordinate.Direction.NorthEast;
            case "SE":
                return Coordinate.Direction.SouthEast;
            case "W":
                return Coordinate.Direction.West;
            case "NW":
                return Coordinate.Direction.NorthWest;
            case "SW":
                return Coordinate.Direction.SouthWest;
            default:
                Debug.Log("Invalid Direction");
                return requestDirection();
        }
    }

    //public Action RequestAction() { // this will no use
    //    Debug.Log("Enter Action 0 for Attack 1 for Move");
    //    int actionType = 0;
    //    try {
    //        string input = ";;;";/*br.readLine();*/
    //        actionType = int.Parse(input);
    //    } catch (Exception) { //todo Detail exception
    //        Debug.Log("Invalid Action");
    //        //return AequestAction();
    //    }
    //    return actionArray[actionType];
    //}

    #endregion
}
