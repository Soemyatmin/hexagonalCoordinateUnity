
public class Placement {

    public enum AttackType {
        Melee = 0,
        Range = 1
    }

    public Coordinate position;
    public string name;
    public string symbol;
    public int damage;
    public AttackType attackType;
    public int attackRange;

    public Placement(Coordinate position) {
        this.position = position;
        symbol = "P";
        damage = 1;
        attackRange = 3;
        attackType = AttackType.Range;
    }
}
// type of placement and cards are out of this project scope
