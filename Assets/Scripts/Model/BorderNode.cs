
public class BorderNode {

    public enum BorderInfo {
        OutOfBoundary,
        FreeSpace,
        Placement
    }

    public Coordinate position;
    public Coordinate.Direction direction;
    public BorderInfo borderInfo;
    public Placement placement;
    //public bool traveled = false;

    public BorderNode(Coordinate position, Coordinate.Direction direction, BorderInfo borderInfo, Placement placement) {
        this.position = position;
        this.direction = direction;
        this.borderInfo = borderInfo;
        this.placement = placement;
    }

}
