using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckHUD : MonoBehaviour {

	public Transform CardHolder;
	public GameObject CardPrefab;
	public DeckCard[] CurrentCardList;

	public float ActiveTimer;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Global.Dataholder.TitleCardVisible && CurrentCardList.Length > 0) {
			DeleteCards ();
		}

		if (Global.Dataholder.CardsOpen) {
			ActiveTimer += Time.deltaTime * 4;
		} else {
			ActiveTimer -= Time.deltaTime * 4;
		}
		ActiveTimer = Mathf.Clamp01 (ActiveTimer);
		CardHolder.localPosition = new Vector3 (0, DataHolder.ParabolicLerp (-4, 0, ActiveTimer, 1), 10);

		if (Global.Dataholder.CardsOpen) {
			
			PrepCardXPositions ();
		}
	}

	public void ReOrderCards(int index)
	{
		for (int i = 0; i < CurrentCardList.Length; i++) {
			CurrentCardList[i].Layer = Mathf.Abs(i - index)*2;
			if (i > index) {
				CurrentCardList [i].Layer++; // weave them so the text cannot overlap adjacent cards
			}
		}

		for (int i = 0; i < CurrentCardList.Length; i++) {
			CurrentCardList [i].SR.sortingOrder = 80 - CurrentCardList [i].Layer * 2;
			CurrentCardList [i].TMesh.sortingOrder = 81 - CurrentCardList [i].Layer * 2;
		}
	}
	public float width;
	public void PrepCardXPositions()
	{
		width = 2 + CurrentCardList.Length*0.5f;
		for (int i = 0; i < CurrentCardList.Length; i++) {
			CurrentCardList [i].transform.localPosition = new Vector3 ( width * 2 * (i+0.5f) * (1f) / (CurrentCardList.Length+0f) - width,-5.5f,0);
		}
	}

	[ContextMenu("TEST")]
	public void CreateCardList()
	{
		for (int i = 0; i < CurrentCardList.Length; i++) {
			if (CurrentCardList [i] != null) {
				Destroy (CurrentCardList [i].gameObject);
			}
		}


		int CardCount = 0;
		for (int i = 0; i < Global.Dataholder.UnlockedCards.Length; i++) {
			// check which cards we have unlocked.
			if (Global.Dataholder.UnlockedCards [i]) {
				CardCount++;
			}
		}
		CurrentCardList = new DeckCard[CardCount];
		int j = 0;
		for (int i = 0; i < Global.Dataholder.UnlockedCards.Length; i++) {
			if (Global.Dataholder.UnlockedCards [i]) {
				CurrentCardList [j] = Instantiate (CardPrefab, transform.position, transform.rotation, transform).GetComponent<DeckCard>();
				CurrentCardList [j].TM.text = Global.Dataholder.CardStringList [i].Replace('#','\n');
				CurrentCardList [j].Layer = j;
				CurrentCardList [j].Index = j;
				CurrentCardList [j].PhraseID = i;
				j++;
			}
		}
		for (int i = 0; i < CurrentCardList.Length; i++) {
			CurrentCardList [i].SR.sortingOrder = 40 - CurrentCardList [i].Layer * 2;
			CurrentCardList [i].TMesh.sortingOrder = 41 - CurrentCardList [i].Layer * 2;
		}
		PrepCardXPositions ();

	}

	public void DeleteCards()
	{
		for (int i = 0; i < CurrentCardList.Length; i++) {
			if (CurrentCardList [i] != null) {
				Destroy (CurrentCardList [i].gameObject);
			}
		}
	}

}
