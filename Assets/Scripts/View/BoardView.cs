
using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardView : MonoBehaviour {

    //public GameObject groundBlockList;
    public GameObject prefGroundBlock;
    public Sprite prefHexBlockOriginal;
    public Sprite prefHexBlockGreen;
    public Sprite prefHexBlockRed;

    public Sprite prefCharacter;

    private readonly float spaceInbetweenX = 1.84f;
    private readonly float spaceInbetweenY = 1.57f;

    private float nextSpaceX = 0f;
    private float nextSpaceY = 0f;

    Dictionary<string, NodePref> m_nodePrefList;
    public Dictionary<string, NodePref> GetAllGenertedNodePref() {
        if (m_nodePrefList == null) {
            m_nodePrefList = new Dictionary<string, NodePref>();
            NodePref[] nodePrefs = GetComponentsInChildren<NodePref>();
            foreach (NodePref ele in nodePrefs) {
                this.m_nodePrefList[ele.key] = ele;
            }
        }
        return m_nodePrefList;


        //return GetComponentsInChildren<NodePref>();
    }

    // this function will not be using in real since this function do not show the Placement data
    public void displayMap(Board board) {
        for (int i = 0; i < board.GetHeight(); i++) {
            if (i % 2 != 0) {
                Debug.Log("   ");
            }
            Debug.Log("|");
            for (int j = 0; j < board.GetWidth(); j++) {
                Debug.Log(" " + i + "," + j + " ");
                Debug.Log("|");
            }
            Debug.Log("");
        }
    }

    // Compare to the Card Module of Side Mover Game, this project navigate top-left corner of this coordinate system is (0,0) whereas Side Mover Game save as bottom-left as (1,1)
    // the loop system changed, the Game. rearrange function changed
    public void Display(Board board) {
        Dictionary<string, Placement> boardItem = board.PlacementList;
        for (int loopY = 0; loopY < board.GetHeight(); loopY++) {
            if (loopY % 2 != 0) {
                nextSpaceX += spaceInbetweenX / 2;
            }
            for (int loopX = 0; loopX < board.GetWidth(); loopX++) {
                if (boardItem.Count == 0) {
                    CreateBlock(new Coordinate(loopX, loopY), new Vector3(nextSpaceX, nextSpaceY, 0));
                } else {
                    string Key = Coordinate.KeyGenerator(new Coordinate(loopX, loopY));
                    if (boardItem.ContainsKey(Key)) {
                        CreateBlock(new Coordinate(loopX, loopY), new Vector3(nextSpaceX, nextSpaceY, 0), boardItem[Key]);
                    } else {
                        CreateBlock(new Coordinate(loopX, loopY), new Vector3(nextSpaceX, nextSpaceY, 0));
                    }
                }
                nextSpaceX += spaceInbetweenX;
            }
            nextSpaceY += spaceInbetweenY;
            nextSpaceX = 0;
        }
    }

    public void shootingBullet(Coordinate bulletPosition, Coordinate.Direction bulletDirection, Placement hitPlacement) {
        if (hitPlacement == null) {
            Debug.Log("A bullet is shooting to the xx direction and nothing hit");
        } else {
            Debug.Log("A bullet is shooting to the xx direction and hit pp on yy");
        }
    }
    public void displayNewPosition(Coordinate coor) {
        Debug.Log("new Position " + coor.x + ", " + coor.y);
    }

    private void CreateBlock(Coordinate coor, Vector3 position, Placement character = null) {
        GameObject groundBlock = GameObject.Instantiate(prefGroundBlock, position, Quaternion.identity);
        groundBlock.transform.SetParent(transform);
        groundBlock.GetComponent<SpriteRenderer>().sprite = prefHexBlockOriginal;
        if (character != null) {
            GameObject characterBlock = new GameObject();
            characterBlock.name = "Character";
            characterBlock.transform.SetParent(groundBlock.transform);
            characterBlock.transform.position = position;
            characterBlock.AddComponent<SpriteRenderer>();
            characterBlock.GetComponent<SpriteRenderer>().sprite = prefCharacter;
            groundBlock.GetComponent<NodePref>().Init(coor, characterBlock, character);
        } else {
            groundBlock.GetComponent<NodePref>().Init(coor);
        }
    }
    private void CreateCharacter() {
        
    }

    #region new Unity
    //public Vector2 GetPhysicalPosition(Coordinate coor) { // not complete
    //    return new Vector2(0, 0);
    //}

    public void ShowSelectablePlacementHighlight() {
        Dictionary<string, NodePref> nodePrefs = GetAllGenertedNodePref();
        foreach (KeyValuePair<string, NodePref> ele in nodePrefs) {
            if (ele.Value.child != null) {
                ele.Value.ShowHighlight();
            }
        }
    }

    public void ShowExtraHighlight(Transform objectHit) {
        objectHit.GetComponent<NodePref>().ShowHighlightLevel2();
    }
    
    public void ShowMovablePositionHighlight(List<String> KeyList) {
        HideHighlight();
        Dictionary<string, NodePref> nodePrefs = GetAllGenertedNodePref();
        for (int i = 0; i < KeyList.Count; i++) {
            nodePrefs[KeyList[i]].ShowHighlight();
        }
    }

    private void HideHighlight() {
        Dictionary<string, NodePref> nodePrefs = GetAllGenertedNodePref();
        foreach (KeyValuePair<string, NodePref> ele in nodePrefs) {
            if (ele.Value.child != null) {
                ele.Value.RemoveHighlight();
            }
        }
    }
    #endregion
}
