
using System.Collections.Generic;

public class Board {
    private int Width;
    private int Height;
    public Dictionary<string, Placement> PlacementList;

    public Board(int width, int height) {
        this.Width = width;
        this.Height = height;
        PlacementList = new Dictionary<string, Placement>();
        CREATIONAL.CREATE_PLACEMENT(this);
    }

    public int GetWidth() {
        return Width;
    }
    public int GetHeight() {
        return Height;
    }
    public bool IsInBoard(int x, int y) {
        return (x > 0 && x < Width && y > 0 && y < Height);
    }

    #region Placement Functions
    public Placement GetIPlacement(string i) {
        return PlacementList[i];
    }
    public void AddingPlacement(Placement placement) {
        string key = Coordinate.KeyGenerator(placement.position);
        PlacementList.Add(key, placement);
    }
    public void RemovingPlacement(string key) {
        PlacementList.Remove(key);
    }
    public Placement FindPlacementByPosition(Coordinate position) {
        string key = Coordinate.KeyGenerator(position);
        Placement placement;
        try {
            placement = PlacementList[key];
        } catch (KeyNotFoundException) {
            placement = null;
        }
        return placement;
    }
    public void MovePlacement(string key, Coordinate newPosition) {
        PlacementList[key].position = new Coordinate(newPosition.x, newPosition.y);
        UpdateByPlacementKey(key);
    }
    // everytime a placement moved, the position and key will no longer the same, so this function will fix the problem.
    private void UpdateByPlacementKey(string key) {
        Placement newPlacement = PlacementList[key];
        PlacementList.Remove(key);
        string newKey = Coordinate.KeyGenerator(newPlacement.position);
        PlacementList.Add(newKey, newPlacement);
    }
    #endregion

}

//Board may have other land data like tree and hill, that may require to create by Board build immutable design pattern