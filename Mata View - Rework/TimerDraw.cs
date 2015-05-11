using System;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace Mata_View___Rework
{
    class TimerDraw
    {
        public int NetworkId;
        public string Name;
        public float Duration;
        public Vector3 Position;
        public float CreatedAt;
        public static GameObject SenderN;
        public int RealtimeCheck;

        public bool Visible = true;

        public Render.Text Timer { get; set; }
        public TimerDraw(int netId, string name, float duration, Vector3 position, float createdAt, GameObject senderN, int realtimecheck)
        {
            try
            {
                NetworkId = netId;
                Name = name;
                Duration = duration;
                Position = position;
                CreatedAt = createdAt;
                if (senderN == null)
                    return;
                SenderN = senderN;
                RealtimeCheck = realtimecheck;

                Timer = new Render.Text("", new Vector2(0, 0), (MenuList.Testsize.GetValue<Slider>().Value) * 2, SharpDX.Color.White)
                {
                    VisibleCondition =
                    condition => (((Math.Abs(Duration - (-1)) < 0 || (int)((CreatedAt + Duration + 1) - Game.Time) > 0) && (duration) > 0 && Visible)),

                    PositionUpdate = delegate
                    {
                        var pos = Drawing.WorldToScreen(new Vector3(Position.X, Position.Y, Position.Z));

                        switch (RealtimeCheck)
                        {
                            case 0: //Object First Created Posistion
                                break;
                            case 2: //Realtime on Sender Position
                                Position = SenderN.Position;
                                Position.Y += 40f;
                                break;

                        }
                        return pos;
                    },
                    TextUpdate = () => ((CreatedAt + Duration) - Game.Time).ToString("0.0"),
                    OutLined = true,
                    Centered = true
                };
                Timer.Add();
            }
            catch (Exception e)
            {
                Console.WriteLine("===MataView Error=== / TimerDraw");
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
