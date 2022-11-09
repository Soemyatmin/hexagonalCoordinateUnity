using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Main : MonoBehaviour {
    void Start() {
        GameManager game = GameManager.Instance();
        game.NewGame();
        game.Init();
    }
}
