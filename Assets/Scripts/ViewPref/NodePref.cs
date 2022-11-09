using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodePref : MonoBehaviour {

    public Coordinate coor;
    public GameObject childPlacement;
    public Placement child;
    public string key;

    private GameObject highlighter;
    private SpriteRenderer highlighterSpriteRenderer;
    public void Init(Coordinate coor, GameObject childPlacement = null, Placement child = null) {
        highlighter = transform.GetChild(0).gameObject;
        highlighter.SetActive(false);
        highlighterSpriteRenderer = highlighter.GetComponent<SpriteRenderer>();

        this.coor = coor;
        this.childPlacement = childPlacement;
        this.child = child;
        this.key = Coordinate.KeyGenerator(coor);
    }
    public void ShowHighlight() {
        highlighter.SetActive(true);
        highlighterSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
    }
    public void ShowHighlightLevel2() {
        highlighterSpriteRenderer.color = Color.white;
    }
    public void RemoveHighlight() {
        highlighter.SetActive(false);
    }

}
