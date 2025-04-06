using Raylib_cs;

class GameManager
{
    public const int SCREEN_WIDTH = 800;
    public const int SCREEN_HEIGHT = 600;

    private string _title;
    private List<GameObject> _gameObjects = new List<GameObject>();
    private Player _player;
    private int _score = 0;
    private int _lives = 3;
    private bool _isGameOver = false;
    private Random _rand = new Random();

    public GameManager()
    {
        _title = "CSE 210 Game";
    }

    /// <summary>
    /// The overall loop that controls the game. It calls functions to
    /// handle interactions, update game elements, and draw the screen.
    /// </summary>
    public void Run()
    {
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(SCREEN_WIDTH, SCREEN_HEIGHT, _title);
        // If using sound, un-comment the lines to init and close the audio device
        // Raylib.InitAudioDevice();

        InitializeGame();

        while (!Raylib.WindowShouldClose())
        {
            if (!_isGameOver)
            {
                HandleInput();
                ProcessActions();
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            DrawElements();

            Raylib.EndDrawing();
        }

        // Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }

    /// <summary>
    /// Sets up the initial conditions for the game.
    /// </summary>
    private void InitializeGame()
    {
        //create a player and add them to the list
        Player p = new Player(SCREEN_WIDTH / 2, SCREEN_HEIGHT - 50);
        _player = p;
        _gameObjects.Add(p);

        Treasure t1 = new Treasure(50, 50);
        Treasure t2 = new Treasure(100, 50);
        _gameObjects.Add(t1);
        _gameObjects.Add(t2);
    }

    /// <summary>
    /// Responds to any input from the user.
    /// </summary>
    private void HandleInput()
    {
        _player.Update();
    }

    /// <summary>
    /// Processes any actions such as moving objects or handling collisions.
    /// </summary>
    private void ProcessActions()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject obj in _gameObjects)
        {
            if (obj is Treasure t)
            {
                t.Fall();
                if (t.CollidesWith(_player))
                {
                    _score++;
                    toRemove.Add(t);
                }
                else if (t.IsOffScreen())
                {
                    toRemove.Add(t);
                }
            }


            else if (obj is Bomb b)
            {
                b.Fall();
                if (b.CollidesWith(_player))
                {
                    _lives--;
                    toRemove.Add(b);


                    if (_lives <= 0)
                    {
                        _isGameOver = true;
                    }
                }
                else if (b.IsOffScreen())
                {
                    toRemove.Add(b);
                }
            }
        }


        foreach (GameObject obj in toRemove)
        {
            _gameObjects.Remove(obj);
        }


        if (_rand.NextDouble() < 0.02)
        {
            _gameObjects.Add(new Treasure(_rand.Next(0, SCREEN_WIDTH - 25), 0));
        }


        if (_rand.NextDouble() < 0.01)
        {
            _gameObjects.Add(new Bomb(_rand.Next(0, SCREEN_WIDTH - 25), 0));
        }
    }

     /// <summary>
    /// Draws all elements on the screen.
    /// </summary>
    private void DrawElements()
    {
        foreach (GameObject item in _gameObjects)
        {
            item.Draw();
        }

        Raylib.DrawText($"Lives: {_lives}", 10, 10, 20, Color.Red);
        Raylib.DrawText($"Score: {_score}", SCREEN_WIDTH - 120, 10, 20, Color.Black);

        if (_isGameOver)
        {
            string message = "GAME OVER";
            int fontSize = 70;
            int textWidth = Raylib.MeasureText(message, fontSize);
            Raylib.DrawText(message, (SCREEN_WIDTH - textWidth) / 2, SCREEN_HEIGHT / 2 - 25, fontSize, Color.Black);
        }
    }
}
