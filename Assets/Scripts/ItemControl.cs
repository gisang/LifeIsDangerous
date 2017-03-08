using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour
{

	public GameObject genepointItem = null;
	public GameObject GENE = null;
	private float geneAppearChance;
	private float geneAppearTime;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (GeneAppear ());
	}

	void Awake() {
		geneAppearChance = 0.25f;
		geneAppearTime = 2.00f;
	}


	IEnumerator GeneAppear() {
		while (true) {
			while (TimeControl.is_paused) {
				yield return null;
			}
			yield return new WaitForSeconds (geneAppearTime);
			if (Random.Range (0.00f, 1.00f) < geneAppearChance) {
				GENE = GameObject.Instantiate (genepointItem, null) as GameObject;
				GENE.SetActive (true);
				continue;
			}
			yield return new WaitForSeconds (geneAppearTime);
			if (Random.Range (0.00f, 1.00f) < geneAppearChance*2) {
				GENE = GameObject.Instantiate (genepointItem, null) as GameObject;
				GENE.SetActive (true);
				continue;
			}
			yield return new WaitForSeconds (geneAppearTime);
			if (Random.Range (0.00f, 1.00f) < geneAppearChance*3) {
				GENE = GameObject.Instantiate (genepointItem, null) as GameObject;
				GENE.SetActive (true);
				continue;
			}
			yield return new WaitForSeconds (geneAppearTime);
			if (Random.Range (0.00f, 1.00f) < geneAppearChance*4) {
				GENE = GameObject.Instantiate (genepointItem, null) as GameObject;
				GENE.SetActive (true);
				continue;
			}
			yield return new WaitForSeconds (geneAppearTime);
			if (Random.Range (0.00f, 1.00f) < geneAppearChance*5) {
				GENE = GameObject.Instantiate (genepointItem, null) as GameObject;
				GENE.SetActive (true);
				continue;
			}
		}
	}
}
