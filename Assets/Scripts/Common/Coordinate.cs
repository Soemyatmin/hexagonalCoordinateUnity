
// in this Hexagonal Coordinate we will use "odd-r" for display.
// this board is starting from (0,0)
public class Coordinate {
    public int x;
    public int y;

    public enum Direction {
        East,
        NorthEast,
        SouthEast,
        West,
        NorthWest,
        SouthWest
    }

    public static Direction[] DIRECTION_LIST = {Direction.East, Direction.NorthEast, Direction.SouthEast,
            Direction.West, Direction.NorthWest, Direction.SouthWest};


    public Coordinate(int x, int y) {
        this.x = x;
        this.y = y;
    }

    // this move direction is only for "odd-r" display with pointy orientation
    public static Coordinate moveDirection(Coordinate position, Direction direction) {
        Coordinate newPosition = new Coordinate(position.x, position.y);

        if (newPosition.y % 2 == 0) {
            switch (direction) {
                case Direction.East:
                    newPosition.x++;
                    break;
                case Direction.NorthEast:
                    newPosition.y--;
                    break;
                case Direction.SouthEast:
                    newPosition.y++;
                    break;
                case Direction.West:
                    newPosition.x--;
                    break;
                case Direction.NorthWest:
                    newPosition.x--;
                    newPosition.y--;
                    break;
                case Direction.SouthWest:
                    newPosition.x--;
                    newPosition.y++;
                    break;
            }
        } else {
            switch (direction) {
                case Direction.East:
                    newPosition.x++;
                    break;
                case Direction.NorthEast:
                    newPosition.x++;
                    newPosition.y--;
                    break;
                case Direction.SouthEast:
                    newPosition.x++;
                    newPosition.y++;
                    break;
                case Direction.West:
                    newPosition.x--;
                    break;
                case Direction.NorthWest:
                    newPosition.y--;
                    break;
                case Direction.SouthWest:
                    newPosition.y++;
                    break;
            }
        }
        return newPosition;
    }

    public static bool collision(Coordinate pos1, Coordinate pos2) {
        return ((pos1.x == pos2.x) && (pos1.y == pos2.y));
    }

    public static string KeyGenerator(Coordinate Coordinate) {
        string key = Coordinate.x.ToString() + "," + Coordinate.y.ToString();
        return key;
    }
}
