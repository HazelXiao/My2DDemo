using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extensions
{
	public static T RemoveRandomElement<T>( this IList<T> list )
	{
		if( list == null )
		{
			throw new ArgumentException( "list" );
		}

		if( list.Count == 0 )
		{
			throw new ArgumentOutOfRangeException( "list" );
		}

		int randomIndex = Random.Range( 0, list.Count );
		T element = list[randomIndex];
		list.RemoveAt( randomIndex );
		return element;
	}

	public static T RandomElement<T>( this IList<T> list )
	{
		if( list == null )
		{
			throw new ArgumentException( "list" );
		}

		if( list.Count == 0 )
		{
			throw new ArgumentOutOfRangeException( "list" );
		}

		int randomIndex = Random.Range( 0, list.Count );
		return list[randomIndex];
	}
}
