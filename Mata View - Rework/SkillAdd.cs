using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mata_View___Rework
{
    class SkillAdd
    {
        private string Name;
        private float Duration;
        private int Realtime;
        private string Champname;
        private string Displayname;

        public SkillAdd(string name, float duration, int realtime, string champname, string displayname)
        {
            Name = name;
            Duration = duration;
            Realtime = realtime;
            Champname = champname;
            Displayname = displayname;
        }

        public void SetDuration(float newDuration)
        {
            Duration = newDuration;
        }

        public int GetRealtime()
        {
            return Realtime;
        }

        public float GetDuration()
        {
            return Duration;
        }

        public string GetChampName()
        {
            return Champname;
        }

        public string GetSkillName()
        {
            return Name;
        }

  
    }
}
