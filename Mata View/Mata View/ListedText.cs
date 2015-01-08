#region

using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using Color = System.Drawing.Color;

#endregion


namespace Mata_View
{
    public class ListedText
    {
        public int NetworkId;
        public string Name;
        public float Duration;
        public Color ObjColor;
        public int Range;
        public Vector3 Position;
        public float CreatedAt;
        public static GameObject SenderN;

        public bool Visible = true;

        public Render.Text Timer { get; set; }
        public ListedText(int netId, string name, float duration, Vector3 position, float createdAt, GameObject senderN)
        {
            NetworkId = netId;
            Name = name;
            Duration = duration;
            Position = position;
            CreatedAt = createdAt;

            SenderN = senderN;

            Timer = new Render.Text("", new Vector2(0, 0), (Menus.testsize.GetValue<Slider>().Value) * 2, SharpDX.Color.White)
            {
                VisibleCondition =
                condition => (((Duration == -1 || (int)((CreatedAt + Duration + 1) - Game.Time) > 0) && (duration) > 0 && Visible)),
                 
                PositionUpdate = delegate
                {
                    var pos = Drawing.WorldToScreen(new Vector3(Position.X, Position.Y, Position.Z));
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
