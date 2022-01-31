using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SchoolUpgrade : MonoBehaviour
{
    public UnityEvent onUpgradeUnlocked = new UnityEvent();

    private bool isUnlocked = false;

    public void Unlock()
    {
        isUnlocked = true;
    }

    public void UpgradeSchool()
    {
        if (isUnlocked)
            onUpgradeUnlocked.Invoke();
    }
}
