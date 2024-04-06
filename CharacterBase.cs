using DefenseViruses.Model;
using Godot;
using System;

public partial class CharacterBase : CharacterBody2D
{
    AnimatedSprite2D playerAnim;
    Vector2 dir = Vector2.Zero;
    bool flip = false;
   
    bool moveAble = false;
    bool stop = false;

    public PlayerData PlayerData; 
     

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playerAnim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        ChoisePlayer("player2");
        PlayerData = new PlayerData();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {


        if (moveAble && !stop)
        {
            Vector2 mouse = GetGlobalMousePosition();

            dir = (mouse - Position).Normalized();

            Velocity = dir * PlayerData.Speed;

            if (mouse.X >= Position.X)
            {
                flip = false;
            }
            else
            {
                flip = true;
            }


            playerAnim.FlipH = flip;


            MoveAndSlide();
        }
    }

    private void ChoisePlayer(string fileName)
    {
        string playerPath = "res://Asserts/";


        playerAnim.SpriteFrames.ClearAll();

        var spritFrame = playerAnim.SpriteFrames;

        //spritFrame.AddAnimation("default");
        // spritFrame.SetAnimationLoop("default", true);
        //图片总大小
        var textureSize = new Vector2(960, 240);
        //一帧的大小
        var spriteSize = new Vector2(240, 240);

        var fullTexture = ResourceLoader.Load<Texture2D>($"{playerPath + fileName}/{fileName}-sheet.png");

        int countColumns = (int)(textureSize.X / spriteSize.X);
        int countRows = (int)(textureSize.Y / spriteSize.Y);

        for (int i = 0; i < countColumns; i++)
        {
            for (int j = 0; j < countRows; j++)
            {
                AtlasTexture frame = new AtlasTexture();
                frame.Atlas = fullTexture;

                frame.Region = new Rect2(new Vector2(i, j) * spriteSize, spriteSize);
                spritFrame.AddFrame("default", frame);
            }

        }
        playerAnim.Play();
    }
    public void OnStopAreaEnter()
    {
        stop = true;
    }
    public void OnStopAreaLeave()
    {
        stop = false;
    }
    public override void _Input(InputEvent @event)
    {
        InputEventMouseButton inputEventMouse = @event as InputEventMouseButton;
        if (@event is InputEventMouseButton)
        {
            if (inputEventMouse.ButtonIndex == MouseButton.Left && inputEventMouse.Pressed)
            {
                //GD.Print($"down:{moveAble}");
                moveAble = !moveAble;
            }


        }

        base._Input(@event);
    }

    public void OnTakeDropItemBodyEntered(Node2D body)
    {
        if (body.IsInGroup("drop_items"))
        {
            (body as DropItems).IsCanMove = true;
        }
    }

    public void OnTakeDropItemBodyExited(Node2D body)
    {
        if (body.IsInGroup("drop_items"))
        {
            (body as DropItems).IsCanMove = false;
        }
    }


    public void OnSropAreaBodyEntered(Node2D body)
    {
        if (body.IsInGroup("drop_items"))
        {
            PlayerData.Gold += 1*PlayerData.GoldGet;
            PlayerData.NowExp += 1 * PlayerData.NowExp;
            if (PlayerData.NowExp == PlayerData.MaxExp)
            {
                PlayerData.MaxExp = 2 * PlayerData.Level;
                PlayerData.Level += 1;
                PlayerData.NowExp = 0;
                
            }
            body.QueueFree();
        }
        if (body.IsInGroup("enemy"))
        {
            PlayerData.NowHP -= 1;
        }
    }

}
