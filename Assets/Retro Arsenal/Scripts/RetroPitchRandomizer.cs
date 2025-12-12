using UnityEngine;
using System.Collections;
using Extensions;

namespace RetroArsenal
{

	public class RetroPitchRandomizer : MonoBehaviour
	{
	
		public float randomPercent = 10;
	
		void Start ()
		{
			if(transform.Has(out AudioSource source))
				source.pitch *= 1 + Random.Range(-randomPercent / 100, randomPercent / 100);
		}
	}
}