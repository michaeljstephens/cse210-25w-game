using Raylib_cs;

public class Treasure : GameObject
{
    public Treasure(int x, int y) : base(x, y)
    {
        //init other things like the treasures value and speed
    }


    public override void Draw()
    {
        Raylib.DrawRectangle(_x, _y, 20, 20, Color.Yellow);
    }


    public void Fall()
    {
        _y += 5;
    }


    public bool CollidesWith(Player player)
    {
        return Raylib.CheckCollisionRecs(GetRect(), player.GetRect());
    }


    public Rectangle GetRect()
    {
        return new Rectangle(_x, _y, 20, 20);
    }


    public bool IsOffScreen()
    {
        return _y > GameManager.SCREEN_HEIGHT;
    }
}
