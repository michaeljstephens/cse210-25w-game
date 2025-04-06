using Raylib_cs;

public class Bomb : GameObject
{
    public Bomb(int x, int y) : base(x, y)
    {
        //init other things like the bombs value and speed
    }


    public override void Draw()
    {
        Raylib.DrawRectangle(_x, _y, 50, 50, Color.Red);
    }


    public void Fall()
    {
        _y += 6;
    }


    public bool CollidesWith(Player player)
    {
        return Raylib.CheckCollisionRecs(GetRect(), player.GetRect());
    }


    public Rectangle GetRect()
    {
        return new Rectangle(_x, _y, 50, 50);
    }


    public bool IsOffScreen()
    {
        return _y > GameManager.SCREEN_HEIGHT;
    }
}
