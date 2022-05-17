using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObjectPooler))]
public class AutoPoolerEvent : MonoBehaviour
{
    public List<EventID> autoSpawn = new List<EventID>();
    public List<EventID> autoReturn = new List<EventID>();


    private GameObjectPooler pooler;
    // Start is called before the first frame update
    void Start()
    {
        pooler = GetComponent<GameObjectPooler>();
        foreach (EventID spawnEvent in autoSpawn)
        {
            this.RegisterListener(spawnEvent, (tag) => AutoSpawn((string)tag));
        }
        foreach (EventID returnEvent in autoReturn)
        {
            this.RegisterListener(EventID.OnPallaDisappear, (basePool) => AutoReturn((BasePool)basePool));
        }

    }

    private void AutoSpawn(string tag)
    {
        pooler.GetFromPool(tag);
    }

    private void AutoReturn(BasePool basePool)
    {
        pooler.ReturnToPool(basePool.tag, basePool.gameObject);
    }
}

public class BasePool
{
    public string tag;
    public GameObject gameObject;

    public BasePool(string t, GameObject gO)
    {
        tag = t;
        gameObject = gO;
    }
}