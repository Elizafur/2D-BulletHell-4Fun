using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerMustKill : MonoBehaviour
{
    [Required]
    public EventTrigger.TriggerEvent methodToTrigger;

    [Required]
    public GameObject target;

    void Update()
    {
        if (target == null) //target dead
        {
            BaseEventData eventData = new BaseEventData(EventSystem.current);
            eventData.selectedObject = this.gameObject;
            methodToTrigger.Invoke(eventData);
        }
    }



}
