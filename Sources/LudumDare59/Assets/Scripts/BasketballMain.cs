using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballMain : MonoBehaviour {

	public byte Timer_SubSecond;
	public byte Timer_Seconds;
	public byte SwooshAnimTimer;

	public byte PlayerAnimTimer;
	public SpriteRenderer PlayerSR;

	public SpriteRenderer Hoop1;
	public SpriteRenderer Hoop2;
	public Sprite[] Hoop1Sprites;
	public Sprite[] Hoop2Sprites;

	public Sprite[] PlayerSprites;

	public TextMesh Countdown;
	public TextMesh Score;

	public GameObject Ball;
	public SpriteRenderer DropShadow;
	public Rigidbody BallRB;

	public byte Ball_X_Hi;
	public byte Ball_X_Lo;
	public byte Ball_Y_Hi;
	public byte Ball_Y_Lo;

	public byte Ball_Euler_Hi;
	public byte Ball_Euler_Lo;

	public byte Ball_VelX_Hi;
	public byte Ball_VelX_Lo;
	public byte Ball_VelY_Hi;
	public byte Ball_VelY_Lo;

	public bool HoldingBall;
	public Camera Cam;

	public bool Checkpoint1;
	public bool Checkpoint2;
	public bool Victory;
	public bool ForceFail;

	public ChetQuizzlyAnims Chet;

	public GameObject PassCutscene;
	public GameObject FailCutscene;

	public LiveReactions GenericBasketball;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void TimelineUpdate (Vector2 ClickPos) {
		if (Global.Dataholder != null) {
			Global.Dataholder.Timeline.LiveReactionButton.CurrentLiveReaction = GenericBasketball;
		}
		PlayerAnimTimer++;
		if (PlayerAnimTimer > 31) {
			PlayerAnimTimer = 0;
		}
		PlayerSR.sprite = PlayerSprites [PlayerAnimTimer / 8];

		if (Victory) {
			SwooshAnimTimer++;
			if (SwooshAnimTimer > 23) {
				SwooshAnimTimer = 23;
			}
			Hoop1.sprite = Hoop1Sprites [SwooshAnimTimer / 4];
			Hoop2.sprite = Hoop2Sprites [SwooshAnimTimer / 4];
			Chet.CurrentAnim = 1;
		}

		if (HoldingBall) {
			BallRB.isKinematic = true;
		}

		if (ClickPos.x != 100 && HoldingBall) {
			// toss the ball!
			HoldingBall = false;
			Vector3 Aim = new Vector3 (ClickPos.x, ClickPos.y, 0);

			BallRB.velocity = (Aim - Ball.transform.position)*2;
		}

		if (!HoldingBall) {
			BallRB.isKinematic = false;
		}

		DropShadow.transform.position = new Vector3 (Ball.transform.position.x, -10.6f, 0);
		DropShadow.color = new Vector4(1,1,1,1f/((Ball.transform.position.y+11)+0.25f));
		BallRB.AddForce(new Vector3(0,-20,0)); //gravity.

		Physics.Simulate (Time.fixedDeltaTime);

		Ball.transform.localPosition = new Vector3(Mathf.Floor(Ball.transform.localPosition.x*2048),Mathf.Floor(Ball.transform.localPosition.y*2048),0);
		Ball_X_Hi = (byte)((Mathf.RoundToInt (Ball.transform.localPosition.x) & 0xFF00)>>8);
		Ball_X_Lo = (byte)(Mathf.RoundToInt (Ball.transform.localPosition.x) & 0x00FF);
		Ball_Y_Hi = (byte)((Mathf.RoundToInt (Ball.transform.localPosition.y) & 0xFF00)>>8);
		Ball_Y_Lo = (byte)(Mathf.RoundToInt (Ball.transform.localPosition.y) & 0x00FF);
		Ball.transform.localPosition = new Vector3(Ball.transform.localPosition.x/2048f,Ball.transform.localPosition.y/2048f,0);

		float BigEuler = Mathf.Floor(((Ball.transform.localEulerAngles.z + 360) % 360) * 182.044444f);
		Ball_Euler_Hi = (byte)((Mathf.RoundToInt (BigEuler) & 0xFF00)>>8);
		Ball_Euler_Lo = (byte)(Mathf.RoundToInt (BigEuler) & 0x00FF);
		Ball.transform.localEulerAngles = new Vector3 (0, 0, BigEuler / 182.044444f);

		float BigVelX = Mathf.Floor (BallRB.velocity.x * 512);
		Ball_VelX_Hi = (byte)((Mathf.RoundToInt(BigVelX) & 0xFF00)>>8);
		Ball_VelX_Lo = (byte)(Mathf.RoundToInt(BigVelX) & 0x00FF);

		float BigVelY = Mathf.Floor (BallRB.velocity.y * 512);
		Ball_VelY_Hi = (byte)((Mathf.RoundToInt(BigVelY) & 0xFF00)>>8);
		Ball_VelY_Lo = (byte)(Mathf.RoundToInt(BigVelY) & 0x00FF);

		BallRB.velocity = new Vector3 (BigVelX / 512f, BigVelY / 512f, 0);

		Chet.SR_Eye.transform.localScale = new Vector3 ((Ball.transform.position.x > Chet.transform.position.x) ? -1 : 1, 1, 1); 
		Chet.TimelineUpdate ();

		Timer_SubSecond++;
		if (Timer_SubSecond >= 60) {
			Timer_SubSecond = 0;
			Timer_Seconds++;
		}

		float TotalSeconds = Timer_Seconds + (Timer_SubSecond / 60f);

		Score.text = Victory ? "2" : "0";
		if (TotalSeconds < 5) {
			Countdown.text = "00:0" + (Mathf.Round ((5 - TotalSeconds) * 100) / 100f).ToString ();
			if (Countdown.text.Length == 7) {
				Countdown.text += "0";
			}
			if (Countdown.text.Length == 5) {
				Countdown.text += ".00";
			}
		} else {
			Countdown.text = "00:00.00";
		}

		if (TotalSeconds > 6) {
			if (Victory) {
				Global.Dataholder.Timeline.PostBasketball_Pass.LoadState (Global.Dataholder.Timeline.GameInit.MGC_Basketball_Pass_Data);
				PassCutscene.SetActive (true);
				gameObject.SetActive (false);
			} else {
				Global.Dataholder.Timeline.PostBasketball_Fail.LoadState (Global.Dataholder.Timeline.GameInit.MGC_Basketball_Fail_Data);
				FailCutscene.SetActive (true);
				gameObject.SetActive (false);
			}
		}

	}

	public List<byte> SaveState()
	{
		List<byte> State = new List<byte> ();
		State.Add (Timer_SubSecond);
		State.Add (Timer_Seconds);
		State.Add (SwooshAnimTimer);

		State.Add (Ball_X_Hi);
		State.Add (Ball_X_Lo);
		State.Add (Ball_Y_Hi);
		State.Add (Ball_Y_Lo);
		State.Add (Ball_Euler_Hi);
		State.Add (Ball_Euler_Lo);
		State.Add (Ball_VelX_Hi);
		State.Add (Ball_VelX_Lo);
		State.Add (Ball_VelY_Hi);
		State.Add (Ball_VelY_Lo);

		State.Add ((byte)(HoldingBall ? 1 : 0));
		State.Add ((byte)(Checkpoint1 ? 1 : 0));
		State.Add ((byte)(Checkpoint2 ? 1 : 0));
		State.Add ((byte)(Victory ? 1 : 0));
		State.Add ((byte)(ForceFail ? 1 : 0));

		State.Add (Chet.AnimTimer);
		State.Add (Chet.TieSpinAnimTimer);
		State.Add (Chet.CurrentAnim);
		State.Add (PlayerAnimTimer);

		return State;
	}

	public void LoadState(List<byte> State)
	{
		Timer_SubSecond = State [0];
		Timer_Seconds = State [1];
		SwooshAnimTimer = State [2];
		Ball_X_Hi = State [3];
		Ball_X_Lo = State [4];
		Ball_Y_Hi = State [5];
		Ball_Y_Lo = State [6];
		Ball_Euler_Hi = State [7];
		Ball_Euler_Lo = State [8];
		Ball_VelX_Hi = State [9];
		Ball_VelX_Lo = State [10];
		Ball_VelY_Hi = State [11];
		Ball_VelY_Lo = State [12];

		HoldingBall = State [13] == 1;
		Checkpoint1 = State [14] == 1;
		Checkpoint2 = State [15] == 1;
		Victory  = State [16] == 1;
		ForceFail  = State [17] == 1;

		Chet.AnimTimer = State [18];
		Chet.TieSpinAnimTimer = State [19];
		Chet.CurrentAnim = State [20];
		PlayerAnimTimer = State [21];

		Chet.ForceAnim (Chet.CurrentAnim);

		PlayerSR.sprite = PlayerSprites [PlayerAnimTimer / 8];


		Ball.transform.localPosition = new Vector3 ((0f + Ball_X_Lo + Ball_X_Hi * 256) / 2048f, (0f + Ball_Y_Lo + Ball_Y_Hi * 256) / 2048f, 0);
		Ball.transform.localEulerAngles = new Vector3 (0, 0, (0f + Ball_Euler_Lo + Ball_Euler_Hi * 256) / 182.044444f);
		bool NegateX = false;
		if (Ball_VelX_Hi >= 128) {
			NegateX = true;
			Ball_VelX_Hi = (byte)(256 - Ball_VelX_Hi);
			Ball_VelX_Lo = (byte)(256 - Ball_VelX_Lo);
		}
		bool NegateY = false;
		if (Ball_VelY_Hi >= 128) {
			NegateY = true;
			Ball_VelY_Hi = (byte)(256 - Ball_VelY_Hi);
			Ball_VelY_Lo = (byte)(256 - Ball_VelY_Lo);
		}
		BallRB.velocity = new Vector3 ((0f + Ball_VelX_Lo + Ball_VelX_Hi * 256) / 512f, (0f + Ball_VelY_Lo + Ball_VelY_Hi * 256) / 512f, 0);
		if (NegateX) {
			BallRB.velocity = new Vector3 (-BallRB.velocity.x, BallRB.velocity.y);
		}
		if (NegateY) {
			BallRB.velocity = new Vector3 (BallRB.velocity.x, -BallRB.velocity.y);
		}

		Chet.SR_Eye.transform.localScale = new Vector3 ((Ball.transform.position.x > Chet.transform.position.x) ? -1 : 1, 1, 1); 

		float TotalSeconds = Timer_Seconds + (Timer_SubSecond / 60f);

		Score.text = Victory ? "2" : "0";
		if (TotalSeconds < 5) {
			Countdown.text = "00:0" + (Mathf.Round ((5 - TotalSeconds) * 100) / 100f).ToString ();
			if (Countdown.text.Length == 7) {
				Countdown.text += "0";
			}
			if (Countdown.text.Length == 5) {
				Countdown.text += ".00";
			}
		} else {
			Countdown.text = "00:00.00";
		}
	}

	public void Ping(bool Ch2)
	{
		if (!Checkpoint1 && Ch2) {
			ForceFail = true;
		}
		if (!ForceFail) {

			if (!Ch2) {
				Checkpoint1 = true;
			}

			if (Checkpoint1 && Ch2) {
				Victory = true;
			}
		}

	}

}
