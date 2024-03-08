using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

#if FACEBOOK
using Facebook.Unity;
#endif


public class LeadboardManager : MonoBehaviour
{
    public GameObject playerIconPrefab;
    List<LeadboardObject> playerIconsList = new List<LeadboardObject>();

    void OnEnable()
    {
        GetComponent<Image>().enabled = false;
#if PLAYFAB || GAMESPARKS
        //PlayFabManager.OnLevelLeadboardLoaded += ShowLeadboard;
        Debug.Log("leadboard enable");
        StartCoroutine(WaitForLeadboard());
#endif
    }

    void OnDisable()
    {
        //		Debug.Log ("Leadboard disable");		
#if PLAYFAB || GAMESPARKS
        //PlayFabManager.OnLevelLeadboardLoaded -= ShowLeadboard;
#endif
        ResetLeadboard();
    }

    void ResetLeadboard()
    {
        transform.localPosition = new Vector3(0, -40f, 0); //1.4.6
        foreach (LeadboardObject item in playerIconsList)
        {
            Destroy(item.gameObject);
        }
        playerIconsList.Clear();
    }

#if PLAYFAB || GAMESPARKS
    IEnumerator WaitForLeadboard()
    {
        yield return new WaitForSeconds(0.5f);
        
    }
#endif
}
