using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour {

	public bool MouseInside;

	public SpriteRenderer SR;
	public Sprite Inactive;
	public Sprite Active;
	public Sprite NotEnoughMoney;
	public Sprite SoldOut;

	public int ShopID;
	public bool[] SoldOuts;

	public int[] prices;
	public string[] Itemnames;

	public TextMesh ItemName;
	public TextMesh ItemPrice;
	public int[] itemIDs;

	public int remainingseeds = 35;

	public bool Corn1;
	public bool Corn10;
	public bool CornAll;

	public Sprite CornDefault;
	public Sprite CornSell1;
	public Sprite CornSell10;
	public Sprite CornSell10Cannot;
	public Sprite CornSellAll;
	public Sprite CornNoCornl;
	public Sprite CornBroke;
	public int RemainingToSell = 400;
	public bool cornshoponce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Corn1 || Corn10 || CornAll) {

			if(RemainingToSell <= 0)
			{
				SR.sprite = CornBroke;
				return;
			}

			if (Main.Data.ItemCounts [5] <= 0) {
				SR.sprite = CornNoCornl;
				return;
			}

			if (Main.Data.ItemCounts [5] > 0 && SR.sprite == CornNoCornl) {
				SR.sprite = CornDefault;
			}

			if (Corn1) {

				if (MouseInside) {
					SR.sprite = CornSell1;
					if (Input.GetKeyDown (KeyCode.Mouse0)) {
						Main.Data.RemainingCornToSell--;
						Main.Data.ItemCounts [5]--;
						Main.Data.Money++;
						Main.Data.Obj_SellCorn = true;
						if (Main.Data.ItemCounts [5] <= 0) {
							Main.Data.RemoveItem (5);
						}
					}

				}

			} else if (Corn10) {
				if (MouseInside) {
					if (Main.Data.ItemCounts [5] >= 10) {
						SR.sprite = CornSell10;
						if (Input.GetKeyDown (KeyCode.Mouse0)) {
							Main.Data.RemainingCornToSell -= 10;
							Main.Data.ItemCounts [5]-=10;
							Main.Data.Money+=10;
							Main.Data.Obj_SellCorn = true;

							if (Main.Data.ItemCounts [5] <= 0) {
								Main.Data.RemoveItem (5);
							}
						}
					} else {
						SR.sprite = CornSell10Cannot;
					}
				}

			}
			else if (CornAll) {

				if (MouseInside) {
					SR.sprite = CornSellAll;
					if (Input.GetKeyDown (KeyCode.Mouse0)) {

						if (Main.Data.ItemCounts [5] > RemainingToSell) {
							Main.Data.ItemCounts [5] -= RemainingToSell;
							Main.Data.Money += RemainingToSell;
							Main.Data.Obj_SellCorn = true;

							Main.Data.RemainingCornToSell = 0;
							if (Main.Data.ItemCounts [5] <= 0) {
								Main.Data.RemoveItem (5);
							}
						} else {
							Main.Data.RemainingCornToSell -= Main.Data.ItemCounts [5];
							Main.Data.Money += Main.Data.ItemCounts [5];
							Main.Data.ItemCounts [5] = 0;
							Main.Data.Obj_SellCorn = true;

							if (Main.Data.ItemCounts [5] <= 0) {
								Main.Data.RemoveItem (5);
							}
						}

					}

				}

			}


			if (MouseInside) {
				cornshoponce = true;
			} else {
				if (cornshoponce) {
					cornshoponce = false;
					SR.sprite = CornDefault;
				}

			}


			MouseInside = false;
			return;
		}
		else
		{
			if (ShopID == -1) {
				return;
			}

			ItemName.text = Itemnames [ShopID];
			ItemPrice.text = "" + prices [ShopID];

			if (SoldOuts [ShopID]) {
				SR.sprite = SoldOut;
				return;
			}

			if (!MouseInside) {
				SR.sprite = Inactive;

			} else {
				if (Main.Data.Money > prices [ShopID]) {
					SR.sprite = Active;
					if (Input.GetKeyDown (KeyCode.Mouse0)) {

						if (Main.Data.Money >= prices [ShopID]) {
							Main.Data.Money -= prices [ShopID];
							if (Main.Data.ItemCounts [itemIDs [ShopID]] == 0) {
								Main.Data.AddItem (itemIDs [ShopID]);
								Main.Data.ItemCounts [itemIDs [ShopID]]++;
							} else {
								Main.Data.ItemCounts [itemIDs [ShopID]]++;
							}
						}
						if (ShopID != 0) {
						
							SoldOuts [ShopID] = true;
						} else {
							Main.Data.Obj_BuySeeds = true;
							remainingseeds--;
							if (remainingseeds <= 0) {
								SoldOuts [0] = true;
							}
						}

						if (ShopID == 1) {
							Main.Data.Obj_BuyTicket = true;

						}

						if (ShopID == 2) {
							Main.Data.Obj_BuyPumpkinSeed = true;

						}

						if (ShopID == 3) {
							Main.Data.Obj_BuySoil = true;

						}

					}
				} else {
					//insufficient funds
					SR.sprite = NotEnoughMoney;
				}
			}
		}
		MouseInside = false;
	}


	void OnMouseOver () {

		MouseInside = true;
	}
}
