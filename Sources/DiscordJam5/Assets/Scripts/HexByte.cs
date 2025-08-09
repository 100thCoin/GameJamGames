using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexByte : MonoBehaviour {

	public bool Hexadecimal;

	public int Value;
	public int Goal;

	public bool ConditionMet;

	public bool Con_Equal;
	public bool Con_Greater;
	public bool Con_Less;
	public bool Con_Even;
	public bool Con_Odd;
	public bool Con_NotEqual;
	public bool Con_EndsWith;
	public bool Con_DoesntEndWith;

	public TextMesh TM;
	public TextMesh GoalTM;

	public Color NotCorrect;
	public Color Correct;
	public string Target;
	public string DynamicGoal;

	public GameObject TB_Main;
	public GameObject TB_Goal;
	public TextMesh TBTM_Main;
	public TextMesh TBTM_Goal;



	[TextArea(5,5)]
	public string GoalText;

	//Poorly made code because it's a game jam.
	public Char_Move Player;
	public Object_Move Object;
	public Object_Move Object2;

	public int TimerSeconds;
	public int StartTime;
	public float Timer;
	public DataHolder Main;

	// Use this for initialization
	public void OnEnable () {
		Timer=0;
		switch(DynamicGoal)
		{
		case "PlayerXPos": 
			Player = GameObject.Find("Player").GetComponent<Char_Move>();
			TBTM_Main.text = "Player X\nposition.";
			break;
		case "PlayerYPos": 
			Player = GameObject.Find("Player").GetComponent<Char_Move>();
			TBTM_Main.text = "Player Y\nposition.";
			break;


		case "Box1XPos": 
			Object2 = GameObject.Find("Block_1").gameObject.GetComponent<Object_Move>();
			TBTM_Main.text = "Block 1 X\nposition.";
			break;
		case "Box1YPos": 
			Object2 = GameObject.Find("Block_1").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 1 Y\nposition.";
			break;
		case "Box1XVel": 
			Object2 = GameObject.Find("Block_1").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 1 X\nvelocity.";
			break;
		case "Box1YVel": 
			Object2 = GameObject.Find("Block_1").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 1 Y\nvelocity.";
			break;

		case "Box2XPos": 
			Object2 = GameObject.Find("Block_2").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 2 X\nposition.";
			break;
		case "Box2YPos": 
			Object2 = GameObject.Find("Block_2").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 2 Y\nposition.";
			break;
		case "Box2XVel": 
			Object2 = GameObject.Find("Block_2").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 2 X\nvelocity.";
			break;
		case "Box2YVel": 
			Object2 = GameObject.Find("Block_2").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 2 Y\nvelocity.";
			break;

		case "Box3XPos": 
			Object2 = GameObject.Find("Block_3").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 3 X\nposition.";
			break;
		case "Box3YPos": 
			Object2 = GameObject.Find("Block_3").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 3 Y\nposition.";
			break;
		case "Box3XVel": 
			Object2 = GameObject.Find("Block_3").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 3 X\nvelocity.";
			break;
		case "Box3YVel": 
			Object2 = GameObject.Find("Block_3").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 3 Y\nvelocity.";
			break;

		case "Box4XPos": 
			Object2 = GameObject.Find("Block_4").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 4 X\nposition.";
			break;
		case "Box4YPos": 
			Object2 = GameObject.Find("Block_4").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 4 Y\nposition.";
			break;
		case "Box4XVel": 
			Object2 = GameObject.Find("Block_4").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 4 X\nvelocity.";
			break;
		case "Box4YVel": 
			Object2 = GameObject.Find("Block_4").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 4 Y\nvelocity.";
			break;

		case "UFOXPos": 
			Object2 = GameObject.Find("UFO").GetComponent<Object_Move>();
			TBTM_Main.text = "UFO X position.";
			break;
		case "UFOYPos": 
			Object2 = GameObject.Find("UFO").GetComponent<Object_Move>();
			TBTM_Main.text = "UFO Y position.";
			break;


		case "PlayerAlive": 
			Player = GameObject.Find("Player").GetComponent<Char_Move>();
			TBTM_Main.text = "Is the player\nalive?\n01 : yes\n00 : no.";
			break;

		case "Box1Alive": 
			Object2 = GameObject.Find("Block_1").GetComponent<Object_Move>();
			TBTM_Main.text = "Is box 1 alive?\n01 : yes\n00 : no.";
			break;

		case "Box2Alive": 
			Object2 = GameObject.Find("Block_2").GetComponent<Object_Move>();
			TBTM_Main.text = "Is box 2 alive?\n01 : yes\n00 : no.";
			break;

		case "Box3Alive": 
			Object2 = GameObject.Find("Block_3").GetComponent<Object_Move>();
			TBTM_Main.text = "Is box 3 alive?\n01 : yes\n00 : no.";
			break;

		case "Box4Alive": 
			Object2 = GameObject.Find("Block_4").GetComponent<Object_Move>();
			TBTM_Main.text = "Is box 4 alive?\n01 : yes\n00 : no.";
			break;

		case "Timer+": 
			Main = GameObject.Find("Main").GetComponent<DataHolder>();
			TBTM_Main.text = "Clock counting\nup in seconds";
			TimerSeconds = StartTime;
			break;

		case "Timer-": 
			Main = GameObject.Find("Main").GetComponent<DataHolder>();
			TBTM_Main.text = "Clock counting\ndown in seconds";
			TimerSeconds = StartTime;
			break;
		}

		switch(Target)
		{
		case "PlayerXPos": 
			Player = GameObject.Find("Player").GetComponent<Char_Move>();
			TBTM_Main.text = "Player X\nposition.";
			break;
		case "PlayerYPos": 
			Player = GameObject.Find("Player").GetComponent<Char_Move>();
			TBTM_Main.text = "Player Y\nposition.";
			break;


		case "Box1XPos": 
			Object = GameObject.Find("Block_1").gameObject.GetComponent<Object_Move>();
			TBTM_Main.text = "Block 1 X\nposition.";
			break;
		case "Box1YPos": 
			Object = GameObject.Find("Block_1").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 1 Y\nposition.";
			break;
		case "Box1XVel": 
			Object = GameObject.Find("Block_1").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 1 X\nvelocity.";
			break;
		case "Box1YVel": 
			Object = GameObject.Find("Block_1").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 1 Y\nvelocity.";
			break;

		case "Box2XPos": 
			Object = GameObject.Find("Block_2").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 2 X\nposition.";
			break;
		case "Box2YPos": 
			Object = GameObject.Find("Block_2").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 2 Y\nposition.";
			break;
		case "Box2XVel": 
			Object = GameObject.Find("Block_2").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 2 X\nvelocity.";
			break;
		case "Box2YVel": 
			Object = GameObject.Find("Block_2").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 2 Y\nvelocity.";
			break;

		case "Box3XPos": 
			Object = GameObject.Find("Block_3").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 3 X\nposition.";
			break;
		case "Box3YPos": 
			Object = GameObject.Find("Block_3").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 3 Y\nposition.";
			break;
		case "Box3XVel": 
			Object = GameObject.Find("Block_3").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 3 X\nvelocity.";
			break;
		case "Box3YVel": 
			Object = GameObject.Find("Block_3").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 3 Y\nvelocity.";
			break;

		case "Box4XPos": 
			Object = GameObject.Find("Block_4").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 4 X\nposition.";
			break;
		case "Box4YPos": 
			Object = GameObject.Find("Block_4").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 4 Y\nposition.";
			break;
		case "Box4XVel": 
			Object = GameObject.Find("Block_4").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 4 X\nvelocity.";
			break;
		case "Box4YVel": 
			Object = GameObject.Find("Block_4").GetComponent<Object_Move>();
			TBTM_Main.text = "Block 4 Y\nvelocity.";
			break;

		case "UFOXPos": 
			Object = GameObject.Find("UFO").GetComponent<Object_Move>();
			TBTM_Main.text = "UFO X position.";
			break;
		case "UFOYPos": 
			Object = GameObject.Find("UFO").GetComponent<Object_Move>();
			TBTM_Main.text = "UFO Y position.";
			break;


		case "PlayerAlive": 
			Player = GameObject.Find("Player").GetComponent<Char_Move>();
			TBTM_Main.text = "Is the player\nalive?\n01 : yes\n00 : no.";
			break;

		case "Box1Alive": 
			Object = GameObject.Find("Block_1").GetComponent<Object_Move>();
			TBTM_Main.text = "Is box 1 alive?\n01 : yes\n00 : no.";
			break;

		case "Box2Alive": 
			Object = GameObject.Find("Block_2").GetComponent<Object_Move>();
			TBTM_Main.text = "Is box 2 alive?\n01 : yes\n00 : no.";
			break;

		case "Box3Alive": 
			Object = GameObject.Find("Block_3").GetComponent<Object_Move>();
			TBTM_Main.text = "Is box 3 alive?\n01 : yes\n00 : no.";
			break;

		case "Box4Alive": 
			Object = GameObject.Find("Block_4").GetComponent<Object_Move>();
			TBTM_Main.text = "Is box 4 alive?\n01 : yes\n00 : no.";
			break;

		case "Timer+": 
			Main = GameObject.Find("Main").GetComponent<DataHolder>();
			TBTM_Main.text = "Clock counting\nup in seconds";
			TimerSeconds = StartTime;
			break;

		case "Timer-": 
			Main = GameObject.Find("Main").GetComponent<DataHolder>();
			TBTM_Main.text = "Clock counting\ndown in seconds";
			TimerSeconds = StartTime;
			break;

		default: break;
		}

		if(!Con_Even && !Con_Odd && !Con_EndsWith && !Con_DoesntEndWith)
		{
			if(Hexadecimal)
			{
				GoalTM.text = Goal.ToString("X").Length > 1 ? "" + Goal.ToString("X") : "0" + Goal.ToString("X");
			}
			else
			{
				string T = "" + Goal;
				GoalTM.text = T.Length > 1 ? T : "0" + T;
			}
		}
		TBTM_Goal.text = GoalText;
	}
	
	// Update is called once per frame
	void LateUpdate () {

		if(Main != null)
		{
			if(!Main.InGame)
			{
				return;
			}

		}

		switch(DynamicGoal)
		{
			case "PlayerXPos": 
			Goal = Hexadecimal ? Player.PosLBX*16+Player.PosHBX : Player.PosLBX*10+Player.PosHBX;
			break;

			case "PlayerYPos": 
			Goal = Hexadecimal ? Player.PosLBY*16+Player.PosHBY : Player.PosLBY*10+Player.PosHBY;
			break;

			case "Box1XPos": 
			Goal = Hexadecimal ? Object2.PosLBX*16+Object2.PosHBX : Object2.PosLBX*10+Object2.PosHBX;
			break;

			case "Box1YPos": 
			Goal = Hexadecimal ? Object2.PosLBY*16+Object2.PosHBY : Object2.PosLBY*10+Object2.PosHBY;
			break;

			case "Box2XPos": 
			Goal = Hexadecimal ? Object2.PosLBX*16+Object2.PosHBX : Object2.PosLBX*10+Object2.PosHBX;
			break;

			case "Box2YPos": 
			Goal = Hexadecimal ? Object2.PosLBY*16+Object2.PosHBY : Object2.PosLBY*10+Object2.PosHBY;
			break;

			case "Box3XPos": 
				Goal = Hexadecimal ? Object2.PosLBX*16+Object2.PosHBX : Object2.PosLBX*10+Object2.PosHBX;
				break;

			case "Box3YPos": 
			Goal = Hexadecimal ? Object2.PosLBY*16+Object2.PosHBY : Object2.PosLBY*10+Object2.PosHBY;
				break;

			case "Box4XPos": 
			Goal = Hexadecimal ? Object2.PosLBX*16+Object2.PosHBX : Object2.PosLBX*10+Object2.PosHBX;
			break;

			case "Box4YPos": 
			Goal = Hexadecimal ? Object2.PosLBY*16+Object2.PosHBY : Object2.PosLBY*10+Object2.PosHBY;
			break;

			case "UFOXPos": 
			Goal = Hexadecimal ? Object2.PosLBX*16+Object2.PosHBX : Object2.PosLBX*10+Object2.PosHBX;
			break;

			case "UFOYPos": 
			Goal = Hexadecimal ? Object2.PosLBY*16+Object2.PosHBY : Object2.PosLBY*10+Object2.PosHBY;
			break;

			default : break;

		}

		if(DynamicGoal != "")
		{
			if(!Con_Even && !Con_Odd && !Con_EndsWith && !Con_DoesntEndWith)
			{
				if(Hexadecimal)
				{
					GoalTM.text = Goal.ToString("X").Length > 1 ? "" + Goal.ToString("X") : "0" + Goal.ToString("X");
				}
				else
				{
					string T = "" + Goal;
					GoalTM.text = T.Length > 1 ? T : "0" + T;
				}
			}
		}


		switch(Target)
		{
			case "PlayerXPos": 
			Value = Hexadecimal ? Player.PosLBX*16+Player.PosHBX : Player.PosLBX*10+Player.PosHBX;
			break;

			case "PlayerYPos": 
			Value = Hexadecimal ? Player.PosLBY*16+Player.PosHBY : Player.PosLBY*10+Player.PosHBY;
			break;

			case "Box1XPos": 
			Value = Hexadecimal ? Object.PosLBX*16+Object.PosHBX : Object.PosLBX*10+Object.PosHBX;
			break;
			case "Box2XPos": 
			Value = Hexadecimal ? Object.PosLBX*16+Object.PosHBX : Object.PosLBX*10+Object.PosHBX;
			break;
			case "Box3XPos": 
			Value = Hexadecimal ? Object.PosLBX*16+Object.PosHBX : Object.PosLBX*10+Object.PosHBX;
			break;
			case "Box4XPos": 
			Value = Hexadecimal ? Object.PosLBX*16+Object.PosHBX : Object.PosLBX*10+Object.PosHBX;
			break;

			case "Box1YPos": 
			Value = Hexadecimal ? Object.PosLBY*16+Object.PosHBY : Object.PosLBY*10+Object.PosHBY;
			break;
			case "Box2YPos": 
			Value = Hexadecimal ? Object.PosLBY*16+Object.PosHBY : Object.PosLBY*10+Object.PosHBY;
			break;
			case "Box3YPos":
			Value = Hexadecimal ? Object.PosLBY*16+Object.PosHBY : Object.PosLBY*10+Object.PosHBY;
			break;
			case "Box4YPos": 
			Value = Hexadecimal ? Object.PosLBY*16+Object.PosHBY : Object.PosLBY*10+Object.PosHBY;
			break;

			case "Box1XVel": 
			Value = Hexadecimal ? Object.SpeedX <0 ? Object.SpeedX + 256 : Object.SpeedX : Object.SpeedX <0 ? Object.SpeedX + 100 : Object.SpeedX;
			break;
			case "Box2XVel": 
			Value = Hexadecimal ? Object.SpeedX <0 ? Object.SpeedX + 256 : Object.SpeedX : Object.SpeedX <0 ? Object.SpeedX + 100 : Object.SpeedX;
			break;
			case "Box3XVel": 
			Value = Hexadecimal ? Object.SpeedX <0 ? Object.SpeedX + 256 : Object.SpeedX : Object.SpeedX <0 ? Object.SpeedX + 100 : Object.SpeedX;
			break;
			case "Box4XVel": 
			Value = Hexadecimal ? Object.SpeedX <0 ? Object.SpeedX + 256 : Object.SpeedX : Object.SpeedX <0 ? Object.SpeedX + 100 : Object.SpeedX;
			break;

			case "UFOXPos": 
			Value = Hexadecimal ? Object.PosLBX*16+Object.PosHBX : Object.PosLBX*10+Object.PosHBX;
			break;
		
			case "UFOYPos": 
			Value = Hexadecimal ? Object.PosLBY*16+Object.PosHBY : Object.PosLBY*10+Object.PosHBY;
			break;

			case "PlayerAlive":
			Value = Player.Dead ? 00 : 01;
			break;

			case "Box1Alive":
			Value = Object.Dead ? 00 : 01;
			break;

			case "Box2Alive":
			Value = Object.Dead ? 00 : 01;
			break;

			case "Box3Alive":
			Value = Object.Dead ? 00 : 01;
			break;

			case "Box4Alive":
			Value = Object.Dead ? 00 : 01;
			break;

			case "Timer+":
			if(!Main.PACE.Happening2)
			{
				Timer+=Time.deltaTime;
				if(Timer>1){Timer-=1;TimerSeconds++;if(Hexadecimal){if(TimerSeconds > 255){TimerSeconds = 255;}}else{if(TimerSeconds > 99){TimerSeconds = 99;}}}
				Value = TimerSeconds;
			}
			break;

			case "Timer-":
			if(!Main.PACE.Happening2)
			{
				Timer+=Time.deltaTime;
				if(Timer>1){Timer-=1;TimerSeconds--;if(TimerSeconds < 0){TimerSeconds = 0;}}
				Value = TimerSeconds;
			}
			break;

			default: break;
		}
		if(Hexadecimal)
		{
			TM.text = Value.ToString("X").Length > 1 ? "" + Value.ToString("X") : "0" + Value.ToString("X");
		}
		else
		{
			string T = "" + Value;
			TM.text = T.Length > 1 ? T : "0" + T;
		}

		if(Con_Equal)
		{
			ConditionMet = Value == Goal;
		}
		else if(Con_Greater)
		{
			ConditionMet = Value > Goal;
		}
		else if(Con_Less)
		{
			ConditionMet = Value < Goal;
		}
		else if(Con_Even)
		{
			ConditionMet = Value %2 == 0;
		}
		else if(Con_Odd)
		{
			ConditionMet = Value %2 == 1;
		}
		else if(Con_NotEqual)
		{
			ConditionMet = Value != Goal;
		}
		else if(Con_EndsWith)
		{
			string t = Hexadecimal ? Value.ToString("X") : Value.ToString();
			if(t.Length > 1){t=t.Substring(1,1);}
			ConditionMet = t == ""+Goal;
		}
		else if(Con_DoesntEndWith)
		{
			string t = Hexadecimal ? Value.ToString("X") : Value.ToString();
			if(t.Length > 1){t=t.Substring(1,1);}
			ConditionMet = t != ""+Goal;
		}
		TM.color = ConditionMet ? Correct : NotCorrect;



	}
}
