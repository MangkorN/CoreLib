using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Components
{
    public class DisableOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}
