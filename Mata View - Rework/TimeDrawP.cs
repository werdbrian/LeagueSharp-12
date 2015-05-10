using System;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace Mata_View___Rework
{
    class TimeDrawP
    {
        public string Name;
        public float Duration;
        public Vector3 Position;
        public float CreatedAt;
        public GameObject Arg;
        public int PositionType;
        public Obj_AI_Base Obj;
        public bool Visible = true;
        public bool VisibleTimer = true;

        public Render.Text Timer { get; set; }
        public TimeDrawP(string name, float duration, Vector3 position, float createdAt, int positiontype, Obj_AI_Base obj, GameObject arg)
        {
            Name = name;
            Duration = duration;
            Position = position;
            CreatedAt = createdAt;
            PositionType = positiontype;
            Arg = arg;
            Obj = obj;

            Timer = new Render.Text("", new Vector2(0, 0), (MenuList.Testsize.GetValue<Slider>().Value) * 2, SharpDX.Color.White)
            {
                VisibleCondition = delegate
                {
                    switch (PositionType)
                    {
                        case 0: 
                            Visible = Obj.IsVisible;
                            break;
                        case 1: 
                            Visible = Arg.IsVisible;
                            break;
                    }
                    return ((Math.Abs(Duration - (-1)) < 0 || (int)((CreatedAt + Duration + 1) - Game.Time) > 0) && (duration) > 0 && Visible && VisibleTimer);
                },
 
                PositionUpdate = delegate
                {
                    var pos = Drawing.WorldToScreen(new Vector3(Position.X, Position.Y, Position.Z));
                    switch (PositionType)
                    {
                        case 0: //always self
                            Position = Obj.Position;    
                            break;
                        case 1: //target
                            Position = Arg.Position;
                            break;
                    }
                    Position.Y += 40;
                    return pos;
                },
                TextUpdate = () => ((CreatedAt + Duration) - Game.Time).ToString("0.0"),
                OutLined = true,
                Centered = true
            };
            Timer.Add();
        }
    }
}
