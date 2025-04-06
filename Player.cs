using Raylib_cs;

public class Player : GameObject
{
    // other member variables
    public Player(int x, int y) : base(x, y)
    {
        //Other initialization tasks here
    }


    public override void Draw()
    {
        Raylib.DrawRectangle(_x, _y, 50, 10, Color.Blue);
    }


    //other methods
    public void Update()
    {
        if (Raylib.IsKeyDown(KeyboardKey.Left)) _x -= 7;
        if (Raylib.IsKeyDown(KeyboardKey.Right)) _x += 7;


        _x = Math.Clamp(_x, 0, GameManager.SCREEN_WIDTH - 50);
    }


    public Rectangle GetRect()
    {
        return new Rectangle(_x, _y, 50, 10);
    }
}
