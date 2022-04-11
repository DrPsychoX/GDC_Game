using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentChangerTrigger : MonoBehaviour
{
    [SerializeField] EnviromentManager enviromentManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.tag=="Player")
        {
            enviromentManager.SpawnObjects();

            enviromentManager.DestroyObjects();

            Destroy(this.gameObject);

        }
    }

    public void ActivateTrigger()
    {
        this.GetComponent<BoxCollider>().enabled = true;
    }
  
}
