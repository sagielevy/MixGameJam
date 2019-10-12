using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class TrailValidatorMock : ITrailValidator
    {
        public bool Validate(ITrail expected, ITrail actual, float threshold)
        {
            return true;
        }
    }
}
