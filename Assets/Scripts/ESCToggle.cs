using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PauseEvent : UnityEvent<bool> // True for IsPaused
{
}

public class ESCToggle : MonoBehaviour
{
    private Transform[] childrens;
    public  PauseEvent OnTogglePause;
    
    T[] GetCompNoRoot<T>(GameObject obj, bool isActive) where T : Component
    {
        // Possibly refactor to remove the new List as a GC allocator
        List<T> tList = new List<T>();
        foreach (Transform child in obj.transform)
        {
            T[] scripts = child.GetComponentsInChildren<T>(isActive);
            if (scripts != null)
            {
                foreach (T sc in scripts)
                    tList.Add(sc);
            }
        }
        return tList.ToArray();
    }

    private void Start()
    {
        childrens = GetCompNoRoot<Transform>(gameObject, true);        
    }

    public void ToggleESC()
    {
        Player.IsPaused = !Player.IsPaused;
        OnTogglePause?.Invoke(Player.IsPaused);
        bool targetEnableValue = !childrens[0].gameObject.activeInHierarchy;
        foreach (var child in childrens)
        {
            child.gameObject.SetActive(targetEnableValue);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Update()
    {
        if(Player.CanInput)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleESC();
            }
        }
    }
}
