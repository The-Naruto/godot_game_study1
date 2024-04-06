using DefenseViruses.Model;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class RoundEnd : CanvasLayer
{
    [Export]
    GridContainer ItemContainer;
    [Export]
    Panel ItemTemplate;

    [Export]
    VBoxContainer AttrContainer;
    [Export]
    HBoxContainer arrtTemplate;

    Button btnContinue;
    Button btnRefresh;
    Button btn;


    Dictionary<string, AttrGroupData> ATTR_GROUP;
    CharacterBase target;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        target = GetTree().GetFirstNodeInGroup("player") as CharacterBase;
        ItemTemplate.Hide();
        // TextureRect textureRect = ItemTemplate.GetNode<TextureRect>("MarginContainer/HBoxContainer/TextureRect");
        // textureRect.Texture = GD.Load<CompressedTexture2D>("res://scenes/round_end/asserts/exp_get.png");



        arrtTemplate.Hide();
        InitBaseData();

        InitCurData();
    }

    private void InitCurData()
    {


        //属性加成选择
        InitItemContainer();

        //加载角色自身数据
        LoadPlayerData();
    }

    private void InitItemContainer()
    {
        //clear
        foreach (var attr in ItemContainer.GetChildren())
        {
            Panel temp = attr as Panel;
            if (attr != null && temp.Visible)
            {
                attr.QueueFree();
            }
        }
        // init 
        string[] keys = ATTR_GROUP.Keys.ToArray();
        for (var i = 0; i < 4; i++)
        {
            Panel item = ItemTemplate.Duplicate() as Panel;

            int value = Random.Shared.Next(0, ATTR_GROUP.Count - 1);
            string key = keys[value];
            AttrType attrType = ATTR_GROUP[key].GetRandType();
            TextureRect textureRect = item.GetNode<TextureRect>("MarginContainer/HBoxContainer/TextureRect");
            textureRect.Texture = GD.Load<CompressedTexture2D>($"res://scenes/round_end/asserts/{attrType.img}.png");
            Label lable1 = item.GetNode<Label>("MarginContainer/HBoxContainer/VBoxContainer/Label");
            lable1.Text = ATTR_GROUP[key].Name;
            int typeValue = attrType.GetRandm();
            RichTextLabel rtl = item.GetNode<RichTextLabel>("RichTextLabel");
            rtl.Text = $"[color=green]{typeValue}[/color] {attrType.name}";
            MyButton button = item.GetNode<MyButton>("Button");

            button.GroupName = ATTR_GROUP[key].Name;
            button.TypeName = attrType.img.Replace("_", "");
            button.Value = typeValue.ToString();
            button.Clicked += Button_Pressed;

            item.Show();
            ItemContainer.AddChild(item);
        }



    }

    /// <summary>
    /// 选择按钮
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="param"></param>
    private void Button_Pressed(object obj, string param)
    {
        GD.Print(param);
        string[] strings = param.Split('@');


        Type type = target.PlayerData.GetType();
        foreach (var field in type.GetFields())
        {
            if (field.Name.ToLower() == strings[1])
            {
                //反射的方式无法设置
                field.SetValue(target.PlayerData, strings[2]);
                 
            }

        }

        if (ItemContainer.GetChildren().Count == 0) InitItemContainer();
    }

    private void LoadPlayerData()
    {
        Type type = target.PlayerData.GetType();
            foreach( var field in type.GetFields())
        {
            HBoxContainer attr =  arrtTemplate.Duplicate() as HBoxContainer;
            Label label =  attr.GetNode<Label>("name");
            label.Text = TranslationServer.Translate(field.Name);
            Label value =  attr.GetNode<Label>("value");
            value.Text =Convert.ToString( field.GetValue(target.PlayerData));
            attr.Show();
            AttrContainer.AddChild(attr);  
        }
      
             


    }

    void InitBaseData()
    {

        ATTR_GROUP = new Dictionary<string, AttrGroupData>() {
            {"attack",new AttrGroupData(){
                Name = "攻击力",Types= new AttrType[]{new("基础伤害叠加", "basic_hurt",1,5),
                 new("基础伤害倍数", "basic_hurt",1,3)
               }

              }
            },
              {"speed",new AttrGroupData(){
                Name = "速度",Types= new AttrType[]{
                  new("移动速度", "speed", 1, 5)
               }
              }
            },
                {"hp",new AttrGroupData(){
                Name = "血量",Types= new AttrType[]{
                   new("最大血量", "max_hp",5, 20)
               }
              }
            },
  {"get_add",new AttrGroupData(){
                Name = "获取叠加",Types= new AttrType[]{
                   new("金币叠加", "gold_get", 1, 3),
                   new("经验叠加", "exp_get", 1, 3)
               }
              }
            },


        };

    }


    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
