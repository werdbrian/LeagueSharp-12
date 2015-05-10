using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mata_View___Rework
{
    class SkillAddP
    {
        private readonly string Name;
        private readonly string Objname;

        public SkillAddP(string name, string objname)
        {
            Name = name;
            Objname = objname;
        }

        public string GetObjName()
        {
            return Objname;
        }

        public string GetSkillName()
        {
            return Name;
        }

    }
}
