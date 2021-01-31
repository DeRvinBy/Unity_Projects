using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.GameManagers
{
    public interface IManager
    {
        StatusOfManager status { get; }

        void StartManager();
    }

    public enum StatusOfManager
    {
        Shutdown,
        Initializiang,
        Started
    }
}