using UnityEngine;
using static UnityEngine.Quaternion;

namespace _Project.Logic.UI
{
    public class ImageVerticalAligner : MonoBehaviour
    {
        private void Update() => 
            transform.rotation = identity;
    }
}
