using Godot;
using System;


public partial class GameUI : CanvasLayer
{
	[Export]
	public ProgressBar hpValueBar;
	[Export]
	public ProgressBar expValueBar;
	[Export]
	public Label goldLable;



	[Export]
	Label roundLable;
	[Export]
	Label timeLable;

	[Export]
	Timer timer;

    private int nowRoundNum;
    public int NowRoundNum { get => nowRoundNum; set 
		{
			roundLable.Text = $"第{value}回合";
            nowRoundNum = value; 
		} 
	}

    private int currentime;
    public int Currentime
    {
        get => currentime; 
		set {
			timeLable.Text = value.ToString();
            currentime = value;
        } 
    }
    CharacterBase player;

	/// <summary>
	/// 回合结束事件,参数为刚结束的回合数
	/// </summary>
	public static event EventHandler<int> RoundEnd;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        player = GetTree().GetFirstNodeInGroup("player") as CharacterBase;
        timer.Timeout += Timer_Timeout;
		InitRound();
    }

    private void Timer_Timeout()
    {
        Currentime = currentime- 1;
		if (currentime <= 0) {

            RoundEnd?.Invoke(this,nowRoundNum);

        }
    }

    void InitRound()
	{
		NowRoundNum += 1;
		currentime = 10;
		timer.Start();
    }


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        hpValueBar.MaxValue = player.PlayerData.MaxHP;
		hpValueBar.Value = player.PlayerData.NowHP;
		hpValueBar.GetChild<Label>(0).Text = string.Concat(player.PlayerData.NowHP, "/", player.PlayerData.MaxHP);
        expValueBar.MaxValue = player.PlayerData.MaxExp;
		expValueBar.Value = player.PlayerData.NowExp;
        expValueBar.GetChild<Label>(0).Text = string.Concat("LV:", player.PlayerData.Level);
		goldLable.Text = player.PlayerData.Gold.ToString();

    }
}
