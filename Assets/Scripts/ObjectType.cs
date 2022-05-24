using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerObjectType
{
	Exit,
	Soda,
	Food
}
public class ObjectType : MonoBehaviour
{
	public TriggerObjectType type;
}
