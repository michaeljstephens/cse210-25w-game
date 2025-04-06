public abstract class GameObject
{
    protected int _x;
    protected int _y;

    public GameObject(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public abstract void Draw();
}
