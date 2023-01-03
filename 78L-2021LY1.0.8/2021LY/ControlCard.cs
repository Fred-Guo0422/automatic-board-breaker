using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// 控制卡抽象类，所有品牌运动控制卡均继承该类
/// </summary>
namespace _2021LY
{
   public abstract class ControlCard
    {


        /// <summary>
        /// 初始化板卡；返回true成功false失败，msg是具体不同情况初始化结果信息
        /// </summary>
        public abstract bool Initial_Card(out string msg);
    }
}
