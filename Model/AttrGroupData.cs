using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefenseViruses.Model
{
    public class AttrGroupData
    {
        public string Name;
        public AttrType[] Types;

        public AttrType GetRandType() => Types[Random.Shared.Next(0, Types.Length-1)];
    }

   public record AttrType(string name,string img,int min,int max)
    {
        /// <summary>
        /// 获取一个符合当前属性设定的随机值
        /// </summary>
        /// <returns></returns>
        public int GetRandm()=>Random.Shared.Next(min,max);
    }
}
