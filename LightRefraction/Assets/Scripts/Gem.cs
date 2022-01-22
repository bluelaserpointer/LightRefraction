using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(menuName = "Gem")]
    public class Gem : ScriptableObject
    {
        [SerializeField]
        List<float> degreeAngles;

        public List<float> DegreeAngles => degreeAngles;
    }
}