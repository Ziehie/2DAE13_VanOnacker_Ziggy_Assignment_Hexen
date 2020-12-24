using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
